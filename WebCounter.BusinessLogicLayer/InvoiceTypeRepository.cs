using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class InvoiceTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public InvoiceTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_InvoiceType> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_InvoiceType.Where(it => it.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="invoiceTypeId">The invoice type id.</param>
        /// <returns></returns>
        public tbl_InvoiceType SelectById(Guid siteId, Guid invoiceTypeId)
        {
            return _dataContext.tbl_InvoiceType.SingleOrDefault(it => it.SiteID == siteId && it.ID == invoiceTypeId);
        }
    }
}