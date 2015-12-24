using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequirementSeverityOfExposureRepository
    {
        private WebCounterEntities _dataContext;
        protected static List<tbl_RequirementSeverityOfExposure> hierarchyCategories = new List<tbl_RequirementSeverityOfExposure>();
        protected static List<tbl_RequirementSeverityOfExposure> linearCategories = new List<tbl_RequirementSeverityOfExposure>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementSeverityOfExposureRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequirementSeverityOfExposureRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_RequirementSeverityOfExposure> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_RequirementSeverityOfExposure.Where(o => o.SiteID == siteId);
        }




        /// <summary>
        /// Selects the hierarchy.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requirementTypeId">The requirement type id.</param>
        /// <returns></returns>
        public List<tbl_RequirementSeverityOfExposure> SelectHierarchy(Guid siteId, Guid? requirementTypeId = null)
        {
            if (!requirementTypeId.HasValue)
                linearCategories = SelectAll(siteId).ToList();
            else
                linearCategories = SelectAll(siteId).Where(r => r.RequirementTypeID == requirementTypeId).ToList();

            hierarchyCategories = new List<tbl_RequirementSeverityOfExposure>();
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
        /// Selects the by id.
        /// </summary>
        /// <param name="requirementSeverityOfExposureId">The requirement severity of exposure id.</param>
        /// <returns></returns>
        public tbl_RequirementSeverityOfExposure SelectById(Guid requirementSeverityOfExposureId)
        {
            return _dataContext.tbl_RequirementSeverityOfExposure.SingleOrDefault(r => r.ID == requirementSeverityOfExposureId);
        }
    }
}