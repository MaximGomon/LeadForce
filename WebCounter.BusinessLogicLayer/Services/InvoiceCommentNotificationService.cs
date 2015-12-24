using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services
{
    public class InvoiceCommentNotificationService
    {
        /// <summary>
        /// Notifies the specified invoice comment.
        /// </summary>
        /// <param name="invoiceComment">The invoice comment.</param>
        public static void Notify(tbl_InvoiceComment invoiceComment)
        {
            if (!invoiceComment.tbl_Invoice.BuyerCompanyID.HasValue)
                return;

            AddSiteAction(invoiceComment, SiteActionTemplates.InvoiceCommentNotification);
        }



        protected static void AddSiteAction(tbl_InvoiceComment invoiceComment, tbl_SiteActionTemplate systemSiteActionTemplate)
        {
            var dataManager = new DataManager();

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(invoiceComment.SiteID, systemSiteActionTemplate);

            var serviceLevelClient = dataManager.ServiceLevelClient.SelectByCompanyId(invoiceComment.SiteID, invoiceComment.tbl_Invoice.BuyerCompanyID.Value);

            if (serviceLevelClient == null)
                return;

            var serviceLevelContacts = dataManager.ServiceLevelContact.SelectByClientId(serviceLevelClient.ID).ToList();

            var commentator = dataManager.User.SelectById(invoiceComment.UserID);
            tbl_User destination = null;

            if (invoiceComment.DestinationUserID.HasValue)
                destination = dataManager.User.SelectById((Guid)invoiceComment.DestinationUserID);

            foreach (var serviceLevelContact in serviceLevelContacts)
            {                
                //Не высылать уведомление самому себе
                if (commentator != null && serviceLevelContact.ContactID == commentator.ContactID)
                    continue;

                if (destination != null && serviceLevelContact.ContactID != destination.ContactID)
                    continue;

                if (invoiceComment.IsInternal)
                {
                    var user = dataManager.User.SelectByContactId(invoiceComment.SiteID, serviceLevelContact.ContactID);
                    if (user == null || user.AccessLevelID == (int)AccessLevel.Portal)
                        continue;
                }

                if (serviceLevelContact.IsInformInvoiceComments)
                {
                    var siteAction = new tbl_SiteAction
                                         {
                                             SiteID = invoiceComment.SiteID,
                                             SiteActionTemplateID = siteActionTemplate.ID,
                                             ContactID = serviceLevelContact.ContactID,
                                             ActionStatusID = (int) ActionStatus.Scheduled,
                                             ActionDate = DateTime.Now,
                                             ObjectID = invoiceComment.ID,
                                             MessageTypeID = (int) MessageType.InvoiceCommentNotification,
                                             DirectionID = (int) Direction.Out,
                                             MessageTitle = siteActionTemplate.Title
                                         };

                    dataManager.SiteAction.Add(siteAction);
                }
            }                        
        }
    }
}
