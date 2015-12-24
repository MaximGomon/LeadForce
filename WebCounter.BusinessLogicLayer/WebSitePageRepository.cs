using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WebSitePageRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSitePageRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WebSitePageRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by web id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="webSitePageId">The web site page id.</param>
        /// <returns></returns>
        public tbl_WebSitePage SelectById(Guid siteId, Guid webSitePageId)
        {
            return _dataContext.tbl_WebSitePage.FirstOrDefault(o => o.ID == webSitePageId && o.tbl_WebSite.SiteID == siteId);
        }


        /// <summary>
        /// Determines whether [is exists page] [the specified web site id].
        /// </summary>
        /// <param name="webSiteId">The web site id.</param>
        /// <param name="pageId">The page id.</param>
        /// <param name="url">The URL.</param>
        /// <returns>
        ///   <c>true</c> if [is exists page] [the specified web site id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExistsPage(Guid webSiteId, Guid pageId, string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            return _dataContext.tbl_WebSitePage.Any(o => o.WebSiteID == webSiteId && o.ID != pageId && o.Url == url);
        }



        /// <summary>
        /// Selects the by URL.
        /// </summary>
        /// <param name="webSiteId">The web site id.</param>
        /// <param name="pageUrl">The page URL.</param>
        /// <returns></returns>
        public tbl_WebSitePage SelectByUrl(Guid webSiteId, string pageUrl)
        {
            return _dataContext.tbl_WebSitePage.FirstOrDefault(o => o.WebSiteID == webSiteId && o.Url == pageUrl);
        }



        /// <summary>
        /// Selects the home page.
        /// </summary>
        /// <param name="webSiteId">The web site id.</param>
        /// <returns></returns>
        public tbl_WebSitePage SelectHomePage(Guid webSiteId)
        {
            return _dataContext.tbl_WebSitePage.FirstOrDefault(o => o.IsHomePage && o.WebSiteID == webSiteId);
        }



        /// <summary>
        /// Adds the specified web site page.
        /// </summary>
        /// <param name="webSitePage">The web site page.</param>
        public void Add(tbl_WebSitePage webSitePage)
        {
            webSitePage.ID = Guid.NewGuid();
            _dataContext.tbl_WebSitePage.AddObject(webSitePage);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified web site page.
        /// </summary>
        /// <param name="webSitePage">The web site page.</param>
        public void Update(tbl_WebSitePage webSitePage)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="webSitePageId">The web site page id.</param>
        public void Delete(Guid siteId, Guid webSitePageId)
        {
            var toDelete = _dataContext.tbl_WebSitePage.SingleOrDefault(o => o.ID == webSitePageId && o.tbl_WebSite.SiteID == siteId);
            if (toDelete != null)
            {
                _dataContext.DeleteObject(toDelete);
                _dataContext.SaveChanges();
            }
        }
    }
}