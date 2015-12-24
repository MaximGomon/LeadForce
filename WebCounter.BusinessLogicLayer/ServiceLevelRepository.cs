using System;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ServiceLevelRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLevelRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ServiceLevelRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="serviceLevelId">The service level id.</param>
        /// <returns></returns>
        public tbl_ServiceLevel SelectById(Guid siteId, Guid serviceLevelId)
        {
            return _dataContext.tbl_ServiceLevel.SingleOrDefault(o => o.SiteID == siteId && o.ID == serviceLevelId);
        }



        /// <summary>
        /// Selects the by company id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public tbl_ServiceLevel SelectByCompanyIdOrDefault(Guid siteId, Guid companyId)
        {
            var serviceLevel = SelectByCompanyId(siteId, companyId);

            if (serviceLevel != null)
                return serviceLevel;

            serviceLevel = SelectDefault(siteId);

            return serviceLevel;
        }



        /// <summary>
        /// Selects the default.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public tbl_ServiceLevel SelectDefault(Guid siteId)
        {
            return _dataContext.tbl_ServiceLevel.FirstOrDefault(o => o.SiteID == siteId && o.IsActive && o.IsDefault);            
        }



        /// <summary>
        /// Selects for contact.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        public tbl_ServiceLevel SelectForContact(Guid siteId, tbl_Contact contact)
        {
            var dataManager = new DataManager();
            
            tbl_ServiceLevel serviceLevel = null;

            if (contact.CompanyID.HasValue)
                serviceLevel = dataManager.ServiceLevel.SelectByCompanyId(siteId, (Guid)contact.CompanyID);

            if (serviceLevel == null)
            {
                var serviceLevelContact = dataManager.ServiceLevelContact.SelectByContactId(contact.ID);
                if (serviceLevelContact != null)
                    serviceLevel = serviceLevelContact.tbl_ServiceLevelClient.tbl_ServiceLevel;
                else
                {
                    serviceLevel = dataManager.ServiceLevel.SelectDefault(siteId);
                    if (serviceLevel == null)
                        return null;
                }
            }
            else
            {
                var serviceLevelClient = serviceLevel.tbl_ServiceLevelClient.FirstOrDefault(o => o.ClientID == contact.CompanyID);

                if (!dataManager.ServiceLevelContact.IsExistInClient(serviceLevelClient.ID, contact.ID))
                {
                    switch ((OutOfListServiceContacts)serviceLevelClient.OutOfListServiceContactsID)
                    {
                        case OutOfListServiceContacts.Reject:
                            return null;
                        case OutOfListServiceContacts.Default:
                            serviceLevel = dataManager.ServiceLevel.SelectDefault(siteId);
                            break;
                    }
                }
            }

            return serviceLevel;
        }



        /// <summary>
        /// Selects the by company id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public tbl_ServiceLevel SelectByCompanyId(Guid siteId, Guid companyId)
        {
            return _dataContext.tbl_ServiceLevel.FirstOrDefault(
                    o => o.SiteID == siteId && o.IsActive && o.tbl_ServiceLevelClient.Any(s => s.ClientID == companyId));
        }



        /// <summary>
        /// Updates the specified service level.
        /// </summary>
        /// <param name="serviceLevel">The service level.</param>
        public void Update(tbl_ServiceLevel serviceLevel)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified service level.
        /// </summary>
        /// <param name="serviceLevel">The service level.</param>
        /// <returns></returns>
        public tbl_ServiceLevel Add(tbl_ServiceLevel serviceLevel)
        {
            serviceLevel.ID = Guid.NewGuid();

            _dataContext.tbl_ServiceLevel.AddObject(serviceLevel);
            _dataContext.SaveChanges();

            return serviceLevel;
        }
    }
}