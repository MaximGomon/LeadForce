using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AnalyticReportRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticReportRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AnalyticReportRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public IQueryable<tbl_AnalyticReport> SelectAll(Guid userId)
        {            
            var availableModules = Access.SelectAvailableModules(userId);
            return _dataContext.tbl_AnalyticReport.Where(o => o.ModuleID.HasValue && availableModules.Contains((Guid)o.ModuleID));
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="analyticReportId">The analytic report id.</param>
        /// <returns></returns>
        public tbl_AnalyticReport SelectById(Guid siteId, Guid analyticReportId)
        {
            return _dataContext.tbl_AnalyticReport.SingleOrDefault(o => o.ID == analyticReportId);
        }



        /// <summary>
        /// Selects the report data.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public DataTable SelectReportData(string query)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(Configuration.Settings.ADONetConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 120;
                    using (var reader = command.ExecuteReader())
                    {
                        var dar = new DataReaderAdapter();
                        dar.FillFromReader(dataTable, reader);
                    }
                }
            }

            return dataTable;
        }


        /// <summary>
        /// Adds the specified analytic report.
        /// </summary>
        /// <param name="analyticReport">The analytic report.</param>
        /// <returns></returns>
        public tbl_AnalyticReport Add(tbl_AnalyticReport analyticReport)
        {
            analyticReport.ID = Guid.NewGuid();
            _dataContext.tbl_AnalyticReport.AddObject(analyticReport);
            _dataContext.SaveChanges();

            return analyticReport;
        }



        /// <summary>
        /// Updates the specified analytic report.
        /// </summary>
        /// <param name="analyticReport">The analytic report.</param>
        public void Update(tbl_AnalyticReport analyticReport)
        {
            _dataContext.SaveChanges();
        }
    }

    public class DataReaderAdapter : System.Data.Common.DataAdapter
    {
        public int FillFromReader(DataTable dataTable, IDataReader dataReader)
        {
            return this.Fill(dataTable, dataReader);
        }
    }
}