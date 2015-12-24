using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class BrandRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public BrandRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Brand> SelectAll()
        {
            return _dataContext.tbl_Brand;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Brand> SelectBySiteId(Guid siteId)
        {
            return _dataContext.tbl_Brand.Where(c => c.SiteID == siteId);
        }

        
        
    }
}