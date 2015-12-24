using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActionAttachmentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActionAttachmentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified site action.
        /// </summary>
        /// <param name="siteActionAttachment">The site action attachment.</param>
        /// <returns></returns>
        public tbl_SiteActionAttachment Add(tbl_SiteActionAttachment siteActionAttachment)
        {
            siteActionAttachment.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActionAttachment.AddObject(siteActionAttachment);
            _dataContext.SaveChanges();

            return siteActionAttachment;
        }



        /// <summary>
        /// Updates the specified site action attacment.
        /// </summary>
        /// <param name="siteActionAttacment">The site action attacment.</param>
        public void Update(tbl_SiteActionAttachment siteActionAttacment)
        {
            var toUpdate = _dataContext.tbl_SiteActionAttachment.SingleOrDefault(o => o.ID == siteActionAttacment.ID);
            if (toUpdate != null)
            {
                toUpdate.FileName = siteActionAttacment.FileName;
                _dataContext.SaveChanges();
            }
        }
    }
}