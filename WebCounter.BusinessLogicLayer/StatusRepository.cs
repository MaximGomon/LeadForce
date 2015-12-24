using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class StatusRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public StatusRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Seelects all site statuses.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_Status> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_Status.Where(a => a.SiteID == siteID).OrderBy(a => a.Order).ToList();
        }



        /// <summary>
        /// Selects the default.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public tbl_Status SelectDefault(Guid siteID)
        {
            return _dataContext.tbl_Status.FirstOrDefault(a => a.SiteID == siteID && a.IsDefault == true) ??
                   _dataContext.tbl_Status.FirstOrDefault(a => a.SiteID == siteID);
        }
    }
}