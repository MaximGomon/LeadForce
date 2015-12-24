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

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SiteActionList : System.Web.UI.UserControl
    {        
        public Guid siteID = new Guid();
        private Guid _contactID;

        public bool ShowWidgets
        {            
            set { ucLeftColumn.Visible = value; }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            siteID = ((LeadForceBasePage)Page).SiteId;
            
            var contactId = Page.RouteData.Values["id"] as string;

            if (contactId != "null" && !Guid.TryParse(contactId, out _contactID))            
                Response.Redirect(UrlsData.AP_Contacts());

            gridSiteAction.SiteID = siteID;
            gridSiteAction.Where = new List<GridWhere>();

            var dataManager = new DataManager();
            var site = dataManager.Sites.SelectById(siteID);

            if (site != null)
            {
                if (!site.ShowHiddenMessages)
                    gridSiteAction.Where.Add(new GridWhere {Column = "IsHidden", Value = site.ShowHiddenMessages.ToString()});
            }

            if (_contactID != Guid.Empty)
            {
                //gridSiteAction.Where.Add(new GridWhere { Column = "ContactID", Value = _contactID.ToString() });
                gridSiteAction.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_SiteAction.ContactID = '{0}' OR tbl_SiteAction.SenderID = '{0}')", _contactID) });
                gridSiteAction.Columns[2].Visible = false;
            }

            gridSiteAction.Actions.Add(new GridAction { Text = "Карточка сообщения", NavigateUrl = string.Format("~/{0}/SiteAction/Message/{{0}}", ((LeadForceBasePage)Page).CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridSiteAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSiteAction_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;
                
                var imgDirection = (Image) item.FindControl("imgDirection");
                var lUserFullName = (Literal)item.FindControl("lUserFullName");
                var lSenderUserFullName = (Literal)item.FindControl("lSenderUserFullName");

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
                else if (!string.IsNullOrEmpty(data["tbl_Contact_ID"].ToString()))
                    lUserFullName.Text = string.Format("&lt;<a href=\"{0}\">Незнакомец</a> от {1}&gt;", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), DateTime.Parse(data["tbl_Contact_CreatedAt"].ToString()).ToString("dd.MM.yyyy HH:mm"));

                if (!string.IsNullOrEmpty(data["c1_UserFullName"].ToString()))
                    lSenderUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["c1_ID"].ToString())), data["c1_UserFullName"]);
                else if (!string.IsNullOrEmpty(data["c1_ID"].ToString()))
                    lSenderUserFullName.Text = string.Format("&lt;<a href=\"{0}\">Незнакомец</a> от {1}&gt;", UrlsData.AP_Contact(Guid.Parse(data["c1_ID"].ToString())), DateTime.Parse(data["c1_CreatedAt"].ToString()).ToString("dd.MM.yyyy HH:mm"));

                switch ((Direction)int.Parse(data["tbl_SiteAction_DirectionID"].ToString()))
                {
                    case Direction.In:
                        imgDirection.ImageUrl = "~/App_Themes/Default/images/icoInbox.png";
                        imgDirection.AlternateText = imgDirection.ToolTip = "Входящее";
                        break;
                    case Direction.Out:
                        imgDirection.ImageUrl = "~/App_Themes/Default/images/icoOutbox.png";
                        imgDirection.AlternateText = imgDirection.ToolTip = "Исходящее";
                        break;
                }

                var hlActionTemplateTitle = (HyperLink)item.FindControl("hlActionTemplateTitle");
                if (!string.IsNullOrEmpty(data["tbl_SiteActionTemplate_ID"].ToString()))
                {
                    hlActionTemplateTitle.Text = data["tbl_SiteActionTemplate_Title"].ToString();
                    hlActionTemplateTitle.NavigateUrl =
                        UrlsData.AP_SiteActionTemplate(Guid.Parse(data["tbl_SiteActionTemplate_ID"].ToString()));
                }
                else if (!string.IsNullOrEmpty(data["tbl_SourceMonitoring_ID"].ToString()))
                {
                    if (!string.IsNullOrEmpty(data["c2_UserFullName"].ToString()))
                        lUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["c2_ID"].ToString())), data["c2_UserFullName"]);

                    hlActionTemplateTitle.Text = data["tbl_SourceMonitoring_Name"].ToString();
                    hlActionTemplateTitle.NavigateUrl =
                        UrlsData.AP_SourceMonitoringEdit(Guid.Parse(data["tbl_SourceMonitoring_ID"].ToString()));
                }
            }
        }
    }
}