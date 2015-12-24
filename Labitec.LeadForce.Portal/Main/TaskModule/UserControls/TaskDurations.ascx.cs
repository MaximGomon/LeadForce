using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.LeadForce.Portal.Shared.UserControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Main.TaskModule.UserControls
{
    public partial class TaskDurations : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();        
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
            rgTaskDurations.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
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
                    var contact = _dataManager.Contact.SelectById(CurrentUser.Instance.SiteID, (Guid) taskDuration.ResponsibleID);
                    if (contact != null)
                        ((Literal)e.Item.FindControl("lrlResponsible")).Text = contact.UserFullName;
                }
            }
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