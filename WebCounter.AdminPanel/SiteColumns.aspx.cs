using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class SiteColumns : LeadForceBasePage
    {        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Реквизиты посетителя - LeadForce";

            rbAddSiteColumn.NavigateUrl = UrlsData.AP_SiteColumnAdd();
            
            //gridSiteColumns.Actions.Add(new GridAction { Text = "Редактировать", NavigateUrl = string.Format("~/{0}/SiteColumns/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridSiteColumns.SiteID = SiteId;
            gridSiteColumns.Where = new List<GridWhere>();
            gridSiteColumns.Where.Add(new GridWhere { Column = "SiteActivityRuleID", Value = null });
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridSiteColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSiteColumns_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((HyperLink)item.FindControl("hlTitle")).Text = data["tbl_SiteColumns_Name"].ToString();
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = ((HyperLink)item.FindControl("hlTitle")).NavigateUrl = UrlsData.AP_SiteColumn(data["ID"].ToString().ToGuid());

                ((Literal)item.FindControl("lType")).Text = data["tbl_ColumnTypes_Title"].ToString();
                ((Literal)item.FindControl("lCategory")).Text = data["tbl_ColumnCategories_Title"].ToString();
                
                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();
                lbDelete.Command += lbDelete_OnCommand;
            }
        }



        /// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            DataManager.SiteColumns.DeleteById(SiteId, Guid.Parse(e.CommandArgument.ToString()));
            gridSiteColumns.Rebind();
        }
    }
}