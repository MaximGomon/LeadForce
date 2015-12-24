using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteEventTemplateScoreRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteEventTemplateScoreRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteEventTemplateScoreRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="siteEventTemplateScoreID">The site event template score ID.</param>
        /// <returns></returns>
        public tbl_SiteEventTemplateScore SelectById(Guid siteID, Guid siteEventTemplateScoreID)
        {
            return _dataContext.tbl_SiteEventTemplateScore.SingleOrDefault(a => a.SiteID == siteID && a.ID == siteEventTemplateScoreID);
        }



        /// <summary>
        /// Selects the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteEventTemplateID">The site event template ID.</param>
        /// <returns></returns>
        public List<tbl_SiteEventTemplateScore> Select(Guid siteId, Guid siteEventTemplateID)
        {
            return _dataContext.tbl_SiteEventTemplateScore.Where(a => a.SiteID == siteId && a.SiteEventTemplateID == siteEventTemplateID).ToList();
        }



        /// <summary>
        /// Adds the specified site event template.
        /// </summary>
        /// <param name="siteEventTemplate">The site event template.</param>
        /// <returns></returns>
        public tbl_SiteEventTemplateScore Add(tbl_SiteEventTemplateScore siteEventTemplateScore)
        {
            siteEventTemplateScore.ID = Guid.NewGuid();
            _dataContext.tbl_SiteEventTemplateScore.AddObject(siteEventTemplateScore);
            _dataContext.SaveChanges();

            return siteEventTemplateScore;
        }



        /// <summary>
        /// Updates the specified site event template score.
        /// </summary>
        /// <param name="siteEventTemplateScore">The site event template score.</param>
        public void Update(tbl_SiteEventTemplateScore siteEventTemplateScore)
        {
            var updateSiteEventTemplateScore = SelectById(siteEventTemplateScore.SiteID, siteEventTemplateScore.ID);
            updateSiteEventTemplateScore.SiteEventTemplateID = siteEventTemplateScore.SiteEventTemplateID;
            updateSiteEventTemplateScore.SiteActivityScoreTypeID = siteEventTemplateScore.SiteActivityScoreTypeID;
            updateSiteEventTemplateScore.OperationID = siteEventTemplateScore.OperationID;
            updateSiteEventTemplateScore.Score = siteEventTemplateScore.Score;

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site event template score.
        /// </summary>
        /// <param name="siteEventTemplateScore">The site event template score.</param>
        public void Delete(tbl_SiteEventTemplateScore siteEventTemplateScore)
        {
            _dataContext.DeleteObject(siteEventTemplateScore);
            _dataContext.SaveChanges();
        }
    }
}