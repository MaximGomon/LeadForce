using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using System.Data;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.ModuleEditionAction.Product
{
    public partial class Products : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        public Guid siteID = new Guid();
        protected RadAjaxManager radAjaxManager = null;
        public Access access;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            siteID = ((LeadForceBasePage)Page).SiteId;
            Page.Title = "Продукты - LeadForce";

            access = Access.Check();

//            gridSiteActivityRules.AddNavigateUrl = UrlsData.AP_SiteActivityRuleAdd(_ruleTypeId);

            //radAjaxManager = RadAjaxManager.GetCurrent(Page);
            //radAjaxManager.AjaxSettings.AddAjaxSetting(rauFile, lbAddFile);
            //radAjaxManager.AjaxSettings.AddAjaxSetting(lbAddFile, gridSiteActivityRules);
            //radAjaxManager.AjaxSettings.AddAjaxSetting(lbAddFile, rauFile,null,UpdatePanelRenderMode.Inline);


            gridProducts.SiteID = siteID;
            //gridProducts.Where = new List<GridWhere>();
            //gridProducts.Where.Add(new GridWhere { Column = "RuleTypeID", Value = ((int)RuleType.File).ToString() });
                   
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridSiteActivityRules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridProducts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((HyperLink)item.FindControl("spanName")).Text = data["tbl_Product_Title"].ToString();
                ((HyperLink)item.FindControl("spanName")).NavigateUrl = "#";
//                ((HyperLink)item.FindControl("spanName")).NavigateUrl = UrlsData.AP_ProductEdit(Guid.Parse(data["tbl_Product_ID"].ToString()));


                ((Literal)item.FindControl("lDescription")).Text = data["tbl_Product_Description"].ToString();
                ((Literal)item.FindControl("lSKU")).Text = data["tbl_Product_SKU"].ToString();
                ((Literal)item.FindControl("lPrice")).Text = (String.IsNullOrEmpty(data["tbl_Product_Price"].ToString())) ? "" : (decimal.Parse(data["tbl_Product_Price"].ToString())).ToString("F") + " рублей";
                
                ((ModuleEditionAction.Product.Product)item.FindControl("ucProduct")).ProductId = (Guid)data["tbl_Product_ID"];

                
                var lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                lbEdit.CommandArgument = data["ID"].ToString();
                lbEdit.Command += new CommandEventHandler(lbEdit_OnCommand);


                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["tbl_Product_ID"].ToString();
                lbDelete.Command += new CommandEventHandler(lbDelete_OnCommand);
                lbDelete.Visible = access.Delete;
            }
        }


        protected void lbEdit_OnCommand(object sender, CommandEventArgs e)
        {
            ((ModuleEditionAction.Product.Product)((LinkButton)sender).Parent.FindControl("ucProduct")).BindData();
            ((LinkButton) sender).Parent.FindControl("ucProduct").Visible = true;
//            Response.Redirect(UrlsData.AP_ProductEdit(Guid.Parse(e.CommandArgument.ToString())));

        }

        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            //_dataManager.SiteActivityRules.DeleteByID(siteID, Guid.Parse(e.CommandArgument.ToString()));
            gridProducts.Rebind();
        }

        protected void lbAddFile_OnClick(object sender, EventArgs e)
        {
            var productData = _dataManager.Product.Save(
              CurrentUser.Instance.SiteID,
               Guid.Empty,
              txtTitle.Text,
              txtSKU.Text,
              null,
              null,
              txtPrice.Text == "" ? null : (decimal?)(decimal.Parse(txtPrice.Text)),
              null,
              null,
              null,
              null,
              null,
              null,
              null,
              txtDescription.Text,
              CurrentUser.Instance.ID,
              null
              );
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtSKU.Text = "";
            txtTitle.Text = "";
            gridProducts.Rebind();
            //Response.Redirect(UrlsData.AP_ProductAdd());
        }

    }
}