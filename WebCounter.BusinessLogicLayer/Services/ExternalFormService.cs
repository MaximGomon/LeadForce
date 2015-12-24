using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Labitec.DataFeed.Common;
using Labitec.DataFeed.Enum;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;
using Labitec.DataFeed;
using WufooSharp;

namespace WebCounter.BusinessLogicLayer.Services
{
    public class ExternalFormService
    {
        public static void ProceedExternalForm(Guid siteId, Guid siteActivityRuleExternalFormId, string code, Guid? contactId, IEnumerable<KeyValuePair<string, string>> values)
        {
            ProceedExternalForm(siteId, siteActivityRuleExternalFormId, code, contactId, values, string.Empty, DateTime.Now);
        }



        /// <summary>
        /// Proceeds the external form.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActivityRuleExternalFormId">The site activity rule external form id.</param>
        /// <param name="code">The code.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="values">The values.</param>
        /// <param name="key">The key.</param>
        /// <param name="actionDate">The action date.</param>
        public static void ProceedExternalForm(Guid siteId, Guid siteActivityRuleExternalFormId, string code, Guid? contactId, IEnumerable<KeyValuePair<string, string>> values, string key, DateTime actionDate)
        {
            //
            // !!! Если contactId передаётся HasValue - внешняя форма; если contactId is null - форма Wufoo
            //
            var dataManager = new DataManager();

            var fields = dataManager.SiteActivityRuleExternalFormFields.SelectByExternalFormId(siteActivityRuleExternalFormId).Where(a => a.SiteColumnID != null || a.SysField != null).ToList();

            tbl_Contact contact = null;
            
            string ip = string.Empty;

            if (contactId.HasValue)
                contact = dataManager.Contact.SelectById(siteId, contactId.Value);
            else
            {                
                if (!string.IsNullOrEmpty(key))
                {
                    var importKey = dataManager.ImportKey.SelectByKey(key, "tbl_Contact");
                    if (importKey != null)                    
                        return;
                }

                ip = values.SingleOrDefault(o => o.Key == "IP").Value;

                var contactActivityByIP = dataManager.ContactActivity.SelectByIP(siteId, ip, ActivityType.ViewPage, actionDate.AddMinutes(-10), actionDate.AddMinutes(10)).FirstOrDefault();

                if (contactActivityByIP == null)
                {
                    var status = dataManager.Status.SelectDefault(siteId) ??
                                 dataManager.Status.SelectAll(siteId).FirstOrDefault();

                    contact = new tbl_Contact
                                  {
                                      SiteID = siteId,
                                      CreatedAt = actionDate,
                                      RefferURL = string.Empty,
                                      UserIP = ip,
                                      UserFullName = string.Empty,
                                      Email = string.Empty,
                                      StatusID = status.ID,
                                      Score = 0,
                                      BehaviorScore = 0,
                                      CharacteristicsScore = 0,
                                      IsNameChecked = false,
                                      LastActivityAt = actionDate
                                  };
                    dataManager.Contact.Add(contact);
                }
                else
                {
                    contact = dataManager.Contact.SelectById(siteId, contactActivityByIP.ContactID);
                }

                dataManager.ImportKey.Add(new tbl_ImportKey
                                    {
                                        ImportID = siteActivityRuleExternalFormId,
                                        LeadForceID = contact.ID,
                                        TableName = "tbl_Contact",
                                        Key = key
                                    });
                
            }

            var keyValuesPairs = new List<KeyValuePair<string, string>>();
            foreach (var field in fields)
            {
                keyValuesPairs.Add(field.SysField != null
                                       ? new KeyValuePair<string, string>(field.SysField, values.SingleOrDefault(o => o.Key == field.Name).Value)
                                       : new KeyValuePair<string, string>(field.SiteColumnID.ToString(), values.SingleOrDefault(o => o.Key == field.Name).Value));
            }

            var contactData = new ContactData(siteId);
            contact = contactData.SaveForm(contact.ID, keyValuesPairs);


            /*foreach (var field in fields)
            {
                tbl_ContactColumnValues contactColumnValue = null;
                if (field.SiteColumnID != null)
                {
                    contactColumnValue = dataManager.ContactColumnValues.Select(contact.ID, (Guid)field.SiteColumnID);
                    if (contactColumnValue == null)
                    {
                        contactColumnValue = new tbl_ContactColumnValues();
                        contactColumnValue.ContactID = contact.ID;
                        contactColumnValue.SiteColumnID = (Guid)field.SiteColumnID;
                    }
                }


                switch ((FormFieldType)field.FieldType)
                {
                    case FormFieldType.Input:
                    case FormFieldType.Textarea:
                        if (field.SysField != null)
                        {
                            switch (field.SysField)
                            {
                                case "sys_fullname":
                                    var nameChecker = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                                    var nameCheck = nameChecker.CheckName(values.SingleOrDefault(o => o.Key == field.Name).Value, NameCheckerFormat.FIO, Correction.Correct);
                                    if (!string.IsNullOrEmpty(nameCheck))
                                    {
                                        contact.UserFullName = nameCheck;
                                        contact.Surname = nameChecker.Surname;
                                        contact.Name = nameChecker.Name;
                                        contact.Patronymic = nameChecker.Patronymic;
                                        contact.IsNameChecked = nameChecker.IsNameCorrect;
                                        if (nameChecker.Gender.HasValue)
                                            contact.Gender = (int)nameChecker.Gender.Value;
                                    }
                                    else
                                        contact.UserFullName = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    break;
                                case "sys_email":
                                    contact.Email = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    break;
                                case "sys_phone":
                                    contact.Phone = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    break;
                                case "sys_jobtitle":
                                    contact.JobTitle = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    break;
                                case "sys_keywords":
                                    keywords = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    break;
                                case "sys_refferurl":
                                    contact.RefferURL = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    break;
                                case "sys_advertisingplatform":
                                    if (!string.IsNullOrEmpty(values.SingleOrDefault(o => o.Key == field.Name).Value))                                    
                                        contact.AdvertisingPlatformID = dataManager.AdvertisingPlatform.SelectByTitleAndCreate(contact.SiteID, values.SingleOrDefault(o => o.Key == field.Name).Value).ID;
                                    break;
                                case "sys_advertisingtype":
                                    if (!string.IsNullOrEmpty(values.SingleOrDefault(o => o.Key == field.Name).Value))                                    
                                        contact.AdvertisingTypeID = dataManager.AdvertisingType.SelectByTitleAndCreate(contact.SiteID, values.SingleOrDefault(o => o.Key == field.Name).Value).ID;
                                    break;
                                case "sys_advertisingcampaign":
                                    if (!string.IsNullOrEmpty(values.SingleOrDefault(o => o.Key == field.Name).Value))
                                        contact.AdvertisingCampaignID = dataManager.AdvertisingCampaign.SelectByTitleAndCreate(contact.SiteID, values.SingleOrDefault(o => o.Key == field.Name).Value).ID;
                                    break;                                    
                                case "sys_comment":
                                    contact.Comment = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    break;
                                case "sys_surname":
                                    contact.Surname = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                    break;
                                case "sys_name":
                                    contact.Name = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                    break;
                                case "sys_patronymic":
                                    contact.Patronymic = values.SingleOrDefault(o => o.Key == field.Name).Value;
                                    contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                    break;
                            }
                        }
                        else
                        {
                            if (contactColumnValue != null)
                                contactColumnValue.StringValue = values.SingleOrDefault(o => o.Key == field.Name).Value;
                        }
                        break;
                    case FormFieldType.Select:
                        var siteColumnValues = dataManager.SiteColumnValues.SelectAll((Guid)field.SiteColumnID);
                        var columnValue = siteColumnValues.Where(a => a.Value.ToLower() == values.SingleOrDefault(o => o.Key == field.Name).Value.ToLower()).FirstOrDefault();
                        if (columnValue == null)
                        {
                            var newColumnValue = new tbl_SiteColumnValues
                            {
                                SiteColumnID = (Guid)field.SiteColumnID,
                                Value = values.SingleOrDefault(o => o.Key == field.Name).Value
                            };
                            columnValue = dataManager.SiteColumnValues.Add(newColumnValue);
                        }

                        if (contactColumnValue != null)
                            contactColumnValue.SiteColumnValueID = columnValue.ID;
                        break;
                }

                if (contactColumnValue != null)
                {
                    if (contactColumnValue.ID == Guid.Empty)
                        dataManager.ContactColumnValues.Add(contactColumnValue);
                    else
                        dataManager.ContactColumnValues.Update(contactColumnValue);
                }
                //dataManager.Contact.Update(contact);
            }

            dataManager.Contact.Update(contact);*/

            CounterServiceHelper.AddContactActivity(siteId, contact.ID, ActivityType.FillForm, actionDate, code, ip);
            
            if (!contactId.HasValue)
            {
                var contactSession = dataManager.ContactSessions.SelectFirstSession(contact.SiteID, contact.ID);
                if (contactSession != null)
                {
                    //contactSession.Keywords = keywords;
                    if (values.Any(o => o.Key == "sys_keywords"))
                        contactSession.Keywords = values.SingleOrDefault(o => o.Key == "sys_keywords").Value;
                    contactSession.AdvertisingCampaignID = contact.AdvertisingCampaignID;
                    contactSession.AdvertisingPlatformID = contact.AdvertisingPlatformID;
                    contactSession.AdvertisingTypeID = contact.AdvertisingTypeID;
                    contactSession.RefferURL = contact.RefferURL;
                    contactSession.UserIP = contact.UserIP;
                    dataManager.ContactSessions.Update(contactSession);
                } 
            }
        }



        /// <summary>
        /// Wufooes the load data.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="siteActivityRuleId">The site activity rule id.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static WufooFormLoadDataResult WufooLoadData(Guid siteId, Guid siteActivityRuleId, DateTime date)
        {
            var result = new WufooFormLoadDataResult();

            var dataManager = new DataManager();

            var siteActivityRule = dataManager.SiteActivityRules.SelectById(siteId, siteActivityRuleId);

            var externalForm = dataManager.SiteActivityRuleExternalForms.Select(siteActivityRule.Code, siteActivityRuleId);

            if (externalForm == null || !externalForm.tbl_SiteActivityRuleExternalFormFields.Any())
            {                
                result.Message = "Перед тем как загружать данные, нужно привязать поля на вкладке 'Форма Wufoo'.";
                return result;
            }

            var client = new WufooClient(siteActivityRule.WufooName, siteActivityRule.WufooAPIKey);
            var wufooForm = client.GetAllForms().FirstOrDefault(o => o.Hash == siteActivityRule.Code);
            var fb = new FilterBuilder(FilterMatchType.And);
            fb.IsAfter("DateCreated", date);
            var revisionDate = DateTime.Now;
            if (wufooForm != null)
            {
                var entries =
                    client.GetEntriesByFormId(wufooForm.Hash, fb, new Sort("DateCreated", SortDirection.Asc)).ToList();
                var users = client.GetAllUsers();

                var user = users.Count() > 1 ? users.FirstOrDefault(o => o.IsAccountOwner) : users.FirstOrDefault();
                var serverTimezone = (DateTime.Now - DateTime.UtcNow).TotalHours;
                var wufooTimezone = user.TimeZone;
                
                foreach (var entry in entries)
                {
                    ProceedExternalForm(siteId, externalForm.ID, wufooForm.Hash,
                                        null, entry.Responses,
                                        string.Format("{0}${1}${2}", siteId, wufooForm.Hash, entry.EntryId),
                                        entry.DateCreated.AddHours(-(double) wufooTimezone).AddHours(serverTimezone));
                }
            }
            siteActivityRule.WufooRevisionDate = revisionDate;
            dataManager.SiteActivityRules.Update(siteActivityRule);
            result.RevisionDate = revisionDate;

            return result;
        }
    }


    public class WufooFormLoadDataResult
    {
        public string Message { get; set; }
        public DateTime RevisionDate { get; set; }
    }
}
