using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ImportColumnRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportColumnRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ImportColumnRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by import ID.
        /// </summary>
        /// <param name="importID">The import ID.</param>
        /// <returns></returns>
        public List<tbl_ImportColumn> SelectByImportID(Guid importID)
        {
            return _dataContext.tbl_ImportColumn.Where(a => a.ImportID == importID).OrderBy(a => a.Order).ToList();
        }



        /// <summary>
        /// Adds the specified import column.
        /// </summary>
        /// <param name="importColumn">The import column.</param>
        /// <returns></returns>
        public tbl_ImportColumn Add(tbl_ImportColumn importColumn)
        {
            if (importColumn.ID == Guid.Empty)
                importColumn.ID = Guid.NewGuid();
            _dataContext.tbl_ImportColumn.AddObject(importColumn);
            _dataContext.SaveChanges();

            return importColumn;
        }



        /// <summary>
        /// Deletes the by import ID.
        /// </summary>
        /// <param name="importID">The import ID.</param>
        public void DeleteByImportID(Guid importID)
        {
            var importColumns = SelectByImportID(importID);
            foreach (var importColumn in importColumns)
                _dataContext.DeleteObject(importColumn);
            _dataContext.SaveChanges();
        }
    }
}