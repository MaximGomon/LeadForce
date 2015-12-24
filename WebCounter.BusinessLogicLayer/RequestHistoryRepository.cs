using System;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequestHistoryRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHistoryRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequestHistoryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified request history.
        /// </summary>
        /// <param name="requestHistory">The request history.</param>
        /// <returns></returns>
        public tbl_RequestHistory Add(tbl_RequestHistory requestHistory)
        {
            requestHistory.ID = Guid.NewGuid();
            requestHistory.CreatedAt = DateTime.Now;

            _dataContext.tbl_RequestHistory.AddObject(requestHistory);
            _dataContext.SaveChanges();

            return requestHistory;
        }
    }
}