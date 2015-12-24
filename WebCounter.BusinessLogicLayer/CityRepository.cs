using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class CityRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CityRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        public tbl_City SelectByTitle(string city)
        {
            return _dataContext.tbl_City.SingleOrDefault(c => c.Name == city);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_City> SelectAll()
        {
            return _dataContext.tbl_City;
        }



        /// <summary>
        /// Selects the by country id.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <returns></returns>
        public IQueryable<tbl_City> SelectByCountryId(Guid countryId)
        {
            return _dataContext.tbl_City.Where(c => c.CountryID == countryId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="cityId">The city id.</param>
        /// <returns></returns>
        public tbl_City SelectById(Guid cityId)
        {
            return _dataContext.tbl_City.SingleOrDefault(o => o.ID == cityId);
        }
    }
}