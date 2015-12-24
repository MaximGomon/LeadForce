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
    public partial class Products : LeadForceBasePage
    {
        public Guid CategoryId;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Продукты - LeadForce";


            //var sCategoryId = Page.RouteData.Values["categoryId"] as string;
            //if (!string.IsNullOrEmpty(sCategoryId))
            //    CategoryId = Guid.Parse(sCategoryId);

            if (Page.RouteData.Values["categoryId"] != null)
                CategoryId = Guid.Parse(Page.RouteData.Values["categoryId"] as string);

            ((ProductCategories) RadPanelBar1.Items[2].FindControl("ucCategoryID")).SiteID = SiteId;
            ((ProductCategories)RadPanelBar1.Items[2].FindControl("ucCategoryID")).CategoryID = CategoryId;

            gridProducts.AddNavigateUrl = UrlsData.AP_ProductAdd();
            gridProducts.Actions.Add(new GridAction { Text = "Карточка продукта", NavigateUrl = string.Format("~/{0}/Products/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridProducts.SiteID = SiteId;

            if (CategoryId != Guid.Empty)
            {
                gridProducts.Where = new List<GridWhere>();
                gridProducts.Where.Add(new GridWhere { Column = "ProductCategoryID", Value = CategoryId.ToString() });
            }
        }

        protected void gridProducts_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;
                ((Literal)item.FindControl("lProductStatus")).Text = EnumHelper.GetEnumDescription((ProductStatus)int.Parse(data["tbl_Product_ProductStatusID"].ToString()));
                ((Literal)item.FindControl("lProductCategory")).Text =
                    DataManager.ProductCategory.GetCategoryFullNameById(
                        Guid.Parse(data["tbl_Product_ProductCategoryID"].ToString()));

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProcessModuleEdition(plPageContainer);
        }

    }
}