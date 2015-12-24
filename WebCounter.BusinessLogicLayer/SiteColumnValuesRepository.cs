using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteColumnValuesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteColumnValuesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteColumnValuesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Seelects all site column values.
        /// </summary>
        /// <param name="siteColumnID">The site column ID.</param>
        /// <returns></returns>
        public List<tbl_SiteColumnValues> SelectAll(Guid siteColumnID)
        {
            return _dataContext.tbl_SiteColumnValues.Where(a => a.SiteColumnID == siteColumnID).ToList();
        }



        /// <summary>
        /// Selects the site column value by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_SiteColumnValues SelectById(Guid id)
        {
            return _dataContext.tbl_SiteColumnValues.SingleOrDefault(a => a.ID == id);
        }



        /// <summary>
        /// Selects the by site activity rule ID.
        /// </summary>
        /// <param name="siteActivityRuleID">The site activity rule ID.</param>
        /// <returns></returns>
        public List<tbl_SiteColumnValues> SelectBySiteActivityRuleID(Guid siteActivityRuleID)
        {
            return _dataContext.tbl_SiteColumnValues.Where(a => a.tbl_SiteColumns.SiteActivityRuleID == siteActivityRuleID).ToList();
        }



        /// <summary>
        /// Deletes the specified site column values.
        /// </summary>
        /// <param name="siteColumnValues">The site column values.</param>
        public void Delete(tbl_SiteColumnValues siteColumnValues)
        {
            _dataContext.DeleteObject(siteColumnValues);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified site column value.
        /// </summary>
        /// <param name="siteColumnValue">The site column value.</param>
        public void Update(tbl_SiteColumnValues siteColumnValue)
        {
            var updatesiteColumnValue = SelectById(siteColumnValue.ID);
            updatesiteColumnValue.SiteColumnID = siteColumnValue.SiteColumnID;
            updatesiteColumnValue.Value = siteColumnValue.Value;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Add the specified site column value.
        /// </summary>
        /// <param name="siteColumnValue">The site column value.</param>
        public tbl_SiteColumnValues Add(tbl_SiteColumnValues siteColumnValue)
        {
            siteColumnValue.ID = Guid.NewGuid();
            _dataContext.tbl_SiteColumnValues.AddObject(siteColumnValue);
            _dataContext.SaveChanges();
            return siteColumnValue;
        }
    }
}