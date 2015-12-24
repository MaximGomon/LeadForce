using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequirementTransitionRepository
    {
        private WebCounterEntities _dataContext;        

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementTransitionRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequirementTransitionRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_RequirementTransition> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_RequirementTransition.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requirementTransitionId">The requirement transition id.</param>
        /// <returns></returns>
        public tbl_RequirementTransition SelectById(Guid siteId, Guid requirementTransitionId)
        {
            return
                _dataContext.tbl_RequirementTransition.SingleOrDefault(
                    o => o.SiteID == siteId && o.ID == requirementTransitionId);
        }



        /// <summary>
        /// Selects all available for portal.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_RequirementTransition> SelectAllAvailableForPortal(Guid siteId)
        {
            return SelectAll(siteId).Where(o => o.IsPortalAllowed);
        }
    }
}