using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;
using System.Configuration;

namespace WebCounter.BusinessLogicLayer
{
    public class ImportRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ImportRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }




        /// <summary>
        /// Selects the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_Import SelectByID(Guid siteId, Guid id)
        {
            return _dataContext.tbl_Import.SingleOrDefault(a => a.SiteID == siteId && a.ID == id);
        }



        /// <summary>
        /// Adds the specified import.
        /// </summary>
        /// <param name="import">The import.</param>
        /// <returns></returns>
        public tbl_Import Add(tbl_Import import)
        {
            if (import.ID == Guid.Empty)
                import.ID = Guid.NewGuid();
            _dataContext.tbl_Import.AddObject(import);
            _dataContext.SaveChanges();

            return import;
        }



        /// <summary>
        /// Updates the specified import.
        /// </summary>
        /// <param name="import">The import.</param>
        public void Update(tbl_Import import)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Does the import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        public void DoImport(Guid importId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"] + ";Connect Timeout=1200"))
            {
                connection.Open();
                using (var command = new SqlCommand("Import", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.CommandTimeout = 1200;
                    command.Parameters.AddWithValue("@ImportID", importId);

                    command.ExecuteNonQuery();
                }
            }
        }



        public void DeleteByID(Guid siteId, Guid importID)
        {
            var importRecord = SelectByID(siteId, importID);
            if (importRecord != null)
            {
                _dataContext.DeleteObject(importRecord);
                _dataContext.SaveChanges();
            }
        }

    }
}