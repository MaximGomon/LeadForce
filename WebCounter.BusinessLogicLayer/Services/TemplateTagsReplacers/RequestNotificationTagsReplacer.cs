using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services.TemplateTagsReplacers
{
    public class RequestNotificationTagsReplacer : TemplateTagsReplacer
    {        
        protected PortalSettingsMap PortalSettings = null;
        protected string PortalLink = string.Empty;

        public RequestNotificationTagsReplacer(tbl_SiteAction siteAction) : base(siteAction) { }

        /// <summary>
        /// Replaces the specified subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public override void Replace(ref string subject, ref string body)
        {            
            if (!SiteAction.ObjectID.HasValue)
                return;

            PortalSettings = DataManager.PortalSettings.SelectMapBySiteId(SiteAction.SiteID, true);

            ReplaceUserInfo(ref body);
            
            if (PortalSettings != null)
            {
                PortalLink = DataManager.PortalSettings.SelectPortalLink(PortalSettings.SiteID);
                RequirementLinkTemplate = RequirementLinkTemplate.Replace("#PortalLink#", PortalLink);
                RequestLinkTemplate = RequestLinkTemplate.Replace("#PortalLink#", PortalLink);
            }            
            
            var request = DataManager.Request.SelectById(SiteAction.SiteID, (Guid) SiteAction.ObjectID);                
            if (request != null)
            {
                var serviceLevel = request.tbl_ServiceLevel;
                tbl_Contact responsible = null;
                tbl_Contact contact = null;
                tbl_Company company = null;

                if (request.ResponsibleID.HasValue)
                    responsible = DataManager.Contact.SelectById(SiteAction.SiteID, (Guid) request.ResponsibleID);
                if (request.ContactID.HasValue)
                    contact = DataManager.Contact.SelectById(SiteAction.SiteID, (Guid) request.ContactID);
                company = request.tbl_Company;

                subject = ReplaceText(subject, request, serviceLevel, responsible, contact, company);
                body = ReplaceText(body, request, serviceLevel, responsible, contact, company);
                if (body.Contains("#Requirement.RegisteredList#"))
                {
                    var requirements = DataManager.Requirement.SelectByRequestId(request.SiteID, request.ID);

                    ProceedRegisteredRequirements(ref body, requirements);
                }

                if (company != null)
                {
                    body = body.Replace("#Request.Notification.Company#", company.Name);
                    subject = subject.Replace("#Request.Notification.Company#", company.Name);
                }
                else
                {
                    body = body.Replace("#Request.Notification.Company#", string.Empty);
                    subject = subject.Replace("#Request.Notification.Company#", string.Empty);
                }
            }

            
            var requirement = DataManager.Requirement.SelectById(SiteAction.SiteID, (Guid) SiteAction.ObjectID);
            if (requirement != null)
            {
                var shortDescription = requirement.ShortDescription;
                var requirementLink = string.Format(RequirementLinkTemplate, requirement.ID);
                shortDescription = string.Format("<a href='{0}'>{1}</a>", requirementLink, shortDescription);

                body = body.Replace("#Requirement.ShortDescription#", shortDescription);

                if (requirement.CompanyID.HasValue)
                {
                    body = body.Replace("#Requirement.Notification.Company#", requirement.tbl_Company.Name);
                    subject = subject.Replace("#Requirement.Notification.Company#", requirement.tbl_Company.Name);
                }
                else
                {
                    body = body.Replace("#Requirement.Notification.Company#", string.Empty);
                    subject = subject.Replace("#Requirement.Notification.Company#", string.Empty);
                }
            }
            

            if (((body.Contains("#Requirement.WorkedList#") || body.Contains("#Requirement.CompanyList#")
                 || body.Contains("#Requirement.CompletedList#")) && SiteAction.ContactID.HasValue))
            {

                var serviceLevelContact = DataManager.ServiceLevelContact.SelectById((Guid)SiteAction.ObjectID);

                if (serviceLevelContact != null)
                {
                    body = body.Replace("#Requirement.Notification.Company#", serviceLevelContact.tbl_ServiceLevelClient.tbl_Company.Name);
                    subject = subject.Replace("#Requirement.Notification.Company#", serviceLevelContact.tbl_ServiceLevelClient.tbl_Company.Name);

                    var startDate = SiteAction.ActionDate.Date;
                    var endDate = SiteAction.ActionDate.Date;
                    RequestNotificationService.GetDatesRangeByServiceLevelContact(serviceLevelContact, SiteAction.ActionDate.Date, ref startDate, ref endDate);

                    var requirements = new List<tbl_Requirement>();

                    switch ((ServiceLevelIncludeToInform)serviceLevelContact.IncludeToInformID)
                    {
                        case ServiceLevelIncludeToInform.All:
                            requirements = DataManager.Requirement.SelectAllByCompanyId(serviceLevelContact.tbl_ServiceLevelClient.ClientID, startDate, endDate).ToList();
                            break;
                        case ServiceLevelIncludeToInform.Personal:
                            requirements = DataManager.Requirement.SelectPersonal(serviceLevelContact.ContactID, startDate, endDate).ToList();
                            break;
                    }

                    body = body.Replace("#Requirement.CompanyList#", string.Empty);

                    ProceedWorkedRequirements(ref body, requirements.Where(o => !o.tbl_RequirementStatus.IsLast).ToList(), startDate, endDate);
                    ProceedCompletedRequirements(ref body, requirements.Where(o => o.tbl_RequirementStatus.IsLast).ToList());
                }
            }
        }        



        /// <summary>
        /// Proceeds the worked requirements.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="requirements">The requirements.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        private void ProceedWorkedRequirements(ref string body, ICollection<tbl_Requirement> requirements, DateTime startDate, DateTime endDate)        
        {
            var tableTemplate = SiteActionTemplates.RequirementTableTemplate;
            var sb = new StringBuilder();

            if (!requirements.Any())
                sb.Append("<tr><td colspan='5'>Пусто</td></tr>");
            
            var requirementsNeedReaction = requirements.Where(o => o.ResponsibleID == SiteAction.ContactID).ToList();            

            foreach (var requirement in requirementsNeedReaction)
            {
                sb.Append(AddRequirementRow(requirement, "background-color: #FFABB0", true));
                requirements.Remove(requirement);
            }

            var requirementsStatusChanged = requirements.Where(o => o.tbl_RequirementHistory.Any(h => h.CreatedAt >= startDate && h.CreatedAt <= endDate)).ToList();

            foreach (var requirement in requirementsStatusChanged)
            {
                sb.Append(AddRequirementRow(requirement, "background-color: #FFE0AB", true));
                requirements.Remove(requirement);
            }

            foreach (var requirement in requirements)            
                sb.Append(AddRequirementRow(requirement, string.Empty, true));

            if (string.IsNullOrEmpty(sb.ToString()))
                body = body.Replace("#Requirement.WorkedList#", string.Empty);
            else
            {
                body = body.Replace("#Requirement.WorkedList#",
                                    string.Concat("<p>Следующие Ваши требования находятся в работе:</p>",
                                        tableTemplate.Replace("#Rows#", sb.ToString())));
            }            
        }



        /// <summary>
        /// Proceeds the completed requirements.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="requirements">The requirements.</param>
        private void ProceedCompletedRequirements(ref string body, IList<tbl_Requirement> requirements)
        {
            var tableTemplate = SiteActionTemplates.RequirementTableTemplate;
            var sb = new StringBuilder();

            foreach (var requirement in requirements)
                sb.Append(AddRequirementRow(requirement, string.Empty));

            if (!requirements.Any())
                body = body.Replace("#Requirement.CompletedList#", string.Empty);
            else
            {
                body = body.Replace("#Requirement.CompletedList#",
                                    string.Concat("<p>Дополнительно за период следующие требования были выполнены:</p>",
                                        tableTemplate.Replace("#Rows#", sb.ToString())));
            }
        }



        /// <summary>
        /// Proceeds the requirements.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="requirements">The requirements.</param>
        private void ProceedRegisteredRequirements(ref string body, IEnumerable<tbl_Requirement> requirements)
        {
            var tableTemplate = SiteActionTemplates.RequirementTableTemplate;
            var sb = new StringBuilder();

            foreach (var requirement in requirements)            
                sb.Append(AddRequirementRow(requirement, string.Empty, true));            

            body = body.Replace("#Requirement.RegisteredList#", tableTemplate.Replace("#Rows#", sb.ToString()));
        }



        /// <summary>
        /// Adds the requirement row.
        /// </summary>
        /// <param name="requirement">The requirement.</param>
        /// <param name="style">The style.</param>
        /// <param name="includeLastComment">if set to <c>true</c> [include last comment].</param>
        /// <returns></returns>
        private string AddRequirementRow(tbl_Requirement requirement, string style, bool includeLastComment = false)
        {                        
            var shortDescription = requirement.ShortDescription;

            if (PortalSettings != null)
            {
                var requirementLink = string.Format(RequirementLinkTemplate, requirement.ID);
                shortDescription = string.Format("<a href='{0}'>{1}</a>", requirementLink, shortDescription);
            }
            
            var row = SiteActionTemplates.RequirementRowTemplate;
            row = row.Replace("#Style#", style)
                .Replace("#ShortDescription#", shortDescription)
                .Replace("#Type#", requirement.tbl_RequirementType.Title)
                .Replace("#Status#", requirement.tbl_RequirementStatus.Title)
                .Replace("#Quantity#", requirement.Quantity > 0 ? String.Format("{0:0}", requirement.Quantity) : string.Empty)
                .Replace("#Unit#",   requirement.Quantity > 0 && requirement.UnitID.HasValue ? requirement.tbl_Unit.Title : string.Empty)
                .Replace("#Amount#", requirement.Quantity > 0 ? requirement.Amount.ToString("F") : string.Empty)
                .Replace("#Currency#", requirement.Quantity > 0 && requirement.CurrencyID.HasValue ? requirement.tbl_Currency.Name : string.Empty);

            var comment = requirement.tbl_RequirementComment.FirstOrDefault(rc => rc.IsOfficialAnswer == true) ?? new tbl_RequirementComment();
            var comments = comment.Comment;

            if (includeLastComment)
            {
                var lastComment = requirement.tbl_RequirementComment.OrderByDescending(rc => rc.CreatedAt).FirstOrDefault();
                if (lastComment != null && comments != lastComment.Comment)
                {
                    if (SiteAction.ContactID.HasValue)
                    {
                        var user = DataManager.User.SelectByContactIdExtended(SiteAction.SiteID, (Guid) SiteAction.ContactID);
                        if (lastComment.DestinationUserID == user.ID)
                        {
                            comments =
                                string.Format("{0}<br/>Требует ответа:<p style='background-color:#FFABB0'>{1}</p>",
                                              comments, lastComment.Comment);
                        }
                        else
                            comments += "<br/>" + lastComment.Comment;
                    }                    
                    else
                        comments += "<br/>" + lastComment.Comment;
                }
            }

            return row.Replace("#OfficialComment#", comments);
        }



        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="request">The request.</param>
        /// <param name="serviceLevel">The service level.</param>
        /// <param name="responsible">The responsible.</param>
        /// <param name="contact">The contact.</param>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        protected string ReplaceText(string text, tbl_Request request, tbl_ServiceLevel serviceLevel, tbl_Contact responsible, tbl_Contact contact, tbl_Company company)
        {
            var shortDescription = request.ShortDescription;

            if (PortalSettings != null)
            {                
                var requestLink = string.Format(RequestLinkTemplate, request.ID);
                shortDescription = string.Format("<a href='{0}'>{1}</a>", requestLink, shortDescription);
            }
            return text.Replace("#Request.ShortDescription#", shortDescription)
                            .Replace("#Request.CreatedAt#", request.CreatedAt.ToString("dd.MM.yyyy"))
                            .Replace("#Request.ReactionTime#", serviceLevel.ReactionTime.ToString())
                            .Replace("#Request.Responsible.UserFullName#", responsible != null ? responsible.UserFullName : string.Empty)
                            .Replace("#Request.ReactionDatePlanned#", request.ReactionDatePlanned.HasValue ? ((DateTime)request.ReactionDatePlanned).ToString("dd.MM.yyyy") : string.Empty)
                            .Replace("#Request.Contact.UserFullName#", contact != null ? contact.UserFullName : string.Empty)
                            .Replace("#Request.Company.Name#", company != null ? company.Name : string.Empty);
        }
    }
}
