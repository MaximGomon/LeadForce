using System;
using System.Linq;
using System.Collections.Generic;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class MonthlyStats
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageViewCount { get; set; }
        public int EmailSendCount { get; set; }
        public int SmsSendCount { get; set; }



        /// <summary>
        /// Selects the stats.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public static List<MonthlyStats> Select(Guid siteId)
        {
            var dataManager = new DataManager();
            var result = new List<MonthlyStats>();
            var currentDate = DateTime.Now;

            for (int i = 0; i < 6; i++)
            {
                result.Add(new MonthlyStats() { StartDate = FirstDayOfMonth(currentDate), EndDate = LastDayOfMonth(currentDate)});
                currentDate = currentDate.AddMonths(-1);
            }

            foreach (var statsItem in result)
            {
                statsItem.PageViewCount = dataManager.ContactActivity.Select(siteId, null, ActivityType.ViewPage, statsItem.StartDate, statsItem.EndDate).Count;
                statsItem.EmailSendCount = dataManager.SiteAction.Select(siteId, null, ActionStatus.Done, statsItem.StartDate, statsItem.EndDate).Count;
            }

            return result.OrderBy(s => s.StartDate).ToList();
        }



        /// <summary>
        /// Firsts the day of month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        protected static DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }



        /// <summary>
        /// Lasts the day of month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        protected static DateTime LastDayOfMonth(DateTime dateTime)
        {
            var firstDayOfTheMonth = FirstDayOfMonth(dateTime);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }
    }
}
