using System;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.ActivityRibbon;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Discussions : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Обсуждения - LeadForce";

            plActivityRibbon.Visible = CurrentUser.Instance.ContactID.HasValue;
            if (!Page.IsPostBack && plActivityRibbon.Visible)
            {
                var dataManager = new DataManager();
                plActivityRibbon.Visible = dataManager.PublicationType.SelectByPublicationKindID(CurrentUser.Instance.SiteID, (int)PublicationKind.Discussion).Count > 0;
            }

            if (!plActivityRibbon.Visible)
                ucNotificationMessage.Text = "Для корректной работы нужно добавить контакт для Вашего пользователя и заполнить справочник \"Типы публикаций\" с видом публикации \"Обсуждение\".";

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPublicationTypes, ucCreatePublication);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPublicationTypes, ucPublicationsRibbon);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucCreatePublication, ucPublicationsRibbon);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPublicationCategory, ucPublicationsRibbon);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPublicationCategory, ucCreatePublication);

            ucPublicationTypes.SelectedIndexChanged += ucPublicationTypes_SelectedIndexChanged;
            ucCreatePublication.PublicationAdded += ucCreatePublication_PublicationAdded;
            ucPublicationCategory.SelectedCategoryChanged += ucPublicationCategory_SelectedCategoryChanged;
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