using System;
using System.Globalization;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Services;

namespace Labitec.LeadForce.CronJobs
{
    public class InvoiceNotificationJob : ICronJob
    {
        public void Run()
        {
            var dataManager = new DataManager();
            
            var serviceLevelContacts = dataManager.ServiceLevelContact.SelectAll();

            var today = DateTime.Now.Date;
            var firstDayOfWeek = today.AddDays(-(today.DayOfWeek - new CultureInfo("ru-RU").DateTimeFormat.FirstDayOfWeek));

            if (firstDayOfWeek != today && today.Day != 1)
                return;

            foreach (var serviceLevelContact in serviceLevelContacts)
            {

                var invoices = dataManager.Invoice.SelectByBuyerCompanyId(serviceLevelContact.tbl_ServiceLevelClient.ClientID).Where(o => o.InvoiceStatusID == (int)InvoiceStatus.PartialPaid || o.InvoiceStatusID == (int)InvoiceStatus.PendingPayment);

                if (!invoices.Any())
                    continue;
                
                var informClient = false;

                switch ((InvoiceInformCatalog)serviceLevelContact.InvoiceInformCatalogID)
                {                    
                    case InvoiceInformCatalog.Weekly:
                        informClient = firstDayOfWeek == today;
                        break;
                    case InvoiceInformCatalog.Biweekly:
                        informClient = WeekNumber(today)%2 == 0;
                        break;
                    case InvoiceInformCatalog.Monthly:
                        informClient = today.Day == 1;
                        break;
                }

                if (informClient)                
                    InvoiceNotificationService.Inform(serviceLevelContact.tbl_Contact.SiteID, serviceLevelContact.ContactID, serviceLevelContact.ID);
            }
        }



        /// <summary>
        /// Weeks the number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public int WeekNumber(DateTime value)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            return cultureInfo.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}