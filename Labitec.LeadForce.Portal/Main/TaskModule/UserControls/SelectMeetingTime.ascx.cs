﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Main.TaskModule.UserControls
{
    public partial class SelectMeetingTime : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public TaskMap Task
        {
            get
            {
                if (ViewState["Task"] == null)
                    ViewState["Task"] = new TaskMap();
                return (TaskMap)ViewState["Task"];
            }
            set
            {
                ViewState["Task"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rScheduler.Culture = new CultureInfo("ru-RU");
            rScheduler.FirstDayOfWeek = DayOfWeek.Monday;
            rScheduler.LastDayOfWeek = DayOfWeek.Sunday;

            if (!Page.IsPostBack)
            {
                rScheduler.DataSource = new List<TaskDurationMap>();
                rScheduler.DataKeyField = "ID";
                rScheduler.DataSubjectField = "Comment";
                rScheduler.DataStartField = "SectionDateStart";
                rScheduler.DataEndField = "SectionDateEnd";
                rScheduler.DataBind();
            }
        }


        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            var dataManager = new DataManager();
            var taskDurations = new List<TaskDurationMap>();

            if (Task.ID != Guid.Empty)
            {
                if (ViewState["TaskDurations"] == null)
                {
                    taskDurations = dataManager.TaskDuration.SelectAll(Task.ID).Select(
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

                    ViewState["TaskDurations"] = taskDurations;
                }

                taskDurations = (List<TaskDurationMap>)ViewState["TaskDurations"];
            }
            else
                ViewState["TaskDurations"] = taskDurations;
            

            rScheduler.DataSource = taskDurations;
            rScheduler.DataKeyField = "ID";
            rScheduler.DataSubjectField = "Comment";
            rScheduler.DataStartField = "SectionDateStart";
            rScheduler.DataEndField = "SectionDateEnd";
            rScheduler.DataBind();

            rScheduler.SelectedDate = Task.StartDate;
        }



        /// <summary>
        /// Handles the OnAppointmentUpdate event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentUpdateEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnAppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {
            var actualTime = ((List<TaskDurationMap>)ViewState["TaskDurations"]).Sum(tdp => tdp.ActualDurationHours * 60 + tdp.ActualDurationMinutes);
            actualTime = actualTime + (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Hours + (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Minutes;

            if (actualTime <= (Task.PlanDurationHours * 60 + Task.PlanDurationMinutes))
            {
                var taskDuration = ((List<TaskDurationMap>)ViewState["TaskDurations"]).Where(tdm => tdm.ID == (Guid)e.ModifiedAppointment.ID).SingleOrDefault();
                if (taskDuration != null)
                {
                    taskDuration.Comment = e.ModifiedAppointment.Subject;
                    taskDuration.SectionDateStart = e.ModifiedAppointment.Start;
                    taskDuration.SectionDateEnd = e.ModifiedAppointment.End;
                    taskDuration.ActualDurationHours = (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Hours;
                    taskDuration.ActualDurationMinutes = (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Minutes;
                }
            }
            else
            {
                if (!Page.ClientScript.IsStartupScriptRegistered("UpdateAlert"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "UpdateAlert", "alert('Указан не правильный промежуток');", true);
            }

            BindData();
        }



        /// <summary>
        /// Handles the OnAppointmentDelete event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentDeleteEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnAppointmentDelete(object sender, AppointmentDeleteEventArgs e)
        {
            ((List<TaskDurationMap>)ViewState["TaskDurations"]).Remove(((List<TaskDurationMap>)ViewState["TaskDurations"]).Where(tdm => tdm.ID == (Guid)e.Appointment.ID).SingleOrDefault());
            BindData();
        }



        /// <summary>
        /// Handles the OnAppointmentInsert event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentInsertEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnAppointmentInsert(object sender, AppointmentInsertEventArgs e)
        {
            var actualTime = ((List<TaskDurationMap>)ViewState["TaskDurations"]).Sum(tdp => tdp.ActualDurationHours * 60 + tdp.ActualDurationMinutes);
            actualTime = actualTime + (e.Appointment.End - e.Appointment.Start).Hours + (e.Appointment.End - e.Appointment.Start).Minutes;

            if (actualTime <= (Task.PlanDurationHours * 60 + Task.PlanDurationMinutes))
            {
                var taskDurationMap = new TaskDurationMap
                {
                    ID = Guid.NewGuid(),
                    SectionDateStart = e.Appointment.Start,
                    SectionDateEnd = e.Appointment.End,
                    ActualDurationHours = (e.Appointment.End - e.Appointment.Start).Hours,
                    ActualDurationMinutes = (e.Appointment.End - e.Appointment.Start).Minutes,
                    Comment = e.Appointment.Subject,
                    ResponsibleID = CurrentUser.Instance.ContactID
                };

                ((List<TaskDurationMap>)ViewState["TaskDurations"]).Add(taskDurationMap);
            }
            else
            {
                if (!Page.ClientScript.IsStartupScriptRegistered("InsertAlert"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "InsertAlert", "alert('Указан не правильный промежуток');", true);
            }

            BindData();
        }



        /// <summary>
        /// Handles the OnAppointmentDataBound event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.SchedulerEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnAppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            var taskDuration = e.Appointment.DataItem as TaskDurationMap;

            if (taskDuration != null)
            {
                if (taskDuration.ResponsibleID == Task.CreatorID)
                {
                    e.Appointment.CssClass = "disable-delete";
                    e.Appointment.BackColor = Color.White;
                    e.Appointment.AllowEdit = false;
                    e.Appointment.AllowDelete = false;
                }
                else if (taskDuration.ResponsibleID != CurrentUser.Instance.ContactID)
                {
                    e.Appointment.CssClass = "disable-delete";
                    e.Appointment.BackColor = Color.ForestGreen;
                    e.Appointment.AllowEdit = false;
                    e.Appointment.AllowDelete = false;
                }
            }
        }



        /// <summary>
        /// Saves the specified task id.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        public void Save(Guid taskId)
        {
            if (ViewState["TaskDurations"] != null)
            {
                var dataManager = new DataManager();

                using (var scope = new TransactionScope())
                {
                    dataManager.TaskDuration.DeleteAll(taskId);

                    var taskDurationList = ((List<TaskDurationMap>)ViewState["TaskDurations"]).Where(td => !td.IsTask).Select(td => new tbl_TaskDuration()
                    {
                        ID = td.ID,
                        SectionDateStart = td.SectionDateStart,
                        SectionDateEnd = td.SectionDateEnd,
                        ActualDurationHours = td.ActualDurationHours,
                        ActualDurationMinutes = td.ActualDurationMinutes,
                        ResponsibleID = td.ResponsibleID,
                        Comment = td.Comment
                    });

                    dataManager.TaskDuration.Add(taskDurationList.ToList(), taskId);
                    scope.Complete();
                }
            }
        }
    }
}