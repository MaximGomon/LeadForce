using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ResponsibleRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponsibleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ResponsibleRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }




        /// <summary>
        /// Selects the specified contact role id.
        /// </summary>
        /// <param name="contactRoleId">The contact role id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="workflowId">The workflow id.</param>
        /// <returns></returns>
        public tbl_Responsible Select(Guid contactRoleId, Guid contactId, Guid? workflowId = null)
        {
            if (workflowId != null)
                return _dataContext.tbl_Responsible.SingleOrDefault(a => a.ContactRoleID == contactRoleId && a.ContactID == contactId && a.WorkflowID == workflowId);

            return _dataContext.tbl_Responsible.SingleOrDefault(a => a.ContactRoleID == contactRoleId && a.ContactID == contactId);
        }


        /// <summary>
        /// Adds the specified responsible.
        /// </summary>
        /// <param name="responsible">The responsible.</param>
        /// <returns></returns>
        public tbl_Responsible Add(tbl_Responsible responsible)
        {
            if (responsible.ID == Guid.Empty)
                responsible.ID = Guid.NewGuid();
            _dataContext.tbl_Responsible.AddObject(responsible);
            _dataContext.SaveChanges();

            return responsible;
        }



        /// <summary>
        /// Gets the responsible.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactRoleId">The contact role id.</param>
        /// <returns></returns>
        public tbl_Contact GetResponsible(Guid siteId, Guid contactRoleId)
        {
            tbl_Contact contact;
            var contactList = new List<Guid>();
            Guid responsibleId = Guid.Empty;

            var contactRole = _dataContext.tbl_ContactRole.SingleOrDefault(a => a.ID == contactRoleId && a.SiteID == siteId);
            if (contactRole != null)
            {
                if (contactRole.SiteTagID.HasValue)
                {
                    var tagContacts = _dataContext.tbl_SiteTagObjects.Where(a => a.SiteTagID == contactRole.SiteTagID).Select(a => a.ID);
                    foreach (var tagContact in tagContacts)
                    {
                        contact = _dataContext.tbl_Contact.SingleOrDefault(a => a.ID == tagContact);
                        if (contact != null)
                            contactList.Add(contact.ID);
                    }
                }
                else
                {
                    var roleContacts = _dataContext.tbl_ContactToContactRole.Where(a => a.SiteID == siteId && a.ContactRoleID == contactRoleId);
                    foreach (var roleContact in roleContacts)
                    {
                        contact = _dataContext.tbl_Contact.SingleOrDefault(a => a.ID == roleContact.ContactID);
                        if (contact != null)
                            contactList.Add(contact.ID);
                    }
                }

                if (contactList.Count == 0)
                    return null;

                // По очереди
                if (contactRole.MethodAssigningResponsible == 0)
                {
                    contactList = contactList.OrderBy(a => a).ToList();

                    if (contactRole.LastAssignmentResponsible == null)
                        responsibleId = contactList[0];
                    else
                    {
                        var index = contactList.IndexOf((Guid)contactRole.LastAssignmentResponsible);
                        if (index != -1)
                            responsibleId = (index + 1) < contactList.Count ? contactList[index + 1] : contactList[0];
                        else
                        {
                            contactList.Add((Guid)contactRole.LastAssignmentResponsible); // Если уже нет в списке, то добавляем и смотрит кто будет следущим
                            contactList = contactList.OrderBy(a => a).ToList();
                            index = contactList.IndexOf((Guid)contactRole.LastAssignmentResponsible);
                            responsibleId = (index + 1) < contactList.Count ? contactList[index + 1] : contactList[0];
                        }
                    }

                    contactRole.LastAssignmentResponsible = responsibleId;
                }
                else // Наиболее свободного
                {
                    var responsible = _dataContext.tbl_Responsible.FirstOrDefault(a => !contactList.Contains(a.ResponsibleID));
                    if (responsible != null)
                        responsibleId = responsible.ID;
                    else
                    {
                        var responsibleByCount =
                            _dataContext.tbl_Responsible.Where(a => a.ContactRoleID == contactRoleId).
                                GroupBy(g => g.ResponsibleID).
                                Select(r => new {ResponsibleID = r.Key, Count = r.Count()}).
                                OrderBy(o => o.Count).ToList();
                        if (responsibleByCount.Any())
                            responsibleId = responsibleByCount.FirstOrDefault().ResponsibleID;
                    }
                }

                return _dataContext.tbl_Contact.SingleOrDefault(a => a.ID == responsibleId && a.SiteID == siteId);
            }

            return null;
        }
    }
}