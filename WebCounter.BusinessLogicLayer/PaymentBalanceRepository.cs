using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentBalanceRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelClientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentBalanceRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public tbl_PaymentBalance SelectById(Guid siteID, Guid Id)
        {
            return _dataContext.tbl_PaymentBalance.SingleOrDefault(o => o.ID == Id && o.SiteID == siteID);
        }




        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentBalance>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentBalance.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="paymentPassCategoryID">The payment pass category ID.</param>
        /// <param name="CFOID">The CFOID.</param>
        /// <param name="paymentArticleId">The payment article id.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentBalance> SelectByDate(Guid siteId, Guid? paymentPassCategoryID, Guid? CFOID, Guid? paymentArticleId, DateTime? CreatedAt)
        {
            return _dataContext.tbl_PaymentBalance.Where(o => o.SiteID == siteId && o.PaymentPassCategoryID == paymentPassCategoryID && o.CFOID == CFOID && o.PaymentArticleID == paymentArticleId && o.Date >= CreatedAt);
        }


        /// <summary>
        /// Updates the specified service level client.
        /// </summary>
        /// <param name="paymentPass">The payment pass.</param>
        public void Update(tbl_PaymentBalance paymentPass)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified service level client.
        /// </summary>
        /// <param name="paymentPass">The payment pass.</param>
        public void Delete(tbl_PaymentBalance paymentPass)
        {
            _dataContext.tbl_PaymentBalance.DeleteObject(paymentPass);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified service level client.
        /// </summary>
        /// <param name="paymentPass">The payment pass.</param>
        /// <returns></returns>
        public tbl_PaymentBalance Add(tbl_PaymentBalance paymentPass)
        {
            paymentPass.ID = Guid.NewGuid();
            _dataContext.tbl_PaymentBalance.AddObject(paymentPass);
            _dataContext.SaveChanges();

            return paymentPass;
        }

    }
}