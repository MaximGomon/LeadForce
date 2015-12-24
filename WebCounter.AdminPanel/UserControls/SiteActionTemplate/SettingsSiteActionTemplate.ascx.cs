using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.AdminPanel.UserControls.DictionaryEditForm;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SettingsSiteActionTemplate : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        private Guid _siteId = CurrentUser.Instance.SiteID;
        protected RadAjaxManager radAjaxManager = null;

        public string PopupClientId { get; set; }

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
        public string ValidationGroup
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    rfvTitle.ValidationGroup = value;
                    rfvActionType.ValidationGroup = value;
                    rfvFromEmail.ValidationGroup = value;
                    revFromEmail.ValidationGroup = value;
                    rfvReplyEmail.ValidationGroup = value;
                    rfvToEmail.ValidationGroup = value;
                    revToEmail.ValidationGroup = value;
                    rfvRecipients.ValidationGroup = value;
                }
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

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool ShowTitle
        {
            get
            {
                if (ViewState["ShowTitle"] == null)
                    ViewState["ShowTitle"] = true;

                return (bool)ViewState["ShowTitle"];
            }
            set
            {
                ViewState["ShowTitle"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool EnableClientScript
        {
            get
            {
                if (ViewState["EnableClientScript"] == null)
                    ViewState["EnableClientScript"] = true;

                return (bool)ViewState["EnableClientScript"];
            }
            set
            {
                ViewState["EnableClientScript"] = value;
            }
        }

        public List<SiteActionTemplateRecipientMap> RecipientsList
        {
            get
            {
                if (ViewState["Recipients"] == null)
                    ViewState["Recipients"] = new List<SiteActionTemplateRecipientMap>();
                return (List<SiteActionTemplateRecipientMap>)ViewState["Recipients"];
            }
            set { ViewState["Recipients"] = value; }
        }

        public List<SiteActionTemplateRecipientMap> RecipientsPopupList
        {
            get
            {
                if (ViewState["RecipientsPopup"] == null)
                    ViewState["RecipientsPopup"] = new List<SiteActionTemplateRecipientMap>();
                return (List<SiteActionTemplateRecipientMap>)ViewState["RecipientsPopup"];
            }
            set { ViewState["RecipientsPopup"] = value; }
        }
        public bool FromSession { get; set; }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlTitle.Visible = ShowTitle;
            pnlActionType.Visible = ShowActionType;

            if (!Page.IsPostBack)
                BindData();

            rfvTitle.EnableClientScript = EnableClientScript;
            rfvActionType.EnableClientScript = EnableClientScript;
            rfvFromEmail.EnableClientScript = EnableClientScript;
            revFromEmail.EnableClientScript = EnableClientScript;
            rfvReplyEmail.EnableClientScript = EnableClientScript;
            rfvToEmail.EnableClientScript = EnableClientScript;
            revToEmail.EnableClientScript = EnableClientScript;
            rfvRecipients.EnableClientScript = EnableClientScript;

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSavePopup, racbRecipients);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rbRecipients, racbRecipientsPopup);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rbAddRole, racbRecipientsPopup);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rbAddCustom, racbRecipientsPopup);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rbAddCustom, txtEmailPopup, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rbAddCustom, txtDisplayNamePopup, null, UpdatePanelRenderMode.Inline);

            rbRoleDictonay.OnClientClicked = this.ClientID + "_ShowRoleDictonaryRadWindow";
            edsRoleDictonary.Where = string.Format("it.SiteID = GUID '{0}'", _siteId);

            rgRoleDictonary.Culture = new CultureInfo("ru-RU");
        }



        /// <summary>
        /// Handles the ItemsRequested event of the SettingsSiteActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs"/> instance containing the event data.</param>
        protected void SettingsSiteActionTemplate_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ((RadComboBox)dcbContactRole.FindControl("rcbDictionary")).Items.Insert(0, new RadComboBoxItem("Конкретные Email и имя"));
        }


        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            ddlActionType.DataSource = _dataManager.ActionTypes.SelectAll();
            ddlActionType.DataTextField = "Title";
            ddlActionType.DataValueField = "ID";
            ddlActionType.DataBind();

            rblReplaceLinks.Items.Clear();
            rblReplaceLinks.Items.Add(new ListItem(EnumHelper.GetEnumDescription(BusinessLogicLayer.Enumerations.ReplaceLinks.None), ((int)BusinessLogicLayer.Enumerations.ReplaceLinks.None).ToString()));
            rblReplaceLinks.Items.Add(new ListItem(EnumHelper.GetEnumDescription(BusinessLogicLayer.Enumerations.ReplaceLinks.ThroughService), ((int)BusinessLogicLayer.Enumerations.ReplaceLinks.ThroughService).ToString()));
            rblReplaceLinks.Items.Add(new ListItem(EnumHelper.GetEnumDescription(BusinessLogicLayer.Enumerations.ReplaceLinks.GoogleLinks), ((int)BusinessLogicLayer.Enumerations.ReplaceLinks.GoogleLinks).ToString()));

            pnlTitle.Visible = ShowTitle;
            pnlActionType.Visible = ShowActionType;

            if (SiteActionTemplateCategory != SiteActionTemplateCategory.Workflow)
            {
                var filter = new DictionaryOnDemandComboBox.DictionaryFilterColumn
                {
                    Name = "RoleTypeID",
                    DbType = DbType.Int32,
                    Value = ((int)ContactRoleType.WorkflowRole).ToString(),
                    Operation = FilterOperation.NotEqual
                };

                dcbContactRole.Filters.Add(filter);
                dcbContactRolePopup.Filters.Add(filter);
            }

            dcbContactRole.SiteID = _siteId;
            dcbContactRole.BindData();
            dcbContactRolePopup.SiteID = _siteId;
            dcbContactRolePopup.BindData();

            var optionReplyEmailName = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "ReplyEmailName");
            trReplyEmailName.Visible = optionReplyEmailName != null;

            racbRecipients.Entries.Clear();
            
            if (SiteActionTemplateId != Guid.Empty)
            {
                tbl_SiteActionTemplate siteActionTemplate;
                if (!FromSession)
                    siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(_siteId, SiteActionTemplateId);
                else
                    siteActionTemplate = ((List<tbl_SiteActionTemplate>)Session["SiteActionTemplates"]).FirstOrDefault(a => a.ID == SiteActionTemplateId);

                txtTitle.Text = siteActionTemplate.SiteActionTemplateCategoryID == (int)SiteActionTemplateCategory.System ? siteActionTemplate.Title : siteActionTemplate.MessageCaption;

                ddlActionType.Items.FindByValue(siteActionTemplate.ActionTypeID.ToString()).Selected = true;
                //txtToEmail.Text = siteActionTemplate.ToEmail;
                txtReplyEmail.Text = siteActionTemplate.ReplyToEmail;
                txtReplyName.Text = siteActionTemplate.ReplyToName;
                rblReplaceLinks.Items.FindByValue(siteActionTemplate.ReplaceLinksID.ToString()).Selected = true;

                if (siteActionTemplate.FromContactRoleID.HasValue)
                {
                    dcbContactRole.SelectedId = (Guid)siteActionTemplate.FromContactRoleID;
                    dcbContactRole.SelectedText = _dataManager.ContactRole.SelectById(_siteId, (Guid)siteActionTemplate.FromContactRoleID).Title;
                }
                else
                {
                    txtFromEmail.Text = siteActionTemplate.FromEmail;
                    txtFromName.Text = siteActionTemplate.FromName;
                    dcbContactRole.SelectedIdNullable = null;
                    dcbContactRole.SelectedText = "Конкретные Email и имя";
                }

                pnlFrom.Visible = !siteActionTemplate.FromContactRoleID.HasValue;
                pnlToEmail.Visible = (ActionType)siteActionTemplate.ActionTypeID != ActionType.EmailToUser;

                if (!FromSession)
                {
                    ViewState["Recipients"] =
                        _dataManager.SiteActionTemplateRecipient.SelectAll(SiteActionTemplateId).Select(
                            a =>
                            new SiteActionTemplateRecipientMap()
                                {
                                    ID = a.ID,
                                    SiteActionTemplateID = a.SiteActionTemplateID,
                                    ContactID = a.ContactID,
                                    ContactRoleID = a.ContactRoleID,
                                    Email = a.Email,
                                    DisplayName = a.DisplayName
                                }).ToList();
                }
                else
                {
                    ViewState["Recipients"] =
                        siteActionTemplate.tbl_SiteActionTemplateRecipient.Select(
                            a =>
                            new SiteActionTemplateRecipientMap()
                                {
                                    ID = a.ID,
                                    SiteActionTemplateID = a.SiteActionTemplateID,
                                    ContactID = a.ContactID,
                                    ContactRoleID = a.ContactRoleID,
                                    Email = a.Email,
                                    DisplayName = a.DisplayName
                                }).ToList();
                }

                foreach (var recipient in RecipientsList)
                {
                    var entry = new AutoCompleteBoxEntry();
                    if (recipient.ContactID.HasValue)
                    {
                        var contact = _dataManager.Contact.SelectById(_siteId, (Guid)recipient.ContactID);
                        if (contact != null)
                        {
                            entry.Text = !string.IsNullOrEmpty(contact.UserFullName)
                                        ? string.Format("{0} &lt;{1}&gt;", contact.UserFullName, contact.Email)
                                        : string.Format("&lt;{0}&gt;", contact.Email);
                            entry.Value = "Contact|" + contact.ID.ToString();
                            RecipientsList.Find(a => a.ID == recipient.ID).Key = entry.Value;
                        }
                    }

                    if (recipient.ContactRoleID.HasValue)
                    {
                        var contactRole = _dataManager.ContactRole.SelectById(_siteId, (Guid)recipient.ContactRoleID);
                        if (contactRole != null)
                        {
                            entry.Text = contactRole.Title;
                            entry.Value = "Role|" + contactRole.ID.ToString();
                            RecipientsList.Find(a => a.ID == recipient.ID).Key = entry.Value;
                        }
                    }

                    if (!recipient.ContactID.HasValue && !recipient.ContactRoleID.HasValue)
                    {
                        entry.Text = !string.IsNullOrEmpty(recipient.DisplayName)
                                    ? string.Format("{0} &lt;{1}&gt;", recipient.DisplayName, recipient.Email)
                                    : string.Format("&lt;{0}&gt;", recipient.Email);
                    }

                    racbRecipients.Entries.Add(entry);
                }
            }
            else
            {
                txtTitle.Text = string.Empty;
                ddlActionType.Items[0].Selected = true;
                txtToEmail.Text = string.Empty;
                txtFromEmail.Text = string.Empty;
                txtFromName.Text = string.Empty;
                txtReplyEmail.Text = string.Empty;
                txtReplyName.Text = string.Empty;
                rblReplaceLinks.Items[0].Selected = true;
                pnlToEmail.Visible = false;

                ViewState["Recipients"] = new List<SiteActionTemplateRecipientMap>();
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlActionType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlActionType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((DropDownList)sender).SelectedValue) && (ActionType)int.Parse(((DropDownList)sender).SelectedValue) == ActionType.EmailToFixed)
                pnlToEmail.Visible = true;
            else
                pnlToEmail.Visible = false;

            if (!Page.ClientScript.IsStartupScriptRegistered(PopupClientId + "_AutoHeightRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), PopupClientId + "_AutoHeightRadWindow", PopupClientId + "_AutoHeightRadWindow();", true);
        }


        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            tbl_SiteActionTemplate siteActionTemplate;
            if (!FromSession)
                siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(_siteId, SiteActionTemplateId);
            else
                siteActionTemplate = ((List<tbl_SiteActionTemplate>)Session["SiteActionTemplates"]).FirstOrDefault(a => a.ID == SiteActionTemplateId);

            var optionReplyEmailName = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "ReplyEmailName");

            if (siteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.BaseTemplate && siteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.System)
                siteActionTemplate.Title = siteActionTemplate.MessageCaption;
            else
                siteActionTemplate.Title = txtTitle.Text;
            
            siteActionTemplate.ActionTypeID = ddlActionType.SelectedValue.ToInt();
            //siteActionTemplate.ToEmail = (ActionType)siteActionTemplate.ActionTypeID == ActionType.EmailToUser ? null : txtToEmail.Text;
            if (dcbContactRole.SelectedId != Guid.Empty)
            {
                siteActionTemplate.FromContactRoleID = dcbContactRole.SelectedId;
                siteActionTemplate.FromEmail = null;
                siteActionTemplate.FromName = null;
            }
            else
            {
                siteActionTemplate.FromContactRoleID = null;
                siteActionTemplate.FromEmail = txtFromEmail.Text;
                siteActionTemplate.FromName = txtFromName.Text;
            }

            if (optionReplyEmailName != null)
            {
                siteActionTemplate.ReplyToEmail = txtReplyEmail.Text;
                siteActionTemplate.ReplyToName = txtReplyName.Text;
            }
            else
            {
                siteActionTemplate.ReplyToEmail = null;
                siteActionTemplate.ReplyToName = null;
            }

            siteActionTemplate.ReplaceLinksID = rblReplaceLinks.SelectedValue.ToInt();

            if (!FromSession)
            {
                _dataManager.SiteActionTemplate.Update(siteActionTemplate);
                _dataManager.SiteActionTemplateRecipient.Save(RecipientsList, siteActionTemplate.ID);
            }
            else
            {
                siteActionTemplate.tbl_SiteActionTemplateRecipient.Clear();
                foreach (var recipient in RecipientsList)
                {
                    siteActionTemplate.tbl_SiteActionTemplateRecipient.Add(new tbl_SiteActionTemplateRecipient
                                                                               {
                                                                                   ID = Guid.NewGuid(),
                                                                                   ContactID = recipient.ContactID,
                                                                                   ContactRoleID = recipient.ContactRoleID,
                                                                                   Email = recipient.Email,
                                                                                   DisplayName = recipient.DisplayName,
                                                                                   SiteActionTemplateID = recipient.SiteActionTemplateID
                                                                               });
                }
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbContactRole control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbContactRole_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            pnlFrom.Visible = string.IsNullOrEmpty(e.Value);

            if (!Page.ClientScript.IsStartupScriptRegistered(PopupClientId + "_AutoHeightRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), PopupClientId + "_AutoHeightRadWindow", PopupClientId + "_AutoHeightRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnEntryAdded event of the racbRecipients control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AutoCompleteEntryEventArgs"/> instance containing the event data.</param>
        protected void racbRecipients_OnEntryAdded(object sender, AutoCompleteEntryEventArgs e)
        {
            var recipient = new SiteActionTemplateRecipientMap { Key = e.Entry.Value };
            if (string.IsNullOrEmpty(e.Entry.Value))
            {
                if (Regex.IsMatch(e.Entry.Text, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                {
                    recipient.Email = e.Entry.Text;
                    RecipientsList.Add(recipient);
                }
            }
            else
            {
                var val = e.Entry.Value.Split('|');
                switch (val[0])
                {
                    case "Contact":
                        recipient.ContactID = val[1].ToGuid();
                        break;
                    case "Role":
                        recipient.ContactRoleID = val[1].ToGuid();
                        break;
                }
                RecipientsList.Add(recipient);
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(PopupClientId + "_AutoHeightRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), PopupClientId + "_AutoHeightRadWindow", PopupClientId + "_AutoHeightRadWindowWithFocus('" + racbRecipients.ClientID + "');", true);
        }



        /// <summary>
        /// Handles the OnEntryRemoved event of the racbRecipients control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AutoCompleteEntryEventArgs"/> instance containing the event data.</param>
        protected void racbRecipients_OnEntryRemoved(object sender, AutoCompleteEntryEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Entry.Value))
                RecipientsList.RemoveAll(a => a.Key == e.Entry.Value);
            else
            {
                RecipientsList.RemoveAll(
                a =>
                a.Email == e.Entry.Text || "&lt;" + a.Email + "&gt;" == e.Entry.Text ||
                (!string.IsNullOrEmpty(a.DisplayName)
                    ? string.Format("{0} &lt;{1}&gt;", a.DisplayName, a.Email)
                    : string.Format("&lt;{0}&gt;", a.Email)) == e.Entry.Text);
            }


            if (!Page.ClientScript.IsStartupScriptRegistered(PopupClientId + "_AutoHeightRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), PopupClientId + "_AutoHeightRadWindow", PopupClientId + "_AutoHeightRadWindowWithFocus('" + racbRecipients.ClientID + "');", true);
        }



        /// <summary>
        /// Handles the OnClick event of the rbRecipients control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbRecipients_OnClick(object sender, EventArgs e)
        {
            racbRecipientsPopup.Entries.Clear();
            foreach (AutoCompleteBoxEntry entry in racbRecipients.Entries)
                racbRecipientsPopup.Entries.Add(entry);

            RecipientsPopupList = RecipientsList;

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_ShowRecipientRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_ShowRecipientRadWindow", this.ClientID + "_ShowRecipientRadWindow();", true);

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_AutoHeight"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_AutoHeight", this.ClientID + "_AutoHeight();", true);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSavePopup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSavePopup_OnClick(object sender, EventArgs e)
        {
            racbRecipients.Entries.Clear();
            foreach (AutoCompleteBoxEntry entry in racbRecipientsPopup.Entries)
                racbRecipients.Entries.Add(entry);

            RecipientsList = RecipientsPopupList;

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_CloseRecipientRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_CloseRecipientRadWindow", this.ClientID + "_CloseRecipientRadWindow();", true);

            if (!Page.ClientScript.IsStartupScriptRegistered(PopupClientId + "_AutoHeightRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), PopupClientId + "_AutoHeightRadWindow", PopupClientId + "_AutoHeightRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnClick event of the rbAddRole control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbAddRole_OnClick(object sender, EventArgs e)
        {
            var entry = new AutoCompleteBoxEntry { Text = dcbContactRolePopup.SelectedText, Value = "Role|" + dcbContactRolePopup.SelectedId };
            racbRecipientsPopup.Entries.Add(entry);
            RecipientsPopupList.Add(new SiteActionTemplateRecipientMap { ContactRoleID = dcbContactRolePopup.SelectedId, Key = "Role|" + dcbContactRolePopup.SelectedId });

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_AutoHeight"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_AutoHeight", this.ClientID + "_AutoHeight();", true);
        }



        /// <summary>
        /// Handles the OnEntryAdded event of the racbRecipientsPopup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AutoCompleteEntryEventArgs"/> instance containing the event data.</param>
        protected void racbRecipientsPopup_OnEntryAdded(object sender, AutoCompleteEntryEventArgs e)
        {
            var recipient = new SiteActionTemplateRecipientMap { Key = e.Entry.Value };
            if (string.IsNullOrEmpty(e.Entry.Value))
            {
                if (Regex.IsMatch(e.Entry.Text, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                {
                    recipient.Email = e.Entry.Text;
                    RecipientsPopupList.Add(recipient);
                }
            }
            else
            {
                var val = e.Entry.Value.Split('|');
                switch (val[0])
                {
                    case "Contact":
                        recipient.ContactID = val[1].ToGuid();
                        break;
                    case "Role":
                        recipient.ContactRoleID = val[1].ToGuid();
                        break;
                }
                RecipientsPopupList.Add(recipient);
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_AutoHeight"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_AutoHeight", this.ClientID + "_AutoHeight();", true);
        }



        /// <summary>
        /// Handles the OnEntryRemoved event of the racbRecipientsPopup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AutoCompleteEntryEventArgs"/> instance containing the event data.</param>
        protected void racbRecipientsPopup_OnEntryRemoved(object sender, AutoCompleteEntryEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Entry.Value))
                RecipientsPopupList.RemoveAll(a => a.Key == e.Entry.Value);
            else
            {
                RecipientsPopupList.RemoveAll(
                    a =>
                    a.Email == e.Entry.Text || "&lt;" + a.Email + "&gt;" == e.Entry.Text ||
                    (!string.IsNullOrEmpty(a.DisplayName)
                         ? string.Format("{0} &lt;{1}&gt;", a.DisplayName, a.Email)
                         : string.Format("&lt;{0}&gt;", a.Email)) == e.Entry.Text);
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_AutoHeight"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_AutoHeight", this.ClientID + "_AutoHeight();", true);
        }



        /// <summary>
        /// Handles the OnClick event of the rbAddCustom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbAddCustom_OnClick(object sender, EventArgs e)
        {
            var entryText = !string.IsNullOrEmpty(txtDisplayNamePopup.Text)
                                ? string.Format("{0} &lt;{1}&gt;", txtDisplayNamePopup.Text, txtEmailPopup.Text)
                                : string.Format("&lt;{0}&gt;", txtEmailPopup.Text);
            var entry = new AutoCompleteBoxEntry { Text = entryText };
            racbRecipientsPopup.Entries.Add(entry);
            RecipientsPopupList.Add(new SiteActionTemplateRecipientMap { Email = txtEmailPopup.Text, DisplayName = !string.IsNullOrEmpty(txtDisplayNamePopup.Text) ? txtDisplayNamePopup.Text : null });

            txtEmailPopup.Text = string.Empty;
            txtDisplayNamePopup.Text = string.Empty;

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_AutoHeight"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_AutoHeight", this.ClientID + "_AutoHeight();", true);
        }



        /// <summary>
        /// Handles the OnNeedDataSource event of the rgRoleDictonary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgRoleDictonary_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgRoleDictonary.DataSource = _dataManager.ContactRole.SelectAll(_siteId);
        }



        /// <summary>
        /// Handles the OnDeleteCommand event of the rgRoleDictonary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgRoleDictonary_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            Guid contactRoleId = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString().ToGuid();
            _dataManager.ContactRole.Delete(_siteId, contactRoleId);
        }



        /// <summary>
        /// Handles the OnClick event of the lbRoleDictonarySave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbRoleDictonarySave_OnClick(object sender, EventArgs e)
        {
            dcbContactRole.BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_CloseRoleDictonaryRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_CloseRoleDictonaryRadWindow", this.ClientID + "_CloseRoleDictonaryRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgRoleDictonary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgRoleDictonary_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = ((WebCounter.DataAccessLayer.tbl_ContactRole)(e.Item.DataItem));
                ((Literal)item.FindControl("litRoleType")).Text = EnumHelper.GetEnumDescription((ContactRoleType)(data.RoleTypeID));
            } 
        }
    }
}