using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteTagsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteTagsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteTags> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_SiteTags.Where(st => st.SiteID == siteId);
        }



        /// <summary>
        /// Selects the name of the by object.
        /// </summary>
        /// <param name="objectTypeName">Name of the object type.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteTags> SelectByObjectName(string objectTypeName)
        {
            return _dataContext.tbl_SiteTags.Where(st => st.tbl_ObjectTypes.Name == objectTypeName);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteTagId">The site tag id.</param>
        /// <returns></returns>
        public tbl_SiteTags SelectById(Guid siteTagId)
        {
            return _dataContext.tbl_SiteTags.FirstOrDefault(st => st.ID == siteTagId);
        }



        /// <summary>
        /// Adds the specified site tag.
        /// </summary>
        /// <param name="siteTag">The site tag.</param>
        /// <returns></returns>
        public tbl_SiteTags Add(tbl_SiteTags siteTag)
        {
            if (siteTag.ID == Guid.Empty)
                siteTag.ID = Guid.NewGuid();
            _dataContext.tbl_SiteTags.AddObject(siteTag);
            _dataContext.SaveChanges();

            return siteTag;
        }



        /// <summary>
        /// Updates the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        public void Update(tbl_SiteTags segment)
        {
            _dataContext.SaveChanges();
        }

        public void Delete(Guid siteTagId)
        {
            var siteTag = _dataContext.tbl_SiteTags.FirstOrDefault(st => st.ID == siteTagId);
            if (siteTag != null)
            {
                _dataContext.DeleteObject(siteTag);
                _dataContext.SaveChanges();
            }
        }
    }
}