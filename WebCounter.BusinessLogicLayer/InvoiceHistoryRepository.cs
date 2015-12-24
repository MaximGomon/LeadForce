using System;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class InvoiceHistoryRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceHistoryRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public InvoiceHistoryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified invoice history.
        /// </summary>
        /// <param name="invoiceHistory">The invoice history.</param>
        /// <returns></returns>
        public tbl_InvoiceHistory Add(tbl_InvoiceHistory invoiceHistory)
        {
            invoiceHistory.ID = Guid.NewGuid();
            invoiceHistory.CreatedAt = DateTime.Now;

            _dataContext.tbl_InvoiceHistory.AddObject(invoiceHistory);
            _dataContext.SaveChanges();

            return invoiceHistory;
        }
    }
}