using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;
using System.Data.SqlClient;

namespace WebCounter.BusinessLogicLayer
{
    public class MassMailRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassMailRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MassMailRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_MassMail> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_MassMail.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="massMailID">The mass mail ID.</param>
        /// <returns></returns>
        public tbl_MassMail SelectById(Guid siteID, Guid massMailID)
        {
            return _dataContext.tbl_MassMail.SingleOrDefault(a => a.SiteID == siteID && a.ID == massMailID);
        }



        /// <summary>
        /// Selects the by site action template id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActionTemplateId">The site action template id.</param>
        /// <returns></returns>
        public tbl_MassMail SelectBySiteActionTemplateId(Guid siteId, Guid siteActionTemplateId)
        {
            return _dataContext.tbl_MassMail.Where(mm => mm.SiteID == siteId && mm.SiteActionTemplateID == siteActionTemplateId).FirstOrDefault();
        }



        /// <summary>
        /// Adds the specified mass mail.
        /// </summary>
        /// <param name="massMail">The mass mail.</param>
        /// <returns></returns>
        public tbl_MassMail Add(tbl_MassMail massMail)
        {
            massMail.ID = Guid.NewGuid();
            _dataContext.tbl_MassMail.AddObject(massMail);
            _dataContext.SaveChanges();

            return massMail;
        }



        /// <summary>
        /// Updates the specified mass mail.
        /// </summary>
        /// <param name="massMail">The mass mail.</param>
        public void Update(tbl_MassMail massMail)
        {
            _dataContext.SaveChanges();
        }


        public void DeleteByID(Guid siteId, Guid mailID)
        {
            var mailRecord = SelectById(siteId, mailID);
            if (mailRecord != null)
            {
                _dataContext.DeleteObject(mailRecord);
                _dataContext.SaveChanges();
            }
        }

    }
}