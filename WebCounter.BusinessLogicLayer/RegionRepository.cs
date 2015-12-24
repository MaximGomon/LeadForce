using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RegionRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RegionRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Region> SelectAll()
        {
            return _dataContext.tbl_Region;
        }




        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public tbl_Region SelectById(Guid regionId)
        {
            return _dataContext.tbl_Region.SingleOrDefault(o => o.ID == regionId);
        }



        /// <summary>
        /// Selects the by country id.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Region> SelectByCountryId(Guid countryId)
        {
            return _dataContext.tbl_Region.Where(r => r.CountryID == countryId);
        }
    }
}