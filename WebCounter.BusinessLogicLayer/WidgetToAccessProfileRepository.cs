using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WidgetToAccessProfileRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetToAccessProfileRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WidgetToAccessProfileRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_WidgetToAccessProfile SelectById(Guid id)
        {
            return _dataContext.tbl_WidgetToAccessProfile.SingleOrDefault(o => o.ID == id);
        }



        /// <summary>
        /// Selects the by access profile id.
        /// </summary>
        /// <param name="accessProfileId">The access profile id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WidgetToAccessProfile> SelectByAccessProfileId(Guid accessProfileId)
        {
            return _dataContext.tbl_WidgetToAccessProfile.Where(o => o.AccessProfileID == accessProfileId);
        }



        /// <summary>
        /// Selects the by access profile and module ids.
        /// </summary>
        /// <param name="accessProfileId">The access profile id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WidgetToAccessProfile> SelectByAccessProfileAndModuleIds(Guid accessProfileId, Guid moduleId)
        {
            if (moduleId != Guid.Empty)
                return _dataContext.tbl_WidgetToAccessProfile.Where(o => o.AccessProfileID == accessProfileId && o.ModuleID == moduleId).OrderBy(o => o.Order);

            return _dataContext.tbl_WidgetToAccessProfile.Where(o => o.AccessProfileID == accessProfileId && o.ModuleID == null).OrderBy(o => o.Order);
        }


        /// <summary>
        /// Selects the by access profile and module all.
        /// </summary>
        /// <param name="accessProfileId">The access profile id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WidgetToAccessProfile> SelectByAccessProfileForAllModules(Guid accessProfileId)
        {
            return _dataContext.tbl_WidgetToAccessProfile.Where(o => o.AccessProfileID == accessProfileId && o.ModuleID == null).OrderBy(o => o.Order);
        }



        /// <summary>
        /// Adds the specified widget to access profile.
        /// </summary>
        /// <param name="widgetToAccessProfile">The widget to access profile.</param>
        /// <returns></returns>
        public tbl_WidgetToAccessProfile Add(tbl_WidgetToAccessProfile widgetToAccessProfile)
        {
            if (widgetToAccessProfile.ID == Guid.Empty)
                widgetToAccessProfile.ID = Guid.NewGuid();
            _dataContext.tbl_WidgetToAccessProfile.AddObject(widgetToAccessProfile);
            _dataContext.SaveChanges();

            return widgetToAccessProfile;
        }



        /// <summary>
        /// Updates the specified widget to access profile.
        /// </summary>
        /// <param name="widgetToAccessProfile">The widget to access profile.</param>
        public void Update(tbl_WidgetToAccessProfile widgetToAccessProfile)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified widget to access profile.
        /// </summary>
        /// <param name="widgetToAccessProfile">The widget to access profile.</param>
        public void Delete(tbl_WidgetToAccessProfile widgetToAccessProfile)
        {
            _dataContext.tbl_WidgetToAccessProfile.DeleteObject(widgetToAccessProfile);
            _dataContext.SaveChanges();
        }
    }
}