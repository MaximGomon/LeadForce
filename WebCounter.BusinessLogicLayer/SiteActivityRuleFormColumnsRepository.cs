using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActivityRuleFormColumnsRepository
    {
        private WebCounterEntities _dataContext;



        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActivityRuleFormColumnsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActivityRuleFormColumnsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified site activity rule form columns.
        /// </summary>
        /// <param name="siteActivityRuleFormColumns">The site activity rule form columns.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleFormColumns Add(tbl_SiteActivityRuleFormColumns siteActivityRuleFormColumns)
        {
            siteActivityRuleFormColumns.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActivityRuleFormColumns.AddObject(siteActivityRuleFormColumns);
            _dataContext.SaveChanges();

            return siteActivityRuleFormColumns;
        }



        /// <summary>
        /// Selects the by site activity rule ID.
        /// </summary>
        /// <param name="siteActivityRuleID">The site activity rule ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActivityRuleFormColumns> SelectBySiteActivityRuleID(Guid siteActivityRuleID)
        {
            return _dataContext.tbl_SiteActivityRuleFormColumns.Where(a => a.SiteActivityRuleID == siteActivityRuleID).ToList();
        }




        /// <summary>
        /// Updates the specified site activity rule form columns.
        /// </summary>
        /// <param name="siteActivityRuleFormColumns">The site activity rule form columns.</param>
        public void Update(tbl_SiteActivityRuleFormColumns siteActivityRuleFormColumns)
        {
            _dataContext.SaveChanges();
        }


        public void DeleteBySiteActivityRuleID(Guid siteActivityRuleID)
        {
            var siteActivityRuleFormColumns = SelectBySiteActivityRuleID(siteActivityRuleID);
            if (siteActivityRuleFormColumns != null && siteActivityRuleFormColumns.Count > 0)
            {
                foreach (var siteActivityRuleFormColumn in siteActivityRuleFormColumns)
                {
                    _dataContext.tbl_SiteActivityRuleFormColumns.DeleteObject(siteActivityRuleFormColumn);
                }
                _dataContext.SaveChanges();
            }
        }
    }
}