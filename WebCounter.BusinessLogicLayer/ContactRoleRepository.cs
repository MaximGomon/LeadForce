using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ContactRoleRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactRoleRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_ContactRole> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_ContactRole.Where(a => a.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="contactRoleId">The contact role id.</param>
        /// <returns></returns>
        public tbl_ContactRole SelectById(Guid contactRoleId)
        {
            return _dataContext.tbl_ContactRole.SingleOrDefault(a => a.ID == contactRoleId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactRoleId">The contact role id.</param>
        /// <returns></returns>
        public tbl_ContactRole SelectById(Guid siteId, Guid contactRoleId)
        {
            return _dataContext.tbl_ContactRole.SingleOrDefault(a => a.SiteID == siteId && a.ID == contactRoleId);
        }



        /// <summary>
        /// Adds the specified contact role.
        /// </summary>
        /// <param name="contactRole">The contact role.</param>
        /// <returns></returns>
        public tbl_ContactRole Add(tbl_ContactRole contactRole)
        {
            if (contactRole.ID == Guid.Empty)
                contactRole.ID = Guid.NewGuid();
            _dataContext.tbl_ContactRole.AddObject(contactRole);
            _dataContext.SaveChanges();

            return contactRole;
        }



        /// <summary>
        /// Updates the specified contact role.
        /// </summary>
        /// <param name="contactRole">The contact role.</param>
        public void Update(tbl_ContactRole contactRole)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactRoleId">The contact role id.</param>
        public void Delete(Guid siteId, Guid contactRoleId)
        {
            var contactRole = SelectById(siteId, contactRoleId);
            if (contactRole != null)
            {
                _dataContext.DeleteObject(contactRole);
                _dataContext.SaveChanges();
            }
        }
    }
}