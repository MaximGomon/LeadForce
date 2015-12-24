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
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class WorkflowTemplateElementRelation : System.Web.UI.UserControl
    {
        private DataManager _dataManager;



        public List<WorkflowTemplateElementRelationMap> WorkflowTemplateElementRelationList
        {
            get
            {
                if (ViewState["WorkflowTemplateElementRelations"] == null)
                    ViewState["WorkflowTemplateElementRelations"] = new List<WorkflowTemplateElementRelationMap>();
                return (List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"];
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

            rgWorkflowTemplateElementRelation.Culture = new CultureInfo("ru-RU");

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
                ViewState["WorkflowTemplateElementRelations"] =
                    _dataManager.WorkflowTemplateElementRelation.SelectAll(WorkflowTemplateId).Select(
                        a =>
                        new WorkflowTemplateElementRelationMap
                        {
                            ID = a.ID,
                            WorkflowTemplateID = a.WorkflowTemplateID,
                            StartElementID = a.StartElementID,
                            StartElementResult = a.StartElementResult,
                            EndElementID = a.EndElementID
                        }).ToList();
            }
            else
                ViewState["WorkflowTemplateElementRelations"] = new List<WorkflowTemplateElementRelationMap>();

            rgWorkflowTemplateElementRelation.Rebind();
        }



        /// <summary>
        /// Rebinds the data.
        /// </summary>
        public void RebindData()
        {
            //ViewState["WorkflowTemplateElementRelations"] = new List<WorkflowTemplateElementRelationMap>();
            rgWorkflowTemplateElementRelation.Rebind();
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgWorkflowTemplateElementRelation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementRelation_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkflowTemplateElementRelation.DataSource = ViewState["WorkflowTemplateElementRelations"];
        }



        /// <summary>
        /// Handles the OnItemCreated event of the rgWorkflowTemplateElementRelation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementRelation_OnItemCreated(object sender, GridItemEventArgs e)
        {
            //to show/hide AddNewRecord button
            if (e.Item is GridCommandItem)
                e.Item.FindControl("InitInsertButton").Parent.Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
        }



        /// <summary>
        /// Handles the ItemDataBound event of the rgWorkflowTemplateElementRelation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementRelation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                rgWorkflowTemplateElementRelation.Columns.FindByUniqueName("DeleteColumn").Display = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
            }

            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (WorkflowTemplateElementRelationMap)e.Item.DataItem;

                var workflowTemplateElementList = ((WorkflowTemplateElement)Parent.FindControl("ucWorkflowTemplateElement")).WorkflowTemplateElementList;
                var elementType = workflowTemplateElementList.SingleOrDefault(a => a.ID == data.StartElementID).ElementType;

                ((Literal)item.FindControl("litStartElementName")).Text = workflowTemplateElementList.SingleOrDefault(a => a.ID == data.StartElementID).Name;
                ((Literal)item.FindControl("litEndElementName")).Text = workflowTemplateElementList.SingleOrDefault(a => a.ID == data.EndElementID).Name;
                switch ((WorkflowTemplateElementType)elementType)
                {
                    case WorkflowTemplateElementType.Message:
                        ((Literal)item.FindControl("litStartElementResultName")).Text = EnumHelper.GetEnumDescription((ActionStatus)data.StartElementResult.ToInt());
                        break;
                    case WorkflowTemplateElementType.WaitingEvent:
                        if (data.StartElementResult == "0")
                            ((Literal)item.FindControl("litStartElementResultName")).Text = "Событие не произошло";
                        if (data.StartElementResult == "1")
                            ((Literal)item.FindControl("litStartElementResultName")).Text = "Событие произошло";
                        break;
                    case WorkflowTemplateElementType.Task:
                        var elementResult = workflowTemplateElementList.SingleOrDefault(a => a.ID == data.StartElementID).ElementResult.FirstOrDefault(a => a.ID == data.StartElementResult.ToGuid());
                        //if (elementResult != null)
                            ((Literal)item.FindControl("litStartElementResultName")).Text = elementResult.Name;
                        break;
                }
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem) e.Item;
                var ddlStartElement = ((DropDownList)gridEditFormItem.FindControl("ddlStartElement"));
                var ddlResultStartElement = ((DropDownList)gridEditFormItem.FindControl("ddlResultStartElement"));
                var ddlEndElement = ((DropDownList)gridEditFormItem.FindControl("ddlEndElement"));
                var workflowTemplateElementList = ((WorkflowTemplateElement)Parent.FindControl("ucWorkflowTemplateElement")).WorkflowTemplateElementList;

                ddlStartElement.DataSource = workflowTemplateElementList;
                ddlStartElement.DataValueField = "ID";
                ddlStartElement.DataTextField = "Name";
                ddlStartElement.DataBind();
                ddlStartElement.Items.Insert(0, new ListItem("Выберите значение", ""));

                ddlEndElement.DataSource = workflowTemplateElementList;
                ddlEndElement.DataValueField = "ID";
                ddlEndElement.DataTextField = "Name";
                ddlEndElement.DataBind();
                ddlEndElement.Items.Insert(0, new ListItem("Выберите значение", ""));

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var elementRelation = (WorkflowTemplateElementRelationMap)gridEditFormItem.DataItem;
                    ddlStartElement.Items.FindByValue(elementRelation.StartElementID.ToString()).Selected = true;
                    BindResultStartElement(ddlStartElement);
                    if (!string.IsNullOrEmpty(elementRelation.StartElementResult))
                        ddlResultStartElement.Items.FindByValue(elementRelation.StartElementResult).Selected = true;
                    ddlEndElement.Items.FindByValue(elementRelation.EndElementID.ToString()).Selected = true;
                }

                gridEditFormItem.FindControl("lbtnSave").Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
            }
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgWorkflowTemplateElementRelation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementRelation_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgWorkflowTemplateElementRelation.MasterTableView.IsItemInserted = false;
            rgWorkflowTemplateElementRelation.Rebind();
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgWorkflowTemplateElementRelation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementRelation_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgWorkflowTemplateElementRelation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElementRelation_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = (e.Item as GridDataItem).GetDataKeyValue("ID").ToString().ToGuid();
            ((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).Remove(((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).Where(s => s.ID == id).FirstOrDefault());
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="workflowTemplateParameterId">The workflow template parameter id.</param>
        /// <param name="item">The item.</param>
        protected void SaveToViewState(Guid workflowTemplateElementRelationId, GridEditableItem item)
        {
            var workflowTemplateElementRelation = ((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).Where(s => s.ID == workflowTemplateElementRelationId).FirstOrDefault() ?? new WorkflowTemplateElementRelationMap();
            //var workflowTemplateElementList = ((WorkflowTemplateElement)Parent.FindControl("ucWorkflowTemplateElement")).WorkflowTemplateElementList;

            workflowTemplateElementRelation.StartElementID = ((DropDownList)item.FindControl("ddlStartElement")).SelectedValue.ToGuid();
            if (item.FindControl("pnlResultStartElement").Visible)
                workflowTemplateElementRelation.StartElementResult = ((DropDownList)item.FindControl("ddlResultStartElement")).SelectedValue;
            workflowTemplateElementRelation.EndElementID = ((DropDownList)item.FindControl("ddlEndElement")).SelectedValue.ToGuid();

            if (workflowTemplateElementRelation.ID == Guid.Empty)
            {
                workflowTemplateElementRelation.ID = Guid.NewGuid();
                ((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).Add(workflowTemplateElementRelation);
            }
        }



        /// <summary>
        /// Saves the specified workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(Guid workflowTemplateId)
        {
            _dataManager.WorkflowTemplateElementRelation.Save(WorkflowTemplateElementRelationList, workflowTemplateId);
        }



        /// <summary>
        /// Deletes the by element id.
        /// </summary>
        /// <param name="elementId">The element id.</param>
        public void DeleteByElementId(Guid elementId)
        {
            var toDeleteList =
                ((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).Where(
                    a => a.StartElementID == elementId || a.EndElementID == elementId).ToList();

            foreach (var toDelete in toDeleteList)
            {
                ((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).Remove(((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).FirstOrDefault(s => s.ID == toDelete.ID));
            }

            rgWorkflowTemplateElementRelation.Rebind();
        }



        /// <summary>
        /// Deletes the by result id.
        /// </summary>
        /// <param name="resultId">The result id.</param>
        public void DeleteByResultId(Guid elementResultId)
        {
            var toDeleteList =
                ((List<WorkflowTemplateElementRelationMap>) ViewState["WorkflowTemplateElementRelations"]).Where(
                    a => a.StartElementResult == elementResultId.ToString()).ToList();

            foreach (var toDelete in toDeleteList)
            {
                ((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).Remove(((List<WorkflowTemplateElementRelationMap>)ViewState["WorkflowTemplateElementRelations"]).FirstOrDefault(s => s.ID == toDelete.ID));
            }

            rgWorkflowTemplateElementRelation.Rebind();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlStartElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlStartElement_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindResultStartElement((DropDownList)sender);
        }



        protected void BindResultStartElement(DropDownList ddlStartElement)
        {
            //var ddlStartElement = (DropDownList)sender;
            var ddlResultStartElement = (DropDownList)ddlStartElement.Parent.FindControl("ddlResultStartElement");
            var pnlResultStartElement = (Panel)ddlStartElement.Parent.FindControl("pnlResultStartElement");

            if (!string.IsNullOrEmpty(ddlStartElement.SelectedValue))
            {
                var startElement = ((WorkflowTemplateElement)Parent.FindControl("ucWorkflowTemplateElement")).WorkflowTemplateElementList.SingleOrDefault(a => a.ID == ddlStartElement.SelectedValue.ToGuid());

                if (startElement != null)
                {
                    switch ((WorkflowTemplateElementType)startElement.ElementType)
                    {
                        case WorkflowTemplateElementType.StartProcess:
                        case WorkflowTemplateElementType.EndProcess:
                            pnlResultStartElement.Visible = false;
                            break;
                        case WorkflowTemplateElementType.Message:
                            ddlResultStartElement.Items.Add(new ListItem("Выберите значение", ""));
                            ddlResultStartElement.Items.Add(new ListItem("Выполнено", ((int)ActionStatus.Done).ToString()));
                            ddlResultStartElement.Items.Add(new ListItem("Ошибка", ((int)ActionStatus.Error).ToString()));
                            pnlResultStartElement.Visible = true;
                            break;
                        case WorkflowTemplateElementType.WaitingEvent:
                            ddlResultStartElement.Items.Add(new ListItem("Выберите значение", ""));
                            ddlResultStartElement.Items.Add(new ListItem("Событие произошло", "1"));
                            ddlResultStartElement.Items.Add(new ListItem("Событие не произошло", "0"));
                            pnlResultStartElement.Visible = true;
                            break;
                        case WorkflowTemplateElementType.Task:
                            ddlResultStartElement.Items.Add(new ListItem("Выберите значение", ""));
                            var elementResults = startElement.ElementResult;
                            foreach (var elementResult in elementResults)
                                ddlResultStartElement.Items.Add(new ListItem(elementResult.Name, elementResult.ID.ToString()));
                            pnlResultStartElement.Visible = true;
                            break;
                    }
                }
            }
        }
    }
}