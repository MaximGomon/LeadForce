using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ModuleEditionActionRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleEditionActionRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ModuleEditionActionRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by module edition id.
        /// </summary>
        /// <param name="moduleEditionId">The module edition id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ModuleEditionAction> SelectByModuleEditionId(Guid moduleEditionId)
        {
            return _dataContext.tbl_ModuleEditionAction.Where(o => o.ModuleEditionID == moduleEditionId);
        }



        /// <summary>
        /// Selects the specified module id.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="action">The action.</param>
        /// <param name="accessProfileId">The access profile id.</param>
        /// <returns></returns>
        public tbl_ModuleEditionAction Select(Guid moduleId, string action, Guid? accessProfileId)
        {
            if (!accessProfileId.HasValue)
                return null;

            return
                _dataContext.tbl_ModuleEditionAction.SingleOrDefault(
                    o =>
                    o.SystemName.ToLower() == action.ToLower() && o.tbl_ModuleEdition.ModuleID == moduleId &&
                    o.tbl_ModuleEdition.tbl_AccessProfileModule.Any(
                        x => x.AccessProfileID == accessProfileId && x.ModuleID == moduleId));
        }
    }
}