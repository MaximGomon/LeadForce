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
    public partial class KnowledgeBases : LeadForceBasePage
    {
        public Guid? CategoryId;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "База знаний - LeadForce";

            if (Page.RouteData.Values["categoryId"] != null)
                CategoryId = Guid.Parse(Page.RouteData.Values["categoryId"] as string);

            gridKnowledgeBase.AddNavigateUrl = UrlsData.AP_KnowledgeBaseAdd();
            gridKnowledgeBase.Actions.Add(new GridAction { Text = "Карточка публикации", NavigateUrl = string.Format("~/{0}/KnowledgeBase/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridKnowledgeBase.SiteID = SiteId;
            
            gridKnowledgeBase.Where = new List<GridWhere>();
            gridKnowledgeBase.Where.Add(new GridWhere { Column = "tbl_PublicationType.PublicationKindID", Value = ((int)PublicationKind.KnowledgeBase).ToString() });

            if (CategoryId.HasValue && CategoryId != Guid.Empty)
            {
                gridKnowledgeBase.Where.Add(new GridWhere { Column = "PublicationCategoryID", Value = CategoryId.ToString() });
            }
        }
    }
}