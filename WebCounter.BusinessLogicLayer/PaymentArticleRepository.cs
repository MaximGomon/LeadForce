using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PaymentArticleRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelClientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PaymentArticleRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public tbl_PaymentArticle SelectById(Guid Id)
        {
            return _dataContext.tbl_PaymentArticle.SingleOrDefault(o => o.ID == Id);
        }




        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PaymentArticle>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PaymentArticle.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Updates the specified service level client.
        /// </summary>
        /// <param name="paymentArticle">The payment article.</param>
        public void Update(tbl_PaymentArticle paymentArticle)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified service level client.
        /// </summary>
        /// <param name="paymentArticle">The payment article.</param>
        public void Delete(tbl_PaymentArticle paymentArticle)
        {
            _dataContext.tbl_PaymentArticle.DeleteObject(paymentArticle);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified service level client.
        /// </summary>
        /// <param name="paymentArticle">The payment article.</param>
        /// <returns></returns>
        public tbl_PaymentArticle Add(tbl_PaymentArticle paymentArticle)
        {
            paymentArticle.ID = Guid.NewGuid();

            _dataContext.tbl_PaymentArticle.AddObject(paymentArticle);
            _dataContext.SaveChanges();

            return paymentArticle;
        }
    }
}