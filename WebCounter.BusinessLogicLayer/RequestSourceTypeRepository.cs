using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequestSourceTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestSourceTypeRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequestSourceTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requestSourceTypeId">The request source type id.</param>
        /// <returns></returns>
        public tbl_RequestSourceType SelectById(Guid siteId, Guid requestSourceTypeId)
        {
            return _dataContext.tbl_RequestSourceType.SingleOrDefault(o => o.SiteID == siteId && o.ID == requestSourceTypeId);
        }



        /// <summary>
        /// Selects the by source category id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requestSourceCategory">The request source category.</param>
        /// <returns></returns>
        public tbl_RequestSourceType SelectBySourceCategoryId(Guid siteId, RequestSourceCategory requestSourceCategory)
        {
            return _dataContext.tbl_RequestSourceType.FirstOrDefault(o => o.SiteID == siteId && o.RequestSourceCategoryID == (int)requestSourceCategory);
        }
    }
}