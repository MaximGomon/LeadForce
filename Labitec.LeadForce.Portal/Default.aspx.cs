using System;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace Labitec.LeadForce.Portal
{
    public partial class Default : LeadForcePortalBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Сообщество " + PortalSettings.Title;

            if (CurrentUser.Instance != null)
            {
                if (CurrentUser.Instance.Password == string.Empty)
                    ucWelcome.Visible = true;
                else                
                    ShowDiscussions();                
            }
            else            
                ShowDiscussions();

            ucSocialSubscription.PortalSettingsId = PortalSettingsId;
            ucSocialSubscription.SiteId = SiteId;
        }



        /// <summary>
        /// Shows the discussions.
        /// </summary>
        private void ShowDiscussions()
        {            
            if (!Page.IsPostBack)
            {
                plDiscussions.Visible = true;        
                ucPublicationsRibbon.BindData();
                //if (CurrentUser.Instance == null)
                //    ucComingEvents.Visible = true;
            }
        
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucCreatePublication, ucPublicationsRibbon);
            ucCreatePublication.PublicationAdded += ucCreatePublication_PublicationAdded;
        }



        /// <summary>
        /// Ucs the create publication_ publication added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void ucCreatePublication_PublicationAdded(object sender)
        {
            ucPublicationsRibbon.BindData();
        }
    }
}