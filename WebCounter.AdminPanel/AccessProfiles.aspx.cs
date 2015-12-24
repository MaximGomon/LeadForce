using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class AccessProfiles : LeadForceBasePage
    {
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



        protected RadAjaxManager radAjaxManager = null;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Профили доступа - LeadForce";

            editRecordTooltip.TargetControls.Clear();
            editRecordTooltip.TargetControls.Add(btnAddAccessProfileRecord.ClientID, Guid.Empty.ToString(), true);

            if (!Page.IsPostBack)
            {
                btnAddProfile.Icon.PrimaryIconCssClass = "rbAdd";
                btnAddProfile.Icon.PrimaryIconLeft = 12;

                BindAccessProfile();
                BindMenu();
            }


            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, gridAccessProfileModule);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbAccessProfile, gridAccessProfileModule);
            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, gridAccessProfileRecord);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbAccessProfile, gridAccessProfileRecord);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbAccessProfile, txtProfileTitle, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, txtProfileTitle, null, UpdatePanelRenderMode.Inline);

            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, gridAccessProfileRecord, null);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, editRecordTooltip, null);
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridAccessProfileRecord, editRecordTooltip, null);
            radAjaxManager.AjaxRequest += AccessProfileRecord_AjaxRequest;

            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, ucMenuConstructor);
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridAccessProfileModule, ucMenuConstructor);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbAccessProfile, ucMenuConstructor);

            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, TabStrip);
        }



        /// <summary>
        /// Binds the menu.
        /// </summary>
        protected void BindMenu()
        {
            if (!string.IsNullOrEmpty(rlbAccessProfile.SelectedValue))
                ucMenuConstructor.AccessProfileID = Guid.Parse(rlbAccessProfile.SelectedValue);
            ucMenuConstructor.BindTreeModules();
            ucMenuConstructor.BindTreeMenu();
            ucMenuConstructor.BindToolbar();
        }



        /// <summary>
        /// Handles the AjaxRequest event of the AccessProfileRecord control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void AccessProfileRecord_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                gridAccessProfileRecord.Where = new List<GridWhere>();
                gridAccessProfileRecord.Where.Add(new GridWhere { Column = "AccessProfileID", Value = rlbAccessProfile.SelectedValue });
                gridAccessProfileRecord.Rebind();
            }
        }




        /// <summary>
        /// Called when [ajax update].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Telerik.Web.UI.ToolTipUpdateEventArgs"/> instance containing the event data.</param>
        protected void OnAjaxUpdate(object sender, ToolTipUpdateEventArgs args)
        {
            UpdateToolTip(args.Value, args.UpdatePanel);
        }




        /// <summary>
        /// Updates the tool tip.
        /// </summary>
        /// <param name="elementId">The element id.</param>
        /// <param name="panel">The panel.</param>
        private void UpdateToolTip(string elementId, UpdatePanel panel)
        {
            Control ctrl = Page.LoadControl("~/UserControls/EditAccessModuleRecord.ascx");
            panel.ContentTemplateContainer.Controls.Add(ctrl);
            var editAccessModuleRecord = (EditAccessModuleRecord)ctrl;
            editAccessModuleRecord.RecordAccessId = Guid.Parse(elementId);
            editAccessModuleRecord.CurrentAccessProfile = CurrentAccessProfile;
            editAccessModuleRecord.BindData();
        }





        /// <summary>
        /// Binds the access profile.
        /// </summary>
        protected void BindAccessProfile(Guid? accessProfileID = null)
        {
            rlbAccessProfile.DataSource = DataManager.AccessProfile.SelectAll(SiteId);
            rlbAccessProfile.DataValueField = "ID";
            rlbAccessProfile.DataTextField = "Title";
            rlbAccessProfile.DataBind();

            if (rlbAccessProfile.Items.Count > 0)
            {
                if (accessProfileID != null)
                    rlbAccessProfile.FindItemByValue(accessProfileID.ToString()).Selected = true;

                if (rlbAccessProfile.SelectedIndex == -1)
                    rlbAccessProfile.SelectedIndex = 0;

                gridAccessProfileModule.Where = new List<GridWhere>();
                gridAccessProfileRecord.Where = new List<GridWhere>();
                gridAccessProfileModule.Where.Add(new GridWhere { Column = "AccessProfileID", Value = rlbAccessProfile.SelectedValue });
                gridAccessProfileRecord.Where.Add(new GridWhere { Column = "AccessProfileID", Value = rlbAccessProfile.SelectedValue });

                CurrentAccessProfile = Guid.Parse(rlbAccessProfile.SelectedValue);

                TabStrip.Tabs[1].Visible = true;
                TabStrip.Tabs[2].Visible = true;

                BindAccessProfileTitle();
            }
            else
            {
                gridAccessProfileModule.Where = new List<GridWhere>();
                gridAccessProfileRecord.Where = new List<GridWhere>();
                gridAccessProfileModule.Where.Add(new GridWhere { Column = "AccessProfileID", Value = Guid.Empty.ToString() });
                gridAccessProfileRecord.Where.Add(new GridWhere { Column = "AccessProfileID", Value = Guid.Empty.ToString() });

                CurrentAccessProfile = Guid.Empty;

                TabStrip.Tabs[1].Visible = false;
                TabStrip.Tabs[2].Visible = false;

                plProfileTitle.Visible = false;
            }
        }





        /// <summary>
        /// Binds the access profile title.
        /// </summary>
        protected void BindAccessProfileTitle()
        {
            if (!string.IsNullOrEmpty(rlbAccessProfile.SelectedValue))
            {
                var accessProfile = DataManager.AccessProfile.SelectById(Guid.Parse(rlbAccessProfile.SelectedValue));
                if (accessProfile != null)
                    txtProfileTitle.Text = accessProfile.Title;
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rlbAccessProfile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rlbAccessProfile_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            gridAccessProfileModule.Where = new List<GridWhere>();
            gridAccessProfileRecord.Where = new List<GridWhere>();
            gridAccessProfileModule.Where.Add(new GridWhere { Column = "AccessProfileID", Value = rlbAccessProfile.SelectedValue });
            gridAccessProfileRecord.Where.Add(new GridWhere { Column = "AccessProfileID", Value = rlbAccessProfile.SelectedValue });

            CurrentAccessProfile = Guid.Parse(rlbAccessProfile.SelectedValue);

            gridAccessProfileModule.Rebind();
            gridAccessProfileRecord.Rebind();

            BindMenu();

            BindAccessProfileTitle();
        }



        /// <summary>
        /// Handles the OnClick event of the btnAddProfile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAddProfile_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccessProfileTitle.Text.Trim()))
            {
                var accessProfileID = Guid.NewGuid();
                DataManager.AccessProfile.Add(new tbl_AccessProfile { ID = accessProfileID, SiteID = SiteId, Title = txtAccessProfileTitle.Text });

                txtAccessProfileTitle.Text = string.Empty;

                BindAccessProfile(accessProfileID);
                gridAccessProfileModule.Rebind();
                gridAccessProfileRecord.Rebind();

                BindMenu();
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridAccessProfileModule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridAccessProfileModule_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;

                Guid? siteID = Guid.Parse(data["tbl_AccessProfile_SiteID"].ToString());
                if (siteID != Guid.Empty)
                {
                    var site = DataManager.Sites.SelectById((Guid)siteID);
                    if (site.AccessProfileID != null)
                    {
                        var accessProfileModules = DataManager.AccessProfileModule.SelectByAccessProfileID((Guid)site.AccessProfileID);
                        if (accessProfileModules != null && accessProfileModules.Count > 0)
                        {
                            var moduleID = Guid.Parse(data["tbl_AccessProfileModule_ModuleID"].ToString());
                            var siteAccessProfileModules = accessProfileModules.Where(a => a.ModuleID == moduleID).SingleOrDefault();
                            if (siteAccessProfileModules != null)
                            {
                                if (!siteAccessProfileModules.Read)
                                    item.Visible = false;

                                ((CheckBox)item.FindControl("cbRead")).Checked = bool.Parse(data["tbl_AccessProfileModule_Read"].ToString());
                                ((CheckBox)item.FindControl("cbWrite")).Checked = bool.Parse(data["tbl_AccessProfileModule_Write"].ToString());
                                ((CheckBox)item.FindControl("cbDelete")).Checked = bool.Parse(data["tbl_AccessProfileModule_Delete"].ToString());

                                if (!siteAccessProfileModules.Write)
                                {
                                    ((CheckBox)item.FindControl("cbWrite")).Checked = false;
                                    ((CheckBox)item.FindControl("cbWrite")).Enabled = false;
                                }

                                if (!siteAccessProfileModules.Delete)
                                {
                                    ((CheckBox)item.FindControl("cbDelete")).Checked = false;
                                    ((CheckBox)item.FindControl("cbDelete")).Enabled = false;
                                }

                                return;
                            }
                        }
                    }
                }

                ((CheckBox)item.FindControl("cbRead")).Checked = bool.Parse(data["tbl_AccessProfileModule_Read"].ToString());
                ((CheckBox)item.FindControl("cbWrite")).Checked = bool.Parse(data["tbl_AccessProfileModule_Write"].ToString());
                ((CheckBox)item.FindControl("cbDelete")).Checked = bool.Parse(data["tbl_AccessProfileModule_Delete"].ToString());   
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridAccessProfileRecord control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridAccessProfileRecord_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("litCompanyRule")).Text = EnumHelper.GetEnumDescription((AccessProfileRecordRule)int.Parse(data["tbl_AccessProfileRecord_CompanyRuleID"].ToString()));
                ((Literal)item.FindControl("litOwnerRule")).Text = EnumHelper.GetEnumDescription((AccessProfileRecordRule)int.Parse(data["tbl_AccessProfileRecord_OwnerRuleID"].ToString()));

                if (data["tbl_AccessProfileRecord_CompanyID"] != null && !string.IsNullOrEmpty(data["tbl_AccessProfileRecord_CompanyID"].ToString()))
                {
                    var company = DataManager.Company.SelectById(SiteId, Guid.Parse(data["tbl_AccessProfileRecord_CompanyID"].ToString()));
                    if (company != null)
                        ((Literal)item.FindControl("litCompany")).Text = company.Name;
                }

                if (data["tbl_AccessProfileRecord_OwnerID"] != null && !string.IsNullOrEmpty(data["tbl_AccessProfileRecord_OwnerID"].ToString()))
                {
                    var contact = DataManager.Contact.SelectById(SiteId, Guid.Parse(data["tbl_AccessProfileRecord_OwnerID"].ToString()));
                    if (contact != null)
                        ((Literal)item.FindControl("litOwner")).Text = contact.UserFullName;
                }

                var target = (LinkButton)e.Item.FindControl("lbEdit");
                editRecordTooltip.TargetControls.Add(target.ClientID, data["ID"].ToString(), true);

                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();
                lbDelete.Command += new CommandEventHandler(lbDelete_OnCommand);
            }
        }




        /// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            DataManager.AccessProfileRecord.Delete(Guid.Parse(e.CommandArgument.ToString()));

            gridAccessProfileRecord.Where = new List<GridWhere>();
            gridAccessProfileRecord.Where.Add(new GridWhere { Column = "AccessProfileID", Value = rlbAccessProfile.SelectedValue });

            gridAccessProfileRecord.Rebind();
        }




        /// <summary>
        /// Handles the OnCheckedChanged event of the cbRights control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void cbRights_OnCheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var dataItem = ((GridDataItem)checkbox.Parent.Parent);
            var id = Guid.Parse(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID"].ToString());

            var cbRead = dataItem.FindControl("cbRead") as CheckBox;
            var cbWrite = dataItem.FindControl("cbWrite") as CheckBox;
            var cbDelete = dataItem.FindControl("cbDelete") as CheckBox;

            var accessProfileModule = DataManager.AccessProfileModule.SelectById(id);
            accessProfileModule.Read = cbRead.Checked;
            accessProfileModule.Write = cbWrite.Checked;
            accessProfileModule.Delete = cbDelete.Checked;

            DataManager.AccessProfileModule.Update(accessProfileModule);

            BindMenu();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rlbAccessProfile.SelectedValue))
            {
                var accessProfile = DataManager.AccessProfile.SelectById(Guid.Parse(rlbAccessProfile.SelectedValue));
                if (accessProfile != null)
                {
                    accessProfile.Title = txtProfileTitle.Text;
                    DataManager.AccessProfile.Update(accessProfile);

                    BindAccessProfile(accessProfile.ID);
                    gridAccessProfileModule.Rebind();

                    BindMenu();
                }
            }
        }
    }
}