using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ServiceLevelClientRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelClientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ServiceLevelClientRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="serviceLevelId">The service level id.</param>
        /// <param name="serviceLevelClientId">The service level client id.</param>
        /// <returns></returns>
        public tbl_ServiceLevelClient SelectById(Guid serviceLevelId, Guid serviceLevelClientId)
        {
            return _dataContext.tbl_ServiceLevelClient.SingleOrDefault(o => o.ServiceLevelID == serviceLevelId && o.ID == serviceLevelClientId);
        }



        /// <summary>
        /// Selects the by company id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public tbl_ServiceLevelClient SelectByCompanyId(Guid siteId, Guid companyId)
        {
            return _dataContext.tbl_ServiceLevelClient.FirstOrDefault(o => o.ClientID == companyId && o.tbl_ServiceLevel.SiteID == siteId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="serviceLevelId">The service level id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ServiceLevelClient>  SelectAll(Guid serviceLevelId)
        {
            return _dataContext.tbl_ServiceLevelClient.Where(o => o.ServiceLevelID == serviceLevelId);
        }



        /// <summary>
        /// Updates the specified service level client.
        /// </summary>
        /// <param name="serviceLevelClient">The service level client.</param>
        public void Update(tbl_ServiceLevelClient serviceLevelClient)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified service level client.
        /// </summary>
        /// <param name="serviceLevelClient">The service level client.</param>
        public void Delete(tbl_ServiceLevelClient serviceLevelClient)
        {
            _dataContext.tbl_ServiceLevelClient.DeleteObject(serviceLevelClient);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified service level client.
        /// </summary>
        /// <param name="serviceLevelClient">The service level client.</param>
        /// <returns></returns>
        public tbl_ServiceLevelClient Add(tbl_ServiceLevelClient serviceLevelClient)
        {
            serviceLevelClient.ID = Guid.NewGuid();

            _dataContext.tbl_ServiceLevelClient.AddObject(serviceLevelClient);
            _dataContext.SaveChanges();

            return serviceLevelClient;
        }
    }
}