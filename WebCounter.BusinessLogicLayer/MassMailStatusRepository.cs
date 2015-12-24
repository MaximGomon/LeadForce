using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class MassMailStatusRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassMailStatusRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MassMailStatusRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_MassMailStatus> SelectAll()
        {
            return _dataContext.tbl_MassMailStatus.ToList();
        }
    }
}