using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WidgetCategoryRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetCategoryRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WidgetCategoryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="widgetCategoryId">The widget category id.</param>
        /// <returns></returns>
        public tbl_WidgetCategory SelectById(Guid widgetCategoryId)
        {
            return _dataContext.tbl_WidgetCategory.SingleOrDefault(o => o.ID == widgetCategoryId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_WidgetCategory> SelectAll()
        {
            return _dataContext.tbl_WidgetCategory.OrderBy(o => o.Title);
        }
    }
}