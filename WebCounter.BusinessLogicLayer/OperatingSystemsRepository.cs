using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class OperatingSystemsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatingSystemsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public OperatingSystemsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified operating system.
        /// </summary>
        /// <param name="operatingSystem">The operating system.</param>
        /// <returns></returns>
        public tbl_OperatingSystems Add(tbl_OperatingSystems operatingSystem)
        {
            operatingSystem.ID = Guid.NewGuid();
            _dataContext.tbl_OperatingSystems.AddObject(operatingSystem);
            _dataContext.SaveChanges();

            return operatingSystem;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_OperatingSystems> SelectBySiteId(Guid siteID)
        {
            return _dataContext.tbl_OperatingSystems.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="operatingSystemId">The operating system id.</param>
        /// <returns></returns>
        public tbl_OperatingSystems SelectById(Guid siteID, Guid operatingSystemId)
        {
            return _dataContext.tbl_OperatingSystems.SingleOrDefault(a => a.SiteID == siteID && a.ID == operatingSystemId);
        }



        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public tbl_OperatingSystems Select(Guid siteID, string name, string version)
        {
            return _dataContext.tbl_OperatingSystems.SingleOrDefault(a => a.SiteID == siteID && a.Name == name && a.Version == version);
        }



        /// <summary>
        /// Updates the specified operating system.
        /// </summary>
        /// <param name="operatingSystem">The operating system.</param>
        public void Update(tbl_OperatingSystems operatingSystem)
        {
            _dataContext.SaveChanges();
        }
    }
}