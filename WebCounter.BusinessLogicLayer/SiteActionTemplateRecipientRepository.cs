using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActionTemplateRecipientRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionTemplateRecipientRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActionTemplateRecipientRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteActionTemplateRecipientId">The site action template recipient id.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplateRecipient SelectById(Guid siteActionTemplateRecipientId)
        {
            return _dataContext.tbl_SiteActionTemplateRecipient.SingleOrDefault(o => o.ID == siteActionTemplateRecipientId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteActionTemplateId">The site action template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteActionTemplateRecipient> SelectAll(Guid siteActionTemplateId)
        {
            return _dataContext.tbl_SiteActionTemplateRecipient.Where(a => a.SiteActionTemplateID == siteActionTemplateId);
        }



        /// <summary>
        /// Adds the specified site action template recipient.
        /// </summary>
        /// <param name="siteActionTemplateRecipient">The site action template recipient.</param>
        /// <returns></returns>
        public tbl_SiteActionTemplateRecipient Add(tbl_SiteActionTemplateRecipient siteActionTemplateRecipient)
        {
            if (siteActionTemplateRecipient.ID == Guid.Empty)
                siteActionTemplateRecipient.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActionTemplateRecipient.AddObject(siteActionTemplateRecipient);
            _dataContext.SaveChanges();

            return siteActionTemplateRecipient;
        }



        /// <summary>
        /// Adds the specified site action template recipients.
        /// </summary>
        /// <param name="siteActionTemplateRecipients">The site action template recipients.</param>
        /// <param name="siteActionTemplateId">The site action template id.</param>
        public void Save(List<SiteActionTemplateRecipientMap> siteActionTemplateRecipients, Guid siteActionTemplateId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_SiteActionTemplateRecipient WHERE SiteActionTemplateID = @SiteActionTemplateID", new SqlParameter { ParameterName = "SiteActionTemplateID", Value = siteActionTemplateId });

            foreach (var siteActionTemplateRecipient in siteActionTemplateRecipients)
                _dataContext.tbl_SiteActionTemplateRecipient.AddObject(new tbl_SiteActionTemplateRecipient
                {
                    ID = Guid.NewGuid(),
                    SiteActionTemplateID = siteActionTemplateId,
                    ContactID = siteActionTemplateRecipient.ContactID,
                    ContactRoleID = siteActionTemplateRecipient.ContactRoleID,
                    Email = siteActionTemplateRecipient.Email,
                    DisplayName = siteActionTemplateRecipient.DisplayName
                });

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified task duration.
        /// </summary>
        /// <param name="siteActionTemplateRecipient">The site action template recipient.</param>
        public void Update(tbl_SiteActionTemplateRecipient siteActionTemplateRecipient)
        {
            _dataContext.SaveChanges();
        }
    }
}