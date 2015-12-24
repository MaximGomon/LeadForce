using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Portal
{
    public partial class PortalSettings : System.Web.UI.UserControl
    {                
        /// <summary>
        /// Gets or sets the site id.
        /// </summary>
        /// <value>
        /// The site id.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteId
        {
            get
            {
                if (ViewState["SiteId"] == null)
                    ViewState["SiteId"] = CurrentUser.Instance.SiteID;

                return (Guid)ViewState["SiteId"];
            }
            set
            {
                ViewState["SiteId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Module
        {
            get
            {                
                return (string)ViewState["Module"];
            }
            set
            {
                ViewState["Module"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteId == Guid.Empty)
            {
                gridPortalSettings.Visible = false;
                return;
            }

            gridPortalSettings.SiteID = SiteId;
            gridPortalSettings.AddNavigateUrl = UrlsData.AP_PortalSettingsAdd(Module, SiteId);            
        }

        

        /// <summary>
        /// Handles the OnItemDataBound event of the gridPortalSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridPortalSettings_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;

                var portalUrl = BusinessLogicLayer.Configuration.Settings.LabitecLeadForcePortalUrl + "/" + data["ID"];
                ((HyperLink) item.FindControl("hlPortal")).Text = portalUrl;
                ((HyperLink) item.FindControl("hlPortal")).NavigateUrl = portalUrl;
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_PortalSettingsEdit(Module, Guid.Parse(data["ID"].ToString()));
            }
        }
    }
}