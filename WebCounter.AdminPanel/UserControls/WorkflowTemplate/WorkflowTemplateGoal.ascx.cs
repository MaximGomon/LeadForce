using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class WorkflowTemplateGoal : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? WorkflowTemplateId
        {
            get { return (Guid?) ViewState["WorkflowTemplateId"]; }
            set { ViewState["WorkflowTemplateId"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<WorkflowTemplateGoalMap> WorkflowTemplateGoals
        {
            get
            {
                if (ViewState["WorkflowTemplateGoals"] == null)
                    ViewState["WorkflowTemplateGoals"] = new List<WorkflowTemplateGoalMap>();

                return (List<WorkflowTemplateGoalMap>)ViewState["WorkflowTemplateGoals"];
            }
            set { ViewState["WorkflowTemplateGoals"] = value; }
        }
        
        

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rgWorkflowTemplateGoal.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (WorkflowTemplateId.HasValue && WorkflowTemplateId != Guid.Empty)
            {
                WorkflowTemplateGoals =
                    _dataManager.WorkflowTemplateGoal.SelectAll((Guid) WorkflowTemplateId).ToList().Select(
                        o => new WorkflowTemplateGoalMap()
                                 {
                                     ID = o.ID,
                                     Title = o.Title,
                                     Description = o.Description,
                                     Elements = o.tbl_WorkflowTemplateElement.Select(w => new WorkflowTemplateElementMap()
                                                                                              {
                                                                                                  ID = w.ID,
                                                                                                  Name = w.Name,
                                                                                                  ElementType = w.ElementType
                                                                                              }).ToList()
                                 }).ToList();
            }
        }



        /// <summary>
        /// Handles the OnNeedDataSource event of the rgWorkflowTemplateGoal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoal_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkflowTemplateGoal.DataSource = WorkflowTemplateGoals;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgWorkflowTemplateGoal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoal_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem) e.Item;

                ((WorkflowTemplateGoalElement)e.Item.FindControl("ucWorkflowTemplateGoalElement")).
                            WorkflowTemplateId = WorkflowTemplateId;                

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var workflowTemplateGoal = (WorkflowTemplateGoalMap)gridEditFormItem.DataItem;
                    if (workflowTemplateGoal != null)
                    {
                        ((TextBox)gridEditFormItem.FindControl("txtTitle")).Text = workflowTemplateGoal.Title;
                        ((TextBox)gridEditFormItem.FindControl("txtDescription")).Text = workflowTemplateGoal.Description;
                        ((WorkflowTemplateGoalElement)e.Item.FindControl("ucWorkflowTemplateGoalElement")).WorkflowTemplateElements = workflowTemplateGoal.Elements;                        
                        ((WorkflowTemplateGoalElement)e.Item.FindControl("ucWorkflowTemplateGoalElement")).Rebind();
                    }
                }
            }            
        }



        /// <summary>
        /// Handles the OnDeleteCommand event of the rgWorkflowTemplateGoal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoal_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var gridDataItem = e.Item as GridDataItem;
            if (gridDataItem != null)
            {
                var id = Guid.Parse(gridDataItem.GetDataKeyValue("ID").ToString());
                WorkflowTemplateGoals.Remove(WorkflowTemplateGoals.FirstOrDefault(s => s.ID == id));
            }
        }



        /// <summary>
        /// Handles the OnInsertCommand event of the rgWorkflowTemplateGoal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoal_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgWorkflowTemplateGoal.MasterTableView.IsItemInserted = false;
            rgWorkflowTemplateGoal.Rebind();
        }



        /// <summary>
        /// Handles the OnUpdateCommand event of the rgWorkflowTemplateGoal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoal_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            if (item != null)
                SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="workflowTemplateGoalId">The workflow template goal id.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid workflowTemplateGoalId, GridEditableItem item)
        {
            var workflowTemplateGoal = WorkflowTemplateGoals.FirstOrDefault(s => s.ID == workflowTemplateGoalId) ?? new WorkflowTemplateGoalMap();

            workflowTemplateGoal.Title = ((TextBox)item.FindControl("txtTitle")).Text;
            workflowTemplateGoal.Description = ((TextBox)item.FindControl("txtDescription")).Text;
            workflowTemplateGoal.Elements = ((WorkflowTemplateGoalElement)item.FindControl("ucWorkflowTemplateGoalElement")).WorkflowTemplateElements;

            if (workflowTemplateGoal.ID == Guid.Empty)
            {
                workflowTemplateGoal.ID = Guid.NewGuid();
                WorkflowTemplateGoals.Add(workflowTemplateGoal);
            }
        }



        /// <summary>
        /// Saves the specified workflow template goal id.
        /// </summary>
        /// <param name="workflowTemplateGoalId">The workflow template goal id.</param>
        public void Save(Guid workflowTemplateGoalId)
        {
            _dataManager.WorkflowTemplateGoal.Update(WorkflowTemplateGoals, workflowTemplateGoalId);
        }
    }
}