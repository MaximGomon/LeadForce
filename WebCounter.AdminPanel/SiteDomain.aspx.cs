using System;
using System.Web.UI;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class SiteDomain : LeadForceBasePage
    {
        private Guid _siteDomainId;
        private Guid? _siteId;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Настройки домена - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                _siteDomainId = Guid.Parse(Page.RouteData.Values["id"] as string);

            if (Page.RouteData.Values["siteId"] != null)
                _siteId = Guid.Parse(Page.RouteData.Values["siteId"] as string);

            if (Request.Url.ToString().ToLower().Contains("sites"))
                hlCancel.NavigateUrl = UrlsData.AP_Sites();
            else
                hlCancel.NavigateUrl = UrlsData.AP_Settings();

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnSave, ucNotificationMessage);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnSave, lrlSiteDomainStatus, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucCheckSite, lrlSiteDomainStatus);

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var siteDomain = CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator
                                 ? DataManager.SiteDomain.SelectById(_siteDomainId)
                                 : DataManager.SiteDomain.SelectById(CurrentUser.Instance.SiteID, _siteDomainId);

            if (siteDomain != null)
            {
                txtDomain.Text = siteDomain.Domain;
                txtAliases.Text = siteDomain.Aliases;
                lrlSiteDomainStatus.Text = EnumHelper.GetEnumDescription((SiteDomainStatus)siteDomain.StatusID);

                if (Session["RunCheck"] != null)
                {
                    ucCheckSite.SiteDomainId = siteDomain.ID;
                    ucCheckSite.IsCheckOnLoad = true;
                    Session["RunCheck"] = null;
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            ucNotificationMessage.Text = string.Empty;

            var siteDomainId = _siteDomainId;

            if (siteDomainId != Guid.Empty)
                _siteId = CurrentUser.Instance.SiteID;

            var result = DataManager.SiteDomain.Save(_siteId, txtDomain.Text, txtAliases.Text, false, ref siteDomainId);

            if (!string.IsNullOrEmpty(result))
            {
                ucNotificationMessage.Text = result;
                return;
            }

            var siteDomain = DataManager.SiteDomain.SelectById(siteDomainId);
            lrlSiteDomainStatus.Text = EnumHelper.GetEnumDescription((SiteDomainStatus) siteDomain.StatusID);
            ucCheckSite.SiteDomainId = siteDomainId;
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "CheckDomains", string.Format("{0}_CheckDomains();",ucCheckSite.ClientID), true);
        }
    }
}