using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace WebCounter.AdminPanel.UserControls.Task.SchedulerAdvancedForm
{
    public partial class AdvancedForm : System.Web.UI.UserControl
    {
        #region Private members

        private bool FormInitialized
        {
            get
            {
                return (bool)(ViewState["FormInitialized"] ?? false);
            }

            set
            {
                ViewState["FormInitialized"] = value;
            }
        }

        private RadSchedulerAdvancedFormAdvancedFormMode mode = RadSchedulerAdvancedFormAdvancedFormMode.Insert;

        #endregion

        #region Protected properties

        protected RadScheduler Owner
        {
            get
            {
                return Appointment.Owner;
            }
        }

        protected Appointment Appointment
        {
            get
            {
                SchedulerFormContainer container = (SchedulerFormContainer)BindingContainer;
                return container.Appointment;
            }
        }

        #endregion

        #region Attributes and resources


        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public string ResponsibleID
        {
            get { return ucResponsible.SelectedValue.ToString(); }

            set
            {
                if (!string.IsNullOrEmpty(value))
                    ucResponsible.SelectedValue = Guid.Parse(value);
            }
        }        

        #endregion

        #region Public properties

        public RadSchedulerAdvancedFormAdvancedFormMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public bool IsResponsibleEnabled
        {
            set { AdvancedControlsPanel.Visible = false; }
        }

        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public string Subject
        {
            get
            {
                return SubjectText.Text;
            }

            set
            {
                SubjectText.Text = value;
            }
        }

        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public DateTime Start
        {
            get
            {
                DateTime result = StartDate.SelectedDate.Value.Date;

                if (AllDayEvent.Checked)
                {
                    result = result.Date;
                }
                else
                {
                    TimeSpan time = StartTime.SelectedDate.Value.TimeOfDay;
                    result = result.Add(time);
                }

                return Owner.DisplayToUtc(result);
            }

            set
            {
                StartDate.SelectedDate = Owner.UtcToDisplay(value);
                StartTime.SelectedDate = Owner.UtcToDisplay(value);
            }
        }

        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public DateTime End
        {
            get
            {
                DateTime result = EndDate.SelectedDate.Value.Date;

                if (AllDayEvent.Checked)
                {
                    result = result.Date.AddDays(1);
                }
                else
                {
                    TimeSpan time = EndTime.SelectedDate.Value.TimeOfDay;
                    result = result.Add(time);
                }

                return Owner.DisplayToUtc(result);
            }

            set
            {
                EndDate.SelectedDate = Owner.UtcToDisplay(value);
                EndTime.SelectedDate = Owner.UtcToDisplay(value);
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
            UpdateButton.ValidationGroup = Owner.ValidationGroup;
            UpdateButton.CommandName = Mode == RadSchedulerAdvancedFormAdvancedFormMode.Edit ? "Update" : "Insert";

            InitializeStrings();        
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!FormInitialized)
            {
                if (IsAllDayAppointment(Appointment))
                {
                    EndDate.SelectedDate = EndDate.SelectedDate.Value.AddDays(-1);
                }

                FormInitialized = true;
            }
        }



        /// <summary>
        /// Handles the DataBinding event of the BasicControlsPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BasicControlsPanel_DataBinding(object sender, EventArgs e)
        {
            AllDayEvent.Checked = IsAllDayAppointment(Appointment);
        }



        /// <summary>
        /// Handles the OnServerValidate event of the DurationValidator control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void DurationValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (End - Start) > TimeSpan.Zero;
        }

        #region Private methods

        /// <summary>
        /// Initializes the strings.
        /// </summary>
        private void InitializeStrings()
        {
            SubjectValidator.ErrorMessage = Owner.Localization.AdvancedSubjectRequired;
            SubjectValidator.ValidationGroup = Owner.ValidationGroup;

            AllDayEvent.Text = Owner.Localization.AdvancedAllDayEvent;

            StartDateValidator.ErrorMessage = Owner.Localization.AdvancedStartDateRequired;
            StartDateValidator.ValidationGroup = Owner.ValidationGroup;

            StartTimeValidator.ErrorMessage = Owner.Localization.AdvancedStartTimeRequired;
            StartTimeValidator.ValidationGroup = Owner.ValidationGroup;

            EndDateValidator.ErrorMessage = Owner.Localization.AdvancedEndDateRequired;
            EndDateValidator.ValidationGroup = Owner.ValidationGroup;

            EndTimeValidator.ErrorMessage = Owner.Localization.AdvancedEndTimeRequired;
            EndTimeValidator.ValidationGroup = Owner.ValidationGroup;

            DurationValidator.ErrorMessage = Owner.Localization.AdvancedStartTimeBeforeEndTime;
            DurationValidator.ValidationGroup = Owner.ValidationGroup;            

            SharedCalendar.FastNavigationSettings.OkButtonCaption = Owner.Localization.AdvancedCalendarOK;
            SharedCalendar.FastNavigationSettings.CancelButtonCaption = Owner.Localization.AdvancedCalendarCancel;
            SharedCalendar.FastNavigationSettings.TodayButtonCaption = Owner.Localization.AdvancedCalendarToday;
        }



        /// <summary>
        /// Determines whether [is all day appointment] [the specified appointment].
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        /// <returns>
        ///   <c>true</c> if [is all day appointment] [the specified appointment]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAllDayAppointment(Appointment appointment)
        {
            DateTime displayStart = Owner.UtcToDisplay(appointment.Start);
            DateTime displayEnd = Owner.UtcToDisplay(appointment.End);
            return displayStart.CompareTo(displayStart.Date) == 0 && displayEnd.CompareTo(displayEnd.Date) == 0;
        }

        #endregion
    }

    public enum RadSchedulerAdvancedFormAdvancedFormMode
    {
        Insert, Edit
    }
}