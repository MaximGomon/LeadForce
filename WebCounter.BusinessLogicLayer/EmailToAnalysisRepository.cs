using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class EmailToAnalysisRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public EmailToAnalysisRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified email to analysis.
        /// </summary>
        /// <param name="emailToAnalysis">The email to analysis.</param>
        /// <returns></returns>
        public tbl_EmailToAnalysis Add(tbl_EmailToAnalysis emailToAnalysis)
        {
            emailToAnalysis.ID = Guid.NewGuid();
            emailToAnalysis.CreatedAt = DateTime.Now;
            _dataContext.tbl_EmailToAnalysis.AddObject(emailToAnalysis);
            _dataContext.SaveChanges();

            return emailToAnalysis;
        }



        /// <summary>
        /// Determines whether [is exist POP message] [the specified pop message id].
        /// </summary>
        /// <param name="popMessageId">The pop message id.</param>
        /// <returns>
        ///   <c>true</c> if [is exist POP message] [the specified pop message id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExistPOPMessage(string popMessageId)
        {
            return _dataContext.tbl_EmailToAnalysis.Where(sa => sa.POPMessageID == popMessageId).Select(sa => sa.POPMessageID).FirstOrDefault() != null;
        }
    }
}