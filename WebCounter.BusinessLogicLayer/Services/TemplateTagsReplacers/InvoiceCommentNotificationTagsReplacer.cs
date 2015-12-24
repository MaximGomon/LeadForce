using System;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services.TemplateTagsReplacers
{
    public class InvoiceCommentNotificationTagsReplacer : TemplateTagsReplacer
    {        
        protected PortalSettingsMap PortalSettings = null;
        protected string PortalLink = string.Empty;

        public InvoiceCommentNotificationTagsReplacer(tbl_SiteAction siteAction) : base(siteAction) { }

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

            var invoiceComment = ContentCommentRepository.SelectById(SiteAction.SiteID, (Guid)SiteAction.ObjectID, CommentTables.tbl_InvoiceComment);
            var invoice = DataManager.Invoice.SelectById(SiteAction.SiteID, invoiceComment.ContentID);

            if (invoice == null)
                return;

            subject = subject.Replace("#Invoice.Number#", invoice.Number).Replace("#Invoice.CreatedAt#", invoice.CreatedAt.ToString("dd.MM.yyyy"));
            body = body.Replace("#Invoice.Number#", invoice.Number).Replace("#Invoice.CreatedAt#", invoice.CreatedAt.ToString("dd.MM.yyyy")).Replace("#Invoice.Comment#", invoiceComment.Comment);

            if (invoice.BuyerCompanyID.HasValue)
            {
                var company = DataManager.Company.SelectById(SiteAction.SiteID, invoice.BuyerCompanyID.Value);
                subject = subject.Replace("#Invoice.Notification.Company#", company.Name);
                body = body.Replace("#Invoice.Notification.Company#", company.Name);
            }
            else
            {
                subject = subject.Replace("#Invoice.Notification.Company#", string.Empty);
                body = body.Replace("#Invoice.Notification.Company#", string.Empty);
            }
        }        

    }
}
