using System;
using System.Web;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public static class CurrentUser
    {
        private const string UserSessionKey = "AdminPanelCurrentUser";
        private static readonly object ForLock = new object();
        private static UserMap _objectUser;

        public static UserMap Instance
        {
            get
            {
                lock (ForLock)
                {
                    
                    if (HttpContext.Current.Session[UserSessionKey] == null)
                    {

                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            var dataManager = new DataManager();

                            var userId = Guid.Parse(HttpContext.Current.User.Identity.Name);
                            var userProfile = dataManager.User.SelectById(userId);

                            _objectUser = new UserMap();

                            if (userProfile != null)
                            {
                                _objectUser.ID = userProfile.ID;
                                _objectUser.SiteID = userProfile.SiteID;
                                _objectUser.ContactID = userProfile.ContactID;
                                _objectUser.Login = userProfile.Login;
                                _objectUser.Password = userProfile.Password;
                                _objectUser.IsActive = userProfile.IsActive;
                                _objectUser.AccessLevelID = userProfile.AccessLevelID;
                                _objectUser.AccessLevel = (AccessLevel)userProfile.AccessLevelID;
                                _objectUser.AccessProfileID = userProfile.AccessProfileID;
                                var site = dataManager.Sites.SelectById(userProfile.SiteID);
                                _objectUser.SiteAccessProfileID = site.AccessProfileID;
                                _objectUser.IsTemplateSite = site.IsTemplate;

                                if (userProfile.ContactID.HasValue)
                                {
                                    var contact = dataManager.Contact.SelectById(userProfile.SiteID, (Guid) userProfile.ContactID);
                                    if (contact != null)
                                    {
                                        _objectUser.UserFullName = contact.UserFullName;
                                        _objectUser.CompanyID = contact.CompanyID;
                                    }
                                }
                            }
                            HttpContext.Current.Session[UserSessionKey] = _objectUser;
                        }
                        else                        
                            _objectUser = null;
                    }
                    else
                    {
                        _objectUser = (UserMap)HttpContext.Current.Session[UserSessionKey];
                    }


                    return _objectUser;
                }
            }
            set
            {                
                HttpContext.Current.Session[UserSessionKey] = value;
            }
        }



        /// <summary>
        /// Users the instance flush.
        /// </summary>
        public static void UserInstanceFlush()
        {            
            HttpContext.Current.Session[UserSessionKey] = null;            
        }
    }
}
