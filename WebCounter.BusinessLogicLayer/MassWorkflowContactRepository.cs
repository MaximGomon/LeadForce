using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class MassWorkflowContactRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassWorkflowContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MassWorkflowContactRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="massWorkflowContactId">The mass workflow contact id.</param>
        /// <returns></returns>
        public tbl_MassWorkflowContact SelectById(Guid massWorkflowContactId)
        {
            return _dataContext.tbl_MassWorkflowContact.SingleOrDefault(a => a.ID == massWorkflowContactId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_MassWorkflowContact> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_MassWorkflowContact.Where(o => o.tbl_MassWorkflow.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by mass workflow id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="massWorkflowId">The mass workflow id.</param>
        /// <returns></returns>
        public IQueryable<tbl_MassWorkflowContact> SelectByMassWorkflowId(Guid massWorkflowId)
        {
            return _dataContext.tbl_MassWorkflowContact.Where(a => a.MassWorkflowID == massWorkflowId);
        }



        /// <summary>
        /// Adds the specified mass workflow contact.
        /// </summary>
        /// <param name="massWorkflowContact">The mass workflow contact.</param>
        /// <returns></returns>
        public tbl_MassWorkflowContact Add(tbl_MassWorkflowContact massWorkflowContact)
        {
            if (massWorkflowContact.ID == Guid.Empty)
                massWorkflowContact.ID = Guid.NewGuid();
            _dataContext.tbl_MassWorkflowContact.AddObject(massWorkflowContact);
            _dataContext.SaveChanges();

            return massWorkflowContact;
        }



        /// <summary>
        /// Updates the specified mass workflow contact.
        /// </summary>
        /// <param name="massWorkflowContact">The mass workflow contact.</param>
        public void Update(tbl_MassWorkflowContact massWorkflowContact)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified contact list.
        /// </summary>
        /// <param name="contactList">The contact list.</param>
        /// <param name="massWorkflowId">The mass workflow id.</param>
        public void Save(List<Guid> contactList, Guid massWorkflowId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_MassWorkflowContact WHERE MassWorkflowID = @MassWorkflowID", new SqlParameter { ParameterName = "MassWorkflowID", Value = massWorkflowId });

            if (contactList != null && contactList.Count > 0)
            {
                foreach (var contactId in contactList)
                {
                    if (contactId == Guid.Empty)
                        continue;

                    _dataContext.tbl_MassWorkflowContact.AddObject(new tbl_MassWorkflowContact
                    {
                        ID = Guid.NewGuid(),
                        MassWorkflowID = massWorkflowId,
                        ContactID = contactId
                    });
                }
            }
            _dataContext.SaveChanges();
        }
    }
}