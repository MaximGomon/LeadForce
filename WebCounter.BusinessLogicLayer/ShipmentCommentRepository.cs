using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ShipmentCommentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipmentCommentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ShipmentCommentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="shipmentCommentId">The shipment comment id.</param>
        /// <returns></returns>
        public tbl_ShipmentComment SelectById(Guid siteId, Guid shipmentCommentId)
        {
            return _dataContext.tbl_ShipmentComment.SingleOrDefault(o => o.SiteID == siteId && o.ID == shipmentCommentId);
        }
    }
}