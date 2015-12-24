using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteEventTemplateActivityRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteEventTemplateActivityRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteEventTemplateActivityRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="siteEventTemplateID">The site event template ID.</param>
        /// <returns></returns>
        public List<tbl_SiteEventTemplateActivity> SelectAll(Guid siteID, Guid siteEventTemplateID)
        {
            return _dataContext.tbl_SiteEventTemplateActivity.Where(a => a.SiteID == siteID && a.SiteEventTemplateID == siteEventTemplateID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="siteEventTemplateActivityID">The site event template activity ID.</param>
        /// <returns></returns>
        public tbl_SiteEventTemplateActivity SelectById(Guid siteID, Guid siteEventTemplateActivityID)
        {
            return _dataContext.tbl_SiteEventTemplateActivity.SingleOrDefault(a => a.SiteID == siteID && a.ID == siteEventTemplateActivityID);
        }



        /// <summary>
        /// Adds the specified site event template activity.
        /// </summary>
        /// <param name="siteEventTemplateActivity">The site event template activity.</param>
        /// <returns></returns>
        public tbl_SiteEventTemplateActivity Add(tbl_SiteEventTemplateActivity siteEventTemplateActivity)
        {
            siteEventTemplateActivity.ID = Guid.NewGuid();
            _dataContext.tbl_SiteEventTemplateActivity.AddObject(siteEventTemplateActivity);
            _dataContext.SaveChanges();

            return siteEventTemplateActivity;
        }



        /// <summary>
        /// Updates the specified site event template activity.
        /// </summary>
        /// <param name="siteEventTemplateActivity">The site event template activity.</param>
        public void Update(tbl_SiteEventTemplateActivity siteEventTemplateActivity)
        {
            var updateSiteEventTemplateActivity = SelectById(siteEventTemplateActivity.SiteID, siteEventTemplateActivity.ID);
            updateSiteEventTemplateActivity.SiteEventTemplateID = siteEventTemplateActivity.SiteEventTemplateID;
            updateSiteEventTemplateActivity.EventCategoryID = siteEventTemplateActivity.EventCategoryID;
            updateSiteEventTemplateActivity.ActivityTypeID = siteEventTemplateActivity.ActivityTypeID;
            updateSiteEventTemplateActivity.ActivityCode = siteEventTemplateActivity.ActivityCode;
            updateSiteEventTemplateActivity.ActualPeriod = siteEventTemplateActivity.ActualPeriod;
            updateSiteEventTemplateActivity.Option = siteEventTemplateActivity.Option;
            updateSiteEventTemplateActivity.FormulaID = siteEventTemplateActivity.FormulaID;
            updateSiteEventTemplateActivity.Value = siteEventTemplateActivity.Value;
            _dataContext.SaveChanges();
        }


        /// <summary>
        /// Deletes the specified site event template activity.
        /// </summary>
        /// <param name="siteEventTemplateActivity">The site event template activity.</param>
        public void Delete(tbl_SiteEventTemplateActivity siteEventTemplateActivity)
        {
            _dataContext.DeleteObject(siteEventTemplateActivity);
            _dataContext.SaveChanges();
        }
    }
}