using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.BusinessLogicLayer
{
    public class MassMailContactRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassMailContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MassMailContactRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified mass mail user.
        /// </summary>
        /// <param name="massMailContact">The mass mail contact.</param>
        /// <returns></returns>
        public tbl_MassMailContact Add(tbl_MassMailContact massMailContact)
        {
            massMailContact.ID = Guid.NewGuid();
            _dataContext.tbl_MassMailContact.AddObject(massMailContact);
            _dataContext.SaveChanges();

            return massMailContact;
        }



        /// <summary>
        /// Selects the by mass mail id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="massMailId">The mass mail id.</param>
        /// <returns></returns>
        public List<tbl_MassMailContact> SelectByMassMailId(Guid siteID, Guid massMailId)
        {
            return _dataContext.tbl_MassMailContact.Where(a => a.SiteID == siteID && a.MassMailID == massMailId).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="massMailContactId">The mass mail user id.</param>
        /// <returns></returns>
        public tbl_MassMailContact SelectById(Guid siteID, Guid massMailContactId)
        {
            return _dataContext.tbl_MassMailContact.SingleOrDefault(a => a.SiteID == siteID && a.ID == massMailContactId);
        }



        /// <summary>
        /// Updates the specified mass mail user.
        /// </summary>
        /// <param name="massMailContact">The mass mail contact.</param>
        public void Update(tbl_MassMailContact massMailContact)
        {
            var updateMassMailContact = SelectById(massMailContact.SiteID, massMailContact.ID);
            updateMassMailContact.ID = massMailContact.ID;
            updateMassMailContact.SiteID = massMailContact.SiteID;
            updateMassMailContact.MassMailID = massMailContact.MassMailID;
            updateMassMailContact.ContactID = massMailContact.ContactID;
            updateMassMailContact.SiteActionID = massMailContact.SiteActionID;

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="massMailId">The mass mail id.</param>
        /// <param name="contactId">The user id.</param>
        public void Delete(Guid siteID, Guid massMailId, Guid contactId)
        {
            var massMailContact = _dataContext.tbl_MassMailContact.SingleOrDefault(a => a.SiteID == siteID && a.MassMailID == massMailId && a.ContactID == contactId);
            _dataContext.DeleteObject(massMailContact);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified contact list.
        /// </summary>
        /// <param name="contactList">The contact list.</param>
        /// <param name="massMailId">The mass mail id.</param>
        public void Save(Guid siteId, List<Guid> contactList, Guid massMailId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_MassMailContact WHERE MassMailID = @MassMailID", new SqlParameter { ParameterName = "MassMailID", Value = massMailId });

            if (contactList != null && contactList.Count > 0)
            {
                foreach (var contactId in contactList)
                {
                    if (contactId == Guid.Empty)
                        continue;

                    _dataContext.tbl_MassMailContact.AddObject(new tbl_MassMailContact
                    {
                        ID = Guid.NewGuid(),
                        SiteID = siteId,
                        MassMailID = massMailId,
                        ContactID = contactId
                    });
                }
            }

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds to queue site action.
        /// </summary>
        /// <param name="massMailId">The mass mail id.</param>
        public void AddToQueueSiteAction(Guid massMailId)
        {
            var massMail = _dataContext.tbl_MassMail.SingleOrDefault(a => a.ID == massMailId);
            List<Guid> contactIds;

            if (massMail != null)
            {
                var dc = new WebCounterEntities();
                var siteActionTemplate = dc.tbl_SiteActionTemplate.SingleOrDefault(a => a.ID == massMail.SiteActionTemplateID);

                massMail.MassMailStatusID = (int)MassMailStatus.Done;

                if (massMail.SiteTagID.HasValue)
                {
                    contactIds = _dataContext.tbl_SiteTagObjects.Where(a => a.SiteTagID == massMail.SiteTagID).Select(a => a.ObjectID).ToList();
                    Save(massMail.SiteID, contactIds, massMailId);
                }
                    
                else
                    contactIds = _dataContext.tbl_MassMailContact.Where(a => a.MassMailID == massMailId).Select(a => a.ContactID).ToList();

                foreach (var contactId in contactIds)
                {
                    var siteActionId = Guid.NewGuid();
                    var siteAction = new tbl_SiteAction
                                         {
                                             ID = siteActionId,
                                             SiteID = massMail.SiteID,
                                             SiteActionTemplateID = massMail.SiteActionTemplateID,
                                             ContactID = contactId,
                                             ObjectID = massMail.ID,
                                             MessageTypeID = (int)MessageType.MassMail,
                                             ActionStatusID = (int)ActionStatus.Scheduled,
                                             ActionDate = (DateTime)massMail.MailDate,
                                             OwnerID = massMail.OwnerID,
                                             MessageTitle = siteActionTemplate.MessageCaption,
                                             MessageText = siteActionTemplate.MessageBody
                                         };
                    _dataContext.tbl_SiteAction.AddObject(siteAction);

                    var updateMassMailContact = _dataContext.tbl_MassMailContact.SingleOrDefault(a => a.MassMailID == massMailId && a.ContactID == contactId); 
                    updateMassMailContact.SiteActionID = siteActionId;
                }
            }

            _dataContext.SaveChanges();
        }
    }
}