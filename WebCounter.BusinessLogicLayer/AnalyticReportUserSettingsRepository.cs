using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AnalyticReportUserSettingsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticReportUserSettingsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AnalyticReportUserSettingsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by analytic report id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="analyticReportId">The analytic report id.</param>
        /// <returns></returns>
        public tbl_AnalyticReportUserSettings SelectByAnalyticReportId(Guid userId, Guid analyticReportId)
        {
            return _dataContext.tbl_AnalyticReportUserSettings.SingleOrDefault(o => o.AnalyticReportID == analyticReportId);
        }



        /// <summary>
        /// Adds the specified analytic report user settings.
        /// </summary>
        /// <param name="analyticReportUserSettings">The analytic report user settings.</param>
        /// <returns></returns>
        public tbl_AnalyticReportUserSettings Add(tbl_AnalyticReportUserSettings analyticReportUserSettings)
        {
            analyticReportUserSettings.ID = Guid.NewGuid();
            _dataContext.tbl_AnalyticReportUserSettings.AddObject(analyticReportUserSettings);
            _dataContext.SaveChanges();

            return analyticReportUserSettings;
        }



        /// <summary>
        /// Updates the specified analytic report user settings.
        /// </summary>
        /// <param name="analyticReportUserSettings">The analytic report user settings.</param>
        public void Update(tbl_AnalyticReportUserSettings analyticReportUserSettings)
        {
            _dataContext.SaveChanges();
        }
    }
}