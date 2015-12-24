using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI.Calendar;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class SaveTaskDuration : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? SiteId
        {
            get
            {
                object o = ViewState["SiteId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["SiteId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public TaskMap Task
        {
            get
            {
                object o = ViewState["Task"];
                return (o == null ? null : (TaskMap)o);
            }
            set
            {
                ViewState["Task"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<TaskDurationMap> TaskDurations
        {
            get
            {
                if (ViewState["TaskDurations"] == null)
                    ViewState["TaskDurations"] = new List<TaskDurationMap>();
                return (List<TaskDurationMap>)ViewState["TaskDurations"];
            }
            set
            {
                ViewState["TaskDurations"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsNotFromRadGrid
        {
            get
            {
                object o = ViewState["IsNotFromRadGrid"];
                return (o != null && (bool)o);
            }
            set
            {
                ViewState["IsNotFromRadGrid"] = value;
            }
        }
        
    

        override protected void OnInit(EventArgs e)
        {
            if (IsNotFromRadGrid)
            {
                lbtnCancel.Click += lbtnCancel_Click;
                lbtnSave.Visible = false;                
            }
            else            
                lbtnSaveNonGrid.Visible = false;

            var id = "group" + Guid.NewGuid();

            rfvActualDurationHours.ValidationGroup = id;
            rfvActualDurationMinutes.ValidationGroup = id;
            rfvStartDate.ValidationGroup = id;
            rfvEndDate.ValidationGroup = id;
            ucResponsible.ValidationGroup = id;
            lbtnSave.ValidationGroup = id;
            lbtnSaveNonGrid.ValidationGroup = id;

            base.OnInit(e);
        }



        /// <summary>
        /// Handles the Click event of the lbtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseRadWindow", "CloseRadWindow();", true);
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if (!SiteId.HasValue)
                return;

            rntxtActualDurationHours.Value = null;
            rntxtActualDurationMinutes.Value = null;
            
            ucResponsible.SelectedValue = CurrentUser.Instance.ContactID;

            if (Task == null)
                return;

            var minutes = (Task.PlanDurationHours*60 + Task.PlanDurationMinutes) - (int) TaskDurations.Sum(td => td.ActualDurationHours*60 + td.ActualDurationMinutes);
            var currentDate = DateTime.Now.AddMinutes(-minutes);
            var workDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);

            if (currentDate < workDate)
                currentDate = workDate;

            rdpStartDate.SelectedDate = currentDate;
            rdpEndDate.SelectedDate = DateTime.Now;

            rntxtActualDurationHours.Value = Task.PlanDurationHours - (int) TaskDurations.Sum(td => td.ActualDurationHours);
            rntxtActualDurationMinutes.Value = Task.PlanDurationMinutes - (int)TaskDurations.Sum(td => td.ActualDurationMinutes);
        }




        /// <summary>
        /// Handles the OnClick event of the lbtnSaveNonGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSaveNonGrid_OnClick(object sender, EventArgs e)
        {
            if (SaveClicked != null)
            {
                var taskDuration = new TaskDurationMap
                                       {
                                           ID = Guid.NewGuid(),                                           
                                           SectionDateStart = rdpStartDate.SelectedDate,
                                           SectionDateEnd = rdpEndDate.SelectedDate,
                                           ActualDurationHours = (int?) rntxtActualDurationHours.Value,
                                           ActualDurationMinutes = (int?) rntxtActualDurationMinutes.Value,
                                           ResponsibleID = ucResponsible.SelectedValue
                                       };
                var eventArgs = new SaveClickEventArgs {TaskDuration = taskDuration};
                SaveClicked(this, eventArgs);
            }

            BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseRadWindow", "CloseRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnSelectedDateChanged event of the rdpStartDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpStartDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (e.NewDate > e.OldDate)
            {
               if (rdpEndDate.SelectedDate.Value.Add((TimeSpan)(e.NewDate - e.OldDate)) <= DateTime.Now)
                   rdpEndDate.SelectedDate = rdpEndDate.SelectedDate.Value.Add((TimeSpan) (e.NewDate - e.OldDate));
               
            }
            else
            {
                if (rdpEndDate.SelectedDate.Value.Add((TimeSpan)(-(e.OldDate - e.NewDate))) <= DateTime.Now)
                    rdpEndDate.SelectedDate = rdpEndDate.SelectedDate.Value.Add((TimeSpan)(-(e.OldDate - e.NewDate)));
            }
        }



        /// <summary>
        /// Handles the OnSelectedDateChanged event of the rdpEndDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpEndDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (rdpEndDate.SelectedDate > rdpStartDate.SelectedDate)
            {
                rntxtActualDurationHours.Value = (rdpEndDate.SelectedDate - rdpStartDate.SelectedDate).Value.Hours;
                rntxtActualDurationMinutes.Value = (rdpEndDate.SelectedDate - rdpStartDate.SelectedDate).Value.Minutes;
            }
        }



        /// <summary>
        /// Handles the OnTextChanged event of the rntxtDurationHours control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rntxtDurationHours_OnTextChanged(object sender, EventArgs e)
        {
            if (rdpStartDate.SelectedDate.HasValue && rntxtActualDurationHours.Value.HasValue && rntxtActualDurationMinutes.Value.HasValue)
                rdpEndDate.SelectedDate = rdpStartDate.SelectedDate.Value.AddHours((int) rntxtActualDurationHours.Value).AddMinutes((int) rntxtActualDurationMinutes.Value);
        }


        public event SaveClickEventHandler SaveClicked;
        public delegate void SaveClickEventHandler(object sender, SaveClickEventArgs e);
        public class SaveClickEventArgs : EventArgs
        {
            public TaskDurationMap TaskDuration { get; set; }
        }        
    }
}