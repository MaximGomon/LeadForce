using System;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class LoggedAs : System.Web.UI.UserControl
    {
        protected bool NeedLogin = true;        
        protected Guid PortalSettingsId = Guid.Empty;
        protected string Login = string.Empty;
        protected string PopupTitle = string.Empty;

        public event UserAuthorizedEventHandler UserAuthorized;
        public delegate void UserAuthorizedEventHandler(object sender, UserAuthorizedEventArgs e);
        public class UserAuthorizedEventArgs : EventArgs
        {
            public Guid UserId { get; set; }
        }


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


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsPopup
        {
            get
            {
                if (ViewState["IsPopup"] == null)
                    ViewState["IsPopup"] = true;

                return (bool)ViewState["IsPopup"];
            }
            set { ViewState["IsPopup"] = value; }
        }



        /// <summary>
        /// Sets the CSS.
        /// </summary>
        /// <param name="css">The CSS.</param>
        public void SetCSS(string css)
        {
            lbtnLogin.Attributes.Add("style", css);
            lbtnReg.Attributes.Add("style", css);
            lbtnRemindPassword.Attributes.Add("style", css);
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Login = CurrentUser.Instance.UserFullName;
                plAnonymous.Visible = false;
                plLogged.Visible = true;
            }
            else
            {
                plAnonymous.Visible = true;
                plLogged.Visible = false;
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    if (!Page.ClientScript.IsStartupScriptRegistered("ShowLogin"))
                        ScriptManager.RegisterStartupScript(this, typeof(LoggedAs), "ShowLogin", "NeedLogin();", true);
                }
            }

            NeedLogin = CurrentUser.Instance == null;
            if (Page is LeadForcePortalBasePage)
            {                
                RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnLogin, plAnonymous);
                RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnLogin, plLogged);            

                SiteId = ((LeadForcePortalBasePage) Page).SiteId;
                PortalSettingsId = ((LeadForcePortalBasePage) Page).PortalSettingsId;
                hlAgreement.NavigateUrl = UrlsData.LFP_Agreement(PortalSettingsId);
                lrlPopupTitle.Text = string.Format("<h3>{0}</h3>", ((LeadForcePortalBasePage) Page).PortalSettings.WelcomeMessage);                
            }
            else
            {
                ucSocialAuthorization.SiteId = SiteId;
                plAnonymous.Visible = false;
                rapLogin.EnableAJAX = false;
                rapReg.EnableAJAX = false;
                rapRemind.EnableAJAX = false;
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnLogin_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ucLoginNotificationMessage.Text = string.Empty;

            var dataManager = new DataManager();

            var login = txtEmail.Text;
            var password = txtPassword.Text;
            var user = dataManager.User.SelectByLoginPassword(SiteId, login, password);

            if (user != null)
            {
                if (user.IsActive)
                {
                    if (user.tbl_Contact!= null)
                        Login = user.tbl_Contact.UserFullName;
                    NeedLogin = false;
                    plAnonymous.Visible = false;
                    plLogged.Visible = true;
                    FormsAuthentication.SetAuthCookie(user.ID.ToString(), true);

                    if (Request.QueryString["ReturnUrl"] != null)                    
                        Response.Redirect(Request.QueryString["ReturnUrl"]);                    
                    else
                    {
                        if (IsPopup)
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseLoginPopup", "needLogin = false;$.modal.close();", true);                        

                        if (UserAuthorized != null)
                            UserAuthorized(this, new UserAuthorizedEventArgs() { UserId = user.ID });
                    }                    
                }
                else
                    ucLoginNotificationMessage.Text = "Вы не активировали свою учетную запись.";                
            }
            else
                ucLoginNotificationMessage.Text = "Неверный электронный адрес или пароль.";            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnReg control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnReg_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowReg", "ShowReg();", true);
                return;
            }

            ucRegNotificationMessage.Text = string.Empty;
            if (string.IsNullOrEmpty(txtRegName.Text) || string.IsNullOrEmpty(txtRegPassword.Text) || string.IsNullOrEmpty(txtRegEmail.Text) || !chxIsAgree.Checked)
            {
                ucRegNotificationMessage.Text = "Пожалуйста заполните все поля корректно.";
                return;
            }

            try
            {
                var dataManager = new DataManager();

                if (dataManager.User.SelectByEmail(SiteId, txtRegEmail.Text) != null)
                {
                    ucRegNotificationMessage.MessageType = NotificationMessageType.Warning;
                    ucRegNotificationMessage.Text = "Пользователь с таким электронным адресом уже зарегистрирован в системе.";
                    return;
                }

                var contactId = dataManager.User.RegisterUser(SiteId, txtRegName.Text, txtRegEmail.Text, txtRegEmail.Text,
                                                              txtRegPassword.Text, Request.UserHostAddress);

                dataManager.SiteAction.AddNotification(SiteId, PortalSettingsId, contactId,
                                                       SiteActionTemplates.NewUserRegistrationNotification,
                                                       MessageType.Notification);
                plRegistrtion.Visible = false;
                plRegClose.Visible = true;
            }
            catch (Exception ex)
            {
                ucRegNotificationMessage.MessageType = NotificationMessageType.Warning;
                ucRegNotificationMessage.Text = "При обработке запроса возникла ошибка. Попробуйте позже.";
                Log.Error("Ошибка регистрации нового пользователя", ex);
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnLogout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnLogout_OnClick(object sender, EventArgs e)
        {
            ((LeadForcePortalBasePage) Page).Logout();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnRemindPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnRemindPassword_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                var dataManager = new DataManager();
                var user = dataManager.User.SelectByEmail(SiteId, txtRemindPasswordEmail.Text);
                if (user != null)
                {
                    dataManager.SiteAction.AddNotification(SiteId, PortalSettingsId, (Guid)user.ContactID,
                                                           SiteActionTemplates.PasswordRemindNotification,
                                                           MessageType.Notification);

                    ucRemindNotificationMessage.MessageType = NotificationMessageType.Success;
                    ucRemindNotificationMessage.Text = "Запрос успешно обработан. Пароль будет выслан вам на электронный ящик.";
                    plRemindContainer.Visible = false;
                    plRemindPasswordClose.Visible = true;
                    txtRemindPasswordEmail.Text = string.Empty;                   
                }
                else
                {
                    ucRemindNotificationMessage.MessageType = NotificationMessageType.Warning;
                    ucRemindNotificationMessage.Text = "Пользователь с таким электронным адресом не найден. Убедитесь в правильности ввода.";
                    txtRemindPasswordEmail.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Ошибка восстановления пароля", ex);
                ucRemindNotificationMessage.MessageType = NotificationMessageType.Warning;
                ucRemindNotificationMessage.Text = "При обработке запроса возникла ошибка. Попробуйте позже.";
            }
        }
    }
}