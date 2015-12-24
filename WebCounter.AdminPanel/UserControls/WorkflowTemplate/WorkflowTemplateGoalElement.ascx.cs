using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Newtonsoft.Json;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class WorkflowTemplateGoalElement : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();        

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<WorkflowTemplateElementMap> WorkflowTemplateElements
        {
            get
            {
                if (ViewState["WorkflowTemplateElements"] == null)
                    ViewState["WorkflowTemplateElements"] = new List<WorkflowTemplateElementMap>();

                return (List<WorkflowTemplateElementMap>)ViewState["WorkflowTemplateElements"];
            }
            set { ViewState["WorkflowTemplateElements"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? WorkflowTemplateId
        {
            get
            {
                return (Guid?)ViewState["WorkflowTemplateId"];
            }
            set { ViewState["WorkflowTemplateId"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        protected List<WorkflowTemplateElementMap> AvailableWorkflowTemplateElements
        {
            get
            {
                if (ViewState["AvailableWorkflowTemplateElements"] == null)
                    ViewState["AvailableWorkflowTemplateElements"] = new List<WorkflowTemplateElementMap>();

                return (List<WorkflowTemplateElementMap>)ViewState["AvailableWorkflowTemplateElements"];
            }
            set { ViewState["AvailableWorkflowTemplateElements"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rgWorkflowTemplateGoalElement.Culture = new CultureInfo("ru-RU");                        
        }



        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {
            rgWorkflowTemplateGoalElement.DataSource = WorkflowTemplateElements;
            rgWorkflowTemplateGoalElement.DataBind();
        }



        /// <summary>
        /// Handles the OnNeedDataSource event of the rgWorkflowTemplateGoalElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoalElement_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {            
            rgWorkflowTemplateGoalElement.DataSource = WorkflowTemplateElements;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgWorkflowTemplateGoalElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoalElement_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                UpdateWorkflowGoalElements();

                var rcbWorkflowTemplateElements = (RadComboBox)gridEditFormItem.FindControl("rcbWorkflowTemplateElements");
                rcbWorkflowTemplateElements.DataSource = AvailableWorkflowTemplateElements;
                rcbWorkflowTemplateElements.DataTextField = "Name";
                rcbWorkflowTemplateElements.DataValueField = "ID";
                rcbWorkflowTemplateElements.DataBind();                

                if (!e.Item.OwnerTableView.IsItemInserted)
                {                    

                    var workflowTemplateElement = (WorkflowTemplateElementMap)gridEditFormItem.DataItem;
                    if (workflowTemplateElement != null)
                        rcbWorkflowTemplateElements.SelectedValue = workflowTemplateElement.ID.ToString();
                    
                }
            }
            else if (e.Item is GridDataItem)
            {
                var workflowTemplateElementMap = e.Item.DataItem as WorkflowTemplateElementMap;
                ((Literal)e.Item.FindControl("lrlElementType")).Text = EnumHelper.GetEnumDescription((WorkflowTemplateElementType)workflowTemplateElementMap.ElementType);                
            }
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            rgWorkflowTemplateGoalElement.Rebind();
        }



        /// <summary>
        /// Updates the workflow goal elements.
        /// </summary>
        private void UpdateWorkflowGoalElements()
        {
            var jsonElements = (HiddenField)FindControlRecursive(Page, "jsonElements");

            if (!string.IsNullOrEmpty(jsonElements.Value))
            {
                var deserializedElements = JsonConvert.DeserializeObject<List<WorkflowTemplateElementMap>>(jsonElements.Value);
                var elements = deserializedElements.Select(o => new WorkflowTemplateElementMap()
                               {
                                   ID = o.ID,
                                   Name = o.Name,
                                   ElementType = o.ElementType
                               });
                AvailableWorkflowTemplateElements = elements.ToList();
            }
            else if (WorkflowTemplateId.HasValue)
            {
                AvailableWorkflowTemplateElements = _dataManager.WorkflowTemplateElement.SelectAll(WorkflowTemplateId.Value).Select(
                        o => new WorkflowTemplateElementMap {ID = o.ID, Name = o.Name, ElementType = o.ElementType}).ToList();
            }                 
        }



        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            return (from Control c in root.Controls select FindControlRecursive(c, id)).FirstOrDefault(rc => rc != null);
        }



        /// <summary>
        /// Handles the OnDeleteCommand event of the rgWorkflowTemplateGoalElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoalElement_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var gridDataItem = e.Item as GridDataItem;
            if (gridDataItem != null)
            {
                var id = Guid.Parse(gridDataItem.GetDataKeyValue("ID").ToString());
                WorkflowTemplateElements.Remove(WorkflowTemplateElements.FirstOrDefault(s => s.ID == id));
            }
        }



        /// <summary>
        /// Handles the OnInsertCommand event of the rgWorkflowTemplateGoalElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoalElement_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgWorkflowTemplateGoalElement.MasterTableView.IsItemInserted = false;
            rgWorkflowTemplateGoalElement.Rebind();
        }



        /// <summary>
        /// Handles the OnUpdateCommand event of the rgWorkflowTemplateGoalElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateGoalElement_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            if (item != null)
                SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template goal id.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid workflowTemplateElementId, GridEditableItem item)
        {
            var workflowTemplateElementMap = WorkflowTemplateElements.FirstOrDefault(s => s.ID == workflowTemplateElementId) ?? new WorkflowTemplateElementMap();

            var workflowTemplateElement = AvailableWorkflowTemplateElements.SingleOrDefault(o => o.ID == (Guid.Parse(((RadComboBox)item.FindControl("rcbWorkflowTemplateElements")).SelectedValue)));
            workflowTemplateElementMap.Name = workflowTemplateElement.Name;
            workflowTemplateElementMap.ElementType = workflowTemplateElement.ElementType;

            if (workflowTemplateElementMap.ID == Guid.Empty)
            {
                workflowTemplateElementMap.ID = workflowTemplateElement.ID;
                WorkflowTemplateElements.Add(workflowTemplateElementMap);
            }
        }
    }
}