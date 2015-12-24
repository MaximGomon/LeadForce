using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ContactCommunicationRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactCommunicationRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="ContactCommunicationID">The contact communication ID.</param>
        /// <returns></returns>
        public tbl_ContactCommunication SelectById(Guid ContactCommunicationId)
        {
            return _dataContext.tbl_ContactCommunication.Where(a => a.ID == ContactCommunicationId).FirstOrDefault();
        }


        /// <summary>
        /// Selects the type of the by.
        /// </summary>
        /// <param name="ContactId">The contact id.</param>
        /// <param name="CommunicationTypeId">The communication type id.</param>
        /// <returns></returns>
        public tbl_ContactCommunication SelectByType(Guid ContactId, int CommunicationTypeId)
        {
            return
                _dataContext.tbl_ContactCommunication.Where(
                    a => a.ContactID == ContactId && a.CommunicationType == CommunicationTypeId).SingleOrDefault();
        }


        /// <summary>
        /// Deletes the specified access profile record ID.
        /// </summary>
        /// <param name="ContactCommunicationID">The contact communication ID.</param>
        public void Delete(Guid ContactCommunicationId)
        {
            var ContactCommunication = SelectById(ContactCommunicationId);
            if (ContactCommunication != null)
            {
                _dataContext.DeleteObject(ContactCommunication);
                _dataContext.SaveChanges();
            }
        }


        /// <summary>
        /// Updates the specified site user.
        /// </summary>
        /// <param name="ContactCommunication">The contact communication.</param>
        public void Update(tbl_ContactCommunication ContactCommunication)
        {
            _dataContext.SaveChanges();
        }


        /// <summary>
        /// Adds the specified contact communication.
        /// </summary>
        /// <param name="contactCommunication">The contact communication.</param>
        public void Add(tbl_ContactCommunication contactCommunication)
        {
            contactCommunication.ID = Guid.NewGuid();
            _dataContext.tbl_ContactCommunication.AddObject(contactCommunication);
            _dataContext.SaveChanges();
        }
    }
}