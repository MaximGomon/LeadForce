using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ContactColumnValuesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactColumnValuesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactColumnValuesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by user id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The contact ID.</param>
        /// <returns></returns>
        public List<tbl_ContactColumnValues> SelectByContactId(Guid siteID, Guid contactID)
        {
            return _dataContext.tbl_ContactColumnValues.Where(a => a.tbl_SiteColumns.SiteID == siteID && a.ContactID == contactID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="contactColumnValueId">The site user column value id.</param>
        /// <returns></returns>
        public tbl_ContactColumnValues SelectById(Guid contactColumnValueId)
        {
            return _dataContext.tbl_ContactColumnValues.SingleOrDefault(a => a.ID == contactColumnValueId);
        }



        /// <summary>
        /// Selects the specified user ID.
        /// </summary>
        /// <param name="contactID">The user ID.</param>
        /// <param name="siteColumnID">The site column ID.</param>
        /// <returns></returns>
        public tbl_ContactColumnValues Select(Guid contactID, Guid siteColumnID)
        {
            return _dataContext.tbl_ContactColumnValues.SingleOrDefault(a => a.ContactID == contactID && a.SiteColumnID == siteColumnID);
        }



        /// <summary>
        /// Selects the by site activity rule id.
        /// </summary>
        /// <param name="siteActivityRuleId">The site activity rule id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ContactColumnValues> SelectBySiteActivityRuleId(Guid siteActivityRuleId)
        {
            return _dataContext.tbl_ContactColumnValues.Where(a => a.tbl_SiteColumns.SiteActivityRuleID == siteActivityRuleId);
        }



        /// <summary>
        /// Gets the sum by characteristics score.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contac">The user ID.</param>
        /// <returns></returns>
        public int GetSumByCharacteristicsScore(Guid siteID, Guid contac)
        {
            var contactColumnValues = _dataContext.tbl_ContactColumnValues.Where(a => a.tbl_SiteColumns.SiteID == siteID && a.ContactID == contac).ToList();
            /*if (contactColumnValues.Count > 0)
                return contactColumnValues.Sum(a => a.Weight);*/
            
            return 0;
        }



        /// <summary>
        /// Adds the specified site user column value.
        /// </summary>
        /// <param name="contactColumnValue">The site user column value.</param>
        /// <returns></returns>
        public tbl_ContactColumnValues Add(tbl_ContactColumnValues contactColumnValue)
        {
            contactColumnValue.ID = Guid.NewGuid();
            _dataContext.tbl_ContactColumnValues.AddObject(contactColumnValue);
            _dataContext.SaveChanges();
            return contactColumnValue;
        }



        /// <summary>
        /// Updates the specified site user column values.
        /// </summary>
        /// <param name="contactColumnValues">The site user column values.</param>
        public void Update(tbl_ContactColumnValues contactColumnValues)
        {
            var updateContactColumnValues = SelectById(contactColumnValues.ID);
            updateContactColumnValues.ContactID = contactColumnValues.ContactID;
            updateContactColumnValues.SiteColumnID = contactColumnValues.SiteColumnID;
            updateContactColumnValues.StringValue = contactColumnValues.StringValue;
            updateContactColumnValues.DateValue = contactColumnValues.DateValue;
            updateContactColumnValues.SiteColumnValueID = contactColumnValues.SiteColumnValueID;
            _dataContext.SaveChanges();
        }
    }
}