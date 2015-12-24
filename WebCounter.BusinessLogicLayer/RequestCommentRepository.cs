using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequestCommentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestCommentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequestCommentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requestCommentId">The request comment id.</param>
        /// <returns></returns>
        public tbl_RequestComment SelectById(Guid siteId, Guid requestCommentId)
        {
            return _dataContext.tbl_RequestComment.SingleOrDefault(o => o.SiteID == siteId && o.ID == requestCommentId);
        }
    }
}