using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AnalyticAxisFilterValuesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticAxisFilterValuesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AnalyticAxisFilterValuesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="analyticAxisFilterValueId">The analytic axis filter value id.</param>
        /// <returns></returns>
        public tbl_AnalyticAxisFilterValues SelectById(Guid analyticAxisFilterValueId)
        {
            return _dataContext.tbl_AnalyticAxisFilterValues.SingleOrDefault(o => o.ID == analyticAxisFilterValueId);
        }
    }
}