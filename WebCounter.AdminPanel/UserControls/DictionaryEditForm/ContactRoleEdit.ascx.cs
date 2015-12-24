using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Contact;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.DictionaryEditForm
{
    public partial class ContactRoleEdit : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();
        protected Guid SiteId = CurrentUser.Instance.SiteID;
        protected RadAjaxManager radAjaxManager = null;
        protected bool flag;


        private object _dataItem = null;

        /// <summary>
        /// Gets or sets the data item.
        /// </summary>
        /// <value>
        /// The data item.
        /// </value>
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }



        /// <summary>
        /// Gets the service level.
        /// </summary>
        protected tbl_ContactRole ContactRole
        {
            get { return (tbl_ContactRole)ViewState["ContactRole"]; }
            set { ViewState["ContactRole"] = value; }
        }



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<Guid> SelectedContactList
        {
            get
            {
                if (HttpContext.Current.Session["SelectedContactList"] == null)
                    HttpContext.Current.Session["SelectedContactList"] = new List<Guid>();
                return (List<Guid>)HttpContext.Current.Session["SelectedContactList"];
            }
            set
            {
                HttpContext.Current.Session["SelectedContactList"] = value;
            }
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (DataItem != null && !(DataItem is GridInsertionObject))
            {
                ContactRole = (tbl_ContactRole)DataItem;
                btnUpdate.CommandArgument = ContactRole.ID.ToString();
            }
                

            rblTargetContacts.Items.Clear();
            rblTargetContacts.Items.Add(new ListItem("Конкретный сегмент", "0"));
            rblTargetContacts.Items.Add(new ListItem("Произвольный список", "1"));
            rblTargetContacts.Items[0].Selected = true;

            var siteTags = DataManager.SiteTags.SelectAll(SiteId).Where(a => a.tbl_ObjectTypes.Name == "tbl_Contact");
            rblTags.DataSource = siteTags;
            rblTags.DataValueField = "ID";
            rblTags.DataTextField = "Name";
            rblTags.DataBind();

            if ((GetPostBackControl(this.Page) is ImageButton) && !flag && (GetPostBackControl(this.Page) == null || GetPostBackControl(this.Page).ID != "EditButton"))
            {
                flag = true;
                BindGridContacts();
                gridContacts.Rebind();
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataItem != null && !(DataItem is GridInsertionObject) && (GetPostBackControl(this.Page) == null || GetPostBackControl(this.Page).ID != "btnUpdate"))
            {
                SelectedContactList = DataManager.ContactToContactRole.SelectByContactRoleId(SiteId, ContactRole.ID).Select(a => a.ContactID).ToList();
                BindGridContacts();

                var roleTypeId = ContactRole.RoleTypeID;
                RadTabStrip1.SelectedIndex = roleTypeId;
                RadMultiPage1.SelectedIndex = roleTypeId == (int)ContactRoleType.WorkflowRole ? (int)ContactRoleType.ContactRole : roleTypeId;
                if (roleTypeId != (int)ContactRoleType.GeneralEmail)
                {
                    rblMethodAssigningResponsible.Items.FindByValue(ContactRole.MethodAssigningResponsible.ToString()).Selected = true;
                    if (ContactRole.SiteTagID.HasValue)
                    {
                        rblTags.SelectedIndex = rblTags.Items.IndexOf(rblTags.Items.FindByValue(ContactRole.SiteTagID.ToString()));
                    }
                    else
                    {
                        rblTargetContacts.SelectedIndex = rblTargetContacts.Items.IndexOf(rblTargetContacts.Items.FindByValue("1"));
                        pnlTags.Visible = false;
                        pnlSelectContacts.Visible = true;
                    }
                }
            }
            else if (DataItem is GridInsertionObject && !(GetPostBackControl(this.Page) is ImageButton) && ContactRole == null)
            {
                if (GetPostBackControl(this.Page) == null || GetPostBackControl(this.Page).ID != "btnInsert")
                {
                    SelectedContactList = null;
                    ((SelectContacts)FindControl("ucSelectContacts", Page.Controls)).SelectedItems = SelectedContactList;

                    gridContacts.Where = new List<GridWhere>();
                    gridContacts.Where.Add(new GridWhere { CustomQuery = " 1 = 0 " });
                }
            }

            rfvEmail.Visible = RadTabStrip1.SelectedTab.Value == "GeneralEmail";
            rfvTags.Visible = RadTabStrip1.SelectedTab.Value != "GeneralEmail";

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(((SelectContacts)FindControl("ucSelectContacts", Page.Controls)).SaveButton, gridContacts);

            ((SelectContacts)FindControl("ucSelectContacts", Page.Controls)).SelectedChanged += new SelectContacts.SelectedChangedEventHandler(ContactRoleEdit_SelectedChanged);
        }

        void ContactRoleEdit_SelectedChanged(object sender, SelectContacts.SelectedChangedEventArgs e)
        {
            SelectedContactList = e.ContactList;

            BindGridContacts();
            gridContacts.Rebind();
        }


        public static Control GetPostBackControl(System.Web.UI.Page page)
        {
            Control control = null;

            string ctrlname = page.Request.Params.Get("__EVENTTARGET");
            if (!string.IsNullOrEmpty(ctrlname))
            {
                control = page.FindControl(ctrlname);
            }
            else
            {
                foreach (string ctl in page.Request.Form)
                {
                    Control c = page.FindControl(ctl.Replace(".x", ""));
                    if (c is ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }
            return control;
        }


        /// <summary>
        /// Handles the OnClick event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            var contactRole = ContactRole != null
                                  ? DataManager.ContactRole.SelectById(SiteId, ContactRole.ID)
                                  : DataManager.ContactRole.SelectById(SiteId, ((RadButton) sender).CommandArgument.ToGuid());

            if (contactRole != null)
            {
                contactRole.Title = txtTitle.Text;
                contactRole.Description = txtDescription.Text;
                contactRole.RoleTypeID = RadTabStrip1.SelectedIndex;

                if (contactRole.RoleTypeID == (int)ContactRoleType.GeneralEmail)
                {
                    contactRole.Email = txtEmail.Text;
                    contactRole.DisplayName = txtDisplayName.Text;
                    contactRole.MethodAssigningResponsible = null;
                }
                else
                {
                    contactRole.Email = null;
                    contactRole.DisplayName = null;
                    contactRole.MethodAssigningResponsible = rblMethodAssigningResponsible.SelectedValue.ToInt();
                    if (rblTargetContacts.SelectedValue == "0")
                        contactRole.SiteTagID = rblTags.SelectedValue.ToGuid();
                    else
                        contactRole.SiteTagID = null;
                }

                DataManager.ContactRole.Update(contactRole);

                DataManager.ContactToContactRole.Save(SiteId,
                                                        rblTargetContacts.SelectedValue == "0"
                                                            ? new List<Guid>()
                                                            : SelectedContactList, contactRole.ID);
            }
        }


        /// <summary>
        /// Handles the OnClick event of the btnInsert control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnInsert_OnClick(object sender, EventArgs e)
        {
            var contactRole = new tbl_ContactRole
            {
                ID = Guid.NewGuid(),
                SiteID = SiteId,
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                RoleTypeID = RadTabStrip1.SelectedIndex
            };

            if (contactRole.RoleTypeID == (int)ContactRoleType.GeneralEmail)
            {
                contactRole.Email = txtEmail.Text;
                contactRole.DisplayName = txtDisplayName.Text;
                contactRole.MethodAssigningResponsible = null;
            }
            else
            {
                contactRole.MethodAssigningResponsible = rblMethodAssigningResponsible.SelectedValue.ToInt();
                if (rblTargetContacts.SelectedValue == "0")
                    contactRole.SiteTagID = rblTags.SelectedValue.ToGuid();
                else
                    contactRole.SiteTagID = null;
            }

            DataItem = contactRole;

            contactRole = DataManager.ContactRole.Add(contactRole);

            DataManager.ContactToContactRole.Save(SiteId,
                                                  rblTargetContacts.SelectedValue == "0"
                                                      ? new List<Guid>()
                                                      : SelectedContactList, contactRole.ID);
        }



        /// <summary>
        /// Handles the OnTabClick event of the RadTabStrip1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTabStripEventArgs"/> instance containing the event data.</param>
        protected void RadTabStrip1_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            rfvEmail.Visible = e.Tab.Value == "GeneralEmail";
            rfvTags.Visible = e.Tab.Value != "GeneralEmail";
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
        /// Handles the OnItemDataBound event of the gridContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridContacts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!(GetPostBackControl(this.Page) is ImageButton) || flag || (GetPostBackControl(this.Page) != null && GetPostBackControl(this.Page).ID == "EditButton"))
            {
                if (e.Item is GridDataItem)
                {
                    var item = (GridDataItem)e.Item;
                    var data = (DataRowView)e.Item.DataItem;

                    var ibtnDelete = (ImageButton)item.FindControl("ibtnDelete");
                    ibtnDelete.CommandArgument = data["ID"].ToString();
                }
            }
        }



        /// <summary>
        /// Binds the grid contacts.
        /// </summary>
        protected void BindGridContacts()
        {
            var selectedItem = new List<Guid>();
            selectedItem = SelectedContactList;
            if (selectedItem.Count > 0)
            {
                var query = new StringBuilder();
                foreach (var item in selectedItem)
                    query.AppendFormat("'{0}',", item);

                gridContacts.Where = new List<GridWhere>();
                gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_Contact.ID IN ({0})", query.ToString().TrimEnd(new[] { ',' })) });
            }
            else
            {
                gridContacts.Where = new List<GridWhere>();
                gridContacts.Where.Add(new GridWhere { CustomQuery = " 1 = 0 " });
            }

            ((SelectContacts)FindControl("ucSelectContacts", Page.Controls)).SelectedItems = SelectedContactList;
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



        protected void rbAdd_OnClick(object sender, EventArgs e)
        {
            ((SelectContacts)FindControl("ucSelectContacts", Page.Controls)).Show();
        }



        public static Control FindControl(string id, ControlCollection col)
        {
            foreach (Control c in col)
            {
                Control child = FindControlRecursive(c, id);
                if (child != null)
                    return child;
            }
            return null;
        }



        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control rc = FindControlRecursive(c, id);
                if (rc != null)
                    return rc;
            }
            return null;
        }
    }
}