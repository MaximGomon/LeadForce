using System;
using Labitec.LeadForce.Portal.Shared.UserControls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace Labitec.LeadForce.Portal.Handlers
{
    public partial class ActivateUser : LeadForcePortalBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Активация пользователя";

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            try
            {
                var user = DataManager.User.SelectById(ObjectId, SiteId);
                if (user != null)
                {
                    user.IsActive = true;
                    DataManager.User.Update(user);
                    ucNotificationMessage.MessageType = NotificationMessageType.Success;
                    ucNotificationMessage.Text = "Вы успешно активировали вашу учетную запись.";
                }
                else
                {
                    ucNotificationMessage.MessageType = NotificationMessageType.Warning;
                    ucNotificationMessage.Text = "Указан не верный код активации.";
                }
            }
            catch (Exception ex)
            {
                Log.Error("Ошибка активации пользователя, id = " + ObjectId, ex);
                ucNotificationMessage.MessageType = NotificationMessageType.Warning;
                ucNotificationMessage.Text = "Ошибка активации пользователя. Попробуйте позже.";
            }            
        }
    }
}