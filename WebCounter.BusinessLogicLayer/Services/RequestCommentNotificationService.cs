using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services
{
    public class RequestCommentNotificationService
    {
        /// <summary>
        /// Notifies the specified request comment.
        /// </summary>
        /// <param name="requestComment">The request comment.</param>
        public static void Notify(tbl_RequestComment requestComment)
        {
            if (!requestComment.tbl_Request.CompanyID.HasValue)
                return;

            AddSiteAction(requestComment, SiteActionTemplates.RequestCommentNotification);
        }



        /// <summary>
        /// Adds the site action.
        /// </summary>
        /// <param name="requestComment">The request comment.</param>
        /// <param name="systemSiteActionTemplate">The system site action template.</param>
        protected static void AddSiteAction(tbl_RequestComment requestComment, tbl_SiteActionTemplate systemSiteActionTemplate)
        {
            var dataManager = new DataManager();

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(requestComment.SiteID, systemSiteActionTemplate);

            var serviceLevelClient = dataManager.ServiceLevelClient.SelectByCompanyId(requestComment.SiteID, (Guid)requestComment.tbl_Request.CompanyID);

            if (serviceLevelClient == null)
                return;

            var serviceLevelContacts = dataManager.ServiceLevelContact.SelectByClientId(serviceLevelClient.ID).ToList();

            var commentator = dataManager.User.SelectById(requestComment.UserID);
            tbl_User destination = null;

            if (requestComment.DestinationUserID.HasValue)
                destination = dataManager.User.SelectById((Guid)requestComment.DestinationUserID);

            foreach (var serviceLevelContact in serviceLevelContacts)
            {                
                //Не высылать уведомление самому себе
                if (commentator != null && serviceLevelContact.ContactID == commentator.ContactID)
                    continue;

                if (destination != null && serviceLevelContact.ContactID != destination.ContactID)
                    continue;

                if (requestComment.IsInternal)
                {
                    var user = dataManager.User.SelectByContactId(requestComment.SiteID, serviceLevelContact.ContactID);
                    if (user == null || user.AccessLevelID == (int)AccessLevel.Portal)
                        continue;
                }

                switch ((ServiceLevelInformComment)serviceLevelContact.InformCommentID)
                {
                    case ServiceLevelInformComment.Not:
                        continue;
                    case ServiceLevelInformComment.Personal:
                        if (requestComment.DestinationUserID.HasValue)
                        {
                            var user = dataManager.User.SelectById((Guid)requestComment.DestinationUserID);
                            if (user.ContactID != serviceLevelContact.ContactID)                            
                                continue;
                        }
                        else
                            continue;                                                    
                        break;
                    case ServiceLevelInformComment.PersonalRequirement:
                        if (requestComment.tbl_Request.ContactID != serviceLevelContact.ContactID &&
                            requestComment.tbl_Request.ResponsibleID != serviceLevelContact.ContactID)                        
                            continue;                        
                        break;
                }

                var siteAction = new tbl_SiteAction
                {
                    SiteID = requestComment.SiteID,
                    SiteActionTemplateID = siteActionTemplate.ID,
                    ContactID = serviceLevelContact.ContactID,
                    ActionStatusID = (int)ActionStatus.Scheduled,
                    ActionDate = DateTime.Now,
                    ObjectID = requestComment.ID,
                    MessageTypeID = (int)MessageType.RequestCommentNotification,
                    DirectionID = (int)Direction.Out,
                    MessageTitle = siteActionTemplate.Title
                };

                dataManager.SiteAction.Add(siteAction);
            }                        
        }
    }
}
