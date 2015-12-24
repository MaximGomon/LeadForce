using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class UnitRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UnitRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Unit> SelectAll()
        {
            return _dataContext.tbl_Unit;
        }


        /// <summary>
        /// Selects the name by id.
        /// </summary>
        /// <param name="unitId">The unit id.</param>
        /// <returns></returns>
        public string SelectNameById(Guid unitId)
        {
            return _dataContext.tbl_Unit.Where(u => u.ID == unitId).Select(u => u.Title).SingleOrDefault();
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Unit> SelectBySiteId(Guid siteId)
        {
            return _dataContext.tbl_Unit.Where(c => c.SiteID == siteId);
        }

    }
}