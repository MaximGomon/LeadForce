using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class WorkflowTemplateParameter : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        private tbl_User _currentUser;



        public List<WorkflowTemplateParameterMap> WorkflowTemplateParameterList
        {
            get
            {
                if (ViewState["WorkflowTemplateParameters"] == null)
                    ViewState["WorkflowTemplateParameters"] = new List<WorkflowTemplateParameterMap>();
                return (List<WorkflowTemplateParameterMap>)ViewState["WorkflowTemplateParameters"];
            }
        }



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid WorkflowTemplateId
        {
            get
            {
                object o = ViewState["WorkflowTemplateId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["WorkflowTemplateId"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;
            _currentUser = _dataManager.User.SelectById(CurrentUser.Instance.ID);

            rgWorkflowTemplateParameter.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if (WorkflowTemplateId != Guid.Empty)
            {
                ViewState["WorkflowTemplateParameters"] =
                    _dataManager.WorkflowTemplateParameter.SelectAll(WorkflowTemplateId).Select(
                        a =>
                        new WorkflowTemplateParameterMap
                            {
                                ID = a.ID,
                                WorkflowTemplateID = a.WorkflowTemplateID,
                                Name = a.Name,
                                ModuleID = a.ModuleID,
                                IsSystem = a.IsSystem,
                                Description = a.Description
                            }).ToList();
            }
            else
                ViewState["WorkflowTemplateParameters"] = new List<WorkflowTemplateParameterMap>();

            rgWorkflowTemplateParameter.Rebind();
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgWorkflowTemplateParameter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateParameter_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkflowTemplateParameter.DataSource = ViewState["WorkflowTemplateParameters"];
        }



        /// <summary>
        /// Handles the OnItemCreated event of the rgWorkflowTemplateParameter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateParameter_OnItemCreated(object sender, GridItemEventArgs e)
        {
            //to show/hide AddNewRecord button
            if (e.Item is GridCommandItem)
                e.Item.FindControl("InitInsertButton").Parent.Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
        }



        /// <summary>
        /// Handles the ItemDataBound event of the rgWorkflowTemplateParameter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateParameter_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (WorkflowTemplateParameterMap)e.Item.DataItem;

                if (data.IsSystem)
                {
                    item["EditColumn"].Controls[0].Visible = false;
                    item["DeleteColumn"].Controls[0].Visible = false;
                }

                rgWorkflowTemplateParameter.Columns.FindByUniqueName("DeleteColumn").Display = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                var ddlModule = (DropDownList)gridEditFormItem.FindControl("ddlModule");

                var modules = _dataManager.Module.SelectAll();
                ddlModule.Items.Add(new ListItem("Выберите значение", ""));
                foreach (var module in modules)
                {
                    if (Access.Check(_currentUser, module.Name).Read)
                        ddlModule.Items.Add(new ListItem(module.Title, module.ID.ToString()));
                }

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var parameter = (WorkflowTemplateParameterMap)gridEditFormItem.DataItem;
                    ((TextBox)gridEditFormItem.FindControl("txtName")).Text = parameter.Name;
                    ((DropDownList)gridEditFormItem.FindControl("ddlModule")).Items.FindByValue(parameter.ModuleID != null ? parameter.ModuleID.ToString() : "").Selected = true;
                }

                gridEditFormItem.FindControl("lbtnSave").Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
            }
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgWorkflowTemplateParameter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateParameter_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgWorkflowTemplateParameter.MasterTableView.IsItemInserted = false;
            rgWorkflowTemplateParameter.Rebind();
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgWorkflowTemplateParameter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateParameter_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgWorkflowTemplateParameter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateParameter_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = (e.Item as GridDataItem).GetDataKeyValue("ID").ToString().ToGuid();
            ((List<WorkflowTemplateParameterMap>)ViewState["WorkflowTemplateParameters"]).Remove(((List<WorkflowTemplateParameterMap>)ViewState["WorkflowTemplateParameters"]).Where(s => s.ID == id).FirstOrDefault());
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="workflowTemplateParameterId">The workflow template parameter id.</param>
        /// <param name="item">The item.</param>
        protected void SaveToViewState(Guid workflowTemplateParameterId, GridEditableItem item)
        {
            var workflowTemplateParameter = ((List<WorkflowTemplateParameterMap>)ViewState["WorkflowTemplateParameters"]).Where(s => s.ID == workflowTemplateParameterId).FirstOrDefault() ?? new WorkflowTemplateParameterMap();

            workflowTemplateParameter.Name = ((TextBox)item.FindControl("txtName")).Text;
            workflowTemplateParameter.ModuleID = !string.IsNullOrEmpty(((DropDownList)item.FindControl("ddlModule")).SelectedValue) ? (Guid?)((DropDownList)item.FindControl("ddlModule")).SelectedValue.ToGuid() : null;

            if (workflowTemplateParameter.ID == Guid.Empty)
            {
                workflowTemplateParameter.ID = Guid.NewGuid();
                ((List<WorkflowTemplateParameterMap>)ViewState["WorkflowTemplateParameters"]).Add(workflowTemplateParameter);
            }   
        }



        /// <summary>
        /// Saves the specified workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(Guid workflowTemplateId)
        {
            _dataManager.WorkflowTemplateParameter.Save(WorkflowTemplateParameterList, workflowTemplateId);
        }



        public void AddSystemParameters(Guid moduleId)
        {
            var workflowTemplateParameters = (List<WorkflowTemplateParameterMap>)ViewState["WorkflowTemplateParameters"] ?? new List<WorkflowTemplateParameterMap>();
            var workflowTemplateParametersCopy = new WorkflowTemplateParameterMap[workflowTemplateParameters.Count];
            workflowTemplateParameters.CopyTo(workflowTemplateParametersCopy);
            if (workflowTemplateParameters.Any())
            {
                foreach (var workflowTemplateParameterCopy in workflowTemplateParametersCopy)
                {
                    if (workflowTemplateParameterCopy.IsSystem)
                        workflowTemplateParameters.Remove(workflowTemplateParameterCopy);
                }
            }

            var workflowTemplateParametersSystem = new List<WorkflowTemplateParameterMap>();

            var module = _dataManager.Module.SelectById(moduleId);
            if (module != null)
            {
                switch (module.Name)
                {
                    case "Contacts":
                        workflowTemplateParametersSystem.Add(new WorkflowTemplateParameterMap { ID = Guid.NewGuid(), Name = "Контакт", ModuleID = moduleId, IsSystem = true });
                        workflowTemplateParametersSystem.Add(new WorkflowTemplateParameterMap { ID = Guid.NewGuid(), Name = "Компания", ModuleID = moduleId, IsSystem = true });
                        workflowTemplateParametersSystem.Add(new WorkflowTemplateParameterMap { ID = Guid.NewGuid(), Name = "Автор (роль)", ModuleID = moduleId, IsSystem = true });
                        workflowTemplateParametersSystem.Add(new WorkflowTemplateParameterMap { ID = Guid.NewGuid(), Name = "Ответственный по компании (роль)", ModuleID = moduleId, IsSystem = true });
                        break;
                }
            }

            ((List<WorkflowTemplateParameterMap>)ViewState["WorkflowTemplateParameters"]).AddRange(workflowTemplateParametersSystem);

            rgWorkflowTemplateParameter.Rebind();
        }
    }
}