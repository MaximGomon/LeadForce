using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Configuration;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Services.TemplateTagsReplacers;
using WebCounter.DataAccessLayer;
using System.IO;

namespace Labitec.LeadForce.CronJobs
{
    public class SendMailJob : ICronJob
    {
        public void Run()
        {
            DataManager dataManager = new DataManager();

            string msg = "";
            bool doSend = true;

            try
            {
                // Sites
                var sites = dataManager.Sites.SelectAll().Where(a => a.IsActive);
                foreach (var site in sites)
                {
                    try
                    {
                        // Site actions
                        List<tbl_SiteAction> siteActions = null;
                        siteActions =
                            dataManager.SiteAction.SelectAll(site.ID).Where(
                                a =>
                                a.ActionDate <= DateTime.Now &&
                                (ActionStatus) a.ActionStatusID == ActionStatus.Scheduled).ToList();
                        

                        //Поиск дубликатов
                        var duplicates =
                            siteActions.GroupBy(sa => new {sa.SiteActionTemplateID, sa.ContactID}).Where(
                                x => x.Count() > 1).Select(group => new {group.Key}).ToList();
                        foreach (var duplicate in duplicates)
                        {
                            var contactId = duplicate.Key.ContactID;
                            var siteActionTemplateId = duplicate.Key.SiteActionTemplateID;
                            //Выбираются все дубликаты пропуская первый
                            var siteActionsToCancel =
                                siteActions.Where(
                                    sa => sa.ContactID == contactId && sa.SiteActionTemplateID == siteActionTemplateId).
                                    OrderByDescending(sa => sa.ActionDate).Skip(1).ToList();
                            //Установка дубликатам статуса Отменен
                            foreach (var siteAction in siteActionsToCancel)
                            {
                                if (!siteAction.SiteActionTemplateID.HasValue ||
                                    siteAction.tbl_SiteActionTemplate.SiteActionTemplateCategoryID !=
                                    (int) SiteActionTemplateCategory.System)
                                {
                                    siteAction.ActionStatusID = (int) ActionStatus.Cancelled;
                                    dataManager.SiteAction.Update(siteAction);
                                    WorkflowProcessing.Processing(
                                        WorkflowProcessing.WorkflowElementByValue(siteAction.ID),
                                        ((int) ActionStatus.Error).ToString());
                                }
                            }
                        }
                        
                        //Если найдены дубликаты заново выбираем события
                        if (duplicates.Count > 0)
                            siteActions =
                                dataManager.SiteAction.SelectAll(site.ID).Where(
                                    a =>
                                    a.ActionDate <= DateTime.Now &&
                                    (ActionStatus) a.ActionStatusID == ActionStatus.Scheduled).ToList();
                        
                        var siteActionGroups = siteActions.GroupBy(o => o.SiteActionTemplateID).Select(o => new {SiteActionTemplateId = o.Key, Count = o.Count()}).ToList();
                        
                        foreach (var siteAction in siteActions)
                        {
                            msg = "";
                            doSend = true;
                            
                            var siteActionTemplate = siteAction.tbl_SiteActionTemplate;
                            var body = siteActionTemplate.MessageBody;

                            if (siteActionTemplate.ParentID.HasValue)
                            {
                                var parentSiteActionTemplate =
                                    dataManager.SiteActionTemplate.SelectById((Guid) siteActionTemplate.ParentID);
                                if (parentSiteActionTemplate != null &&
                                    parentSiteActionTemplate.MessageBody.Contains("#Text#"))
                                {
                                    body = parentSiteActionTemplate.MessageBody.Replace("#Text#",
                                                                                        siteActionTemplate.MessageBody);
                                }
                            }

                            var subject = siteAction.tbl_SiteActionTemplate.MessageCaption;

                            var contact = dataManager.Contact.SelectById(site.ID, (Guid) siteAction.ContactID);

                            if (body.Contains("#Advert#"))
                                body = body.Replace("#Advert#", GetAdvertBlock());
                            else if (site.ServiceAdvertisingActionID != null)
                            {
                                switch ((EmailAction) site.ServiceAdvertisingActionID)
                                {
                                    case EmailAction.DoNotSend:
                                        doSend = false;
                                        break;
                                    case EmailAction.Auto:
                                        body = body + "<br/>" + GetAdvertBlock();
                                        break;
                                }
                            }
                            
                            var mailMessage = new MailMessage();

                            if (site.IsSendFromLeadForce)
                                mailMessage.Sender = new MailAddress("postman@leadforce.ru", "LeadForce");

                            if (siteAction.tbl_SiteActionTagValue.Any(o => o.Tag == "#System.SenderEmail#"))
                            {
                                mailMessage.From = new MailAddress(siteAction.tbl_SiteActionTagValue.FirstOrDefault(o => o.Tag == "#System.SenderEmail#").Value,
                                    siteAction.tbl_SiteActionTagValue.FirstOrDefault(o => o.Tag == "#System.SenderUserFullName#").Value);
                            }
                            else
                            {
                                if (siteActionTemplate.FromContactRoleID.HasValue)
                                {
                                    var contactRole = dataManager.ContactRole.SelectById(site.ID, (Guid)siteActionTemplate.FromContactRoleID);
                                    if (contactRole != null)
                                    {
                                        tbl_Contact responsibleContact = null;
                                        switch ((ContactRoleType)contactRole.RoleTypeID)
                                        {
                                            case ContactRoleType.GeneralEmail:
                                                mailMessage.From = new MailAddress(contactRole.Email, contactRole.DisplayName);
                                                break;
                                            case ContactRoleType.ContactRole:
                                                responsibleContact = GetResponsible(site.ID, contactRole.ID, (Guid)siteAction.ContactID);
                                                if (responsibleContact != null)
                                                    mailMessage.From = new MailAddress(responsibleContact.Email, responsibleContact.UserFullName);
                                                break;
                                            case ContactRoleType.WorkflowRole:
                                                responsibleContact = GetResponsible(site.ID, contactRole.ID, (Guid)siteAction.ContactID, WorkflowProcessing.GetWorkflowIdByValue(siteAction.ID));
                                                if (responsibleContact != null)
                                                    mailMessage.From = new MailAddress(responsibleContact.Email, responsibleContact.UserFullName);
                                                break;
                                        }
                                        if ((ContactRoleType)contactRole.RoleTypeID != ContactRoleType.GeneralEmail && responsibleContact == null)
                                        {
                                            msg = "Отсутствует отправитель";
                                            doSend = false;
                                        }
                                    }
                                }
                                else
                                    mailMessage.From = new MailAddress(siteActionTemplate.FromEmail, siteActionTemplate.FromName);
                            }
                            
                            mailMessage.Headers.Add("Message-ID", string.Format("<{0}@{1}>", Guid.NewGuid(), mailMessage.From.Host));
                            mailMessage.HeadersEncoding = Encoding.UTF8;

                            if (siteActionGroups.SingleOrDefault(o => o.SiteActionTemplateId == siteAction.SiteActionTemplateID).Count > 5)
                                mailMessage.Headers.Add("Precedence", "bulk");

                            if (!string.IsNullOrEmpty(siteActionTemplate.ReplyToEmail))
                                mailMessage.ReplyToList.Add(new MailAddress(siteActionTemplate.ReplyToEmail,
                                                                            siteActionTemplate.ReplyToName));

                            bool isIncorrectEmail = false;

                            switch ((ActionType) siteActionTemplate.ActionTypeID)
                            {
                                case ActionType.EmailToUser:
                                    if (string.IsNullOrEmpty(contact.Email) ||
                                        !Regex.IsMatch(contact.Email.Trim(),
                                                       @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                                    {
                                        msg = "Неверный E-mail посетителя";
                                        isIncorrectEmail = true;
                                        doSend = false;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mailMessage.To.Add(new MailAddress(contact.Email));
                                        }
                                        catch (Exception)
                                        {
                                            msg = "Неверный E-mail посетителя";
                                            isIncorrectEmail = true;
                                            doSend = false;
                                        }
                                    }
                                    break;
                                case ActionType.EmailToFixed:
                                    var recipients = GetSiteActionTemplateRecipients(site.ID, siteAction.ID, siteActionTemplate.ID, (Guid)siteAction.ContactID);
                                    if (recipients.Any())
                                    {
                                        foreach (var recipient in recipients)
                                        {
                                            mailMessage.To.Add(!string.IsNullOrEmpty(recipient.DisplayName)
                                                                   ? new MailAddress(recipient.Email, recipient.DisplayName)
                                                                   : new MailAddress(recipient.Email));
                                        }
                                    }
                                    else
                                    {
                                        msg = "Отсутствуют получатели";
                                        doSend = false;
                                    }
                                    break;
                            }
                            
                            if (siteAction.MessageTypeID.HasValue)
                            {
                                TemplateTagsReplacer templateTagsReplacer = null;

                                switch ((MessageType) siteAction.MessageTypeID)
                                {
                                    case MessageType.TaskNotification:
                                        templateTagsReplacer = new TaskNotificationTagsReplacer(siteAction);
                                        if (!body.Contains(GetAdvertBlock()))
                                        {
                                            if (body.Contains("#Advert#"))
                                                body = body.Replace("#Advert#", GetAdvertBlock());
                                            else
                                                body = body + "<br/>" + GetAdvertBlock();
                                        }
                                        break;
                                    case MessageType.RequestNotification:
                                        templateTagsReplacer = new RequestNotificationTagsReplacer(siteAction);
                                        break;
                                    case MessageType.RequirementCommentNotification:
                                        templateTagsReplacer = new RequirementCommentNotificationTagsReplacer(siteAction);
                                        break;
                                    case MessageType.RequestCommentNotification:
                                        templateTagsReplacer = new RequestCommentNotificationTagsReplacer(siteAction);
                                        break;
                                    case MessageType.InvoiceCommentNotification:
                                        templateTagsReplacer = new InvoiceCommentNotificationTagsReplacer(siteAction);
                                        break;
                                    case MessageType.InvoiceNotification:
                                        templateTagsReplacer = new InvoiceNotificationTagsReplacer(siteAction, ref mailMessage); 
                                        break;
                                }

                                if (templateTagsReplacer != null)
                                    templateTagsReplacer.Replace(ref subject, ref body);                                
                            }
                            
                            body = body.Replace("=\"/files/",
                                                "=\"" +
                                                WebCounter.BusinessLogicLayer.Configuration.Settings.LeadForceSiteUrl +
                                                "/files/");                            

                            var user = dataManager.User.SelectByContactId(site.ID, contact.ID);
                            if (user != null)
                            {
                                if (siteAction.ObjectID.HasValue)
                                    body = body.Replace("#Activation.Url#",
                                                        WebCounter.BusinessLogicLayer.Configuration.Settings.
                                                            LabitecLeadForcePortalActivateUserUrl(
                                                                (Guid) siteAction.ObjectID, user.ID));

                                if (body.Contains("#User.Password#"))
                                {
                                    body = body.Replace("#User.Password#", user.Password)
                                        .Replace("#User.Email#", user.Login);
                                }
                            }

                            body = body.Replace("#User.UserFullName#", contact.UserFullName ?? "")
                                .Replace("#User.LastName#", contact.Surname ?? "")
                                .Replace("#User.FirstName#", contact.Name ?? "")
                                .Replace("#User.MiddleName#", contact.Patronymic ?? "")
                                .Replace("#User.Email#", contact.Email ?? "")
                                .Replace("#User.Phone#", contact.Phone ?? "")
                                .Replace("#User.Score#", contact.Score.ToString());

                            subject = subject.Replace("#User.UserFullName#", contact.UserFullName ?? "")
                                .Replace("#User.LastName#", contact.Surname ?? "")
                                .Replace("#User.FirstName#", contact.Name ?? "")
                                .Replace("#User.MiddleName#", contact.Patronymic ?? "")
                                .Replace("#User.Email#", contact.Email ?? "")
                                .Replace("#User.Phone#", contact.Phone ?? "")
                                .Replace("#User.Score#", contact.Score.ToString());

                            var portalLink = string.Format("<a href='{0}'>{0}</a>",
                                                           dataManager.PortalSettings.SelectPortalLink(
                                                               siteAction.SiteID, false));
                            subject = subject.Replace("#Portal.Link#", portalLink);
                            body = body.Replace("#Portal.Link#", portalLink);

                            var r = new Regex(@"#User.[\S]+?#");
                            var results = r.Matches(body);
                            foreach (Match result in results)
                            {
                                var siteColumns = dataManager.SiteColumns.SelectByCode(site.ID,
                                                                                       result.Value.Replace("#User.", "")
                                                                                           .Replace("#", ""));
                                if (siteColumns != null)
                                {
                                    var contactColumnValue = dataManager.ContactColumnValues.Select(contact.ID,
                                                                                                    siteColumns.ID);
                                    if (contactColumnValue != null)
                                    {
                                        switch ((ColumnType) contactColumnValue.tbl_SiteColumns.TypeID)
                                        {
                                            case ColumnType.String:
                                            case ColumnType.Number:
                                            case ColumnType.Text:
                                                body = body.Replace(result.Value,
                                                                    contactColumnValue.StringValue.Replace("[BR]", "\n"));
                                                subject = subject.Replace(result.Value,
                                                                          contactColumnValue.StringValue.Replace(
                                                                              "[BR]", ""));
                                                break;
                                            case ColumnType.Date:
                                                body = body.Replace(result.Value,
                                                                    ((DateTime) contactColumnValue.DateValue).ToString(
                                                                        "dd.MM.yyyy HH:mm"));
                                                subject = subject.Replace(result.Value,
                                                                          ((DateTime) contactColumnValue.DateValue).
                                                                              ToString("dd.MM.yyyy HH:mm"));
                                                break;
                                            case ColumnType.Enum:
                                                body = body.Replace(result.Value,
                                                                    contactColumnValue.tbl_SiteColumnValues.Value);
                                                subject = subject.Replace(result.Value,
                                                                          contactColumnValue.tbl_SiteColumnValues.Value);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        body = body.Replace(result.Value, "");
                                        subject = subject.Replace(result.Value, "");
                                    }
                                }
                            }

                            MatchCollection matches;
                            
                            if (siteActionTemplate.ReplaceLinksID != (int) ReplaceLinks.None)
                            {
                                //matches = Regex.Matches(body, "<a(.*)href=\"(?<href>\\S*)\"(.*)>(?<name>.*)</a>", RegexOptions.IgnoreCase);
                                matches = Regex.Matches(body, @"<a.*?href=[""'](?<href>.*?)[""'].*?>(?<name>.*?)</a>",
                                                        RegexOptions.IgnoreCase);
                                foreach (Match match in matches)
                                {
                                    if (match.Groups["href"].Value.Contains("#Link."))
                                        continue;

                                    var siteActionLink = dataManager.SiteActionLink.Select(contact.ID, siteAction.ID,
                                                                                           siteAction.
                                                                                               tbl_SiteActionTemplate.ID,
                                                                                           match.Groups["href"].Value);
                                    if (siteActionLink == null)
                                    {
                                        siteActionLink = new tbl_SiteActionLink
                                                             {
                                                                 ContactID = contact.ID,
                                                                 SiteActionID = siteAction.ID,
                                                                 SiteActionTemplateID = siteActionTemplate.ID,
                                                                 LinkURL = match.Groups["href"].Value
                                                             };
                                        dataManager.SiteActionLink.Add(siteActionLink);
                                    }
                                    //match.Groups[0].ToString()
                                    //body = Regex.Replace(body, string.Format("<a(.*)href=\"{0}\"(.*)>{1}</a>", Regex.Escape(match.Groups["href"].Value), Regex.Escape(match.Groups["name"].Value)), string.Format("<a href=\"{0}/linkService.aspx?ID={1}\" target=\"_blank\">{2}</a>", WebConfigurationManager.AppSettings["webServiceUrl"], siteActionLink.ID, match.Groups["name"].Value), RegexOptions.IgnoreCase);
                                    if (siteActionTemplate.ReplaceLinksID == (int) ReplaceLinks.ThroughService)
                                        body = body.Replace(match.Groups[0].ToString(),
                                                            string.Format(
                                                                "<a href=\"{0}/linkService.aspx?ID={1}\" target=\"_blank\">{2}</a>",
                                                                WebConfigurationManager.AppSettings["webServiceUrl"],
                                                                siteActionLink.ID, match.Groups["name"].Value));
                                    else if (siteActionTemplate.ReplaceLinksID == (int) ReplaceLinks.GoogleLinks)
                                    {
                                        const string utmSource = "LeadForce";
                                        const string utmMedium = "email";
                                        var utmTerm = match.Groups["name"].Value;
                                        utmTerm = Regex.Replace(utmTerm, @"<[^>]*>", string.Empty);
                                        var utmContent = siteActionLink.ID.ToString();
                                        var massMail =
                                            dataManager.MassMail.SelectBySiteActionTemplateId(siteAction.SiteID,
                                                                                              siteActionTemplate.ID);
                                        var utmCampaign = massMail != null ? massMail.Name : subject;
                                        var queryParams =
                                            string.Format(
                                                "utm_source={0}&utm_medium={1}&utm_term={2}&utm_content={3}&utm_campaign={4}",
                                                utmSource, utmMedium, utmTerm, utmContent, utmCampaign);

                                        var url = match.Groups["href"].Value;

                                        try
                                        {
                                            Uri outUrl = null;
                                            if (Uri.TryCreate(url, UriKind.Absolute, out outUrl))
                                            {
                                                if (string.IsNullOrEmpty(outUrl.Query))
                                                    url += "?" + queryParams;
                                                else
                                                    url += "&" + queryParams;
                                            }
                                            else
                                                url += "?" + queryParams;
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("Не верный формат ссылки", ex);
                                            url += "?" + queryParams;
                                        }

                                        body = body.Replace(match.Groups[0].ToString(),
                                                            string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>",
                                                                          url, match.Groups["name"].Value));
                                    }
                                }
                            }
                            
                            matches = Regex.Matches(body, @"#Link.(?<code>[\S]+)#", RegexOptions.IgnoreCase);
                            foreach (Match match in matches)
                            {
                                var siteActionLink = dataManager.SiteActionLink.Select(contact.ID, siteAction.ID,
                                                                                       siteAction.tbl_SiteActionTemplate.ID,
                                                                                       dataManager.Links.Select(site.ID,match.Groups["code"].Value).ID);                                
                                if (siteActionLink == null)
                                {
                                    siteActionLink = new tbl_SiteActionLink
                                                         {
                                                             ContactID = contact.ID,
                                                             SiteActionID = siteAction.ID,
                                                             SiteActionTemplateID = siteAction.tbl_SiteActionTemplate.ID,
                                                             SiteActivityRuleID = dataManager.Links.Select(site.ID,match.Groups["code"].Value).ID
                                                         };
                                    dataManager.SiteActionLink.Add(siteActionLink);
                                }
                                
                                body = Regex.Replace(body,
                                                     string.Format("#Link.{0}#",
                                                                   Regex.Escape(match.Groups["code"].Value)),
                                                     string.Format("{0}/linkService.aspx?ID={1}",
                                                                   WebConfigurationManager.AppSettings["webServiceUrl"],
                                                                   siteActionLink.ID), RegexOptions.IgnoreCase);                                
                            }

                            if (body.Contains("#Unsubscribe#") || body.Contains("#User.Unsubscribe#") || body.Contains("#User.UnsubscribeLink#"))
                            {
                                if (body.Contains("#Unsubscribe#") || body.Contains("#User.Unsubscribe#"))
                                {
                                    body = body.Replace("#Unsubscribe#", GetUnsubscribeBlock(contact.ID, site.ID));
                                    body = body.Replace("#User.Unsubscribe#", GetUnsubscribeBlock(contact.ID, site.ID));
                                }
                                if (body.Contains("#User.UnsubscribeLink#"))
                                    body = body.Replace("#User.UnsubscribeLink#", GetUnsubscribeLink(contact.ID, site.ID));
                            }
                            else
                            {
                                if (!site.UnsubscribeActionID.HasValue)
                                    body = body + "<br/>" + GetUnsubscribeBlock(contact.ID, site.ID);
                                else
                                {
                                    switch ((EmailAction)site.UnsubscribeActionID)
                                    {
                                        case EmailAction.DoNotSend:
                                            doSend = false;
                                            break;
                                        case EmailAction.Auto:
                                            body = body + "<br/>" + GetUnsubscribeBlock(contact.ID, site.ID);
                                            break;
                                    }
                                }
                            }                            
                            foreach (var siteActionTagValue in siteAction.tbl_SiteActionTagValue)
                            {
                                subject = subject.Replace(siteActionTagValue.Tag, siteActionTagValue.Value);
                                body = body.Replace(siteActionTagValue.Tag, siteActionTagValue.Value);
                            }                            
                            //mailMessage.Subject = siteAction.tbl_SiteActionTemplate.MessageCaption;
                            //byte[] bytes = Encoding.Default.GetBytes(subject);
                            //subject = Encoding.UTF8.GetString(bytes);
                            mailMessage.Subject = subject.Replace(Environment.NewLine, string.Empty);
                            mailMessage.SubjectEncoding = Encoding.GetEncoding(1251);
                            mailMessage.Body = body;                            
                            mailMessage.IsBodyHtml = true;                            

                            try
                            {
                                siteAction.MessageTitle = mailMessage.Subject;
                                siteAction.MessageText = mailMessage.Body;

                                if ((ActionType)siteActionTemplate.ActionTypeID == ActionType.EmailToUser)
                                {
                                    var emailStats = dataManager.EmailStats.SelectByEmail(contact.Email);

                                    //Если у контакта стоит статус email "Не работает" или "Запрещен" или не корректный email не отсылать почту
                                    if (isIncorrectEmail || contact.EmailStatusID == (int) EmailStatus.DoesNotWork ||
                                        contact.EmailStatusID == (int) EmailStatus.Banned || 
                                        (emailStats != null && (emailStats.ReturnCount > 5 || emailStats.tbl_EmailStatsUnsubscribe.Any(o => o.SiteID == contact.SiteID))))
                                    {
                                        siteAction.ActionStatusID = (int) ActionStatus.InvalidEmail;
                                        if (contact.EmailStatusID.HasValue)
                                        {
                                            switch ((EmailStatus) contact.EmailStatusID)
                                            {
                                                case EmailStatus.DoesNotWork:
                                                    siteAction.Comments = "E-mail контакта со статусом \"Не работает\"";
                                                    break;
                                                case EmailStatus.Banned:
                                                    siteAction.Comments = "E-mail контакта со статусом \"Запрещен\"";
                                                    break;
                                                default:
                                                    siteAction.Comments = msg;
                                                    break;
                                            }
                                        }
                                        else
                                            siteAction.Comments = msg;

                                        if (emailStats != null && (emailStats.ReturnCount > 5 || emailStats.tbl_EmailStatsUnsubscribe.Any(o => o.SiteID == contact.SiteID)))
                                        {
                                            if (emailStats.ReturnCount > 5)
                                                siteAction.Comments = "Большое количество возвратов. Нужно проверить корректность e-mail.";
                                            if (emailStats.tbl_EmailStatsUnsubscribe.Any(o => o.SiteID == contact.SiteID))
                                                siteAction.Comments = "Контакт отписан.";
                                        }

                                        dataManager.SiteAction.Update(siteAction);
                                        WorkflowProcessing.Processing(
                                            WorkflowProcessing.WorkflowElementByValue(siteAction.ID),
                                            ((int) ActionStatus.Error).ToString());
                                        continue;
                                    }
                                }

                                if (doSend)
                                {
                                    var smtpClient = new SmtpClient();
                                    if (!string.IsNullOrEmpty(site.SmtpHost))
                                    {
                                        if (site.SmtpPort != null)
                                            smtpClient = new SmtpClient(site.SmtpHost, (int) site.SmtpPort);
                                        else
                                            smtpClient = new SmtpClient(site.SmtpHost);
                                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        //smtpClient.EnableSsl = true;
                                        smtpClient.Credentials = new NetworkCredential(site.SmtpUsername,
                                                                                       site.SmtpPassword);
                                    }
                                    smtpClient.Send(mailMessage);

                                    Thread.Sleep(1000);

                                    dataManager.SiteActionTagValue.Delete(siteAction.ID);

                                    siteAction.ActionStatusID = (int) ActionStatus.Done;
                                    var sendedTo = string.Empty;
                                    foreach (var item in mailMessage.To)
                                        sendedTo = sendedTo + item.Address + ", ";
                                    sendedTo = sendedTo.TrimEnd().TrimEnd(',');
                                    siteAction.Comments = string.Format("Отправлено на {0}", sendedTo);
                                    //siteAction.Comments = string.Format("Отправлено на {0}", mailMessage.To[0].Address);
                                    dataManager.SiteAction.Update(siteAction);
                                    WorkflowProcessing.Processing(
                                        WorkflowProcessing.WorkflowElementByValue(siteAction.ID),
                                        ((int) ActionStatus.Done).ToString());
                                }
                                else
                                {
                                    siteAction.ActionStatusID = (int) ActionStatus.Error;
                                    siteAction.Comments = msg;
                                    dataManager.SiteAction.Update(siteAction);
                                    WorkflowProcessing.Processing(
                                        WorkflowProcessing.WorkflowElementByValue(siteAction.ID),
                                        ((int) ActionStatus.Error).ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                siteAction.ActionStatusID = (int) ActionStatus.Error;
                                siteAction.Comments = ex.ToString();
                                dataManager.SiteAction.Update(siteAction);
                                WorkflowProcessing.Processing(WorkflowProcessing.WorkflowElementByValue(siteAction.ID),
                                                              ((int) ActionStatus.Error).ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(string.Format("Scheduler ERROR: {0}", site.ID), ex);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log.Error("Scheduler ERROR: ", ex);
            }
        }



        /// <summary>
        /// Gets the advert block.
        /// </summary>
        /// <returns></returns>
        private string GetAdvertBlock()
        {
            return "Рассылка выполнена сервисом <a href=\"http://www.leadforce.ru\">LeadForce&trade;</a>";
        }



        /// <summary>
        /// Gets the unsubscribe block.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public string GetUnsubscribeBlock(Guid contactId, Guid siteId)
        {
            return string.Format("<a href=\"{0}\">Отписаться от получения рассылки</a>",
                          string.Format(ConfigurationManager.AppSettings["unsubscribeUrl"], contactId, siteId));
        }



        public string GetUnsubscribeLink(Guid contactId, Guid siteId)
        {
            return string.Format("<a href=\"{0}\">{0}</a>",
                          string.Format(ConfigurationManager.AppSettings["unsubscribeUrl"], contactId, siteId));
        }



        /// <summary>
        /// Gets the site action template recipients.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActionTemplateId">The site action template id.</param>
        /// <returns></returns>
        public List<Recipient> GetSiteActionTemplateRecipients(Guid siteId, Guid siteActionId, Guid siteActionTemplateId, Guid contactId)
        {
            var dataManager = new DataManager();
            var recipients = new List<Recipient>();

            var siteActionTemplateRecipients = dataManager.SiteActionTemplateRecipient.SelectAll(siteActionTemplateId);
            foreach (var siteActionTemplateRecipient in siteActionTemplateRecipients)
            {
                // Если получатель Контакт
                tbl_Contact contact;
                if (siteActionTemplateRecipient.ContactID.HasValue)
                {
                    contact = dataManager.Contact.SelectById(siteId, (Guid)siteActionTemplateRecipient.ContactID);
                    if (contact != null)
                        recipients.Add(AddRecipient(contact.Email, contact.UserFullName));
                }

                // Если получатель Роль
                if (siteActionTemplateRecipient.ContactRoleID.HasValue)
                {
                    var contactRole = dataManager.ContactRole.SelectById(siteId, (Guid)siteActionTemplateRecipient.ContactRoleID);
                    if (contactRole != null)
                    {
                        tbl_Contact responsibleContact = null;
                        switch ((ContactRoleType)contactRole.RoleTypeID)
                        {
                            case ContactRoleType.GeneralEmail:
                                recipients.Add(AddRecipient(contactRole.Email, contactRole.DisplayName));
                                break;
                            case ContactRoleType.ContactRole:
                                responsibleContact = GetResponsible(siteId, contactRole.ID, contactId);
                                if (responsibleContact != null)
                                    recipients.Add(AddRecipient(responsibleContact.Email, responsibleContact.UserFullName));
                                break;
                            case ContactRoleType.WorkflowRole:
                                responsibleContact = GetResponsible(siteId, contactRole.ID, contactId, WorkflowProcessing.GetWorkflowIdByValue(siteActionId));
                                if (responsibleContact != null)
                                    recipients.Add(AddRecipient(responsibleContact.Email, responsibleContact.UserFullName));
                                break;
                        }
                    }
                }

                if (!siteActionTemplateRecipient.ContactID.HasValue && !siteActionTemplateRecipient.ContactRoleID.HasValue)
                {
                    recipients.Add(AddRecipient(siteActionTemplateRecipient.Email, siteActionTemplateRecipient.DisplayName));
                }
            }

            return recipients;
        }


        /// <summary>
        /// Adds the recipient.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="displayName">The display name.</param>
        /// <returns></returns>
        public Recipient AddRecipient(string email, string displayName)
        {
            return !string.IsNullOrEmpty(displayName)
                       ? new Recipient { Email = email, DisplayName = displayName }
                       : new Recipient { Email = email };
        }



        /// <summary>
        /// Gets the responsible.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactRoleId">The contact role id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="workflowId">The workflow id.</param>
        /// <returns></returns>
        public tbl_Contact GetResponsible(Guid siteId, Guid contactRoleId, Guid contactId, Guid? workflowId = null)
        {
            var dataManager = new DataManager();
            tbl_Contact responsibleContact = null;

            var responsible = dataManager.Responsible.Select(contactRoleId, contactId, workflowId);
            if (responsible == null)
            {
                responsibleContact = dataManager.Responsible.GetResponsible(siteId, contactRoleId);
                if (responsibleContact != null)
                {
                    responsible = new tbl_Responsible { ContactRoleID = contactRoleId, ContactID = contactId, ResponsibleID = responsibleContact.ID };
                    if (workflowId != null)
                        responsible.WorkflowID = workflowId;
                    dataManager.Responsible.Add(responsible);
                }
            }
            else
                responsibleContact = dataManager.Contact.SelectById(siteId, responsible.ResponsibleID);

            return responsibleContact;
        }
    }

    public class Recipient
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}