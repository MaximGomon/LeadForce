using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class TaskRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public TaskRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="taskId">The task id.</param>
        /// <returns></returns>
        public tbl_Task SelectById(Guid siteId, Guid taskId)
        {
            return _dataContext.tbl_Task.Where(o => o.SiteID == siteId && o.ID == taskId).SingleOrDefault();
        }



        /// <summary>
        /// Adds the specified task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public tbl_Task Add(tbl_Task task)
        {
            task.ID = Guid.NewGuid();
            _dataContext.tbl_Task.AddObject(task);
            _dataContext.SaveChanges();

            return task;
        }



        /// <summary>
        /// Updates the specified task.
        /// </summary>
        /// <param name="task">The task.</param>
        public void Update(tbl_Task task)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Task> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_Task.Where(t => t.SiteID == siteId);
        }



        /// <summary>
        /// Selects the public tasks.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Task> SelectPublicTasks(Guid siteId)
        {
            return _dataContext.tbl_Task.Where(t => t.SiteID == siteId && t.tbl_TaskType.IsPublicEvent);
        }
    }
}