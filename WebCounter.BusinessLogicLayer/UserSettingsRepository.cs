using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class UserSettingsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UserSettingsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the name of the by class.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="className">Name of the class.</param>
        /// <returns></returns>
        public tbl_UserSettings SelectByClassName(Guid userId, string className)
        {
            return _dataContext.tbl_UserSettings.Where(us => us.UserID == userId && us.ClassName.ToLower() == className.ToLower()).SingleOrDefault();
        }



        /// <summary>
        /// Saves the specified user settings.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        public void Save(tbl_UserSettings userSettings)
        {
            var toUpdate = _dataContext.tbl_UserSettings.Where(us => us.ID == userSettings.ID).SingleOrDefault() ?? new tbl_UserSettings();
            toUpdate.UserID = userSettings.UserID;
            toUpdate.ClassName = userSettings.ClassName;
            toUpdate.UserSettings = userSettings.UserSettings;
            toUpdate.ShowGroupPanel = userSettings.ShowGroupPanel;
            toUpdate.ShowFilterPanel = userSettings.ShowFilterPanel;
            toUpdate.ShowAlternativeControl = userSettings.ShowAlternativeControl;

            if (toUpdate.ID == Guid.Empty)
            {
                toUpdate.ID = Guid.NewGuid();
                _dataContext.tbl_UserSettings.AddObject(toUpdate);
            }

            _dataContext.SaveChanges();
        }
    }
}