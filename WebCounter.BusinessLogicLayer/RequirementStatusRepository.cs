using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequirementStatusRepository
    {
        private WebCounterEntities _dataContext;        

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementStatusRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequirementStatusRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requirementStatusId">The requirement status id.</param>
        /// <returns></returns>
        public tbl_RequirementStatus SelectById(Guid siteId, Guid requirementStatusId)
        {
            return _dataContext.tbl_RequirementStatus.SingleOrDefault(rs => rs.SiteID == siteId && rs.ID == requirementStatusId);
        }



        /// <summary>
        /// Selects the default.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public tbl_RequirementStatus SelectDefault(Guid siteId)
        {
            return _dataContext.tbl_RequirementStatus.FirstOrDefault(rs => rs.SiteID == siteId && rs.IsDefault);
        }



        /// <summary>
        /// Selects the responsible id.
        /// </summary>
        /// <param name="requirementStatus">The requirement status.</param>
        /// <param name="serviceLevel">The service level.</param>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public Guid SelectResponsibleId(tbl_RequirementStatus requirementStatus, tbl_ServiceLevel serviceLevel, Guid? companyId)
        {
            if (requirementStatus.ServiceLevelRoleID.HasValue)
            {
                var serviceLevelClient =
                    serviceLevel.tbl_ServiceLevelClient.FirstOrDefault(
                        o => o.ClientID == companyId);

                if (serviceLevelClient != null)
                {
                    var serviceLevelContact =
                        serviceLevelClient.tbl_ServiceLevelContact.FirstOrDefault(
                            o => o.ServiceLevelRoleID == requirementStatus.ServiceLevelRoleID);
                    if (serviceLevelContact != null)
                    {
                        return serviceLevelContact.ContactID;
                    }
                }
            }

            return Guid.Empty;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_RequirementStatus> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_RequirementStatus.Where(o => o.SiteID == siteId);
        }
    }
}