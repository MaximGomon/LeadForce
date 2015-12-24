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

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class WorkflowTemplateElementResult : System.Web.UI.UserControl
    {
        private DataManager _dataManager;



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<WorkflowTemplateElementResultMap> WorkflowTemplateElementResultList
        {
            get
            {
                if (this.ViewState["WorkflowTemplateElementResults"] == null)
                    this.ViewState["WorkflowTemplateElementResults"] = new List<WorkflowTemplateElementResultMap>();
                return (List<WorkflowTemplateElementResultMap>)(ViewState["WorkflowTemplateElementResults"]);
            }

            set
            {
                ViewState["WorkflowTemplateElementResults"] = value;
            }
        }



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid WorkflowTemplateElementId
        {
            get
            {
                object o = ViewState["WorkflowTemplateElementId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["WorkflowTemplateElementId"] = value;
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

            rgWorkflowTemplateElementResult.Culture = new CultureInfo("ru-RU");
        }



        /// <summary>
        /// Binds the element results.
        /// </summary>
        public void BindData()
        {
            rgWorkflowTemplateElementResult.DataSource = WorkflowTemplateElementResultList;
            rgWorkflowTemplateElementResult.Rebind();
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgWorkflowTemplateElementResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementResult_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkflowTemplateElementResult.DataSource = WorkflowTemplateElementResultList;
        }



        /// <summary>
        /// Handles the OnItemCreated event of the rgWorkflowTemplateElementResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementResult_OnItemCreated(object sender, GridItemEventArgs e)
        {
            //to show/hide AddNewRecord button
            if (e.Item is GridCommandItem)
                e.Item.FindControl("InitInsertButton").Parent.Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
        }



        /// <summary>
        /// Handles the ItemDataBound event of the rgWorkflowTemplateElementResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementResult_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (WorkflowTemplateElementResultMap)e.Item.DataItem;

                if (data.IsSystem)
                {
                    item["EditColumn"].Controls[0].Visible = false;
                    item["DeleteColumn"].Controls[0].Visible = false;
                }

                rgWorkflowTemplateElementResult.Columns.FindByUniqueName("EditColumn").Display = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
                rgWorkflowTemplateElementResult.Columns.FindByUniqueName("DeleteColumn").Display = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var elementResult = (WorkflowTemplateElementResultMap)gridEditFormItem.DataItem;
                    ((TextBox)gridEditFormItem.FindControl("txtName")).Text = elementResult.Name;
                }

                gridEditFormItem.FindControl("lbtnSave").Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
            }
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgWorkflowTemplateElementResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementResult_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgWorkflowTemplateElementResult.MasterTableView.IsItemInserted = false;
            rgWorkflowTemplateElementResult.Rebind();
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgWorkflowTemplateElementResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementResult_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgWorkflowTemplateElementResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementResult_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = (e.Item as GridDataItem).GetDataKeyValue("ID").ToString().ToGuid();
            ((List<WorkflowTemplateElementResultMap>)ViewState["WorkflowTemplateElementResults"]).Remove(((List<WorkflowTemplateElementResultMap>)ViewState["WorkflowTemplateElementResults"]).Where(s => s.ID == id).FirstOrDefault());

            ((AdminPanel.WorkflowTemplate)Page).DeleteRelationByResultId(id);
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="workflowTemplateElementResultId">The workflow template element result id.</param>
        /// <param name="item">The item.</param>
        protected void SaveToViewState(Guid workflowTemplateElementResultId, GridEditableItem item)
        {
            var workflowTemplateElementResult = ((List<WorkflowTemplateElementResultMap>)ViewState["WorkflowTemplateElementResults"]).Where(s => s.ID == workflowTemplateElementResultId).FirstOrDefault() ?? new WorkflowTemplateElementResultMap();

            workflowTemplateElementResult.Name = ((TextBox)item.FindControl("txtName")).Text;

            if (workflowTemplateElementResult.ID == Guid.Empty)
            {
                workflowTemplateElementResult.ID = Guid.NewGuid();
                ((List<WorkflowTemplateElementResultMap>)ViewState["WorkflowTemplateElementResults"]).Add(workflowTemplateElementResult);
            }
        }
    }
}