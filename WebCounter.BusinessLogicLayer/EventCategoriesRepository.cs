using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class EventCategoriesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCategoriesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public EventCategoriesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_EventCategories> SelectAll()
        {
            return _dataContext.tbl_EventCategories.ToList();
        }
    }
}