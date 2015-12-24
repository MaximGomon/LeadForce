using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PublicationStatusRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationStatusRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PublicationStatusRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_PublicationStatus> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_PublicationStatus.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the type by id.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <returns></returns>
        public tbl_PublicationStatus SelectById(Guid statusId)
        {
            return _dataContext.tbl_PublicationStatus.Single(pc => pc.ID == statusId);
        }



        /// <summary>
        /// Selects the by publication kind ID.
        /// </summary>
        /// <param name="publicationTypeID">The publication type ID.</param>
        /// <returns></returns>
        public List<tbl_PublicationStatus> SelectByPublicationTypeID(Guid publicationTypeID)
        {
            return
                _dataContext.tbl_PublicationStatus.Where(
                    a =>
                    a.tbl_PublicationStatusToPublicationType.Select(p => p.PublicationTypeID).Contains(publicationTypeID))
                    .OrderBy(a => a.Order).ToList();
        }
    }
}