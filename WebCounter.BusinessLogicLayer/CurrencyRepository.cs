using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class CurrencyRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CurrencyRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Currency> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_Currency.Where(c => c.SiteID == siteId);
        }


        /// <summary>
        /// Selects the name by id.
        /// </summary>
        /// <param name="currencyId">The currency id.</param>
        /// <returns></returns>
        public string SelectNameById(Guid currencyId)
        {
            return _dataContext.tbl_Currency.Where(c => c.ID == currencyId).Select(c => c.Name).SingleOrDefault();
        }
    }
}