using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AdvertisingTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisingTypeRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AdvertisingTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public tbl_AdvertisingType SelectByTitle(string title)
        {
            return _dataContext.tbl_AdvertisingType.SingleOrDefault(o => o.Title.ToLower() == title.ToLower());
        }



        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public tbl_AdvertisingType SelectByTitle(Guid siteId, string title)
        {
            return _dataContext.tbl_AdvertisingType.SingleOrDefault(o => o.Title.ToLower() == title.ToLower() && o.SiteID == siteId);
        }



        /// <summary>
        /// Adds the specified advertising platform.
        /// </summary>
        /// <param name="advertisingType">The advertising platform.</param>
        /// <returns></returns>
        public tbl_AdvertisingType Add(tbl_AdvertisingType advertisingType)
        {
            advertisingType.ID = Guid.NewGuid();

            _dataContext.tbl_AdvertisingType.AddObject(advertisingType);

            _dataContext.SaveChanges();

            return advertisingType;
        }


        public tbl_AdvertisingType SelectByTitleAndCreate(Guid siteId, string title)
        {
            var advertisingType = SelectByTitle(siteId, title);
            if (advertisingType == null)
            {
                advertisingType = new tbl_AdvertisingType {SiteID = siteId, AdvertisingTypeCategoryID = 1};
                advertisingType.Title = advertisingType.Code = title;
                Add(advertisingType);
            }
            return advertisingType;
        }
    }
}