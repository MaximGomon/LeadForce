using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class ProfileSites : LeadForceBasePage
    {
        protected RadAjaxManager radAjaxManager = null;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Профили сайтов - LeadForce";

            ucNotificationMessage.Text = string.Empty;
            dcbProduct.SiteID = CurrentUser.Instance.SiteID;

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

            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, ucMenuConstructor);
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridAccessProfileModule, ucMenuConstructor);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbAccessProfile, ucMenuConstructor);

            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, ucWidgetSettings);
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridAccessProfileModule, ucWidgetSettings);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbAccessProfile, ucWidgetSettings);

            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, TabStrip);
            
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbAccessProfile, plProfileInfo, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddProfile, plProfileInfo, null, UpdatePanelRenderMode.Inline);

            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, dcbModuleEdition, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, lbtnSelectModuleEdition, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, plOptions, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(dcbModuleEdition, plOptions, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSelectModuleEdition, gridAccessProfileModule, null, UpdatePanelRenderMode.Inline);

            radAjaxManager.AjaxRequest += radAjaxManager_AjaxRequest;
        }



        /// <summary>
        /// Binds the menu.
        /// </summary>
        protected void BindMenu()
        {
            if (!string.IsNullOrEmpty(rlbAccessProfile.SelectedValue))
            {
                ucMenuConstructor.AccessProfileID = Guid.Parse(rlbAccessProfile.SelectedValue);
                ucWidgetSettings.AccessProfileId = Guid.Parse(rlbAccessProfile.SelectedValue);
            }

            ucMenuConstructor.BindTreeModules();
            ucMenuConstructor.BindTreeMenu();
            ucMenuConstructor.BindToolbar();

            ucWidgetSettings.BindData();
        }



        /// <summary>
        /// Binds the access profile.
        /// </summary>
        /// <param name="accessProfileID">The access profile ID.</param>
        protected void BindAccessProfile(Guid? accessProfileID = null)
        {
            rlbAccessProfile.DataSource = DataManager.AccessProfile.SelectAll(null);
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
                gridAccessProfileModule.Where.Add(new GridWhere { Column = "AccessProfileID", Value = rlbAccessProfile.SelectedValue });

                TabStrip.Tabs[1].Visible = true;
                plProfileInfo.Visible = true;

                BindAccessProfileInfo();
            }
            else
            {                
                gridAccessProfileModule.Where = new List<GridWhere>();
                gridAccessProfileModule.Where.Add(new GridWhere { Column = "AccessProfileID", Value = Guid.Empty.ToString() });
                TabStrip.Tabs[1].Visible = false;
                plProfileInfo.Visible = false;
            }
        }



        /// <summary>
        /// Binds the access profile title.
        /// </summary>
        protected void BindAccessProfileInfo()
        {
            if (!string.IsNullOrEmpty(rlbAccessProfile.SelectedValue))
            {
                var accessProfile = DataManager.AccessProfile.SelectById(Guid.Parse(rlbAccessProfile.SelectedValue));
                if (accessProfile != null)
                {
                    txtProfileTitle.Text = accessProfile.Title;                    
                    rntxtDomainsCount.Value = accessProfile.DomainsCount;
                    txtContactsPageUrl.Text = accessProfile.ContactsPageUrl;
                    dcbProduct.SelectedIdNullable = accessProfile.ProductID;
                    rntxtActiveContactsCount.Value = accessProfile.ActiveContactsCount;
                    rntxtEmailPerContactCount.Value = accessProfile.EmailPerContactCount;
                    rntxtDurationPeriod.Value = accessProfile.DurationPeriod;
                }
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
            gridAccessProfileModule.Where.Add(new GridWhere { Column = "AccessProfileID", Value = rlbAccessProfile.SelectedValue });

            gridAccessProfileModule.Rebind();

            BindMenu();

            BindAccessProfileInfo();
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
                DataManager.AccessProfile.Add(new tbl_AccessProfile { ID = accessProfileID, Title = txtAccessProfileTitle.Text });

                txtAccessProfileTitle.Text = string.Empty;

                BindAccessProfile(accessProfileID);
                gridAccessProfileModule.Rebind();

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
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((CheckBox)item.FindControl("cbRead")).Checked = bool.Parse(data["tbl_AccessProfileModule_Read"].ToString());
                ((CheckBox)item.FindControl("cbWrite")).Checked = bool.Parse(data["tbl_AccessProfileModule_Write"].ToString());
                ((CheckBox)item.FindControl("cbDelete")).Checked = bool.Parse(data["tbl_AccessProfileModule_Delete"].ToString());
                ((HyperLink)item.FindControl("hlSelectEdition")).Attributes.Add("onclick", string.Format("InitModuleEditionRadWindow('{0}')", data["ID"]));

                if (!string.IsNullOrEmpty(data["tbl_ModuleEdition_Title"].ToString()))
                    ((HyperLink)item.FindControl("hlSelectEdition")).Text = data["tbl_ModuleEdition_Title"].ToString();
            }
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
                    accessProfile.DomainsCount = (int?)rntxtDomainsCount.Value;
                    accessProfile.ContactsPageUrl = txtContactsPageUrl.Text;
                    accessProfile.ProductID = dcbProduct.SelectedIdNullable;

                    accessProfile.ActiveContactsCount = (int?)rntxtActiveContactsCount.Value;
                    accessProfile.EmailPerContactCount = (int?)rntxtEmailPerContactCount.Value;
                    accessProfile.DurationPeriod = (int?)rntxtDurationPeriod.Value;

                    DataManager.AccessProfile.Update(accessProfile);

                    BindAccessProfile(accessProfile.ID);
                    gridAccessProfileModule.Rebind();

                    BindMenu();
                }
            }

            ucNotificationMessage.Text = "Профиль успешно обновлен";
        }

        #region Редакция модуля

        /// <summary>
        /// Handles the AjaxRequest event of the radAjaxManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void radAjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            var accessProfileModuleId = Guid.Empty;

            if (!Guid.TryParse(e.Argument, out  accessProfileModuleId))
                return;

            var accessProfileModule = DataManager.AccessProfileModule.SelectById(accessProfileModuleId);

            dcbModuleEdition.Filters.Clear();
            dcbModuleEdition.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn()
                                             {
                                                 Name = "ModuleID",
                                                 DbType = DbType.Guid,
                                                 Value = accessProfileModule.ModuleID.ToString()
                                             });
            dcbModuleEdition.SelectedIdNullable = accessProfileModule.ModuleEditionID;
            dcbModuleEdition.BindData();
            lbtnSelectModuleEdition.CommandArgument = e.Argument;                        
            
            BindOptions(accessProfileModule.ModuleEditionID, false);

            foreach (var moduleEditionOption in accessProfileModule.tbl_ModuleEditionOption)
            {
                chxOptions.Items.FindByValue(moduleEditionOption.ID.ToString()).Selected = true;
            }

            plOptions.Visible = chxOptions.Items.Count > 0;

            if (!Page.ClientScript.IsStartupScriptRegistered("ShowModuleEditionRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "ShowModuleEditionRadWindow", "ShowModuleEditionRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbModuleEdition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbModuleEdition_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindOptions(dcbModuleEdition.SelectedIdNullable);            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSelectModuleEdition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSelectModuleEdition_OnClick(object sender, EventArgs e)
        {
            var accessProfileModule = DataManager.AccessProfileModule.SelectById(Guid.Parse(lbtnSelectModuleEdition.CommandArgument));
            accessProfileModule.ModuleEditionID = dcbModuleEdition.SelectedIdNullable;            
            accessProfileModule.tbl_ModuleEditionOption.Clear();

            foreach (ListItem listItem in chxOptions.Items)
            {
                if (listItem.Selected)
                    accessProfileModule.tbl_ModuleEditionOption.Add(DataManager.ModuleEditionOption.SelectById(Guid.Parse(listItem.Value)));
            }

            DataManager.AccessProfileModule.Update(accessProfileModule);

            gridAccessProfileModule.Rebind();

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseModuleEditionRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "CloseModuleEditionRadWindow", "CloseModuleEditionRadWindow();", true);
        }



        /// <summary>
        /// Binds the options.
        /// </summary>
        /// <param name="moduleEditionId">The module edition id.</param>
        /// <param name="selected">if set to <c>true</c> [selected].</param>
        protected void BindOptions(Guid? moduleEditionId, bool selected = true)
        {
            chxOptions.Items.Clear();

            if (moduleEditionId.HasValue)
            {
                var options = DataManager.ModuleEditionOption.SelectByModuleEditionId(moduleEditionId.Value);

                chxOptions.DataSource = options;
                chxOptions.DataTextField = "Title";
                chxOptions.DataValueField = "ID";
                chxOptions.DataBind();

                foreach (ListItem listItem in chxOptions.Items)
                {
                    listItem.Selected = selected;
                }

                plOptions.Visible = options.Any();
            }
            else
                plOptions.Visible = false;
        }

        #endregion
    }
}