using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ReminderRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ReminderRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified reminder.
        /// </summary>
        /// <param name="reminder">The reminder.</param>
        public void Add(tbl_Reminder reminder)
        {
            reminder.ID = Guid.NewGuid();
            _dataContext.tbl_Reminder.AddObject(reminder);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="reminderId">The reminder id.</param>
        /// <returns></returns>
        public tbl_Reminder SelectById(Guid contactId, Guid reminderId)
        {
            return _dataContext.tbl_Reminder.Where(r => r.ContactID == contactId && r.ID == reminderId).SingleOrDefault();
        }



        /// <summary>
        /// Selects the by object id.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        public tbl_Reminder SelectByObjectId(Guid contactId, Guid objectId)
        {
            return _dataContext.tbl_Reminder.Where(r => r.ContactID == contactId && r.ObjectID == objectId).SingleOrDefault();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public IQueryable<ReminderExtended> SelectAll(Guid contactId, DateTime dateTime)
        {
            return _dataContext.tbl_Reminder.Where(r => r.ContactID == contactId && r.ReminderDate <= dateTime).Join(_dataContext.tbl_Module,
                                                                                       r => r.ModuleID, m => m.ID,
                                                                                       (r, m) =>
                                                                                       new ReminderExtended()
                                                                                           {
                                                                                               ID = r.ID,
                                                                                               Title = r.Title,
                                                                                               ReminderDate = r.ReminderDate,
                                                                                               ContactID = r.ContactID,
                                                                                               ModuleID = r.ModuleID,
                                                                                               ObjectID = r.ObjectID,
                                                                                               ModuleTitle = m.Title
                                                                                           });
        }



        /// <summary>
        /// Updates the specified reminder.
        /// </summary>
        /// <param name="reminder">The reminder.</param>
        public void Update(tbl_Reminder reminder)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified contact id.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="reminderId">The reminder id.</param>
        public void Delete(Guid contactId, Guid reminderId)
        {
            var toDelete = _dataContext.tbl_Reminder.Where(r => r.ID == reminderId && r.ContactID == contactId).SingleOrDefault();
            if (toDelete != null)
            {
                _dataContext.tbl_Reminder.DeleteObject(toDelete);
                _dataContext.SaveChanges();
            }
        }



        /// <summary>
        /// Deletes the specified contact id.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="reminderIds">The reminder ids.</param>
        public void Delete(Guid contactId, List<Guid> reminderIds)
        {
            var parameters = reminderIds.Select(reminderId => string.Format("ID = '{0}'", reminderId)).ToList();
            var query = string.Format("DELETE FROM tbl_Reminder WHERE ContactID = '{0}' AND ({1})", contactId, string.Join(" OR ", parameters));
            _dataContext.ExecuteStoreCommand(query);
        }
    }
}