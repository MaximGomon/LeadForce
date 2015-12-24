using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ReadyToSellRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ReadyToSellRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Seelects all site statuses.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_ReadyToSell> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_ReadyToSell.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by score.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="score">The score.</param>
        /// <returns></returns>
        public tbl_ReadyToSell SelectByScore(Guid siteID, int score)
        {
            return _dataContext.tbl_ReadyToSell.SingleOrDefault(a => a.SiteID == siteID && (score >= a.MinScore && score <= a.MaxScore));
        }
    }
}