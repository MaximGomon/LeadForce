using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActivityRuleLayoutRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActivityRuleLayoutRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActivityRuleLayoutRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified site activity rule layout.
        /// </summary>
        /// <param name="siteActivityRuleLayout">The site activity rule layout.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleLayout Add(tbl_SiteActivityRuleLayout siteActivityRuleLayout)
        {
            if (siteActivityRuleLayout.ID == Guid.Empty)
                siteActivityRuleLayout.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActivityRuleLayout.AddObject(siteActivityRuleLayout);
            _dataContext.SaveChanges();

            return siteActivityRuleLayout;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActivityRuleLayout> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_SiteActivityRuleLayout.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteActivityRuleLayoutID">The site activity rule layout ID.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleLayout SelectById(Guid siteActivityRuleLayoutID)
        {
            return _dataContext.tbl_SiteActivityRuleLayout.SingleOrDefault(a => a.ID == siteActivityRuleLayoutID);
        }



        /// <summary>
        /// Selects the by site activity rule id.
        /// </summary>
        /// <param name="siteActivityRuleId">The site activity rule id.</param>
        /// <returns></returns>
        public List<tbl_SiteActivityRuleLayout> SelectBySiteActivityRuleId(Guid siteActivityRuleId)
        {
            return _dataContext.tbl_SiteActivityRuleLayout.Where(a => a.SiteActivityRuleID == siteActivityRuleId).ToList();
        }



        /// <summary>
        /// Updates the specified site activity rule layout.
        /// </summary>
        /// <param name="siteActivityRuleLayout">The site activity rule layout.</param>
        public void Update(tbl_SiteActivityRuleLayout siteActivityRuleLayout)
        {
            var updateSiteActivityRuleLayout = SelectById(siteActivityRuleLayout.ID);
            updateSiteActivityRuleLayout.SiteID = siteActivityRuleLayout.SiteID;
            updateSiteActivityRuleLayout.SiteActivityRuleID = siteActivityRuleLayout.SiteActivityRuleID;
            updateSiteActivityRuleLayout.ParentID = siteActivityRuleLayout.ParentID;
            updateSiteActivityRuleLayout.SiteColumnID = siteActivityRuleLayout.SiteColumnID;
            updateSiteActivityRuleLayout.IsRequired = siteActivityRuleLayout.IsRequired;
            updateSiteActivityRuleLayout.IsExtraField = siteActivityRuleLayout.IsExtraField;
            updateSiteActivityRuleLayout.IsAdmin = siteActivityRuleLayout.IsAdmin;
            updateSiteActivityRuleLayout.CSSStyle = siteActivityRuleLayout.CSSStyle;
            updateSiteActivityRuleLayout.DefaultValue = siteActivityRuleLayout.DefaultValue;
            updateSiteActivityRuleLayout.Name = siteActivityRuleLayout.Name;
            updateSiteActivityRuleLayout.Order = siteActivityRuleLayout.Order;
            updateSiteActivityRuleLayout.ShowName = siteActivityRuleLayout.ShowName;
            updateSiteActivityRuleLayout.Orientation = siteActivityRuleLayout.Orientation;
            updateSiteActivityRuleLayout.OutputFormat = siteActivityRuleLayout.OutputFormat;
            updateSiteActivityRuleLayout.OutputFormatFields = siteActivityRuleLayout.OutputFormatFields;
            updateSiteActivityRuleLayout.Description = siteActivityRuleLayout.Description;
            updateSiteActivityRuleLayout.LayoutType = siteActivityRuleLayout.LayoutType;
            updateSiteActivityRuleLayout.LayoutParams = siteActivityRuleLayout.LayoutParams;
            updateSiteActivityRuleLayout.SysField = siteActivityRuleLayout.SysField;
            updateSiteActivityRuleLayout.ColumnTypeExpressionID = siteActivityRuleLayout.ColumnTypeExpressionID;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site activity rule layout id.
        /// </summary>
        /// <param name="siteActivityRuleLayoutId">The site activity rule layout id.</param>
        public void Delete(Guid siteActivityRuleLayoutId)
        {
            var siteActivityRuleLayout = SelectById(siteActivityRuleLayoutId);
            if (siteActivityRuleLayout != null)
            {
                _dataContext.DeleteObject(siteActivityRuleLayout);
                _dataContext.SaveChanges();
            }
        }
    }
}