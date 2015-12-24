using System;
using System.Linq;
using System.Net;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PortalSettingsRepository
    {
        private const string CacheKey = "PortalSettings";

        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PortalSettingsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteid">The siteid.</param>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <returns></returns>
        public tbl_PortalSettings SelectById(Guid siteid, Guid portalSettingsId)
        {
            return _dataContext.tbl_PortalSettings.SingleOrDefault(c => c.SiteID == siteid && c.ID == portalSettingsId);
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteid">The siteid.</param>
        /// <returns></returns>
        public tbl_PortalSettings SelectBySiteId(Guid siteid)
        {
            return _dataContext.tbl_PortalSettings.FirstOrDefault(c => c.SiteID == siteid);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <returns></returns>
        public tbl_PortalSettings SelectById(Guid portalSettingsId)
        {
            return _dataContext.tbl_PortalSettings.SingleOrDefault(c => c.ID == portalSettingsId);
        }


        /// <summary>
        /// Selects the map by id.
        /// </summary>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <param name="fromCache">if set to <c>true</c> [from cache].</param>
        /// <returns></returns>
        public PortalSettingsMap SelectMapById(Guid portalSettingsId, bool fromCache)
        {
            var key = string.Format("{0}_SelectMapById_{1}", CacheKey, portalSettingsId);

            var cacheItem = fromCache ? LeadForceCache.Cache[key] : null;

            if (cacheItem != null)
                return (PortalSettingsMap)cacheItem;

            PortalSettingsMap portalSettings = _dataContext.tbl_PortalSettings.Select(ps => new PortalSettingsMap()
            {
                ID = ps.ID,
                SiteID = ps.SiteID,
                Domain = ps.Domain,
                Logo = ps.Logo,
                CompanyMessage = ps.CompanyMessage,
                HeaderTemplate = ps.HeaderTemplate,
                Title = ps.Title,
                WelcomeMessage = ps.WelcomeMessage,
                MainMenuBackground = ps.MainMenuBackground,
                BlockTitleBackground = ps.BlockTitleBackground
            }).SingleOrDefault(c => c.ID == portalSettingsId);

            LeadForceCache.CacheData(key, portalSettings, 60);

            return portalSettings;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="fromCache">if set to <c>true</c> [from cache].</param>
        /// <returns></returns>
        public PortalSettingsMap SelectMapBySiteId(Guid siteId, bool fromCache)
        {
            var key = string.Format("{0}_SelectMapBySiteId_{1}", CacheKey, siteId);

            var cacheItem = fromCache ? LeadForceCache.Cache[key] : null;

            if (cacheItem != null)
                return (PortalSettingsMap)cacheItem;

            var tblPortalSettings =
                _dataContext.tbl_PortalSettings.FirstOrDefault(c => c.SiteID == siteId && !string.IsNullOrEmpty(c.Domain)) ??
                _dataContext.tbl_PortalSettings.FirstOrDefault(c => c.SiteID == siteId);

            PortalSettingsMap portalSettings = null;

            if (tblPortalSettings != null)
                portalSettings = new PortalSettingsMap()
                                     {
                                         ID = tblPortalSettings.ID,
                                         SiteID = tblPortalSettings.SiteID,
                                         Domain = tblPortalSettings.Domain,
                                         Logo = tblPortalSettings.Logo,
                                         CompanyMessage = tblPortalSettings.CompanyMessage,
                                         HeaderTemplate = tblPortalSettings.HeaderTemplate,
                                         Title = tblPortalSettings.Title,
                                         WelcomeMessage = tblPortalSettings.WelcomeMessage,
                                         MainMenuBackground = tblPortalSettings.MainMenuBackground,
                                         BlockTitleBackground = tblPortalSettings.BlockTitleBackground
                                     };

            LeadForceCache.CacheData(key, portalSettings, 60);

            return portalSettings;
        }



        /// <summary>
        /// Selects the portal link.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="includePortalSettingId">if set to <c>true</c> [include portal setting id].</param>
        /// <returns></returns>
        public string SelectPortalLink(Guid siteId, bool includePortalSettingId = true)
        {
            var portalLink = string.Empty;
            var portalSettings = SelectMapBySiteId(siteId, true);
            if (portalSettings != null)
            {
                if (string.IsNullOrEmpty(portalSettings.Domain))
                    portalLink = string.Format("{0}/{1}",
                                               Configuration.Settings.LabitecLeadForcePortalUrl, portalSettings.ID);
                else
                {
                    if (includePortalSettingId)
                        portalLink = string.Format("http://{0}/{1}", portalSettings.Domain.Replace("http://", string.Empty),
                                               portalSettings.ID);
                    else
                        portalLink = string.Format("http://{0}", portalSettings.Domain.Replace("http://", string.Empty));
                }
            }

            return portalLink;
        }



        /// <summary>
        /// Selects the by domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="fromCache">if set to <c>true</c> [from cache].</param>
        /// <returns></returns>
        public PortalSettingsMap SelectMapByDomain(string domain, bool fromCache = false)
        {
            var key = string.Format("{0}_SelectMapByDomain_{1}", CacheKey, domain);

            var cacheItem = fromCache ? LeadForceCache.Cache[key] : null;

            if (cacheItem != null)
                return (PortalSettingsMap)cacheItem;

            PortalSettingsMap portalSettings = null;

            portalSettings = _dataContext.tbl_PortalSettings.Select(ps => new PortalSettingsMap()
                                                                                {
                                                                                    ID = ps.ID,
                                                                                    SiteID = ps.SiteID,
                                                                                    Domain = ps.Domain,
                                                                                    Logo = ps.Logo,
                                                                                    CompanyMessage = ps.CompanyMessage,
                                                                                    HeaderTemplate = ps.HeaderTemplate,
                                                                                    Title = ps.Title,
                                                                                    WelcomeMessage = ps.WelcomeMessage
                                                                                }).FirstOrDefault(c => c.Domain.ToLower() == domain.ToLower());

            LeadForceCache.CacheData(key, portalSettings, 300);

            return portalSettings;            
        }



        /// <summary>
        /// Determines whether [is exists domain] [the specified site id].
        /// </summary>        
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///   <c>true</c> if [is exists domain] [the specified site id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExistsDomain(Guid portalSettingsId, string domain)
        {
            return _dataContext.tbl_PortalSettings.SingleOrDefault(ps => ps.ID != portalSettingsId  && ps.Domain == domain.Trim()) != null;
        }



        /// <summary>
        /// Adds the specified portal settings.
        /// </summary>
        /// <param name="portalSettings">The portal settings.</param>
        /// <returns></returns>
        public tbl_PortalSettings Add(tbl_PortalSettings portalSettings)
        {
            portalSettings.ID = Guid.NewGuid();
            _dataContext.tbl_PortalSettings.AddObject(portalSettings);
            _dataContext.SaveChanges();

            var dataManager = new DataManager();
            dataManager.StatisticData.LoyaltyManagementIsExistPortal.DbValue = IsPortalUp(portalSettings.SiteID) ? 1 : 0;
            dataManager.StatisticData.LoyaltyManagementIsExistPortal.Update(portalSettings.SiteID);

            return portalSettings;
        }



        /// <summary>
        /// Updates the specified order.
        /// </summary>
        /// <param name="portalSettings">The portal settings.</param>
        public void Update(tbl_PortalSettings portalSettings)
        {            
            _dataContext.SaveChanges();

            var dataManager = new DataManager();
            dataManager.StatisticData.LoyaltyManagementIsExistPortal.DbValue = IsPortalUp(portalSettings.SiteID) ? 1 : 0;
            dataManager.StatisticData.LoyaltyManagementIsExistPortal.Update(portalSettings.SiteID);
        }



        /// <summary>
        /// Determines whether [is portal up] [the specified site id].
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns>
        ///   <c>true</c> if [is portal up] [the specified site id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPortalUp(Guid siteId)
        {
            var portal = SelectBySiteId(siteId);
            if (portal == null || string.IsNullOrEmpty(portal.Domain))
                return false;

            var url = "http://" + portal.Domain.Replace("http://", string.Empty);

            string page;
            try
            {
                using (var webClient = new WebClient())
                {
                    page = webClient.DownloadString(url);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Ошибка проверки портала: ID {0}", portal.ID), ex);
                return false;
            }

            if (string.IsNullOrEmpty(page) || !page.Contains("LeadForcePortal"))
                return false;

            return true;
        }
    }
}