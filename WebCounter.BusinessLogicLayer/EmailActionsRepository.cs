using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class EmailActionsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public EmailActionsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }


        
        /// <summary>
        /// Seelects all email actions.
        /// </summary>
        /// <returns></returns>
        public List<tbl_EmailActions> SelectAll()
        {
            return _dataContext.tbl_EmailActions.Where(a => true).ToList();
        }
    }
}