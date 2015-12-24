using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class TaskPersonalCommentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public TaskPersonalCommentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by task contact id.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public tbl_TaskPersonalComment SelectByTaskContactId(Guid taskId, Guid contactId)
        {
            return _dataContext.tbl_TaskPersonalComment.Where(tpc => tpc.TaskID == taskId && tpc.ContactID == contactId).SingleOrDefault();
        }



        /// <summary>
        /// Adds the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public tbl_TaskPersonalComment Add(tbl_TaskPersonalComment comment)
        {
            comment.ID = Guid.NewGuid();
            _dataContext.tbl_TaskPersonalComment.AddObject(comment);
            _dataContext.SaveChanges();

            return comment;
        }



        /// <summary>
        /// Updates the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void Update(tbl_TaskPersonalComment comment)
        {
            _dataContext.SaveChanges();
        }
    }
}