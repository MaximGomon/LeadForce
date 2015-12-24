using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Payment> SelectAll()
        {
            return _dataContext.tbl_Payment;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Payment> SelectBySiteId(Guid siteId)
        {
            return _dataContext.tbl_Payment.Where(c => c.SiteID == siteId);
        }





        /// <summary>
        /// Selects the by  id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="paymentId">The payment id.</param>
        /// <returns></returns>
        public tbl_Payment SelectById(Guid siteId, Guid paymentId)
        {
            return _dataContext.tbl_Payment.Where(c => c.SiteID == siteId && c.ID == paymentId).SingleOrDefault();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="paymentId">The payment id.</param>
        /// <returns></returns>
        public tbl_Payment SelectById(Guid paymentId)
        {
            return _dataContext.tbl_Payment.SingleOrDefault(o => o.ID == paymentId);
        }



        public tbl_Payment Save(Guid SiteID, Guid paymentId, string Assignment,DateTime DatePlan, DateTime? DateFact,int PaymentTypeID,Guid? StatusID,Guid? PayerID,
            Guid? PayerLegalAccountID, Guid? RecipientID, Guid? RecipientLegalAccountID, Guid CurrencyID, double Course, decimal Amount, decimal Total, Guid? OrderID, Guid? InvoiceID)
        {
            var payment = _dataContext.tbl_Payment.Where(p => p.ID == paymentId).SingleOrDefault() ?? new tbl_Payment();

            payment.Assignment = Assignment;
            payment.SiteID = SiteID;
            payment.CreatedAt = DateTime.Now;
            payment.DatePlan = DatePlan;
            payment.DateFact = DateFact;
            payment.PaymentTypeID = PaymentTypeID;
            payment.StatusID = StatusID;
            payment.PayerID = PayerID;
            payment.PayerLegalAccountID = PayerLegalAccountID;
            payment.RecipientID = RecipientID;
            payment.RecipientLegalAccountID = RecipientLegalAccountID;
            payment.CurrencyID = CurrencyID;
            payment.Course = Course;
            payment.Amount = Amount;
            payment.Total = (decimal)payment.Course * payment.Amount;
            payment.OrderID = OrderID;
            payment.InvoiceID = InvoiceID;

            if (paymentId == Guid.Empty)
            {
                payment.ID = Guid.NewGuid();
                _dataContext.tbl_Payment.AddObject(payment);
            }
            _dataContext.SaveChanges();
            return payment;

        }


    }
}