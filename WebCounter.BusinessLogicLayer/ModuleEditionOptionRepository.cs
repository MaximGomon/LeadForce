using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ModuleEditionOptionRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleEditionOptionRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ModuleEditionOptionRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by module edition id.
        /// </summary>
        /// <param name="moduleEditionId">The module edition id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ModuleEditionOption> SelectByModuleEditionId(Guid moduleEditionId)
        {
            return _dataContext.tbl_ModuleEditionOption.Where(o => o.ModuleEditionID == moduleEditionId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="moduleEditionOptionId">The module edition option id.</param>
        /// <returns></returns>
        public tbl_ModuleEditionOption SelectById(Guid moduleEditionOptionId)
        {
            return _dataContext.tbl_ModuleEditionOption.SingleOrDefault(o => o.ID == moduleEditionOptionId);
        }
    }
}