using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ColumnCategoriesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnCategoriesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ColumnCategoriesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Seelects all column categories.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_ColumnCategories> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_ColumnCategories.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects column category.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="columnCategoryID">The column category ID.</param>
        /// <returns></returns>
        public tbl_ColumnCategories SelectById(Guid siteID, Guid columnCategoryID)
        {
            return _dataContext.tbl_ColumnCategories.SingleOrDefault(a => a.SiteID == siteID && a.ID == columnCategoryID);
        }
    }
}