using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ModuleEditionRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleEditionRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ModuleEditionRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by modele id.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ModuleEdition> SelectByModuleId(Guid moduleId)
        {
            return _dataContext.tbl_ModuleEdition.Where(o => o.ModuleID == moduleId);
        }
    }
}