using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;
using WebCounter.DataAccessLayer;
using System.Data.Objects;

namespace WebCounter.BusinessLogicLayer
{
    public class ContactRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <returns></returns>
        public int GetCount(Guid siteID, Guid contactID)
        {
            return _dataContext.tbl_Contact.Where(a => a.SiteID == siteID && a.ID == contactID).ToList().Count;
        }



        /// <summary>
        /// Adds the specified site user.
        /// </summary>
        /// <param name="contact">The site user.</param>
        /// <returns></returns>
        public tbl_Contact Add(tbl_Contact contact)
        {
            contact.ID = Guid.NewGuid();

            if (contact.CreatedAt == DateTime.MinValue)
                contact.CreatedAt = DateTime.Now;

            if (!contact.LastActivityAt.HasValue || contact.LastActivityAt == DateTime.MinValue)
                contact.LastActivityAt = DateTime.Now;
            
            if (contact.CellularPhoneStatusID == null) contact.CellularPhoneStatusID = 0;
            if (contact.EmailStatusID == null) contact.EmailStatusID = 0;
            contact.RegistrationSourceID = (int) RegistrationSource.Manual;
            _dataContext.tbl_Contact.AddObject(contact);
            _dataContext.SaveChanges();            
            return contact;
        }



        /// <summary>
        /// Seelects all site users.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Contact> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_Contact.Where(a => a.SiteID == siteID);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Contact> SelectAll()
        {
            return _dataContext.tbl_Contact;
        }


        /// <summary>
        /// Selects the client base statistic active count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public int SelectClientBaseStatisticActiveCount(Guid siteId, DateTime startDate, DateTime endDate)
        {
            var pSiteId = new SqlParameter("SiteID", siteId);
            var pStartDate = new SqlParameter("StartDate", startDate);
            var pEndDate = new SqlParameter("EndDate", endDate);
            var pActive = new SqlParameter("Active", ((int) ContactCategory.Active).ToString());
            var pActiveAboveTariff = new SqlParameter("ActiveAboveTariff", ((int) ContactCategory.ActiveAboveTariff).ToString());
            var pKnown = new SqlParameter("Known", ((int) ContactCategory.Known).ToString());

            return
                _dataContext.ExecuteStoreQuery<int>(
                    @"SELECT COUNT(*)
              FROM tbl_Contact
              WHERE SiteID = @SiteID AND (Category = @Active OR Category = @ActiveAboveTariff OR Category = @Known) AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate
              AND ID IN (SELECT ContactID FROM tbl_ContactActivity WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate)",
                    pSiteId, pStartDate, pEndDate,
                    pActive, pActiveAboveTariff, pKnown).FirstOrDefault();

            //return
            //    SelectAll(siteId).Count(
            //        o => o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
            //            (o.Category == (int) ContactCategory.Active ||
            //             o.Category == (int) ContactCategory.ActiveAboveTariff ||
            //             o.Category == (int) ContactCategory.Known) &&
            //        _dataContext.tbl_ContactActivity.Any(
            //            x =>
            //            x.SiteID == siteId && x.CreatedAt >= startDate && x.CreatedAt <= endDate && x.ContactID == o.ID));                        
        }


        /// <summary>
        /// Selects the count.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public int SelectCount(Guid siteID)
        {
            return _dataContext.tbl_Contact.Where(s => s.SiteID == siteID && !string.IsNullOrEmpty(s.UserFullName) && !string.IsNullOrEmpty(s.Email) && !string.IsNullOrEmpty(s.Phone)).Count();
        }



        /// <summary>
        /// Seelects all site users by status.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="statusID">The status ID.</param>
        /// <returns></returns>
        public List<tbl_Contact> SelectByStatus(Guid siteID, Guid statusID)
        {
            return _dataContext.tbl_Contact.Where(a => a.SiteID == siteID && a.StatusID == statusID).ToList();
        }



        /// <summary>
        /// Selects the by ready to sell.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="ReadyToSellID">The ready to sell ID.</param>
        /// <returns></returns>
        public List<tbl_Contact> SelectByReadyToSell(Guid siteID, Guid ReadyToSellID)
        {
            return _dataContext.tbl_Contact.Where(a => a.SiteID == siteID && a.ReadyToSellID == ReadyToSellID).ToList();
        }



        /// <summary>
        /// Selects the by priorities.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="priorityID">The priority ID.</param>
        /// <returns></returns>
        public List<tbl_Contact> SelectByPriorities(Guid siteID, Guid priorityID)
        {
            return _dataContext.tbl_Contact.Where(a => a.SiteID == siteID && a.PriorityID == priorityID).ToList();
        }



        /// <summary>
        /// Selects the site user by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <returns></returns>
        public tbl_Contact SelectById(Guid siteID, Guid contactID)
        {
            return _dataContext.tbl_Contact.SingleOrDefault(a => a.SiteID == siteID && a.ID == contactID);
        }



        /// <summary>
        /// Selects the by email.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public tbl_Contact SelectByEmail(Guid siteId, string email)
        {
            return _dataContext.tbl_Contact.FirstOrDefault(c => c.SiteID == siteId && c.Email != null && c.Email == email);
        }



        /// <summary>
        /// Selects all by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public IQueryable<tbl_Contact> SelectAllByEmail(string email)
        {
            return _dataContext.tbl_Contact.Where(c => c.Email != null && c.Email.ToLower() == email.ToLower());
        }



        /// <summary>
        /// Checks the names.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        public void CheckNames(Guid siteId)
        {
            var contacts = SelectAll(siteId);

                      
            foreach (var contact in contacts)
            {
                var nameChecker = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                var nameCheck = nameChecker.CheckName(contact.UserFullName, NameCheckerFormat.FIO, Correction.Correct);
                if (!string.IsNullOrEmpty(nameCheck))
                {
                    contact.UserFullName = nameCheck;
                    contact.Surname = nameChecker.Surname;
                    contact.Name = nameChecker.Name;
                    contact.Patronymic = nameChecker.Patronymic;
                    contact.IsNameChecked = nameChecker.IsNameCorrect;
                }
            }
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified site user.
        /// </summary>
        /// <param name="contact">The site user.</param>
        public void Update(tbl_Contact contact)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactList">The contact list.</param>
        public void DeleteAll(Guid siteId, List<Guid> contactList)
        {
            foreach (var contactId in contactList)
            {
                var contact = SelectById(siteId, contactId);
                contact.Category = (int)ContactCategory.Deleted;
            }
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Restores all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactList">The contact list.</param>
        public void RestoreAll(Guid siteId, List<Guid> contactList)
        {
            foreach (var contactId in contactList)
            {
                var contact = SelectById(siteId, contactId);
                if (!string.IsNullOrEmpty(contact.UserFullName) || !string.IsNullOrEmpty(contact.Email))
                    contact.Category = (int)ContactCategory.Known;
                else
                    contact.Category = (int)ContactCategory.Anonymous;
            }
            _dataContext.SaveChanges();
        }


        /// <summary>
        /// Changes the active to known.
        /// </summary>
        public void ChangeActiveToKnown()
        {
            var contacts = _dataContext.tbl_Contact.Where(a => a.Category == (int)ContactCategory.Active || a.Category == (int)ContactCategory.ActiveAboveTariff);
            Guid? accessProfileId;

            foreach (var contact in contacts)
            {
                
            }
            _dataContext.SaveChanges();
        }
    }
}