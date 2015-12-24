using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActivityRuleExternalFormsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActivityRuleExternalFormsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActivityRuleExternalFormsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleExternalForms SelectById(Guid id)
        {
            return _dataContext.tbl_SiteActivityRuleExternalForms.Where(a => a.ID == id).FirstOrDefault();
        }



        /// <summary>
        /// Selects the by rule id.
        /// </summary>
        /// <param name="siteActivityRuleID">The site activity rule ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActivityRuleExternalForms> SelectByRuleId(Guid siteActivityRuleID)
        {
            return _dataContext.tbl_SiteActivityRuleExternalForms.Where(a => a.SiteActivityRuleID == siteActivityRuleID).ToList();
        }



        /// <summary>
        /// Selects the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleExternalForms Select(string name, Guid siteActivityRuleID)
        {
            return _dataContext.tbl_SiteActivityRuleExternalForms.Where(a => a.Name == name && a.SiteActivityRuleID == siteActivityRuleID).FirstOrDefault();
        }



        /// <summary>
        /// Adds the specified site activity rule external form.
        /// </summary>
        /// <param name="siteActivityRuleExternalForm">The site activity rule external form.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleExternalForms Add(tbl_SiteActivityRuleExternalForms siteActivityRuleExternalForm)
        {
            if (siteActivityRuleExternalForm.ID == Guid.Empty)
                siteActivityRuleExternalForm.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActivityRuleExternalForms.AddObject(siteActivityRuleExternalForm);
            _dataContext.SaveChanges();

            return siteActivityRuleExternalForm;
        }



        /// <summary>
        /// Updates the specified site activity rule external forms.
        /// </summary>
        /// <param name="siteActivityRuleExternalForms">The site activity rule external forms.</param>
        public void Update(tbl_SiteActivityRuleExternalForms siteActivityRuleExternalForms)
        {
            var updateSiteActivityRuleExternalForms = SelectById(siteActivityRuleExternalForms.ID);
            updateSiteActivityRuleExternalForms.Name = siteActivityRuleExternalForms.Name;
            updateSiteActivityRuleExternalForms.SiteActivityRuleID = siteActivityRuleExternalForms.SiteActivityRuleID;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site activity rule external form.
        /// </summary>
        /// <param name="siteActivityRuleExternalForm">The site activity rule external form.</param>
        public void Delete(tbl_SiteActivityRuleExternalForms siteActivityRuleExternalForm)
        {
            _dataContext.DeleteObject(siteActivityRuleExternalForm);
            _dataContext.SaveChanges();
        }
    }
}