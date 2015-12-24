using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class ContactComboBox : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();
        public event RadComboBoxSelectedIndexChangedEventHandler SelectedIndexChanged;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool FilterByFullName
        {
            get
            {
                object o = ViewState["FilterByFullName"];
                return (o != null && (bool) o);
            }
            set { ViewState["FilterByFullName"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool SelectResponsibles
        {
            get
            {
                if (ViewState["SelectResponsibles"] == null)
                    ViewState["SelectResponsibles"] = false;

                return (bool)ViewState["SelectResponsibles"];
            }
            set { ViewState["SelectResponsibles"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid CurrentContactId
        {
            get
            {
                object o = ViewState["CurrentContactId"];
                return (o == null ? Guid.Empty : (Guid) o);
            }
            set { ViewState["CurrentContactId"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? CompanyId
        {
            get
            {
                return (Guid?)ViewState["CompanyId"];
            }
            set { ViewState["CompanyId"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Unit Width
        {
            get { return rcbContact.Width; }
            set { rcbContact.Width = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool Enabled
        {
            get { return rcbContact.Enabled; }
            set { rcbContact.Enabled = value; }
        }


        /// <summary>
        /// Gets the selected.
        /// </summary>
        public Guid? SelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(rcbContact.SelectedValue))
                    return Guid.Parse(rcbContact.SelectedValue);

                return null;
            }
            set
            {
                rcbContact.Items.Clear();
                rcbContact.Text = string.Empty;

                if (value.HasValue)
                {                    
                    var contact = DataManager.Contact.SelectById(CurrentUser.Instance.SiteID, (Guid)value);
                    if (contact != null)
                    {
                        rcbContact.Items.Add(new RadComboBoxItem(contact.UserFullName, contact.ID.ToString()){Selected = true});                        
                    }
                }
            }
        }



        /// <summary>
        /// Gets or sets the  Validation Group.
        /// </summary>
        /// <value>
        /// The empty item text.
        /// </value>
        public string ValidationGroup
        {
            get
            {
                object o = ViewState["ValidationGroup"];
                return (o == null ? string.Empty : (string)o);
            }
            set { ViewState["ValidationGroup"] = value; }
        }



        /// <summary>
        /// Gets or sets the  Validation Error Message.
        /// </summary>
        /// <value>
        /// The empty item text.
        /// </value>
        public string ValidationErrorMessage
        {
            get
            {
                object o = ViewState["ValidationErrorMessage"];
                return (o == null ? string.Empty : (string)o);
            }
            set { ViewState["ValidationErrorMessage"] = value; }
        }



        /// <summary>
        /// Gets or sets a value indicating whether [auto post back].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [auto post back]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoPostBack
        {
            get { return rcbContact.AutoPostBack; }
            set { rcbContact.AutoPostBack = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ValidationGroup))
                rfvDictionary.ValidationGroup = ValidationGroup;
            else
                rfvDictionary.Enabled = false;

            rcbContact.EmptyMessage = "Выберите значение";

            rfvDictionary.ErrorMessage = ValidationErrorMessage;

            if (SelectedIndexChanged != null)
                rcbContact.SelectedIndexChanged += rcbContact_SelectedIndexChanged;
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the rcbDictionary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void rcbContact_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }



        /// <summary>
        /// Handles the ItemsRequested event of the rcbContact control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs"/> instance containing the event data.</param>
        protected void rcbContact_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {            
            rcbContact.Items.Clear();

            IQueryable<tbl_Contact> contacts;

            if (SelectResponsibles)
            {
                contacts = DataManager.Requirement.SelectResponsibles(CurrentUser.Instance.SiteID,
                                                                      CurrentUser.Instance.ContactID);
            }
            else
            {
                if (!FilterByFullName)
                    contacts =
                        DataManager.Contact.SelectAll(CurrentUser.Instance.SiteID).Where(
                            c => c.tbl_User.Count > 0);
                else
                    contacts =
                        DataManager.Contact.SelectAll(CurrentUser.Instance.SiteID).Where(
                            c => !string.IsNullOrEmpty(c.UserFullName));
            }            

            if (CurrentContactId != Guid.Empty)
                contacts = contacts.Where(c => c.ID != CurrentContactId);

            if (CompanyId.HasValue)
                contacts = contacts.Where(c => c.CompanyID == CompanyId);

            if (!string.IsNullOrEmpty(e.Text))
                contacts = contacts.Where(c => c.UserFullName.ToLower().StartsWith(e.Text.ToLower()));
            var listOfContacts = contacts.ToList();
            var count = listOfContacts.Count;
            int itemOffset = e.NumberOfItems;
            int endOffset = Math.Min(itemOffset + 15, count);
            e.EndOfItems = endOffset == count;            

            for (int i = itemOffset; i < endOffset; i++)
            {
                rcbContact.Items.Add(new RadComboBoxItem(listOfContacts[i].UserFullName, listOfContacts[i].ID.ToString()));
            }

            e.Message = GetStatusMessage(endOffset, count);            
        }



        /// <summary>
        /// Gets the status message.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="total">The total.</param>
        /// <returns></returns>
        private static string GetStatusMessage(int offset, int total)
        {
            if (total <= 0)
                return "Пусто";

            return String.Format("Элементы <b>1</b>-<b>{0}</b> из <b>{1}</b>", offset, total);
        }
    }
}