using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ImportFieldRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportFieldRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ImportFieldRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ImportField> SelectAll()
        {
            return _dataContext.tbl_ImportField.OrderBy(a => a.Order).ToList();
        }



        public tbl_ImportField SelectByID(Guid importFieldID)
        {
            return _dataContext.tbl_ImportField.Where(a => a.ID == importFieldID).SingleOrDefault();
        }
    }
}