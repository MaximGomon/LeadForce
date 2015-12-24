using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class MobileDevicesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileDevicesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MobileDevicesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified mobile device.
        /// </summary>
        /// <param name="mobileDevice">The mobile device.</param>
        /// <returns></returns>
        public tbl_MobileDevices Add(tbl_MobileDevices mobileDevice)
        {
            mobileDevice.ID = Guid.NewGuid();
            _dataContext.tbl_MobileDevices.AddObject(mobileDevice);
            _dataContext.SaveChanges();

            return mobileDevice;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_MobileDevices> SelectBySiteId(Guid siteID)
        {
            return _dataContext.tbl_MobileDevices.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="mobileDeviceId">The mobile device id.</param>
        /// <returns></returns>
        public tbl_MobileDevices SelectById(Guid siteID, Guid mobileDeviceId)
        {
            return _dataContext.tbl_MobileDevices.SingleOrDefault(a => a.SiteID == siteID && a.ID == mobileDeviceId);
        }



        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_MobileDevices Select(Guid siteID, string name)
        {
            return _dataContext.tbl_MobileDevices.SingleOrDefault(a => a.SiteID == siteID && a.Name == name);
        }



        /// <summary>
        /// Updates the specified mobile device.
        /// </summary>
        /// <param name="mobileDevice">The mobile device.</param>
        public void Update(tbl_MobileDevices mobileDevice)
        {
            _dataContext.SaveChanges();
        }
    }
}