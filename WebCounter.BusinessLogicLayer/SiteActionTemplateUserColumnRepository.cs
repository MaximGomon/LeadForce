using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActionTemplateUserColumnRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionTemplateUserColumnRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActionTemplateUserColumnRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="siteColumnID">The site column ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActionTemplateUserColumn> SelectByEventTemplateId(Guid siteID, Guid siteEventTemplateID)
        {
            return _dataContext.tbl_SiteActionTemplateUserColumn.Where(a => a.SiteEventTemplateID == siteEventTemplateID).ToList();
        }



        /// <summary>
        /// Selects the by column id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="siteColumnID">The site column ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActionTemplateUserColumn> SelectByColumnId(Guid siteID, Guid siteColumnID)
        {
            return _dataContext.tbl_SiteActionTemplateUserColumn.Where(a => a.SiteColumnID == siteColumnID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplateUserColumn SelectById(Guid id)
        {
            return _dataContext.tbl_SiteActionTemplateUserColumn.SingleOrDefault(a => a.ID == id);
        }



        /// <summary>
        /// Deletes the specified site action template user column.
        /// </summary>
        /// <param name="siteActionTemplateUserColumn">The site action template user column.</param>
        public void Delete(tbl_SiteActionTemplateUserColumn siteActionTemplateUserColumn)
        {
            _dataContext.DeleteObject(siteActionTemplateUserColumn);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified site action template user column.
        /// </summary>
        /// <param name="siteActionTemplateUserColumn">The site action template user column.</param>
        public void Update(tbl_SiteActionTemplateUserColumn siteActionTemplateUserColumn)
        {
            var updatesiteActionTemplateUserColumn = SelectById(siteActionTemplateUserColumn.ID);
            updatesiteActionTemplateUserColumn.SiteEventTemplateID = siteActionTemplateUserColumn.SiteEventTemplateID;
            updatesiteActionTemplateUserColumn.SiteColumnID = siteActionTemplateUserColumn.SiteColumnID;
            updatesiteActionTemplateUserColumn.StringValue = siteActionTemplateUserColumn.StringValue;
            updatesiteActionTemplateUserColumn.DateValue = siteActionTemplateUserColumn.DateValue;
            updatesiteActionTemplateUserColumn.SiteColumnValueID = siteActionTemplateUserColumn.SiteColumnValueID;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified site action template user column.
        /// </summary>
        /// <param name="siteActionTemplateUserColumn">The site action template user column.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplateUserColumn Add(tbl_SiteActionTemplateUserColumn siteActionTemplateUserColumn)
        {
            siteActionTemplateUserColumn.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActionTemplateUserColumn.AddObject(siteActionTemplateUserColumn);
            _dataContext.SaveChanges();
            return siteActionTemplateUserColumn;
        }
    }
}