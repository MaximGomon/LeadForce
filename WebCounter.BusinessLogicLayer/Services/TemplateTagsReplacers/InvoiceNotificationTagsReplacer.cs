using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services.TemplateTagsReplacers
{
    public class InvoiceNotificationTagsReplacer : TemplateTagsReplacer
    {        
        protected PortalSettingsMap PortalSettings = null;
        protected string PortalLink = string.Empty;

        public InvoiceNotificationTagsReplacer(tbl_SiteAction siteAction, ref MailMessage mailMessage) : base(siteAction, ref mailMessage) { }

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

            var printLink = string.Empty;            
            
            var invoice = DataManager.Invoice.SelectById(SiteAction.SiteID, (Guid) SiteAction.ObjectID);                
            if (invoice != null)
            {                
                if (PortalSettings != null)
                {
                    PortalLink = DataManager.PortalSettings.SelectPortalLink(PortalSettings.SiteID, true);
                    printLink = string.Format(InvoicePrintLinkTemplate.Replace("#PortalLink#", PortalLink), invoice.ID);
                }            

                body = body.Replace("#Invoice.PrintVersion.Link#", printLink);
                if (SiteAction.tbl_SiteActionAttachment.Any())
                {
                    var siteActionAttacment = SiteAction.tbl_SiteActionAttachment.FirstOrDefault();
                    siteActionAttacment.FileName = printLink + "?autoexport=pdf";
                    DataManager.SiteActionAttachment.Update(siteActionAttacment);

                    var webClient = new WebClient();
                    var file = webClient.DownloadData(SiteAction.tbl_SiteActionAttachment.First().FileName);
                    MailMessage.Attachments.Add(new Attachment(new MemoryStream(file), string.Format("Счет #{0}.pdf", invoice.Number), MediaTypeNames.Application.Pdf));
                }


                if (invoice.ExecutorContactID.HasValue)
                {
                    var contact = DataManager.Contact.SelectById(SiteAction.SiteID, invoice.ExecutorContactID.Value);
                    subject = subject.Replace("#Invoice.Executor.UserFullName#", contact.UserFullName).Replace("#Invoice.Executor.Phone#", contact.Phone);
                    body = body.Replace("#Invoice.Executor.UserFullName#", contact.UserFullName).Replace("#Invoice.Executor.Phone#", contact.Phone);
                }
                else
                {
                    subject = subject.Replace("#Invoice.Executor.UserFullName#", string.Empty).Replace("#Invoice.Executor.Phone#", string.Empty);
                    body = body.Replace("#Invoice.Executor.UserFullName#", string.Empty).Replace("#Invoice.Executor.Phone#", string.Empty);
                }

                tbl_Company company = null;

                if (invoice.ExecutorContactID.HasValue)
                {
                    var executorContact = DataManager.Contact.SelectById(SiteAction.SiteID, invoice.ExecutorContactID.Value);
                    MailMessage.Bcc.Add(new MailAddress(executorContact.Email, executorContact.UserFullName));
                }

                if (invoice.BuyerCompanyID.HasValue)
                {
                    company = DataManager.Company.SelectById(SiteAction.SiteID, invoice.BuyerCompanyID.Value);
                }

                if (company != null)
                {
                    body = body.Replace("#Invoice.Notification.Company#", company.Name);
                    subject = subject.Replace("#Invoice.Notification.Company#", company.Name);
                }
                else
                {
                    body = body.Replace("#Invoice.Notification.Company#", string.Empty);
                    subject = subject.Replace("#Invoice.Notification.Company#", string.Empty);
                }                
            }

            if (body.Contains("#Invoice.PayableList#"))
            {
                var serviceLevelContact = DataManager.ServiceLevelContact.SelectById((Guid)SiteAction.ObjectID);
                if (serviceLevelContact != null)
                {
                    body = body.Replace("#Invoice.Notification.Company#", serviceLevelContact.tbl_ServiceLevelClient.tbl_Company.Name);
                    subject = subject.Replace("#Invoice.Notification.Company#", serviceLevelContact.tbl_ServiceLevelClient.tbl_Company.Name);

                    ProceedInvoices(ref body, DataManager.Invoice.SelectByBuyerCompanyId(serviceLevelContact.tbl_ServiceLevelClient.ClientID).Where(o => o.InvoiceStatusID == (int)InvoiceStatus.PartialPaid || o.InvoiceStatusID == (int)InvoiceStatus.PendingPayment), serviceLevelContact);
                }
            }
        }


        private void ProceedInvoices(ref string body, IEnumerable<tbl_Invoice> invoices, tbl_ServiceLevelContact serviceLevelContact)
        {
            var tableTemplate = SiteActionTemplates.InvoiceTableTemplate;
            var sb = new StringBuilder();

            foreach (var invoice in invoices.ToList())
                sb.Append(AddInvoiceRow(invoice, serviceLevelContact));

            body = body.Replace("#Invoice.PayableList#", tableTemplate.Replace("#Rows#", sb.ToString()));            
        }


        private string AddInvoiceRow(tbl_Invoice invoice, tbl_ServiceLevelContact serviceLevelContact)
        {
            var printLink = string.Empty;   

            if (PortalSettings != null)
            {
                PortalLink = DataManager.PortalSettings.SelectPortalLink(PortalSettings.SiteID, true);
                printLink = string.Format(InvoicePrintLinkTemplate.Replace("#PortalLink#", PortalLink), invoice.ID);
            }


            if (serviceLevelContact.InvoiceInformFormID != (int)InvoiceInformForm.None)
            {
                var siteActionAttachment = new tbl_SiteActionAttachment
                {
                    SiteID = SiteAction.SiteID,
                    SiteActionID = SiteAction.ID,
                    FileName = printLink + "?autoexport=pdf"
                };
                DataManager.SiteActionAttachment.Add(siteActionAttachment);

                var webClient = new WebClient();
                var file = webClient.DownloadData(siteActionAttachment.FileName);
                MailMessage.Attachments.Add(new Attachment(new MemoryStream(file), string.Format("Счет #{0}.pdf", invoice.Number), MediaTypeNames.Application.Pdf));
            }

            var row = SiteActionTemplates.InvoiceRowTemplate;        
            row = row.Replace("#Invoice.List.PrintVersion.Link#", printLink).Replace("#Invoice.List.Number#", invoice.Number)
                .Replace("#Invoice.List.CreatedAt#", invoice.CreatedAt.ToString("dd.MM.yyyy"))
                .Replace("#Invoice.List.Amount#", invoice.InvoiceAmount.ToString("F"))
                .Replace("#Invoice.List.PaymentDatePlanned#", invoice.PaymentDatePlanned.HasValue ? invoice.PaymentDatePlanned.Value.ToString("dd.MM.yyyy") : "")
                .Replace("#Invoice.List.Note#", invoice.Note);

            return row;
        }
    }
}
