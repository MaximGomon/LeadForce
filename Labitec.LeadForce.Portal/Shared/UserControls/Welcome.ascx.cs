using System;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class Welcome : System.Web.UI.UserControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && CurrentUser.Instance != null)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var dataManager = new DataManager();

            var siteAction = dataManager.SiteAction.SelectLastTaskNotification(CurrentUser.Instance.SiteID, (Guid) CurrentUser.Instance.ContactID);
            if (siteAction != null && siteAction.ObjectID.HasValue)
            {
                var task = dataManager.Task.SelectById(CurrentUser.Instance.SiteID, (Guid)siteAction.ObjectID);
                if (task != null)
                {
                    var contact = dataManager.Contact.SelectById(CurrentUser.Instance.SiteID, task.CreatorID);
                    lrlCreatorFullName.Text = contact.UserFullName;
                    var company = contact.tbl_Company1;
                    if (contact.tbl_Company1 != null)
                        lrlCreatorCompany1.Text = lrlCreatorCompany2.Text = company.Name;
                }
            }

            lrlMyEmail.Text = string.Format("<b>{0}</b>", CurrentUser.Instance.Login);
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnStartWork control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnStartWork_OnClick(object sender, EventArgs e)
        {
            var dataManager = new DataManager();
            var user = dataManager.User.SelectById(CurrentUser.Instance.ID, CurrentUser.Instance.SiteID);
            if (user != null)
            {
                user.Password = txtPassword.Text;
                dataManager.User.Update(user);
            }

            CurrentUser.UserInstanceFlush();

            Response.Redirect(UrlsData.LFP_Home(((LeadForcePortalBasePage) Page).PortalSettingsId));
        }
    }
}