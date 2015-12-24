using System;
using Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.Portal.Main.Discussions
{
    public partial class ActivityRibbon : LeadForcePortalBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Обсуждения - Лента Активности";

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPublicationTypes, ucCreatePublication);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPublicationTypes, ucPublicationsRibbon);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucCreatePublication, ucPublicationsRibbon);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPublicationCategory, ucPublicationsRibbon);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPublicationCategory, ucCreatePublication);

            ucPublicationTypes.SelectedIndexChanged += ucPublicationTypes_SelectedIndexChanged;
            ucCreatePublication.PublicationAdded += ucCreatePublication_PublicationAdded;
            ucPublicationCategory.SelectedCategoryChanged += ucPublicationCategory_SelectedCategoryChanged;

            if (Session["Portal-Discussion-CategoryId"] != null && Session["Portal-Discussion-PublicationTypeId"] != null)
            {
                ucPublicationCategory.SelectedCategory = (Guid)Session["Portal-Discussion-CategoryId"];                
                ucPublicationTypes.SelectedValue = (Guid)Session["Portal-Discussion-PublicationTypeId"];

                Session["Portal-Discussion-CategoryId"] = null;
                Session["Portal-Discussion-PublicationTypeId"] = null;
            }
        }




        /// <summary>
        /// Handles the SelectedCategoryChanged event of the ucPublicationCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.ActivityRibbon.PublicationCategory.SelectedCategoryChangedEventArgs"/> instance containing the event data.</param>
        protected void ucPublicationCategory_SelectedCategoryChanged(object sender, PublicationCategory.SelectedCategoryChangedEventArgs e)
        {
            ucCreatePublication.PublicationCategoryId = e.SelectedCategory;
            ucPublicationsRibbon.PublicationCategoryId = e.SelectedCategory;
            ucPublicationsRibbon.BindData();
        }



        /// <summary>
        /// Ucs the create publication_ publication added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void ucCreatePublication_PublicationAdded(object sender)
        {
            ucPublicationsRibbon.BindData();
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ucPublicationTypes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.ActivityRibbon.PublicationTypes.SelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucPublicationTypes_SelectedIndexChanged(object sender, PublicationTypes.SelectedIndexChangedEventArgs e)
        {            
            ucCreatePublication.SelectedValue = e.SelectedValue;
            ucPublicationsRibbon.PublicationTypeId = e.SelectedValue;
            ucPublicationsRibbon.BindData();
        }
    }
}