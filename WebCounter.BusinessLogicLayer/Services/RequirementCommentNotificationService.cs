using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services
{
    public class RequirementCommentNotificationService
    {
        /// <summary>
        /// Notifies the specified requirement comment.
        /// </summary>
        /// <param name="requirementComment">The requirement comment.</param>
        public static void Notify(tbl_RequirementComment requirementComment)
        {
            if (!requirementComment.tbl_Requirement.CompanyID.HasValue)
                return;

            AddSiteAction(requirementComment, SiteActionTemplates.RequirementCommentNotification);
        }



        /// <summary>
        /// Adds the site action.
        /// </summary>
        /// <param name="requirementComment">The requirement comment.</param>
        /// <param name="systemSiteActionTemplate">The system site action template.</param>
        protected static void AddSiteAction(tbl_RequirementComment requirementComment, tbl_SiteActionTemplate systemSiteActionTemplate)
        {
            var dataManager = new DataManager();

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(requirementComment.SiteID, systemSiteActionTemplate);

            var serviceLevelClient = dataManager.ServiceLevelClient.SelectByCompanyId(requirementComment.SiteID, (Guid)requirementComment.tbl_Requirement.CompanyID);

            if (serviceLevelClient == null)
                return;

            var serviceLevelContacts = dataManager.ServiceLevelContact.SelectByClientId(serviceLevelClient.ID).ToList();            

            var commentator = dataManager.User.SelectById(requirementComment.UserID);

            tbl_User destination = null;

            if (requirementComment.DestinationUserID.HasValue)
                destination = dataManager.User.SelectById((Guid)requirementComment.DestinationUserID);

            foreach (var serviceLevelContact in serviceLevelContacts)
            {                
                //Не высылать уведомление самому себе
                if (commentator != null && serviceLevelContact.ContactID == commentator.ContactID)
                    continue;

                if (destination != null && serviceLevelContact.ContactID != destination.ContactID)
                    continue;

                if (requirementComment.IsInternal)
                {                    
                    var user = dataManager.User.SelectByContactId(requirementComment.SiteID, serviceLevelContact.ContactID);
                    if (user == null || user.AccessLevelID == (int)AccessLevel.Portal)
                        continue;                    
                }

                switch ((ServiceLevelInformComment)serviceLevelContact.InformCommentID)
                {
                    case ServiceLevelInformComment.Not:
                        continue;
                    case ServiceLevelInformComment.Personal:
                        if (requirementComment.DestinationUserID.HasValue)
                        {
                            var user = dataManager.User.SelectById((Guid)requirementComment.DestinationUserID);
                            if (user.ContactID != serviceLevelContact.ContactID)                            
                                continue;
                        }
                        else
                            continue;                                                    
                        break;
                    case ServiceLevelInformComment.PersonalRequirement:
                        if (requirementComment.tbl_Requirement.ContactID != serviceLevelContact.ContactID && 
                            requirementComment.tbl_Requirement.ResponsibleID != serviceLevelContact.ContactID)                        
                            continue;                        
                        break;
                }

                var siteAction = new tbl_SiteAction
                {
                    SiteID = requirementComment.SiteID,
                    SiteActionTemplateID = siteActionTemplate.ID,
                    ContactID = serviceLevelContact.ContactID,
                    ActionStatusID = (int)ActionStatus.Scheduled,
                    ActionDate = DateTime.Now,
                    ObjectID = requirementComment.ID,
                    MessageTypeID = (int)MessageType.RequirementCommentNotification,
                    DirectionID = (int)Direction.Out,
                    MessageTitle = siteActionTemplate.Title
                };

                dataManager.SiteAction.Add(siteAction);
            }                        
        }
    }
}
