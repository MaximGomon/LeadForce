using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class EditAccessModuleRecord : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid RecordAccessId
        {
            get
            {
                var o = ViewState["RecordAccessId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["RecordAccessId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid CurrentAccessProfile
        {
            get
            {
                var o = ViewState["CurrentAccessProfile"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["CurrentAccessProfile"] = value;
            }
        }


        readonly DataManager _dataManager = new DataManager();


        protected void Page_Load(object sender, EventArgs e)
        {
            /*var radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lnkSave, FindControl("gridAccessProfileRecord", Page.Controls));*/
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            BindCompanyRules();
            BindOwnerRules();
            BindOwner();
            BindCompany();
            BindModules();
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ddlOwnerRules.Attributes.Add("onchange", string.Format("EnableRadCombobox(this, '{0}')", rcbOwner.ClientID));
            ddlCompanyRules.Attributes.Add("onchange", string.Format("EnableRadCombobox(this, '{0}')", rcbCompany.ClientID));

            this.DataBind();

            if (RecordAccessId != Guid.Empty)
            {
                var accessProfileRecord = _dataManager.AccessProfileRecord.SelectById(RecordAccessId);
                ddlModules.SelectedIndex =
                    ddlModules.Items.IndexOf(ddlModules.Items.FindByValue(accessProfileRecord.ModuleID.ToString()));

                ddlCompanyRules.SelectedIndex =
                    ddlCompanyRules.Items.IndexOf(ddlCompanyRules.Items.FindByValue(accessProfileRecord.CompanyRuleID.ToString()));

                if (accessProfileRecord.CompanyID != null)
                    rcbCompany.SelectedIndex =
                        rcbCompany.Items.FindItemIndexByValue(accessProfileRecord.CompanyID.ToString());

                rcbCompany.Enabled = accessProfileRecord.CompanyRuleID == 2;

                ddlOwnerRules.SelectedIndex =
                    ddlOwnerRules.Items.IndexOf(ddlOwnerRules.Items.FindByValue(accessProfileRecord.OwnerRuleID.ToString()));

                rcbOwner.Enabled = accessProfileRecord.OwnerRuleID == 2;

                if (accessProfileRecord.OwnerID != null)
                    rcbOwner.SelectedIndex = rcbOwner.Items.FindItemIndexByValue(accessProfileRecord.OwnerID.ToString());
                if (accessProfileRecord.OwnerRuleID != 2)
                    rcbOwner.Enabled = false;

                chxRead.Checked = accessProfileRecord.Read;
                chxWrite.Checked = accessProfileRecord.Write;
                chxDelete.Checked = accessProfileRecord.Delete;
            }
            else
            {
                ddlCompanyRules.SelectedIndex = -1;
                ddlOwnerRules.SelectedIndex = -1;
                ddlModules.SelectedIndex = -1;
                chxRead.Checked = false;
                chxWrite.Checked = false;
                chxDelete.Checked = false;
                rcbCompany.Enabled = false;
                rcbOwner.Enabled = false;
            }


            if (!Page.ClientScript.IsClientScriptBlockRegistered("editRecordScripts"))
            {
                const string script =
                                    @"function EnableRadCombobox(element, id) {
                                        var combo = $find(id); " +
                                        @"if ($(element).val() == ""2"") combo.enable();
                                          else { combo.set_text(''); combo.set_value(''); combo.disable(); }
                                    }
                                    function SelectItem(id, value) {                                        
                                        var combo = $find(id);
                                        if (combo != null) { combo.clearCache(); combo.enable(); combo.findItemByValue(value).select();}
                                    }";

                ScriptManager.RegisterClientScriptBlock(this, typeof(EditAccessModuleRecord), "editRecordScripts", script, true);
            }
        }



        /// <summary>
        /// Handles the Click event of the lnkSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            Guid? companyId = null;
            Guid? ownerId = null;

            if (ddlCompanyRules.SelectedValue == "2")
                companyId = Guid.Parse(rcbCompany.SelectedValue);
            if (ddlOwnerRules.SelectedValue == "2")
                ownerId = Guid.Parse(rcbOwner.SelectedValue);

            var accessProfileRecord = _dataManager.AccessProfileRecord.SelectById(RecordAccessId) ?? new tbl_AccessProfileRecord();
            accessProfileRecord.AccessProfileID = CurrentAccessProfile;
            accessProfileRecord.ModuleID = Guid.Parse(ddlModules.SelectedValue);
            accessProfileRecord.CompanyRuleID = byte.Parse(ddlCompanyRules.SelectedValue);
            accessProfileRecord.CompanyID = companyId;
            accessProfileRecord.OwnerRuleID = byte.Parse(ddlOwnerRules.SelectedValue);
            accessProfileRecord.OwnerID = ownerId;
            accessProfileRecord.Read = chxRead.Checked;
            accessProfileRecord.Write = chxWrite.Checked;
            accessProfileRecord.Delete = chxDelete.Checked;

            if (accessProfileRecord.ID == Guid.Empty)
                _dataManager.AccessProfileRecord.Add(accessProfileRecord);
            else
                _dataManager.AccessProfileRecord.Update(accessProfileRecord);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "editRecordSave", "CloseTooltip();", true);

            /*var gridAccessProfileRecord = (Grid)FindControl("gridAccessProfileRecord", Page.Controls);
            gridAccessProfileRecord.AdditionalFields = new List<AdditionalField>();
            gridAccessProfileRecord.AdditionalFields.Add(new AdditionalField { Name = "AccessProfileID", Value = CurrentAccessProfile.ToString() });
            gridAccessProfileRecord.Rebind();*/
        }



        /// <summary>
        /// Binds the owner.
        /// </summary>
        private void BindOwner()
        {
            rcbOwner.DataSource = _dataManager.Contact.SelectAll(CurrentUser.Instance.SiteID).Where(a => !string.IsNullOrEmpty(a.UserFullName));
            rcbOwner.DataValueField = "ID";
            rcbOwner.DataTextField = "UserFullName";
            rcbOwner.DataBind();
            rcbOwner.EmptyMessage = "Выберите ответственного";
        }




        /// <summary>
        /// Binds the company rules.
        /// </summary>
        private void BindCompanyRules()
        {
            ddlCompanyRules.Items.Clear();
            foreach (var accessProfileRecordRule in EnumHelper.EnumToList<AccessProfileRecordRule>())
                ddlCompanyRules.Items.Add(new ListItem(EnumHelper.GetEnumDescription(accessProfileRecordRule), ((int)accessProfileRecordRule).ToString()));
        }



        /// <summary>
        /// Binds the owner rules.
        /// </summary>
        private void BindOwnerRules()
        {
            ddlOwnerRules.Items.Clear();
            foreach (var accessProfileRecordRule in EnumHelper.EnumToList<AccessProfileRecordRule>())
                ddlOwnerRules.Items.Add(new ListItem(EnumHelper.GetEnumDescription(accessProfileRecordRule), ((int)accessProfileRecordRule).ToString()));
        }



        /// <summary>
        /// Binds the modules.
        /// </summary>
        private void BindModules()
        {
            var modulesFiltered = new List<tbl_Module>();

            var accessProfile = _dataManager.AccessProfile.SelectById(CurrentAccessProfile);
            List<tbl_AccessProfileModule> siteAccessProfileModules = null;

            Guid? siteID = accessProfile.SiteID;
            if (siteID != Guid.Empty)
            {
                var site = _dataManager.Sites.SelectById((Guid)siteID);
                if (site.AccessProfileID != null)
                    siteAccessProfileModules = _dataManager.AccessProfileModule.SelectByAccessProfileID((Guid)site.AccessProfileID).Where(a => a.Read).ToList();
            }

            var accessProfileModules = _dataManager.AccessProfileModule.SelectByAccessProfileID(CurrentAccessProfile);
            foreach (var accessProfileModule in accessProfileModules)
            {
                if (siteAccessProfileModules != null)
                {
                    if (accessProfileModule.Read && siteAccessProfileModules.SingleOrDefault(a => a.ModuleID == accessProfileModule.ModuleID).Read)
                        modulesFiltered.Add(accessProfileModule.tbl_Module);
                }
                else
                {
                    if (accessProfileModule.Read)
                        modulesFiltered.Add(accessProfileModule.tbl_Module);
                }
            }


            ddlModules.DataSource = modulesFiltered;
            ddlModules.DataValueField = "ID";
            ddlModules.DataTextField = "Title";
            ddlModules.DataBind();
        }



        /// <summary>
        /// Binds the company.
        /// </summary>
        protected void BindCompany()
        {
            rcbCompany.DataSource = _dataManager.Company.SelectAll(CurrentUser.Instance.SiteID);
            rcbCompany.DataValueField = "ID";
            rcbCompany.DataTextField = "Name";
            rcbCompany.DataBind();
            rcbCompany.Text = string.Empty;
            rcbCompany.EmptyMessage = "Выберите компанию";
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