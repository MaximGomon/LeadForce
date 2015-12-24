using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteEventActionTemplateRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteEventActionTemplateRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteEventActionTemplateRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="SiteEventActionTemplateID">The site event action template ID.</param>
        /// <returns></returns>
        public tbl_SiteEventActionTemplate SelectById(Guid siteId, Guid SiteEventActionTemplateID)
        {
            return _dataContext.tbl_SiteEventActionTemplate.SingleOrDefault(a => a.SiteID == siteId && a.ID == SiteEventActionTemplateID);
        }



        /// <summary>
        /// Selects the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteEventTemplateID">The site event template ID.</param>
        /// <returns></returns>
        public List<tbl_SiteEventActionTemplate> Select(Guid siteId, Guid siteEventTemplateID)
        {
            return _dataContext.tbl_SiteEventActionTemplate.Where(a => a.SiteID == siteId && a.SiteEventTemplateID == siteEventTemplateID).ToList();
        }



        /// <summary>
        /// Adds the specified site event action template.
        /// </summary>
        /// <param name="siteEventActionTemplate">The site event action template.</param>
        /// <returns></returns>
        public tbl_SiteEventActionTemplate Add(tbl_SiteEventActionTemplate siteEventActionTemplate)
        {
            siteEventActionTemplate.ID = Guid.NewGuid();
            _dataContext.tbl_SiteEventActionTemplate.AddObject(siteEventActionTemplate);
            _dataContext.SaveChanges();

            return siteEventActionTemplate;
        }



        /// <summary>
        /// Updates the specified site event action template.
        /// </summary>
        /// <param name="siteEventActionTemplate">The site event action template.</param>
        public void Update(tbl_SiteEventActionTemplate siteEventActionTemplate)
        {
            var updateSiteEventActionTemplate = SelectById(siteEventActionTemplate.SiteID, siteEventActionTemplate.ID);
            updateSiteEventActionTemplate.SiteEventTemplateID = siteEventActionTemplate.SiteEventTemplateID;
            updateSiteEventActionTemplate.SiteActionTemplateID = siteEventActionTemplate.SiteActionTemplateID;
            updateSiteEventActionTemplate.StartAfter = siteEventActionTemplate.StartAfter;
            updateSiteEventActionTemplate.StartAfterTypeID = siteEventActionTemplate.StartAfterTypeID;
            updateSiteEventActionTemplate.MessageText = siteEventActionTemplate.MessageText;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site event action template.
        /// </summary>
        /// <param name="siteEventActionTemplate">The site event action template.</param>
        public void Delete(tbl_SiteEventActionTemplate siteEventActionTemplate)
        {
            _dataContext.DeleteObject(siteEventActionTemplate);
            _dataContext.SaveChanges();
        }
    }
}