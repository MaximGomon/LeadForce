using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RuleTypesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleTypesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RuleTypesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_RuleTypes> SelectAll()
        {
            return _dataContext.tbl_RuleTypes.Where(a => a.ID != 2).ToList(); // !!!
        }

    }
}