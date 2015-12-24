using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequirementRepository
    {
        private WebCounterEntities _dataContext;
        protected static List<tbl_Requirement> hierarchyCategories = new List<tbl_Requirement>();
        protected static List<tbl_Requirement> linearCategories = new List<tbl_Requirement>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequirementRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Requirement> SelectAll(Guid siteId)
        {            
            return _dataContext.tbl_Requirement.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the responsibles.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="currentContactId">The current contact id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Contact> SelectResponsibles(Guid siteId, Guid? currentContactId)
        {
            return _dataContext.tbl_Contact.Where(o =>
                    o.SiteID == siteId && (_dataContext.tbl_Requirement.Where(r => r.SiteID == siteId).Select(x => x.ResponsibleID).Contains(
                        o.ID) || o.ID == currentContactId));
        }



        /// <summary>
        /// Selects the hierarchy.
        /// </summary>
        /// <returns></returns>
        public List<tbl_Requirement> SelectHierarchy(Guid siteId)
        {
            linearCategories = SelectAll(siteId).ToList();
            hierarchyCategories = new List<tbl_Requirement>();
            BuildCategoryHierarchy(null);
            return hierarchyCategories;
        }



        /// <summary>
        /// Builds the category hierarchy.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        protected void BuildCategoryHierarchy(Guid? parentId)
        {
            var cat = linearCategories.Where(c => c.ParentID == parentId);
            foreach (var item in cat)
            {
                hierarchyCategories.Add(item);
                BuildCategoryHierarchy(item.ID);
            }
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requirementId">The requirement id.</param>
        /// <returns></returns>
        public tbl_Requirement SelectById(Guid siteId, Guid requirementId)
        {
            return _dataContext.tbl_Requirement.SingleOrDefault(o => o.SiteID == siteId && o.ID == requirementId);
        }



        /// <summary>
        /// Selects the by request id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requestId">The request id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Requirement> SelectByRequestId(Guid siteId, Guid requestId)
        {
            return _dataContext.tbl_Requirement.Where(o => o.SiteID == siteId && o.RequestID == requestId);
        }



        /// <summary>
        /// Selects the by contact id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Requirement> SelectByContactId(Guid siteId, Guid contactId)
        {
            return _dataContext.tbl_Requirement.Where(o => o.SiteID == siteId && o.ContactID == contactId);
        }



        /// <summary>
        /// Selects the personal.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Requirement> SelectPersonal(Guid contactId, DateTime startDate, DateTime endDate)
        {
            //Личные требования – требования, где данный контакт указан в поле Контакт или Ответственный

            return _dataContext.tbl_Requirement.Where(o => (!o.tbl_RequirementStatus.IsLast ||
                     o.tbl_RequirementHistory.Any(h => h.CreatedAt >= startDate && h.CreatedAt <= endDate)) &&
                    (o.ResponsibleID == contactId || o.ContactID == contactId));
        }



        /// <summary>
        /// Selects all by company id.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Requirement> SelectAllByCompanyId(Guid companyId, DateTime startDate, DateTime endDate)
        {
            //Все требования – требования, где указана компания такая, 
            //что контакт входит в список контактных лиц к соглашению по данной компании.

            return _dataContext.tbl_Requirement.Where(o => (!o.tbl_RequirementStatus.IsLast ||
                                                            o.tbl_RequirementHistory.Any(h => h.CreatedAt >= startDate && h.CreatedAt <= endDate)) &&
                                                           o.CompanyID == companyId);
        }



        /// <summary>
        /// Selects the by period.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Requirement> SelectByPeriod(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return _dataContext.tbl_Requirement.Where(o => (!o.tbl_RequirementStatus.IsLast ||
                                                            o.tbl_RequirementHistory.Any(h => h.CreatedAt >= startDate && h.CreatedAt <= endDate)) &&
                                                           o.SiteID == siteId);
        }



        /// <summary>
        /// Adds the specified requirement.
        /// </summary>
        /// <param name="requirement">The requirement.</param>        
        /// <returns></returns>
        public tbl_Requirement Add(tbl_Requirement requirement)
        {
            requirement.ID = Guid.NewGuid();
            _dataContext.tbl_Requirement.AddObject(requirement);
            _dataContext.SaveChanges();

            AddHistory(new DataManager(), requirement);

            return requirement;
        }



        /// <summary>
        /// Updates the specified requirement.
        /// </summary>
        /// <param name="requirement">The requirement.</param>        
        public void Update(tbl_Requirement requirement)
        {
            var dataManager = new DataManager();
            var requirementInDataBase = dataManager.Requirement.SelectById(requirement.SiteID, requirement.ID);

            _dataContext.SaveChanges();

            if (requirementInDataBase.RequirementStatusID != requirement.RequirementStatusID || requirementInDataBase.ResponsibleID != requirement.ResponsibleID)
            {
                AddHistory(dataManager, requirement);
            }

            if (requirementInDataBase.ResponsibleID != requirement.ResponsibleID)
                RequestNotificationService.ChangeResponsible(requirement.SiteID, requirement.ID, (Guid)requirement.ResponsibleID);
        }



        /// <summary>
        /// Adds the history.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="requirement">The requirement.</param>
        protected void AddHistory(DataManager dataManager, tbl_Requirement requirement)
        {
            var requirementHistory = new tbl_RequirementHistory
            {
                RequirementID = requirement.ID,
                RequirementStatusID = requirement.RequirementStatusID,
                ContactID = CurrentUser.Instance.ContactID,
                ResponsibleID = requirement.ResponsibleID
            };

            dataManager.RequirementHistory.Add(requirementHistory);
        }
    }
}