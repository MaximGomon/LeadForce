using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using System.Linq;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;


namespace WebCounter.AdminPanel
{
    public partial class Tasks : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Задачи - LeadForce";

            rScheduler.Culture = new CultureInfo("ru-RU");
            rScheduler.FirstDayOfWeek = DayOfWeek.Monday;
            rScheduler.LastDayOfWeek = DayOfWeek.Sunday;
            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucTaskFilter, gridTasks);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucTaskFilter, rScheduler);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rScheduler, ucTaskFilter);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rScheduler, gridTasks);

            ucTaskFilter.SiteId = SiteId;
            ucTaskFilter.FilterChanged += ucTaskFilter_FilterChanged;

            gridTasks.AddNavigateUrl = UrlsData.AP_TaskAdd();
            gridTasks.Actions.Add(new GridAction { Text = "Карточка задачи", NavigateUrl = string.Format("~/{0}/Tasks/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });            
            gridTasks.SiteID = SiteId;            
        }



        /// <summary>
        /// Binds the scheduler.
        /// </summary>
        protected void BindScheduler()
        {
            var tasks = gridTasks.GetDataFromGrid();

            if (ucTaskFilter.StartDate.HasValue && ucTaskFilter.EndDate.HasValue)
            {
                var prevTaskId = Guid.Empty;
                var longTasks = DataManager.TaskDuration.SelectAll(SiteId, (DateTime)ucTaskFilter.StartDate, (DateTime)ucTaskFilter.EndDate, ucTaskFilter.ResponsibleId);
                longTasks = longTasks.Where(td => td.tbl_Task.tbl_TaskType.TaskTypeCategoryID == (int) TaskTypeCategory.LongTermTask);

                foreach (var longTask in longTasks.ToList())
                {                    
                    AddTableRow(longTask, ref tasks, 0);

                    if (!tasks.Rows.Cast<DataRow>().Any(row => (Guid)row["ID"] == longTask.TaskID))
                    {
                        if (longTask.TaskID != prevTaskId)
                        {
                            prevTaskId = longTask.TaskID;
                            AddTableRow(longTask, ref tasks, (int) TaskTypeCategory.LongTermTask);
                        }
                    }
                }
            }

            rScheduler.DataSource = tasks;
            rScheduler.DataKeyField = "ID";
            rScheduler.DataSubjectField = "tbl_Task_Title";
            rScheduler.DataStartField = "tbl_Task_StartDate";
            rScheduler.DataEndField = "tbl_Task_EndDate";
            rScheduler.DataBind();
        }



        /// <summary>
        /// Adds the table row.
        /// </summary>
        /// <param name="longTask">The long task.</param>
        /// <param name="table">The table.</param>
        /// <param name="taskTypeCategory">The task type category.</param>
        private void AddTableRow(tbl_TaskDuration longTask, ref DataTable table, int taskTypeCategory)
        {
            var dataRow = table.NewRow();
            if (taskTypeCategory == 0)
                dataRow["ID"] = longTask.ID;
            else
                dataRow["ID"] = longTask.TaskID;
            dataRow["tbl_Task_Title"] = longTask.tbl_Task.Title;
            dataRow["tbl_Task_StartDate"] = longTask.SectionDateStart;
            dataRow["tbl_Task_EndDate"] = longTask.SectionDateEnd;
            dataRow["tbl_TaskType_TaskTypeCategoryID"] = taskTypeCategory;
            dataRow["tbl_Task_TaskStatusID"] = longTask.tbl_Task.TaskStatusID;
            table.Rows.Add(dataRow);
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //if (!Page.IsPostBack)
            BindScheduler();            
        }


        
        /// <summary>
        /// Handles the FilterChanged event of the ucTaskFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.Task.TaskFilter.FilterChangedEventArgs"/> instance containing the event data.</param>
        protected void ucTaskFilter_FilterChanged(object sender, UserControls.Task.TaskFilter.FilterChangedEventArgs e)
        {            
            RebindTasks(e.StartDate, e.EndDate, e.ResponsibleId);            
        }



        /// <summary>
        /// Rebinds the tasks.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="responsibleId">The responsible id.</param>
        /// <param name="updateScheduler">if set to <c>true</c> [update scheduler].</param>
        protected void RebindTasks(DateTime? startDate, DateTime? endDate, Guid? responsibleId, bool updateScheduler = true)
        {
            gridTasks.Where = new List<GridWhere>();
            if (startDate.HasValue && endDate.HasValue)
            {
                if (startDate.Value == endDate.Value)
                    endDate = endDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);

                gridTasks.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_Task.StartDate >= '{0}' AND tbl_Task.EndDate <= '{1}'", startDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), endDate.Value.ToString("yyyy-MM-dd HH:mm:ss")) });

                //rScheduler.SelectedDate = startDate.Value;

                if (updateScheduler)
                {
                    if ((endDate.Value - startDate.Value).Days == 0)
                        rScheduler.SelectedView = SchedulerViewType.DayView;
                    else if ((endDate.Value - startDate.Value).Days <= 7)
                        rScheduler.SelectedView = SchedulerViewType.WeekView;
                    else if ((endDate.Value - startDate.Value).Days > 7)
                        rScheduler.SelectedView = SchedulerViewType.MonthView;
                }
            }

            if (responsibleId.HasValue && responsibleId != Guid.Empty)
                gridTasks.Where.Add(new GridWhere() { Column = "ResponsibleID", Value = responsibleId.ToString() });

            gridTasks.Rebind();

            BindScheduler();            
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridTasks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridTasks_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;
                
                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");

                ((Literal)item.FindControl("lrlTaskStatus")).Text = EnumHelper.GetEnumDescription((TaskStatus)int.Parse(data["tbl_Task_TaskStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
            }
        }



        /// <summary>
        /// Handles the OnAppointmentDataBound event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.SchedulerEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnAppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            var taskTypeCategoryType = int.Parse((((DataRowView)e.Appointment.DataItem)).Row["tbl_TaskType_TaskTypeCategoryID"].ToString());

            if (taskTypeCategoryType == (int)TaskTypeCategory.LongTermTask || taskTypeCategoryType == (int) TaskTypeCategory.TODO)
            {
                if (ucTaskFilter.StartDate.HasValue && ucTaskFilter.EndDate.HasValue && rScheduler.SelectedView == SchedulerViewType.DayView)
                {
                    e.Appointment.Start = new DateTime(ucTaskFilter.StartDate.Value.Year, ucTaskFilter.StartDate.Value.Month, ucTaskFilter.StartDate.Value.Day);
                    e.Appointment.End = new DateTime(ucTaskFilter.EndDate.Value.Year, ucTaskFilter.EndDate.Value.Month, ucTaskFilter.EndDate.Value.Day).AddHours(24);
                    e.Appointment.AllowEdit = false;
                }

                if ((taskTypeCategoryType == (int)TaskTypeCategory.TODO || taskTypeCategoryType == (int)TaskTypeCategory.LongTermTask) && rScheduler.SelectedView != SchedulerViewType.DayView)                
                    e.Appointment.Visible = false;

                if (taskTypeCategoryType == (int)TaskTypeCategory.LongTermTask && ucTaskFilter.StartDate.HasValue && ucTaskFilter.EndDate.HasValue && rScheduler.SelectedView == SchedulerViewType.WeekView)
                {
                    e.Appointment.Start = new DateTime(e.Appointment.Start.Year, e.Appointment.Start.Month, e.Appointment.Start.Day, 0, 0,0);
                    e.Appointment.End = new DateTime(e.Appointment.End.Year, e.Appointment.End.Month, e.Appointment.End.Day, 0, 0, 0).AddHours(24);
                    e.Appointment.AllowEdit = false;
                    e.Appointment.Visible = true;
                }
            }

            if (int.Parse((((DataRowView)e.Appointment.DataItem)).Row["tbl_TaskType_TaskTypeCategoryID"].ToString()) == 0)
                e.Appointment.Attributes.Add("TaskID", DataManager.TaskDuration.SelectById((Guid)e.Appointment.ID).TaskID.ToString());            
            else
                e.Appointment.Attributes.Add("TaskID", e.Appointment.ID.ToString());            
        }



        /// <summary>
        /// Handles the OnNavigationComplete event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.SchedulerNavigationCompleteEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnNavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            if (e.Command == SchedulerNavigationCommand.SwitchToDayView || e.Command == SchedulerNavigationCommand.NavigateToSelectedDate || e.Command == SchedulerNavigationCommand.SwitchToSelectedDay)
            {
                //ucTaskFilter.UpdatePeriod(rScheduler.VisibleRangeStart, rScheduler.VisibleRangeEnd);
                RebindTasks(rScheduler.VisibleRangeStart, rScheduler.VisibleRangeEnd, null, false);                
            }
            else if (e.Command == SchedulerNavigationCommand.SwitchToWeekView ||
                e.Command == SchedulerNavigationCommand.SwitchToMonthView || e.Command == SchedulerNavigationCommand.SwitchToTimelineView ||
                e.Command == SchedulerNavigationCommand.NavigateToPreviousPeriod || e.Command == SchedulerNavigationCommand.NavigateToNextPeriod)
            {
                //ucTaskFilter.UpdatePeriod(rScheduler.VisibleRangeStart, rScheduler.VisibleRangeEnd);
                RebindTasks(rScheduler.VisibleRangeStart, rScheduler.VisibleRangeEnd, null, false);
            }            
        }



        /// <summary>
        /// Handles the OnAppointmentUpdate event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentUpdateEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnAppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {             
            var task = DataManager.Task.SelectById(SiteId, (Guid) e.ModifiedAppointment.ID);
            if (task != null)
            {
                task.StartDate = e.ModifiedAppointment.Start;
                task.EndDate = e.ModifiedAppointment.End;
                task.PlanDurationHours = (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Hours;
                task.PlanDurationMinutes = (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Minutes;

                DataManager.Task.Update(task);

                gridTasks.Rebind();
            }
            else
            {
                var taskDuration = DataManager.TaskDuration.SelectById((Guid) e.ModifiedAppointment.ID);
                if (taskDuration != null)
                {
                    taskDuration.SectionDateStart = e.ModifiedAppointment.Start;
                    taskDuration.SectionDateEnd = e.ModifiedAppointment.End;
                    taskDuration.ActualDurationHours = (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Hours;
                    taskDuration.ActualDurationMinutes = (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Minutes;

                    DataManager.TaskDuration.Update(taskDuration);

                    var taskDurations = DataManager.TaskDuration.SelectAll(taskDuration.TaskID);
                    task = DataManager.Task.SelectById(SiteId, taskDuration.TaskID);
                    if (task != null)
                    {
                        task.ActualDurationHours = taskDurations.Sum(td => td.ActualDurationHours);
                        task.ActualDurationMinutes = taskDurations.Sum(td => td.ActualDurationMinutes);
                        var completePecent = 0;
                        var taskDurationActual = taskDurations.Sum(td => td.ActualDurationHours * 60 + td.ActualDurationMinutes);
                        var taskDurationPlanned = task.PlanDurationHours * 60 + task.PlanDurationMinutes;

                        if (taskDurationActual >= taskDurationPlanned)
                            completePecent = 100;
                        else
                            completePecent = (int)(((taskDurationActual * 100) / taskDurationPlanned));

                        task.CompletePercentage = completePecent;

                        DataManager.Task.Update(task);
                    }
                }
            }
        }
    }
}