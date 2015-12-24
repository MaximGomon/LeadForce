using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActionRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActionRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteAction> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_SiteAction.Where(a => a.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActionID">The site action ID.</param>
        /// <returns></returns>
        public tbl_SiteAction SelectById(Guid siteId, Guid siteActionID)
        {
            return _dataContext.tbl_SiteAction.SingleOrDefault(a => a.SiteID == siteId && a.ID == siteActionID);
        }



        /// <summary>
        /// Selects the by site action template ID.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActionTemplateID">The site action template ID.</param>
        /// <returns></returns>
        public List<tbl_SiteAction> SelectBySiteActionTemplateID(Guid siteId, Guid siteActionTemplateID)
        {
            return _dataContext.tbl_SiteAction.Where(a => a.SiteID == siteId && a.SiteActionTemplateID == siteActionTemplateID).ToList();
        }



        /// <summary>
        /// Selects the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactID">The user ID.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public List<tbl_SiteAction> Select(Guid siteId, Guid? contactID = null, ActionStatus? actionStatus = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var siteAction = _dataContext.tbl_SiteAction.Where(a => a.SiteID == siteId);

            if (contactID != null)
                siteAction = siteAction.Where(a => a.ContactID == contactID);
            if (actionStatus != null)
                siteAction = siteAction.Where(a => a.ActionStatusID == (int)actionStatus);
            if (startDate != null)
                siteAction = siteAction.Where(a => a.ActionDate >= (DateTime)startDate);
            if (endDate != null)
                siteAction = siteAction.Where(a => a.ActionDate <= (DateTime)endDate);

            return siteAction.OrderByDescending(a => a.ActionDate).ToList();
        }



        /// <summary>
        /// Selects the last task notification.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public tbl_SiteAction SelectLastTaskNotification(Guid siteId, Guid contactId)
        {
            return
                _dataContext.tbl_SiteAction.Where(
                    sa => sa.SiteID == siteId && sa.ContactID == contactId && sa.MessageTypeID == (int) MessageType.TaskNotification).OrderByDescending(sa => sa.ActionDate).FirstOrDefault();
        }


        /// <summary>
        /// Adds the specified site action.
        /// </summary>
        /// <param name="siteAction">The site action.</param>
        /// <returns></returns>
        public tbl_SiteAction Add(tbl_SiteAction siteAction)
        {
            siteAction.ID = Guid.NewGuid();
            _dataContext.tbl_SiteAction.AddObject(siteAction);
            _dataContext.SaveChanges();

            return siteAction;
        }



        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="siteActionTemplate">The site action template.</param>
        /// <param name="messageType">Type of the message.</param>
        public void AddNotification(Guid siteId, Guid portalSettingsId, Guid contactId, tbl_SiteActionTemplate siteActionTemplate, MessageType messageType)
        {
            var dataManager = new DataManager();
            var siteActionTemaplate = dataManager.SiteActionTemplate.SelectBySystemName(siteId, siteActionTemplate.SystemName);

            if (siteActionTemaplate == null)
            {
                siteActionTemaplate = siteActionTemplate;
                siteActionTemaplate.SiteID = siteId;
                siteActionTemaplate = dataManager.SiteActionTemplate.Add(siteActionTemaplate);
            }

            var siteAction = new tbl_SiteAction
            {
                SiteID = siteId,
                SiteActionTemplateID = siteActionTemaplate.ID,
                ContactID = contactId,
                ActionStatusID = (int)ActionStatus.Scheduled,
                ActionDate = DateTime.Now,
                ObjectID = portalSettingsId,
                MessageTypeID = (int)messageType,
                DirectionID = (int)Direction.Out,
                MessageTitle = siteActionTemplate.Title
            };
            
            dataManager.SiteAction.Add(siteAction);
        }



        /// <summary>
        /// Determines whether [is exist POP message] [the specified pop message id].
        /// </summary>
        /// <param name="popMessageId">The pop message id.</param>
        /// <returns>
        ///   <c>true</c> if [is exist POP message] [the specified pop message id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExistPOPMessage(string popMessageId)
        {
            return _dataContext.tbl_SiteAction.Where(sa => sa.POPMessageID == popMessageId).Select(sa => sa.POPMessageID).FirstOrDefault() != null;
        }



        /// <summary>
        /// Updates the specified site action.
        /// </summary>
        /// <param name="siteAction">The site action.</param>
        public void Update(tbl_SiteAction siteAction)
        {
            var updateSiteAction = SelectById(siteAction.SiteID, siteAction.ID);
            updateSiteAction.SiteActionTemplateID = siteAction.SiteActionTemplateID;
            updateSiteAction.ContactID = siteAction.ContactID;
            updateSiteAction.ActionStatusID = siteAction.ActionStatusID;
            updateSiteAction.ActionDate = siteAction.ActionDate;
            updateSiteAction.ResponseDate = siteAction.ResponseDate;
            updateSiteAction.Comments = siteAction.Comments;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site action.
        /// </summary>
        /// <param name="siteAction">The site action.</param>
        public void Delete(tbl_SiteAction siteAction)
        {
            _dataContext.DeleteObject(siteAction);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Selects the by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteAction> SelectByEmail(string email)
        {
            var lastWeek = DateTime.Now.AddDays(-7);

            return
                _dataContext.tbl_SiteAction.Where(o => o.ContactID.HasValue && o.ActionDate > lastWeek).Join(
                    _dataContext.tbl_Contact.Where(o => o.Email == email), (s => s.ContactID), (c => c.ID),
                    ((s, c) => new {SiteActions = s})).Select(o => o.SiteActions).OrderByDescending(o => o.ActionDate);
        }



        /// <summary>
        /// Selects the by contact id.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteAction> SelectByContactId(Guid contactId)
        {
            var lastWeek = DateTime.Now.AddDays(-7);

            return _dataContext.tbl_SiteAction.Where(o => o.ActionDate > lastWeek && o.ContactID.HasValue && o.ContactID == contactId).OrderByDescending(o => o.ActionDate);
        }
    }
}