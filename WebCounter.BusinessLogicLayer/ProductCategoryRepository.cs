using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ProductCategoryRepository
    {
        private WebCounterEntities _dataContext;
        protected static List<tbl_ProductCategory> hierarchyCategories = new List<tbl_ProductCategory>();
        protected static List<tbl_ProductCategory> linearCategories = new List<tbl_ProductCategory>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProductCategoryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ProductCategory> SelectAll(Guid SiteID)
        {
            return _dataContext.tbl_ProductCategory.Where(a => a.SiteID == SiteID).OrderBy(a=>a.Order).ToList();
        }



        /// <summary>
        /// Selects the category by id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public tbl_ProductCategory SelectById(Guid categoryId)
        {
            return _dataContext.tbl_ProductCategory.SingleOrDefault(pc => pc.ID == categoryId);
        }


        /// <summary>
        /// Selects the hierarchy.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ProductCategory> SelectHierarchy(Guid SiteID)
        {
            linearCategories = SelectAll(SiteID);
            hierarchyCategories = new List<tbl_ProductCategory>();
            BuildCategoryHierarchy(null);
            return hierarchyCategories;
        }



        /// <summary>
        /// Builds the category hierarchy.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        protected void BuildCategoryHierarchy(Guid? parentId)
        {
            var cat = linearCategories.Where(c => c.ParentID == parentId);
            foreach (var item in cat)
            {
                hierarchyCategories.Add(item);
                BuildCategoryHierarchy(item.ID);
            }
        }



        /// <summary>
        /// Gets the full name of the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public string GetCategoryFullName(tbl_ProductCategory category)
        {
            var result = string.Empty;

            while (category != null)
            {
                if (String.IsNullOrEmpty(result))
                    result = category.Title;
                else
                    result = category.Title + " >> " + result;

                category = category.tbl_ProductCategory2;
            }
            return result;
        }



        /// <summary>
        /// Gets the category full name by id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public string GetCategoryFullNameById(Guid categoryId)
        {
            return GetCategoryFullName(SelectById(categoryId));
        }


        /// <summary>
        /// Gets the full name of the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="delimetr">The delimetr.</param>
        /// <returns></returns>
        public string GetCategoryFullName(tbl_ProductCategory category, string delimetr)
        {
            var result = string.Empty;

            while (category != null)
            {
                if (String.IsNullOrEmpty(result))
                    result = category.Title;
                else
                    result = delimetr + result;
                category = category.tbl_ProductCategory2;
            }
            return result;
        }



        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public int GetLevel(tbl_ProductCategory category)
        {
            var level = 0;

            while (category != null)
            {
                level++;
                category = category.tbl_ProductCategory2;
            }

            return level;
        }


        /// <summary>
        /// Selects the childs.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public List<tbl_ProductCategory> SelectChilds(Guid SiteID, Guid categoryId)
        {
            linearCategories = SelectAll(SiteID);
            hierarchyCategories = new List<tbl_ProductCategory>();
            BuildCategoryHierarchy(categoryId);
            return hierarchyCategories;
        }

    }
}