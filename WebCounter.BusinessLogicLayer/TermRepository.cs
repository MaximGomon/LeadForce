using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class TermRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TermRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public TermRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by code.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public tbl_Term SelectByCode(Guid siteId, string code)
        {
            return _dataContext.tbl_Term.SingleOrDefault(o => o.SiteID == siteId && o.Code == code);
        }
    }
}