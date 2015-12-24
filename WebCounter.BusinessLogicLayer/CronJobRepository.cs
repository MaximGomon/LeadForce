using System;
using System.Data.Objects;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations.CronJob;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class CronJobRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CronJobRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CronJobRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_CronJob> SelectAll()
        {
            var dateTimeNow = DateTime.Now;

            return _dataContext.tbl_CronJob.Where(o => (!o.NextRunPlannedAt.HasValue || dateTimeNow > o.NextRunPlannedAt || o.LastRunStatusID == (int) LastRunStatus.Error));
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="cronJobId">The cron job id.</param>
        /// <returns></returns>
        public tbl_CronJob SelectById(Guid cronJobId)
        {
            return _dataContext.tbl_CronJob.SingleOrDefault(o => o.ID == cronJobId);
        }



        /// <summary>
        /// Updates the specified cron job.
        /// </summary>
        /// <param name="cronJob">The cron job.</param>
        public void Update(tbl_CronJob cronJob)
        {
            _dataContext.SaveChanges();
        }
    }
}