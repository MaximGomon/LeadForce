using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class LinksRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="LinksRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public LinksRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified site activity rules.
        /// </summary>
        /// <param name="siteActivityRules">The site activity rules.</param>
        /// <returns></returns>
        public tbl_Links Add(tbl_Links siteActivityRules)
        {
            if (siteActivityRules.ID == Guid.Empty)
                siteActivityRules.ID = Guid.NewGuid();
            _dataContext.tbl_Links.AddObject(siteActivityRules);
            _dataContext.SaveChanges();

            return siteActivityRules;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site ID.</param>
        /// <returns></returns>
        public List<tbl_Links> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_Links.Where(a => a.SiteID == siteId).ToList();
        }



        /// <summary>
        /// Selects all templates.
        /// </summary>
        /// <returns></returns>
        public List<tbl_Links> SelectAllTemplates()
        {
            return _dataContext.tbl_Links.Where(a => a.tbl_Sites.IsTemplate).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="linkId">The link id.</param>
        /// <returns></returns>
        public tbl_Links SelectById(Guid linkId)
        {
            return _dataContext.tbl_Links.SingleOrDefault(a => a.ID == linkId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="linkId">The site activity rules ID.</param>
        /// <returns></returns>
        public tbl_Links SelectById(Guid siteId, Guid linkId)
        {
            return _dataContext.tbl_Links.SingleOrDefault(a => a.ID == linkId && a.SiteID == siteId);
        }
        


        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteId">The site ID.</param>
        /// <param name="siteActivityRulesCode">The site activity rules code.</param>
        /// <returns></returns>
        public tbl_Links Select(Guid siteId, string siteActivityRulesCode)
        {
            return _dataContext.tbl_Links.FirstOrDefault(a => a.SiteID == siteId && a.Code == siteActivityRulesCode);
        }



        /// <summary>
        /// Selects the type of the by rule.
        /// </summary>
        /// <param name="siteId">The site ID.</param>
        /// <param name="ruleTypes">The rule types.</param>
        /// <returns></returns>
        public IQueryable<tbl_Links> SelectByRuleType(Guid siteId, List<int> ruleTypes)
        {
            return _dataContext.tbl_Links.Where(a => ruleTypes.Contains(a.RuleTypeID) && a.SiteID == siteId);
        }



        /// <summary>
        /// Updates the specified site user.
        /// </summary>
        /// <param name="siteUser">The site user.</param>
        public void Update(tbl_Links siteUser)
        {
            _dataContext.SaveChanges();
        }


        /// <summary>
        /// Selects the by code.
        /// </summary>
        /// <param name="siteId">The site ID.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public int SelectByCode(Guid siteId, string code)
        {
            var maxCode = _dataContext.tbl_Links.Count(a => a.Code == code && a.SiteID == siteId);
            return maxCode;
        }



        /// <summary>
        /// Selects the form by code.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public tbl_Links SelectFormByCode(Guid siteId, string code)
        {
            return _dataContext.tbl_Links.SingleOrDefault(a => a.Code == code && a.SiteID == siteId);
        }



        /// <summary>
        /// Deletes the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        public void DeleteByID(Guid id)
        {
            var siteActivityRule = SelectById(id);
            if (siteActivityRule != null)
            {                
                _dataContext.DeleteObject(siteActivityRule);
                _dataContext.SaveChanges();
            }

        }
    }
}