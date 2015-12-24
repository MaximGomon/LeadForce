using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Labitec.DataFeed;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;
using Labitec.DataFeed.Enum;

namespace WebCounter.Service
{
    /// <summary>
    /// Summary description for externalFormHandler
    /// </summary>
    public class externalFormHandler : IHttpHandler
    {
        private DataManager dataManager = new DataManager();

        public void ProcessRequest(HttpContext context)
        {            
            Guid _siteActivityRuleExternalFormID;
            var async = false;

            if (!string.IsNullOrEmpty(context.Request.QueryString["async"]) && context.Request.QueryString["async"] == "true")
                async = true;            

            if (Guid.TryParse(context.Request.QueryString["ID"], out _siteActivityRuleExternalFormID))            
            {
                try
                {
                    var siteActivityRuleExternalForm = dataManager.SiteActivityRuleExternalForms.SelectById(_siteActivityRuleExternalFormID);

                    var siteActivityRule = dataManager.SiteActivityRules.SelectById(siteActivityRuleExternalForm.SiteActivityRuleID);
                    var siteID = siteActivityRule.SiteID;
                    var contactID = async ? Guid.Parse(context.Request.Form["WebCounterUserID"]) : Guid.Parse(context.Request.Params["WebCounterUserID"]);


                    var fields = new List<tbl_SiteActivityRuleExternalFormFields>();
                    fields = dataManager.SiteActivityRuleExternalFormFields.SelectByExternalFormId(_siteActivityRuleExternalFormID).Where(a => a.SiteColumnID != null || a.SysField != null).ToList();

                    var values = context.Request.Params.AllKeys.Select(key => new KeyValuePair<string, string>(key, context.Request.Params[key])).ToList();
                    ExternalFormService.ProceedExternalForm(siteID, _siteActivityRuleExternalFormID, siteActivityRule.Code, contactID, values);

                    /*var contact = dataManager.Contact.SelectById(siteID, contactID);
                    foreach (var field in fields)
                    {
                        tbl_ContactColumnValues contactColumnValue = null;
                        if (field.SiteColumnID != null)
                        {
                            contactColumnValue = dataManager.ContactColumnValues.Select(contactID, (Guid) field.SiteColumnID);
                            if (contactColumnValue == null)
                            {
                                contactColumnValue = new tbl_ContactColumnValues();
                                contactColumnValue.ContactID = contactID;
                                contactColumnValue.SiteColumnID = (Guid) field.SiteColumnID;
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
                                            var nameCheck = nameChecker.CheckName(context.Request.Params[field.Name], NameCheckerFormat.FIO, Correction.Correct);
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
                                                contact.UserFullName = context.Request.Params[field.Name];
                                            break;
                                        case "sys_email":
                                            contact.Email = context.Request.Params[field.Name];
                                            break;
                                        case "sys_phone":
                                            contact.Phone = context.Request.Params[field.Name];
                                            break;
                                        case "sys_surname":
                                            contact.Surname = context.Request.Params[field.Name];
                                            contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                            break;
                                        case "sys_name":
                                            contact.Name = context.Request.Params[field.Name];
                                            contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                            break;
                                        case "sys_patronymic":
                                            contact.Patronymic = context.Request.Params[field.Name];
                                            contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                            break;
                                    }
                                }
                                else
                                {
                                    if (contactColumnValue != null)
                                        contactColumnValue.StringValue = context.Request.Params[field.Name];
                                }
                                break;
                            case FormFieldType.Select:
                                var siteColumnValues = dataManager.SiteColumnValues.SelectAll((Guid)field.SiteColumnID);
                                var columnValue = siteColumnValues.Where(a => a.Value.ToLower() == context.Request.Params[field.Name].ToLower()).FirstOrDefault();
                                if (columnValue == null)
                                {
                                    var newColumnValue = new tbl_SiteColumnValues
                                    {
                                        SiteColumnID = (Guid)field.SiteColumnID,
                                        Value = context.Request.Params[field.Name]
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
                        dataManager.Contact.Update(contact);
                    }*/

                    //CounterServiceHelper.AddContactActivity(siteID, contactID, ActivityType.FillForm, siteActivityRule.Code);

                    if (!async)
                    {
                        /*context.Response.Clear();
                        context.Response.Write("<html><head>");
                        context.Response.Write(String.Format("</head><body onload='document.{0}.submit()'>Отправка запроса...", "ExternalForm"));
                        context.Response.Write(String.Format("<form name='{0}' method='post' action='{1}'>", "ExternalForm", siteActivityRule.RepostURL));

                        foreach (var key in context.Request.Params.AllKeys)
                            context.Response.Write(String.Format("<input type='hidden' name='{0}' value='{1}' />", key, context.Request.Params[key]));

                        context.Response.Write("</form>");
                        context.Response.Write("</body></html>");*/

                        context.Response.Redirect(siteActivityRule.RepostURL, false);
                    }
                    else
                        context.Response.Write("success");
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    context.Response.Write("error");
                }
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["lead_id"]))
            {
                try
                {
                    var referrer = context.Request.UrlReferrer.ToString();
                    var siteActivityRule = dataManager.SiteActivityRules.SelectByUrl(referrer);

                    var contactActivityByIP = dataManager.ContactActivity.SelectByIPAndUrl(siteActivityRule.SiteID, context.Request.UserHostAddress, referrer,
                                                               ActivityType.ViewPage, DateTime.Now.AddMinutes(-10),
                                                               DateTime.Now.AddMinutes(10)).FirstOrDefault();
                    if (contactActivityByIP != null)
                    {
                        var values = context.Request.Params.AllKeys.Select(key => new KeyValuePair<string, string>(key, context.Request.Params[key])).ToList();
                        ExternalFormService.ProceedExternalForm(siteActivityRule.SiteID,
                                                                siteActivityRule.tbl_SiteActivityRuleExternalForms.FirstOrDefault().ID, siteActivityRule.Code,
                                                                contactActivityByIP.ContactID,
                                                                values);

                        if (!string.IsNullOrEmpty(siteActivityRule.RepostURL))
                            context.Response.Redirect(siteActivityRule.RepostURL, false);
                        else
                            context.Response.Write("Форма успешно заполнена.");  
                    }
                    else
                    {
                        Log.Error(string.Format("Лог действий не найден Referrer: {0}; User IP: {1}", referrer, context.Request.UserHostAddress));
                        context.Response.Write("Возникла ошибка при обработке формы");    
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    context.Response.Write("error");
                }                
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}