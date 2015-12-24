using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class PopupSiteActionTemplate : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        private Guid _siteId = CurrentUser.Instance.SiteID;
        protected RadAjaxManager radAjaxManager = null;
        public Access access;


        public event TemplateSavedEventHandler TemplateSaved;
        public delegate void TemplateSavedEventHandler(object sender, TemplateSavedEventArgs e);
        public class TemplateSavedEventArgs : EventArgs
        {
            public Guid SelectedTemplateId { get; set; }
        }

        #region Properties
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteActionTemplateId
        {
            get
            {
                if (ViewState["SiteActionTemplateId"] == null)
                    ViewState["SiteActionTemplateId"] = Guid.Empty;

                return (Guid)ViewState["SiteActionTemplateId"];
            }
            set
            {
                ViewState["SiteActionTemplateId"] = value;
            }
        }



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public SiteActionTemplateCategory SiteActionTemplateCategory
        {
            get
            {
                if (ViewState["SiteActionTemplateCategory"] == null)
                    ViewState["SiteActionTemplateCategory"] = SiteActionTemplateCategory.BaseTemplate;

                return (SiteActionTemplateCategory)ViewState["SiteActionTemplateCategory"];
            }
            set
            {
                ViewState["SiteActionTemplateCategory"] = value;
            }
        }



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string ValidationGroup
        {
            get
            {
                return (string)ViewState["ValidationGroup"];
            }
            set
            {
                ViewState["ValidationGroup"] = value;
            }
        }



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool ShowLabel
        {
            get
            {
                if (ViewState["ShowLabel"] == null)
                    ViewState["ShowLabel"] = true;

                return (bool)ViewState["ShowLabel"];
            }
            set
            {
                ViewState["ShowLabel"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool ShowActionType
        {
            get
            {
                if (ViewState["ShowActionType"] == null)
                    ViewState["ShowActionType"] = true;

                return (bool)ViewState["ShowActionType"];
            }
            set
            {
                ViewState["ShowActionType"] = value;
            }
        }


        public bool fromSilverlight { get; set; }
        public bool FromSession { get; set; }
        #endregion



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            access = Access.Check();

            if (!access.Write)
                lbtnSave.Visible = false;

            if (!string.IsNullOrEmpty(ValidationGroup))
            {
                rfvTemplateValidator.ValidationGroup = ValidationGroup;
                rfvTemplateValidator.Enabled = true;
            }
            else
            {
                rfvTemplateValidator.ValidationGroup = string.Empty;
                rfvTemplateValidator.Enabled = false;
            }

            if (ShowLabel)
                lrlLabel.Text = "<label style=\"float:left;margin-right: 3px\">Шаблон:</label>";


            ucSettingsSiteActionTemplate.SiteActionTemplateCategory = SiteActionTemplateCategory;

            if (fromSilverlight || SiteActionTemplateCategory == SiteActionTemplateCategory.Personal || FromSession)
                pnlSiteActionTemplate.Visible = false;

            if (SiteActionTemplateCategory == SiteActionTemplateCategory.Personal)
            {
                lbtnSave.Text = "<em>&nbsp;</em><span>Отправить</span>";
                ShowActionType = false;
            }

            ucSettingsSiteActionTemplate.ShowActionType = ShowActionType;
            ucSettingsSiteActionTemplate.PopupClientId = this.ClientID;

            ucStatsSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            radAjaxPanel.ClientEvents.OnResponseEnd = this.ClientID + "_OnResponseEnd";
            radAjaxPanel.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(radAjaxPanel_AjaxRequest);

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, pnlSiteActionTemplate);

            rtsTabs.OnClientTabSelected = this.ClientID + "_AutoHeightRadWindow";
            rpbTemplateMessage.OnClientItemAnimationEnd = this.ClientID + "_AutoHeightRadWindow";
            rwSiteActionTemplate.OnClientClose = this.ClientID + "_OnClientClose";

            ucSettingsSiteActionTemplate.FromSession = FromSession;
            ucEditorSiteActionTemplate.FromSession = FromSession;

            if (!Page.IsPostBack)
            {
                var siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(SiteActionTemplateId);
                UpdateUI(siteActionTemplate);
            }
        }
        



        /// <summary>
        /// Handles the AjaxRequest event of the radAjaxPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        void radAjaxPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            Guid outSiteActionTemplate;
            if (Guid.TryParse(e.Argument, out outSiteActionTemplate))
            {
                rtsTabs.Tabs.FindTabByValue("Template").Selected = true;
                RadPageView2.Selected = true;

                if (outSiteActionTemplate == Guid.Empty)
                {
                    rpbTemplateMessage.FindItemByValue("SelectTemplate").Selected = true;
                    rpbTemplateMessage.FindItemByValue("SelectTemplate").Expanded = true;
                }
                else
                {
                    rpbTemplateMessage.FindItemByValue("TemplateMessage").Selected = true;
                    rpbTemplateMessage.FindItemByValue("TemplateMessage").Expanded = true;
                }

                SiteActionTemplateId = outSiteActionTemplate;
                BindData();
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            ucSettingsSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            ucSettingsSiteActionTemplate.ShowTitle = false;
            ucSettingsSiteActionTemplate.BindData();

            ucEditorSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            ucEditorSiteActionTemplate.BindData();

            if (SiteActionTemplateId != Guid.Empty)
            {
                rtsTabs.Tabs.FindTabByValue("stats").Visible = true;
                return;
            }
            
            rtsTabs.Tabs.FindTabByValue("stats").Visible = false;
        }



        /// <summary>
        /// Forces the not postback page load.
        /// </summary>
        public void ForceNotPostbackPageLoad()
        {            
            ucSettingsSiteActionTemplate.BindData();
            ucEditorSiteActionTemplate.BindSiteColumns();
            ucEditorSiteActionTemplate.BindFiles();
            ucSelectSiteActionTemplate.BindData();            
        }



        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="siteActionTemplate">The site action template.</param>
        public void UpdateUI(tbl_SiteActionTemplate siteActionTemplate)
        {
            if (siteActionTemplate != null)
            {
                txtSiteActionTemplateId.Text = siteActionTemplate.ID.ToString();
                lbtnSiteActionTemplate.Text = siteActionTemplate.Title;
                lbtnSiteActionTemplate.OnClientClick = string.Format("{0}_ShowSiteActionTemplateRadWindow('{1}'); return false;", this.ClientID, siteActionTemplate.ID.ToString());
                hlGoToTemplate.NavigateUrl = UrlsData.AP_SiteActionTemplate(siteActionTemplate.ID);
                //hlGoToTemplate.Visible = true;
            }
            else
            {
                lbtnSiteActionTemplate.Text = "Выберите шаблон";
                lbtnSiteActionTemplate.OnClientClick = string.Format("{0}_ShowSiteActionTemplateRadWindow('{1}'); return false;", this.ClientID, Guid.Empty.ToString());
                hlGoToTemplate.Visible = false;
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (!access.Write)
                return;

            tbl_SiteActionTemplate siteActionTemplate;
            if (!FromSession)
                siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(_siteId, SiteActionTemplateId) ?? new tbl_SiteActionTemplate();
            else
                siteActionTemplate = ((List<tbl_SiteActionTemplate>)Session["SiteActionTemplates"]).FirstOrDefault(a => a.ID == SiteActionTemplateId);

            //siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(SiteActionTemplateId) ?? new tbl_SiteActionTemplate();
            siteActionTemplate.SiteID = _siteId;
            siteActionTemplate.Title = string.Empty;
            siteActionTemplate.FromEmail = string.Empty;
            siteActionTemplate.SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory;
            siteActionTemplate.OwnerID = CurrentUser.Instance.ContactID;
            siteActionTemplate.ActionTypeID = 1;

            if (!FromSession)
            {
                if (siteActionTemplate.ID == Guid.Empty)
                    siteActionTemplate = _dataManager.SiteActionTemplate.Add(siteActionTemplate);
                else
                    _dataManager.SiteActionTemplate.Update(siteActionTemplate);
            }


            SiteActionTemplateId = siteActionTemplate.ID;

            ucEditorSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            ucEditorSiteActionTemplate.Save(); // editor is first for save!
            ucSettingsSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            ucSettingsSiteActionTemplate.Save();

            if (!FromSession)
            {
                _dataManager = new DataManager();
                siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(SiteActionTemplateId);

                UpdateUI(siteActionTemplate);
            }

            if (TemplateSaved != null)
            {
                TemplateSaved(this, new TemplateSavedEventArgs { SelectedTemplateId = siteActionTemplate.ID });
            }

            if (fromSilverlight && !Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_WorkflowCallback"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_WorkflowCallback", string.Format("{0}_WorkflowCallback('{1}', '{2}');", this.ClientID, siteActionTemplate.ID, siteActionTemplate.Title), true);
                return;
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_CloseSiteActionTemplateRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_CloseSiteActionTemplateRadWindow", this.ClientID + "_CloseSiteActionTemplateRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnSelectedChanged event of the ucSelectSiteActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.SelectSiteActionTemplate.SelectedChangedEventArgs"/> instance containing the event data.</param>
        protected void ucSelectSiteActionTemplate_OnSelectedChanged(object sender, SelectSiteActionTemplate.SelectedChangedEventArgs e)
        {
            ucEditorSiteActionTemplate.SiteActionTemplateId = e.SiteActionTemplateId;
            ucEditorSiteActionTemplate.BindData();

            rtsTabs.Tabs.FindTabByValue("stats").Visible = false;

            rpbTemplateMessage.FindItemByValue("TemplateMessage").Selected = true;
            rpbTemplateMessage.FindItemByValue("TemplateMessage").Expanded = true;
        }

        protected void lbSendTest_OnClick(object sender, EventArgs e)
        {
            TestSend();
        }

        protected void TestSend()
        {
            try
            {
                string msg = "";
                bool doSend = true;

                var site = _dataManager.Sites.SelectById(_siteId);
                //var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(SiteId, SiteActionTemplateId);
                var body = ((HtmlEditor)ucEditorSiteActionTemplate.FindControl("ucHtmlEditor")).Content;

                /*if (siteActionTemplate.ParentID.HasValue)
                {
                    var parentSiteActionTemplate = DataManager.SiteActionTemplate.SelectById((Guid)siteActionTemplate.ParentID);
                    if (parentSiteActionTemplate != null && parentSiteActionTemplate.MessageBody.Contains("#Text#"))
                    {
                        body = parentSiteActionTemplate.MessageBody.Replace("#Text#", siteActionTemplate.MessageBody);
                    }
                }*/

                var subject = ((TextBox)ucEditorSiteActionTemplate.FindControl("txtMessageCaption")).Text;

                var contact = _dataManager.Contact.SelectById(site.ID, (Guid)CurrentUser.Instance.ContactID);

                if (body.Contains("#Advert#"))
                    body = body.Replace("#Advert#", GetAdvertBlock());
                else if (site.ServiceAdvertisingActionID != null)
                {
                    switch ((EmailAction)site.ServiceAdvertisingActionID)
                    {
                        case EmailAction.DoNotSend:
                            doSend = false;
                            break;
                        case EmailAction.Auto:
                            body = body + "<br/>" + GetAdvertBlock();
                            break;
                    }
                }


                var mailMessage = new MailMessage();

                if (site.IsSendFromLeadForce)
                    mailMessage.Sender = new MailAddress("info@leadforce.ru", "LeadForce");

                if (!string.IsNullOrEmpty(((TextBox)ucSettingsSiteActionTemplate.FindControl("txtFromEmail")).Text))
                    mailMessage.From = new MailAddress(((TextBox)ucSettingsSiteActionTemplate.FindControl("txtFromEmail")).Text, ((TextBox)ucSettingsSiteActionTemplate.FindControl("txtFromName")).Text);
                else
                    mailMessage.From = new MailAddress(contact.Email);

                mailMessage.Headers.Add("Message-ID", string.Format("<{0}@{1}>", Guid.NewGuid(), mailMessage.From.Host));
                mailMessage.Headers.Add("Return-Path", "<trash@leadforce.ru>");

                if (!string.IsNullOrEmpty(((TextBox)ucSettingsSiteActionTemplate.FindControl("txtReplyEmail")).Text))
                    mailMessage.ReplyToList.Add(new MailAddress(((TextBox)ucSettingsSiteActionTemplate.FindControl("txtReplyEmail")).Text, ((TextBox)ucSettingsSiteActionTemplate.FindControl("txtReplyName")).Text));

                bool isIncorrectEmail = false;

                /*switch ((ActionType)siteActionTemplate.ActionTypeID)
                {
                    case ActionType.EmailToUser:
                        if (string.IsNullOrEmpty(contact.Email) || !Regex.IsMatch(contact.Email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                        {
                            msg = "Неверный E-mail посетителя";
                            isIncorrectEmail = true;
                            doSend = false;
                        }
                        else
                        {
                            try
                            {
                                mailMessage.To.Add(new MailAddress(contact.Email));
                            }
                            catch (Exception)
                            {
                                msg = "Неверный E-mail посетителя";
                                isIncorrectEmail = true;
                                doSend = false;
                            }
                        }
                        break;
                    case ActionType.EmailToFixed:
                        mailMessage.To.Add(new MailAddress(siteActionTemplate.ToEmail));
                        break;
                }*/
                mailMessage.To.Add(new MailAddress(contact.Email));

                body = body.Replace("=\"/files/", "=\"" + WebCounter.BusinessLogicLayer.Configuration.Settings.LeadForceSiteUrl + "/files/");

                body = body.Replace("#User.UserFullName#", contact.UserFullName ?? "")
                            .Replace("#User.LastName#", contact.Surname ?? "")
                            .Replace("#User.FirstName#", contact.Name ?? "")
                            .Replace("#User.MiddleName#", contact.Patronymic ?? "")
                            .Replace("#User.Email#", contact.Email ?? "")
                            .Replace("#User.Phone#", contact.Phone ?? "")
                            .Replace("#User.Score#", contact.Score.ToString());

                subject = subject.Replace("#User.UserFullName#", contact.UserFullName ?? "")
                            .Replace("#User.LastName#", contact.Surname ?? "")
                            .Replace("#User.FirstName#", contact.Name ?? "")
                            .Replace("#User.MiddleName#", contact.Patronymic ?? "")
                            .Replace("#User.Email#", contact.Email ?? "")
                            .Replace("#User.Phone#", contact.Phone ?? "")
                            .Replace("#User.Score#", contact.Score.ToString());

                var r = new Regex(@"#User.[\S]+?#");
                var results = r.Matches(body);
                foreach (Match result in results)
                {
                    var siteColumns = _dataManager.SiteColumns.SelectByCode(site.ID, result.Value.Replace("#User.", "").Replace("#", ""));
                    if (siteColumns != null)
                    {
                        var contactColumnValue = _dataManager.ContactColumnValues.Select(contact.ID, siteColumns.ID);
                        if (contactColumnValue != null)
                        {
                            switch ((ColumnType)contactColumnValue.tbl_SiteColumns.TypeID)
                            {
                                case ColumnType.String:
                                case ColumnType.Number:
                                case ColumnType.Text:
                                    body = body.Replace(result.Value, contactColumnValue.StringValue.Replace("[BR]", "\n"));
                                    subject = subject.Replace(result.Value, contactColumnValue.StringValue.Replace("[BR]", ""));
                                    break;
                                case ColumnType.Date:
                                    body = body.Replace(result.Value, ((DateTime)contactColumnValue.DateValue).ToString("dd.MM.yyyy HH:mm"));
                                    subject = subject.Replace(result.Value, ((DateTime)contactColumnValue.DateValue).ToString("dd.MM.yyyy HH:mm"));
                                    break;
                                case ColumnType.Enum:
                                    body = body.Replace(result.Value, contactColumnValue.tbl_SiteColumnValues.Value);
                                    subject = subject.Replace(result.Value, contactColumnValue.tbl_SiteColumnValues.Value);
                                    break;
                            }
                        }
                        else
                        {
                            body = body.Replace(result.Value, "");
                            subject = subject.Replace(result.Value, "");
                        }
                    }
                }

                MatchCollection matches;

                if (((RadioButtonList)ucSettingsSiteActionTemplate.FindControl("rblReplaceLinks")).SelectedValue.ToInt() != (int)ReplaceLinks.None)
                {
                    matches = Regex.Matches(body, @"<a.*?href=[""'](?<href>.*?)[""'].*?>(?<name>.*?)</a>", RegexOptions.IgnoreCase);
                    foreach (Match match in matches)
                    {
                        if (match.Groups["href"].Value.Contains("#Link."))
                            continue;

                        var siteActionLink = _dataManager.SiteActionLink.Select(contact.ID, match.Groups["href"].Value);
                        if (siteActionLink == null)
                        {
                            siteActionLink = new tbl_SiteActionLink
                            {
                                ContactID = contact.ID,
                                LinkURL = match.Groups["href"].Value
                            };
                            _dataManager.SiteActionLink.Add(siteActionLink);
                        }

                        if (((RadioButtonList)ucSettingsSiteActionTemplate.FindControl("rblReplaceLinks")).SelectedValue.ToInt() == (int)ReplaceLinks.ThroughService)
                            body = body.Replace(match.Groups[0].ToString(), string.Format("<a href=\"{0}/linkService.aspx?ID={1}\" target=\"_blank\">{2}</a>", WebConfigurationManager.AppSettings["webServiceUrl"], siteActionLink.ID, match.Groups["name"].Value));
                        else if (((RadioButtonList)ucSettingsSiteActionTemplate.FindControl("rblReplaceLinks")).SelectedValue.ToInt() == (int)ReplaceLinks.GoogleLinks)
                        {
                            const string utmSource = "LeadForce";
                            const string utmMedium = "email";
                            var utmTerm = match.Groups["name"].Value;
                            utmTerm = Regex.Replace(utmTerm, @"<[^>]*>", string.Empty);
                            var utmContent = siteActionLink.ID.ToString();
                            var utmCampaign = ""; //txtName.Text;
                            var queryParams =
                                string.Format("utm_source={0}&utm_medium={1}&utm_term={2}&utm_content={3}&utm_campaign={4}",
                                                utmSource, utmMedium, utmTerm, utmContent, utmCampaign);

                            var url = match.Groups["href"].Value;

                            try
                            {
                                Uri outUrl = null;
                                if (Uri.TryCreate(url, UriKind.Absolute, out outUrl))
                                {
                                    if (string.IsNullOrEmpty(outUrl.Query))
                                        url += "?" + queryParams;
                                    else
                                        url += "&" + queryParams;
                                }
                                else
                                    url += "?" + queryParams;
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Не верный формат ссылки", ex);
                                url += "?" + queryParams;
                            }

                            body = body.Replace(match.Groups[0].ToString(), string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", url, match.Groups["name"].Value));
                        }
                    }
                }

                matches = Regex.Matches(body, @"#Link.(?<code>[\S]+)#", RegexOptions.IgnoreCase);
                foreach (Match match in matches)
                {
                    var siteActionLink = _dataManager.SiteActionLink.Select(contact.ID, _dataManager.Links.Select(site.ID, match.Groups["code"].Value).ID);
                    if (siteActionLink == null)
                    {
                        siteActionLink = new tbl_SiteActionLink
                        {
                            ContactID = contact.ID,
                            SiteActivityRuleID = _dataManager.Links.Select(site.ID, match.Groups["code"].Value).ID
                        };
                        _dataManager.SiteActionLink.Add(siteActionLink);
                    }

                    body = Regex.Replace(body, string.Format("#Link.{0}#", Regex.Escape(match.Groups["code"].Value)), string.Format("{0}/linkService.aspx?ID={1}", WebConfigurationManager.AppSettings["webServiceUrl"], siteActionLink.ID), RegexOptions.IgnoreCase);
                }

                if (body.Contains("#Unsubscribe#") || body.Contains("#User.Unsubscribe#") || body.Contains("#User.UnsubscribeLink#"))
                {
                    if (body.Contains("#Unsubscribe#") || body.Contains("#User.Unsubscribe#"))
                    {
                        body = body.Replace("#Unsubscribe#", GetUnsubscribeBlock(contact.ID, site.ID));
                        body = body.Replace("#User.Unsubscribe#", GetUnsubscribeBlock(contact.ID, site.ID));
                    }
                    if (body.Contains("#User.UnsubscribeLink#"))
                        body = body.Replace("#User.UnsubscribeLink#", GetUnsubscribeLink(contact.ID, site.ID));
                }
                else
                {
                    if (!site.UnsubscribeActionID.HasValue)
                        body = body + "<br/>" + GetUnsubscribeBlock(contact.ID, site.ID);
                    else
                    {
                        switch ((EmailAction)site.UnsubscribeActionID)
                        {
                            case EmailAction.DoNotSend:
                                doSend = false;
                                break;
                            case EmailAction.Auto:
                                body = body + "<br/>" + GetUnsubscribeBlock(contact.ID, site.ID);
                                break;
                        }
                    }
                }

                //mailMessage.Subject = siteAction.tbl_SiteActionTemplate.MessageCaption;
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                try
                {
                    //Если у контакта стоит статус email "Не работает" или "Запрещен" или не корректный email не отсылать почту
                    /*if (isIncorrectEmail || contact.EmailStatusID == (int)EmailStatus.DoesNotWork || contact.EmailStatusID == (int)EmailStatus.Banned)
                    {
                        siteAction.ActionStatusID = (int)ActionStatus.InvalidEmail;
                        if (contact.EmailStatusID.HasValue)
                        {
                            switch ((EmailStatus)contact.EmailStatusID)
                            {
                                case EmailStatus.DoesNotWork:
                                    siteAction.Comments = "E-mail контакта со статусом \"Не работает\"";
                                    break;
                                case EmailStatus.Banned:
                                    siteAction.Comments = "E-mail контакта со статусом \"Запрещен\"";
                                    break;
                                default:
                                    siteAction.Comments = msg;
                                    break;
                            }
                        }
                        else
                            siteAction.Comments = msg;

                        dataManager.SiteAction.Update(siteAction);
                        WorkflowProcessing.Processing(WorkflowProcessing.WorkflowElementByValue(siteAction.ID), ((int)ActionStatus.Error).ToString());
                        continue;
                    }*/

                    if (doSend)
                    {
                        var smtpClient = new SmtpClient();
                        if (!string.IsNullOrEmpty(site.SmtpHost))
                        {
                            if (site.SmtpPort != null)
                                smtpClient = new SmtpClient(site.SmtpHost, (int)site.SmtpPort);
                            else
                                smtpClient = new SmtpClient(site.SmtpHost);
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            //smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new NetworkCredential(site.SmtpUsername, site.SmtpPassword);
                        }
                        smtpClient.Send(mailMessage);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
                Log.Error("Scheduler ERROR: ", ex);
            }
        }


        /// <summary>
        /// Gets the advert block.
        /// </summary>
        /// <returns></returns>
        private string GetAdvertBlock()
        {
            return "Рассылка выполнена сервисом <a href=\"http://www.leadforce.ru\">LeadForce&trade;</a>";
        }



        /// <summary>
        /// Gets the unsubscribe block.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public string GetUnsubscribeBlock(Guid contactId, Guid siteId)
        {
            return string.Format("<a href=\"{0}\">Отписаться от получения рассылки</a>",
                          string.Format(ConfigurationManager.AppSettings["unsubscribeUrl"], contactId, siteId));
        }


        public string GetUnsubscribeLink(Guid contactId, Guid siteId)
        {
            return string.Format("<a href=\"{0}\">{0}</a>",
                          string.Format(ConfigurationManager.AppSettings["unsubscribeUrl"], contactId, siteId));
        }
    }
}