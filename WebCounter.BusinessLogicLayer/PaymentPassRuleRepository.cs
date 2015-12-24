using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentPassRuleRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelClientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentPassRuleRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="SiteId">The site id.</param>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public tbl_PaymentPassRule SelectById(Guid SiteId, Guid Id)
        {
            return _dataContext.tbl_PaymentPassRule.SingleOrDefault(o => o.ID == Id && o.SiteID == SiteId);
        }




        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPassRule>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentPassRule.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Updates the specified service level client.
        /// </summary>
        /// <param name="paymentPassRule">The payment pass rule.</param>
        public void Update(tbl_PaymentPassRule paymentPassRule)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified service level client.
        /// </summary>
        /// <param name="paymentPassRule">The payment pass rule.</param>
        public void Delete(tbl_PaymentPassRule paymentPassRule)
        {
            _dataContext.tbl_PaymentPassRule.DeleteObject(paymentPassRule);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified service level client.
        /// </summary>
        /// <param name="paymentPassRule">The payment pass rule.</param>
        /// <returns></returns>
        public tbl_PaymentPassRule Add(tbl_PaymentPassRule paymentPassRule)
        {
            paymentPassRule.ID = Guid.NewGuid();

            _dataContext.tbl_PaymentPassRule.AddObject(paymentPassRule);
            _dataContext.SaveChanges();

            return paymentPassRule;
        }

        public IQueryable<tbl_PaymentPassRule> SelectAll(Guid siteId, int paymentType, Guid? payerId, Guid? payerLegalAccountId, Guid? recipientId, Guid? recipientLegalAccountId)
        {
            var paymentPassRuleIds = _dataContext.tbl_PaymentPassRuleCompany.Where(o => o.SiteID == siteId && (!payerId.HasValue || o.PayerID == payerId) && (!payerLegalAccountId.HasValue || o.PayerLegalAccountID == payerLegalAccountId) && (!recipientId.HasValue || o.RecipientID == recipientId) && (!recipientLegalAccountId.HasValue || o.RecipientLegalAccountID == recipientLegalAccountId)).Select(a => a.PaymentPassRuleID);
                      return _dataContext.tbl_PaymentPassRule.Where(o => o.SiteID == siteId && (paymentPassRuleIds.Contains(o.ID)) && o.PaymentTypeID==paymentType);
        }

    }
}