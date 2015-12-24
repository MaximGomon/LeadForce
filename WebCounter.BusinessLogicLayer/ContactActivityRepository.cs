using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ContactActivityRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactActivityRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactActivityRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public List<tbl_ContactActivity> Select(Guid siteID, Guid? contactID = null, ActivityType? activityType = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var contactActivity = _dataContext.tbl_ContactActivity.Where(a => a.SiteID == siteID);

            if (contactID != null)
                contactActivity = contactActivity.Where(a => a.ContactID == contactID);
            if (activityType != null)
                contactActivity = contactActivity.Where(a => a.ActivityTypeID == (int)activityType);
            if (startDate != null)
                contactActivity = contactActivity.Where(a => a.CreatedAt >= (DateTime)startDate);
            if (endDate != null)
                contactActivity = contactActivity.Where(a => a.CreatedAt <= (DateTime)endDate);
            
            return contactActivity.OrderByDescending(a => a.CreatedAt).ToList();
        }


        public IQueryable<tbl_ContactActivity> SelectByIP(Guid siteId, string userIP, ActivityType? activityType = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var contactActivity = _dataContext.tbl_ContactActivity.Where(a => a.SiteID == siteId && a.tbl_ContactSessions.UserIP == userIP);
            if (activityType != null)
                contactActivity = contactActivity.Where(a => a.ActivityTypeID == (int)activityType);
            if (startDate != null)
                contactActivity = contactActivity.Where(a => a.CreatedAt >= (DateTime)startDate);
            if (endDate != null)
                contactActivity = contactActivity.Where(a => a.CreatedAt <= (DateTime)endDate);

            return contactActivity.OrderByDescending(a => a.CreatedAt);
        }


        public IQueryable<tbl_ContactActivity> SelectByIPAndUrl(Guid siteId, string userIP, string url, ActivityType activityType, DateTime startDate, DateTime endDate)
        {
            var contactActivity = _dataContext.tbl_ContactActivity.Where(a => a.SiteID == siteId && a.tbl_ContactSessions.UserIP == userIP && a.ActivityCode.Contains(url)
                && a.ActivityTypeID == (int)activityType && a.CreatedAt >= startDate && a.CreatedAt <= endDate);

            return contactActivity.OrderByDescending(a => a.CreatedAt);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ContactActivity>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_ContactActivity.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <param name="ActivityCode">The activity code.</param>
        /// <returns></returns>
        public tbl_ContactActivity SelectByCode(Guid siteID, Guid contactID, string ActivityCode)
        {
            return _dataContext.tbl_ContactActivity.OrderByDescending(a => a.CreatedAt).FirstOrDefault(a => a.SiteID == siteID && a.ContactID == contactID && a.ActivityCode == ActivityCode);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactActivityID">The contact activity ID.</param>
        /// <returns></returns>
        public tbl_ContactActivity SelectById(Guid siteID, Guid contactActivityID)
        {
            return _dataContext.tbl_ContactActivity.OrderByDescending(a => a.CreatedAt).FirstOrDefault(a => a.SiteID == siteID && a.ID == contactActivityID);
        }



        /// <summary>
        /// Adds the specified site user activity.
        /// </summary>
        /// <param name="contactActivity">The site user activity.</param>
        /// <returns></returns>
        public tbl_ContactActivity Add(tbl_ContactActivity contactActivity)
        {
            contactActivity.ID = Guid.NewGuid();
            contactActivity.CreatedAt = DateTime.Now;
            _dataContext.tbl_ContactActivity.AddObject(contactActivity);
            _dataContext.SaveChanges();

            return contactActivity;
        }
    }
}