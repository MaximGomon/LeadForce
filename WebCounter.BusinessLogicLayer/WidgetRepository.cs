using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WidgetRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WidgetRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="widgetId">The widget id.</param>
        /// <returns></returns>
        public tbl_Widget SelectById(Guid widgetId)
        {
            return _dataContext.tbl_Widget.SingleOrDefault(o => o.ID == widgetId);
        }
        


        /// <summary>
        /// Selects the by category id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Widget> SelectByCategoryId(Guid categoryId)
        {
            return _dataContext.tbl_Widget.Where(o => o.WidgetCategoryID == categoryId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Widget> SelectAll()
        {
            return _dataContext.tbl_Widget;
        }
    }
}