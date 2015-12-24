using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Site : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (CurrentUser.Instance.AccessLevelID == (int)AccessLevel.Portal)
                Response.Redirect(BusinessLogicLayer.Configuration.Settings.LabitecLeadForcePortalUrl);

            Page.Header.DataBind();
            LabitecPage.UserID = CurrentUser.Instance.ID;
            LabitecPage.SiteID = CurrentUser.Instance.SiteID;         
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ((Labitec.UI.BaseWorkspace.Menu)FindControl("MainMenu", Page.Controls)).LoggedAs = CurrentUser.Instance.Login + "<span class=\"help-question\"> | <a href=\"javascript:openHelpWindow('9ad82292-465b-4697-b3b3-8bb66e7ff04b','','');\">?</a></span>";            

            /*if (CurrentUser.Instance.AccessLevelID == (int)AccessLevel.SystemAdministrator)
                ((MenuTab)LabitecPage.FindControl("MainMenu").FindControl("MenuTab5")).Items.Add(new MenuItem() { Text = "Сайты", Value = "Sites", NavigateUrl = "~/Settings/Sites", ImageUrl = "~/App_Themes/Default/images/menu-icons/iconSites.png" });*/
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
            Response.Redirect(UrlsData.AP_Home());
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




        /// <summary>
        /// Hides the inaccessible tabs.
        /// </summary>
        /// <param name="tabs">The tabs.</param>
        /// <param name="multipage">The multipage.</param>
        public void HideInaccessibleTabs(ref RadTabStrip tabs, ref RadMultiPage multipage)
        {
            var dataManager = new DataManager();
            var user = dataManager.User.SelectById(CurrentUser.Instance.ID);

            for (int i = 0; i < tabs.Tabs.Count; i++)
            {
                if (!string.IsNullOrEmpty(tabs.Tabs[i].Value))
                {
                    var access = Access.Check(user, tabs.Tabs[i].Value);
                    if (!access.Read)
                    {
                        tabs.Tabs[i].Visible = false;
                        multipage.PageViews[i].Visible = false;
                    }
                }
            }
        }


        /// <summary>
        /// Reminders the on callback update.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadNotificationEventArgs"/> instance containing the event data.</param>
        protected void ReminderOnCallbackUpdate(object sender, RadNotificationEventArgs e)
        {
            //rnReminder.Value = ucReminder.BindData().ToString();
        }        
    }
}