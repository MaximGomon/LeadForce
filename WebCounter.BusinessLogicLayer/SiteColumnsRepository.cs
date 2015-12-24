using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteColumnsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteColumnsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteColumnsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Seelects all site columns.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_SiteColumns> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_SiteColumns.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by activity rule id.
        /// </summary>
        /// <param name="siteActivityRuleId">The site activity rule id.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteColumns> SelectByActivityRuleId(Guid siteActivityRuleId)
        {
            return _dataContext.tbl_SiteColumns.Where(a => a.SiteActivityRuleID == siteActivityRuleId);
        }




        /// <summary>
        /// Selects site column.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="siteColumnID">The site column ID.</param>
        /// <returns></returns>
        public tbl_SiteColumns SelectById(Guid siteID, Guid siteColumnID)
        {
            return _dataContext.tbl_SiteColumns.SingleOrDefault(a => a.SiteID == siteID && a.ID == siteColumnID);
        }



        /// <summary>
        /// Selects the by code.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public tbl_SiteColumns SelectByCode(Guid siteID, string code)
        {
            return _dataContext.tbl_SiteColumns.SingleOrDefault(a => a.SiteID == siteID && a.Code == code);
        }



        /// <summary>
        /// Adds the specified site columns.
        /// </summary>
        /// <param name="siteColumns">The site columns.</param>
        /// <returns></returns>
        public tbl_SiteColumns Add(tbl_SiteColumns siteColumns)
        {
            if (siteColumns.ID == Guid.Empty)
                siteColumns.ID = Guid.NewGuid();
            _dataContext.tbl_SiteColumns.AddObject(siteColumns);
            _dataContext.SaveChanges();
            return siteColumns;
        }



        /// <summary>
        /// Updates the specified site column.
        /// </summary>
        /// <param name="siteColumn">The site column.</param>
        public void Update(tbl_SiteColumns siteColumn)
        {
            var updateSiteColumn = SelectById(siteColumn.SiteID, siteColumn.ID);
            updateSiteColumn.SiteID = siteColumn.SiteID;
            updateSiteColumn.SiteActivityRuleID = siteColumn.SiteActivityRuleID;
            updateSiteColumn.Name = siteColumn.Name;
            updateSiteColumn.CategoryID = siteColumn.CategoryID;
            updateSiteColumn.TypeID = siteColumn.TypeID;
            updateSiteColumn.Code = siteColumn.Code;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteColumnId">The site column id.</param>
        public void DeleteById(Guid siteId, Guid siteColumnId)
        {
            var siteColumn = SelectById(siteId, siteColumnId);
            if (siteColumn != null)
            {
                _dataContext.tbl_SiteColumns.DeleteObject(siteColumn);
                _dataContext.SaveChanges();
            }
        }
    }
}