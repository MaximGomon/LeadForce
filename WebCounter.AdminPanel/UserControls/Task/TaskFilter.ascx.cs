using System;
using System.ComponentModel;
using System.Globalization;
using Telerik.Web.UI.Calendar;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class TaskFilter : System.Web.UI.UserControl
    {
        protected const string TaskWidgetFilterClassName = "WebCounter.AdminPanel.TaskWidgetFilter";
        protected DateTime FirstDayOfWeek = new DateTime();
        protected DateTime LastDayOfWeek = new DateTime();


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? SiteId
        {
            get { return (Guid?) (ViewState["SiteId"]); }
            set { ViewState["SiteId"] = value; }
        }

        public DateTime? StartDate
        {
            get { return rdpStartDate.SelectedDate; }
        }

        public DateTime? EndDate
        {
            get { return rdpEndDate.SelectedDate; }
        }

        public Guid? ResponsibleId
        {
            get { return ucResponsible.SelectedValue; }
        }


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
            ucResponsible.SelectedIndexChanged += ucResponsible_SelectedIndexChanged;        

            if (!rdpStartDate.SelectedDate.HasValue && !rdpEndDate.SelectedDate.HasValue)
            {
                var dataManager = new DataManager();
                var userSettings = dataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID, TaskWidgetFilterClassName);
                if (userSettings != null)
                {
                    var filter = new TaskFilterSettings();                    
                    filter.Deserialize(userSettings.UserSettings);
                    rdpStartDate.SelectedDate = filter.StartDate;
                    rdpEndDate.SelectedDate = filter.EndDate;                    
                    rcRangeSelection.RangeSelectionStartDate = filter.StartDate;
                    rcRangeSelection.RangeSelectionEndDate = filter.EndDate;
                    ucResponsible.SelectedValue = filter.ResponsibleId;
                    FireFilterChangedEvent();
                }
                else
                {
                    rdpStartDate.SelectedDate = DateTime.Now;
                    rdpEndDate.SelectedDate = DateTime.Now;
                    rcRangeSelection.RangeSelectionStartDate = DateTime.Now;
                    rcRangeSelection.RangeSelectionEndDate = DateTime.Now;
                    FireFilterChangedEvent();
                }
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
            SaveSettings();
        }



        /// <summary>
        /// Saves the settings.
        /// </summary>
        protected void SaveSettings()
        {
            var dataManager = new DataManager();
            var userSettings = dataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID, TaskWidgetFilterClassName) ?? new tbl_UserSettings();
            var filter = new TaskFilterSettings
                             {
                                 StartDate = rdpStartDate.SelectedDate ?? DateTime.Now,
                                 EndDate = rdpEndDate.SelectedDate ?? DateTime.Now,
                                 ResponsibleId = ucResponsible.SelectedValue
                             };
            userSettings.UserID = CurrentUser.Instance.ID;
            userSettings.ClassName = TaskWidgetFilterClassName;
            userSettings.UserSettings = filter.Serialize();

            dataManager.UserSettings.Save(userSettings);

            rcRangeSelection.RangeSelectionStartDate = rdpStartDate.SelectedDate ?? DateTime.Now;
            rcRangeSelection.RangeSelectionEndDate = rdpEndDate.SelectedDate ?? DateTime.Now;
            
            Session["sd"] = rdpStartDate.SelectedDate;
            Session["ed"] = rdpEndDate.SelectedDate;
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
            SaveSettings();
            if (FilterChanged != null)
            {
                var eventArgs = new FilterChangedEventArgs { StartDate = rdpStartDate.SelectedDate, EndDate = rdpEndDate.SelectedDate, ResponsibleId = ucResponsible.SelectedValue };
                FilterChanged(this, eventArgs);
            }
        }



        public event FilterChangedEventHandler FilterChanged;
        public delegate void FilterChangedEventHandler(object sender, FilterChangedEventArgs e);
        public class FilterChangedEventArgs : EventArgs
        {
            public Guid? ResponsibleId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }
    }



    public class TaskFilterSettings
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? ResponsibleId { get; set; }


        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return string.Format("{0}${1}${2}", this.StartDate.ToString("yyyy-MM-dd"), this.EndDate.ToString("yyyy-MM-dd"), this.ResponsibleId.ToString());
        }



        /// <summary>
        /// Deserializes the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void Deserialize(string filter)
        {
            var values = filter.Split('$');
            if (values.Length >= 1 && !string.IsNullOrEmpty(values[0]))
                this.StartDate = DateTime.Parse(values[0]);
            if (values.Length >= 2 && !string.IsNullOrEmpty(values[1]))
                this.EndDate = DateTime.Parse(values[1]);
            if (values.Length >= 3 && !string.IsNullOrEmpty(values[2]))
                this.ResponsibleId = Guid.Parse(values[2]);
        }
    }
}