using System;
using System.Net.Mail;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Interfaces
{
    public abstract class TemplateTagsReplacer
    {
        protected string RequirementLinkTemplate = "#PortalLink#/Main/Requirements/Edit/{0}";
        protected string RequestLinkTemplate = "#PortalLink#/Main/Requests/Edit/{0}";
        protected string InvoicePrintLinkTemplate = "#PortalLink#/Main/Invoices/Print/{0}";

        protected TemplateTagsReplacer(tbl_SiteAction siteAction)
        {
            SiteAction = siteAction;
        }


        protected TemplateTagsReplacer(tbl_SiteAction siteAction, ref MailMessage mailMessage)
        {
            SiteAction = siteAction;
            MailMessage = mailMessage;
        }
        
        protected tbl_SiteAction SiteAction;
        protected MailMessage MailMessage;
        protected DataManager DataManager = new DataManager();
        public abstract void Replace(ref string subject, ref string body);

        public void ReplaceUserInfo(ref string body)
        {
            var user = DataManager.User.SelectByContactIdExtended(SiteAction.SiteID, (Guid)SiteAction.ContactID);
            
            if (SiteAction.ContactID.HasValue && body.Contains("#User.Password#"))
            {
                if (user == null)
                {
                    user = DataManager.User.AddPortalUser(SiteAction.SiteID, (Guid)SiteAction.ContactID);
                    body = body.Replace("#User.Password#", "оставить пустым")
                        .Replace("#User.Email#", user.Login);
                }
                else
                {
                    body = body.Replace("#User.Password#", string.IsNullOrEmpty(user.Password) ? "оставить пустым" : "*******")
                        .Replace("#User.Email#", user.Login);
                }                
            }

            if (user != null && user.AccessLevelID != (int)AccessLevel.Portal)
            {
                RequestLinkTemplate = RequestLinkTemplate.Replace("#PortalLink#/Main",
                                                                  string.Concat(Settings.LeadForceSiteUrl, "/Support"));
                RequirementLinkTemplate = RequirementLinkTemplate.Replace("#PortalLink#/Main",
                                                                  string.Concat(Settings.LeadForceSiteUrl, "/Support"));
                //InvoicePrintLinkTemplate = InvoicePrintLinkTemplate.Replace("#PortalLink#/Main/Invoices/Print/",
                //                                                  string.Concat(Settings.LeadForceSiteUrl, "/ShowInvoiceReport.aspx?invoiceId="));

            }
        }
    }
}
