using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ColumnTypesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnTypesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ColumnTypesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Seelects all column categories.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ColumnTypes> SelectAll()
        {
            return _dataContext.tbl_ColumnTypes.Where(a => true).ToList();
        }

    }
}