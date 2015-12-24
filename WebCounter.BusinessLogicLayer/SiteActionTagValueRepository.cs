using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActionTagValueRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionTagValueRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActionTagValueRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Deletes the specified site action id.
        /// </summary>
        /// <param name="siteActionId">The site action id.</param>
        public void Delete(Guid siteActionId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_SiteActionTagValue WHERE SiteActionID = @SiteActionID", new SqlParameter { ParameterName = "SiteActionID", Value = siteActionId });
        }
    }
}