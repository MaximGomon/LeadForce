using System;
using System.Globalization;
using System.Linq;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services
{
    public class RequestNotificationService
    {
        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public static void Register(tbl_Request request)
        {            
            if (!request.ContactID.HasValue)
                return;

            AddSiteAction(request, SiteActionTemplates.RequestRegistrationNotification);

            NewRequest(request);
        }



        /// <summary>
        /// News the request.
        /// </summary>
        /// <param name="request">The request.</param>
        protected static void NewRequest(tbl_Request request)
        {
            var dataManager = new DataManager();

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(request.SiteID, SiteActionTemplates.RequestNewNotification);

            var client = request.tbl_ServiceLevel.tbl_ServiceLevelClient.FirstOrDefault(o => o.ClientID == request.CompanyID);

            if (client != null)
            {
                var contacts = client.tbl_ServiceLevelContact.Where(o => o.ContactID != request.ContactID).ToList();

                foreach (var serviceLevelContact in contacts)
                {
                    if (!serviceLevelContact.IsInformByRequest) continue;

                    var siteAction = new tbl_SiteAction
                                         {
                                             SiteID = request.SiteID,
                                             SiteActionTemplateID = siteActionTemplate.ID,
                                             ContactID = serviceLevelContact.ContactID,
                                             ActionStatusID = (int)ActionStatus.Scheduled,
                                             ActionDate = DateTime.Now,
                                             ObjectID = request.ID,
                                             MessageTypeID = (int)MessageType.RequestNotification,
                                             DirectionID = (int)Direction.Out,
                                             MessageTitle = siteActionTemplate.Title
                                         };

                    dataManager.SiteAction.Add(siteAction);
                }                
            }
        }



        /// <summary>
        /// Proceeds the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public static void Process(tbl_Request request)
        {
            if (!request.ContactID.HasValue)
                return;

            AddSiteAction(request, SiteActionTemplates.RequestProcessedNotification);
        }



        /// <summary>
        /// Changes the responsible.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requirementId">The requirement id.</param>
        /// <param name="responsibleId">The responsible id.</param>
        public static void ChangeResponsible(Guid siteId, Guid requirementId, Guid responsibleId)
        {
            var dataManager = new DataManager();

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(siteId,
                                                                                                   SiteActionTemplates.RequirementChangeResponsibleNotification);

            var siteAction = new tbl_SiteAction
                                 {
                                     SiteID = siteId,
                                     SiteActionTemplateID = siteActionTemplate.ID,
                                     ContactID = responsibleId,
                                     ActionStatusID = (int) ActionStatus.Scheduled,
                                     ActionDate = DateTime.Now,
                                     ObjectID = requirementId,
                                     MessageTypeID = (int) MessageType.RequestNotification,
                                     DirectionID = (int) Direction.Out,
                                     MessageTitle = siteActionTemplate.Title
                                 };

            dataManager.SiteAction.Add(siteAction);
        }



        /// <summary>
        /// Notifies the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="serviceLevelContactId">The service level contact id.</param>
        public static void Notify(Guid siteId, Guid contactId, Guid serviceLevelContactId)
        {
            var dataManager = new DataManager();

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(siteId, SiteActionTemplates.RequestClientInformNotification);

            var siteAction = new tbl_SiteAction
            {
                SiteID = siteId,
                SiteActionTemplateID = siteActionTemplate.ID,
                ContactID = contactId,
                ActionStatusID = (int)ActionStatus.Scheduled,
                ActionDate = DateTime.Now,
                ObjectID = serviceLevelContactId,
                MessageTypeID = (int)MessageType.RequestNotification,
                DirectionID = (int)Direction.Out,
                MessageTitle = siteActionTemplate.Title
            };

            dataManager.SiteAction.Add(siteAction);
        }



        /// <summary>
        /// Adds the site action.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="systemSiteActionTemplate">The system site action template.</param>
        protected static void AddSiteAction(tbl_Request request, tbl_SiteActionTemplate systemSiteActionTemplate)
        {
            var dataManager = new DataManager();

            var siteActionTemplate = dataManager.SiteActionTemplate.SelectSystemSiteActionTemplate(request.SiteID, systemSiteActionTemplate);

            var siteAction = new tbl_SiteAction
            {
                SiteID = request.SiteID,
                SiteActionTemplateID = siteActionTemplate.ID,
                ContactID = request.ContactID,
                ActionStatusID = (int)ActionStatus.Scheduled,
                ActionDate = DateTime.Now,
                ObjectID = request.ID,
                MessageTypeID = (int)MessageType.RequestNotification,
                DirectionID = (int)Direction.Out,
                MessageTitle = siteActionTemplate.Title
            };

            dataManager.SiteAction.Add(siteAction);
        }



        /// <summary>
        /// Gets the dates range by service level contact.
        /// </summary>
        /// <param name="serviceLevelContact">The service level contact.</param>
        /// <param name="date">The date.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public static bool GetDatesRangeByServiceLevelContact(tbl_ServiceLevelContact serviceLevelContact, DateTime date, ref DateTime startDate, ref DateTime endDate)
        {
            var firstDayOfWeek = date.AddDays(-(date.DayOfWeek - new CultureInfo("ru-RU").DateTimeFormat.FirstDayOfWeek));

            if (!serviceLevelContact.InformRequestID.HasValue)
                return false;

            switch ((ServiceLevelInform)serviceLevelContact.InformRequestID)
            {
                case ServiceLevelInform.Not:
                    return false;
                case ServiceLevelInform.EveryDay:
                    startDate = date.AddDays(-1);
                    break;
                case ServiceLevelInform.EveryWeek:
                    if (date == firstDayOfWeek)
                        startDate = date.AddDays(-7);
                    else
                        return false;
                    break;
            }

            return true;
        }
    }
}
