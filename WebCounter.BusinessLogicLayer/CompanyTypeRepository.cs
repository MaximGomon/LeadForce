using System;
using System.Linq;
using WebCounter.DataAccessLayer;


namespace WebCounter.BusinessLogicLayer
{
    public class CompanyTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CompanyTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>        
        /// <returns></returns>
        public IQueryable<tbl_CompanyType> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_CompanyType.Where(ct => ct.SiteID == siteId);
        }




        /// <summary>
        /// Selects our company ID.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public Guid SelectOurCompanyTypeID(Guid siteId)
        {
            var ret = _dataContext.tbl_CompanyType.Where(ct => ct.SiteID == siteId && ct.Name == "Наша компания").Select(ct=>ct.ID).SingleOrDefault();
            return ret;
        }

    }
}