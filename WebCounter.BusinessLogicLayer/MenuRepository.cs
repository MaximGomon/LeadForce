using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class MenuRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MenuRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified menu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <returns></returns>
        public tbl_Menu Add(tbl_Menu menu)
        {
            if (menu.ID == Guid.Empty)
                menu.ID = Guid.NewGuid();
            _dataContext.tbl_Menu.AddObject(menu);
            _dataContext.SaveChanges();

            return menu;
        }


        /// <summary>
        /// Deletes all by access profile ID.
        /// </summary>
        /// <param name="accessProfileID">The access profile ID.</param>
        public void DeleteAllByAccessProfileID(Guid accessProfileID)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_Menu WHERE AccessProfileID = @AccessProfileID", new SqlParameter { ParameterName = "AccessProfileID", Value = accessProfileID });
        }



        /// <summary>
        /// Deletes the by menu ID.
        /// </summary>
        /// <param name="menuID">The menu ID.</param>
        public void DeleteByMenuID(Guid menuID)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_Menu WHERE ID = @MenuID OR ParentID = @MenuID", new SqlParameter { ParameterName = "MenuID", Value = menuID });
        }



        /// <summary>
        /// Selects the by access profile ID.
        /// </summary>
        /// <param name="accessProfileID">The access profile ID.</param>
        /// <returns></returns>
        public List<tbl_Menu> SelectByAccessProfileID(Guid accessProfileID)
        {
            return _dataContext.tbl_Menu.Where(a => a.AccessProfileID == accessProfileID).OrderBy(a => a.Order).ToList();
        }


        /// <summary>
        /// Selects the by ID.
        /// </summary>
        /// <param name="menuID">The menu ID.</param>
        /// <returns></returns>
        public tbl_Menu SelectByID(Guid menuID)
        {
            return _dataContext.tbl_Menu.SingleOrDefault(a => a.ID == menuID);
        }



        /// <summary>
        /// Updates the specified menu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        public void Update(tbl_Menu menu)
        {
            _dataContext.SaveChanges();
        }
    }
}