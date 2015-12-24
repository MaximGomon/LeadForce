using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class UserEdit : LeadForceBasePage
    {
        private Guid _userId;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Пользователи - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                _userId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_Users();

            tagsUser.ObjectID = _userId;

            if ((AccessLevel)CurrentUser.Instance.AccessLevelID == AccessLevel.SystemAdministrator)
            {                
                dcbSite.SelectedIndexChanged += dcbSite_SelectedIndexChanged;
                dcbSite.AutoPostBack = true;
                RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbSite, rcbContact, null, UpdatePanelRenderMode.Inline);
            }

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            var currentUser = CurrentUser.Instance;

            tbl_User user;
            if ((AccessLevel)currentUser.AccessLevelID == AccessLevel.SystemAdministrator)
            {
                user = DataManager.User.SelectById(_userId);
                pnlSite.Visible = true;                
            }
            else
                user = DataManager.User.SelectById(_userId, SiteId);

            ddlAccessLevel.Items.Add(new ListItem("Выберите значение", ""));
            foreach (var accessLevel in EnumHelper.EnumToList<AccessLevel>())
            {
                if ((AccessLevel)currentUser.AccessLevelID == AccessLevel.Administrator && accessLevel == AccessLevel.SystemAdministrator)
                    continue;
                ddlAccessLevel.Items.Add(new ListItem(EnumHelper.GetEnumDescription(accessLevel), ((int)accessLevel).ToString()));
            }
            
            BindContacts(user == null ? SiteId : user.SiteID);

            rcbAccessProfile.DataSource = DataManager.AccessProfile.SelectAll(SiteId);
            rcbAccessProfile.DataTextField = "Title";
            rcbAccessProfile.DataValueField = "ID";
            rcbAccessProfile.DataBind();
            rcbAccessProfile.Items.Insert(0, new RadComboBoxItem("Выберите профиль", Guid.Empty.ToString()) { Selected = true });

            if (user != null)
            {
                txtEmail.Text = user.Login;
                txtPassword.Attributes.Add("value", user.Password);
                cbIsActive.Checked = user.IsActive;
                ddlAccessLevel.Items.FindByValue(user.AccessLevelID.ToString()).Selected = true;
                if (user.ContactID != null)
                    rcbContact.SelectedIndex = rcbContact.Items.FindItemIndexByValue(user.ContactID.ToString());
                if (user.AccessProfileID != null)
                    rcbAccessProfile.SelectedIndex = rcbAccessProfile.Items.FindItemIndexByValue(user.AccessProfileID.ToString());
                dcbSite.SelectedId = user.SiteID;
            }
        }

        void dcbSite_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindContacts(dcbSite.SelectedValue);
        }


        protected void BindContacts(Guid siteId)
        {            
            rcbContact.DataSource = DataManager.Contact.SelectAll(siteId).Where(a => !string.IsNullOrEmpty(a.UserFullName) || !string.IsNullOrEmpty(a.Email) || !string.IsNullOrEmpty(a.Phone));
            rcbContact.DataBind();
            rcbContact.Text = string.Empty;
            rcbContact.Items.Insert(0, new RadComboBoxItem("Выберите контакт", Guid.Empty.ToString()) { Selected = true });
        }


        /// <summary>
        /// Handles the OnItemDataBound event of the rcbContact control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemEventArgs"/> instance containing the event data.</param>
        protected void rcbContact_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var text = string.Empty;
            if (!string.IsNullOrEmpty(((tbl_Contact)e.Item.DataItem).UserFullName) && !string.IsNullOrEmpty(((tbl_Contact)e.Item.DataItem).UserFullName.Trim()))
                text += ((tbl_Contact)e.Item.DataItem).UserFullName + ", ";
            if (!string.IsNullOrEmpty(((tbl_Contact)e.Item.DataItem).Email) && !string.IsNullOrEmpty(((tbl_Contact)e.Item.DataItem).Email.Trim()))
                text += ((tbl_Contact)e.Item.DataItem).Email + ", ";
            if (!string.IsNullOrEmpty(((tbl_Contact)e.Item.DataItem).Phone) && !string.IsNullOrEmpty(((tbl_Contact)e.Item.DataItem).Phone.Trim()))
                text += ((tbl_Contact)e.Item.DataItem).Phone + ", ";

            text = text.Trim(new[] {' ', ','});

            e.Item.Text = text;
            e.Item.Value = ((tbl_Contact)e.Item.DataItem).ID.ToString();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            var currentUser = CurrentUser.Instance;

            tbl_User user;
            if ((AccessLevel)currentUser.AccessLevelID == AccessLevel.SystemAdministrator)
            {
                user = DataManager.User.SelectById(_userId) ?? new tbl_User();
                user.SiteID = dcbSite.SelectedId;
            }
            else
            {
                user = DataManager.User.SelectById(_userId, SiteId) ?? new tbl_User();
                user.SiteID = SiteId;
            }

            user.Login = txtEmail.Text;
            user.Password = txtPassword.Text;
            user.IsActive = cbIsActive.Checked;
            user.AccessLevelID = int.Parse(ddlAccessLevel.SelectedValue);
            if (rcbContact.SelectedValue != Guid.Empty.ToString())
                user.ContactID = Guid.Parse(rcbContact.SelectedValue);
            else
                user.ContactID = null;

            if (rcbAccessProfile.SelectedValue != Guid.Empty.ToString())
                user.AccessProfileID = Guid.Parse(rcbAccessProfile.SelectedValue);
            else
                user.AccessProfileID = null;

            if (user.ID == Guid.Empty)
                DataManager.User.Add(user);
            else
                DataManager.User.Update(user);

            tagsUser.SaveTags(user.ID);

            CurrentUser.UserInstanceFlush();

            Response.Redirect(UrlsData.AP_Users());
        }
    }
}