using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentStatusRepository
    {
        private WebCounterEntities _dataContext;        

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementStatusRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentStatusRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="paymentStatusId">The payment status id.</param>
        /// <returns></returns>
        public tbl_PaymentStatus SelectById(Guid siteId, Guid paymentStatusId)
        {
            return _dataContext.tbl_PaymentStatus.SingleOrDefault(rs => rs.SiteID == siteId && rs.ID == paymentStatusId);
        }



        /// <summary>
        /// Selects the default.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public tbl_PaymentStatus SelectDefault(Guid siteId)
        {
            return _dataContext.tbl_PaymentStatus.FirstOrDefault(rs => rs.SiteID == siteId && rs.IsDefault);
        }



        



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentStatus> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentStatus.Where(o => o.SiteID == siteId);
        }
    }
}