using System;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services.TemplateTagsReplacers
{
    public class RequirementCommentNotificationTagsReplacer : TemplateTagsReplacer
    {        
        protected PortalSettingsMap PortalSettings = null;
        protected string PortalLink = string.Empty;

        public RequirementCommentNotificationTagsReplacer(tbl_SiteAction siteAction) : base(siteAction) { }

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
            }

            var requirementComment = ContentCommentRepository.SelectById(SiteAction.SiteID, (Guid)SiteAction.ObjectID, CommentTables.tbl_RequirementComment);
            var requirement = DataManager.Requirement.SelectById(SiteAction.SiteID, requirementComment.ContentID);

            if (requirement == null)
                return;

            if (requirement.CompanyID.HasValue)
            {
                subject = subject.Replace("#Requirement.Notification.Company#", requirement.tbl_Company.Name);
                body = body.Replace("#Requirement.Notification.Company#", requirement.tbl_Company.Name);                
            }
            else
            {
                subject = subject.Replace("#Requirement.Notification.Company#", string.Empty);
                body = body.Replace("#Requirement.Notification.Company#", string.Empty);
            }

            var replyLink = "Ответить";
            var shortDescription = requirement.ShortDescription;            
            if (PortalSettings != null)
            {                
                var requirementLink = string.Format(RequirementLinkTemplate, requirement.ID);
                replyLink = string.Format("<a href='{0}'>Ответить</a>", requirementLink);
                shortDescription = string.Format("<a href='{0}'>{1}</a>", requirementLink, shortDescription);
            }

            body = body.Replace("#Requirement.ShortDescription#", shortDescription)
                       .Replace("#Requirement.Comment#", requirementComment.Comment)
                       .Replace("#Requirement.Comment.ReplyLink#", replyLink);
        }        

    }
}
