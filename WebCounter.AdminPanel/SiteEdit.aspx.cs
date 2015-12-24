using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class SiteEdit : LeadForceBasePage
    {       
        private Guid _siteId;
        protected List<MonthlyStats> Stats = new List<MonthlyStats>();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentUser.Instance.AccessLevelID != (int)AccessLevel.SystemAdministrator)
                Response.Redirect(UrlsData.AP_Home());

            Title = "Сайты - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                _siteId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_Sites();

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rcbAccessProfile, plAccessBlock, null, UpdatePanelRenderMode.Inline);

            tagsSite.ObjectID = _siteId;

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            BindStats();

            ucSiteDomains.SiteId = ucPortal.SiteId = _siteId;
            ucSiteDomains.Module = ucPortal.Module = "Sites";
            
            ucPayerCompany.SiteID = SiteId;
            ucPriceList.SiteID = SiteId;

            var site = DataManager.Sites.SelectById(_siteId);

            var emailActions = DataManager.EmailActions.SelectAll();
            BindEmailActions(rblUnsubscribeActions, emailActions);
            BindEmailActions(rblServiceAdvertisingActions, emailActions);

            rcbAccessProfile.DataSource = DataManager.AccessProfile.SelectAll(null);
            rcbAccessProfile.DataTextField = "Title";
            rcbAccessProfile.DataValueField = "ID";
            rcbAccessProfile.DataBind();
            rcbAccessProfile.Items.Insert(0, new RadComboBoxItem("Выберите профиль", Guid.Empty.ToString()) { Selected = true });

            if (site != null)
            {
                txtName.Text = site.Name;
                txtLinkProcessingURL.Text = site.LinkProcessingURL;
                lrlCounterCode.Text = HttpUtility.HtmlEncode(ScriptTemplates.Counter(_siteId));
                lrlScriptCode.Text = HttpUtility.HtmlEncode(ScriptTemplates.Script(true));
                chxIsActive.Checked = site.IsActive;
                chxIsTemplate.Checked = site.IsTemplate;
                txtSmtpHost.Text = site.SmtpHost;
                txtSmtpUsername.Text = site.SmtpUsername;
                txtSmtpPassword.Attributes["value"] = site.SmtpPassword;
                txtSmtpPort.Text = site.SmtpPort.ToString();
                txtSystemEmail.Text = site.SystemEmail;
                chxIsAllowUseSystemEmail.Checked = site.IsAllowUseSystemEmail;                
                chxIsSendEmailToSubscribedUser.Checked = site.IsSendEmailToSubscribedUser;
                chxIsSendFromLeadForce.Checked = site.IsSendFromLeadForce;
                txtSessionTimeout.Text = site.SessionTimeout.ToString();
                txtUserSessionTimeout.Text = site.UserSessionTimeout.ToString();                

                if (site.MainUserID.HasValue)
                {
                    var user = DataManager.User.SelectById(site.MainUserID.Value);
                    ucMainUser.SelectedIdNullable = user.ID;
                    ucMainUser.SelectedText = user.Login;
                }
                rdpActiveUntilDate.SelectedDate = site.ActiveUntilDate;
                if (site.PriceListID.HasValue)
                {
                    var priceList = DataManager.PriceList.SelectById(site.PriceListID.Value);
                    ucPriceList.SelectedIdNullable = priceList.ID;
                    ucPriceList.SelectedText = priceList.Title;
                }
                if (site.PayerCompanyID.HasValue)
                {
                    var company = DataManager.Company.SelectById(CurrentUser.Instance.SiteID, site.PayerCompanyID.Value);
                    ucPayerCompany.SelectedIdNullable = company.ID;
                    ucPayerCompany.SelectedText = company.Name;
                }

                if (site.UnsubscribeActionID != null)
                    rblUnsubscribeActions.SelectedIndex = rblUnsubscribeActions.Items.IndexOf(rblUnsubscribeActions.Items.FindByValue(site.UnsubscribeActionID.ToString()));

                if (site.ServiceAdvertisingActionID != null)
                    rblServiceAdvertisingActions.SelectedIndex = rblServiceAdvertisingActions.Items.IndexOf(rblServiceAdvertisingActions.Items.FindByValue(site.ServiceAdvertisingActionID.ToString()));

                txtMaxFileSize.Text = site.MaxFileSize.ToString();
                txtFileQuota.Text = site.FileQuota.ToString();

                lrlClientCount.Text = DataManager.Contact.SelectCount(_siteId).ToString();


                if (site.AccessProfileID != null)
                {
                    rcbAccessProfile.SelectedIndex = rcbAccessProfile.Items.FindItemIndexByValue(site.AccessProfileID.ToString());

                    var accessProfile = DataManager.AccessProfile.SelectById(site.AccessProfileID.Value);
                    if (accessProfile != null && accessProfile.DomainsCount > 0)
                    {
                        chxIsBlockAccessFromDomainsOutsideOfList.Checked = true;
                        plAccessBlock.Visible = false;
                    }
                }

                plCounterCode.Visible = true;                
            }
            else
            {
                plMainUser.Visible = false;
                txtSessionTimeout.Text = "30";
                txtUserSessionTimeout.Text = "30";
                pUser.Visible = true;
                rblUnsubscribeActions.SelectedIndex = rblUnsubscribeActions.Items.IndexOf(rblUnsubscribeActions.Items.FindByValue("3"));
            }
        }



        /// <summary>
        /// Binds the stats.
        /// </summary>
        private void BindStats()
        {
            Stats = MonthlyStats.Select(_siteId);
            rprSmsSendCount.DataSource = rprEmailSendCount.DataSource = rprMonthes.DataSource = rprPageViews.DataSource = Stats;
            rprPageViews.DataBind();
            rprMonthes.DataBind();
            rprEmailSendCount.DataBind();
            rprSmsSendCount.DataBind();
        }



        /// <summary>
        /// Binds the email actions.
        /// </summary>
        /// <param name="radioButtonList">The radio button list.</param>
        /// <param name="emailActions">The email actions.</param>
        protected void BindEmailActions(RadioButtonList radioButtonList, List<tbl_EmailActions> emailActions)
        {
            radioButtonList.DataSource = emailActions;
            radioButtonList.DataValueField = "ID";
            radioButtonList.DataTextField = "Title";
            radioButtonList.DataBind();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            var site = DataManager.Sites.SelectById(_siteId) ?? new tbl_Sites();

            site.Name = txtName.Text;
            site.LinkProcessingURL = txtLinkProcessingURL.Text;
            site.IsActive = chxIsActive.Checked;
            site.IsTemplate = chxIsTemplate.Checked;
            site.SmtpHost = txtSmtpHost.Text;
            site.SmtpUsername = txtSmtpUsername.Text;
            site.SmtpPassword = txtSmtpPassword.Text;

            int smtpPort;
            if (int.TryParse(txtSmtpPort.Text, out smtpPort))
                site.SmtpPort = smtpPort;
            else
                site.SmtpPort = null;

            site.SystemEmail = txtSystemEmail.Text;
            site.IsAllowUseSystemEmail = chxIsAllowUseSystemEmail.Checked;            
            site.IsSendEmailToSubscribedUser = chxIsSendEmailToSubscribedUser.Checked;
            site.IsSendFromLeadForce = chxIsSendFromLeadForce.Checked;

            if (rblUnsubscribeActions.SelectedValue != string.Empty)
                site.UnsubscribeActionID = int.Parse(rblUnsubscribeActions.SelectedValue);
            else
                site.UnsubscribeActionID = null;

            if (rblServiceAdvertisingActions.SelectedValue != string.Empty)
                site.ServiceAdvertisingActionID = int.Parse(rblServiceAdvertisingActions.SelectedValue);
            else
                site.ServiceAdvertisingActionID = null;

            int maxFileSize;
            if (int.TryParse(txtMaxFileSize.Text, out maxFileSize))
                site.MaxFileSize = maxFileSize;

            int fileQuota;
            if (int.TryParse(txtFileQuota.Text, out fileQuota))
                site.FileQuota = fileQuota;

            int sessionTimeout;
            if (int.TryParse(txtSessionTimeout.Text, out sessionTimeout))
                site.SessionTimeout = sessionTimeout;

            int userSessionTimeout;
            if (int.TryParse(txtUserSessionTimeout.Text, out  userSessionTimeout))
                site.UserSessionTimeout = userSessionTimeout;

            if (rcbAccessProfile.SelectedValue != Guid.Empty.ToString())
                site.AccessProfileID = Guid.Parse(rcbAccessProfile.SelectedValue);
            else
                site.AccessProfileID = null;

            site.IsBlockAccessFromDomainsOutsideOfList = chxIsBlockAccessFromDomainsOutsideOfList.Checked;
            site.MainUserID = ucMainUser.SelectedIdNullable;
            site.ActiveUntilDate = rdpActiveUntilDate.SelectedDate;
            site.PriceListID = ucPriceList.SelectedIdNullable;
            site.PayerCompanyID = ucPayerCompany.SelectedIdNullable;

            if (site.ID == Guid.Empty)
            {
                if (site.AccessProfileID != null)
                {                    
                    var accessProfile = DataManager.AccessProfile.SelectById(site.AccessProfileID.Value);
                    if (accessProfile != null && accessProfile.DomainsCount > 0)                    
                        site.IsBlockAccessFromDomainsOutsideOfList = true;                                                                        
                }

                site.ID = Guid.NewGuid();                
                site = DataManager.Sites.Add(site);

		var contact = new tbl_Contact
                {
                    SiteID = site.ID,
                    Email = txtEmail.Text,
		    UserFullName = txtEmail.Text,
                    RefferID = null,
                    RefferURL = string.Empty,
                    IsNameChecked = false,
                    UserIP = string.Empty,
                    StatusID = DataManager.Status.SelectDefault(site.ID).ID
                };
                DataManager.Contact.Add(contact);

                var user = new tbl_User
                               {
                                   SiteID = site.ID,
                                   Login = txtEmail.Text,
                                   Password = txtPassword.Text,
                                   IsActive = true,
                                   AccessLevelID = 1,
                                   ContactID = contact.ID
                               };

                DataManager.User.Add(user);
                site.MainUserID = user.ID;
                DataManager.Sites.Update(site);
            }
            else            
                DataManager.Sites.Update(site);

            tagsSite.SaveTags(site.ID);   
         
            Response.Redirect(UrlsData.AP_Sites());
        }



        protected void rcbAccessProfile_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(rcbAccessProfile.SelectedValue))
            {
                var accessProfile = DataManager.AccessProfile.SelectById(Guid.Parse(rcbAccessProfile.SelectedValue));
                if (accessProfile != null && accessProfile.DomainsCount > 0)
                {
                    chxIsBlockAccessFromDomainsOutsideOfList.Checked = true;
                    plAccessBlock.Visible = false;
                }
                else
                {
                    chxIsBlockAccessFromDomainsOutsideOfList.Checked = false;
                    plAccessBlock.Visible = true;
                }
            }
        }
    }
}