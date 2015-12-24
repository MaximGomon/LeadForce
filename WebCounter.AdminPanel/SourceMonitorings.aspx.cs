using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class SourceMonitorings : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Мониторинг внешних источников - LeadForce";

            gridSourceMonitorings.AddNavigateUrl = UrlsData.AP_SourceMonitoringAdd();
            gridSourceMonitorings.Actions.Add(new GridAction { Text = "Карточка мониторинга", NavigateUrl = string.Format("~/{0}/SourceMonitorings/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridSourceMonitorings.SiteID = SiteId;
        }



        /// <summary>
        /// Handles the ItemDataBound event of the gridSourceMonitorings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSourceMonitorings_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;
                ((Literal)item.FindControl("lrlSourceType")).Text = EnumHelper.GetEnumDescription((SourceType)int.Parse(data["tbl_SourceMonitoring_SourceTypeID"].ToString()));
                ((Literal)item.FindControl("lrlStatus")).Text = EnumHelper.GetEnumDescription((Status)int.Parse(data["tbl_SourceMonitoring_StatusID"].ToString()));
            }
        }
    }
}