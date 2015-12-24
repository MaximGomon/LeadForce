using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using Labitec.UI.Dictionary;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class ContactEdit : System.Web.UI.UserControl
    {
        public Access access;
        readonly DataManager _dataManager = new DataManager();
        protected RadAjaxManager radAjaxManager = null;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteId
        {
            get
            {
                object o = ViewState["SiteId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["SiteId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ContactId
        {
            get
            {
                object o = ViewState["ContactId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["ContactId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? OwnerId
        {
            get
            {
                object o = ViewState["OwnerId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["OwnerId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? CompanyId
        {
            get
            {
                object o = ViewState["CompanyId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["CompanyId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Section Section
        {
            get
            {
                object o = ViewState["Section"];
                return (o == null ? Section.Monitoring : (Section)o);
            }
            set
            {
                ViewState["Section"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            access = Access.Check();
            if (!access.Write)
            {
                lbtnConfirm.Visible = false;                
            }

            radAjaxManager = RadAjaxManager.GetCurrent(this.Page);
            radAjaxManager.AjaxRequest += radAjaxManager_AjaxRequest;            
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnConfirm, rttmCheckFIO, null);
            radAjaxManager.AjaxSettings.AddAjaxSetting(imgbtnOk, rttmCheckFIO, null);
            

            rttmCheckFIO.TargetControls.Clear();
            rttmCheckFIO.TargetControls.Add(lbtnConfirm.ClientID, true);
            rttmCheckFIO.TargetControls.Add(imgbtnOk.ClientID, true);
            
            if (!Page.IsPostBack)
                rttmCheckFIO.TargetControls.Add(lbtnConfirm.ClientID, true);
        }



        /// <summary>
        /// Handles the AjaxRequest event of the radAjaxManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void radAjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "AddCompany")
            {
                if (Session["UpdatedContactId"] != null && Session["CompanyTitle"] != null)
                {
                    var company = new tbl_Company
                                      {
                                          ID = Guid.NewGuid(),
                                          CreatedAt = DateTime.Now,
                                          SiteID = CurrentUser.Instance.SiteID,
                                          Name = Session["CompanyTitle"].ToString(),
                                          StatusID = _dataManager.Status.SelectDefault(CurrentUser.Instance.SiteID).ID
                                      };
                    _dataManager.Company.Add(company);

                    var contact = _dataManager.Contact.SelectById(CurrentUser.Instance.SiteID, Guid.Parse(Session["UpdatedContactId"].ToString()));
                    contact.CompanyID = company.ID;
                    _dataManager.Contact.Update(contact);

                    Session["CompanyTitle"] = null;
                    Session["UpdatedContactId"] = null;

                    Response.Redirect(UrlsData.AP_Company(company.ID));
                }                
            }

            if (e.Argument == "CancelAddCompany")
            {
                Session["CompanyTitle"] = null;
                Session["UpdatedContactId"] = null;
                Response.Redirect(UrlsData.AP_Contacts());
            }
        }



        /// <summary>
        /// Called when [ajax update].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Telerik.Web.UI.ToolTipUpdateEventArgs"/> instance containing the event data.</param>
        protected void OnAjaxUpdate(object sender, ToolTipUpdateEventArgs args)
        {
            UpdateToolTip(args.UpdatePanel);
        }



        /// <summary>
        /// Updates the tool tip.
        /// </summary>        
        /// <param name="panel">The panel.</param>
        private void UpdateToolTip( UpdatePanel panel)
        {
            var ctrl = Page.LoadControl("~/UserControls/NameCheckerTooltip.ascx");
            var nameChecker = (NameCheckerTooltip)ctrl;
            nameChecker.FullName = txtUserFullName.Text;            
            nameChecker.ContactId = ContactId;
            nameChecker.SiteId = SiteId;
            nameChecker.NameConfirmClicked += nameChecker_NameConfirmClicked;
            panel.ContentTemplateContainer.Controls.Add(ctrl);
        }



        /// <summary>
        /// Handles the NameConfirmClicked event of the nameChecker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void nameChecker_NameConfirmClicked(object sender, NameCheckerTooltip.NameConfirmEventArgs e)
        {
            lbtnConfirm.Visible = false;
            imgbtnOk.Visible = true;
            txtUserFullName.Text = e.FullName;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            BindContactTypes();        
            ucOwner.CurrentContactId = ContactId;
            ucOwner.SelectedValue = CurrentUser.Instance.ContactID;
            dcbAdvertisingPlatform.SiteID = CurrentUser.Instance.SiteID;
            dcbAdvertisingCampaign.SiteID = CurrentUser.Instance.SiteID;
            dcbAdvertisingType.SiteID = CurrentUser.Instance.SiteID;
            BindContactFunctionsInCompany();
            BindContactJobLevel();
            ucReffer.FilterByFullName = true;            
            dcbAdvertisingPlatform.Order = new List<DictionaryComboBox.DictionaryOrderColumn>();
            dcbAdvertisingPlatform.Order.Add(new DictionaryComboBox.DictionaryOrderColumn() { Name = "it.[Title]", Direction = "ASC" });

            foreach (var emailStatus in EnumHelper.EnumToList<EmailStatus>())
                ddlEmailStatus.Items.Add(new ListItem(EnumHelper.GetEnumDescription(emailStatus), ((int)emailStatus).ToString()));

            foreach (var celluralPhoneStatus in EnumHelper.EnumToList<CellularPhoneStatus>())
                ddlCellularPhoneStatus.Items.Add(new ListItem(EnumHelper.GetEnumDescription(celluralPhoneStatus), ((int)celluralPhoneStatus).ToString()));

            var contact = _dataManager.Contact.SelectById(SiteId, ContactId);

            if (contact != null)
            {
                txtUserFullName.Text = contact.UserFullName;

                if (contact.IsNameChecked)
                    imgbtnOk.Visible = true;
                else
                    lbtnConfirm.Visible = true;

                ddlContactType.SelectedIndex = ddlContactType.Items.IndexOf(ddlContactType.Items.FindByValue(contact.ContactTypeID.ToString()));
                if (contact.tbl_Priorities != null)
                {
                    lrlPriority.Text = contact.tbl_Priorities.Title;

                    if (!string.IsNullOrEmpty(contact.tbl_Priorities.Image))
                    {
                        imgPriority.ImageUrl = BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(SiteId, "tbl_Priorities") + contact.tbl_Priorities.Image;
                        imgPriority.AlternateText = imgPriority.ToolTip = contact.tbl_Priorities.Title;
                        imgPriority.Visible = true;
                    }
                }                
                ucOwner.SelectedValue = contact.OwnerID;
                if (contact.tbl_ReadyToSell != null)
                {
                    lrlReadyToSell.Text = contact.tbl_ReadyToSell.Title;

                    if (!string.IsNullOrEmpty(contact.tbl_ReadyToSell.Image))
                    {
                        imgReadyToSell.ImageUrl = BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(SiteId, "tbl_ReadyToSell") + contact.tbl_ReadyToSell.Image;
                        imgReadyToSell.AlternateText = imgReadyToSell.ToolTip = contact.tbl_ReadyToSell.Title;
                        imgReadyToSell.Visible = true;
                    }
                }
                ucCompany.SelectedValue = contact.CompanyID;
                txtJobTitle.Text = contact.JobTitle;
                ddlFunctionInCompany.SelectedIndex = ddlFunctionInCompany.Items.IndexOf(ddlFunctionInCompany.Items.FindByValue(contact.ContactFunctionInCompanyID.ToString()));
                ddlJobLevel.SelectedIndex = ddlJobLevel.Items.IndexOf(ddlJobLevel.Items.FindByValue(contact.ContactJobLevelID.ToString()));
                txtPhone.Text = contact.Phone;
                txtCellularPhone.Text = contact.CellularPhone;
                ddlCellularPhoneStatus.SelectedIndex = ddlCellularPhoneStatus.Items.IndexOf(ddlCellularPhoneStatus.Items.FindByValue(contact.CellularPhoneStatusID.ToString()));
                txtEmail.Text = contact.Email;
                ddlEmailStatus.SelectedIndex = ddlEmailStatus.Items.IndexOf(ddlEmailStatus.Items.FindByValue(contact.EmailStatusID.ToString()));
                if (ddlEmailStatus.SelectedValue.ToInt() == (int)EmailStatus.Banned
                    && CurrentUser.Instance.AccessLevelID != (int)AccessLevel.Administrator && CurrentUser.Instance.AccessLevelID != (int)AccessLevel.SystemAdministrator)
                    ddlEmailStatus.Enabled = false;
                ucPostalAddress.AddressId = contact.AddressID;
                ucPostalAddress.BindData();
                if (!string.IsNullOrEmpty(contact.RefferURL))
                {
                    lblRefferURL.Text = HttpUtility.UrlDecode(contact.RefferURL).Truncate(45, false, true);
                    lblRefferURL.ToolTip = HttpUtility.UrlDecode(contact.RefferURL);
                }
                rdpBirthday.SelectedDate = contact.BirthDate;
                lrlCreatedAt.Text = contact.CreatedAt.ToString();
                lrlLastActivity.Text = contact.LastActivityAt.ToString();
                lrlUserIP.Text = contact.UserIP;
                lrlScore.Text = contact.Score.ToString();

                OwnerId = contact.OwnerID;
                CompanyId = contact.CompanyID;
                
                ucReffer.SelectedValue = contact.RefferID;

                dcbAdvertisingCampaign.SelectedIdNullable = contact.AdvertisingCampaignID;

                dcbAdvertisingPlatform.SelectedIdNullable = contact.AdvertisingPlatformID;

                dcbAdvertisingType.SelectedIdNullable = contact.AdvertisingTypeID;

                txtComment.Text = contact.Comment;
                
                var contactSession = _dataManager.ContactSessions.SelectFirstSession(CurrentUser.Instance.SiteID, contact.ID);
                if (contactSession != null)
                {
                    lrlKeywords.Text = contactSession.Keywords;
                }
            }
        }



        /// <summary>
        /// Binds the contact job level.
        /// </summary>
        private void BindContactJobLevel()
        {
            ddlJobLevel.DataSource = _dataManager.ContactJobLevel.SelectAll(SiteId);
            ddlJobLevel.DataTextField = "Name";
            ddlJobLevel.DataValueField = "ID";
            ddlJobLevel.DataBind();
            ddlJobLevel.Items.Insert(0, new ListItem("Выберите значение", string.Empty) { Selected = true });
        }



        /// <summary>
        /// Binds the contact functions in company.
        /// </summary>
        private void BindContactFunctionsInCompany()
        {
            ddlFunctionInCompany.DataSource = _dataManager.ContactFunctionInCompany.SelectAll(SiteId);
            ddlFunctionInCompany.DataTextField = "Name";
            ddlFunctionInCompany.DataValueField = "ID";
            ddlFunctionInCompany.DataBind();
            ddlFunctionInCompany.Items.Insert(0, new ListItem("Выберите значение", string.Empty) { Selected = true });
        }



        /// <summary>
        /// Binds the contact types.
        /// </summary>
        private void BindContactTypes()
        {
            ddlContactType.DataSource = _dataManager.ContactType.SelectAll(SiteId);
            ddlContactType.DataTextField = "Name";
            ddlContactType.DataValueField = "ID";
            ddlContactType.DataBind();
            ddlContactType.Items.Insert(0, new ListItem("Выберите значение", string.Empty) { Selected = true });
        }



        /// <summary>
        /// Saves this instance.
        /// </summary>
        public bool Save()
        {
            if (!access.Write)
                return false;

            var contact = _dataManager.Contact.SelectById(SiteId, ContactId) ?? new tbl_Contact();

            if (contact.UserFullName != txtUserFullName.Text || !contact.IsNameChecked)
            {
                var nameChecker = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                var nameCheck = nameChecker.CheckName(txtUserFullName.Text, NameCheckerFormat.FIO, Correction.Correct);
                if (!string.IsNullOrEmpty(nameCheck))
                {
                    contact.UserFullName = nameCheck;
                    contact.Surname = nameChecker.Surname;
                    contact.Name = nameChecker.Name;
                    contact.Patronymic = nameChecker.Patronymic;
                    contact.IsNameChecked = nameChecker.IsNameCorrect;
                }
                else
                {
                    contact.UserFullName = txtUserFullName.Text;
                    contact.Name = string.Empty;
                    contact.Surname = string.Empty;
                    contact.Patronymic = string.Empty;
                    contact.IsNameChecked = false;
                }
            }
            else
            {
                contact.IsNameChecked = imgbtnOk.Visible;
            }

            contact.ContactTypeID = ddlContactType.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlContactType.SelectedValue) : null;
            contact.StatusID = _dataManager.Status.SelectDefault(CurrentUser.Instance.SiteID).ID;
            contact.OwnerID = ucOwner.SelectedValue;
            contact.CompanyID = ucCompany.SelectedValue;
            contact.JobTitle = txtJobTitle.Text;
            contact.ContactFunctionInCompanyID = ddlFunctionInCompany.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlFunctionInCompany.SelectedValue) : null;
            contact.ContactJobLevelID = ddlJobLevel.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlJobLevel.SelectedValue) : null;
            contact.Phone = txtPhone.Text;
            contact.CellularPhone = txtCellularPhone.Text;
            contact.CellularPhoneStatusID = int.Parse(ddlCellularPhoneStatus.SelectedValue);
            contact.Email = txtEmail.Text;
            if (ddlEmailStatus.SelectedValue.ToInt() != (int)EmailStatus.Banned
                || CurrentUser.Instance.AccessLevelID == (int)AccessLevel.SystemAdministrator || CurrentUser.Instance.AccessLevelID == (int)AccessLevel.Administrator)
                contact.EmailStatusID = int.Parse(ddlEmailStatus.SelectedValue);
            contact.AddressID = ucPostalAddress.Save();
            contact.RefferID = ucReffer.SelectedValue;
            contact.Comment = txtComment.Text;

            if (rdpBirthday.SelectedDate != null)
                contact.BirthDate = rdpBirthday.SelectedDate;

            if (dcbAdvertisingPlatform.SelectedIdNullable.HasValue)
                contact.AdvertisingPlatformID = dcbAdvertisingPlatform.SelectedIdNullable;
            else if (!string.IsNullOrEmpty(dcbAdvertisingPlatform.Text) && !string.Equals(dcbAdvertisingPlatform.EmptyItemText, dcbAdvertisingPlatform.Text, StringComparison.OrdinalIgnoreCase))
            {
                var advertisingPlatform = _dataManager.AdvertisingPlatform.SelectByTitle(CurrentUser.Instance.SiteID, dcbAdvertisingPlatform.Text);
                if (advertisingPlatform == null)
                {
                    advertisingPlatform = new tbl_AdvertisingPlatform {SiteID = CurrentUser.Instance.SiteID};
                    advertisingPlatform.Code = advertisingPlatform.Title = dcbAdvertisingPlatform.Text;
                    _dataManager.AdvertisingPlatform.Add(advertisingPlatform);
                }
                contact.AdvertisingPlatformID = advertisingPlatform.ID;
            }

            if (dcbAdvertisingCampaign.SelectedIdNullable.HasValue)
                contact.AdvertisingCampaignID = dcbAdvertisingCampaign.SelectedIdNullable;
            else if (!string.IsNullOrEmpty(dcbAdvertisingCampaign.Text) && !string.Equals(dcbAdvertisingCampaign.EmptyItemText, dcbAdvertisingCampaign.Text, StringComparison.OrdinalIgnoreCase))
            {
                var advertisingCampaign = _dataManager.AdvertisingCampaign.SelectByTitle(CurrentUser.Instance.SiteID, dcbAdvertisingCampaign.Text);
                if (advertisingCampaign == null)                                    
                {
                    advertisingCampaign = new tbl_AdvertisingCampaign { SiteID = CurrentUser.Instance.SiteID };
                    advertisingCampaign.Code = advertisingCampaign.Title = dcbAdvertisingCampaign.Text;
                    _dataManager.AdvertisingCampaign.Add(advertisingCampaign);
                }
                contact.AdvertisingCampaignID = advertisingCampaign.ID;
            }

            if (dcbAdvertisingType.SelectedIdNullable.HasValue)
                contact.AdvertisingTypeID = dcbAdvertisingType.SelectedIdNullable;
            else if (!string.IsNullOrEmpty(dcbAdvertisingType.Text) && !string.Equals(dcbAdvertisingType.EmptyItemText, dcbAdvertisingType.Text, StringComparison.OrdinalIgnoreCase))
            {
                var advertisingType = _dataManager.AdvertisingType.SelectByTitle(CurrentUser.Instance.SiteID, dcbAdvertisingType.Text);
                if (advertisingType == null)
                {
                    advertisingType = new tbl_AdvertisingType { SiteID = CurrentUser.Instance.SiteID };
                    advertisingType.Code = advertisingType.Title = dcbAdvertisingType.Text;
                    _dataManager.AdvertisingType.Add(advertisingType);
                }
                contact.AdvertisingTypeID = advertisingType.ID;
            }

            if (contact.ID == Guid.Empty)
            {
                contact.SiteID = SiteId;
                contact.RefferURL = string.Empty;
                contact.UserIP = string.Empty;
                contact.OwnerID = CurrentUser.Instance.ContactID;
                _dataManager.Contact.Add(contact);

                ContactId = contact.ID;
            }
            else
                _dataManager.Contact.Update(contact);

            if (!ucCompany.SelectedValue.HasValue && !string.IsNullOrEmpty(ucCompany.Text))
            {
                Session["UpdatedContactId"] = contact.ID;
                Session["CompanyTitle"] = ucCompany.Text;

                RadWindowManager1.RadConfirm("Создать новую компанию?", "confirmCallBackFn", 400, 100, null, "Создание компании");

                return false;
            }

            return true;
        }
    }
}