using System;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class SiteActionMessage : LeadForceBasePage
    {
        private Guid _siteActionId;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Сообщение - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                _siteActionId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_SiteAction();

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var siteAction = DataManager.SiteAction.SelectById(SiteId, _siteActionId);

            if (siteAction != null)
            {
                lrlMessageTitle.Text = siteAction.MessageTitle;
                lrlMessageText.Text = siteAction.MessageText;
            }
        }
    }
}