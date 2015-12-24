using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ModuleRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ModuleRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_Module SelectByName(string name)
        {
            return _dataContext.tbl_Module.Where(m => m.Name.ToLower() == name.ToLower()).SingleOrDefault();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_Module> SelectAll()
        {
            return _dataContext.tbl_Module.OrderBy(a => a.Title).ToList();
        }


        /// <summary>
        /// Selects the module by id.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public tbl_Module SelectById(Guid Id)
        {
            return _dataContext.tbl_Module.Where(m => m.ID == Id).SingleOrDefault();
        }

    }
}