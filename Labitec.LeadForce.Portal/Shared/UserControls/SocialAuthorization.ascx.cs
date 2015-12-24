using System;
using System.ComponentModel;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class SocialAuthorization : System.Web.UI.UserControl
    {
        protected string VkontakteAPIOAuthUrl = string.Empty;
        protected string FacebookAPIOAuthUrl = string.Empty;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteId
        {
            get
            {
                if (ViewState["SiteId"] == null)
                    ViewState["SiteId"] = Guid.Empty;

                return (Guid)ViewState["SiteId"];
            }
            set { ViewState["SiteId"] = value; }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page is LeadForcePortalBasePage)
            {
                VkontakteAPIOAuthUrl = Settings.Vkontakte.OAuthFullUrl(((LeadForcePortalBasePage) Page).PortalSettingsId, Request.Url.Host);
                FacebookAPIOAuthUrl = Settings.Facebook.OAuthFullUrl(((LeadForcePortalBasePage) Page).PortalSettingsId,Request.Url.Host);
            }
            else
            {
                var dataManager = new DataManager();
                var portalSettings = dataManager.PortalSettings.SelectBySiteId(SiteId);
                if (portalSettings != null)
                {
                    VkontakteAPIOAuthUrl = Settings.Vkontakte.OAuthFullUrl(portalSettings.ID, Request.Url.Host);
                    FacebookAPIOAuthUrl = Settings.Facebook.OAuthFullUrl(portalSettings.ID, Request.Url.Host);
                }
                else
                    this.Visible = false;
            }
        }
    }
}