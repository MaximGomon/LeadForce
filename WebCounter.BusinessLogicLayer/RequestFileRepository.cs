using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequestFileRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestFileRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequestFileRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by request id.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <returns></returns>
        public IQueryable<tbl_RequestFile> SelectByRequestId(Guid requestId)
        {
            return _dataContext.tbl_RequestFile.Where(r => r.RequestID == requestId);
        }



        /// <summary>
        /// Adds the specified request file.
        /// </summary>
        /// <param name="requestFile">The request file.</param>
        /// <returns></returns>
        public tbl_RequestFile Add(tbl_RequestFile requestFile)
        {
            requestFile.ID = Guid.NewGuid();
            _dataContext.tbl_RequestFile.AddObject(requestFile);
            _dataContext.SaveChanges();

            return requestFile;
        }
    }
}