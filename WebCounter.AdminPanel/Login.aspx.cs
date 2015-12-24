using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Login1 : System.Web.UI.Page
    {
        private DataManager dataManager = new DataManager();



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "LeadForce";

            AdminPanelCss.Href = ResolveUrl("~/App_Themes/Default/AdminPanel.css");
            ErrorMessage.Visible = false;
        }



        /// <summary>
        /// Handles the Click event of the btnLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnLogin_Click(Object sender, EventArgs e)
        {           
            var aUrl = Request.QueryString["ReturnUrl"];

            var login = txtLogin.Text;
            var password = txtPassword.Text;
            var users = dataManager.User.SelectByLoginPassword(login, password).Where(u => u.AccessLevelID != (int)AccessLevel.Portal).ToList();

            ErrorMessage.Visible = false;

            if (users != null && users.Count > 0)
            {
                if (users.Count > 1)
                {
                    if (pSites.Visible == false)
                    {
                        ddlSites.Items.Clear();
                        ddlSites.Items.Add(new ListItem("", ""));
                        foreach (var u in users)
                        {
                            var site = dataManager.Sites.SelectById(u.SiteID);
                            ddlSites.Items.Add(new ListItem(site.Name, site.ID.ToString()));
                        }
                    }

                    if (!string.IsNullOrEmpty(ddlSites.SelectedValue))
                    {
                        var siteId = Guid.Parse(ddlSites.SelectedValue);
                        var user = users.SingleOrDefault(a => a.SiteID == siteId);

                        if (user.IsActive)
                        {
                            AuthorizeUser(user);
                        }
                        else
                        {
                            ErrorMessage.Text = "Вы не активировали свою учетную запись.";
                            ErrorMessage.Visible = true;
                        }
                    }
                    else
                    {
                        if (pSites.Visible)
                        {
                            ErrorMessage.Text = "Вы не выбрали сайт.";
                            ErrorMessage.Visible = true;
                        }
                    }

                    pSites.Visible = true;
                }
                else
                {
                    if (users[0].IsActive)
                    {
                        AuthorizeUser(users[0]);
                    }
                    else
                    {
                        ErrorMessage.Text = "Вы не активировали свою учетную запись.";
                        ErrorMessage.Visible = true;
                    }

                    pSites.Visible = false;
                }
            }
            else
            {
                ErrorMessage.Text = "Неверное имя пользователя или пароль.";
                ErrorMessage.Visible = true;

                pSites.Visible = false;
            }

            txtPassword.Attributes.Add("value", txtPassword.Text);
        }



        /// <summary>
        /// Authorizes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        protected void AuthorizeUser(tbl_User user)
        {
            var site = dataManager.Sites.SelectById(user.SiteID);

            var tkt = new FormsAuthenticationTicket(1, user.ID.ToString(), DateTime.Now,
                                                    (!Persist.Checked ? DateTime.Now.AddMinutes(site.UserSessionTimeout) : DateTime.Now.AddDays(7)),
                                                    Persist.Checked, string.Empty);

            var cookiestr = FormsAuthentication.Encrypt(tkt);

            var ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);

            if (Persist.Checked)
                ck.Expires = DateTime.Now.AddDays(7);

            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Add(ck);

            var strRedirect = Request["ReturnUrl"] ?? "default.aspx";
            
            Response.Redirect(strRedirect, true);
        }
    }
}