using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ActionTypesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionTypesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ActionTypesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ActionTypes> SelectAll()
        {
            return _dataContext.tbl_ActionTypes.ToList();
        }

    }
}