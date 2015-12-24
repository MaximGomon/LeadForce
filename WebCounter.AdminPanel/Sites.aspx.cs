using System;
using Labitec.UI.BaseWorkspace;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using System.Collections.Generic;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Sites : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentUser.Instance.AccessLevelID != (int)AccessLevel.SystemAdministrator)
                Response.Redirect(UrlsData.AP_Home());

            Title = "Сайты - LeadForce";

            gridSites.AddNavigateUrl = UrlsData.AP_SiteAdd();
            gridSites.Actions.Add(new GridAction { Text = "Карточка сайта", NavigateUrl = string.Format("~/{0}/Sites/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
        }
    }
}