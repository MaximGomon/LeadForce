using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteActivityRuleExternalFormFieldsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActivityRuleExternalFormFieldsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteActivityRuleExternalFormFieldsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleExternalFormFields SelectById(Guid id)
        {
            return _dataContext.tbl_SiteActivityRuleExternalFormFields.Where(a => a.ID == id).FirstOrDefault();
        }



        /// <summary>
        /// Selects the by external form id.
        /// </summary>
        /// <param name="siteActivityRuleExternalFormID">The site activity rule external form ID.</param>
        /// <returns></returns>
        public List<tbl_SiteActivityRuleExternalFormFields> SelectByExternalFormId(Guid siteActivityRuleExternalFormID)
        {
            return _dataContext.tbl_SiteActivityRuleExternalFormFields.Where(a => a.SiteActivityRuleExternalFormID == siteActivityRuleExternalFormID).ToList();
        }



        /// <summary>
        /// Selects the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="siteActivityRuleExternalFormID">The site activity rule external form ID.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleExternalFormFields Select(string name, Guid siteActivityRuleExternalFormID)
        {
            return _dataContext.tbl_SiteActivityRuleExternalFormFields.FirstOrDefault(a => a.Name == name && a.SiteActivityRuleExternalFormID == siteActivityRuleExternalFormID);
        }



        /// <summary>
        /// Selects the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="siteActivityRuleExternalFormId">The site activity rule external form id.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleExternalFormFields Select(Guid id, Guid siteActivityRuleExternalFormId)
        {
            return _dataContext.tbl_SiteActivityRuleExternalFormFields.FirstOrDefault(a => a.ID == id && a.SiteActivityRuleExternalFormID == siteActivityRuleExternalFormId);
        }



        /// <summary>
        /// Adds the specified site activity rule external form field.
        /// </summary>
        /// <param name="siteActivityRuleExternalFormField">The site activity rule external form field.</param>
        /// <returns></returns>
        public tbl_SiteActivityRuleExternalFormFields Add(tbl_SiteActivityRuleExternalFormFields siteActivityRuleExternalFormField)
        {
            if (siteActivityRuleExternalFormField.ID == Guid.Empty)
                siteActivityRuleExternalFormField.ID = Guid.NewGuid();
            _dataContext.tbl_SiteActivityRuleExternalFormFields.AddObject(siteActivityRuleExternalFormField);
            _dataContext.SaveChanges();

            return siteActivityRuleExternalFormField;
        }



        /// <summary>
        /// Updates the specified site activity rule external form field.
        /// </summary>
        /// <param name="siteActivityRuleExternalFormField">The site activity rule external form field.</param>
        public void Update(tbl_SiteActivityRuleExternalFormFields siteActivityRuleExternalFormField)
        {
            var updateSiteActivityRuleExternalFormFields = SelectById(siteActivityRuleExternalFormField.ID);
            updateSiteActivityRuleExternalFormFields.SiteActivityRuleExternalFormID = siteActivityRuleExternalFormField.SiteActivityRuleExternalFormID;
            updateSiteActivityRuleExternalFormFields.Name = siteActivityRuleExternalFormField.Name;
            updateSiteActivityRuleExternalFormFields.SiteColumnID = siteActivityRuleExternalFormField.SiteColumnID;
            updateSiteActivityRuleExternalFormFields.FieldType = siteActivityRuleExternalFormField.FieldType;
            updateSiteActivityRuleExternalFormFields.SysField = siteActivityRuleExternalFormField.SysField;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified site activity rule external form field.
        /// </summary>
        /// <param name="siteActivityRuleExternalFormField">The site activity rule external form field.</param>
        /// <param name="siteActivityRuleExternalFormId">The site activity rule external form id.</param>
        public void Update(tbl_SiteActivityRuleExternalFormFields siteActivityRuleExternalFormField, Guid siteActivityRuleExternalFormId)
        {
            var updateSiteActivityRuleExternalFormFields = Select(siteActivityRuleExternalFormField.ID, siteActivityRuleExternalFormId);

            if (updateSiteActivityRuleExternalFormFields == null)
                updateSiteActivityRuleExternalFormFields = Select(siteActivityRuleExternalFormField.Name, siteActivityRuleExternalFormId);

            updateSiteActivityRuleExternalFormFields.SiteActivityRuleExternalFormID = siteActivityRuleExternalFormField.SiteActivityRuleExternalFormID;
            updateSiteActivityRuleExternalFormFields.Name = siteActivityRuleExternalFormField.Name;
            updateSiteActivityRuleExternalFormFields.SiteColumnID = siteActivityRuleExternalFormField.SiteColumnID;
            updateSiteActivityRuleExternalFormFields.FieldType = siteActivityRuleExternalFormField.FieldType;
            updateSiteActivityRuleExternalFormFields.SysField = siteActivityRuleExternalFormField.SysField;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified site activity rule external form field.
        /// </summary>
        /// <param name="siteActivityRuleExternalFormField">The site activity rule external form field.</param>
        public void Delete(tbl_SiteActivityRuleExternalFormFields siteActivityRuleExternalFormField)
        {
            _dataContext.DeleteObject(siteActivityRuleExternalFormField);
            _dataContext.SaveChanges();
        }
    }
}