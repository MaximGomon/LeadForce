using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;
using System.Web.UI;

namespace WebCounter.AdminPanel
{
    public partial class Task : LeadForceBasePage
    {        
        private Guid _taskId;

        #region Properties

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public TaskStatus CurrentTaskStatus
        {
            get
            {
                object o = ViewState["CurrentTaskStatus"];
                return (o == null ? TaskStatus.Charged : (TaskStatus)o);
            }
            set
            {
                ViewState["CurrentTaskStatus"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ResponsibleId
        {
            get
            {
                object o = Session["ResponsibleId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                Session["ResponsibleId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid CreatorId
        {
            get
            {
                object o = Session["CreatorId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                Session["CreatorId"] = value;
            }
        }

        #endregion


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            Title = "Задача - LeadForce";            

            if (!CurrentUser.Instance.ContactID.HasValue)
            {
                radWindowManager.RadAlert("Текущий пользователь не имеет контактной информации. Пожалуйста обновите данные.", 420, 100, "Предупреждение", "RedirectToTasksList");
                return;
            }

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbTaskType, ucTaskDurations);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbTaskType, ucTaskMembers);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbTaskType, rtsTabs);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbTaskType, ucMainTaskMember);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucSaveTaskDuration, ucTaskDurations);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucTaskDurations, plDuration);            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucSaveTaskDuration, plDuration);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucResponsible, ucTaskMembers);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnCharg, plStatuses);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnCharg, ucResponsible);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucResponsible, upCharg);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnRun, plResult);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnRun, plWorkflowResult);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbProducts, ucTaskMembers);
            
            if (!string.IsNullOrEmpty(Request.QueryString["ctid"]))
                ucTaskMembers.ContactId = Guid.Parse(Request.QueryString["ctid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["cyid"]))
                ucTaskMembers.CompanyId = Guid.Parse(Request.QueryString["cyid"]);

            ucTaskMembers.SiteId = SiteId;
            ucTaskDurations.SiteId = SiteId;
            ucSaveTaskDuration.SiteId = SiteId;

            ucSaveTaskDuration.SaveClicked += ucSaveTaskDuration_SaveClicked;
            ucTaskDurations.TaskDurationsChanged += ucTaskDurations_TaskDurationsChanged;
            ucResponsible.SelectedIndexChanged += ucResponsible_SelectedIndexChanged;
            rdpStartDate.SelectedDateChanged += rdpStartDate_SelectedDateChanged;
            rdpEndDate.SelectedDateChanged += rdpEndDate_SelectedDateChanged;
            dcbTaskType.SelectedIndexChanged += dcbTaskType_SelectedIndexChanged;

            if (Page.RouteData.Values["id"] != null)
                _taskId = Guid.Parse(Page.RouteData.Values["id"] as string);            

            ucTaskHistories.TaskId = _taskId;            

            hlCancel.NavigateUrl = UrlsData.AP_Tasks();

            if (!Page.IsPostBack)
                BindData();
        }


        #region Methods

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            dcbProducts.SiteID = dcbOrders.SiteID = dcbTaskResult.SiteID = dcbTaskType.SiteID = SiteId;            

            var task = DataManager.Task.SelectById(SiteId, _taskId);
            if (task != null)
            {
                var taskMap = new TaskMap()
                                  {
                                      ID = task.ID,
                                      PlanDurationHours = task.PlanDurationHours,
                                      PlanDurationMinutes = task.PlanDurationMinutes,
                                      Title = task.Title,
                                      StartDate = task.StartDate,
                                      EndDate = task.EndDate,
                                      MainMemberCompanyID = task.MainMemberCompanyID,
                                      MainMemberContactID = task.MainMemberContactID
                                  };

                ucTaskMembers.Task = taskMap;                
                ucTaskDurations.Task = taskMap;
                ucMainTaskMember.Task = taskMap;
                ucSaveTaskDuration.Task = taskMap;

                txtTitle.Text = task.Title;
                dcbTaskType.SelectedId = task.TaskTypeID;
                rdpStartDate.SelectedDate = task.StartDate;
                rdpEndDate.SelectedDate = task.EndDate;
                rdpDateOfControl.SelectedDate = task.DateOfControl;
                rdpDateOfControl.Enabled = (Guid)CurrentUser.Instance.ContactID == task.CreatorID;
                
                chxIsImportantTask.Checked = task.IsImportantTask;
                rntxtPlanDurationHours.Value = task.PlanDurationHours;
                rntxtPlanDurationMinutes.Value = task.PlanDurationMinutes;
                ucResponsible.SelectedValue = task.ResponsibleID;
                ucRadWindowResponsible.SelectedValue = task.ResponsibleID;
                rcbCreator.Items.Add(new RadComboBoxItem(DataManager.Contact.SelectById(SiteId, task.CreatorID).UserFullName));
                rdpResponsibleReminderDate.SelectedDate = task.ResponsibleReminderDate;
                rdpCreatorReminderDate.SelectedDate = task.CreatorReminderDate;
                chxIsUrgentTask.Checked = task.IsUrgentTask;
                SetStatus((TaskStatus)task.TaskStatusID);
                if (task.TaskResultID.HasValue)
                    dcbTaskResult.SelectedId = (Guid)task.TaskResultID;
                txtDetailedResult.Text = task.DetailedResult;

                // Workflow
                var isWorkflow = WorkflowProcessing.IsWorkflow(task.ID);
                rdpDateOfControl.Enabled = !isWorkflow;
                if (isWorkflow)
                {
                    var workflowTemplateElementResults = WorkflowProcessing.WorkflowElementResultsByValue(task.ID);
                    ddlWorkflowResult.Items.Add(new ListItem("Выберите значение", ""));
                    foreach (var workflowTemplateElementResult in workflowTemplateElementResults)
                    {
                        if (!workflowTemplateElementResult.IsSystem)
                            ddlWorkflowResult.Items.Add(new ListItem(workflowTemplateElementResult.Name, workflowTemplateElementResult.ID.ToString()));
                    }
                        
                }

                var taskType = task.tbl_TaskType;

                //ucTaskMembers.TypeCategory = (TaskTypeCategory)task.tbl_TaskType.TaskTypeCategoryID;
                //ucTaskDurations.Visible = plDuration.Visible = (TaskTypeCategory)task.tbl_TaskType.TaskTypeCategoryID == TaskTypeCategory.LongTermTask;
                //if ((TaskTypeCategory)task.tbl_TaskType.TaskTypeCategoryID == TaskTypeCategory.LongTermTask)
                //{
                //    ucTaskDurations.BindData();
                //    ucSaveTaskDuration.TaskDurations = ucTaskDurations.TaskDurationsList;
                //    ucSaveTaskDuration.BindData();
                //    ucProgressBar.UpdateProgressBar(CalculateCompletePercent());
                //}  

                if (task.OrderID.HasValue)
                    dcbOrders.SelectedId = (Guid)task.OrderID;

                dcbProducts.SelectedIdNullable = task.ProductID;

                ucTaskMembers.ProductId = task.ProductID;

                UpdateInterfaceRelatedTaskType(taskType);                                

                rntxtActualDurationHours.Value = task.ActualDurationHours;
                rntxtActualDurationMinutes.Value = task.ActualDurationMinutes;
                rntxtCompletePercentage.Value = (double?)task.CompletePercentage;


                CreatorId = task.CreatorID;
                ResponsibleId = task.ResponsibleID;

                CheckUpdatePlanDurationRights(taskType);                
            }
            else
            {
                CreatorId = (Guid)CurrentUser.Instance.ContactID;
                rcbCreator.Items.Add(new RadComboBoxItem(DataManager.Contact.SelectById(SiteId, (Guid)CurrentUser.Instance.ContactID).UserFullName));
                ucResponsible.SelectedValue = CurrentUser.Instance.ContactID;
                ucRadWindowResponsible.SelectedValue = CurrentUser.Instance.ContactID;
                rdpStartDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0, 0);
                SetStatus(TaskStatus.Planned);
                ucProgressBar.UpdateProgressBar(0);

                if (Session["sd"] != null && Session["ed"] != null)
                {
                    rdpStartDate.SelectedDate = DateTime.Parse(Session["sd"].ToString());
                    rdpEndDate.SelectedDate = DateTime.Parse(Session["ed"].ToString());
                    Session["sd"] = Session["ed"] = null;
                }
            }
        }



        /// <summary>
        /// Proceeds the type of the task.
        /// </summary>
        private void ProceedTaskType(bool updateDuration = false)
        {
            if (dcbTaskType.SelectedId != Guid.Empty)
            {
                var taskType = DataManager.TaskType.SelectById(SiteId, dcbTaskType.SelectedId);
                if (taskType != null)
                {
                    rdpEndDate.SelectedDate = rdpStartDate.SelectedDate.Value.AddHours(taskType.StandardDurationHours).AddMinutes(taskType.StandardDurationMinutes);
                    rdpDateOfControl.SelectedDate = rdpEndDate.SelectedDate;

                    if (updateDuration)
                    {
                        rntxtPlanDurationHours.Value = taskType.StandardDurationHours;
                        rntxtPlanDurationMinutes.Value = taskType.StandardDurationMinutes;
                        CheckUpdatePlanDurationRights(taskType);
                    }

                    UpdateInterfaceRelatedTaskType(taskType);
                }
            }
        }



        /// <summary>
        /// Updates the type of the UI related task.
        /// </summary>
        /// <param name="taskType">Type of the task.</param>
        private void UpdateInterfaceRelatedTaskType(tbl_TaskType taskType)
        {
            if ((TaskTypeCategory)taskType.TaskTypeCategoryID == TaskTypeCategory.LongTermTask)
            {
                ucTaskDurations.Visible = true;
                ucTaskDurations.BindData();
                plDuration.Visible = true;
                ucSaveTaskDuration.BindData();
                ucProgressBar.UpdateProgressBar(CalculateCompletePercent());
            }
            else
            {
                plDuration.Visible = false;
                ucTaskDurations.Visible = false;
            }

            if (taskType.TaskTypePaymentSchemeID == (int)TaskTypePaymentScheme.None)
            {
                plOrders.Visible = true;
                dcbProducts.SelectedId = Guid.Empty;
                plProductForPaying.Visible = false;
            }
            else
            {
                if (taskType.ProductID.HasValue && _taskId == Guid.Empty && dcbProducts.SelectedId == Guid.Empty)
                    dcbProducts.SelectedId = (Guid)taskType.ProductID;

                plProductForPaying.Visible = true;
                dcbOrders.SelectedId = Guid.Empty;
                plOrders.Visible = false;
            }

            rtsTabs.Tabs[1].Visible = false;
            rtsTabs.Tabs[2].Visible = false;

            if (taskType.TaskMembersCountID.HasValue)
            {
                if (taskType.TaskMembersCountID == (int)TaskMembersCount.One || taskType.TaskMembersCountID == (int)TaskMembersCount.MainPlusList)
                    rtsTabs.Tabs[1].Visible = true;
                if (taskType.TaskMembersCountID == (int)TaskMembersCount.List || taskType.TaskMembersCountID == (int)TaskMembersCount.MainPlusList)
                    rtsTabs.Tabs[2].Visible = true;
            }

            ucTaskMembers.TypePaymentScheme = (TaskTypePaymentScheme)taskType.TaskTypePaymentSchemeID;
            ucTaskMembers.TypeCategory = (TaskTypeCategory)taskType.TaskTypeCategoryID;
            if (taskType.TaskMembersCountID.HasValue)
                ucTaskMembers.MembersCount = (TaskMembersCount)taskType.TaskMembersCountID;
            ucTaskMembers.UpdateActions();

            ucMainTaskMember.TypePaymentScheme = (TaskTypePaymentScheme)taskType.TaskTypePaymentSchemeID;
            ucMainTaskMember.BindData();
        }



        /// <summary>
        /// Checks the update plan duration rights.
        /// </summary>
        /// <param name="taskType">Type of the task.</param>
        /// <returns></returns>
        private bool CheckUpdatePlanDurationRights(tbl_TaskType taskType)
        {
            switch ((TaskTypeAdjustDuration)taskType.TaskTypeAdjustDurationID)
            {
                case TaskTypeAdjustDuration.None:
                    rntxtPlanDurationHours.Enabled = rntxtPlanDurationMinutes.Enabled = false;
                    EnableDisableRadDateTimePicker(rdpEndDate, false);
                    break;
                case TaskTypeAdjustDuration.OnlyCreator:
                    if ((Guid)CurrentUser.Instance.ContactID != CreatorId)
                    {
                        rntxtPlanDurationHours.Enabled = rntxtPlanDurationMinutes.Enabled = false;
                        EnableDisableRadDateTimePicker(rdpEndDate, false);
                    }
                    else
                    {
                        rntxtPlanDurationHours.Enabled = rntxtPlanDurationMinutes.Enabled = true;
                        EnableDisableRadDateTimePicker(rdpEndDate, true);
                    }
                    break;
                case TaskTypeAdjustDuration.CreatorOrResponsible:
                    if ((Guid)CurrentUser.Instance.ContactID != CreatorId && (Guid)CurrentUser.Instance.ContactID != ResponsibleId)
                    {
                        rntxtPlanDurationHours.Enabled = rntxtPlanDurationMinutes.Enabled = false;
                        EnableDisableRadDateTimePicker(rdpEndDate, false);
                    }
                    else
                    {
                        rntxtPlanDurationHours.Enabled = rntxtPlanDurationMinutes.Enabled = true;
                        EnableDisableRadDateTimePicker(rdpEndDate, true);
                    }
                    break;
            }

            return rntxtPlanDurationMinutes.Enabled;
        }



        /// <summary>
        /// Enables the disable RAD date time picker.
        /// </summary>
        /// <param name="radDateTimePicker">The RAD date time picker.</param>
        /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
        protected void EnableDisableRadDateTimePicker(RadDateTimePicker radDateTimePicker, bool isEnabled)
        {
            radDateTimePicker.Enabled = isEnabled;
            radDateTimePicker.DateInput.Enabled = isEnabled;
            radDateTimePicker.TimePopupButton.Enabled = isEnabled;
            radDateTimePicker.DatePopupButton.Enabled = isEnabled;
        }



        /// <summary>
        /// Calculates the complete percent.
        /// </summary>        
        /// <returns></returns>
        public int CalculateCompletePercent()
        {
            var completePecent = 0;

            var taskDurationActual = ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationHours * 60 + td.ActualDurationMinutes);
            var taskDurationPlanned = rntxtPlanDurationHours.Value * 60 + rntxtPlanDurationMinutes.Value;

            if (taskDurationActual >= taskDurationPlanned)
                completePecent = 100;
            else if (taskDurationPlanned.HasValue && taskDurationPlanned != 0)
                completePecent = (int)(((taskDurationActual * 100) / taskDurationPlanned));

            if (CurrentTaskStatus == TaskStatus.Planned && completePecent > 0)
                SetStatus(TaskStatus.InWork);

            return completePecent;
        }




        /// <summary>
        /// Sets the button status text.
        /// </summary>
        /// <param name="taskStatus">The task status.</param>
        private void SetStatus(TaskStatus taskStatus)
        {
            CurrentTaskStatus = taskStatus;

            plResult.Visible = taskStatus == TaskStatus.Completed;

            if (WorkflowProcessing.IsWorkflow(_taskId))
            {
                plResult.Visible = false;
                plWorkflowResult.Visible = taskStatus == TaskStatus.Completed;
            }

            lrlTaskStatus.Text = EnumHelper.GetEnumDescription(taskStatus);

            rbtnRun.Visible = rbtnCancel.Visible = rbtnHoldOver.Visible = false;
            rbtnAddToPlan.Visible = rbtnCharg.Visible = rbtnReject.Visible = false;

            if (taskStatus == TaskStatus.Planned || taskStatus == TaskStatus.InWork)
                rbtnRun.Visible = rbtnCancel.Visible = rbtnHoldOver.Visible = true;

            if (taskStatus == TaskStatus.Charged || taskStatus == TaskStatus.HoldOver)
                rbtnAddToPlan.Visible = true;

            if (taskStatus == TaskStatus.Planned || taskStatus == TaskStatus.Rejected)
                rbtnCharg.Visible = true;

            if (taskStatus == TaskStatus.Charged)
                rbtnReject.Visible = true;

            if (taskStatus == TaskStatus.Planned)
                CalculateCompletePercent();
        }



        /// <summary>
        /// Updates the reminder.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="reminderDate">The reminder date.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="taskId">The task id.</param>
        protected void UpdateReminder(Guid contactId, DateTime reminderDate, Guid moduleId, Guid taskId)
        {
            var reminder = DataManager.Reminder.SelectByObjectId(CreatorId, _taskId) ?? new tbl_Reminder();
            reminder.Title = txtTitle.Text;
            reminder.ReminderDate = reminderDate;
            reminder.ContactID = contactId;
            reminder.ModuleID = moduleId;
            reminder.ObjectID = taskId;

            if (reminder.ID == Guid.Empty)
                DataManager.Reminder.Add(reminder);
            else
                DataManager.Reminder.Update(reminder);
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the TaskDurationsChanged event of the ucTaskDurations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void ucTaskDurations_TaskDurationsChanged(object sender, EventArgs e)
        {            
            rntxtActualDurationHours.Value = ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationHours);
            rntxtActualDurationMinutes.Value = ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationMinutes);
            ucProgressBar.UpdateProgressBar(CalculateCompletePercent());
            ucSaveTaskDuration.TaskDurations = ucTaskDurations.TaskDurationsList;            
        }



        /// <summary>
        /// Handles the SaveClicked event of the ucSaveTaskDuration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.Task.SaveTaskDuration.SaveClickEventArgs"/> instance containing the event data.</param>
        protected void ucSaveTaskDuration_SaveClicked(object sender, UserControls.Task.SaveTaskDuration.SaveClickEventArgs e)
        {
            ucTaskDurations.AddTaskDuration(e.TaskDuration);            
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the dcbTaskType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbTaskType_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ProceedTaskType(true);
        }



        /// <summary>
        /// Handles the SelectedDateChanged event of the rdpStartDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpStartDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            ProceedTaskType();
        }



        /// <summary>
        /// Handles the SelectedDateChanged event of the rdpEndDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpEndDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (rdpStartDate.SelectedDate.HasValue && rdpEndDate.SelectedDate.HasValue && rdpEndDate.SelectedDate > rdpStartDate.SelectedDate)
            {
                rntxtPlanDurationHours.Value = (rdpEndDate.SelectedDate - rdpStartDate.SelectedDate).Value.Hours;
                rntxtPlanDurationMinutes.Value = (rdpEndDate.SelectedDate - rdpStartDate.SelectedDate).Value.Minutes;                
            }
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ucResponsible control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucResponsible_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CurrentUser.Instance.ContactID == ucResponsible.SelectedValue)            
                SetStatus(TaskStatus.Planned);
            else
                SetStatus(TaskStatus.Charged);

            ucRadWindowResponsible.SelectedValue = ucResponsible.SelectedValue;

            if (ucResponsible.SelectedValue.HasValue)
                ucTaskMembers.UpdateResponsible((Guid)ucResponsible.SelectedValue);
        }



        /// <summary>
        /// Handles the OnTextChanged event of the rntxtPlanDuration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rntxtPlanDuration_OnTextChanged(object sender, EventArgs e)
        {
            if (rdpStartDate.SelectedDate.HasValue)
            {                
                if (rntxtPlanDurationHours.Value.HasValue && rntxtPlanDurationMinutes.Value.HasValue)
                    rdpEndDate.SelectedDate = rdpStartDate.SelectedDate.Value.AddHours((int)rntxtPlanDurationHours.Value).AddMinutes((int)rntxtPlanDurationMinutes.Value);
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var task = DataManager.Task.SelectById(SiteId, _taskId) ?? new tbl_Task();
            var isWorkflow = WorkflowProcessing.IsWorkflow(_taskId);

            var needAddHistory = false;
            var needAddCreatorReminder = false;

            if (task.ID == Guid.Empty)
                needAddHistory = true;
            else if (task.StartDate != rdpStartDate.SelectedDate || task.EndDate != rdpEndDate.SelectedDate || task.DateOfControl != rdpDateOfControl.SelectedDate
                || task.IsImportantTask != chxIsImportantTask.Checked || task.PlanDurationHours != rntxtPlanDurationHours.Value || task.PlanDurationMinutes != rntxtPlanDurationMinutes.Value
                || task.ResponsibleID != ucResponsible.SelectedValue || task.TaskStatusID != (int)CurrentTaskStatus || task.TaskResultID != (dcbTaskResult.SelectedId == Guid.Empty ? null : (Guid?)dcbTaskResult.SelectedId)
                || task.DetailedResult != txtDetailedResult.Text)
                needAddHistory = true;

            if (task.ID != Guid.Empty && task.TaskStatusID != (int)CurrentTaskStatus && task.CreatorID != task.ResponsibleID && (CurrentTaskStatus == TaskStatus.Completed || CurrentTaskStatus == TaskStatus.Canceled || CurrentTaskStatus == TaskStatus.Rejected))
                needAddCreatorReminder = true;

            task.Title = txtTitle.Text;
            if (dcbTaskType.SelectedId != Guid.Empty)
                task.TaskTypeID = dcbTaskType.SelectedId;

            if (rdpStartDate.SelectedDate.HasValue)
                task.StartDate = (DateTime)rdpStartDate.SelectedDate;

            // && (!WorkflowProcessing.IsWorkflow(_taskId) || _taskId == Guid.Empty)
            if (rdpDateOfControl.SelectedDate.HasValue && ((Guid)CurrentUser.Instance.ContactID == task.CreatorID || _taskId == Guid.Empty))
                task.DateOfControl = (DateTime)rdpDateOfControl.SelectedDate;

            task.IsImportantTask = chxIsImportantTask.Checked;

            var taskType = DataManager.TaskType.SelectById(SiteId, task.TaskTypeID);

            if (CheckUpdatePlanDurationRights(taskType))
            {
                if (rntxtPlanDurationHours.Value.HasValue)
                    task.PlanDurationHours = (int)rntxtPlanDurationHours.Value;

                if (rntxtPlanDurationMinutes.Value.HasValue)
                    task.PlanDurationMinutes = (int)rntxtPlanDurationMinutes.Value;

                if (rdpEndDate.SelectedDate.HasValue)
                    task.EndDate = (DateTime)rdpEndDate.SelectedDate;
            }
            else
            {
                task.PlanDurationHours = taskType.StandardDurationHours;
                task.PlanDurationMinutes = taskType.StandardDurationMinutes;
                task.EndDate = rdpStartDate.SelectedDate.Value.AddHours(taskType.StandardDurationHours).AddMinutes(taskType.StandardDurationMinutes);
            }

            task.ResponsibleID = (Guid)ucResponsible.SelectedValue;
            task.ResponsibleReminderDate = rdpResponsibleReminderDate.SelectedDate;
            task.CreatorReminderDate = rdpCreatorReminderDate.SelectedDate;
            task.IsUrgentTask = chxIsUrgentTask.Checked;
            task.TaskStatusID = (int)CurrentTaskStatus;

            if (dcbTaskResult.SelectedId != Guid.Empty)
                task.TaskResultID = dcbTaskResult.SelectedId;
            else
                task.TaskResultID = null;

            task.DetailedResult = txtDetailedResult.Text;

            if (((TaskTypeCategory)taskType.TaskTypeCategoryID) == TaskTypeCategory.LongTermTask)
            {
                task.ActualDurationHours = ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationHours);
                task.ActualDurationMinutes = ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationMinutes);
                task.CompletePercentage = CalculateCompletePercent();
            }
            else
            {
                if (CurrentTaskStatus == TaskStatus.Completed)
                {
                    var date = task.EndDate - task.StartDate;
                    task.ActualDurationHours = date.Hours;
                    task.ActualDurationMinutes = date.Minutes;
                    task.CompletePercentage = 100;
                }
            }

            if (dcbProducts.SelectedId != Guid.Empty)
                task.ProductID = dcbProducts.SelectedId;
            else
                task.ProductID = null;

            if (dcbOrders.SelectedId != Guid.Empty)
                task.OrderID = dcbOrders.SelectedId;
            else
                task.OrderID = null;

            if (task.ID == Guid.Empty)
            {
                task.SiteID = SiteId;
                task.CreatorID = (Guid)CurrentUser.Instance.ContactID;
                DataManager.Task.Add(task);
            }
            else
                DataManager.Task.Update(task);

            var redirect = ucTaskMembers.Save(task.ID);

            if (((TaskTypeCategory)taskType.TaskTypeCategoryID) == TaskTypeCategory.LongTermTask)
                ucTaskDurations.Save(task.ID);

            if (taskType.TaskMembersCountID == (int)TaskMembersCount.MainPlusList || taskType.TaskMembersCountID == (int)TaskMembersCount.One)
                ucMainTaskMember.Save(task.ID);

            if (needAddHistory)
            {
                var taskHistory = new tbl_TaskHistory
                {
                    TaskID = task.ID,
                    DateStart = task.StartDate,
                    DateEnd = task.EndDate,
                    DateOfControl = task.DateOfControl,
                    IsImportantTask = task.IsImportantTask,
                    PlanDurationHours = task.PlanDurationHours,
                    PlanDurationMinutes = task.PlanDurationMinutes,
                    ResponsibleID = task.ResponsibleID,
                    TaskStatusID = task.TaskStatusID,
                    TaskResultID = task.TaskResultID,
                    DetailedResult = task.DetailedResult
                };
                DataManager.TaskHistory.Add(taskHistory);
            }

            var module = DataManager.Module.SelectByName("Tasks");

            if (rdpCreatorReminderDate.SelectedDate.HasValue)
                UpdateReminder(task.CreatorID, rdpCreatorReminderDate.SelectedDate.Value, module.ID, task.ID);

            if (rdpResponsibleReminderDate.SelectedDate.HasValue)
                UpdateReminder(task.ResponsibleID, rdpResponsibleReminderDate.SelectedDate.Value, module.ID, task.ID);

            if (needAddCreatorReminder)
                UpdateReminder(task.CreatorID, DateTime.Now, module.ID, task.ID);

            // Workflow
            if (isWorkflow)
            {
                switch (CurrentTaskStatus)
                {
                    case TaskStatus.Completed:
                        WorkflowProcessing.Processing(WorkflowProcessing.WorkflowElementByValue(task.ID), ddlWorkflowResult.SelectedValue);
                        break;
                    case TaskStatus.Canceled:
                        WorkflowProcessing.Processing(WorkflowProcessing.WorkflowElementByValue(task.ID), WorkflowProcessing.WorkflowElementResult(task.ID, "Задача отменена").ID.ToString());
                        break;
                }
            }

            if (redirect)
                Response.Redirect(UrlsData.AP_Tasks());
            else
            {
                ucNotificationMessage.Text = "Ошибка обновления участников в задаче. Возможно текущие данные уже обновлены.";
            }
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnStatus_OnClick(object sender, EventArgs e)
        {
            var status = ((RadButton)sender).CommandArgument;
            switch (status)
            {
                case "Run":
                    SetStatus(TaskStatus.Completed);
                    break;
                case "Cancel":
                    SetStatus(TaskStatus.Canceled);
                    break;
                case "HoldOver":
                    SetStatus(TaskStatus.HoldOver);
                    break;
                case "AddToPlan":
                    SetStatus(TaskStatus.Planned);
                    break;
                case "Charg":                    
                    if (!Page.ClientScript.IsStartupScriptRegistered("ShowChargRadWindow"))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowChargRadWindow", "ShowChargRadWindow();", true);
                    break;
                case "Reject":
                    SetStatus(TaskStatus.Rejected);
                    break;
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnCharg control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnCharg_OnClick(object sender, EventArgs e)
        {
            SetStatus(TaskStatus.Charged);

            ucResponsible.SelectedValue = ucRadWindowResponsible.SelectedValue;

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseChargRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseChargRadWindow", "CloseChargRadWindow();", true);
        }

        #endregion


        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbProducts_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucTaskMembers.ProductId = dcbProducts.SelectedIdNullable;
        }
    }
}