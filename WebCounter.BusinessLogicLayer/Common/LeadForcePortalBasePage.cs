using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class LeadForcePortalBasePage : System.Web.UI.Page
    {
        public DataManager DataManager = new DataManager();
        public Guid ObjectId { get; private set; }
        public tbl_User TblUser = null;

        /// <summary>
        /// Gets the site id.
        /// </summary>
        public Guid SiteId
        {
            get
            {                
                var portaSettings = DataManager.PortalSettings.SelectMapById(PortalSettingsId, true);

                if (portaSettings != null)                
                    return portaSettings.SiteID;                

                return Guid.Empty;
            }
        }



        /// <summary>
        /// Gets the portal settings id.
        /// </summary>
        public Guid PortalSettingsId
        {
            get
            {
                PortalSettingsMap portaSettings;

                var guidId = Guid.Empty;
                var id = Page.RouteData.Values["portalSettingsId"] as string;
                if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out guidId))
                {
                    portaSettings = DataManager.PortalSettings.SelectMapById(guidId, true);
                    if (portaSettings != null)
                        return portaSettings.ID;
                }

                var domain = Request.Url.Host;

                portaSettings = DataManager.PortalSettings.SelectMapByDomain(domain, true);

                if (portaSettings != null)
                    return portaSettings.ID;

                return guidId;
            }
        }



        /// <summary>
        /// Gets the portal settings.
        /// </summary>
        public PortalSettingsMap PortalSettings
        {
            get
            {
                var portalSettings = DataManager.PortalSettings.SelectMapById(PortalSettingsId, true) ?? new PortalSettingsMap();

                if (!string.IsNullOrEmpty(portalSettings.HeaderTemplate))
                {
                    if (!portalSettings.HeaderTemplate.Contains("http"))
                        portalSettings.HeaderTemplate = portalSettings.HeaderTemplate.Replace("/files/" + SiteId, 
                            ConfigurationManager.AppSettings["LeadForceSiteUrl"] + "/files/" + SiteId);
                }                

                return portalSettings;
            }
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (CurrentUser.Instance != null)
            {
                TblUser = DataManager.User.SelectById(CurrentUser.Instance.ID);
                if (TblUser.SiteID != SiteId)
                {
                    Logout();
                }
            }

            if (Page.RouteData.Values["id"] == null) 
                return;
            
            var id = Page.RouteData.Values["id"] as string;
            Guid guidId;
            if (Guid.TryParse(id, out guidId))
                ObjectId = guidId;
        }



        /// <summary>
        /// Checks the read access.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="moduleName">Name of the module.</param>
        public void CheckReadAccess(Guid? ownerId, string moduleName)
        {        
            if (ownerId.HasValue)
            {
                var owner = DataManager.Contact.SelectById(SiteId, (Guid)ownerId);
                if (owner != null)
                {
                    var accessCheck = Access.Check(TblUser, moduleName, owner.ID, owner.CompanyID);
                    if (!accessCheck.Read)
                        Response.Redirect(UrlsData.LFP_AccessDenied(PortalSettingsId));
                }
            }        
        }



        /// <summary>
        /// Checks the write access.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="moduleName">Name of the module.</param>
        public void CheckWriteAccess(Guid? ownerId, string moduleName)
        {
            if (ownerId.HasValue)
            {
                var owner = DataManager.Contact.SelectById(SiteId, (Guid)ownerId);
                if (owner != null)
                {
                    var accessCheck = Access.Check(TblUser, moduleName, owner.ID, owner.CompanyID);
                    if (!accessCheck.Write)
                        Response.Redirect(UrlsData.LFP_AccessDenied(PortalSettingsId));
                }
            }
        }



        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public void Logout()
        {
            HttpContext.Current.Session.Remove("siteID");
            CurrentUser.UserInstanceFlush();
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect(UrlsData.LFP_Home(PortalSettingsId));
        }
    }
}
