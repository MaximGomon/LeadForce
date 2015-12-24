using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard
{
    public partial class GeneralInformation : WorkflowTemplateWizardStep
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();

            EnumHelper.EnumToDropDownList<WorkflowTemplateStatus>(ref ddlStatus, false);

            tbl_WorkflowTemplate workflowTemplate = null;

            if (!IsEditMode)
                workflowTemplate = DataManager.WorkflowTemplate.SelectById(CurrentWorkflowTemplate);
            else if (EditObjectId.HasValue)
                workflowTemplate = DataManager.WorkflowTemplate.SelectById(EditObjectId.Value);
            else
                return;

            if (workflowTemplate == null)
                return;

            txtName.Text = workflowTemplate.Name;
            ddlStatus.Items.FindByValue(workflowTemplate.Status.ToString()).Selected = true;
        }



        protected List<Role> GetContactRoles()
        {
            Guid workflowTemplateId = Guid.Empty;

            if (!IsEditMode)
                workflowTemplateId = CurrentWorkflowTemplate;
            else if (EditObjectId.HasValue)
                workflowTemplateId = EditObjectId.Value;

            var roles = new List<Role>();

            var workflowTemplateElements =
                DataManager.WorkflowTemplateElement.SelectAll(workflowTemplateId).Where(
                    a => a.ElementType == (int) WorkflowTemplateElementType.Message);

            foreach (var workflowTemplateElement in workflowTemplateElements)
            {
                var role = new Role();
                var siteActionTemplateId = DataManager.WorkflowTemplateElementParameter.SelectByElementId(workflowTemplateElement.ID).FirstOrDefault(a => a.Name == "SiteActionTemplateID").Value;
                var actionTemplate = DataManager.SiteActionTemplate.SelectById(siteActionTemplateId.ToGuid());
                tbl_ContactRole contactRole = null;

                if (actionTemplate.FromContactRoleID != null)
                {
                    if (roles.Count(a => a.ContactRoleID == actionTemplate.FromContactRoleID) == 0)
                    {
                        role = new Role();
                        role.ContactRoleID = (Guid)actionTemplate.FromContactRoleID;
                        contactRole = DataManager.ContactRole.SelectById((Guid)actionTemplate.FromContactRoleID);
                        role.RoleInTemplate = contactRole.Title;
                        role.Description = contactRole.Description;
                        roles.Add(role);
                    }
                }
                else
                {
                    if (roles.Count(a => a.Email == actionTemplate.FromEmail) == 0)
                    {
                        role = new Role();
                        role.Email = actionTemplate.FromEmail;
                        role.DisplayName = actionTemplate.FromName;
                        role.RoleInTemplate = actionTemplate.FromEmail;
                        roles.Add(role);
                    }
                }

                var actionTemplateRecipients = DataManager.SiteActionTemplateRecipient.SelectAll(actionTemplate.ID);
                foreach (var actionTemplateRecipient in actionTemplateRecipients)
                {
                    if (actionTemplateRecipient.ContactRoleID != null)
                    {
                        if (roles.Count(a => a.ContactRoleID == actionTemplateRecipient.ContactRoleID) == 0)
                        {
                            role = new Role();
                            role.ContactRoleID = (Guid)actionTemplateRecipient.ContactRoleID;
                            contactRole = DataManager.ContactRole.SelectById((Guid)actionTemplateRecipient.ContactRoleID);
                            role.RoleInTemplate = contactRole.Title;
                            role.Description = contactRole.Description;
                            roles.Add(role);
                        }
                    }
                    else if (actionTemplateRecipient.ContactID != null)
                    {
                        //var contact = DataManager.Contact.SelectById()
                    }
                    else
                    {
                        if (roles.Count(a => a.Email == actionTemplateRecipient.Email) == 0)
                        {
                            role = new Role();
                            role.Email = actionTemplateRecipient.Email;
                            role.DisplayName = actionTemplateRecipient.DisplayName;
                            role.RoleInTemplate = actionTemplateRecipient.Email;
                            roles.Add(role);
                        }
                    }
                }
            }

            return roles;
        }



        /// <summary>
        /// Handles the OnNeedDataSource event of the rgContactRoles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgContactRoles_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgContactRoles.DataSource = GetContactRoles();
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgContactRoles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgContactRoles_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                //var data = (tbl_WorkflowTemplateElement)e.Item.DataItem;
                var data = (Role)e.Item.DataItem;

                var dcbContactRole = ((DictionaryOnDemandComboBox)e.Item.FindControl("dcbContactRole"));
                dcbContactRole.SiteID = CurrentUser.Instance.SiteID;
                dcbContactRole.BindData();

                if (IsEditMode)
                {
                    if (data.ContactRoleID != Guid.Empty)
                    {
                        var role = DataManager.ContactRole.SelectById(data.ContactRoleID);
                        dcbContactRole.SelectedId = data.ContactRoleID;
                        dcbContactRole.SelectedText = role.Title;
                        e.Item.FindControl("pnlFrom").Visible = false;
                    }
                    else
                    {
                        ((TextBox)e.Item.FindControl("txtFromEmail")).Text = data.RoleInTemplate;
                        ((TextBox)e.Item.FindControl("txtFromName")).Text = data.DisplayName;
                    }
                    rgContactRoles.Columns[0].Display = false;
                    rgContactRoles.Columns[1].Display = false;
                }


                dcbContactRole.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(dcbContactRole_OnSelectedIndexChanged);
            }
        }



        /// <summary>
        /// Handles the ItemsRequested event of the SettingsSiteActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs"/> instance containing the event data.</param>
        protected void dcbContactRole_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ((RadComboBox)((DictionaryOnDemandComboBox)sender).FindControl("rcbDictionary")).Items.Insert(0, new RadComboBoxItem("Конкретные Email и имя"));
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbContactRole control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbContactRole_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var gridTableCell = FindControlParent((DictionaryOnDemandComboBox)sender, typeof(GridTableCell)) as GridTableCell;
            gridTableCell.FindControl("pnlFrom").Visible = string.IsNullOrEmpty(e.Value);
        }


        private static Control FindControlParent(Control control, Type type)
        {
            Control ctrlParent = control;
            while ((ctrlParent = ctrlParent.Parent) != null)
            {
                if (ctrlParent.GetType() == type)
                {
                    return ctrlParent;
                }
            }
            return null;
        }
    }

    public class Role
    {
        public string RoleInTemplate { get; set; }
        public string DisplayName { get; set; }
        public Guid ContactRoleID { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }
}