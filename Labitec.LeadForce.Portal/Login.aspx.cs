using System;
using System.Web.Security;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.Portal
{
    public partial class Login : LeadForcePortalBasePage
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ReturnUrl"] != null)
            {
                var query = Request.QueryString["ReturnUrl"];
                var parameters = query.Split('/').Where(o => o.Trim() != string.Empty).ToList();
                Guid portalSettingsId;                
                if (parameters.Count > 0 && Guid.TryParse(parameters[0], out portalSettingsId))
                {
                    Response.Redirect(string.Concat("http://", Request.Url.Authority, "/", portalSettingsId, "?ReturnUrl=", query));
                }
            }

            Page.Header.DataBind();
            ErrorMessage.Visible = false;
        }


        /// <summary>
        /// Handles the Click event of the btnLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnLogin_Click(Object sender, EventArgs e)
        {
            var query = Request.QueryString["ReturnUrl"];
            var parameters = query.Split('/').Where(o => o.Trim() != string.Empty).ToList();
            Guid portalSettingsId;
            var siteId = Guid.Empty;
            if (parameters.Count > 0 && Guid.TryParse(parameters[0], out portalSettingsId))
            {
                var portalSettings = DataManager.PortalSettings.SelectById(portalSettingsId);
                if (portalSettings != null)
                    siteId = portalSettings.SiteID;                    
            }

            ErrorMessage.Visible = false;
            ErrorMessage.Text = string.Empty;

            var dataManager = new DataManager();

            var login = txtLogin.Text;
            var password = txtPassword.Text;
            var user = dataManager.User.SelectByLoginPassword(siteId, login, password);

            if (user != null)
            {
                if (user.IsActive)
                {                    
                    //FormsAuthentication.RedirectFromLoginPage(user.ID.ToString(), true);                    
                    var aUrl = Request.QueryString["ReturnUrl"];
                    FormsAuthentication.SetAuthCookie(user.ID.ToString(), true);
                    Response.Redirect(aUrl);
                }
                else
                    ErrorMessage.Text = "Вы не активировали свою учетную запись.";
            }
            else
                ErrorMessage.Text = "Неверный электронный адрес или пароль.";

            if (!string.IsNullOrEmpty(ErrorMessage.Text))
                ErrorMessage.Visible = true;
        }
    }
}