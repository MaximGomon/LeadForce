using System;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services.TemplateTagsReplacers
{
    public class RequestCommentNotificationTagsReplacer : TemplateTagsReplacer
    {        
        protected PortalSettingsMap PortalSettings = null;
        protected string PortalLink = string.Empty;

        public RequestCommentNotificationTagsReplacer(tbl_SiteAction siteAction) : base(siteAction) { }

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
                RequestLinkTemplate = RequestLinkTemplate.Replace("#PortalLink#", PortalLink);             
            }
            
            var requestComment = ContentCommentRepository.SelectById(SiteAction.SiteID, (Guid)SiteAction.ObjectID, CommentTables.tbl_RequestComment);
            var request = DataManager.Request.SelectById(SiteAction.SiteID, requestComment.ContentID);

            if (request == null)
                return;

            if (request.CompanyID.HasValue)
            {
                subject = subject.Replace("#Request.Notification.Company#", request.tbl_Company.Name);
                body = body.Replace("#Request.Notification.Company#", request.tbl_Company.Name);
            }
            else
            {
                subject = subject.Replace("#Request.Notification.Company#", string.Empty);
                body = body.Replace("#Request.Notification.Company#", string.Empty);
            }

            var replyLink = "Ответить";
            var shortDescription = request.ShortDescription;            
            if (PortalSettings != null)
            {                
                var requestLink = string.Format(RequestLinkTemplate, request.ID);
                replyLink = string.Format("<a href='{0}'>Ответить</a>", requestLink);
                shortDescription = string.Format("<a href='{0}'>{1}</a>", requestLink, shortDescription);
            }

            body = body.Replace("#Request.ShortDescription#", shortDescription)
                       .Replace("#Request.Comment#", requestComment.Comment)
                       .Replace("#Request.Comment.ReplyLink#", replyLink);
        }        

    }
}
