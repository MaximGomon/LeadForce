using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActivityRulesRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActivityRulesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActivityRulesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified site activity rules.
        /// </summary>
        /// <param name="siteActivityRules">The site activity rules.</param>
        /// <returns></returns>
        public tbl_SiteActivityRules Add(tbl_SiteActivityRules siteActivityRules)
        {
            if (siteActivityRules.ID == Guid.Empty)
                siteActivityRules.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActivityRules.AddObject(siteActivityRules);
            _dataContext.SaveChanges();

            return siteActivityRules;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActivityRules> SelectAll(Guid siteID)
        {
            return _dataContext.tbl_SiteActivityRules.Where(a => a.SiteID == siteID).ToList();
        }



        /// <summary>
        /// Selects all templates.
        /// </summary>
        /// <returns></returns>
        public List<tbl_SiteActivityRules> SelectAllTemplates()
        {
            return _dataContext.tbl_SiteActivityRules.Where(a => a.tbl_Sites.IsTemplate).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <returns></returns>
        public tbl_SiteActivityRules SelectById(Guid siteActivityRulesID)
        {
            return _dataContext.tbl_SiteActivityRules.SingleOrDefault(a => a.ID == siteActivityRulesID);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActivityRulesID">The site activity rules ID.</param>
        /// <returns></returns>
        public tbl_SiteActivityRules SelectById(Guid siteId, Guid siteActivityRulesID)
        {
            return _dataContext.tbl_SiteActivityRules.SingleOrDefault(a => a.ID == siteActivityRulesID && a.SiteID == siteId);
        }
        


        /// <summary>
        /// Selects the specified site ID.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="siteActivityRulesCode">The site activity rules code.</param>
        /// <returns></returns>
        public tbl_SiteActivityRules Select(Guid siteID, string siteActivityRulesCode)
        {
            return _dataContext.tbl_SiteActivityRules.FirstOrDefault(a => a.SiteID == siteID && a.Code == siteActivityRulesCode);
        }



        /// <summary>
        /// Selects the type of the by rule.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="ruleTypes">The rule types.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteActivityRules> SelectByRuleType(Guid siteID, List<int> ruleTypes)
        {
            return _dataContext.tbl_SiteActivityRules.Where(a => ruleTypes.Contains(a.RuleTypeID) && a.SiteID == siteID);
        }



        /// <summary>
        /// Updates the specified site user.
        /// </summary>
        /// <param name="siteUser">The site user.</param>
        public void Update(tbl_SiteActivityRules siteUser)
        {
            _dataContext.SaveChanges();
        }


        /// <summary>
        /// Selects the by code.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public int SelectByCode(Guid siteID, string code)
        {
            var maxCode = _dataContext.tbl_SiteActivityRules.Where(a => a.Code == code && a.SiteID == siteID).Count();
            return maxCode;
        }



        /// <summary>
        /// Selects the by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public tbl_SiteActivityRules SelectByUrl(string url)
        {            
            return _dataContext.tbl_SiteActivityRules.FirstOrDefault(o => url.Contains(o.ExternalFormURL) || o.ExternalFormURL.Contains(url));
        }



        /// <summary>
        /// Selects the form by code.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public tbl_SiteActivityRules SelectFormByCode(Guid siteId, string code)
        {
            return _dataContext.tbl_SiteActivityRules.SingleOrDefault(a => a.Code == code && a.SiteID == siteId);
        }



        /// <summary>
        /// Deletes the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        public void DeleteByID(Guid siteId, Guid id)
        {
            var siteActivityRule = SelectById(siteId, id);
            if (siteActivityRule != null)
            {
                var siteActivityRuleLayouts = _dataContext.tbl_SiteActivityRuleLayout.Where(a => a.SiteActivityRuleID == id);
                if (siteActivityRuleLayouts.Any())
                {
                    foreach (var siteActivityRuleLayout in siteActivityRuleLayouts)
                    {
                        _dataContext.tbl_SiteActivityRuleLayout.DeleteObject(siteActivityRuleLayout);
                    }
                }
                var siteColumnValues = _dataContext.tbl_SiteColumnValues.Where(a => a.tbl_SiteColumns.SiteActivityRuleID == id);
                if (siteColumnValues.Any())
                {
                    foreach (var siteColumnValue in siteColumnValues)
                    {
                        _dataContext.tbl_SiteColumnValues.DeleteObject(siteColumnValue);
                    }
                }

		var contactColumnValues = _dataContext.tbl_ContactColumnValues.Where(o => o.tbl_SiteColumns.SiteActivityRuleID == id);
                if (contactColumnValues.Any())                
                    foreach (var columnValue in contactColumnValues)                    
                        _dataContext.tbl_ContactColumnValues.DeleteObject(columnValue);                

                var siteColumns = _dataContext.tbl_SiteColumns.Where(a => a.SiteActivityRuleID == id);
                if (siteColumns.Any())
                {
                    foreach (var siteColumn in siteColumns)
                    {
                        _dataContext.tbl_SiteColumns.DeleteObject(siteColumn);
                    }
                }
                var siteActivityRuleExternalFormFields = _dataContext.tbl_SiteActivityRuleExternalFormFields.Where(a => a.tbl_SiteActivityRuleExternalForms.SiteActivityRuleID == id);
                if (siteActivityRuleExternalFormFields.Any())
                {
                    foreach (
                        var siteActivityRuleExternalFormField in
                            siteActivityRuleExternalFormFields)
                    {
                        _dataContext.tbl_SiteActivityRuleExternalFormFields.DeleteObject(siteActivityRuleExternalFormField);
                    }
                }
                var siteActivityRuleExternalForms = _dataContext.tbl_SiteActivityRuleExternalForms.Where(a => a.SiteActivityRuleID == id);
                if (siteActivityRuleExternalForms.Any())
                {
                    foreach (var siteActivityRuleExternalForm in siteActivityRuleExternalForms)
                    {
                        _dataContext.tbl_SiteActivityRuleExternalForms.DeleteObject(siteActivityRuleExternalForm);
                    }
                }
                _dataContext.DeleteObject(siteActivityRule);
                _dataContext.SaveChanges();
            }

        }


        /// <summary>
        /// Deletes the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        public tbl_SiteActivityRules CopyByID(Guid id)
        {
            var siteActivityRule = SelectById(id);
            if (siteActivityRule != null)
            {
                var newSiteActivityRule = new tbl_SiteActivityRules();
                newSiteActivityRule = siteActivityRule;
                _dataContext.Detach(newSiteActivityRule);
                newSiteActivityRule.ID = new Guid();
                newSiteActivityRule = Add(newSiteActivityRule);
                NameValueCollection id2id = new NameValueCollection();
                var ruleLayouts = _dataContext.tbl_SiteActivityRuleLayout.Where(a=>a.SiteActivityRuleID == id).OrderBy(a=>a.Order);
                if (ruleLayouts.Any())
                {
                    foreach (var ruleLayout in ruleLayouts)
                    {
                        _dataContext.Detach(ruleLayout);
                        var newId = Guid.NewGuid();
                        id2id.Add(ruleLayout.ID.ToString(), newId.ToString());
                        ruleLayout.ID = newId;
                        ruleLayout.SiteActivityRuleID = newSiteActivityRule.ID;
                        string newParrentId = id2id[ruleLayout.ParentID.ToString()];
                        if (newParrentId != null)
                        {
                            ruleLayout.ParentID = Guid.Parse(newParrentId);
                        }

                        _dataContext.tbl_SiteActivityRuleLayout.AddObject(ruleLayout);
                    }

                    _dataContext.SaveChanges();
                }                
                //ruleLayouts = _dataContext.tbl_SiteActivityRuleLayout.Where(a => a.SiteActivityRuleID == newSiteActivityRule.ID).OrderBy(a => a.Order);
                //if (ruleLayouts.Any())
                //{
                //    foreach (var ruleLayout in ruleLayouts)
                //    {
                //        if (ruleLayout.ParentID != null)
                //        {
                //            string newParrentId = id2id[ruleLayout.ParentID.ToString()];
                //            if (newParrentId != null)
                //            {
                //                ruleLayout.ParentID = Guid.Parse(newParrentId);
                //            }
                //        }
                //    }
                //}
                
                var siteColumns = _dataContext.tbl_SiteColumns.Where(a => a.SiteActivityRuleID == id);
                if (siteColumns.Any())
                {
                    foreach (var siteColumn in siteColumns)
                    {
                        _dataContext.Detach(siteColumn);                        
                        var oldId = siteColumn.ID;
                        siteColumn.ID = Guid.NewGuid();                        
                        siteColumn.SiteActivityRuleID = newSiteActivityRule.ID;
                        _dataContext.tbl_SiteColumns.AddObject(siteColumn);
                        var siteColumnValues = _dataContext.tbl_SiteColumnValues.Where(a => a.SiteColumnID == oldId);
                        if (siteColumnValues.Any())
                        {
                            foreach (var siteColumnValue in siteColumnValues)
                            {
                                _dataContext.Detach(siteColumnValue);
                                siteColumnValue.ID = Guid.NewGuid();
                                siteColumnValue.SiteColumnID = siteColumn.ID;
                                _dataContext.tbl_SiteColumns.AddObject(siteColumn);
                            }
                        }

                        foreach (var activityRuleLayout in _dataContext.tbl_SiteActivityRuleLayout.Where(o => o.SiteActivityRuleID == newSiteActivityRule.ID && o.SiteColumnID == oldId))                        
                            activityRuleLayout.SiteColumnID = siteColumn.ID;
                    }
                }
                var siteActivityRuleExternalForms = _dataContext.tbl_SiteActivityRuleExternalForms.Where(a => a.SiteActivityRuleID == id);
                if (siteActivityRuleExternalForms.Any())
                {
                    foreach (var siteActivityRuleExternalForm in siteActivityRuleExternalForms)
                    {
                        _dataContext.Detach(siteActivityRuleExternalForm);
                        var oldId = siteActivityRuleExternalForm.ID;
                        siteActivityRuleExternalForm.ID = Guid.NewGuid();
                        siteActivityRuleExternalForm.SiteActivityRuleID = newSiteActivityRule.ID;
                        _dataContext.tbl_SiteActivityRuleExternalForms.AddObject(siteActivityRuleExternalForm);
                        var siteActivityRuleExternalFormFields = _dataContext.tbl_SiteActivityRuleExternalFormFields.Where(a => a.SiteActivityRuleExternalFormID == oldId);
                        if (siteActivityRuleExternalFormFields.Any())
                        {
                            foreach (var siteActivityRuleExternalFormField in siteActivityRuleExternalFormFields)
                            {
                                _dataContext.Detach(siteActivityRuleExternalFormField);
                                siteActivityRuleExternalFormField.ID = Guid.NewGuid();
                                siteActivityRuleExternalFormField.SiteActivityRuleExternalFormID = siteActivityRuleExternalForm.ID;
                                _dataContext.tbl_SiteActivityRuleExternalFormFields.AddObject(siteActivityRuleExternalFormField);
                            }
                        }
                    }
                }
                _dataContext.SaveChanges();
                return newSiteActivityRule;
            }
            return new tbl_SiteActivityRules();
        }
    }
}