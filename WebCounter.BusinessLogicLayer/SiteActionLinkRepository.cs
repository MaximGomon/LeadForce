using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActionLinkRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionLinkRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActionLinkRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActionLinkID">The site action link ID.</param>
        /// <returns></returns>
        public tbl_SiteActionLink SelectById(Guid siteActionLinkID)
        {
            return _dataContext.tbl_SiteActionLink.SingleOrDefault(a => a.ID == siteActionLinkID);
        }



        /// <summary>
        /// Selects the specified user ID.
        /// </summary>
        /// <param name="contactID">The user ID.</param>
        /// <param name="actionID">The action ID.</param>
        /// <param name="actionTemplateID">The action template ID.</param>
        /// <param name="activityRuleID">The activity rule ID.</param>
        /// <param name="linkURL">The link URL.</param>
        /// <returns></returns>
        public tbl_SiteActionLink Select(Guid contactID, Guid? actionID, Guid? actionTemplateID, Guid? activityRuleID, string linkURL)
        {
            return
                _dataContext.tbl_SiteActionLink.SingleOrDefault(
                    a =>
                    a.ContactID == contactID &&
                    (actionID == null ? a.SiteActionID == null : a.SiteActionID == actionID) &&
                    (actionTemplateID == null ? a.SiteActionTemplateID == null : a.SiteActionTemplateID == actionTemplateID) &&
                    (activityRuleID == null ? a.SiteActivityRuleID == null : a.SiteActivityRuleID == activityRuleID) &&
                    (linkURL == null ? a.LinkURL == null : a.LinkURL == linkURL));
        }



        public tbl_SiteActionLink Select(Guid contactID, Guid actionID, Guid actionTemplateID, string linkURL)
        {
            return
                _dataContext.tbl_SiteActionLink.SingleOrDefault(
                    a =>
                    a.ContactID == contactID &&
                    a.SiteActionID == actionID &&
                    a.SiteActionTemplateID == actionTemplateID &&
                    a.LinkURL == linkURL);
        }



        public tbl_SiteActionLink Select(Guid contactID, Guid actionID, Guid actionTemplateID, Guid siteActivityRuleID)
        {
            return
                _dataContext.tbl_SiteActionLink.FirstOrDefault(
                    a =>
                    a.ContactID == contactID &&
                    a.SiteActionID == actionID &&
                    a.SiteActionTemplateID == actionTemplateID &&
                    a.SiteActivityRuleID == siteActivityRuleID);
        }



        public tbl_SiteActionLink Select(Guid contactID, string linkURL)
        {
            return
                _dataContext.tbl_SiteActionLink.FirstOrDefault(
                    a =>
                    a.ContactID == contactID &&
                    a.LinkURL == linkURL);
        }



        public tbl_SiteActionLink Select(Guid contactID, Guid siteActivityRuleID)
        {
            return
                _dataContext.tbl_SiteActionLink.FirstOrDefault(
                    a =>
                    a.ContactID == contactID &&
                    a.SiteActivityRuleID == siteActivityRuleID);
        }




        /// <summary>
        /// Selects the by action template ID.
        /// </summary>
        /// <param name="siteActionTemplateID">The site action template ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActionLink> SelectByActionTemplateID(Guid siteActionTemplateID)
        {
            return _dataContext.tbl_SiteActionLink.Where(a => a.SiteActionTemplateID == siteActionTemplateID).ToList();
        }



        /// <summary>
        /// Adds the specified site action.
        /// </summary>
        /// <param name="siteAction">The site action.</param>
        /// <returns></returns>
        public tbl_SiteActionLink Add(tbl_SiteActionLink siteActionLink)
        {
            siteActionLink.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActionLink.AddObject(siteActionLink);
            _dataContext.SaveChanges();

            return siteActionLink;
        }



        /// <summary>
        /// Updates the specified site action link.
        /// </summary>
        /// <param name="siteActionLink">The site action link.</param>
        public void Update(tbl_SiteActionLink siteActionLink)
        {
            var updateSiteActionLink = SelectById(siteActionLink.ID);
            updateSiteActionLink.ContactID = siteActionLink.ContactID;
            updateSiteActionLink.SiteActionID = siteActionLink.SiteActionID;
            updateSiteActionLink.SiteActionTemplateID = siteActionLink.SiteActionTemplateID;
            updateSiteActionLink.SiteActivityRuleID = siteActionLink.SiteActivityRuleID;
            updateSiteActionLink.LinkURL = siteActionLink.LinkURL;
            updateSiteActionLink.ActionLinkDate = siteActionLink.ActionLinkDate;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site action link.
        /// </summary>
        /// <param name="siteActionLink">The site action link.</param>
        public void Delete(tbl_SiteActionLink siteActionLink)
        {
            _dataContext.DeleteObject(siteActionLink);
            _dataContext.SaveChanges();
        }
    }
}