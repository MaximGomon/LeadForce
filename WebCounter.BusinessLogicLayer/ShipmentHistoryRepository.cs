using System;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ShipmentHistoryRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipmentHistoryRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ShipmentHistoryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified shipment history.
        /// </summary>
        /// <param name="shipmentHistory">The shipment history.</param>
        /// <returns></returns>
        public tbl_ShipmentHistory Add(tbl_ShipmentHistory shipmentHistory)
        {
            shipmentHistory.ID = Guid.NewGuid();
            shipmentHistory.CreatedAt = DateTime.Now;

            _dataContext.tbl_ShipmentHistory.AddObject(shipmentHistory);
            _dataContext.SaveChanges();

            return shipmentHistory;
        }
    }
}