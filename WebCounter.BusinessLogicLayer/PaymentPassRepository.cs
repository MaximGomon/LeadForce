using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentPassRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelClientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentPassRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="paymentID">The payment ID.</param>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public tbl_PaymentPass SelectById(Guid paymentID, Guid Id)
        {
            return _dataContext.tbl_PaymentPass.SingleOrDefault(o => o.ID == Id && o.PaymentID == paymentID);
        }




        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPass>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentPass.Where(o => o.SiteID == siteId);
        }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPass> SelectAll()
        {
            return _dataContext.tbl_PaymentPass;
        }


        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="paymentId">The payment id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentPass> SelectAll(Guid siteId, Guid paymentId)
        {
            return _dataContext.tbl_PaymentPass.Where(o => o.SiteID == siteId && o.PaymentID == paymentId);
        }


        /// <summary>
        /// Updates the specified service level client.
        /// </summary>
        /// <param name="paymentPass">The payment pass.</param>
        public void Update(tbl_PaymentPass paymentPass)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified service level client.
        /// </summary>
        /// <param name="paymentPass">The payment pass.</param>
        public void Delete(tbl_PaymentPass paymentPass)
        {
            _dataContext.tbl_PaymentPass.DeleteObject(paymentPass);
            _dataContext.SaveChanges();
        }




        /// <summary>
        /// Adds the specified service level client.
        /// </summary>
        /// <param name="paymentPass">The payment pass.</param>
        /// <returns></returns>
        public tbl_PaymentPass Add(tbl_PaymentPass paymentPass)
        {
            paymentPass.ID = Guid.NewGuid();
            _dataContext.tbl_PaymentPass.AddObject(paymentPass);
            _dataContext.SaveChanges();

            return paymentPass;
        }

    }
}