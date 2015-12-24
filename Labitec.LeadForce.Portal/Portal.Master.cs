using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.LeadForce.Portal.Shared.UserControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Mapping;

namespace Labitec.LeadForce.Portal
{
    public partial class Portal : System.Web.UI.MasterPage
    {
        protected PortalSettingsMap PortalSettings = new PortalSettingsMap();

        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(FindControl("ucLoggedAs", Page.Controls), FindControl("ucMainMenu", Page.Controls), null, UpdatePanelRenderMode.Inline);

            PortalSettings = ((LeadForcePortalBasePage)Page).PortalSettings;

            Page.Header.DataBind();

            ((Literal) FindControl("lrlHeader", Page.Controls)).Text = ((LeadForcePortalBasePage) Page).PortalSettings.HeaderTemplate;            

            if (CurrentUser.Instance != null)
            {
                if (string.IsNullOrEmpty(CurrentUser.Instance.Password) && !Page.GetType().Name.ToLower().Contains("default"))
                    Response.Redirect(UrlsData.LFP_Home(((LeadForcePortalBasePage) Page).PortalSettingsId));

                LabitecPage.UserID = CurrentUser.Instance.ID;
                LabitecPage.SiteID = CurrentUser.Instance.SiteID;
            }            
        }



        /// <summary>
        /// Handles the OnUserAuthorized event of the ucLoggedAs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ucLoggedAs_OnUserAuthorized(object sender, LoggedAs.UserAuthorizedEventArgs e)
        {
            ((MainMenu) FindControl("ucMainMenu", Page.Controls)).RefreshMenu(e.UserId);
        }




        /// <summary>
        /// Handles the OnLogoutClick event of the MainMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void MainMenu_OnLogoutClick(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Remove("siteID");
            CurrentUser.UserInstanceFlush();
            FormsAuthentication.SignOut();
            Response.Redirect(UrlsData.LFP_Home(((LeadForcePortalBasePage) Page).PortalSettingsId));
        }



        /// <summary>
        /// Finds the control.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="col">The col.</param>
        /// <returns></returns>
        public static Control FindControl(string id, ControlCollection col)
        {
            foreach (Control c in col)
            {
                Control child = FindControlRecursive(c, id);
                if (child != null)
                    return child;
            }
            return null;
        }



        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control rc = FindControlRecursive(c, id);
                if (rc != null)
                    return rc;
            }
            return null;
        }        
    }
}