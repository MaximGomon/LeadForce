using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class InvoiceCommentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceCommentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public InvoiceCommentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="invoiceCommentId">The invoice comment id.</param>
        /// <returns></returns>
        public tbl_InvoiceComment SelectById(Guid siteId, Guid invoiceCommentId)
        {
            return _dataContext.tbl_InvoiceComment.SingleOrDefault(o => o.SiteID == siteId && o.ID == invoiceCommentId);
        }
    }
}