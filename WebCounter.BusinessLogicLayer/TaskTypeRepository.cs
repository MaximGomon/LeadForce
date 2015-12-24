using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class TaskTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskTypeRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public TaskTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="taskTypeId">The task type id.</param>
        /// <returns></returns>
        public tbl_TaskType SelectById(Guid siteId, Guid taskTypeId)
        {
            return _dataContext.tbl_TaskType.Where(o => o.SiteID == siteId && o.ID == taskTypeId).SingleOrDefault();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_TaskType> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_TaskType.Where(a => a.SiteID == siteId);
        }
    }
}