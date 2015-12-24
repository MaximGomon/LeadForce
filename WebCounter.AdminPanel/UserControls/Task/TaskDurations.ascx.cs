using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class TaskDurations : System.Web.UI.UserControl
    {
        private DataManager _dataManager;        
        public event EventHandler TaskDurationsChanged;

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
        public Guid SiteId
        {
            get
            {
                object o = ViewState["SiteId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["SiteId"] = value;
            }
        }


        public List<TaskDurationMap> TaskDurationsList
        {
            get
            {
                if (ViewState["TaskDurations"] == null)
                    ViewState["TaskDurations"] = new List<TaskDurationMap>();
                return (List<TaskDurationMap>)ViewState["TaskDurations"];
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;

            rgTaskDurations.Culture = new CultureInfo("ru-RU");            
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;

            if (Task != null)
            {
                ViewState["TaskDurations"] =
                    _dataManager.TaskDuration.SelectAll(Task.ID).Select(
                        td =>
                        new TaskDurationMap()
                            {
                                ID = td.ID,
                                ActualDurationHours = td.ActualDurationHours,
                                ActualDurationMinutes = td.ActualDurationMinutes,
                                SectionDateStart = td.SectionDateStart,
                                SectionDateEnd = td.SectionDateEnd,
                                ResponsibleID = td.ResponsibleID,
                                Comment = td.Comment
                            }).ToList();
            }
            else
                ViewState["TaskDurations"] = new List<TaskDurationMap>();                
            
            
            rgTaskDurations.Rebind();
        }


        


        /// <summary>
        /// Handles the NeedDataSource event of the rgTaskDurations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgTaskDurations_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgTaskDurations.DataSource = ViewState["TaskDurations"];
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgTaskDurations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgTaskDurations_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {                
                var userControl = (SaveTaskDuration)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                userControl.SiteId = SiteId;
                if (Task != null)
                    userControl.Task = Task;
                userControl.TaskDurations = TaskDurationsList;
                userControl.BindData();
                
                var item = e.Item as GridEditableItem;

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var taskDuration = (TaskDurationMap)item.DataItem;
                    ((ContactComboBox)userControl.FindControl("ucResponsible")).SelectedValue = taskDuration.ResponsibleID;
                    ((RadDateTimePicker)userControl.FindControl("rdpStartDate")).SelectedDate = taskDuration.SectionDateStart;
                    ((RadDateTimePicker)userControl.FindControl("rdpEndDate")).SelectedDate = taskDuration.SectionDateEnd;
                    ((RadNumericTextBox)userControl.FindControl("rntxtActualDurationHours")).Value = taskDuration.ActualDurationHours;
                    ((RadNumericTextBox)userControl.FindControl("rntxtActualDurationMinutes")).Value = taskDuration.ActualDurationMinutes;
                    ((TextBox)userControl.FindControl("txtComment")).Text = taskDuration.Comment;
                }
            }
            else if (e.Item is GridDataItem)
            {
                var taskDuration = e.Item.DataItem as TaskDurationMap;
                if (taskDuration != null && taskDuration.ResponsibleID.HasValue)
                {
                    ((Literal)e.Item.FindControl("lrlResponsible")).Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact((Guid)taskDuration.ResponsibleID), _dataManager.Contact.SelectById(SiteId, (Guid)taskDuration.ResponsibleID).UserFullName);                                                
                }
            }
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgTaskDurations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgTaskDurations_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<TaskDurationMap>)ViewState["TaskDurations"]).Remove(
                ((List<TaskDurationMap>)ViewState["TaskDurations"]).Where(s => s.ID == id).FirstOrDefault());

            if (TaskDurationsChanged != null)
                TaskDurationsChanged(this, EventArgs.Empty);
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgTaskDurations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgTaskDurations_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgTaskDurations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgTaskDurations_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgTaskDurations.MasterTableView.IsItemInserted = false;
            rgTaskDurations.Rebind();
        }



        /// <summary>
        /// Adds the duration of the task.
        /// </summary>
        /// <param name="taskDuration">Duration of the task.</param>
        public void AddTaskDuration(TaskDurationMap taskDuration)
        {
            ((List<TaskDurationMap>)ViewState["TaskDurations"]).Add(taskDuration);
            rgTaskDurations.Rebind();

            if (TaskDurationsChanged != null)
                TaskDurationsChanged(this, EventArgs.Empty);
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="taskDurationId">The taskDurationId.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid taskDurationId, GridEditableItem item)
        {
            var userControl = (UserControl)item.FindControl(GridEditFormItem.EditFormUserControlID);

            var taskDuration = ((List<TaskDurationMap>)ViewState["TaskDurations"]).Where(s => s.ID == taskDurationId).FirstOrDefault() ?? new TaskDurationMap();
            taskDuration.SectionDateStart = ((RadDateTimePicker)userControl.FindControl("rdpStartDate")).SelectedDate;
            taskDuration.SectionDateEnd = ((RadDateTimePicker)userControl.FindControl("rdpEndDate")).SelectedDate;
            taskDuration.ActualDurationHours = (int?)((RadNumericTextBox)userControl.FindControl("rntxtActualDurationHours")).Value;
            taskDuration.ActualDurationMinutes = (int?)((RadNumericTextBox)userControl.FindControl("rntxtActualDurationMinutes")).Value;
            taskDuration.ResponsibleID = ((ContactComboBox)userControl.FindControl("ucResponsible")).SelectedValue;
            taskDuration.Comment = ((TextBox)userControl.FindControl("txtComment")).Text;

            if (taskDuration.ID == Guid.Empty)
            {
                taskDuration.ID = Guid.NewGuid();
                ((List<TaskDurationMap>)ViewState["TaskDurations"]).Add(taskDuration);
            }            

            if (TaskDurationsChanged != null)
                TaskDurationsChanged(this, EventArgs.Empty);
        }



        /// <summary>
        /// Saves the specified task id.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        public void Save(Guid taskId)
        {
            using (var scope = new TransactionScope())
            {
                _dataManager.TaskDuration.DeleteAll(taskId);
                var taskDurationList = TaskDurationsList.Select(td => new tbl_TaskDuration()
                {
                    ID = td.ID,
                    SectionDateStart = td.SectionDateStart,
                    SectionDateEnd = td.SectionDateEnd,
                    ActualDurationHours = td.ActualDurationHours,
                    ActualDurationMinutes = td.ActualDurationMinutes,
                    ResponsibleID = td.ResponsibleID,
                    Comment = td.Comment
                });

                _dataManager.TaskDuration.Add(taskDurationList.ToList(), taskId);
                scope.Complete();
            }
        }
    }
}