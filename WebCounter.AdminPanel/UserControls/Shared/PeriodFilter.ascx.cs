using System;
using System.Globalization;
using Telerik.Web.UI.Calendar;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.UserControls.Shared
{
    public partial class PeriodFilter : System.Web.UI.UserControl
    {        
        protected DateTime FirstDayOfWeek = new DateTime();
        protected DateTime LastDayOfWeek = new DateTime();


        public DateTime? StartDate
        {
            get { return rdpStartDate.SelectedDate; }
        }

        public DateTime? EndDate
        {
            get { return rdpEndDate.SelectedDate; }
        }

        public bool AllPeriod { get; set; }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            FirstDayOfWeek = DateTimeHelper.GetFirstDayOfWeek(DateTime.Now);
            LastDayOfWeek = FirstDayOfWeek.AddDays(6);

            rcRangeSelection.CultureInfo = new CultureInfo("ru-RU");

            /*if (!Page.IsPostBack && AllPeriod)
            {
                rdpStartDate.SelectedDate = DateTime.Parse("2010-01-01");
                rdpEndDate.SelectedDate = DateTime.Now;
                rcRangeSelection.RangeSelectionStartDate = DateTime.Parse("2010-01-01");
                rcRangeSelection.RangeSelectionEndDate = DateTime.Now;
                FireFilterChangedEvent(); 
            }*/

            if (!rdpStartDate.SelectedDate.HasValue && !rdpEndDate.SelectedDate.HasValue)
            {                
                rdpStartDate.SelectedDate = DateTime.Now;
                rdpEndDate.SelectedDate = DateTime.Now;
                rcRangeSelection.RangeSelectionStartDate = DateTime.Now;
                rcRangeSelection.RangeSelectionEndDate = DateTime.Now;
                FireFilterChangedEvent();                
            }
        }




        /// <summary>
        /// Updates the period.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        public void UpdatePeriod(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(-1);
            rdpStartDate.SelectedDate = startDate;
            rdpEndDate.SelectedDate = endDate;
            rcRangeSelection.RangeSelectionStartDate = startDate;
            rcRangeSelection.RangeSelectionEndDate = endDate;            
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ucResponsible control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucResponsible_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            FireFilterChangedEvent();
        }



        /// <summary>
        /// Handles the OnSelectedDateChanged event of the period control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void period_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            FireFilterChangedEvent();
        }



        /// <summary>
        /// Fires the filter changed event.
        /// </summary>
        private void FireFilterChangedEvent()
        {
            rcRangeSelection.RangeSelectionStartDate = rdpStartDate.SelectedDate ?? DateTime.Now;
            rcRangeSelection.RangeSelectionEndDate = rdpEndDate.SelectedDate ?? DateTime.Now;

            if (FilterChanged != null)
            {
                var eventArgs = new FilterChangedEventArgs { StartDate = rdpStartDate.SelectedDate, EndDate = rdpEndDate.SelectedDate };
                FilterChanged(this, eventArgs);
            }
        }


        public event FilterChangedEventHandler FilterChanged;
        public delegate void FilterChangedEventHandler(object sender, FilterChangedEventArgs e);
        public class FilterChangedEventArgs : EventArgs
        {            
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }
    }
}