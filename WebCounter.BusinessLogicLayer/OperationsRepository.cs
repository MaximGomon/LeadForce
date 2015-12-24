using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class OperationsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public OperationsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_Operations> SelectAll()
        {
            return _dataContext.tbl_Operations.OrderBy(a => a.Order).ToList();
        }

    }
}