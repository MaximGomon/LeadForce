using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SourceMonitoringRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceMonitoringRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SourceMonitoringRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified source monitoring.
        /// </summary>
        /// <param name="sourceMonitoring">The source monitoring.</param>
        /// <returns></returns>
        public tbl_SourceMonitoring Add(tbl_SourceMonitoring sourceMonitoring)
        {
            sourceMonitoring.ID = Guid.NewGuid();
            sourceMonitoring.LastReceivedAt = DateTime.UtcNow;
            _dataContext.tbl_SourceMonitoring.AddObject(sourceMonitoring);
            _dataContext.SaveChanges();
            return sourceMonitoring;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public IQueryable<tbl_SourceMonitoring> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_SourceMonitoring.Where(a => a.SiteID == siteID);
        }



        /// <summary>
        /// Selects all by status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public IQueryable<tbl_SourceMonitoring> SelectAllByStatus(Status status)
        {
            return _dataContext.tbl_SourceMonitoring.Where(a => a.StatusID == (int) status);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="sourceMonitoringId">The source monitoring id.</param>
        /// <returns></returns>
        public tbl_SourceMonitoring SelectById(Guid siteId, Guid sourceMonitoringId)
        {
            return _dataContext.tbl_SourceMonitoring.SingleOrDefault(a => a.SiteID == siteId && a.ID == sourceMonitoringId);
        }



        /// <summary>
        /// Updates the specified source monitoring.
        /// </summary>
        /// <param name="sourceMonitoring">The source monitoring.</param>
        public void Update(tbl_SourceMonitoring sourceMonitoring)
        {
            _dataContext.SaveChanges();
        }
    }
}