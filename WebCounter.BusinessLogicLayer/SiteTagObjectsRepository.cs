using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteTagObjectsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteTagObjectsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteTagObjectsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the specified tag id.
        /// </summary>
        /// <param name="tagId">The tag id.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        public tbl_SiteTagObjects Select(Guid tagId, Guid objectId)
        {
            return _dataContext.tbl_SiteTagObjects.SingleOrDefault(a => a.SiteTagID == tagId && a.ObjectID == objectId);
        }



        /// <summary>
        /// Selects the specified tag id.
        /// </summary>
        /// <param name="tagId">The tag id.</param>
        /// <returns></returns>
        public List<tbl_SiteTagObjects> SelectByTagId(Guid tagId)
        {
            return _dataContext.tbl_SiteTagObjects.Where(a => a.SiteTagID == tagId).ToList();
        }


        /// <summary>
        /// Selects the by tag ID.
        /// </summary>
        /// <param name="tagId">The tag id.</param>
        /// <returns></returns>
        public List<Guid> SelectIdsByTagID(List<Guid> tagIds)
        {
            return _dataContext.tbl_SiteTagObjects.Where(a => tagIds.Contains(a.SiteTagID)).Select(a => a.ObjectID).ToList();
        }


        /// <summary>
        /// Selects the by tag ID.
        /// </summary>
        /// <param name="tagIds">The tag ids.</param>
        /// <returns></returns>
        public List<Guid> SelectIdsByTagID(Guid tagIds)
        {
            return _dataContext.tbl_SiteTagObjects.Where(a => a.SiteTagID == tagIds).Select(a => a.ObjectID).ToList();
        }



        /// <summary>
        /// Adds the specified site tag object.
        /// </summary>
        /// <param name="siteTagObject">The site tag object.</param>
        /// <returns></returns>
        public tbl_SiteTagObjects Add(tbl_SiteTagObjects siteTagObject)
        {
            if (Select(siteTagObject.SiteTagID, siteTagObject.ObjectID) == null)
            {
                siteTagObject.ID = Guid.NewGuid();
                _dataContext.tbl_SiteTagObjects.AddObject(siteTagObject);
                _dataContext.SaveChanges();
            }

            return siteTagObject;
        }



        /// <summary>
        /// Deletes the specified site tag object.
        /// </summary>
        /// <param name="siteTagObject">The site tag object.</param>
        public void Delete(tbl_SiteTagObjects siteTagObject)
        {
            var deleteSiteTagObject = Select(siteTagObject.SiteTagID, siteTagObject.ObjectID);
            if (deleteSiteTagObject != null)
            {
                _dataContext.DeleteObject(deleteSiteTagObject);
                _dataContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified site tag object.
        /// </summary>
        /// <param name="tagId">The tag id.</param>
        public void DeleteByTagID(Guid tagId)
        {
            var toDelete = SelectByTagId(tagId);
            foreach (var tblSiteTagObject in toDelete)
            {
                _dataContext.DeleteObject(tblSiteTagObject);
                _dataContext.SaveChanges();

            }
        }

    }
}