using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.CronJobs
{
    public class SourceMonitoringJob : ICronJob
    {
        private readonly DataManager _dataManager = new DataManager();

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            var sourceMonitorings = _dataManager.SourceMonitoring.SelectAllByStatus(Status.Active).ToList();

            foreach (var sourceMonitoring in sourceMonitorings)
            {
                try
                {
                    switch ((SourceType) sourceMonitoring.SourceTypeID)
                    {
                        case SourceType.POP3Server:
                            ProceedPOP3Source(sourceMonitoring);
                            break;
                    }

                    sourceMonitoring.LastReceivedAt = DateTime.UtcNow;
                    _dataManager.SourceMonitoring.Update(sourceMonitoring);
                }
                catch (Exception ex)
                {
                    Log.Error("Мониторинг ID : " + sourceMonitoring.ID, ex);
                }
            }
        }



        /// <summary>
        /// Proceeds the POP3 source.
        /// </summary>
        /// <param name="sourceMonitoring">The source monitoring.</param>
        private void ProceedPOP3Source(tbl_SourceMonitoring sourceMonitoring)
        {
            using (var client = new Pop3Client())
            {
                client.Connect(sourceMonitoring.PopHost, (int)sourceMonitoring.PopPort, sourceMonitoring.IsSsl);
                client.Authenticate(sourceMonitoring.PopUserName, sourceMonitoring.PopPassword, AuthenticationMethod.UsernameAndPassword);
                var messageCount = client.GetMessageCount();

                //Log.Debug(string.Format("Мониторинг ID: {0} количество сообщений {1}", sourceMonitoring.ID, messageCount));

                for (int messageNumber = 1; messageNumber <= messageCount; messageNumber++)
                {
                    try
                    {
                        MessageHeader headers = client.GetMessageHeaders(messageNumber);

                        //Log.Debug(string.Format("Мониторинг ID: {0} дата сообщения {1}", sourceMonitoring.ID, headers.DateSent));

                        if (!_dataManager.SiteAction.IsExistPOPMessage(headers.MessageId) && !_dataManager.EmailToAnalysis.IsExistPOPMessage(headers.MessageId) && (!sourceMonitoring.StartDate.HasValue || headers.DateSent >= sourceMonitoring.StartDate))                        
                        {

                            //Log.Debug(string.Format("Мониторинг ID: {0} тема {1}", sourceMonitoring.ID, headers.Subject));

                            var isValidMessage = true;

                            var from = headers.From;
                            var subject = headers.Subject ?? string.Empty;

                            //Фильтр сообщений по полям
                            var sourceMonitoringFilters = sourceMonitoring.tbl_SourceMonitoringFilter.ToList();
                            foreach (var sourceMonitoringFilter in sourceMonitoringFilters)
                            {
                                var regex = new Regex(sourceMonitoringFilter.Mask, RegexOptions.IgnoreCase);

                                switch ((SourceEmailProperty)sourceMonitoringFilter.SourcePropertyID)
                                {
                                    case SourceEmailProperty.FromEmail:
                                        isValidMessage = !regex.IsMatch(from.Address);
                                        break;
                                    case SourceEmailProperty.FromName:
                                        isValidMessage = !regex.IsMatch(from.DisplayName);
                                        break;
                                    case SourceEmailProperty.Title:
                                        isValidMessage = !regex.IsMatch(subject);
                                        break;
                                }

                                if (!isValidMessage)
                                    break;
                            }

                            var message = client.GetMessage(messageNumber);
                            var messageText = string.Empty;

                            if (message.FindFirstHtmlVersion() != null)
                                messageText = message.FindFirstHtmlVersion().GetBodyAsText();
                            else if (message.FindFirstPlainTextVersion() != null)
                                messageText = message.FindFirstPlainTextVersion().GetBodyAsText();

                            foreach (var sourceMonitoringFilter in sourceMonitoringFilters.Where(smf => (SourceEmailProperty)smf.SourcePropertyID == SourceEmailProperty.Text))
                            {
                                isValidMessage = !new Regex(sourceMonitoringFilter.Mask, RegexOptions.IgnoreCase).IsMatch(messageText);
                                if (!isValidMessage)
                                    break;
                            }

                            tbl_Contact sender = _dataManager.Contact.SelectByEmail(sourceMonitoring.SiteID, from.Address);
                            //Обработка возвратов
                            var isReturn = false;
                            if (POP3MonitoringProcessingOfReturnsFromFilterCheck(from, messageText))
                            {
                                sender = null;
                                isReturn = true;
                                var regex = new Regex(@"(?<email>(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*))", RegexOptions.IgnoreCase);
                                var match = regex.Match(messageText);
                                var returnEmail = string.Empty;
                                if (match.Success)
                                {
                                    returnEmail = match.Groups["email"].Value;

                                    sender = _dataManager.Contact.SelectByEmail(sourceMonitoring.SiteID, returnEmail);
                                    if (sender == null) isValidMessage = false;

                                    var emailStats = _dataManager.EmailStats.SelectByEmail(returnEmail);
                                    if (emailStats != null)
                                    {
                                        emailStats.ReturnCount++;
                                        _dataManager.EmailStats.Update(emailStats);
                                    }
                                    else                                    
                                        _dataManager.EmailStats.Add(new tbl_EmailStats {ReturnCount = 1, Email = returnEmail});
                                }
                                else
                                {
                                    var emailToAnalysis = new tbl_EmailToAnalysis
                                    {
                                        SourceMonitoringID = sourceMonitoring.ID,
                                        From = headers.From.Address,
                                        Name = headers.From.DisplayName,
                                        MessageText = messageText,
                                        Subject = subject,
                                        POPMessageID = headers.MessageId
                                    };
                                    _dataManager.EmailToAnalysis.Add(emailToAnalysis);
                                    isValidMessage = false;
                                }

                                if ((ProcessingOfReturns)sourceMonitoring.ProcessingOfReturnsID == ProcessingOfReturns.ChangeEmailStatus ||
                                    (ProcessingOfReturns)sourceMonitoring.ProcessingOfReturnsID == ProcessingOfReturns.Download)
                                {

                                    if (!string.IsNullOrEmpty(returnEmail) && sender == null && sourceMonitoring.PopUserName == "postman@leadforce.ru")
                                    {
                                        var siteAction = _dataManager.SiteAction.SelectByEmail(returnEmail).FirstOrDefault();
                                        if (siteAction != null)
                                            sender = _dataManager.Contact.SelectById(siteAction.SiteID, siteAction.ContactID.Value);

                                    }
                                    if (sender != null)
                                    {
                                        if ((ProcessingOfReturns)sourceMonitoring.ProcessingOfReturnsID == ProcessingOfReturns.ChangeEmailStatus)
                                        {
                                            sender.EmailStatusID = (int) EmailStatus.DoesNotWork;
                                            _dataManager.Contact.Update(sender);
                                        }

                                        var siteAction = _dataManager.SiteAction.SelectByContactId(sender.ID).FirstOrDefault();
                                        if (siteAction != null)
                                        {
                                            siteAction.ActionStatusID = (int)ActionStatus.InvalidEmail;
                                            siteAction.Comments += " 550 user not found";
                                            _dataManager.SiteAction.Update(siteAction);
                                        }

                                        _dataManager.SiteAction.Add(new tbl_SiteAction
                                        {
                                            SiteID = sender.SiteID,
                                            ActionStatusID = (int)ActionStatus.Done,
                                            ActionDate = DateTime.Now,
                                            MessageTitle = subject,
                                            SourceMonitoringID = sourceMonitoring.ID,
                                            DirectionID = (int)Direction.In,
                                            SenderID = (sender == null ? null : (Guid?)sender.ID),
                                            MessageText = messageText,
                                            Comments = "Получено от " + sender.Email,
                                            IsHidden = true,
                                            POPMessageID = headers.MessageId
                                        });

                                        //Формируется действие с типом Возврат сообщения
                                        _dataManager.ContactActivity.Add(new tbl_ContactActivity
                                        {
                                            SiteID = sender.SiteID,
                                            ContactID = sender.ID,
                                            ActivityTypeID = (int)ActivityType.ReturnMessage,
                                            ActivityCode = "returnMessage",
                                            ContactSessionID = null,
                                            SourceMonitoringID = sourceMonitoring.ID
                                        });
                                    }
                                }
                            }

                            //Обработка отправителя по правилу: "Загружать по известным контактам"                            
                            if (!isReturn && ((SenderProcessing)sourceMonitoring.SenderProcessingID == SenderProcessing.LoadOfKnownContacts) && sender == null)
                                isValidMessage = false;

                            if (isValidMessage)
                            {
                                //Обработка автоответов
                                var isAutoReply = false;
                                if (POP3MonitoringProcessingOfAutorepliesFilters(subject, messageText))
                                {
                                    isAutoReply = true;
                                    switch ((ProcessingOfAutoReplies)sourceMonitoring.ProcessingOfAutoRepliesID)
                                    {
                                        case ProcessingOfAutoReplies.Skip:
                                            isValidMessage = false;
                                            break;
                                    }
                                }

                                if (isValidMessage && !isReturn)
                                {
                                    //Обработка отправителя по правилу: "Создавать новый контакт"
                                    if ((SenderProcessing)sourceMonitoring.SenderProcessingID == SenderProcessing.CreateNewContact && sender == null)
                                    {
                                        sender = CreateSender(sourceMonitoring.SiteID, headers.From);

                                        if (sender.Email.Split('@').Length == 2)
                                        {
                                            var company = _dataManager.Company.SearchByDomain(sourceMonitoring.SiteID, sender.Email.Split('@')[1]);
                                            if (company != null)
                                                sender.CompanyID = company.ID;
                                        }

                                        sender = _dataManager.Contact.Add(sender);
                                    }

                                    var addSiteAction = true;

                                    if ((SenderProcessing)sourceMonitoring.SenderProcessingID == SenderProcessing.ServiceLevelContacts)
                                    {
                                        if (sender == null)
                                            addSiteAction = false;
                                        else
                                        {
                                            var serviceLevelContact = _dataManager.ServiceLevelContact.SelectByContactId(sender.ID);
                                            if (serviceLevelContact != null)
                                                addSiteAction = serviceLevelContact.IsAutomateDownload;
                                            else
                                                addSiteAction = false;   
                                        }                                        
                                    }

                                    if (addSiteAction)
                                    {
                                        var siteAction = new tbl_SiteAction
                                                             {
                                                                 SiteID = sourceMonitoring.SiteID,
                                                                 ActionStatusID = (int) ActionStatus.Done,
                                                                 ActionDate = DateTime.Now,
                                                                 MessageTitle = subject,
                                                                 SourceMonitoringID = sourceMonitoring.ID,
                                                                 DirectionID = (int) Direction.In,
                                                                 SenderID = (sender == null ? null : (Guid?) sender.ID),
                                                                 MessageText = messageText,
                                                                 Comments = "Получено от " + sender.Email,
                                                                 IsHidden = false,
                                                                 POPMessageID = headers.MessageId
                                                             };

                                        _dataManager.SiteAction.Add(siteAction);

                                        messageText = ProceedAttachments(siteAction, message.FindAllAttachments());

                                        //Формируется действие с типом Входящее сообщение
                                        _dataManager.ContactActivity.Add(new tbl_ContactActivity
                                                                             {
                                                                                 SiteID = sourceMonitoring.SiteID,
                                                                                 ContactID = sender.ID,
                                                                                 ActivityTypeID =
                                                                                     (int) ActivityType.InboxMessage,
                                                                                 ActivityCode = subject,
                                                                                 ContactSessionID = null,
                                                                                 SourceMonitoringID =
                                                                                     sourceMonitoring.ID
                                                                             });

                                        if (sourceMonitoring.RequestSourceTypeID.HasValue)
                                        {
                                            var shortDescription = string.Format("{0} (email от {1})", subject,
                                                                     headers.DateSent.ToString("dd.MM.yyyy hh:mm"));
                                            AddRequest(sourceMonitoring, siteAction.ID, shortDescription, messageText, sender);
                                        }
                                    }
                                }

                                if (!sourceMonitoring.IsLeaveOnServer &&
                                        sourceMonitoring.DaysToDelete != null &&
                                        headers.DateSent < DateTime.Now.AddDays((int)sourceMonitoring.DaysToDelete)
                                        || (isReturn && sourceMonitoring.IsRemoveReturns)
                                        || (isAutoReply && sourceMonitoring.IsRemoveAutoReplies))
                                {
                                    client.DeleteMessage(messageNumber);
                                }
                            }

                            //Отрабатываются возможные события. 
                            /*if (sender != null)
                                CounterServiceHelper.CheckEvent(sourceMonitoring.SiteID, sender.ID);*/
                        }
                        else if (sourceMonitoring.StartDate.HasValue && headers.DateSent < sourceMonitoring.StartDate)
                        {
                            var tmpMessage = client.GetMessage(messageNumber);
                            //Log.Debug(string.Format("Мониторинг ID: {0} загрузка сообщения {1}", sourceMonitoring.ID, headers.Subject));
                        }

                        if (!sourceMonitoring.IsLeaveOnServer && sourceMonitoring.DaysToDelete != null && headers.DateSent < DateTime.Now.AddDays((int)sourceMonitoring.DaysToDelete))
                        {
                            client.DeleteMessage(messageNumber);
                        }
                    }
                    catch (IOException ioex)
                    {
                        Log.Warn(string.Format("Ошибка обработки сообщения {0}, POP3 мониторинг ID : {1}", messageNumber, sourceMonitoring.ID), ioex);
                    }
                    catch (FormatException fex)
                    {
                        Log.Warn(string.Format("Ошибка обработки сообщения {0}, POP3 мониторинг ID : {1}", messageNumber, sourceMonitoring.ID), fex);
                    }
                    catch(OpenPop.Pop3.Exceptions.PopServerException pex)
                    {
                        client.Disconnect();
                        client.Connect(sourceMonitoring.PopHost, (int)sourceMonitoring.PopPort, sourceMonitoring.IsSsl);
                        client.Authenticate(sourceMonitoring.PopUserName, sourceMonitoring.PopPassword, AuthenticationMethod.UsernameAndPassword);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(string.Format("Ошибка обработки сообщения {0}, POP3 мониторинг ID : {1}", messageNumber, sourceMonitoring.ID), ex);
                    }

                    Thread.Sleep(500);
                }

                client.Disconnect();
            }
        }



        /// <summary>
        /// Adds the request.
        /// </summary>
        /// <param name="sourceMonitoring">The source monitoring.</param>
        /// <param name="siteActionId">The site action id.</param>
        /// <param name="shortDescription">The short description.</param>
        /// <param name="longDescription">The long description.</param>
        /// <param name="sender">The sender.</param>
        private void AddRequest(tbl_SourceMonitoring sourceMonitoring, Guid siteActionId, string shortDescription, string longDescription, tbl_Contact sender)
        {
            if (sender == null)
                return;

            _dataManager.Request.Add(sourceMonitoring.SiteID, siteActionId,
                                     sourceMonitoring.RequestSourceTypeID, shortDescription, longDescription, sender.ID, null);
        }



        /// <summary>
        /// POP3 monitoring processing of returns from filter check.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="messageText">The message text.</param>
        /// <returns></returns>
        private bool POP3MonitoringProcessingOfReturnsFromFilterCheck(RfcMailAddress from, string messageText)
        {
            var criteria = Settings.POP3MonitoringProcessingOfReturnsFromFilter.Split(',').Select(p => p.Trim()).ToList();
            var messageTextCriteria = Settings.POP3MonitoringProcessingOfReturnsBodyFilter.Split(',').Select(p => p.Trim()).ToList();

            if (criteria.Where(p => from.Address.ToLower().Contains(p.ToLower())).FirstOrDefault() != null)
                return true;

            if (criteria.Where(p => from.DisplayName.ToLower().Contains(p.ToLower())).FirstOrDefault() != null)
                return true;

            if (messageTextCriteria.Where(p => messageText.ToLower().Contains(p.ToLower())).FirstOrDefault() != null)
                return true;

            return false;
        }



        /// <summary>
        /// POP3 monitoring processing of autoreplies filters.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="messageText">The message text.</param>
        /// <returns></returns>
        private bool POP3MonitoringProcessingOfAutorepliesFilters(string subject, string messageText)
        {
            var subjectCriteria = Settings.POP3MonitoringProcessingOfAutoRepliesSubjectFilter.Split(',').Select(p => p.Trim()).ToList();
            var messageTextCriteria = Settings.POP3MonitoringProcessingOfAutoRepliesBodyFilter.Split(',').Select(p => p.Trim()).ToList();

            if (subjectCriteria.Where(p => subject.ToLower().Contains(p.ToLower())).FirstOrDefault() != null)
                return true;

            if (messageTextCriteria.Where(p => messageText.ToLower().Contains(p.ToLower())).FirstOrDefault() != null)
                return true;

            return false;
        }



        /// <summary>
        /// Proceeds the attachments.
        /// </summary>
        /// <param name="siteAction">The site action.</param>
        /// <param name="attacments">The attacments.</param>
        private string ProceedAttachments(tbl_SiteAction siteAction, IEnumerable<MessagePart> attacments)
        {
            var fsp = new FileSystemProvider();

            foreach (var attachment in attacments)
            {
                if (!attachment.IsAttachment)
                    continue;

                var newFileName = fsp.Upload(siteAction.SiteID, "POP3SourceMonitorings", attachment.FileName,
                                             new MemoryStream(attachment.Body),
                                             FileType.Attachment);

                var siteActionAttachment = new tbl_SiteActionAttachment
                                               {
                                                   SiteID = siteAction.SiteID,
                                                   SiteActionID = siteAction.ID,
                                                   FileName = newFileName
                                               };

                _dataManager.SiteActionAttachment.Add(siteActionAttachment);

                if (siteAction.MessageText.ToLower().Contains("<img"))
                {
                    var regex = new Regex("src=[\"\']\\S*" + attachment.FileName.Replace(Path.GetExtension(attachment.FileName), string.Empty) + "[\\S]+[\"\']");
                    
                    foreach (Match match in regex.Matches(siteAction.MessageText))
                    {
                        if (!match.Value.Contains("http://"))
                        {
                            siteAction.MessageText = siteAction.MessageText.Replace(match.Value,
                                                                                        string.Format(
                                                                                            "src=\"{0}{1}\"",                                                                                              
                                                                                                Settings.
                                                                                                LeadForceSiteUrl,
                                                                                            fsp.GetLink(siteAction.SiteID,
                                                                                                "POP3SourceMonitorings",
                                                                                                newFileName,
                                                                                                FileType.Attachment)));
                        }
                    }
                    
                }
            }

            _dataManager.SiteAction.Update(siteAction);

            return siteAction.MessageText;
        }



        /// <summary>
        /// Creates the sender.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="mailAddress">The mail address.</param>
        /// <returns></returns>
        private tbl_Contact CreateSender(Guid siteId, RfcMailAddress mailAddress)
        {
            var contact = new tbl_Contact
            {
                ID = Guid.NewGuid(),
                SiteID = siteId,
                UserFullName = mailAddress.DisplayName,
                Email = mailAddress.Address,
                IsNameChecked = false,
                UserIP = string.Empty,
                RefferURL = string.Empty,
                StatusID = _dataManager.Status.SelectDefault(siteId).ID
            };


            var nameChecker = new NameChecker(Settings.ADONetConnectionString);
            var nameCheck = nameChecker.CheckName(contact.UserFullName, NameCheckerFormat.FIO, Correction.Correct);
            if (!string.IsNullOrEmpty(nameCheck))
            {
                contact.UserFullName = nameCheck;
                contact.Surname = nameChecker.Surname;
                contact.Name = nameChecker.Name;
                contact.Patronymic = nameChecker.Patronymic;
                contact.IsNameChecked = nameChecker.IsNameCorrect;
            }

            return contact;
        }
    }
}
