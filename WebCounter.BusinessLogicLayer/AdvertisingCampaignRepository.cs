using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AdvertisingCampaignRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisingCampaignRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AdvertisingCampaignRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public tbl_AdvertisingCampaign SelectByTitle(string title)
        {
            return _dataContext.tbl_AdvertisingCampaign.SingleOrDefault(o => o.Title.ToLower() == title.ToLower());
        }


        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public tbl_AdvertisingCampaign SelectByTitle(Guid siteId, string title)
        {
            return _dataContext.tbl_AdvertisingCampaign.SingleOrDefault(o => o.Title.ToLower() == title.ToLower() && o.SiteID == siteId);
        }



        /// <summary>
        /// Adds the specified advertising platform.
        /// </summary>
        /// <param name="advertisingCampaign">The advertising campaign.</param>
        /// <returns></returns>
        public tbl_AdvertisingCampaign Add(tbl_AdvertisingCampaign advertisingCampaign)
        {
            advertisingCampaign.ID = Guid.NewGuid();

            _dataContext.tbl_AdvertisingCampaign.AddObject(advertisingCampaign);

            _dataContext.SaveChanges();

            return advertisingCampaign;
        }


        public tbl_AdvertisingCampaign SelectByTitleAndCreate(Guid siteId, string title)
        {
            var advertisingCampaign = SelectByTitle(siteId, title);
            if (advertisingCampaign == null)
            {
                advertisingCampaign = new tbl_AdvertisingCampaign { SiteID = siteId };
                advertisingCampaign.Title = advertisingCampaign.Code = title;
                Add(advertisingCampaign);
            }
            return advertisingCampaign;
        }
    }
}