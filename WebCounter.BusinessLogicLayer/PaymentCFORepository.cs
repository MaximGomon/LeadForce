using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentCFORepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelClientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentCFORepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public tbl_PaymentCFO SelectById(Guid Id)
        {
            return _dataContext.tbl_PaymentCFO.SingleOrDefault(o => o.ID == Id);
        }




        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentCFO>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentCFO.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Updates the specified service level client.
        /// </summary>
        /// <param name="paymentCFO">The payment CFO.</param>
        public void Update(tbl_PaymentCFO paymentCFO)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified service level client.
        /// </summary>
        /// <param name="paymentCFO">The payment CFO.</param>
        public void Delete(tbl_PaymentCFO paymentCFO)
        {
            _dataContext.tbl_PaymentCFO.DeleteObject(paymentCFO);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified service level client.
        /// </summary>
        /// <param name="paymentCFO">The payment CFO.</param>
        /// <returns></returns>
        public tbl_PaymentCFO Add(tbl_PaymentCFO paymentCFO)
        {
            paymentCFO.ID = Guid.NewGuid();

            _dataContext.tbl_PaymentCFO.AddObject(paymentCFO);
            _dataContext.SaveChanges();

            return paymentCFO;
        }
    }
}