using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SitesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SitesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns></returns>
        public tbl_Sites Add(tbl_Sites site)
        {
            _dataContext.tbl_Sites.AddObject(site);
            _dataContext.SaveChanges();

            return site;
        }



        /// <summary>
        /// Seelects all site users.
        /// </summary>
        /// <returns></returns>
        public List<tbl_Sites> SelectAll()
        {
            return _dataContext.tbl_Sites.Where(a => true).ToList();
        }



        /// <summary>
        /// Selects the templates.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Sites> SelectTemplates()
        {
            return _dataContext.tbl_Sites.Where(s => s.IsActive && s.IsTemplate);
        }
        


        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public tbl_Sites SelectById(Guid siteID)
        {
            return _dataContext.tbl_Sites.SingleOrDefault(a => a.ID == siteID);
        }



        /// <summary>
        /// Updates the specified site.
        /// </summary>
        /// <param name="site">The site.</param>
        public void Update(tbl_Sites site)
        {
            _dataContext.SaveChanges();
        }
    }
}