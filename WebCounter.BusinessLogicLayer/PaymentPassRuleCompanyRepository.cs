using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentPassRuleCompanyRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelClientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentPassRuleCompanyRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="paymentPassRuleID">The payment pass rule ID.</param>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public tbl_PaymentPassRuleCompany SelectById(Guid paymentPassRuleID, Guid Id)
        {
            return _dataContext.tbl_PaymentPassRuleCompany.SingleOrDefault(o => o.ID == Id && o.PaymentPassRuleID == paymentPassRuleID);
        }




        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPassRuleCompany>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentPassRuleCompany.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="paymentPassRuleID">The payment pass rule ID.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPassRuleCompany> SelectAll(Guid siteId, Guid paymentPassRuleID)
        {
            return _dataContext.tbl_PaymentPassRuleCompany.Where(o => o.SiteID == siteId && o.PaymentPassRuleID == paymentPassRuleID);
        }



        /// <summary>
        /// Updates the specified service level client.
        /// </summary>
        /// <param name="paymentPassRulePass">The payment pass rule pass.</param>
        public void Update(tbl_PaymentPassRuleCompany paymentPassRulePass)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified service level client.
        /// </summary>
        /// <param name="paymentPassRulePass">The payment pass rule pass.</param>
        public void Delete(tbl_PaymentPassRuleCompany paymentPassRulePass)
        {
            _dataContext.tbl_PaymentPassRuleCompany.DeleteObject(paymentPassRulePass);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified service level client.
        /// </summary>
        /// <param name="paymentPassRulePass">The payment pass rule pass.</param>
        /// <returns></returns>
        public tbl_PaymentPassRuleCompany Add(tbl_PaymentPassRuleCompany paymentPassRulePass)
        {
            paymentPassRulePass.ID = Guid.NewGuid();
            _dataContext.tbl_PaymentPassRuleCompany.AddObject(paymentPassRulePass);
            _dataContext.SaveChanges();

            return paymentPassRulePass;
        }
    }
}