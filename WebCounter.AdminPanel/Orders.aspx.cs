using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Orders : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Заказы - LeadForce";

            gridOrders.AddNavigateUrl = UrlsData.AP_OrderAdd();
            gridOrders.Actions.Add(new GridAction { Text = "Карточка заказа", NavigateUrl = string.Format("~/{0}/Orders/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridOrders.SiteID = SiteId;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridOrders control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridOrders_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;

                var lrlCompanyName = (Literal) item.FindControl("lrlCompanyName");
                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");

                ((Literal)item.FindControl("lrlOrderStatus")).Text = EnumHelper.GetEnumDescription((OrderStatus)int.Parse(data["tbl_Order_OrderStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Company_Name"].ToString()))
                    lrlCompanyName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Company(Guid.Parse(data["tbl_Company_ID"].ToString())), data["tbl_Company_Name"]);

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
            }
        }
    }
}