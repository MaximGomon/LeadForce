using System;
using System.Globalization;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Gets the first day of week.
        /// </summary>
        /// <param name="dayInWeek">The day in week.</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            var defaultCultureInfo = new CultureInfo("ru-RU");
            return GetFirstDateOfWeek(dayInWeek, defaultCultureInfo);
        }



        /// <summary>
        /// Gets the first date of week.
        /// </summary>
        /// <param name="dayInWeek">The day in week.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }


        
        /// <summary>
        /// Gets the period.
        /// </summary>
        /// <param name="period">The period.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        public static void GetPeriod(DateTimePeriod period, ref DateTime startDate, ref DateTime endDate)
        {
            var date = DateTime.Now.Date;
            var firstDayOfWeek = DateTimeHelper.GetFirstDayOfWeek(date);
            startDate = date;
            endDate = date;

            switch (period)
            {
                case DateTimePeriod.Today:
                    startDate = date.Date;
                    endDate = date.AddDays(1);
                    break;
                case DateTimePeriod.Yesterday:
                    startDate = date.Date.AddDays(-1);
                    endDate = date;
                    break;
                case DateTimePeriod.Week:
                    startDate = firstDayOfWeek;
                    endDate = date.AddDays(1);
                    break;
                case DateTimePeriod.Month:
                    startDate = new DateTime(date.Year, date.Month, 1);
                    endDate = date.AddDays(1);
                    break;
                case DateTimePeriod.Quarter:
                    startDate = new DateTime(date.Year, (3 * GetQuarterName(date)) - 2, 1);
                    endDate = date.AddDays(1);
                    break;
                case DateTimePeriod.Year:
                    startDate = new DateTime(date.Year, 1, 1);
                    endDate = date.AddDays(1);
                    break;
            }            
        }



        /// <summary>
        /// Gets the period.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public static DateTimePeriod GetPeriod(DateTime startDate, DateTime endDate)
        {
            var result = DateTimePeriod.Week;

            var date = DateTime.Now.Date;
            var firstDayOfWeek = DateTimeHelper.GetFirstDayOfWeek(date);
            
            if (startDate == date.Date && endDate == date.AddDays(1))
                result = DateTimePeriod.Today;
            if (startDate == date.Date.AddDays(-1) && endDate == date)
                result = DateTimePeriod.Yesterday;
            if (startDate == firstDayOfWeek && endDate == date.AddDays(1))
                result = DateTimePeriod.Week;
            if (startDate == new DateTime(date.Year, date.Month, 1) && endDate == date.AddDays(1))
                result = DateTimePeriod.Month;
            if (startDate == new DateTime(date.Year, (3 * GetQuarterName(date)) - 2, 1) && endDate == date.AddDays(1))
                result = DateTimePeriod.Quarter;
            if (startDate == new DateTime(date.Year, 1, 1) && endDate == date.AddDays(1))
                result = DateTimePeriod.Year;

            return result;
        }



        /// <summary>
        /// Gets the name of the quarter.
        /// </summary>
        /// <param name="myDate">My date.</param>
        /// <returns></returns>
        protected static int GetQuarterName(DateTime myDate)
        {
            return (int)Math.Ceiling(myDate.Month / 3.0);
        }
    }
}
