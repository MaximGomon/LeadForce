using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Analytic;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AnalyticReportSystemRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticReportSystemRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AnalyticReportSystemRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by analytic report id.
        /// </summary>
        /// <param name="analyticReportId">The analytic report id.</param>
        /// <returns></returns>
        public IQueryable<tbl_AnalyticReportSystem> SelectByAnalyticReportId(Guid analyticReportId)
        {
            return _dataContext.tbl_AnalyticReportSystem.Where(o => o.AnalyticReportID == analyticReportId);
        }



        /// <summary>
        /// Selects the by analytic report id.
        /// </summary>
        /// <param name="analyticReportId">The analytic report id.</param>
        /// <returns></returns>
        public IQueryable<tbl_AnalyticReportSystem> SelectFiltersByAnalyticReportId(Guid analyticReportId)
        {
            return _dataContext.tbl_AnalyticReportSystem.Where(o => o.AnalyticReportID == analyticReportId && (o.tbl_AnalyticAxis.AxisRoleID == (int)AxisRole.IsFilter || o.tbl_AnalyticAxis.AxisRoleID == (int)AxisRole.IsFilterAndLegend));
        }



        /// <summary>
        /// Selects the axis values by analytic report id.
        /// </summary>
        /// <param name="analyticReportId">The analytic report id.</param>
        /// <returns></returns>
        public IQueryable<tbl_AnalyticReportSystem> SelectAxisValuesByAnalyticReportId(Guid analyticReportId)
        {
            return _dataContext.tbl_AnalyticReportSystem.Where(o => o.AnalyticReportID == analyticReportId && o.tbl_AnalyticAxis.AxisRoleID == (int)AxisRole.Value);
        }



        /// <summary>
        /// Selects the X axis by analytic report id.
        /// </summary>
        /// <param name="analyticReportId">The analytic report id.</param>
        /// <returns></returns>
        public IQueryable<tbl_AnalyticReportSystem> SelectXAxisByAnalyticReportId(Guid analyticReportId)
        {
            return _dataContext.tbl_AnalyticReportSystem.Where(o => o.AnalyticReportID == analyticReportId && o.AxisTypeID == (int)AxisType.XAxis);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="analyticReportSystemId">The analytic report system id.</param>
        /// <returns></returns>
        public tbl_AnalyticReportSystem SelectById(Guid analyticReportSystemId)
        {
            return _dataContext.tbl_AnalyticReportSystem.SingleOrDefault(o => o.ID == analyticReportSystemId);
        }
    }
}