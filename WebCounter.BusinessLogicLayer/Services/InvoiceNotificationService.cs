using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services
{
    public class InvoiceNotificationService
    {
        /// <summary>
        /// Pendings the payment.
        /// </summary>
        /// <param name="invocieId">The invocie id.</param>
        public static void PendingPayment(Guid invocieId)
        {
            var dataManager = new DataManager();
            var invoice = dataManager.Invoice.SelectById(invocieId);

            if (!invoice.BuyerCompanyID.HasValue)
                return;

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(invoice.SiteID, SiteActionTemplates.InvoicingNotification);
            var client = dataManager.ServiceLevelClient.SelectByCompanyId(invoice.SiteID, invoice.BuyerCompanyID.Value);

            if (client != null)
            {
                var contacts = client.tbl_ServiceLevelContact.ToList();

                foreach (var serviceLevelContact in contacts)
                {
                    if (!serviceLevelContact.IsInformAboutInvoice) continue;
                    
                    var siteAction = new tbl_SiteAction
                    {
                        SiteID = invoice.SiteID,
                        SiteActionTemplateID = siteActionTemplate.ID,
                        ContactID = serviceLevelContact.ContactID,
                        ActionStatusID = (int)ActionStatus.Scheduled,
                        ActionDate = DateTime.Now,
                        ObjectID = invoice.ID,
                        MessageTypeID = (int)MessageType.InvoiceNotification,
                        DirectionID = (int)Direction.Out,
                        MessageTitle = siteActionTemplate.Title
                    };

                    dataManager.SiteAction.Add(siteAction);

                    if (serviceLevelContact.InvoiceInformFormID != (int)InvoiceInformForm.None)
                    {
                        var siteActionAttachment = new tbl_SiteActionAttachment
                                                       {
                                                           SiteID = siteAction.SiteID,
                                                           SiteActionID = siteAction.ID,
                                                           FileName = string.Empty
                                                       };
                        dataManager.SiteActionAttachment.Add(siteActionAttachment);
                    }
                }
            }
        }



        /// <summary>
        /// Notifies the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="serviceLevelContactId">The service level contact id.</param>
        public static void Inform(Guid siteId, Guid contactId, Guid serviceLevelContactId)
        {
            var dataManager = new DataManager();

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(siteId, SiteActionTemplates.InvoiceInformClientNotification);

            var siteAction = new tbl_SiteAction
            {
                SiteID = siteId,
                SiteActionTemplateID = siteActionTemplate.ID,
                ContactID = contactId,
                ActionStatusID = (int)ActionStatus.Scheduled,
                ActionDate = DateTime.Now,
                ObjectID = serviceLevelContactId,
                MessageTypeID = (int)MessageType.InvoiceNotification,
                DirectionID = (int)Direction.Out,
                MessageTitle = siteActionTemplate.Title
            };

            dataManager.SiteAction.Add(siteAction);
        }



        /// <summary>
        /// Sends to contact.
        /// </summary>
        /// <param name="invoiceId">The invoice id.</param>
        /// <param name="contactId">The contact id.</param>
        public static void SendToContact(Guid invoiceId, Guid contactId)
        {
            var dataManager = new DataManager();
            var invoice = dataManager.Invoice.SelectById(invoiceId);
            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(invoice.SiteID, SiteActionTemplates.InvoicingNotification);

            var siteAction = new tbl_SiteAction
            {
                SiteID = invoice.SiteID,
                SiteActionTemplateID = siteActionTemplate.ID,
                ContactID = contactId,
                ActionStatusID = (int)ActionStatus.Scheduled,
                ActionDate = DateTime.Now,
                ObjectID = invoice.ID,
                MessageTypeID = (int)MessageType.InvoiceNotification,
                DirectionID = (int)Direction.Out,
                MessageTitle = siteActionTemplate.Title
            };

            dataManager.SiteAction.Add(siteAction);

            var siteActionAttachment = new tbl_SiteActionAttachment
            {
                SiteID = siteAction.SiteID,
                SiteActionID = siteAction.ID,
                FileName = string.Empty
            };

            dataManager.SiteActionAttachment.Add(siteActionAttachment);
        }
    }
}
