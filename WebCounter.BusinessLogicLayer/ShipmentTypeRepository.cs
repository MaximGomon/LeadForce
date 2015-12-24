using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ShipmentTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ShipmentTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ShipmentType> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_ShipmentType.Where(it => it.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="shipmentTypeId">The shipment type id.</param>
        /// <returns></returns>
        public tbl_ShipmentType SelectById(Guid siteId, Guid shipmentTypeId)
        {
            return _dataContext.tbl_ShipmentType.SingleOrDefault(it => it.SiteID == siteId && it.ID == shipmentTypeId);
        }
    }
}