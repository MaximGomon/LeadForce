using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentPassRulePassRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelClientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentPassRulePassRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="paymentPassRuleID">The payment pass rule ID.</param>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public tbl_PaymentPassRulePass SelectById(Guid paymentPassRuleID, Guid Id)
        {
            return _dataContext.tbl_PaymentPassRulePass.SingleOrDefault(o => o.ID == Id && o.PaymentPassRuleID == paymentPassRuleID);
        }




        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPassRulePass>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentPassRulePass.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="paymentPassRuleID">The payment pass rule ID.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPassRulePass> SelectAll(Guid siteId, Guid paymentPassRuleID)
        {
            return _dataContext.tbl_PaymentPassRulePass.Where(o => o.SiteID == siteId && o.PaymentPassRuleID == paymentPassRuleID);
        }



        /// <summary>
        /// Updates the specified service level client.
        /// </summary>
        /// <param name="paymentPassRulePass">The payment pass rule pass.</param>
        public void Update(tbl_PaymentPassRulePass paymentPassRulePass)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified service level client.
        /// </summary>
        /// <param name="paymentPassRulePass">The payment pass rule pass.</param>
        public void Delete(tbl_PaymentPassRulePass paymentPassRulePass)
        {
            _dataContext.tbl_PaymentPassRulePass.DeleteObject(paymentPassRulePass);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified service level client.
        /// </summary>
        /// <param name="paymentPassRulePass">The payment pass rule pass.</param>
        /// <returns></returns>
        public tbl_PaymentPassRulePass Add(tbl_PaymentPassRulePass paymentPassRulePass)
        {
            paymentPassRulePass.ID = Guid.NewGuid();
            _dataContext.tbl_PaymentPassRulePass.AddObject(paymentPassRulePass);
            _dataContext.SaveChanges();

            return paymentPassRulePass;
        }



        /// <summary>
        /// Selects the by rule id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="paymentPassRuleId">The payment pass rule id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPassRulePass> SelectByRuleId(Guid siteId, Guid paymentPassRuleId)
        {
            return _dataContext.tbl_PaymentPassRulePass.Where(o => o.SiteID == siteId && o.PaymentPassRuleID == paymentPassRuleId);

        }
    }
}