using System;
using System.Linq;
using WebCounter.DataAccessLayer;


namespace WebCounter.BusinessLogicLayer
{
    public class CompanySizeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CompanySizeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>        
        /// <returns></returns>
        public IQueryable<tbl_CompanySize> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_CompanySize.Where(cs => cs.SiteID == siteId);
        }
    }
}