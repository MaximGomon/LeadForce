using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class Users : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Пользователи - LeadForce";

            var user = CurrentUser.Instance;

            gridUsers.AddNavigateUrl = UrlsData.AP_UserAdd();
            gridUsers.Actions.Add(new GridAction { Text = "Карточка пользователя", NavigateUrl = string.Format("~/{0}/Users/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            if ((AccessLevel)user.AccessLevelID != AccessLevel.SystemAdministrator)
            {
                gridUsers.SiteID = SiteId;
                gridUsers.Columns[1].Visible = false;
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridUsers_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;

                ((Literal)item.FindControl("litAccessLevel")).Text = EnumHelper.GetEnumDescription((AccessLevel)int.Parse(data["tbl_User_AccessLevelID"].ToString()));
            }
        }
    }
}