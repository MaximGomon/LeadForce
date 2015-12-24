using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class ProductCategories : System.Web.UI.UserControl
    {

        private DataManager dataManager = new DataManager();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //radCategories.DataTextField = "Title";
            //radCategories.DataNavigateUrlField = "Url";
            //radCategories.DataFieldID = "Id";
            //radCategories.DataFieldParentID = "ParentId";
            
            //radCategories.DataSource = 
            //radCategories.DataBind();

            //var currentItem = radCategories.FindItemByUrl(Request.Url.PathAndQuery);
            //if (currentItem != null)
            //    currentItem.HighlightPath();


            var categories = dataManager.ProductCategory.SelectHierarchy(SiteID);


            radProductCategoryTreeView.DataSource = GetCategories();
            radProductCategoryTreeView.DataTextField = "Title";
            radProductCategoryTreeView.DataFieldID = "Id";
            radProductCategoryTreeView.DataValueField = "Id";
            radProductCategoryTreeView.DataFieldParentID = "ParentId";
            radProductCategoryTreeView.DataBind();
           
            if (CategoryID != Guid.Empty)
            {
                RadTreeNode rtn = radProductCategoryTreeView.FindNodeByValue(CategoryID.ToString());
                rtn.Expanded = true;
                rtn.Selected = true;
                rtn.ExpandParentNodes();
            }
        }



        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        private List<CategoryItem> GetCategories()
        {
            var categories = dataManager.ProductCategory.SelectAll(SiteID).OrderBy(pc => pc.Order);
            List<CategoryItem> ret = categories.Select(category => new CategoryItem()
            {
                Id = category.ID,
                ParentId = category.ParentID,
                Title = category.Title,
            }).ToList();
            ret.Insert(0, new CategoryItem()
            {
                Id = Guid.Empty,
                ParentId = null,
                Title = "Любая категория",
            });
            return ret;
        }


        /// <summary>
        /// Gets or sets the siteID.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public Guid SiteID
        {
            get
            {
                if (ViewState["SiteID"] == null)
                    return Guid.Empty;
                return (Guid)ViewState["SiteID"];
            }
            set { ViewState["SiteID"] = value; }
        }



        /// <summary>
        /// Gets or sets the siteID.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public Guid CategoryID
        {
            get
            {
                if (ViewState["CategoryID"] == null)
                    return Guid.Empty;
                return (Guid)ViewState["CategoryID"];
            }
            set { ViewState["CategoryID"] = value; }
        }



        protected void radProductCategoryTreeView_Click(object sender, RadTreeNodeEventArgs e)
        {
            if (Guid.Parse(e.Node.Value) != Guid.Empty)
            {
                Response.Redirect(UrlsData.AP_Products(Guid.Parse(e.Node.Value)));
            } else
            {
                Response.Redirect(UrlsData.AP_Products());
            }
        }
    }

    public class CategoryItem
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
    }
}