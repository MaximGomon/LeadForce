using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class OrderTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public OrderTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_OrderType> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_OrderType.Where(ot => ot.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="orderTypeId">The order type id.</param>
        /// <returns></returns>
        public tbl_OrderType SelectById(Guid siteId, Guid orderTypeId)
        {
            return _dataContext.tbl_OrderType.Where(ot => ot.SiteID == siteId && ot.ID == orderTypeId).SingleOrDefault();
        }
    }
}