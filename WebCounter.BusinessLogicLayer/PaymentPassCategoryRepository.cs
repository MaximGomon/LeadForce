using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentPassCategoryRepository
    {
        private WebCounterEntities _dataContext;        

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementStatusRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentPassCategoryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="paymentPassCategoryId">The payment pass category id.</param>
        /// <returns></returns>
        public tbl_PaymentPassCategory SelectById(Guid paymentPassCategoryId)
        {
            return _dataContext.tbl_PaymentPassCategory.SingleOrDefault(rs =>  rs.ID == paymentPassCategoryId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPassCategory> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentPassCategory.Where(o => o.SiteID == siteId);
        }
    }
}