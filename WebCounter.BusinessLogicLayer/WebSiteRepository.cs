using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WebSiteRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSiteRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WebSiteRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_WebSite SelectById(Guid siteId, Guid id)
        {
            return _dataContext.tbl_WebSite.SingleOrDefault(o => o.ID == id && o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_WebSite SelectById(Guid id)
        {
            return _dataContext.tbl_WebSite.SingleOrDefault(o => o.ID == id);
        }



        /// <summary>
        /// Selects the by domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns></returns>
        public tbl_WebSite SelectByDomain(string domain)
        {
            domain = domain.Replace("www.", string.Empty);

            return
                _dataContext.tbl_WebSite.SingleOrDefault(
                    o => o.SiteDomainID.HasValue && (o.tbl_SiteDomain.Domain.StartsWith("http://" + domain) || o.tbl_SiteDomain.Domain.StartsWith("http://www." + domain)));
        }


        public tbl_WebSite SelectByAlias(string domain)
        {
            domain = domain.Replace("www.", string.Empty);

            return
                _dataContext.tbl_WebSite.FirstOrDefault(
                    o => o.SiteDomainID.HasValue && o.tbl_SiteDomain.Aliases.Contains(domain));
        }



        /// <summary>
        /// Adds the specified web site.
        /// </summary>
        /// <param name="webSite">The web site.</param>
        public void Add(tbl_WebSite webSite)
        {
            webSite.ID = Guid.NewGuid();
            _dataContext.tbl_WebSite.AddObject(webSite);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified web site.
        /// </summary>
        /// <param name="webSite">The web site.</param>
        public void Update(tbl_WebSite webSite)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="webSiteId">The web site id.</param>
        public void Delete(Guid siteId, Guid webSiteId)
        {
            var toDelete = _dataContext.tbl_WebSite.SingleOrDefault(o => o.ID == webSiteId && o.SiteID == siteId);
            if (toDelete != null)
            {
                _dataContext.DeleteObject(toDelete);
                _dataContext.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the web site URL.
        /// </summary>
        /// <returns></returns>
        public string GetWebSiteUrl(Guid siteId, Guid webSiteId, bool withId = true)
        {
            var result = string.Empty;            

            if (webSiteId != Guid.Empty)
            {
                var webSite = SelectById(siteId, webSiteId);

                if (webSite != null)
                {
                    if (!webSite.SiteDomainID.HasValue)
                    {
                        if (withId)
                            result = Configuration.Settings.MiniSiteUrl(webSite.ID) + "/";
                        else
                            result = Configuration.Settings.MiniSiteUrl(webSite.ID).Replace(webSite.ID.ToString(), string.Empty);
                    }
                    else
                    {
                        var dataManager = new DataManager();
                        var siteDomain = dataManager.SiteDomain.SelectById(webSite.SiteDomainID.Value);
                        if (siteDomain != null)
                        {
                            var url = dataManager.SiteDomain.GetDomainUrl(siteDomain.Domain);
                            result = url != null ? url.ToString() : string.Empty;
                        }
                    }
                }
            }

            return result;
        }
    }
}