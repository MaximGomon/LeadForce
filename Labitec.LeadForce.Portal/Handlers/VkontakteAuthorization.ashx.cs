using System;
using System.Web;
using System.Web.Security;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Providers.SocialNetwork.Vkontakte;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Handlers
{
    /// <summary>
    /// Summary description for VkontakteAuthorization
    /// </summary>
    public class VkontakteAuthorization : IHttpHandler
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
                
                var api = new VkontakteAPI(context.Request.QueryString["code"]);
                var userProfile = api.GetProfile();
                var country = api.GetCountry(userProfile.country);
                var city = api.GetCity(userProfile.city);
                
                var portalSettings = dataManager.PortalSettings.SelectById(portalSettingsId);
                var siteId = portalSettings.SiteID;

                var user = dataManager.User.SelectByEmail(siteId, userProfile.screen_name);

                if (user == null)
                {
                    var contactId = dataManager.User.RegisterUser(siteId,
                                                                string.Format("{0} {1}", userProfile.first_name,
                                                                              userProfile.last_name),
                                                                userProfile.screen_name, string.Empty,
                                                                Guid.NewGuid().ToString());

                    var contact = dataManager.Contact.SelectById(siteId, contactId);

                    var tblCountry = dataManager.Country.SelectByTitle(country.name);
                    var tblCity = dataManager.City.SelectByTitle(city.name);
                    
                    if (tblCountry != null || tblCity != null)
                    {
                        var address = new tbl_Address();
                        if (tblCountry != null)
                            address.CountryID = tblCountry.ID;
                        if (tblCity != null)
                            address.CityID = tblCity.ID;
                        
                        contact.AddressID = dataManager.Address.Add(address);
                    }

                    DateTime birthDate;
                    if (DateTime.TryParse(userProfile.bdate, out  birthDate))
                        contact.BirthDate = birthDate;

                    dataManager.Contact.Update(contact);

                    user = dataManager.User.SelectByContactId(siteId, contact.ID);

                    var contactCommunication = new tbl_ContactCommunication
                                                   {
                                                       ContactID = contact.ID,
                                                       CommunicationNumber = userProfile.uid,
                                                       CommunicationType = (int) CommunicationType.VKontakte
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