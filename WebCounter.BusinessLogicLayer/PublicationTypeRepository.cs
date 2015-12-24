using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PublicationTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationTypeRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PublicationTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_PublicationType> SelectAll()
        {
            return _dataContext.tbl_PublicationType.ToList();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PublicationType> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PublicationType.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by ids.
        /// </summary>
        /// <param name="publicationTypeIds">The publication type ids.</param>
        /// <returns></returns>
        public List<tbl_PublicationType> SelectByIds(List<Guid> publicationTypeIds)
        {
            return _dataContext.tbl_PublicationType.Where(o => publicationTypeIds.Contains(o.ID)).ToList();
        }



        /// <summary>
        /// Selects the type by id.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <returns></returns>
        public tbl_PublicationType SelectById(Guid typeId)
        {
            return _dataContext.tbl_PublicationType.SingleOrDefault(pc => pc.ID == typeId);
        }


        /// <summary>
        /// Selects the type by id.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <returns></returns>
        public tbl_PublicationType SelectByTitle(string Title)
        {
            return _dataContext.tbl_PublicationType.SingleOrDefault(pc => pc.Title == Title);
        }



        /// <summary>
        /// Selects the by publication kind ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="publicationKindId">The publication kind id.</param>
        /// <returns></returns>
        public List<tbl_PublicationType> SelectByPublicationKindID(Guid siteID, int publicationKindId)
        {
           return _dataContext.tbl_PublicationType.Where(a => a.SiteID == siteID && a.PublicationKindID == publicationKindId).ToList();
        }
    }
}