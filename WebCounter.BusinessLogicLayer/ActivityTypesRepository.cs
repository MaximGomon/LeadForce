using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ActivityTypesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTypesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ActivityTypesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ActivityTypes> SelectAll()
        {
            return _dataContext.tbl_ActivityTypes.Where(at => at.ID != (int)ActivityType.OpenLandingPage).ToList();
        }

    }
}