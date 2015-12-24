using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequirementTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementTypeRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequirementTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requirementTypeId">The requirement type id.</param>
        /// <returns></returns>
        public tbl_RequirementType SelectById(Guid siteId, Guid requirementTypeId)
        {
            return _dataContext.tbl_RequirementType.SingleOrDefault(o => o.SiteID == siteId && o.ID == requirementTypeId);
        }



        /// <summary>
        /// Selects the default.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public tbl_RequirementType SelectDefault(Guid siteId)
        {
            return _dataContext.tbl_RequirementType.FirstOrDefault(o => o.SiteID == siteId && o.IsDefault);
        }
    }
}