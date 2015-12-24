using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class BrowsersRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsersRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public BrowsersRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified browser.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <returns></returns>
        public tbl_Browsers Add(tbl_Browsers browser)
        {
            browser.ID = Guid.NewGuid();
            _dataContext.tbl_Browsers.AddObject(browser);
            _dataContext.SaveChanges();

            return browser;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_Browsers> SelectBySiteId(Guid siteID)
        {
            return _dataContext.tbl_Browsers.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="browserId">The browser id.</param>
        /// <returns></returns>
        public tbl_Browsers SelectById(Guid siteID, Guid browserId)
        {
            return _dataContext.tbl_Browsers.SingleOrDefault(a => a.SiteID == siteID && a.ID == browserId);
        }



        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public tbl_Browsers Select(Guid siteID, string name, string version)
        {
            return _dataContext.tbl_Browsers.SingleOrDefault(a => a.SiteID == siteID && a.Name == name && a.Version == version);
        }



        /// <summary>
        /// Updates the specified browser.
        /// </summary>
        /// <param name="browser">The browser.</param>
        public void Update(tbl_Browsers browser)
        {
            _dataContext.SaveChanges();
        }
    }
}