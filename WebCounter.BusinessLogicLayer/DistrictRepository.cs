using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class DistrictRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public DistrictRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_District> SelectAll()
        {
            return _dataContext.tbl_District;
        }



        /// <summary>
        /// Selects the by country id.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <returns></returns>
        public IQueryable<tbl_District> SelectByCountryId(Guid countryId)
        {
            return _dataContext.tbl_District.Where(d => d.CountryID == countryId);
        }


        public IQueryable<tbl_District> SelectByCountryAndRegionId(Guid? countryId, Guid? regionId)
        {
            var districts = _dataContext.tbl_District.Where(d => d.ID != Guid.Empty);
            if (countryId.HasValue)
                districts = districts.Where(d => d.CountryID == countryId);
            if (countryId.HasValue && regionId.HasValue)
                districts = districts.Where(d => d.CountryID == countryId && d.RegionID == regionId);

            return districts;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns></returns>
        public tbl_District SelectById(Guid districtId)
        {
            return _dataContext.tbl_District.SingleOrDefault(o => o.ID == districtId);
        }
    }
}