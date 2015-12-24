using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ResolutionsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ResolutionsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified resolution.
        /// </summary>
        /// <param name="resolution">The resolution.</param>
        /// <returns></returns>
        public tbl_Resolutions Add(tbl_Resolutions resolution)
        {
            resolution.ID = Guid.NewGuid();
            _dataContext.tbl_Resolutions.AddObject(resolution);
            _dataContext.SaveChanges();

            return resolution;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_Resolutions> SelectBySiteId(Guid siteID)
        {
            return _dataContext.tbl_Resolutions.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="resolutionId">The resolution id.</param>
        /// <returns></returns>
        public tbl_Resolutions SelectById(Guid siteID, Guid resolutionId)
        {
            return _dataContext.tbl_Resolutions.SingleOrDefault(a => a.SiteID == siteID && a.ID == resolutionId);
        }



        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public tbl_Resolutions Select(Guid siteID, string value)
        {
            return _dataContext.tbl_Resolutions.SingleOrDefault(a => a.SiteID == siteID && a.Value == value);
        }



        /// <summary>
        /// Updates the specified resolution.
        /// </summary>
        /// <param name="resolution">The resolution.</param>
        public void Update(tbl_Resolutions resolution)
        {
            _dataContext.SaveChanges();
        }
    }
}