using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class PortalSetting : LeadForceBasePage
    {
        private Guid _portalSettingsId;
        private Guid? _siteId;
        protected RadAjaxManager RadAjaxManager;
        protected FileSystemProvider FileSystemProvider = new FileSystemProvider();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string CurrentLogo
        {
            get
            {
                return (string)Session["CurrentLogo"];
            }
            set
            {
                Session["CurrentLogo"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            Title = "Настройки портала - LeadForce";
                                    
            if (Page.RouteData.Values["id"] != null)
                _portalSettingsId = Guid.Parse(Page.RouteData.Values["id"] as string);

            if (Page.RouteData.Values["siteId"] != null)
                _siteId = Guid.Parse(Page.RouteData.Values["siteId"] as string);

            RadAjaxManager = RadAjaxManager.GetCurrent(Page);
            RadAjaxManager.AjaxSettings.AddAjaxSetting(RadAjaxManager, rbiLogo);
            rauLogo.Localization.Select = "Выбрать";
            rauLogo.Localization.Remove = "Удалить";
            rauLogo.Localization.Cancel = "Отмена";

            txtTwitterProfile.Attributes.Add("style", "display:none");
            txtVkontakteProfile.Attributes.Add("style", "display:none");
            txtFacebookProfile.Attributes.Add("style", "display:none");

            if (Request.Url.ToString().ToLower().Contains("sites"))
                hlCancel.NavigateUrl = UrlsData.AP_Sites();
            else
                hlCancel.NavigateUrl = UrlsData.AP_Settings();

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var portalSettings = CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator
                                     ? DataManager.PortalSettings.SelectById(_portalSettingsId)
                                     : DataManager.PortalSettings.SelectById(SiteId, _portalSettingsId);

            rbiLogo.ImageUrl = "~/App_Themes/Default/images/DefaultPortalLogo.png";
            ucHtmlEditor.Content = PortalTemplates.HeaderTemplate;

            rcpMainMenu.SelectedColor = ColorTranslator.FromHtml("#ffffff");
            rcpBlockTitleBackground.SelectedColor = ColorTranslator.FromHtml("#41aee8");

            if (portalSettings != null)
            {
                txtTitle.Text = portalSettings.Title;
                txtWelcomeMessage.Text = portalSettings.WelcomeMessage;
                txtDomain.Text = portalSettings.Domain;
                txtCompanyMessage.Text = portalSettings.CompanyMessage;
                if (!string.IsNullOrEmpty(portalSettings.HeaderTemplate))
                    ucHtmlEditor.Content = portalSettings.HeaderTemplate;
                if (!string.IsNullOrEmpty(portalSettings.Logo))
                {
                    rbiLogo.ImageUrl = FileSystemProvider.GetLink(SiteId, "Portal", portalSettings.Logo, FileType.Image);
                    CurrentLogo = portalSettings.Logo;
                }

                rcpMainMenu.SelectedColor = ColorTranslator.FromHtml(portalSettings.MainMenuBackground);
                rcpBlockTitleBackground.SelectedColor = ColorTranslator.FromHtml(portalSettings.BlockTitleBackground);

                if (!string.IsNullOrEmpty(portalSettings.FacebookProfile))
                {
                    chxFacebookProfile.Checked = true;
                    txtFacebookProfile.Attributes["style"] = "display:inline";
                    txtFacebookProfile.Text = portalSettings.FacebookProfile;
                }
                if (!string.IsNullOrEmpty(portalSettings.VkontakteProfile))
                {
                    chxVkontakteProfile.Checked = true;
                    txtVkontakteProfile.Attributes["style"] = "display:inline";
                    txtVkontakteProfile.Text = portalSettings.VkontakteProfile;
                }
                if (!string.IsNullOrEmpty(portalSettings.TwitterProfile))
                {
                    chxTwitterProfile.Checked = true;
                    txtTwitterProfile.Attributes["style"] = "display:inline";
                    txtTwitterProfile.Text = portalSettings.TwitterProfile;
                }
            }
        }



        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            ucNotificationMessage.Text = string.Empty;

            if (!string.IsNullOrEmpty(txtDomain.Text) && DataManager.PortalSettings.IsExistsDomain(_portalSettingsId, txtDomain.Text))
            {
                ucNotificationMessage.Text = "Портал с таким \"Доменом\" уже зарегистрирован. Пожалуйста введите другое значение.";
                return;
            }

            //var portalSettings = DataManager.PortalSettings.SelectById(SiteId, _portalSettingsId) ?? new tbl_PortalSettings();
            var portalSettings = (CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator
                                      ? DataManager.PortalSettings.SelectById(_portalSettingsId)
                                      : DataManager.PortalSettings.SelectById(SiteId, _portalSettingsId)) ??
                                 new tbl_PortalSettings();

            portalSettings.Title = txtTitle.Text;
            portalSettings.WelcomeMessage = txtWelcomeMessage.Text;
            portalSettings.Domain = txtDomain.Text.Trim();
            portalSettings.CompanyMessage = txtCompanyMessage.Text;
            portalSettings.Logo = CurrentLogo;
            portalSettings.HeaderTemplate = ucHtmlEditor.Content;
            portalSettings.MainMenuBackground = ColorTranslator.ToHtml(rcpMainMenu.SelectedColor);
            portalSettings.BlockTitleBackground = ColorTranslator.ToHtml(rcpBlockTitleBackground.SelectedColor);

            portalSettings.FacebookProfile = chxFacebookProfile.Checked ? txtFacebookProfile.Text : null;
            portalSettings.VkontakteProfile = chxVkontakteProfile.Checked ? txtVkontakteProfile.Text : null;
            portalSettings.TwitterProfile = chxTwitterProfile.Checked ? txtTwitterProfile.Text : null;

            if (portalSettings.ID == Guid.Empty)
            {
                portalSettings.SiteID = _siteId.HasValue && CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator ? _siteId.Value : SiteId;

                DataManager.PortalSettings.Add(portalSettings);
            }
            else
            {
                DataManager.PortalSettings.Update(portalSettings);
            }

            CurrentLogo = null;

            Response.Redirect(hlCancel.NavigateUrl);
        }



        /// <summary>
        /// Handles the OnFileUploaded event of the rauLogo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.FileUploadedEventArgs"/> instance containing the event data.</param>
        protected void rauLogo_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {            
            var fileName = FileSystemProvider.Upload(SiteId, "Portal", e.File.FileName, e.File.InputStream, FileType.Image);

            CurrentLogo = fileName;

            rbiLogo.ImageUrl = FileSystemProvider.GetLink(SiteId, "Portal", fileName, FileType.Image);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            Save();
        }
    }
}