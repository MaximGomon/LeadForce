using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteEventTemplatesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteEventTemplatesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteEventTemplatesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public List<tbl_SiteEventTemplates> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_SiteEventTemplates.Where(a => a.SiteID == siteId).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteEventTemplateId">The site event template id.</param>
        /// <returns></returns>
        public tbl_SiteEventTemplates SelectById(Guid siteEventTemplateId)
        {
            return _dataContext.tbl_SiteEventTemplates.SingleOrDefault(a => a.ID == siteEventTemplateId);
        }



        /// <summary>
        /// Adds the specified site event template.
        /// </summary>
        /// <param name="siteEventTemplate">The site event template.</param>
        /// <returns></returns>
        public tbl_SiteEventTemplates Add(tbl_SiteEventTemplates siteEventTemplate)
        {
            siteEventTemplate.ID = Guid.NewGuid();
            _dataContext.tbl_SiteEventTemplates.AddObject(siteEventTemplate);
            _dataContext.SaveChanges();

            return siteEventTemplate;
        }



        /// <summary>
        /// Updates the specified site event template.
        /// </summary>
        /// <param name="siteEventTemplate">The site event template.</param>
        public void Update(tbl_SiteEventTemplates siteEventTemplate)
        {
            _dataContext.SaveChanges();
        }
    }
}