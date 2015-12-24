using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.BusinessLogicLayer
{
    public class ContactToContactRoleRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactToContactRoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactToContactRoleRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified contact to contact role.
        /// </summary>
        /// <param name="contactToContactRole">The contact to contact role.</param>
        /// <returns></returns>
        public tbl_ContactToContactRole Add(tbl_ContactToContactRole contactToContactRole)
        {
            if (contactToContactRole.ID == Guid.Empty)
                contactToContactRole.ID = Guid.NewGuid();
            _dataContext.tbl_ContactToContactRole.AddObject(contactToContactRole);
            _dataContext.SaveChanges();

            return contactToContactRole;
        }



        /// <summary>
        /// Selects the by contact role id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactRoleId">The contact role id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ContactToContactRole> SelectByContactRoleId(Guid siteId, Guid contactRoleId)
        {
            return _dataContext.tbl_ContactToContactRole.Where(a => a.SiteID == siteId && a.ContactRoleID == contactRoleId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactRoleId">The contact role id.</param>
        /// <returns></returns>
        public tbl_ContactToContactRole SelectById(Guid siteId, Guid contactRoleId)
        {
            return _dataContext.tbl_ContactToContactRole.SingleOrDefault(a => a.SiteID == siteId && a.ID == contactRoleId);
        }



        /// <summary>
        /// Updates the specified contact to contact role.
        /// </summary>
        /// <param name="contactToContactRole">The contact to contact role.</param>
        public void Update(tbl_ContactToContactRole contactToContactRole)
        {
            _dataContext.SaveChanges();
        }




        /// <summary>
        /// Saves the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactToContactRoleList">The contact to contact role list.</param>
        /// <param name="contactRoleId">The contact role id.</param>
        public void Save(Guid siteId, List<Guid> contactToContactRoleList, Guid contactRoleId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_ContactToContactRole WHERE ContactRoleID = @ContactRoleID", new SqlParameter { ParameterName = "ContactRoleID", Value = contactRoleId });

            if (contactToContactRoleList != null && contactToContactRoleList.Count > 0)
            {
                foreach (var contactId in contactToContactRoleList)
                {
                    if (contactId == Guid.Empty)
                        continue;

                    _dataContext.tbl_ContactToContactRole.AddObject(new tbl_ContactToContactRole
                    {
                        ID = Guid.NewGuid(),
                        SiteID = siteId,
                        ContactRoleID = contactRoleId,
                        ContactID = contactId
                    });
                }
            }

            _dataContext.SaveChanges();
        }
    }
}