using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class CountryRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CountryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns></returns>
        public tbl_Country SelectByTitle(string country)
        {
            return _dataContext.tbl_Country.SingleOrDefault(c => c.Name == country);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <returns></returns>
        public tbl_Country SelectById(Guid countryId)
        {
            return _dataContext.tbl_Country.SingleOrDefault(o => o.ID == countryId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Country> SelectAll()
        {
            return _dataContext.tbl_Country;
        }
    }
}