using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class TaskDurationRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDurationRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public TaskDurationRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="taskDurationId">The task duration id.</param>
        /// <returns></returns>
        public tbl_TaskDuration SelectById(Guid taskDurationId)
        {
            return _dataContext.tbl_TaskDuration.Where(o => o.ID == taskDurationId).SingleOrDefault();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="responsibleId">The responsible id.</param>
        /// <returns></returns>
        public IQueryable<tbl_TaskDuration> SelectAll(Guid siteId, DateTime startDate, DateTime endDate, Guid? responsibleId)
        {
            if (startDate == endDate)
                endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            var taskDurations = _dataContext.tbl_TaskDuration.Where(td => td.tbl_Task.SiteID == siteId && td.SectionDateStart >= startDate && td.SectionDateEnd <= endDate);
            if (responsibleId.HasValue && responsibleId != Guid.Empty)
                taskDurations = taskDurations.Where(td => td.ResponsibleID == responsibleId);

            return taskDurations;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <returns></returns>
        public IQueryable<tbl_TaskDuration> SelectAll(Guid taskId)
        {
            return _dataContext.tbl_TaskDuration.Where(o => o.TaskID == taskId);
        }



        /// <summary>
        /// Adds the specified task duration.
        /// </summary>
        /// <param name="taskDuration">Duration of the task.</param>
        /// <returns></returns>
        public tbl_TaskDuration Add(tbl_TaskDuration taskDuration)
        {
            taskDuration.ID = Guid.NewGuid();
            _dataContext.tbl_TaskDuration.AddObject(taskDuration);
            _dataContext.SaveChanges();

            return taskDuration;
        }



        /// <summary>
        /// Adds the specified task durations.
        /// </summary>
        /// <param name="taskDurations">The task durations.</param>
        /// <param name="taskId">The task id.</param>
        public void Add(List<tbl_TaskDuration> taskDurations, Guid taskId)
        {
            foreach (var taskDuration in taskDurations)
                _dataContext.tbl_TaskDuration.AddObject(new tbl_TaskDuration()
                {
                    ID = taskDuration.ID,
                    TaskID = taskId,
                    SectionDateStart = taskDuration.SectionDateStart,
                    SectionDateEnd = taskDuration.SectionDateEnd,
                    ActualDurationHours = taskDuration.ActualDurationHours,
                    ActualDurationMinutes = taskDuration.ActualDurationMinutes,
                    ResponsibleID = taskDuration.ResponsibleID,
                    Comment = taskDuration.Comment
                });

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        public void DeleteAll(Guid taskId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_TaskDuration WHERE TaskID = @TaskID",
                                             new SqlParameter { ParameterName = "TaskID", Value = taskId });
        }



        /// <summary>
        /// Updates the specified task duration.
        /// </summary>
        /// <param name="taskDuration">Duration of the task.</param>
        public void Update(tbl_TaskDuration taskDuration)
        {
            _dataContext.SaveChanges();
        }
    }
}