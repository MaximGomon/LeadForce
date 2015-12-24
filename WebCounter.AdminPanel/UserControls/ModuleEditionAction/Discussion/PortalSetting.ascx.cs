using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.ModuleEditionAction.Discussion
{
    public partial class PortalSetting : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();
        private Guid _portalSettingsId;
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
            RadAjaxManager = RadAjaxManager.GetCurrent(Page);
            RadAjaxManager.AjaxSettings.AddAjaxSetting(RadAjaxManager, rbiLogo);
            rauLogo.Localization.Select = "Выбрать";
            rauLogo.Localization.Remove = "Удалить";
            rauLogo.Localization.Cancel = "Отмена";

            txtTwitterProfile.Attributes.Add("style", "display:none");
            txtVkontakteProfile.Attributes.Add("style", "display:none");
            txtFacebookProfile.Attributes.Add("style", "display:none");

            hlCancel.NavigateUrl = Request.Url.ToString();

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var portalSettings = DataManager.PortalSettings.SelectBySiteId(CurrentUser.Instance.SiteID);
            rbiLogo.ImageUrl = "~/App_Themes/Default/images/DefaultPortalLogo.png";
            ucHtmlEditor.Content = PortalTemplates.HeaderTemplate;

            rcpMainMenu.SelectedColor = ColorTranslator.FromHtml("#ffffff");
            rcpBlockTitleBackground.SelectedColor = ColorTranslator.FromHtml("#41aee8");            

            if (portalSettings != null)
            {                
                plPortalLink.Visible = true;
                var portalUrl = BusinessLogicLayer.Configuration.Settings.LabitecLeadForcePortalUrl + "/" + portalSettings.ID;
                hlPortalLink.Text = portalUrl;
                hlPortalLink.NavigateUrl = portalUrl;                

                txtTitle.Text = portalSettings.Title;
                txtWelcomeMessage.Text = portalSettings.WelcomeMessage;
                txtDomain.Text = portalSettings.Domain;
                txtCompanyMessage.Text = portalSettings.CompanyMessage;
                if (!string.IsNullOrEmpty(portalSettings.HeaderTemplate))
                    ucHtmlEditor.Content = portalSettings.HeaderTemplate;
                if (!string.IsNullOrEmpty(portalSettings.Logo))
                {
                    rbiLogo.ImageUrl = FileSystemProvider.GetLink(CurrentUser.Instance.SiteID, "Portal", portalSettings.Logo, FileType.Image);
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

            var portalSettings = DataManager.PortalSettings.SelectBySiteId(CurrentUser.Instance.SiteID) ?? new tbl_PortalSettings();

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
                portalSettings.SiteID = CurrentUser.Instance.SiteID;
                DataManager.PortalSettings.Add(portalSettings);
            }
            else
            {
                DataManager.PortalSettings.Update(portalSettings);
            }

            CurrentLogo = null;

            Response.Redirect(Request.Url.ToString());
        }



        /// <summary>
        /// Handles the OnFileUploaded event of the rauLogo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.FileUploadedEventArgs"/> instance containing the event data.</param>
        protected void rauLogo_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            var fileName = FileSystemProvider.Upload(CurrentUser.Instance.SiteID, "Portal", e.File.FileName, e.File.InputStream, FileType.Image);

            CurrentLogo = fileName;

            rbiLogo.ImageUrl = FileSystemProvider.GetLink(CurrentUser.Instance.SiteID, "Portal", fileName, FileType.Image);
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