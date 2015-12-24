using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SourceMonitoringFilterRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceMonitoringFilterRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SourceMonitoringFilterRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified source monitoring filter.
        /// </summary>
        /// <param name="sourceMonitoringFilter">The source monitoring filter.</param>
        /// <returns></returns>
        public tbl_SourceMonitoringFilter Add(tbl_SourceMonitoringFilter sourceMonitoringFilter)
        {
            sourceMonitoringFilter.ID = Guid.NewGuid();

            _dataContext.tbl_SourceMonitoringFilter.AddObject(sourceMonitoringFilter);
            _dataContext.SaveChanges();
            return sourceMonitoringFilter;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="sourceMonitoringId">The source monitoring id.</param>
        /// <returns></returns>
        public IQueryable<tbl_SourceMonitoringFilter> SelectAll(Guid siteId, Guid sourceMonitoringId)
        {
            return _dataContext.tbl_SourceMonitoringFilter.Where(a => a.SiteID == siteId && a.SourceMonitoringID == sourceMonitoringId);
        }



        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="sourceMonitoringId">The source monitoring id.</param>
        public void DeleteAll(Guid siteId, Guid sourceMonitoringId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_SourceMonitoringFilter WHERE SiteID = @SiteId AND SourceMonitoringID = @SourceMonitoringId",
                                             new SqlParameter { ParameterName = "SourceMonitoringId", Value = sourceMonitoringId },
                                             new SqlParameter { ParameterName = "SiteId", Value = siteId });
        }



        /// <summary>
        /// Adds the specified source monitoring filters.
        /// </summary>
        /// <param name="sourceMonitoringFilters">The source monitoring filters.</param>
        public void Add(List<tbl_SourceMonitoringFilter> sourceMonitoringFilters)
        {
            foreach (var sourceMonitoringFilter in sourceMonitoringFilters)
                _dataContext.tbl_SourceMonitoringFilter.AddObject(new tbl_SourceMonitoringFilter()
                                                                      {
                                                                          ID = sourceMonitoringFilter.ID,
                                                                          SiteID = sourceMonitoringFilter.SiteID,
                                                                          SourceMonitoringID = sourceMonitoringFilter.SourceMonitoringID,
                                                                          SourcePropertyID = sourceMonitoringFilter.SourcePropertyID,
                                                                          Mask = sourceMonitoringFilter.Mask,
                                                                          MonitoringActionID = sourceMonitoringFilter.MonitoringActionID
                                                                      });
            
            _dataContext.SaveChanges();            
        }



        /// <summary>
        /// Updates the specified source monitoring filter.
        /// </summary>
        /// <param name="sourceMonitoringFilter">The source monitoring filter.</param>
        public void Update(tbl_SourceMonitoringFilter sourceMonitoringFilter)
        {
            _dataContext.SaveChanges();
        }
    }
}