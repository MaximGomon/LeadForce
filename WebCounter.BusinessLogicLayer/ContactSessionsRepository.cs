using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ContactSessionsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactSessionsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactSessionsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified site user.
        /// </summary>
        /// <param name="contactSession">The site contact.</param>
        /// <returns></returns>
        public tbl_ContactSessions Add(tbl_ContactSessions contactSession)
        {
            contactSession.ID = Guid.NewGuid();
            _dataContext.tbl_ContactSessions.AddObject(contactSession);
            _dataContext.SaveChanges();

            return contactSession;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ContactSessions> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_ContactSessions.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by user id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <returns></returns>
        public List<tbl_ContactSessions> SelectByContactId(Guid siteID, Guid contactID)
        {
            return _dataContext.tbl_ContactSessions.Where(a => a.SiteID == siteID && a.ContactID == contactID).OrderByDescending(a => a.SessionDate).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactSessionsId">The site user sessions id.</param>
        /// <returns></returns>
        public tbl_ContactSessions SelectById(Guid siteID, Guid contactSessionsId)
        {
            return _dataContext.tbl_ContactSessions.SingleOrDefault(a => a.SiteID == siteID && a.ID == contactSessionsId);
        }



        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <returns></returns>
        /*public tbl_SiteUserSessions Select(Guid siteID, Guid userID, string userIP, Guid browserID)
        {
            return _dataContext.tbl_SiteUserSessions.SingleOrDefault(a => a.SiteID == siteID && a.SiteUserID == userID && a.UserIP == userIP && a.BrowserID == browserID);
        }*/



        public tbl_ContactSessions SelectLastSession(Guid siteID, Guid contactID)
        {
            var maxNumber = _dataContext.tbl_ContactSessions.Where(a => a.SiteID == siteID && a.ContactID == contactID).Max(o => o.UserSessionNumber);
            return _dataContext.tbl_ContactSessions.FirstOrDefault(a => a.SiteID == siteID && a.ContactID == contactID && a.UserSessionNumber == maxNumber);
        }


        /// <summary>
        /// Selects the first session.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The contact ID.</param>
        /// <returns></returns>
        public tbl_ContactSessions SelectFirstSession(Guid siteID, Guid contactID)
        {
            return _dataContext.tbl_ContactSessions.FirstOrDefault(a => a.SiteID == siteID && a.ContactID == contactID && a.UserSessionNumber == 1);
        }



        /// <summary>
        /// Updates the specified site user.
        /// </summary>
        /// <param name="contactSession">The site user.</param>
        public void Update(tbl_ContactSessions contactSession)
        {
            _dataContext.SaveChanges();
        }
    }
}