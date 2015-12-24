using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.API.Core
{
    public class APIAuthorization
    {        
        private readonly DataManager _dataManager = null;

        public APIAuthorization()
        {
            _dataManager = new DataManager();
        }



        /// <summary>
        /// Authorizes the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public tbl_User Authorize(Guid siteId, string userName, string password)
        {            
            return _dataManager.User.SelectByLoginPassword(siteId, userName, password);
        }
    }
}