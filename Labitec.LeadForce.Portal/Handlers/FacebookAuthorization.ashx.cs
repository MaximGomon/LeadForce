using System;
using System.Web;
using System.Web.Security;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Providers.SocialNetwork.Facebook;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Handlers
{
    /// <summary>
    /// Summary description for FacebookAuthorization
    /// </summary>
    public class FacebookAuthorization : IHttpHandler
    {
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            var dataManager = new DataManager();

            if (!string.IsNullOrEmpty(context.Request.QueryString["code"]) && !string.IsNullOrEmpty(context.Request.QueryString["psid"]))
            {
                var portalSettingsId = Guid.Parse(context.Request.QueryString["psid"].Substring(0, 36));
                var domain = context.Request.QueryString["psid"].Substring(36, context.Request.QueryString["psid"].Length - 36);

                var api = new FacebookAPI(context.Request.QueryString["code"], portalSettingsId, domain);
                var userProfile = api.GetProfile();                

                var portalSettings = dataManager.PortalSettings.SelectById(portalSettingsId);

                var siteId = portalSettings.SiteID;

                var user = dataManager.User.SelectByEmail(siteId, userProfile.id);

                if (user == null)
                {
                    var contactId = dataManager.User.RegisterUser(siteId,
                                                                string.Format("{0} {1}", userProfile.first_name,
                                                                              userProfile.last_name),
                                                                userProfile.id, string.Empty,
                                                                Guid.NewGuid().ToString());

                    var contact = dataManager.Contact.SelectById(siteId, contactId);                    

                    dataManager.Contact.Update(contact);
                    user = dataManager.User.SelectByContactId(siteId, contact.ID);

                    var contactCommunication = new tbl_ContactCommunication
                    {
                        ContactID = contact.ID,
                        CommunicationNumber = userProfile.id,
                        CommunicationType = (int)CommunicationType.Facebook
                    };
                    dataManager.ContactCommunication.Add(contactCommunication);
                }

                var socialAuthToken = new tbl_SocialAuthorizationToken
                {
                    UserID = user.ID,
                    PortalSettingsID = portalSettingsId,
                    ExpirationDate = DateTime.Now.AddMinutes(5)
                };

                socialAuthToken = dataManager.SocialAuthorizationToken.Add(socialAuthToken);
                var url = HttpUtility.UrlDecode(context.Request.Url.ToString())
                                    .Replace(HttpUtility.UrlDecode(context.Request.Url.Query), "")
                                    .Replace(HttpUtility.UrlDecode(context.Request.Url.Host), HttpUtility.UrlDecode(domain));

                context.Response.Redirect(url + "?sat=" + socialAuthToken.ID, true);
            }
            else if (!string.IsNullOrEmpty(context.Request.QueryString["sat"]))
            {
                var socialAuthToken = dataManager.SocialAuthorizationToken.SelectById(Guid.Parse(context.Request.QueryString["sat"]));
                if (socialAuthToken != null && socialAuthToken.ExpirationDate > DateTime.Now)
                {
                    dataManager.SocialAuthorizationToken.Delete(socialAuthToken.ID);
                    FormsAuthentication.SetAuthCookie(socialAuthToken.UserID.ToString(), true);
                }
                context.Response.Write("<script type=\"text/javascript\">window.opener.document.location.href = window.opener.document.location.href;window.close();</script>");
            }
            else
                context.Response.Write("<script type=\"text/javascript\">window.opener.document.location.href = window.opener.document.location.href;window.close();</script>");                     
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}