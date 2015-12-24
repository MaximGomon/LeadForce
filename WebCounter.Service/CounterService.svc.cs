using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using Labitec.DataFeed.Enum;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;
using System.Linq;
using System.Web.Script.Serialization;
using Labitec.DataFeed;
using System.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.Service
{
    [ServiceBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CounterService : ICounterService
    {
        //private DataManager dataManager = new DataManager();
        private WebCounterServiceRepository repository = new WebCounterServiceRepository();


        /// <summary>
        /// Implement counter service.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="ContactID">The user ID.</param>
        /// <param name="URL">The URL.</param>
        /// <param name="RefferURL">The reffer URL.</param>
        public void LG_CounterService(string SiteID, string ContactID, string URL, string RefferURL, string Resolution)
        {            
            /*var context = OperationContext.Current;
            var properties = context.IncomingMessageProperties;
            var httpRequestMessageProperty = properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            if (httpRequestMessageProperty != null)
            {
                var userAgent = httpRequestMessageProperty.Headers["User-Agent"];
            }*/

            Guid siteIdOut, contactIdOut;

            if (Guid.TryParse(SiteID, out siteIdOut) && Guid.TryParse(ContactID, out contactIdOut))
            {
                if (!IsExistsAccess(siteIdOut, HttpContext.Current.Request.UrlReferrer))
                    return;

                CounterServiceHelper.AddContact(siteIdOut, contactIdOut, ActivityType.ViewPage, URL, RefferURL, Resolution, SessionSource.Select(URL, RefferURL), null);
                //var sessionId = CounterServiceHelper.AddSiteUserSession(siteIdOut, userIdOut, RefferURL, Resolution);
                //CounterServiceHelper.AddSiteUserActivity(siteIdOut, userIdOut, ActivityType.ViewPage, URL, sessionId);
            }
        }



        /// <summary>
        /// Implement helper service.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="ContactID">The user ID.</param>
        /// <param name="URL">The URL.</param>
        /// <returns></returns>
        public string LG_HelperService(string SiteID, string ContactID, string URL)
        {
            return "Введите E-mail:<br /><input type=\"\" value=\"\" />";
        }



        /// <summary>
        /// Ls the g_ link service.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="ContactID">The user ID.</param>
        /// <param name="ActivityCode">The activity code.</param>
        /// <returns></returns>
        public JsonResponse LG_LinkService(string SiteID, string ContactID, string ActivityCode, string register)
        {
            Guid siteIdOut, contactIdOut;

            if (Guid.TryParse(SiteID, out siteIdOut) && Guid.TryParse(ContactID, out contactIdOut))
            {
                if (!IsExistsAccess(siteIdOut, HttpContext.Current.Request.UrlReferrer))
                    return new JsonResponse();

                var siteActivityRule = repository.Links_SelectByCode(siteIdOut, ActivityCode);

                if (siteActivityRule != null)
                {
                    if ((RuleType)siteActivityRule.RuleTypeID == RuleType.Link)
                    {
                        CounterServiceHelper.AddContact(siteIdOut, contactIdOut, ActivityType.Link, ActivityCode, null, null);

                        return new JsonResponse { RuleType = "Link", Value = siteActivityRule.URL };
                    }
                    
                    if ((RuleType)siteActivityRule.RuleTypeID == RuleType.File)
                    {
                        CounterServiceHelper.AddContact(siteIdOut, contactIdOut, ActivityType.DownloadFile, ActivityCode, null, null);

                        var redirectUrl = string.Format("{0}/DownloadFile.aspx?id={1}", WebConfigurationManager.AppSettings["webServiceUrl"], siteActivityRule.ID);
                        return new JsonResponse { RuleType = "File", Value = redirectUrl };
                    }
                }
            }

            return new JsonResponse();
        }



        /// <summary>
        /// Ls the g_ form service.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="ContactID">The user ID.</param>
        /// <param name="FormCode">The form code.</param>
        /// <returns></returns>
        public JsonResponse LG_FormService(string SiteID, string ContactID, string ActivityCode, string Mode, string FromVisit, string Through, string Period, string Parameter, string ContactCategory, string PopupAppear, string register, string ContainerID)
        {
            Guid siteIdOut, contactIdOut;
            var _mode = int.Parse(Mode);
            int? _fromVisit = null;
            int? _through = null;
            int? _period = null;
            var _contactCategory = ContactCategory.ToEnum<FormContactCategory>();


            if (Guid.TryParse(SiteID, out siteIdOut) && Guid.TryParse(ContactID, out contactIdOut))
            {
                if (!IsExistsAccess(siteIdOut, HttpContext.Current.Request.UrlReferrer))
                    return new JsonResponse();                                

                var siteActivityRule = repository.SiteActivityRules_SelectByCode(siteIdOut, ActivityCode);

                if (siteActivityRule != null)
                {                    
                    if (_mode != 1)
                    {
                        if (!string.IsNullOrEmpty(FromVisit)) _fromVisit = int.Parse(FromVisit);
                        if (!string.IsNullOrEmpty(Through)) _through = int.Parse(Through);
                        if (!string.IsNullOrEmpty(Period)) _period = int.Parse(Period);
                        
                        if (!repository.CheckForm(siteIdOut, contactIdOut, ActivityCode, _mode, _fromVisit, _through, _period, _contactCategory))                        
                            return new JsonResponse();                        
                    }

                    var ActivityCodeWithParameter = ActivityCode;
                    if (!string.IsNullOrEmpty(Parameter))
                        ActivityCodeWithParameter = ActivityCodeWithParameter + "#" + Parameter;

                    if (bool.Parse(register))                    
                        CounterServiceHelper.AddContact(siteIdOut, contactIdOut, ActivityType.OpenForm, ActivityCodeWithParameter, null, null);
                    
                    var form = string.Empty;

                    var siteActivityRuleLayouts = repository.SiteActivityRuleLayout_SelectBySiteActivityRuleID(siteActivityRule.ID);
                    var siteActivityRuleLayout = siteActivityRuleLayouts.FirstOrDefault(o => o.LayoutType == (int) LayoutType.Feedback || o.LayoutType == (int) LayoutType.InviteFriend);
                    
                    if (siteActivityRuleLayout == null)
                    {                        
                        //form = GenerateForm(siteIdOut, contactIdOut, ActivityCode, Parameter, siteActivityRule, bool.Parse(register));
                        //form = string.Format("<iframe id='fb-{5}' width='100%' height='10px' src='{0}/FormService.aspx?sid={1}&cid={2}&code={3}&parameter={6}&register={4}' frameborder='0' scrolling='no' onload=\"FrameManager.registerFrame(this, '{0}/FormService.aspx?sid={1}&cid={2}&code={3}&register={4}')\" />", ConfigurationManager.AppSettings["webServiceUrl"], siteIdOut, contactIdOut, ActivityCode, register, siteActivityRule.ID, Parameter);
                        form = string.Format("SocketSetup('{0}/FormService.aspx?sid={1}&cid={2}&code={3}&parameter={4}&register={5}', '{6}', '{7}');", ConfigurationManager.AppSettings["webServiceUrl"], siteIdOut, contactIdOut, ActivityCode, Parameter, register, ContainerID, Mode);
                    }
                    else
                    {                        
                        switch ((LayoutType)siteActivityRuleLayout.LayoutType)
                        {
                            case LayoutType.Feedback:
                                if (!string.IsNullOrEmpty(siteActivityRuleLayout.LayoutParams))
                                    form = string.Format("SocketSetup('{0}?sId={1}&sarId={2}', '{3}', '{4}');", ConfigurationManager.AppSettings["FeedbackFormUrl"], SiteID, siteActivityRule.ID, ContainerID, Mode);
                                    /*form = string.Format(
                                "<form id='CounterServiceForm-{0}'>{4}<iframe id='fb-{0}' src='{1}?sId={2}&sarId={3}' onload=\"FrameManager.registerFrame(this, '{1}?sId={2}&sarId={3}')\" height='10px' width='100px' frameborder='no' /></form>", ActivityCode,
                                ConfigurationManager.AppSettings["FeedbackFormUrl"], SiteID, siteActivityRule.ID, !string.IsNullOrEmpty(siteActivityRule.CSSForm) ? string.Format("<style type=\"text/css\">#WebCounter-dialog {{ {0} }}</style>", siteActivityRule.CSSForm) : string.Empty);*/
                                break;
                            case LayoutType.InviteFriend:
                                form = string.Format("SocketSetup('{0}?sId={1}&sarId={2}&cId={3}', '{4}', '{5}');", string.Format("{0}/SystemForms/InviteFriend/InviteFriends.aspx", ConfigurationManager.AppSettings["webServiceUrl"]), SiteID, siteActivityRule.ID, ContactID, ContainerID, Mode);
                                /*form = string.Format(
                            "<form id='CounterServiceForm-{0}'>{5}<iframe id='fb-{0}' src='{1}?sId={2}&sarId={3}&cId={4}' onload=\"FrameManager.registerFrame(this, '{1}?sId={2}&sarId={3}&cId={4}')\" height='10px' width='100px' frameborder='no' scrolling='no' /></form>", ActivityCode,
                            string.Format("{0}/SystemForms/InviteFriend/InviteFriends.aspx", ConfigurationManager.AppSettings["webServiceUrl"]), SiteID, siteActivityRule.ID, ContactID, !string.IsNullOrEmpty(siteActivityRule.CSSForm) ? string.Format("<style type=\"text/css\">#WebCounter-dialog {{ {0} }}</style>", siteActivityRule.CSSForm) : string.Empty);*/
                                break;
                        }                        
                    }

                    var outPopupAppear = PopupAppear;

                    return new JsonResponse { RuleType = "Form", Value = form, ContainerID = ContainerID, Mode = _mode.ToString(), ActivityCode = ActivityCode, Parameter = Parameter, PopupAppear = outPopupAppear };
                }
            }

            return new JsonResponse();
        }


        /// <summary>
        /// As the p_ form service.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="ContactID">The contact ID.</param>
        /// <param name="ActivityCode">The activity code.</param>
        /// <param name="Mode">The mode.</param>
        /// <param name="ContainerID">The container ID.</param>
        /// <returns></returns>
        public JsonResponse AP_FormService(string SiteID, string ContactID, string ActivityCode, string Mode, string ContainerID)
        {
            Guid siteIdOut, contactIdOut;
            var _mode = int.Parse(Mode);

            if (Guid.TryParse(SiteID, out siteIdOut) && Guid.TryParse(ContactID, out contactIdOut))
            {
                if (!IsExistsAccess(siteIdOut, HttpContext.Current.Request.UrlReferrer))
                    return new JsonResponse();

                var siteActivityRule = repository.SiteActivityRules_SelectByCode(siteIdOut, ActivityCode);

                if (siteActivityRule != null)
                {
                    //var form = GenerateForm(siteIdOut, contactIdOut, ActivityCode, "", siteActivityRule, false);
                    var form = string.Format("SocketSetup('{0}/FormService.aspx?sid={1}&cid={2}&code={3}&parameter=&register={4}&isadmin={6}', 'WebCounter-dialog-container', '{5}');", ConfigurationManager.AppSettings["webServiceUrl"], siteIdOut, contactIdOut, ActivityCode, false, Mode, true);

                    return new JsonResponse { RuleType = "Form", Value = form, ContainerID = ContainerID, Mode = _mode.ToString(), ActivityCode = ActivityCode };
                }
            }

            return new JsonResponse();
        }


        private XDocument xmlForm;
        private int extraFieldCount = 0;
        protected string GenerateForm(Guid siteID, Guid contactID, string ActivityCode, string Parameter, tbl_SiteActivityRules siteActivityRule, bool register)
        {
            var siteActivityRuleLayouts = repository.SiteActivityRuleLayout_SelectBySiteActivityRuleID(siteActivityRule.ID);
            if (siteActivityRuleLayouts.Count > 0)
            {
                xmlForm = new XDocument(new XDeclaration("1.0", "utf-8", "no"));
                var formNode = new XElement("form",
                                            new XAttribute("id", string.Format("CounterServiceForm-{0}", ActivityCode)),
                                            new XAttribute("action", ""),
                                            new XAttribute("method", "POST"),
                                            new XAttribute("class", "clearfix")
                    );
                if (siteActivityRule.FormWidth != null && (int)siteActivityRule.FormWidth > 0)
                    formNode.Add(new XAttribute("style", string.Format("width:{0}px", siteActivityRule.FormWidth)));
                xmlForm.Add(formNode);

                if (!string.IsNullOrEmpty(siteActivityRule.CSSForm))
                {
                    var style = new XElement("style", "",
                        new XAttribute("type", "text/css")
                    );
                    style.Value = string.Format("#WebCounter-dialog {{ {0} }}", siteActivityRule.CSSForm);
                    xmlForm.Descendants("form").First().Add(style);
                }                

                var contactColumnValues = repository.ContactColumnValues_SelectByContactID(contactID);

                //BuildLayout(siteID, contactID, siteActivityRule, siteActivityRuleLayouts, null, contactColumnValues, register);

                var divClear = new XElement("div", "",
                        new XAttribute("class", "clear")
                    );
                xmlForm.Descendants("form").First().Add(divClear);

                var saveBtn = new XElement("a",
                                           new XAttribute("href", "javascript:;"),
                                           new XAttribute("class", "btn"),
                                           new XAttribute("onclick", string.Format("WebCounter.LG_FormServiceResult('{0}', '{1}')", ActivityCode, Parameter)),
                                           //new XElement("span", !string.IsNullOrEmpty(siteActivityRule.TextButton) ? siteActivityRule.TextButton : "Сохранить")
                                           new XElement("em", " "),
                                           new XElement("span", !string.IsNullOrEmpty(siteActivityRule.TextButton) ? siteActivityRule.TextButton : "Сохранить")
                    );
                if (!string.IsNullOrEmpty(siteActivityRule.CSSButton))
                    saveBtn.Add(new XAttribute("style", siteActivityRule.CSSButton));

                xmlForm.Descendants("form").First().Add(saveBtn);

                var emptyNodes = xmlForm.Descendants("div").Where(a => a.IsEmpty).ToList();
                foreach (var emptyNode in emptyNodes)
                {
                    xmlForm.Descendants("div").First(x => (string)x.Attribute("class") == (string)emptyNode.Attribute("class")).Remove();
                    if (xmlForm.Descendants("div").FirstOrDefault(x => ((string)x.Attribute("class")).Replace("hdr-", "") == ((string)emptyNode.Attribute("class")).Replace("csf-", "")) != null)
                        xmlForm.Descendants("div").First(x => ((string)x.Attribute("class")).Replace("hdr-", "") == ((string)emptyNode.Attribute("class")).Replace("csf-", "")).Remove();
                }

                if (!string.IsNullOrEmpty(siteActivityRule.ErrorMessage))
                {
                    var div = new XElement("div", siteActivityRule.ErrorMessage);
                    div.Add(new XAttribute("id", "LFErrorMessage"));
                    div.Add(new XAttribute("style", "display:none;"));                    
                    xmlForm.Descendants("form").First().Add(div);
                }

                return HttpUtility.HtmlDecode(xmlForm.ToString(SaveOptions.DisableFormatting));
            }

            return string.Empty;
        }


        /*
        protected void BuildLayout(Guid siteID, Guid contactID, tbl_SiteActivityRules siteActivityRule, List<SiteActivityRuleLayoutParams> siteActivityRuleLayouts, SiteActivityRuleLayoutParams parentLayout, List<tbl_ContactColumnValues> contactColumnValues, bool register)
        {
            List<SiteActivityRuleLayoutParams> layouts;
            if (parentLayout == null)
                layouts = siteActivityRuleLayouts.Where(a => a.ParentID == null).OrderBy(a => a.Order).ToList();
            else
                layouts = siteActivityRuleLayouts.Where(a => a.ParentID == parentLayout.ID).OrderBy(a => a.Order).ToList();

            foreach (var layout in layouts)
            {
                XElement node;
                XElement div;
                XElement label;
                XElement input;

                var fieldWrapper = new XElement("div", new XAttribute("class", "field-wrapper"));

                if (parentLayout == null)
                    node = xmlForm.Descendants("form").First();
                else
                    node = xmlForm.Descendants("div").First(x => (string)x.Attribute("class") == string.Format("csf-{0}", layout.ParentID));


                switch ((LayoutType)layout.LayoutType)
                {
                    case LayoutType.GroupFields:
                        if ((OutputFormat)layout.OutputFormat == OutputFormat.Header)
                        {
                            node.Add(new XElement("div", new XAttribute("class", string.Format("hdr-{0}", layout.ID)), new XElement("a", layout.Name,
                                                    new XAttribute("href", "javascript:;"),
                                                    new XAttribute("onclick", string.Format("WebCounter.Form.toggleForm('csf-{0}')", layout.ID))
                                                    )
                                                )
                            );
                        }
                        node.Add(new XElement("div", 
                                    new XAttribute("class", string.Format("csf-{0}", layout.ID)), 
                                    new XAttribute("style", "width: 100%; display: table;" + layout.CSSStyle)));                        
                        break;

                    case LayoutType.TextBlock:
                        div = new XElement("div", layout.Description,
                                           new XAttribute("class", string.Format("csf-{0}", layout.ID)));

                        if (!string.IsNullOrEmpty(layout.CSSStyle) && layout.CSSStyle.Trim() != string.Empty)
                            div.Add(new XAttribute("style", layout.CSSStyle));

                        if (!string.IsNullOrEmpty(siteActivityRule.ErrorMessage) && !string.IsNullOrEmpty(layout.LayoutParams))
                        {
                            var layoutParams = LayoutParams.Deserialize(layout.LayoutParams);
                            if (layoutParams.GetBoolValue("IsUsedForErrorMessage"))
                            {
                                if (div.Attribute("style") != null)
                                    div.Attribute("style").Value += ";display:none";
                                else
                                    div.Add(new XAttribute("style", "display:none;"));

                                div.Attribute("style").Value += "padding: 5px 0;color:#b63306";
                                div.Add(new XAttribute("id", "LFErrorMessageTextBlock"));                                
                            }
                        }

                        node.Add(div);                        
                        break;

                    case LayoutType.ProfileField:
                    case LayoutType.FormField:
                        if (register && layout.IsAdmin) continue;

                        div = new XElement("div");
                        label = new XElement("label", string.Format("{0}:", layout.SiteColumnName));

                        if (parentLayout != null)
                        {
                            if ((OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Top
                                || (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Left)
                            {
                                div.Add(label);
                            }
                            if ((OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Top)
                                div.Add(new XElement("br"));

                            var fieldClass = GetFieldClass(parentLayout);
                            if (!string.IsNullOrEmpty(fieldClass))
                                div.Add(new XAttribute("class", fieldClass));
                        }

                        var value = string.Empty;
                        var contactColumnValue = contactColumnValues.SingleOrDefault(a => a.ContactID == contactID && a.SiteColumnID == (Guid)layout.SiteColumnID); //repository.SiteUserColumnValues_Select(userID, (Guid)layout.SiteColumnID);
                        switch ((ColumnType)layout.SiteColumnTypeID)
                        {
                            case ColumnType.String:
                            case ColumnType.Number:
                                if (contactColumnValue != null && !string.IsNullOrEmpty(contactColumnValue.StringValue))
                                    value = contactColumnValue.StringValue;
                                else
                                    if (!string.IsNullOrEmpty(layout.DefaultValue))
                                        value = layout.DefaultValue;

                                input = new XElement("input",
                                                         new XAttribute("type", "text"),
                                                         new XAttribute("class",
                                                                        string.Format("csf-{0}", layout.SiteColumnID)),
                                                         new XAttribute("value", value),
                                                         new XAttribute("rel", layout.IsRequired ? "required" : "")
                                            );

                                if (!string.IsNullOrEmpty(layout.CSSStyle) && layout.CSSStyle.Trim() != string.Empty)
                                    input.Add(new XAttribute("style", layout.CSSStyle));

                                fieldWrapper.Add(input);                                

                                div.Add(fieldWrapper);

                                if (!layout.IsExtraField || (layout.IsExtraField && (siteActivityRule.CountExtraFields == null || siteActivityRule.CountExtraFields > extraFieldCount) && string.IsNullOrEmpty(contactColumnValue != null ? contactColumnValue.StringValue : "")))
                                {
                                    node.Add(div);
                                    extraFieldCount++;
                                }
                                break;
                            case ColumnType.Date:
                                value = string.Empty;
                                if (contactColumnValue != null && !string.IsNullOrEmpty(contactColumnValue.StringValue))
                                    value = ((DateTime)contactColumnValue.DateValue).ToString();
                                else if (!string.IsNullOrEmpty(layout.DefaultValue))
                                        value = (DateTime.Parse(layout.DefaultValue)).ToString();

                                input = new XElement("input",
                                                         new XAttribute("type", "text"),
                                                         new XAttribute("class", string.Format("csf-{0}", layout.SiteColumnID)),
                                                         new XAttribute("value", value),
                                                         new XAttribute("rel", layout.IsRequired ? "required" : "")
                                            );

                                if (!string.IsNullOrEmpty(layout.CSSStyle) && layout.CSSStyle.Trim() != string.Empty)
                                    input.Add(new XAttribute("style", layout.CSSStyle));

                                fieldWrapper.Add(input);

                                div.Add(fieldWrapper);

                                if (!layout.IsExtraField || (layout.IsExtraField && (siteActivityRule.CountExtraFields == null || siteActivityRule.CountExtraFields > extraFieldCount) && string.IsNullOrEmpty(contactColumnValue != null ? contactColumnValue.StringValue : "")))
                                {
                                    node.Add(div);
                                    extraFieldCount++;
                                }
                                break;
                            case ColumnType.Text:
                                if (contactColumnValue != null && !string.IsNullOrEmpty(contactColumnValue.StringValue))
                                    value = contactColumnValue.StringValue.Replace("<br />", "[BR]");
                                else
                                    if (!string.IsNullOrEmpty(layout.DefaultValue))
                                        value = layout.DefaultValue.Replace("<br />", "[BR]");

                                input = new XElement("textarea", value,
                                                     new XAttribute("class",
                                                                    string.Format("csf-{0}", layout.SiteColumnID)),
                                                     new XAttribute("row", "3"),
                                                     new XAttribute("rel", layout.IsRequired ? "required" : ""));

                                if (!string.IsNullOrEmpty(layout.CSSStyle) && layout.CSSStyle.Trim() != string.Empty)
                                    input.Add(new XAttribute("style", layout.CSSStyle));

                                fieldWrapper.Add(input);

                                div.Add(fieldWrapper);

                                if (!layout.IsExtraField || (layout.IsExtraField && (siteActivityRule.CountExtraFields == null || siteActivityRule.CountExtraFields > extraFieldCount) && string.IsNullOrEmpty(contactColumnValue != null ? contactColumnValue.StringValue : "")))
                                {
                                    node.Add(div);
                                    extraFieldCount++;
                                }
                                break;
                            case ColumnType.Enum:
                                var siteColumnValues = repository.SiteColumnValues_SelectAll((Guid)layout.SiteColumnID);

                                var select = new XElement("select");

                                if (!string.IsNullOrEmpty(layout.CSSStyle) && layout.CSSStyle.Trim() != string.Empty)
                                    select.Add(new XAttribute("style", layout.CSSStyle));

                                if (contactColumnValue == null)
                                    select.Add(new XElement("option", "--",
                                                new XAttribute("value", ""),
                                                new XAttribute("selected", "selected"),
                                                new XAttribute("rel", layout.IsRequired ? "required" : "")                                                
                                                )
                                    );

                                if (siteColumnValues != null && siteColumnValues.Count > 0)
                                {
                                    foreach (var siteColumnValue in siteColumnValues)
                                    {
                                        var option = new XElement("option", siteColumnValue.Value,
                                                                  new XAttribute("class", string.Format("csf-{0}", layout.SiteColumnID)),
                                                                  new XAttribute("value", siteColumnValue.ID)
                                                                  
                                            );
                                        if (contactColumnValue == null && !string.IsNullOrEmpty(layout.DefaultValue))
                                            option.Add(new XAttribute("selected", "selected"));

                                        if (contactColumnValue != null && contactColumnValue.SiteColumnValueID == siteColumnValue.ID)
                                            option.Add(new XAttribute("selected", "selected"));

                                        select.Add(option);
                                    }
                                }
                                fieldWrapper.Add(select);
                                div.Add(fieldWrapper);

                                if (!layout.IsExtraField || (layout.IsExtraField && (siteActivityRule.CountExtraFields == null || siteActivityRule.CountExtraFields > extraFieldCount) && (contactColumnValue == null || contactColumnValue.SiteColumnValueID == null)))
                                {
                                    node.Add(div);
                                    extraFieldCount++;
                                }
                                break;
                        }
                    break;
                    case LayoutType.FullName:
                    case LayoutType.Email:
                    case LayoutType.Phone:
                    case LayoutType.Surname:
                    case LayoutType.Name:
                    case LayoutType.Patronymic:
                        div = new XElement("div");
                        label = new XElement("label", string.Format("{0}:", layout.Name));

                        if (parentLayout != null)
                        {
                            if ((OutputFormatFields) parentLayout.OutputFormatFields == OutputFormatFields.Top
                                || (OutputFormatFields) parentLayout.OutputFormatFields == OutputFormatFields.Left)
                            {
                                div.Add(label);
                            }
                            if ((OutputFormatFields) parentLayout.OutputFormatFields == OutputFormatFields.Top)
                                div.Add(new XElement("br"));

                            var fieldClass = GetFieldClass(parentLayout);
                            if (!string.IsNullOrEmpty(fieldClass))
                                div.Add(new XAttribute("class", fieldClass));
                        }

                        var contact = repository.Contact_SelectById(siteID, contactID);
                        string contactFieldValue = string.Empty;
                        string contactFieldClass = string.Empty;
                        if ((LayoutType)layout.LayoutType == LayoutType.FullName)
                        {
                            contactFieldValue = contact.UserFullName ?? "";
                            contactFieldClass = "sys_fullname";
                        }
                        if ((LayoutType)layout.LayoutType == LayoutType.Email)
                        {
                            contactFieldValue = contact.Email ?? "";
                            contactFieldClass = "sys_email";
                        }
                        if ((LayoutType)layout.LayoutType == LayoutType.Phone)
                        {
                            contactFieldValue = contact.Phone ?? "";
                            contactFieldClass = "sys_phone";
                        }
                        if ((LayoutType)layout.LayoutType == LayoutType.Surname)
                        {
                            contactFieldValue = contact.Surname ?? "";
                            contactFieldClass = "sys_surname";
                        }
                        if ((LayoutType)layout.LayoutType == LayoutType.Name)
                        {
                            contactFieldValue = contact.Name ?? "";
                            contactFieldClass = "sys_name";
                        }
                        if ((LayoutType)layout.LayoutType == LayoutType.Patronymic)
                        {
                            contactFieldValue = contact.Patronymic ?? "";
                            contactFieldClass = "sys_patronymic";
                        }

                        input = new XElement("input",
                                                new XAttribute("type", "text"),
                                                new XAttribute("class", contactFieldClass),
                                                new XAttribute("value", contactFieldValue),
                                                new XAttribute("rel", layout.IsRequired ? "required" : ""));

                        if (!string.IsNullOrEmpty(layout.CSSStyle) && layout.CSSStyle.Trim() != string.Empty)
                            input.Add(new XAttribute("style", layout.CSSStyle));

                        fieldWrapper.Add(input);

                        div.Add(fieldWrapper);

                        if (!layout.IsExtraField || (layout.IsExtraField && (siteActivityRule.CountExtraFields == null || siteActivityRule.CountExtraFields > extraFieldCount) && string.IsNullOrEmpty(contactFieldValue)))
                        {
                            node.Add(div);
                            extraFieldCount++;
                        }

                        break;
                }

                BuildLayout(siteID, contactID, siteActivityRule, siteActivityRuleLayouts, layout, contactColumnValues, register);
            }
        }
        */


        protected string GetFieldClass(tbl_SiteActivityRuleLayout parentLayout)
        {
            var fieldClass = string.Empty;
            if ((Orientation)parentLayout.Orientation == Orientation.Horizontal && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Left)
                fieldClass = fieldClass + "hor-left";
            if ((Orientation)parentLayout.Orientation == Orientation.Horizontal && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Top)
                fieldClass = fieldClass + "hor-top";
            if ((Orientation)parentLayout.Orientation == Orientation.Vertical && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Left)
                fieldClass = fieldClass + "ver-left";
            if ((Orientation)parentLayout.Orientation == Orientation.Vertical && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Top)
                fieldClass = fieldClass + "ver-top";
            if ((Orientation)parentLayout.Orientation == Orientation.Horizontal && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.None)
                fieldClass = fieldClass + "hor-none";
            if ((Orientation)parentLayout.Orientation == Orientation.Vertical && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.None)
                fieldClass = fieldClass + "ver-none";

            return fieldClass;
        }



        /// <summary>
        /// Ls the g_ form service result.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="ContactID">The user ID.</param>
        /// <param name="ActivityCode">The activity code.</param>
        /// <param name="FormData">The form data.</param>
        /// <returns></returns>
        public JsonResponse LG_FormServiceResult(string SiteID, string ContactID, string ActivityCode, string Parameter, string register, string FormData)
        {
            Guid siteIdOut, contactIdOut;

            if (Guid.TryParse(SiteID, out siteIdOut) && Guid.TryParse(ContactID, out contactIdOut))
            {
                if (!IsExistsAccess(siteIdOut, HttpContext.Current.Request.UrlReferrer))
                    return new JsonResponse();

                var siteActivityRule = repository.SiteActivityRules_SelectByCode(siteIdOut, ActivityCode);

                if (siteActivityRule != null)
                {
                    if (bool.Parse(register))
                    {
                        var ActivityCodeWithParameter = ActivityCode;
                        if (!string.IsNullOrEmpty(Parameter))
                            ActivityCodeWithParameter = ActivityCodeWithParameter + "#" + Parameter;

                        CounterServiceHelper.AddContact(siteIdOut, contactIdOut, ActivityType.FillForm, ActivityCodeWithParameter, null, null);
                        //CounterServiceHelper.AddSiteUser(siteIdOut, userIdOut);
                        //CounterServiceHelper.AddSiteUserActivity(siteIdOut, userIdOut, ActivityType.FillForm, ActivityCode);
                    }

                    var contact = repository.Contact_SelectById(siteIdOut, contactIdOut);

                    var data = new JavaScriptSerializer().Deserialize<IList<NameValuePair>>(FormData);
                    if (data.Count > 0)
                    {
                        var nameValuePairs = data.Select(nameValuePair => new KeyValuePair<string, string>(nameValuePair.N, nameValuePair.V)).ToList();
                        var contactData = new ContactData(siteIdOut);
                        contactData.SaveForm(contact.ID, nameValuePairs);
                    }

                    /*if (data.Count > 0)
                    {
                        foreach (var nameValuePair in data)
                        {
                            switch (nameValuePair.N)
                            {
                                case "csf-UserFullName":
                                    //siteUser.UserFullName = nameValuePair.Value;

                                    var nameChecker = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                                    var nameCheck = nameChecker.CheckName(nameValuePair.V, NameCheckerFormat.FIO, Correction.Correct);
                                    if (!string.IsNullOrEmpty(nameCheck))
                                    {
                                        contact.UserFullName = nameCheck;
                                        contact.Surname = nameChecker.Surname;
                                        contact.Name = nameChecker.Name;
                                        contact.Patronymic = nameChecker.Patronymic;
                                        if (nameChecker.Gender.HasValue)
                                            contact.Gender = (int)nameChecker.Gender.Value;
                                        contact.IsNameChecked = nameChecker.IsNameCorrect;
                                    }
                                    else
                                        contact.UserFullName = nameValuePair.V;
                                    break;
                                case "csf-Email":
                                    contact.Email = nameValuePair.V;
                                    break;
                                case "csf-Phone":
                                    contact.Phone = nameValuePair.V;
                                    break;
                                case "csf-Surname":
                                    contact.Surname = nameValuePair.V;
                                    contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                    break;
                                case "csf-Name":
                                    contact.Name = nameValuePair.V;
                                    contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                    break;
                                case "csf-Patronymic":
                                    contact.Patronymic = nameValuePair.V;
                                    contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                                    break;
                                default:
                                    bool isExist = true;
                                    var siteColumnID = Guid.Parse(nameValuePair.N.Replace("csf-", ""));
                                    var siteColumn = repository.SiteColumns_SelectById(siteIdOut, siteColumnID);
                                    var contactColumnValue = repository.ContactColumnValues_Select(contactIdOut, siteColumnID);

                                    if (contactColumnValue == null)
                                    {
                                        contactColumnValue = new tbl_ContactColumnValues { ContactID = contactIdOut, SiteColumnID = siteColumnID };
                                        isExist = false;
                                    }

                                    switch ((ColumnType)siteColumn.TypeID)
                                    {
                                        case ColumnType.String:
                                        case ColumnType.Number:
                                        case ColumnType.Text:
                                            contactColumnValue.StringValue = nameValuePair.V;
                                            break;
                                        case ColumnType.Date:
                                            contactColumnValue.DateValue = DateTime.Parse(nameValuePair.V);
                                            break;
                                        case ColumnType.Enum:
                                            if (!string.IsNullOrEmpty(nameValuePair.V))
                                                contactColumnValue.SiteColumnValueID = Guid.Parse(nameValuePair.V);
                                            else
                                                contactColumnValue.SiteColumnValueID = null;
                                            break;
                                    }
                                    if (isExist)
                                        repository.ContactColumnValues_Update(contactColumnValue);
                                    else
                                        repository.ContactColumnValues_Add(contactColumnValue);
                                    break;
                            }
                        }
                        repository.Contact_Update(contact);
                    }*/

                    return new JsonResponse { RuleType = "FormServiceResult", Value = !string.IsNullOrEmpty(siteActivityRule.URL) ? siteActivityRule.URL : HttpContext.Current.Request.UrlReferrer.ToString() };
                }
            }

            return new JsonResponse();
        }



        /// <summary>
        /// Ls the g_ form service cancel.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="ContactID">The user ID.</param>
        /// <param name="ActivityCode">The activity code.</param>
        public void LG_FormServiceCancel(string SiteID, string ContactID, string ActivityCode, string Parameter)
        {
            Guid siteIdOut, contactIdOut;

            if (Guid.TryParse(SiteID, out siteIdOut) && Guid.TryParse(ContactID, out contactIdOut))
            {
                if (!IsExistsAccess(siteIdOut, HttpContext.Current.Request.UrlReferrer))
                    return;

                var siteActivityRule = repository.SiteActivityRules_SelectByCode(siteIdOut, ActivityCode);

                if (siteActivityRule != null)
                {
                    var ActivityCodeWithParameter = ActivityCode;
                    if (!string.IsNullOrEmpty(Parameter))
                        ActivityCodeWithParameter = ActivityCodeWithParameter + "#" + Parameter;

                    CounterServiceHelper.AddContact(siteIdOut, contactIdOut, ActivityType.CancelForm, ActivityCodeWithParameter, null, null);
                    //CounterServiceHelper.AddSiteUser(siteActivityRule.SiteID, userIdOut);
                    //CounterServiceHelper.AddSiteUserActivity(siteIdOut, userIdOut, ActivityType.CancelForm, ActivityCode);
                }
            }
        }



        /// <summary>
        /// Ls the g_ form service open.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="ContactID">The contact ID.</param>
        /// <param name="ActivityCode">The activity code.</param>
        /// <param name="Parameter">The parameter.</param>
        public void LG_FormServiceOpen(string SiteID, string ContactID, string ActivityCode, string Parameter)
        {
            Guid siteIdOut, contactIdOut;

            if (Guid.TryParse(SiteID, out siteIdOut) && Guid.TryParse(ContactID, out contactIdOut))
            {
                if (!IsExistsAccess(siteIdOut, HttpContext.Current.Request.UrlReferrer))
                    return;

                var siteActivityRule = repository.SiteActivityRules_SelectByCode(siteIdOut, ActivityCode);

                if (siteActivityRule != null)
                {
                    var ActivityCodeWithParameter = ActivityCode;
                    if (!string.IsNullOrEmpty(Parameter))
                        ActivityCodeWithParameter = ActivityCodeWithParameter + "#" + Parameter;

                    CounterServiceHelper.AddContact(siteIdOut, contactIdOut, ActivityType.OpenForm, ActivityCodeWithParameter, null, null);
                }
            }
        }


        /// <summary>
        /// Ls the g_ link processing.
        /// </summary>
        /// <param name="ContactID">The contact ID.</param>
        /// <param name="ActionLinkID">The action link ID.</param>
        /// <param name="Resolution">The resolution.</param>
        /// <returns></returns>
        public JsonResponse LG_LinkProcessing(string ContactID, string ActionLinkID, string Resolution)
        {
            Guid contactIdOut, actionLinkIdOut;

            if (Guid.TryParse(ContactID, out contactIdOut) && Guid.TryParse(ActionLinkID, out actionLinkIdOut))
            {                
                var activityCode = string.Empty;
                var redirectUrl = string.Empty;
                var activityType = ActivityType.Link;
                var ruleType = "Link";

                var linkProcessing = CounterServiceHelper.LinkProcessing(actionLinkIdOut);
                if (linkProcessing != null)
                {
                    if (!IsExistsAccess(linkProcessing.SiteID, HttpContext.Current.Request.UrlReferrer))
                        return new JsonResponse();

                    if (linkProcessing.SiteActivityRuleID != null)
                    {
                        activityCode = linkProcessing.SiteActivityRuleCode;
                        redirectUrl = linkProcessing.SiteActivityRuleURL;

                        if ((RuleType)linkProcessing.SiteActivityRuleTypeID == RuleType.File)
                        {
                            activityType = ActivityType.DownloadFile;
                            ruleType = "DownloadFile";
                            redirectUrl = string.Format("{0}/DownloadFile.aspx?id={1}", WebConfigurationManager.AppSettings["webServiceUrl"], linkProcessing.SiteActivityRuleID);
                        }
                    }
                    else
                    {
                        activityCode = linkProcessing.LinkURL;
                        redirectUrl = linkProcessing.LinkURL;
                    }

                    if (contactIdOut != linkProcessing.ContactID)
                        CounterServiceHelper.AddContact(linkProcessing.SiteID, contactIdOut, activityType, activityCode, "", Resolution, null, linkProcessing.ContactID);
                    else
                        CounterServiceHelper.AddContact(linkProcessing.SiteID, linkProcessing.ContactID, activityType, activityCode, "", Resolution, null, null);

                    return new JsonResponse { RuleType = ruleType, Value = redirectUrl };
                }
            }

            return new JsonResponse();
        }



        /// <summary>
        /// Determines whether [is exists access] [the specified site id].
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="refferUrl">The reffer URL.</param>
        /// <returns>
        ///   <c>true</c> if [is exists access] [the specified site id]; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsExistsAccess(Guid siteId, Uri refferUrl)
        {
            if (refferUrl == null)
                return false;            

            try
            {
                var siteDomains = (List<CounterServiceDomainAccessMap>)HttpContext.Current.Cache[siteId.ToString()];

                if (siteDomains == null)
                {
                    siteDomains = CounterServiceHelper.SelectSiteDomains(siteId);
                    HttpContext.Current.Cache.Insert(siteId.ToString(), siteDomains, null, DateTime.Now.AddMinutes(60), System.Web.Caching.Cache.NoSlidingExpiration);
                }


                if (siteDomains.Any() && siteDomains.FirstOrDefault().IsBlockAccessFromDomainsOutsideOfList)
                {
                    var result = siteDomains.Any(siteDomain => siteDomain.Domains.Any(o => o.ToLower().Replace("www.", string.Empty) == refferUrl.Host.ToLower().Replace("www.", string.Empty)));

                    if (!result)
                        Log.Error(string.Format("Доступ с сайта {0} заблокирован для siteId = {1}", refferUrl, siteId));

                    return result;
                }
            }
            catch(Exception ex)
            {
                Log.Error(string.Format("Ошибка проверки домена: SiteId = {0} RefferUrl = {1}", siteId, refferUrl), ex);
            }
                        
            return true;
        }
    }
}