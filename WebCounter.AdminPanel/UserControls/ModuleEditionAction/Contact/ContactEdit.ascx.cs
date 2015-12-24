using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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

namespace WebCounter.AdminPanel.UserControls.ModuleEditionAction.Contact
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
            ucOwner.CurrentContactId = ContactId;
            ucOwner.SelectedValue = CurrentUser.Instance.ContactID;
            dcbAdvertisingPlatform.SiteID = CurrentUser.Instance.SiteID;
            dcbAdvertisingCampaign.SiteID = CurrentUser.Instance.SiteID;
            dcbAdvertisingType.SiteID = CurrentUser.Instance.SiteID;
            ucReffer.FilterByFullName = true;            


            var contact = _dataManager.Contact.SelectById(SiteId, ContactId);

            if (contact != null)
            {
                txtUserFullName.Text = contact.UserFullName;

                if (contact.IsNameChecked)
                    imgbtnOk.Visible = true;
                else
                    lbtnConfirm.Visible = true;

                ucOwner.SelectedValue = contact.OwnerID;
                ucCompany.SelectedValue = contact.CompanyID;
                txtJobTitle.Text = contact.JobTitle;
                txtPhone.Text = contact.Phone;
                txtCellularPhone.Text = contact.CellularPhone;
                txtEmail.Text = contact.Email;
                var WebContactCommunication = _dataManager.ContactCommunication.SelectByType(ContactId,
                                                                                             (int) CommunicationType.Web);
                if (WebContactCommunication != null)
                    txtWeb.Text = WebContactCommunication.CommunicationNumber;
                var FacebookContactCommunication = _dataManager.ContactCommunication.SelectByType(ContactId,
                                                                                             (int)CommunicationType.Facebook);
                if (FacebookContactCommunication != null)
                    txtFacebook.Text = FacebookContactCommunication.CommunicationNumber;
                var VKontakteContactCommunication = _dataManager.ContactCommunication.SelectByType(ContactId,
                                                                                             (int)CommunicationType.VKontakte);
                if (VKontakteContactCommunication != null)
                    txtVKontakte.Text = VKontakteContactCommunication.CommunicationNumber;
                var TwitterContactCommunication = _dataManager.ContactCommunication.SelectByType(ContactId,
                                                                                             (int)CommunicationType.Twitter);
                if (TwitterContactCommunication != null)
                    txtTwitter.Text = TwitterContactCommunication.CommunicationNumber;
                ucPostalAddress.AddressId = contact.AddressID;
                ucPostalAddress.BindData();
                lrlRefferURL.Text = contact.RefferURL;
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

                if(contact.EmailStatusID !=null)
                {
                    emailStatus.Visible = true;
                    if (contact.EmailStatusID == (int)EmailStatus.Allow)
                    {
                        emailStatus.CssClass = "statuses left green";
                        emailStatus.ToolTip = EnumHelper.GetEnumDescription(EmailStatus.Allow);
                    }
                    if (contact.EmailStatusID == (int)EmailStatus.NotConfirmed)
                    {
                        emailStatus.CssClass = "statuses left yellow";
                        emailStatus.ToolTip = EnumHelper.GetEnumDescription(EmailStatus.NotConfirmed);
                    }
                    if (contact.EmailStatusID == (int)EmailStatus.Banned)
                    {
                        emailStatus.CssClass = "statuses left red";
                        emailStatus.ToolTip = EnumHelper.GetEnumDescription(EmailStatus.Banned);
                    }
                    if (contact.EmailStatusID == (int)EmailStatus.DoesNotWork)
                    {
                        emailStatus.CssClass = "statuses left stop";
                        emailStatus.ToolTip = EnumHelper.GetEnumDescription(EmailStatus.DoesNotWork);
                    }
                }

                if (contact.CellularPhoneStatusID != null)
                {
                    cellularPhoneStatus.Visible = true;
                    if (contact.CellularPhoneStatusID == (int)CellularPhoneStatus.Allow)
                    {
                        cellularPhoneStatus.CssClass = "statuses left green";
                        cellularPhoneStatus.ToolTip = EnumHelper.GetEnumDescription(CellularPhoneStatus.Allow);
                    }
                    if (contact.CellularPhoneStatusID == (int)CellularPhoneStatus.NotConfirmed)
                    {
                        cellularPhoneStatus.CssClass = "statuses left yellow";
                        cellularPhoneStatus.ToolTip = EnumHelper.GetEnumDescription(CellularPhoneStatus.NotConfirmed);
                    }
                    if (contact.CellularPhoneStatusID == (int)CellularPhoneStatus.Banned)
                    {
                        cellularPhoneStatus.CssClass = "statuses left red";
                        cellularPhoneStatus.ToolTip = EnumHelper.GetEnumDescription(CellularPhoneStatus.Banned);
                    }
                    if (contact.CellularPhoneStatusID == (int)CellularPhoneStatus.DoesNotWork)
                    {
                        cellularPhoneStatus.CssClass = "statuses left stop";
                        cellularPhoneStatus.ToolTip = EnumHelper.GetEnumDescription(CellularPhoneStatus.DoesNotWork);
                    }
                }

            }
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

            //contact.ContactTypeID = ddlContactType.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlContactType.SelectedValue) : null;
            contact.StatusID = _dataManager.Status.SelectDefault(SiteId).ID;
            contact.OwnerID = ucOwner.SelectedValue;
            contact.CompanyID = ucCompany.SelectedValue;
            contact.JobTitle = txtJobTitle.Text;
            //contact.ContactFunctionInCompanyID = ddlFunctionInCompany.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlFunctionInCompany.SelectedValue) : null;
            //contact.ContactJobLevelID = ddlJobLevel.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlJobLevel.SelectedValue) : null;
            contact.Phone = txtPhone.Text;
            contact.CellularPhone = txtCellularPhone.Text;
            //contact.CellularPhoneStatusID = int.Parse(ddlCellularPhoneStatus.SelectedValue);
            contact.Email = txtEmail.Text;


            //contact.EmailStatusID = int.Parse(ddlEmailStatus.SelectedValue);
            contact.AddressID = ucPostalAddress.Save();
            contact.RefferID = ucReffer.SelectedValue;

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
                var company = new tbl_Company
                {
                    ID = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    SiteID = CurrentUser.Instance.SiteID,
                    Name = ucCompany.Text,
                    StatusID = _dataManager.Status.SelectDefault(CurrentUser.Instance.SiteID).ID
                };
                _dataManager.Company.Add(company);

                contact.CompanyID = company.ID;
                _dataManager.Contact.Update(contact);
            }
            SaveCommuncation(ContactId, (int)CommunicationType.Web, txtWeb.Text);
            SaveCommuncation(ContactId, (int)CommunicationType.Facebook, txtFacebook.Text);
            SaveCommuncation(ContactId, (int)CommunicationType.VKontakte, txtVKontakte.Text);
            SaveCommuncation(ContactId, (int)CommunicationType.Twitter, txtTwitter.Text);

            return true;
        }

        public void SaveCommuncation (Guid ContactId, int CommuncationTypeId, string ComuncationNumber)
        {
            var ContactCommunication = _dataManager.ContactCommunication.SelectByType(ContactId, CommuncationTypeId) ?? new tbl_ContactCommunication();
            if (ContactCommunication.ID != Guid.Empty)
            {
                if (ComuncationNumber.Trim() != String.Empty)
                {
                    ContactCommunication.CommunicationNumber = ComuncationNumber.Trim();
                    _dataManager.ContactCommunication.Update(ContactCommunication);
                } else
                {
                    _dataManager.ContactCommunication.Delete(ContactCommunication.ID);
                }
            }
            else
            {
                if (ComuncationNumber.Trim() != String.Empty)
                {

                    ContactCommunication.ID = Guid.NewGuid();
                    ContactCommunication.ContactID = ContactId;
                    ContactCommunication.CommunicationNumber = ComuncationNumber.Trim();
                    ContactCommunication.CommunicationType = CommuncationTypeId;
                    _dataManager.ContactCommunication.Add(ContactCommunication);
                }
            }
        }
    }
}