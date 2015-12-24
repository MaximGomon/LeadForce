using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AccessProfileModuleRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessProfileModuleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AccessProfileModuleRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by access profile ID.
        /// </summary>
        /// <param name="accessProfileID">The access profile ID.</param>
        /// <returns></returns>
        public List<tbl_AccessProfileModule> SelectByAccessProfileID(Guid accessProfileID)
        {
            return _dataContext.tbl_AccessProfileModule.Where(a => a.AccessProfileID == accessProfileID && a.tbl_Module.Name != "API").ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="accessProfileModuleID">The access profile module ID.</param>
        /// <returns></returns>
        public tbl_AccessProfileModule SelectById(Guid accessProfileModuleID)
        {
            return _dataContext.tbl_AccessProfileModule.Where(a => a.ID == accessProfileModuleID).FirstOrDefault();
        }



        /// <summary>
        /// Updates the specified access profile module.
        /// </summary>
        /// <param name="accessProfileModule">The access profile module.</param>
        public void Update(tbl_AccessProfileModule accessProfileModule)
        {
            _dataContext.SaveChanges();
        }
    }
}