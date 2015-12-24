using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.AdminPanel.UserControls.Contact;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Services.TemplateTagsReplacers;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class MassMail : LeadForceBasePage
    {
        public Access access;
        public Guid _massMailId;
        protected RadAjaxManager radAjaxManager = null;
        protected string validationGroup = "valGroup";


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<Guid> SelectedContactList
        {
            get
            {
                if (ViewState["SelectedContactList"] == null)
                    ViewState["SelectedContactList"] = new List<Guid>();
                return (List<Guid>)ViewState["SelectedContactList"];
            }
            set
            {
                ViewState["SelectedContactList"] = value;
            }
        }



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



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Рассылки - LeadForce";

            access = Access.Check();

            hlCancel.NavigateUrl = UrlsData.AP_MassMails();

            string massMailId = Page.RouteData.Values["ID"] as string;
            if (!string.IsNullOrEmpty(massMailId))
                Guid.TryParse(massMailId, out _massMailId);

            if (!Page.IsPostBack)
                BindData();

            radAjaxManager = RadAjaxManager.GetCurrent(Page);

            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnBack, radTabStrip);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnNext, radTabStrip);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnBack, radMultiPage);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnNext, radMultiPage);

            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnBack, pnlBtnAdd);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnNext, pnlBtnAdd);

            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnBack, ucEditorSiteActionTemplate);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnNext, ucEditorSiteActionTemplate);

            radAjaxManager.AjaxSettings.AddAjaxSetting(radTabStrip, radMultiPage);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radTabStrip, pnlBtnAdd);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radTabStrip, ucEditorSiteActionTemplate);

            radAjaxManager.AjaxSettings.AddAjaxSetting(rblTargetContacts, pnlTags);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rblTargetContacts, pnlSelectContacts);

            radAjaxManager.AjaxSettings.AddAjaxSetting(ucSelectContacts, gridContacts);

            radAjaxManager.AjaxSettings.AddAjaxSetting(ucSelectSiteActionTemplate.FindControl("rrThumbnails"), ucEditorSiteActionTemplate);
            radAjaxManager.AjaxSettings.AddAjaxSetting(ucSelectSiteActionTemplate.FindControl("rblActionTemplate"), ucEditorSiteActionTemplate);

            radAjaxManager.AjaxSettings.AddAjaxSetting(rbAdd, ucSelectContacts);

            radAjaxManager.ClientEvents.OnResponseEnd = "OnResponseEnd";
        }

        

        protected void rbAdd_OnClick(object sender, EventArgs e)
        {
            ucSelectContacts.Show();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            if (_massMailId == Guid.Empty)
            {
                radTabStrip.Tabs[1].Enabled = false;
                radTabStrip.Tabs[2].Enabled = false;

                radTabStrip.AutoPostBack = true;
                radTabStrip.TabClick += new RadTabStripEventHandler(radTabStrip_TabClick);

                rfvTags.ValidationGroup = string.Empty;
                ucEditorSiteActionTemplate.ValidationGroup = string.Empty;

                gridContacts.Where = new List<GridWhere>();
                gridContacts.Where.Add(new GridWhere { CustomQuery = " 1 = 0 " });
            }

            EnumHelper.EnumToDropDownList<MassMailStatus>(ref ddlStatus);

            rblTargetContacts.Items.Clear();
            rblTargetContacts.Items.Add(new ListItem("Конкретный сегмент", "0"));
            rblTargetContacts.Items.Add(new ListItem("Произвольный список", "1"));
            rblTargetContacts.Items[0].Selected = true;

            var siteTags = DataManager.SiteTags.SelectAll(SiteId).Where(a => a.tbl_ObjectTypes.Name == "tbl_Contact");
            rblTags.DataSource = siteTags;
            rblTags.DataValueField = "ID";
            rblTags.DataTextField = "Name";
            rblTags.DataBind();

            if (_massMailId != Guid.Empty)
            {
                var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);
                txtName.Text = massMail.Name;
                ddlStatus.Items.FindByValue(massMail.MassMailStatusID.ToString()).Selected = true;
                if (massMail.MailDate != null)
                    rdtpMailDate.SelectedDate = massMail.MailDate;
                SiteActionTemplateId = massMail.SiteActionTemplateID;
                if (massMail.SiteTagID != null)
                {
                    rblTargetContacts.Items.FindByValue("0").Selected = true;
                    rblTags.Items.FindByValue(massMail.SiteTagID.ToString()).Selected = true;

                    litTag.Text = siteTags.SingleOrDefault(a => a.ID == massMail.SiteTagID).Name;
                    pnlTagsList.Visible = false;
                }
                else
                {
                    rblTargetContacts.Items.FindByValue("1").Selected = true;
                    SelectedContactList = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId).Select(a => a.ContactID).ToList();
                    pnlTags.Visible = false;
                    pnlSelectContacts.Visible = true;
                }

                pnlStatus.Visible = true;
                pnlBtnEdit.Visible = true;
                radPageView1.Visible = true;

                rblTargetContacts.Enabled = false;

                BindGridContacts();

                rpbTemplateMessage.FindItemByValue("TemplateMessage").Selected = true;
                rpbTemplateMessage.FindItemByValue("TemplateMessage").Expanded = true;

                if (massMail.MassMailStatusID == (int)MassMailStatus.Done || massMail.MassMailStatusID == (int)MassMailStatus.Cancel)
                {
                    lbtnSave.Visible = false;
                    lbtnSendNow2.Visible = false;
                    lbtnCancelMail.Visible = false;
                    ucSelectContacts.Visible = false;
                }

                if (massMail.MassMailStatusID == (int)MassMailStatus.Done)
                {
                    var massMailContact = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId);
                    litRecipients.Text = massMailContact.Count.ToString();

                    radTabStrip.FindTabByValue("Stats").Visible = true;
                }
            }
            else
            {
                pnlBtnAdd.Visible = true;
            }

            ucSettingsSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            ucSettingsSiteActionTemplate.BindData();
            ucEditorSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            ucEditorSiteActionTemplate.BindData();
        }



        /// <summary>
        /// Handles the TabClick event of the radTabStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTabStripEventArgs"/> instance containing the event data.</param>
        protected void radTabStrip_TabClick(object sender, RadTabStripEventArgs e)
        {
            rfvTags.ValidationGroup = string.Empty;
            ucEditorSiteActionTemplate.ValidationGroup = string.Empty;
            ucEditorSiteActionTemplate.RebindValidators(string.Empty);

            var index = radTabStrip.SelectedIndex;

            if (index == 1)
            {
                rfvTags.ValidationGroup = validationGroup;
            }

            if (index == 2)
            {
                rfvTags.ValidationGroup = validationGroup;
                ucEditorSiteActionTemplate.ValidationGroup = validationGroup;
                ucEditorSiteActionTemplate.RebindValidators(validationGroup);
            }

            lbtnBack.Visible = index != 0;
            lbtnNext.Visible = index != 2;
            lbtnSendNow.Visible = index == 2;
            lbtnSchedule.Visible = index == 2;
            lbtnTest.Visible = index == 2;

            radTabStrip.Tabs[index].Selected = true;
            radMultiPage.FindPageViewByID("radPageView" + index).Selected = true;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridContacts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var ibtnDelete = (ImageButton)item.FindControl("ibtnDelete");
                ibtnDelete.CommandArgument = data["ID"].ToString();
            }
        }



        /// <summary>
        /// Binds the grid contacts.
        /// </summary>
        protected void BindGridContacts()
        {
            var selectedItem = new List<Guid>();
            selectedItem = SelectedContactList;
            if (selectedItem.Count == 0)
                selectedItem.Add(Guid.Empty);

            var query = new StringBuilder();
            foreach (var item in selectedItem)
                query.AppendFormat("'{0}',", item);


            gridContacts.Where = new List<GridWhere>();
            gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_Contact.ID IN ({0})", query.ToString().TrimEnd(new[] { ',' })) });

            ucSelectContacts.SelectedItems = SelectedContactList;
        }



        /// <summary>
        /// Handles the OnSelectedChanged event of the ucSelectContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.Contact.SelectContacts.SelectedChangedEventArgs"/> instance containing the event data.</param>
        protected void ucSelectContacts_OnSelectedChanged(object sender, SelectContacts.SelectedChangedEventArgs e)
        {
            SelectedContactList = e.ContactList;

            BindGridContacts();
            gridContacts.Rebind();
        }




        /// <summary>
        /// Handles the OnCommand event of the ibtnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void ibtnDelete_OnCommand(object sender, CommandEventArgs e)
        {
            SelectedContactList.Remove(e.CommandArgument.ToString().ToGuid());

            BindGridContacts();
            gridContacts.Rebind();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rblTargetContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rblTargetContacts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            pnlTags.Visible = false;
            pnlSelectContacts.Visible = false;

            if (rblTargetContacts.SelectedValue == "0")
                pnlTags.Visible = true;
            else
                pnlSelectContacts.Visible = true;
        }



        /// <summary>
        /// Handles the OnSelectedChanged event of the ucSelectSiteActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.SelectSiteActionTemplate.SelectedChangedEventArgs"/> instance containing the event data.</param>
        protected void ucSelectSiteActionTemplate_OnSelectedChanged(object sender, SelectSiteActionTemplate.SelectedChangedEventArgs e)
        {
            ucEditorSiteActionTemplate.RebindValidators(validationGroup);
            ucEditorSiteActionTemplate.SiteActionTemplateId = e.SiteActionTemplateId;
            ucEditorSiteActionTemplate.BindData();

            rpbTemplateMessage.FindItemByValue("TemplateMessage").Selected = true;
            rpbTemplateMessage.FindItemByValue("TemplateMessage").Expanded = true;
        }


        /// <summary>
        /// Handles the OnCommand event of the lbtnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbtnBackNext_OnCommand(object sender, CommandEventArgs e)
        {
            var currentIndex = radTabStrip.SelectedIndex;
            var index = 0;

            if (e.CommandName.ToLower() == "next")
            {
                index = currentIndex + 1;
                radTabStrip.Tabs[index].Enabled = true;
                radTabStrip.Tabs[index].Selected = true;

                if (index == 1)
                    rfvTags.ValidationGroup = validationGroup;
                if (index == 2)
                {
                    ucEditorSiteActionTemplate.ValidationGroup = validationGroup;
                    ucEditorSiteActionTemplate.RebindValidators(validationGroup);
                }

                radMultiPage.FindPageViewByID("radPageView" + index).Selected = true;
            }

            if (e.CommandName.ToLower() == "back")
            {
                index = currentIndex - 1;
                radTabStrip.Tabs[index].Enabled = true;
                radTabStrip.Tabs[index].Selected = true;
                radMultiPage.FindPageViewByID("radPageView" + index).Selected = true;

                if (index == 0)
                    rfvTags.ValidationGroup = string.Empty;
                if (index == 1)
                {
                    ucEditorSiteActionTemplate.ValidationGroup = string.Empty;
                    ucEditorSiteActionTemplate.RebindValidators(string.Empty);
                }
            }

            lbtnSendNow.Visible = index == 2;
            lbtnSchedule.Visible = index == 2;
            lbtnTest.Visible = index == 2;

            lbtnBack.Visible = index != 0;
            lbtnNext.Visible = index != 2;
        }



        /// <summary>
        /// Saves this instance.
        /// </summary>
        protected void Save()
        {
            if (!Page.IsValid)
                return;

            if (!access.Write)
                return;

            tbl_MassMail massMail = null;

            massMail = DataManager.MassMail.SelectById(SiteId, _massMailId) ?? new tbl_MassMail();

            massMail.SiteID = SiteId;
            massMail.Name = txtName.Text;
            massMail.SiteActionTemplateID = SaveSiteActionTemplate();
            massMail.OwnerID = CurrentUser.Instance.ContactID;
            if (_massMailId == Guid.Empty && rdtpSchedule.SelectedDate != null)
                massMail.MailDate = rdtpSchedule.SelectedDate;
            else
            {
                if (rdtpMailDate.SelectedDate != null)
                    massMail.MailDate = rdtpMailDate.SelectedDate;
            }

            if (massMail.ID == Guid.Empty)
            {
                if (rblTargetContacts.SelectedValue == "0")
                {
                    massMail.SiteTagID = rblTags.SelectedValue.ToGuid();
                    SelectedContactList.Clear();
                }
                else
                    massMail.SiteTagID = null;

                massMail.MassMailStatusID = (int)MassMailStatus.Scheduled;
                massMail = DataManager.MassMail.Add(massMail);
            }
            else
                DataManager.MassMail.Update(massMail);

            DataManager.MassMailContact.Save(SiteId, SelectedContactList, massMail.ID);

            _massMailId = massMail.ID;
        }



        /// <summary>
        /// Saves the site action template.
        /// </summary>
        /// <returns></returns>
        protected Guid SaveSiteActionTemplate()
        {
            tbl_SiteActionTemplate siteActionTemplate = null;

            siteActionTemplate = DataManager.SiteActionTemplate.SelectById(SiteActionTemplateId) ?? new tbl_SiteActionTemplate();
            siteActionTemplate.SiteID = SiteId;
            siteActionTemplate.Title = string.Empty;
            siteActionTemplate.FromEmail = string.Empty;
            siteActionTemplate.SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.MassMail;
            siteActionTemplate.OwnerID = CurrentUser.Instance.ContactID;
            siteActionTemplate.ActionTypeID = 1;

            if (siteActionTemplate.ID == Guid.Empty)
                siteActionTemplate = DataManager.SiteActionTemplate.Add(siteActionTemplate);
            else
                DataManager.SiteActionTemplate.Update(siteActionTemplate);

            SiteActionTemplateId = siteActionTemplate.ID;

            ucEditorSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            ucEditorSiteActionTemplate.Save(); // editor is first for save!
            ucSettingsSiteActionTemplate.SiteActionTemplateId = SiteActionTemplateId;
            ucSettingsSiteActionTemplate.Save();

            return siteActionTemplate.ID;
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


        protected void lbtnCancelMail_OnClick(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);
            massMail.MassMailStatusID = (int)MassMailStatus.Cancel;
            DataManager.MassMail.Update(massMail);

            Response.Redirect(UrlsData.AP_MassMails());
        }

        protected void RadButton1_OnClick(object sender, EventArgs e)
        {
            Save();
            Response.Redirect(UrlsData.AP_MassMails());
        }

        protected void lbtnTest_OnClick(object sender, EventArgs e)
        {
            TestSend();
        }

        protected void lbtnSendNow_OnClick(object sender, EventArgs e)
        {
            if (_massMailId == Guid.Empty)
                Save();

            var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);
            massMail.MailDate = DateTime.Now;
            DataManager.MassMail.Update(massMail);

            DataManager.MassMailContact.AddToQueueSiteAction(_massMailId);
            Response.Redirect(UrlsData.AP_MassMails());
        }


        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            Save();
            Response.Redirect(UrlsData.AP_MassMails());
        }





        protected void TestSend()
        {
            try
            {
                string msg = "";
                bool doSend = true;

                var site = DataManager.Sites.SelectById(SiteId);
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

                var contact = DataManager.Contact.SelectById(site.ID, (Guid)CurrentUser.Instance.ContactID);

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
                    var siteColumns = DataManager.SiteColumns.SelectByCode(site.ID, result.Value.Replace("#User.", "").Replace("#", ""));
                    if (siteColumns != null)
                    {
                        var contactColumnValue = DataManager.ContactColumnValues.Select(contact.ID, siteColumns.ID);
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

                        var siteActionLink = DataManager.SiteActionLink.Select(contact.ID, match.Groups["href"].Value);
                        if (siteActionLink == null)
                        {
                            siteActionLink = new tbl_SiteActionLink
                            {
                                ContactID = contact.ID,
                                LinkURL = match.Groups["href"].Value
                            };
                            DataManager.SiteActionLink.Add(siteActionLink);
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
                            var utmCampaign = txtName.Text;
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
                    var siteActionLink = DataManager.SiteActionLink.Select(contact.ID, DataManager.Links.Select(site.ID, match.Groups["code"].Value).ID);
                    if (siteActionLink == null)
                    {
                        siteActionLink = new tbl_SiteActionLink
                        {
                            ContactID = contact.ID,
                            SiteActivityRuleID = DataManager.Links.Select(site.ID, match.Groups["code"].Value).ID
                        };
                        DataManager.SiteActionLink.Add(siteActionLink);
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


    }
}