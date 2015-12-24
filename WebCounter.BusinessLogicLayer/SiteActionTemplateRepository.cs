using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActionTemplateRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionTemplateRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActionTemplateRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified site action template.
        /// </summary>
        /// <param name="siteActionTemplate">The site action template.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplate Add(tbl_SiteActionTemplate siteActionTemplate)
        {
            if (siteActionTemplate.ID == Guid.Empty)
                siteActionTemplate.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActionTemplate.AddObject(siteActionTemplate);
            _dataContext.SaveChanges();

            return siteActionTemplate;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActionTemplate> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_SiteActionTemplate.Where(a => a.SiteID == siteID).ToList();
        }




        /// <summary>
        /// Selects all by site templates.
        /// </summary>
        /// <returns></returns>
        public List<tbl_SiteActionTemplate> SelectAllBySiteTemplates()
        {
            return _dataContext.tbl_SiteActionTemplate.Where(a => a.tbl_Sites.IsTemplate).ToList();
        }




        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteActionTemplateID">The site action template ID.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplate SelectById(Guid siteActionTemplateID)
        {
            return _dataContext.tbl_SiteActionTemplate.SingleOrDefault(a => a.ID == siteActionTemplateID);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActionTemplateId">The site action template id.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplate SelectById(Guid siteId, Guid siteActionTemplateId)
        {
            return _dataContext.tbl_SiteActionTemplate.SingleOrDefault(a => a.SiteID == siteId && a.ID == siteActionTemplateId);
        }



        /// <summary>
        /// Selects the name of the by system.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="systemName">Name of the system.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplate SelectBySystemName(Guid siteId, string systemName)
        {
            return _dataContext.tbl_SiteActionTemplate.SingleOrDefault(a => a.SiteID == siteId && a.SystemName.ToLower() == systemName.ToLower());
        }



        /// <summary>
        /// Gets the system site action template.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="systemSiteActionTemplate">The system site action template.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplate SelectSystemSiteActionTemplate(Guid siteId, tbl_SiteActionTemplate systemSiteActionTemplate)
        {
            var siteActionTemplate = SelectBySystemName(siteId, systemSiteActionTemplate.SystemName);

            if (siteActionTemplate == null)
            {
                siteActionTemplate = systemSiteActionTemplate;
                siteActionTemplate.SiteID = siteId;
                siteActionTemplate = Add(siteActionTemplate);
            }

            return siteActionTemplate;
        }



        /// <summary>
        /// Updates the specified site action template.
        /// </summary>
        /// <param name="siteActionTemplate">The site action template.</param>
        public void Update(tbl_SiteActionTemplate siteActionTemplate)
        {
            _dataContext.SaveChanges();
        }
    }
}