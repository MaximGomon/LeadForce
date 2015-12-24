using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;

namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class SocialSubscription : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? PortalSettingsId
        {
            get { return (Guid?) ViewState["PortalSettingsId"]; }
            set { ViewState["PortalSettingsId"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteId
        {
            get { return (Guid)ViewState["SiteId"]; }
            set { ViewState["SiteId"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PortalSettingsId.HasValue)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var dataManager = new DataManager();
            var portalSettings = dataManager.PortalSettings.SelectById(SiteId, (Guid)PortalSettingsId);
            if (portalSettings != null)
            {
                if (!string.IsNullOrEmpty(portalSettings.FacebookProfile))
                {
                    plSocialSubscription.Visible = true;
                    var subscribeBlock =
                        string.Format(
                            "<iframe scrolling='no' frameborder='0' allowtransparency='true' style='border:none; overflow:hidden; width:170px; height:195px;' src='http://www.facebook.com/plugins/likebox.php?href={0}&width=202&colorscheme=light&show_faces=true&stream=false&header=false&height=195'></iframe><br/>",
                            HttpUtility.UrlEncode(portalSettings.FacebookProfile));
                    lrlFacebookProfile.Text = subscribeBlock;
                }
                if (!string.IsNullOrEmpty(portalSettings.VkontakteProfile))
                {
                    plSocialSubscription.Visible = true;
                    var urlSplitted = portalSettings.VkontakteProfile.Split('/');
                    var profile = Regex.Replace(urlSplitted[urlSplitted.Length - 1], "^public", string.Empty);
                    profile = Regex.Replace(profile, "^id", string.Empty);

                    var subscribeBlock = string.Format(@"
<script type=""text/javascript"" src=""http://userapi.com/js/api/openapi.js?52""></script>
<!-- VK Widget -->
<!-- VK Widget -->
<div id=""vk_groups""></div>
<script type=""text/javascript"">
VK.Widgets.Group(""vk_groups"", {{mode: 0, width: ""170"", height: ""290""}}, {0});</script><br/>", profile);

                    lrlVkontakteProfile.Text = subscribeBlock;
                }
                if (!string.IsNullOrEmpty(portalSettings.TwitterProfile))
                {
                    plSocialSubscription.Visible = true;
                    var urlSplitted = portalSettings.TwitterProfile.Split('/');
                    var profile = urlSplitted[urlSplitted.Length - 1];
                    var subscribeBlock = string.Format(
                    @"<a href=""https://twitter.com/{0}"" class=""twitter-follow-button"" data-show-count=""false"" data-lang=""en"">Follow @{0}</a>
<script>!function(d,s,id){{var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){{js=d.createElement(s);js.id=id;js.src=""//platform.twitter.com/widgets.js"";fjs.parentNode.insertBefore(js,fjs);}}}}(document,""script"",""twitter-wjs"");</script><br/><br/>", profile);
                    lrlTwitterProfile.Text = subscribeBlock;
                }
            }
        }
    }
}