using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class OrderRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public OrderRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="orderId">The order id.</param>
        /// <returns></returns>
        public tbl_Order SelectById(Guid siteId, Guid orderId)
        {
            return _dataContext.tbl_Order.SingleOrDefault(o => o.SiteID == siteId && o.ID == orderId);
        }



        /// <summary>
        /// Adds the specified order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public tbl_Order Add(tbl_Order order)
        {
            order.ID = Guid.NewGuid();
            _dataContext.tbl_Order.AddObject(order);
            _dataContext.SaveChanges();

            return order;
        }



        /// <summary>
        /// Updates the specified order.
        /// </summary>
        /// <param name="order">The order.</param>
        public void Update(tbl_Order order)
        {
            _dataContext.SaveChanges();
        }
    }
}