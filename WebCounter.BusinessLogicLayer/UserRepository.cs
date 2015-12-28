using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class UserRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UserRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by login password.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public List<tbl_User> SelectByLoginPassword(string login, string password)
        {
            var users = _dataContext.tbl_User.Where(a => a.Login == login && a.Password == password).ToList();
            return users;
        }



        /// <summary>
        /// Selects the by login password.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public tbl_User SelectByLoginPassword(Guid siteId, string login, string password)
        {
            return _dataContext.tbl_User.SingleOrDefault(a => a.SiteID == siteId && a.Login == login && a.Password == password);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_User SelectById(Guid id, Guid? siteID = null)
        {
            if (siteID != null)
                return _dataContext.tbl_User.SingleOrDefault(a => a.SiteID == siteID && a.ID == id);
            return _dataContext.tbl_User.SingleOrDefault(a => a.ID == id);
        }



        /// <summary>
        /// Selects the by email.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public tbl_User SelectByEmail(Guid siteId, string email)
        {
            return _dataContext.tbl_User.FirstOrDefault(u => u.SiteID == siteId && u.Login.ToLower() == email.ToLower());
        }



        /// <summary>
        /// Selects the by contact id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public tbl_User SelectByContactId(Guid siteId, Guid contactId)
        {            
            return _dataContext.tbl_User.SingleOrDefault(a => a.SiteID == siteId && a.ContactID == contactId);
        }



        /// <summary>
        /// Selects the by contact id extended.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public tbl_User SelectByContactIdExtended(Guid siteId, Guid contactId)
        {
            var dataManager = new DataManager();
            var contact = dataManager.Contact.SelectById(siteId, contactId);
            return  SelectByContactId(siteId, contactId) ?? SelectByEmail(siteId, contact.Email);
        }



        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public tbl_User Add(tbl_User user)
        {
            if (user.ID == Guid.Empty)
                user.ID = Guid.NewGuid();
            _dataContext.tbl_User.AddObject(user);
            _dataContext.SaveChanges();

            return user;
        }



        /// <summary>
        /// Adds the portal user.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public tbl_User AddPortalUser(Guid siteId, Guid contactId)
        {
            tbl_User user = null;

            var contact = _dataContext.tbl_Contact.SingleOrDefault(c => c.ID == contactId && c.SiteID == siteId);

            if (contact != null)
            {
                user = SelectByEmail(siteId, contact.Email) ?? Add(new tbl_User
                                                                       {
                                                                           ID = Guid.NewGuid(),
                                                                           SiteID = siteId,
                                                                           ContactID = contactId,
                                                                           Login = contact.Email,
                                                                           Password = string.Empty,
                                                                           IsActive = true,
                                                                           AccessLevelID = (int)AccessLevel.Portal
                                                                       });
            }

            return user;
        }




        /// <summary>
        /// Invites the portal user.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="taskId">The task id.</param>
        /// <param name="siteActionTemplate">The site action template.</param>
        /// <returns></returns>
        public tbl_User InvitePortalUser(Guid siteId, Guid contactId, Guid taskId, tbl_SiteActionTemplate siteActionTemplate)
        {
            var user = _dataContext.tbl_User.SingleOrDefault(u => u.ContactID == contactId && u.SiteID == siteId) ??
                       AddPortalUser(siteId, contactId);

            if (user != null)
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
                                         ActionStatusID = (int) ActionStatus.Scheduled,
                                         ActionDate = DateTime.Now,
                                         ObjectID = taskId,
                                         MessageTypeID = (int) MessageType.TaskNotification,
                                         DirectionID = (int) Direction.Out,
                                         MessageTitle = siteActionTemplate.Title
                                     };

                dataManager.SiteAction.Add(siteAction);
            }

            return user;
        }



        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="name">The name.</param>
        /// <param name="login">The login.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="ip">The ip.</param>
        /// <returns></returns>
        public Guid RegisterUser(Guid siteId, string name, string login, string email, string password, string ip = null)
        {
            var dataManager = new DataManager();
            var status = dataManager.Status.SelectDefault(siteId) ?? dataManager.Status.SelectAll(siteId).FirstOrDefault();
            
            var contact = new tbl_Contact
                              {
                                  SiteID = siteId,
                                  CreatedAt = DateTime.Now,
                                  RefferURL = string.Empty,
                                  UserIP = ip ?? string.Empty,
                                  UserFullName = name,
                                  Email = email,
                                  StatusID = status.ID,
                                  Score = 0,
                                  BehaviorScore = 0,
                                  CharacteristicsScore = 0,
                                  IsNameChecked = false
                              };

            dataManager.Contact.Add(contact);

            var user = new tbl_User
                           {
                               SiteID = siteId,
                               ContactID = contact.ID,
                               Login = login,
                               Password = password,
                               IsActive = false,
                               AccessLevelID = (int) AccessLevel.Portal
                           };
            Add(user);

            return contact.ID;
        }



        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Update(tbl_User user)
        {
            _dataContext.SaveChanges();
        }
    }
}