using System;
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

namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class SelectEventTime : System.Web.UI.UserControl
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

            if (taskDurations.Where(td => td.ID == Task.ID).SingleOrDefault() == null)
            {
                rScheduler.SelectedDate = Task.StartDate;
                taskDurations.Add(new TaskDurationMap()
                                      {
                                          ID = Task.ID,
                                          SectionDateStart = Task.StartDate,
                                          SectionDateEnd = Task.EndDate,
                                          Comment = Task.Title,
                                          IsTask = true
                                      });
            }

            rScheduler.DataSource = taskDurations;
            rScheduler.DataKeyField = "ID";
            rScheduler.DataSubjectField = "Comment";
            rScheduler.DataStartField = "SectionDateStart";
            rScheduler.DataEndField = "SectionDateEnd";
            rScheduler.DataBind();            
        }



        /// <summary>
        /// Handles the OnAppointmentUpdate event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentUpdateEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnAppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {
            if (e.ModifiedAppointment.Start >= Task.StartDate && e.ModifiedAppointment.End <= Task.EndDate)
            {
                var taskDuration = ((List<TaskDurationMap>)ViewState["TaskDurations"]).Where(tdm => tdm.ID == (Guid)e.ModifiedAppointment.ID).SingleOrDefault();
                if (taskDuration != null)
                {
                    taskDuration.Comment = e.ModifiedAppointment.Subject;
                    taskDuration.SectionDateStart = e.ModifiedAppointment.Start;
                    taskDuration.SectionDateEnd = e.ModifiedAppointment.End;
                    taskDuration.ActualDurationHours = (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Hours;
                    taskDuration.ActualDurationMinutes = (e.ModifiedAppointment.End - e.ModifiedAppointment.Start).Minutes;
                    if (!string.IsNullOrEmpty(e.ModifiedAppointment.Attributes["ResponsibleID"]))
                        taskDuration.ResponsibleID = Guid.Parse(e.ModifiedAppointment.Attributes["ResponsibleID"]);
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
            ((List<TaskDurationMap>) ViewState["TaskDurations"]).Remove(((List<TaskDurationMap>) ViewState["TaskDurations"]).Where(tdm => tdm.ID == (Guid) e.Appointment.ID).SingleOrDefault());
            BindData();            
        }



        /// <summary>
        /// Handles the OnAppointmentInsert event of the rScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentInsertEventArgs"/> instance containing the event data.</param>
        protected void rScheduler_OnAppointmentInsert(object sender, AppointmentInsertEventArgs e)
        {
            if (e.Appointment.Start >= Task.StartDate && e.Appointment.End <= Task.EndDate)
            {
                var taskDurationMap = new TaskDurationMap
                                          {
                                              ID = Guid.NewGuid(),
                                              SectionDateStart = e.Appointment.Start,
                                              SectionDateEnd = e.Appointment.End,
                                              ActualDurationHours = (e.Appointment.End - e.Appointment.Start).Hours,
                                              ActualDurationMinutes = (e.Appointment.End - e.Appointment.Start).Minutes,
                                              Comment = e.Appointment.Subject
                                          };
                if (!string.IsNullOrEmpty(e.Appointment.Attributes["ResponsibleID"]))
                    taskDurationMap.ResponsibleID = Guid.Parse(e.Appointment.Attributes["ResponsibleID"]);

                ((List<TaskDurationMap>) ViewState["TaskDurations"]).Add(taskDurationMap);                
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
                if (taskDuration.IsTask)
                {
                    e.Appointment.CssClass = "disable-delete";
                    e.Appointment.BackColor = Color.FromArgb(247, 251,209);
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
            var dataManager = new DataManager();

            if (ViewState["TaskDurations"] != null)
            {
                using (var scope = new TransactionScope())
                {
                    dataManager.TaskDuration.DeleteAll(taskId);

                    var taskDurationList =
                        ((List<TaskDurationMap>) ViewState["TaskDurations"]).Where(td => !td.IsTask).Select(
                            td => new tbl_TaskDuration()
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