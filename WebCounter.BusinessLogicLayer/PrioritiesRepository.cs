using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PrioritiesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrioritiesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PrioritiesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_Priorities> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_Priorities.Where(a => a.SiteID == siteID).ToList();
        }


        /// <summary>
        /// Selects the by score.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="score">The score.</param>
        /// <returns></returns>
        public tbl_Priorities SelectByScore(Guid siteID, int score)
        {
            return _dataContext.tbl_Priorities.SingleOrDefault(a => a.SiteID == siteID && (score >= a.MinScore && score <= a.MaxScore));
        }
    }
}