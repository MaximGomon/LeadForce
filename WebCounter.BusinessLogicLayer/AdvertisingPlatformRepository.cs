using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AdvertisingPlatformRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisingPlatformRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AdvertisingPlatformRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public tbl_AdvertisingPlatform SelectByTitle(string title)
        {
            return _dataContext.tbl_AdvertisingPlatform.SingleOrDefault(o => o.Title.ToLower() == title.ToLower());
        }



        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public tbl_AdvertisingPlatform SelectByTitle(Guid siteId, string title)
        {
            return _dataContext.tbl_AdvertisingPlatform.SingleOrDefault(o => o.Title.ToLower() == title.ToLower() && o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_AdvertisingPlatform SelectById(Guid id)
        {
            return _dataContext.tbl_AdvertisingPlatform.SingleOrDefault(o => o.ID == id);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_AdvertisingPlatform> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_AdvertisingPlatform.Where(o => o.SiteID == siteId);
        }


        /// <summary>
        /// Adds the specified advertising platform.
        /// </summary>
        /// <param name="advertisingPlatform">The advertising platform.</param>
        /// <returns></returns>
        public tbl_AdvertisingPlatform Add(tbl_AdvertisingPlatform advertisingPlatform)
        {
            advertisingPlatform.ID = Guid.NewGuid();

            _dataContext.tbl_AdvertisingPlatform.AddObject(advertisingPlatform);

            _dataContext.SaveChanges();

            return advertisingPlatform;
        }



        /// <summary>
        /// Selects the by title and create.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public tbl_AdvertisingPlatform SelectByTitleAndCreate(Guid siteId, string title)
        {
            var advertisingPlatform = SelectByTitle(siteId, title);
            if (advertisingPlatform == null)
            {
                advertisingPlatform = new tbl_AdvertisingPlatform {SiteID = siteId};
                advertisingPlatform.Title = advertisingPlatform.Code = title;
                Add(advertisingPlatform);
            }
            return advertisingPlatform;
        }
    }
}