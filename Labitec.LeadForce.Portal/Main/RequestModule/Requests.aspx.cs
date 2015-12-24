using System;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.Request;

namespace Labitec.LeadForce.Portal.Main.RequestModule
{
    public partial class Requests : LeadForcePortalBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(All, gridRequests);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(New, gridRequests);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(Open, gridRequests);

            var user = DataManager.User.SelectById(CurrentUser.Instance.ID);
            var accessCheck = Access.Check(user, "Requests");
            if (!accessCheck.Read)
                Response.Redirect(UrlsData.LFP_AccessDenied(PortalSettingsId));

            Title = "Запросы";

            gridRequests.ModuleName = "Requests";

            gridRequests.AddNavigateUrl = UrlsData.LFP_RequestAdd(PortalSettingsId);
            gridRequests.Actions.Add(new GridAction { Text = "Карточка запроса", NavigateUrl = "~/"+ PortalSettingsId +"/Main/Requests/Edit/{0}", ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridRequests.SiteID = SiteId;

            if (!Page.IsPostBack)
            {
                gridRequests.Where.Add(new GridWhere() { CustomQuery = string.Format("RequestStatusID <> {0}", (int) RequestStatus.Closed) });
                AddSystemWhere();
            }
        }



        /// <summary>
        /// Adds the system where.
        /// </summary>
        private void AddSystemWhere()
        {            
            if (CurrentUser.Instance.CompanyID.HasValue)
            {
                gridRequests.Where.Add(new GridWhere()
                {
                    CustomQuery =
                        string.Format(
                            "(tbl_Request.CompanyID = '{0}' OR ResponsibleID = '{1}' OR ContactID = '{1}' OR tbl_Request.CompanyID IN (SELECT tbl_ServiceLevelClient.ClientID FROM tbl_ServiceLevelContact LEFT JOIN tbl_ServiceLevelClient ON tbl_ServiceLevelContact.ServiceLevelClientID = tbl_ServiceLevelClient.ID WHERE ContactID = '{1}'))",
                            CurrentUser.Instance.CompanyID.Value,
                            CurrentUser.Instance.ContactID.Value)
                });
            }
            else
            {
                gridRequests.Where.Add(new GridWhere()
                {
                    CustomQuery =
                        string.Format(
                            "(ResponsibleID = '{0}' OR ContactID = '{0}' OR tbl_Request.CompanyID IN (SELECT tbl_ServiceLevelClient.ClientID FROM tbl_ServiceLevelContact LEFT JOIN tbl_ServiceLevelClient ON tbl_ServiceLevelContact.ServiceLevelClientID = tbl_ServiceLevelClient.ID WHERE ContactID = '{0}'))",
                            CurrentUser.Instance.ContactID.Value)
                });
            }            
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridRequests control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridRequests_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("lrlRequestStatus")).Text = EnumHelper.GetEnumDescription((RequestStatus)int.Parse(data["tbl_Request_RequestStatusID"].ToString()));
            }
        }



        /// <summary>
        /// Handles the OnCheckedChanged event of the filters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void filters_OnCheckedChanged(object sender, EventArgs e)
        {
            gridRequests.Where.Clear();
            var radioButton = (RadioButton) sender;            
            switch (radioButton.ID)
            {                
                case "Open":
                    gridRequests.Where.Add(new GridWhere() { CustomQuery = string.Format("RequestStatusID <> {0}", (int)RequestStatus.Closed) });
                    break;
                case "New":
                    gridRequests.Where.Add(new GridWhere() { CustomQuery = string.Format("RequestStatusID = {0}", (int)RequestStatus.New) });
                    break;
            }

            AddSystemWhere();

            gridRequests.Rebind();
        }
    }
}