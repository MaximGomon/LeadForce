using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class TaskHistoryRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskHistoryRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public TaskHistoryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified task history.
        /// </summary>
        /// <param name="taskHistory">The task history.</param>
        /// <returns></returns>
        public tbl_TaskHistory Add(tbl_TaskHistory taskHistory)
        {
            taskHistory.ID = Guid.NewGuid();
            taskHistory.CreatedAt = DateTime.Now;
            _dataContext.tbl_TaskHistory.AddObject(taskHistory);
            _dataContext.SaveChanges();

            return taskHistory;
        }
    }
}