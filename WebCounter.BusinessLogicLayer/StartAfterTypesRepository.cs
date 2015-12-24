using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class StartAfterTypesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartAfterTypesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public StartAfterTypesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_StartAfterTypes> SelectAll()
        {
            return _dataContext.tbl_StartAfterTypes.OrderBy(a => a.Order).ToList();
        }

    }
}