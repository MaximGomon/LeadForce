using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActivityScoreTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActivityScoreTypeRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActivityScoreTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all site activity score type.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActivityScoreType> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_SiteActivityScoreType.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="siteActivityScoreTypeId">The site activity score type id.</param>
        /// <returns></returns>
        public tbl_SiteActivityScoreType SelectById(Guid siteID, Guid siteActivityScoreTypeId)
        {
            return _dataContext.tbl_SiteActivityScoreType.SingleOrDefault(a => a.SiteID == siteID && a.ID == siteActivityScoreTypeId);
        }
    }
}