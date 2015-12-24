using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class PublicationCategories : System.Web.UI.UserControl
    {
        public Guid? CategoryId;
        private readonly DataManager _dataManager = new DataManager();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["categoryId"] != null)
                CategoryId = Guid.Parse(Page.RouteData.Values["categoryId"] as string);

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            rtvPublicationCategory.DataSource = GetCategories();
            rtvPublicationCategory.DataTextField = "Title";
            rtvPublicationCategory.DataFieldID = "Id";
            rtvPublicationCategory.DataValueField = "Id";
            rtvPublicationCategory.DataFieldParentID = "ParentId";
            rtvPublicationCategory.DataBind();

            if (CategoryId != null && CategoryId != Guid.Empty)
            {
                var node = rtvPublicationCategory.FindNodeByValue(CategoryId.ToString());
                if (node != null)
                {
                    node.Selected = true;
                    node.ExpandParentNodes();
                }
            }
        }



        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        private List<CategoryItem> GetCategories()
        {
            var categories = _dataManager.PublicationCategory.SelectAll(((LeadForceBasePage)Page).SiteId).OrderBy(pc => pc.Order);
            var result = categories.Select(category => new CategoryItem()
            {
                Id = category.ID,
                ParentId = category.ParentID,
                Title = category.Title,
            }).ToList();

            result.Insert(0, new CategoryItem()
            {
                Id = Guid.Empty,
                ParentId = null,
                Title = "Любая категория",
            });

            return result;
        }



        /// <summary>
        /// Handles the OnNodeClick event of the rtvPublicationCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeEventArgs"/> instance containing the event data.</param>
        protected void rtvPublicationCategory_OnNodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (Guid.Parse(e.Node.Value) != Guid.Empty)
                Response.Redirect(UrlsData.AP_KnowledgeBase(Guid.Parse(e.Node.Value)));
            else
                Response.Redirect(UrlsData.AP_KnowledgeBase());
        }
    }
}