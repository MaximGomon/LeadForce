using System;
using System.Collections.Generic;
using Labitec.UI.BaseWorkspace;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class SiteEventTemplates : LeadForceBasePage
    {        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Шаблоны событий - LeadForce";

            gridSiteEventTemplates.AddNavigateUrl = UrlsData.AP_SiteEventTemplateAdd();
            gridSiteEventTemplates.Actions.Add(new GridAction { Text = "Редактировать", NavigateUrl = string.Format("~/{0}/SiteEventTemplates/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridSiteEventTemplates.SiteID = SiteId;
        }
    }
}