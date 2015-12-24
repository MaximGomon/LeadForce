using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ContactActivityScoreRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactActivityScoreRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactActivityScoreRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactActivityScoreID">The site user activity score ID.</param>
        /// <returns></returns>
        public tbl_ContactActivityScore SelectById(Guid siteID, Guid contactActivityScoreID)
        {
            return _dataContext.tbl_ContactActivityScore.SingleOrDefault(a => a.SiteID == siteID && a.ID == contactActivityScoreID);
        }



        /// <summary>
        /// Selects the type of the by score.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <param name="siteActivityScoreTypeID">The site activity score type ID.</param>
        /// <returns></returns>
        public tbl_ContactActivityScore SelectByScoreType(Guid siteID, Guid contactID, Guid siteActivityScoreTypeID)
        {
            return _dataContext.tbl_ContactActivityScore.SingleOrDefault(a => a.SiteID == siteID && a.ContactID == contactID && a.SiteActivityScoreTypeID == siteActivityScoreTypeID);
        }



        /// <summary>
        /// Selects the by user id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <returns></returns>
        public List<tbl_ContactActivityScore> SelectByContactId(Guid siteID, Guid contactID)
        {
            return _dataContext.tbl_ContactActivityScore.Where(a => a.SiteID == siteID && a.ContactID == contactID).ToList();
        }



        /// <summary>
        /// Gets the max score.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <returns></returns>
        public int GetMaxScore(Guid siteID, Guid contactID)
        {
            var maxScore = _dataContext.tbl_ContactActivityScore.Where(a => a.SiteID == siteID && a.ContactID == contactID).Max(a => a.Score);
            return maxScore;
        }



        /// <summary>
        /// Adds the specified site user activity score.
        /// </summary>
        /// <param name="contactActivityScore">The site user activity score.</param>
        /// <returns></returns>
        public tbl_ContactActivityScore Add(tbl_ContactActivityScore contactActivityScore)
        {
            contactActivityScore.ID = Guid.NewGuid();
            _dataContext.tbl_ContactActivityScore.AddObject(contactActivityScore);
            _dataContext.SaveChanges();

            return contactActivityScore;
        }



        /// <summary>
        /// Updates the specified site user activity score.
        /// </summary>
        /// <param name="contactActivityScore">The site user activity score.</param>
        public void Update(tbl_ContactActivityScore contactActivityScore)
        {
            var updateContactActivityScore = SelectById(contactActivityScore.SiteID, contactActivityScore.ID);
            updateContactActivityScore.SiteActivityScoreTypeID = contactActivityScore.SiteActivityScoreTypeID;
            updateContactActivityScore.ContactID = contactActivityScore.ContactID;
            updateContactActivityScore.Score = contactActivityScore.Score;

            _dataContext.SaveChanges();
        }
    }
}