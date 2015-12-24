using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ServiceLevelContactRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ServiceLevelContactRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Determines whether [is exist in client] [the specified service level client].
        /// </summary>
        /// <param name="serviceLevelClient">The service level client.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns>
        ///   <c>true</c> if [is exist in client] [the specified service level client]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExistInClient(Guid serviceLevelClient, Guid contactId)
        {
            return
                _dataContext.tbl_ServiceLevelContact.Any(
                    o => o.ServiceLevelClientID == serviceLevelClient && o.ContactID == contactId);
        }



        /// <summary>
        /// Selects the by contact id.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public tbl_ServiceLevelContact SelectByContactId(Guid contactId)
        {
            return _dataContext.tbl_ServiceLevelContact.FirstOrDefault(o => o.ContactID == contactId);
        }


        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="serviceLevelContactId">The service level contact id.</param>
        /// <returns></returns>
        public tbl_ServiceLevelContact SelectById(Guid serviceLevelContactId)
        {
            return _dataContext.tbl_ServiceLevelContact.FirstOrDefault(o => o.ID == serviceLevelContactId);
        }



        /// <summary>
        /// Selects the by client id.
        /// </summary>
        /// <param name="serviceLevelClientId">The service level client id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ServiceLevelContact> SelectByClientId(Guid serviceLevelClientId)
        {
            return _dataContext.tbl_ServiceLevelContact.Where(o => o.ServiceLevelClientID == serviceLevelClientId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_ServiceLevelContact> SelectAll()
        {
            return _dataContext.tbl_ServiceLevelContact;
        }
    }
}