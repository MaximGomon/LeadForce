using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace Labitec.LeadForce.Portal.Main.TaskModule.UserControls
{
    public partial class Reminder : System.Web.UI.UserControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }




        /// <summary>
        /// Binds the data.
        /// </summary>
        public int BindData()
        {
            var count = 0;

            if (CurrentUser.Instance.ContactID.HasValue)
            {
                rcbPosponePeriods.SelectedIndex = 0;
                var dataManager = new DataManager();
                var reminderList = dataManager.Reminder.SelectAll((Guid) CurrentUser.Instance.ContactID, DateTime.Now).ToList();
                count = reminderList.Count;

                rlbRemindersList.Items.Clear();

                foreach (var reminder in reminderList)
                {
                    var item = new RadListBoxItem();
                    item.Text = reminder.Title;
                    item.Value = reminder.ID.ToString();
                    item.Attributes.Add("ReminderDate", reminder.ReminderDate.ToString("yyyy-MM-dd HH:mm"));
                    item.Attributes.Add("ModuleTitle", reminder.ModuleTitle);

                    rlbRemindersList.Items.Add(item);
                }

                rlbRemindersList.DataBind();
            }

            return count;
        }



        /// <summary>
        /// Deletes the reminders.
        /// </summary>
        /// <param name="items">The items.</param>
        protected void DeleteReminders(List<RadListBoxItem> items)
        {
            var toDelete = items.Select(listBoxItem => Guid.Parse(listBoxItem.Value)).ToList();
            if (toDelete.Count > 0 && CurrentUser.Instance.ContactID.HasValue)
            {
                var dataManager = new DataManager();
                dataManager.Reminder.Delete((Guid)CurrentUser.Instance.ContactID, toDelete);
            }
        }



        /// <summary>
        /// Updates the reminders.
        /// </summary>
        /// <param name="items">The items.</param>
        protected void UpdateReminders(List<RadListBoxItem> items)
        {
            if (!CurrentUser.Instance.ContactID.HasValue)
                return;

            var minutes = int.Parse(rcbPosponePeriods.SelectedValue);
            var dataManager = new DataManager();
            foreach (RadListBoxItem listBoxItem in items)
            {
                var reminder = dataManager.Reminder.SelectById((Guid) CurrentUser.Instance.ContactID, Guid.Parse(listBoxItem.Value));
                if (reminder != null)
                {
                    reminder.ReminderDate = minutes == 0 ? DateTime.Now.AddMonths(1) : DateTime.Now.AddMinutes(minutes);
                    dataManager.Reminder.Update(reminder);
                }
            }            
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnCancel_OnClick(object sender, EventArgs e)
        {
            DeleteReminders(rlbRemindersList.SelectedItems.ToList());            
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnCancelAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnCancelAll_OnClick(object sender, EventArgs e)
        {            
            DeleteReminders(rlbRemindersList.Items.ToList());
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnPostpone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnPostpone_OnClick(object sender, EventArgs e)
        {
            UpdateReminders(rlbRemindersList.SelectedItems.ToList());
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnPostponeAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnPostponeAll_OnClick(object sender, EventArgs e)
        {
            UpdateReminders(rlbRemindersList.Items.ToList());
        }
    }
}