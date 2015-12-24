using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class LogicConditionsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicConditionsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public LogicConditionsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_LogicConditions> SelectAll()
        {
            return _dataContext.tbl_LogicConditions.ToList();
        }

    }
}