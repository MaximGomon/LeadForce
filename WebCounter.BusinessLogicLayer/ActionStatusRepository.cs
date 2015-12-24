using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ActionStatusRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionStatusRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ActionStatusRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ActionStatus> SelectAll()
        {
            return _dataContext.tbl_ActionStatus.ToList();
        }

    }
}