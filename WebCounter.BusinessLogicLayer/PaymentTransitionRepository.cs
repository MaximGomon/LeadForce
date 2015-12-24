using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentTransitionRepository
    {
        private WebCounterEntities _dataContext;        

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementTransitionRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentTransitionRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentTransition> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentTransition.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="paymentTransitionId">The payment transition id.</param>
        /// <returns></returns>
        public tbl_PaymentTransition SelectById(Guid siteId, Guid paymentTransitionId)
        {
            return
                _dataContext.tbl_PaymentTransition.SingleOrDefault(
                    o => o.SiteID == siteId && o.ID == paymentTransitionId);
        }


    }
}