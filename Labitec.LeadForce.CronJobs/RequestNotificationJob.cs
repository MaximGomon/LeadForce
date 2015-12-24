using System;
using System.Globalization;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Services;

namespace Labitec.LeadForce.CronJobs
{
    public class RequestNotificationJob : ICronJob
    {
        public void Run()
        {
            var dataManager = new DataManager();
            
            var serviceLevelContacts = dataManager.ServiceLevelContact.SelectAll();

            var today = DateTime.Now.Date;

            foreach (var serviceLevelContact in serviceLevelContacts)
            {
                var startDate = today;
                var endDate = today;

                if (!RequestNotificationService.GetDatesRangeByServiceLevelContact(serviceLevelContact, today, ref startDate, ref endDate))
                    continue;

                var informClient = false;

                switch((ServiceLevelIncludeToInform)serviceLevelContact.IncludeToInformID)
                {
                    case ServiceLevelIncludeToInform.All:
                        informClient = dataManager.Requirement.SelectAllByCompanyId(serviceLevelContact.tbl_ServiceLevelClient.ClientID, startDate, endDate).Any();
                        break;
                    case ServiceLevelIncludeToInform.Personal:
                        informClient = dataManager.Requirement.SelectPersonal(serviceLevelContact.ContactID, startDate, endDate).Any();
                        break;
                }

                if (informClient)                
                    RequestNotificationService.Notify(serviceLevelContact.tbl_Contact.SiteID, serviceLevelContact.ContactID, serviceLevelContact.ID);
            }
        }
    }
}