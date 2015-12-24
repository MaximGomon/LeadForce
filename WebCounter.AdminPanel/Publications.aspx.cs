using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class Publications : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Публикации - LeadForce";

            gridPublications.AddNavigateUrl = UrlsData.AP_PublicationAdd();
            gridPublications.Actions.Add(new GridAction { Text = "Редактировать", NavigateUrl = string.Format("~/{0}/Publications/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridPublications.SiteID = SiteId;

            gridPublications.Where = new List<GridWhere>();
            gridPublications.Where.Add(new GridWhere
                                            {
                                                CustomQuery =
                                                    "tbl_PublicationType.PublicationKindID <>'" + ((int)PublicationKind.KnowledgeBase).ToString() + "'"
                                            });
        }
    }
}