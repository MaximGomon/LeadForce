using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class WebSites : LeadForceBasePage
    {
        public Access access;


        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Мини сайты - LeadForce";
            access = Access.Check();
            rbAddWebSite.NavigateUrl = UrlsData.AP_WebSiteAdd();
            gridWebSites.SiteID = SiteId;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridWebSites control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridWebSites_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("lrlTitle")).Text = data["tbl_WebSite_Title"].ToString();
                ((Literal)item.FindControl("lrlDescription")).Text = data["tbl_WebSite_Description"].ToString();                
                ((HyperLink) item.FindControl("hlEdit")).Visible = access.Write;
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_WebSiteEdit(Guid.Parse(data["tbl_WebSite_ID"].ToString()));

                if (data["tbl_SiteDomain_Domain"] != DBNull.Value)
                {
                    var url = DataManager.SiteDomain.GetDomainUrl((string)data["tbl_SiteDomain_Domain"]);
                    var result = url != null ? url.ToString() : string.Empty;

                    ((Literal)item.FindControl("lrlUrl")).Text = result;
                }
                else
                {
                    ((Literal)item.FindControl("lrlUrl")).Text = BusinessLogicLayer.Configuration.Settings.MiniSiteUrl(Guid.Parse(data["tbl_WebSite_ID"].ToString()));
                }

                ((LinkButton)e.Item.FindControl("lbDelete")).CommandArgument = data["ID"].ToString();
                e.Item.FindControl("lbDelete").Visible = access.Delete;
            }
        }



        /// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            DataManager.WebSite.Delete(CurrentUser.Instance.SiteID, (Guid.Parse(e.CommandArgument.ToString())));
            gridWebSites.Rebind(); 
        }
    }
}