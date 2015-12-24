using System;
using System.Linq;
using System.Web.UI;
using Labitec.LeadForce.Portal.Main.TaskModule.UserControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Main.TaskModule
{
    public partial class Task : LeadForcePortalBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            var accessCheck = Access.Check(TblUser, "Tasks");
            if (!accessCheck.Write)
                Response.Redirect(UrlsData.LFP_AccessDenied(PortalSettingsId));

            Title = "Задачи";

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucSaveTaskDuration, ucTaskDurations);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucTaskDurations, plDuration);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucSaveTaskDuration, plDuration);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnSelectMeetingTime, ucSelectMeetingTime);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnConfirm, ucSelectMeetingTime);

            ucRefusedComment.TaskId = ObjectId;

            ucSaveTaskDuration.SaveClicked += ucSaveTaskDuration_SaveClicked;
            ucTaskDurations.TaskDurationsChanged += ucTaskDurations_TaskDurationsChanged;

            if (CurrentUser.Instance != null && !CurrentUser.Instance.ContactID.HasValue)
            {
                radWindowManager.RadAlert("Текущий пользователь не имеет контактной информации. Пожалуйста обновите данные.", 420, 100, "Предупреждение", "RedirectToTasksList");
                return;
            }

            if (CurrentUser.Instance != null)
                hlCancel.NavigateUrl = UrlsData.LFP_Tasks(PortalSettingsId);
            else
                hlCancel.NavigateUrl = UrlsData.LFP_Home(PortalSettingsId);

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Handles the SaveClicked event of the ucSaveTaskDuration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.LeadForce.Portal.Main.TaskModule.UserControls.SaveTaskDuration.SaveClickEventArgs"/> instance containing the event data.</param>
        protected void ucSaveTaskDuration_SaveClicked(object sender, SaveTaskDuration.SaveClickEventArgs e)
        {
            ucTaskDurations.AddTaskDuration(e.TaskDuration);
        }



        /// <summary>
        /// Handles the TaskDurationsChanged event of the ucTaskDurations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void ucTaskDurations_TaskDurationsChanged(object sender, EventArgs e)
        {
            lrlActualDurationHours.Text = ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationHours).ToString();
            lrlActualDurationMinutes.Text = ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationMinutes).ToString();
            var completePercent = 0;

            var taskDurationActual = ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationHours) * 60 + ucTaskDurations.TaskDurationsList.Sum(td => td.ActualDurationMinutes);
            var taskDurationPlanned = int.Parse(lrlPlanDurationHours.Text) * 60 + int.Parse(lrlPlanDurationMinutes.Text);

            if (taskDurationActual >= taskDurationPlanned)
                completePercent = 100;
            else if (taskDurationPlanned != 0)
                completePercent = (int)(((taskDurationActual * 100) / taskDurationPlanned));

            ucProgressBar.UpdateProgressBar(completePercent);
            ucSaveTaskDuration.TaskDurations = ucTaskDurations.TaskDurationsList;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            var task = DataManager.Task.SelectById(SiteId, ObjectId);            
            if (task != null)
            {
                Title = string.Concat("Задачи - ", task.Title);

                var taskType = task.tbl_TaskType;

                if (CurrentUser.Instance == null && !taskType.IsPublicEvent)
                    Response.Redirect(UrlsData.LFP_Home(PortalSettingsId));

                lrlTitle.Text = task.Title;                                
                lrlTaskType.Text = task.tbl_TaskType.Title;
                lrlStartDate.Text = task.StartDate.ToString("dd.MM.yyy hh:MM");
                lrlEndDate.Text = task.EndDate.ToString("dd.MM.yyy hh:MM");
                lrlDateOfControl.Text = task.DateOfControl.ToString("dd.MM.yyy hh:MM");
                chxIsImportantTask.Checked = task.IsImportantTask;
                lrlPlanDurationHours.Text = task.PlanDurationHours.ToString();

                var responsible = DataManager.Contact.SelectById(SiteId, task.ResponsibleID);
                if (responsible != null)
                {
                    lrlResponsible.Text = responsible.UserFullName;                    
                }

                var creator = DataManager.Contact.SelectById(SiteId, task.CreatorID);
                if (creator != null)
                    lrlCreator.Text = creator.UserFullName;

                chxIsUrgentTask.Checked = task.IsUrgentTask;
                lrlPlanDurationMinutes.Text = task.PlanDurationMinutes.ToString();

                lrlTaskStatus.Text = EnumHelper.GetEnumDescription((TaskStatus)task.TaskStatusID);

                if (task.ActualDurationHours.HasValue)
                    lrlActualDurationHours.Text = task.ActualDurationHours.Value.ToString();

                if (task.ActualDurationMinutes.HasValue)
                    lrlActualDurationMinutes.Text = task.ActualDurationMinutes.Value.ToString();

                if (taskType.TaskTypeCategoryID == (int)TaskTypeCategory.Event && taskType.IsPublicEvent)
                    rbtnRegistration.Visible = true;

                if (CurrentUser.Instance != null)
                {
                    if (taskType.TaskTypeCategoryID == (int) TaskTypeCategory.LongTermTask &&
                        task.ResponsibleID == CurrentUser.Instance.ContactID)
                    {
                        ucTaskDurations.Task = new TaskMap()
                                                   {
                                                       ID = task.ID,
                                                       PlanDurationHours = task.PlanDurationHours,
                                                       PlanDurationMinutes = task.PlanDurationMinutes
                                                   };
                        ucSaveTaskDuration.Task = new TaskMap()
                                                      {
                                                          ID = task.ID,
                                                          PlanDurationHours = task.PlanDurationHours,
                                                          PlanDurationMinutes = task.PlanDurationMinutes
                                                      };
                        plDuration.Visible = true;
                        plNoteTime.Visible = true;
                        rtsTabs.Tabs[1].Visible = true;
                        rpvTaskDurations.Visible = true;
                        ucTaskDurations.BindData();
                        ucSaveTaskDuration.TaskDurations = ucTaskDurations.TaskDurationsList;
                        ucSaveTaskDuration.BindData();
                    }

                    var personalComment = DataManager.TaskPersonalComment.SelectByTaskContactId(task.ID, (Guid)CurrentUser.Instance.ContactID);
                    if (personalComment != null)
                        txtPersonalComment.Text = personalComment.Comment;

                    var taskMember = DataManager.TaskMember.SelectByContactId(ObjectId, (Guid) CurrentUser.Instance.ContactID);
                    if (taskMember != null)
                    {
                        chxIsInformed.Checked = taskMember.IsInformed;

                        if (taskMember.IsInformed)
                            plNoteTime.Visible = false;

                        if (taskMember.TaskMemberStatusID.HasValue)
                            lrlTaskMemberStatus.Text =
                                EnumHelper.GetEnumDescription((TaskMemberStatus) taskMember.TaskMemberStatusID);
                    }


                    if (taskType.TaskTypeCategoryID == (int) TaskTypeCategory.Event && taskType.IsPublicEvent)
                    {
                        rbtnRegistration.Visible = true;
                        if (taskMember != null)
                            rbtnRegistration.Visible = false;
                    }
                }                

                if (taskType.TaskTypeCategoryID == (int)TaskTypeCategory.Meeting)
                {
                    //var taskDurations = DataManager.TaskDuration.SelectAll(task.ID);

                    //if (taskDurations.Count() > 0)
                    //    rbtnSelectMeetingTime.Visible = true;                            
                        
                }

                var completePercent = 0;

                var taskDurationActual = task.ActualDurationHours * 60 + task.ActualDurationMinutes;
                var taskDurationPlanned = int.Parse(lrlPlanDurationHours.Text) * 60 + int.Parse(lrlPlanDurationMinutes.Text);

                if (taskDurationActual >= taskDurationPlanned)
                    completePercent = 100;
                else if (taskDurationActual.HasValue && taskDurationPlanned != 0)
                    completePercent = (int)(((taskDurationActual * 100) / taskDurationPlanned));

                ucProgressBar.UpdateProgressBar(completePercent);                
            }

            UpdateActions();
        }



        /// <summary>
        /// Updates the actions.
        /// </summary>
        /// <param name="taskMember">The task member.</param>
        private void UpdateActions(tbl_TaskMember taskMember = null)
        {
            if (CurrentUser.Instance == null)
                return;

            if (taskMember == null)
                taskMember = DataManager.TaskMember.SelectByContactId(ObjectId, (Guid)CurrentUser.Instance.ContactID);

            if (taskMember != null)
            {
                if (taskMember.IsInformed)
                    return;

                switch ((TaskMemberStatus)taskMember.TaskMemberStatusID)
                {
                    case TaskMemberStatus.Invited:
                        rbtnConfirm.Visible = rbtnNotInterest.Visible = rbtnFailureNoWay.Visible = true;
                        break;
                    case TaskMemberStatus.MemberConfirmed:
                    case TaskMemberStatus.OrganizerConfirmed:
                        rbtnNotInterest.Visible = rbtnFailureNoWay.Visible = true;
                        rbtnConfirm.Visible = false;
                        break;
                    case TaskMemberStatus.RefusedFailureNoWay:
                    case TaskMemberStatus.RefusedNotInterest:
                        rbtnNotInterest.Visible = rbtnFailureNoWay.Visible = false;
                        rbtnConfirm.Visible = true;
                        break;                        
                    default:
                        rbtnConfirm.Visible = rbtnNotInterest.Visible = rbtnFailureNoWay.Visible = false;
                        break;
                }
            }
        }


        /// <summary>
        /// Shows the select time dialog.
        /// </summary>
        private void ShowSelectTimeDialog()
        {
            //var task = DataManager.Task.SelectById(SiteId, ObjectId);

            //if (task != null)
            //{
            //    ucSelectMeetingTime.Task.ID = ObjectId;
            //    ucSelectMeetingTime.Task.CreatorID = task.CreatorID;
            //    ucSelectMeetingTime.Task.Title = task.Title;
            //    ucSelectMeetingTime.Task.StartDate = task.StartDate;
            //    ucSelectMeetingTime.Task.EndDate = task.EndDate;
            //    ucSelectMeetingTime.Task.PlanDurationHours = task.PlanDurationHours;
            //    ucSelectMeetingTime.Task.PlanDurationMinutes = task.PlanDurationMinutes;

            //    ucSelectMeetingTime.BindData();

            //    if (!Page.ClientScript.IsStartupScriptRegistered("ShowSelectMeetingTimeRadWindow"))
            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowSelectMeetingTimeRadWindow", "ShowSelectMeetingTimeRadWindow();", true);
            //}
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {            
            if (CurrentUser.Instance == null)
                return;

            var task = DataManager.Task.SelectById(SiteId, ObjectId);
            if (task == null)
                return;
            
            var personalComment = DataManager.TaskPersonalComment.SelectByTaskContactId(task.ID, (Guid)CurrentUser.Instance.ContactID) ?? new tbl_TaskPersonalComment();
            personalComment.Comment = txtPersonalComment.Text;

            if (personalComment.ID == Guid.Empty)
            {
                personalComment.TaskID = ObjectId;
                personalComment.ContactID = (Guid) CurrentUser.Instance.ContactID;
                DataManager.TaskPersonalComment.Add(personalComment);
            }
            else            
                DataManager.TaskPersonalComment.Update(personalComment);

            var taskType = task.tbl_TaskType;

            if (taskType.TaskTypeCategoryID == (int)TaskTypeCategory.LongTermTask && task.ResponsibleID == CurrentUser.Instance.ContactID)
            {
                ucTaskDurations.Save(task.ID);
            }

            if (taskType.TaskTypeCategoryID == (int)TaskTypeCategory.Meeting)
            {
                var taskDurations = DataManager.TaskDuration.SelectAll(task.ID);

                if (taskDurations.Count() > 0)
                    ucSelectMeetingTime.Save(task.ID);
            }

            Response.Redirect(UrlsData.LFP_Tasks(PortalSettingsId));
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnMemberStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnMemberStatus_OnClick(object sender, EventArgs e)
        {
            if (CurrentUser.Instance == null)
                return;

            ChangeMemberStatus(((RadButton)sender).CommandArgument);
        }



        /// <summary>
        /// Changes the member status.
        /// </summary>
        /// <param name="command">The command.</param>
        private void ChangeMemberStatus(string command)
        {
            var taskMember = DataManager.TaskMember.SelectByContactId(ObjectId, (Guid)CurrentUser.Instance.ContactID);
            
            switch (command)
            {
                case "Confirm":
                    taskMember.TaskMemberStatusID = (int)TaskMemberStatus.MemberConfirmed;
                    ShowSelectTimeDialog();
                    break;
                case "NotInterest":
                    taskMember.TaskMemberStatusID = (int)TaskMemberStatus.RefusedNotInterest;
                    if (!Page.ClientScript.IsStartupScriptRegistered("ShowRefusedCommentRadWindow"))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowRefusedCommentRadWindow", "ShowRefusedCommentRadWindow();", true);
                    break;
                case "FailureNoWay":
                    taskMember.TaskMemberStatusID = (int)TaskMemberStatus.RefusedFailureNoWay;
                    if (!Page.ClientScript.IsStartupScriptRegistered("ShowRefusedCommentRadWindow"))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowRefusedCommentRadWindow", "ShowRefusedCommentRadWindow();", true);
                    break;
            }
            
            DataManager.TaskMember.Update(taskMember);
            UpdateActions(taskMember);

            if (taskMember.TaskMemberStatusID.HasValue)
                lrlTaskMemberStatus.Text = EnumHelper.GetEnumDescription((TaskMemberStatus)taskMember.TaskMemberStatusID);
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnRegistration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnRegistration_OnClick(object sender, EventArgs e)
        {
            if (CurrentUser.Instance == null)
                return;

            var taskMember = DataManager.TaskMember.SelectByContactId(ObjectId, (Guid) CurrentUser.Instance.ContactID);
            
            if (taskMember == null)
            {
                var contact = DataManager.Contact.SelectById(CurrentUser.Instance.SiteID, (Guid)CurrentUser.Instance.ContactID);
                if (contact != null)
                {
                    taskMember = new tbl_TaskMember
                                     {
                                         TaskID = ObjectId,
                                         ContractorID = contact.CompanyID,
                                         ContactID = contact.ID,
                                         TaskMemberRoleID = (int) TaskMemberRole.Member,
                                         TaskMemberStatusID = (int) TaskMemberStatus.BidGiven
                                     };

                    DataManager.TaskMember.Add(taskMember);
                }

                lrlSuccessMessage.Text = string.Format("<div class=\"success\">Вы успешно зарегистрировались на мероприятии.</div><br/>");
            }
            else            
                lrlSuccessMessage.Text = string.Format("<div class=\"success\">Вы уже зарегистрированы на этом мероприятии.</div><br/>");            
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnSelectMeetingTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnSelectMeetingTime_OnClick(object sender, EventArgs e)
        {
            if (CurrentUser.Instance == null)
                return;

            ShowSelectTimeDialog();     
        }        
    }
}