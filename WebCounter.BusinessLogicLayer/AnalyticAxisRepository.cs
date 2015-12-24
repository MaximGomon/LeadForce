using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AnalyticAxisRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticAxisRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AnalyticAxisRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="analyticAxisId">The analytic axis id.</param>
        /// <returns></returns>
        public tbl_AnalyticAxis SelectById(Guid analyticAxisId)
        {
            return _dataContext.tbl_AnalyticAxis.SingleOrDefault(o => o.ID == analyticAxisId);
        }




        /// <summary>
        /// Selects the name of the by report and system.
        /// </summary>
        /// <param name="analyticReportId">The analytic report id.</param>
        /// <param name="systemName">Name of the system.</param>
        /// <returns></returns>
        public tbl_AnalyticAxis SelectByReportAndSystemName(Guid analyticReportId, string systemName)
        {
            return
                _dataContext.tbl_AnalyticAxis.SingleOrDefault(
                    o =>
                    o.tbl_AnalyticReportSystem.Any(
                        x => x.tbl_AnalyticReport.ID == analyticReportId && x.tbl_AnalyticAxis.SystemName == systemName));
        }


        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_AnalyticAxis> SelectAll()
        {
            return _dataContext.tbl_AnalyticAxis;
        }




        /// <summary>
        /// Selects the series by data set.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public Dictionary<string,string> SelectSeriesByDataSet(tbl_AnalyticAxis axis, Guid siteId)
        {
            var result = new Dictionary<string, string>();

            var dataSet = Regex.Replace(axis.DataSet, "\\d", string.Empty);

            using (var connection = new SqlConnection(Configuration.Settings.ADONetConnectionString))
            {
                connection.Open();

                var query = string.Empty;

                if (string.IsNullOrEmpty(axis.Query))                
                    query = string.Format(@"if exists (select * from information_schema.columns where table_name = '{0}' and column_name = 'SiteID')
	                            EXEC dbo.sp_executesql @statement = N'SELECT * FROM {0} WHERE SiteID = ''{1}'' ORDER BY Title ASC '
                              ELSE
	                            SELECT * FROM {0} ORDER BY Title ASC ", dataSet, siteId.ToString());                
                else                
                    query = axis.Query.Replace("#DataSet#", dataSet).Replace("#SiteID#", siteId.ToString());                

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["ID"] is Guid)
                                result.Add(((Guid)reader["ID"]).ToString(), (string)reader["Title"]);
                            else if (reader["ID"] is int)
                                result.Add(((int)reader["ID"]).ToString(), (string)reader["Title"]);
                            else
                                result.Add((reader["ID"]).ToString(), (string)reader["Title"]);
                        }
                    }
                }
            }

            return result;
        }
    }
}