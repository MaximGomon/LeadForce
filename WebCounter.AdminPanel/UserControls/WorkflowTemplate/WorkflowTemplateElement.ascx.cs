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
    public partial class WorkflowTemplateElement : System.Web.UI.UserControl
    {
        private DataManager _dataManager;



        public List<WorkflowTemplateElementMap> WorkflowTemplateElementList
        {
            get
            {
                if (ViewState["WorkflowTemplateElements"] == null)
                    ViewState["WorkflowTemplateElements"] = new List<WorkflowTemplateElementMap>();
                return (List<WorkflowTemplateElementMap>)ViewState["WorkflowTemplateElements"];
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

            rgWorkflowTemplateElement.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if (WorkflowTemplateId != Guid.Empty)
                ViewState["WorkflowTemplateElements"] = _dataManager.WorkflowTemplateElement.SelectAllMap(WorkflowTemplateId);
            else
            {
                var seElements = new List<WorkflowTemplateElementMap>();
                seElements.Add(new WorkflowTemplateElementMap
                                   {
                                       ID = Guid.NewGuid(),
                                       WorkflowTemplateID = WorkflowTemplateId,
                                       Name = "Начало",
                                       ElementType = 5,
                                       Optional = false,
                                       AllowOptionalTransfer = false,
                                       ShowCurrentUser = false,
                                       Order = 0,
                                       StartAfter = 0,
                                       StartPeriod = 0,
                                       ControlFromBeginProccess = false
                                   });
                seElements.Add(new WorkflowTemplateElementMap
                {
                    ID = Guid.NewGuid(),
                    WorkflowTemplateID = WorkflowTemplateId,
                    Name = "Конец",
                    ElementType = 6,
                    Optional = false,
                    AllowOptionalTransfer = false,
                    ShowCurrentUser = false,
                    Order = 0,
                    StartAfter = 0,
                    StartPeriod = 0,
                    ControlFromBeginProccess = false
                    //ElementResult = new List<WorkflowTemplateElementResultMap>()
                });
                ViewState["WorkflowTemplateElements"] = seElements;
            }
                

            rgWorkflowTemplateElement.Rebind();
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkflowTemplateElement.DataSource = ViewState["WorkflowTemplateElements"];
        }



        /// <summary>
        /// Handles the OnItemCreated event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_OnItemCreated(object sender, GridItemEventArgs e)
        {
            //to show/hide AddNewRecord button
            if (e.Item is GridCommandItem)
                e.Item.FindControl("InitInsertButton").Parent.Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
        }



        /// <summary>
        /// Handles the ItemDataBound event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (WorkflowTemplateElementMap)e.Item.DataItem;

                if (data.ElementType == 5 || data.ElementType == 6)
                {
                    item["EditColumn"].Controls[0].Visible = false;
                    item["DeleteColumn"].Controls[0].Visible = false;
                }

                rgWorkflowTemplateElement.Columns.FindByUniqueName("DeleteColumn").Display = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var userControl = (SaveWorkflowTemplateElement)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                userControl.BindData();

                var gridEditFormItem = (GridEditFormItem)e.Item;

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var element = (WorkflowTemplateElementMap)gridEditFormItem.DataItem;
                    ((TextBox)userControl.FindControl("txtName")).Text = element.Name;
                    ((DropDownList)userControl.FindControl("ddlElementType")).Items.FindByValue(element.ElementType.ToString()).Selected = true;
                    ((CheckBox)userControl.FindControl("cbOptional")).Checked = element.Optional;
                    userControl.FindControl("pnlResultName").Visible = element.Optional;
                    ((TextBox)userControl.FindControl("txtResultName")).Text = element.ResultName;
                    ((CheckBox)userControl.FindControl("cbAllowOptionalTransfer")).Checked = element.AllowOptionalTransfer;
                    ((CheckBox)userControl.FindControl("cbShowCurrentUser")).Checked = element.ShowCurrentUser;
                    ((TextBox)userControl.FindControl("txtDescription")).Text = element.Description;
                    ((RadNumericTextBox)userControl.FindControl("txtStartAfter")).Text = element.StartAfter.ToString();
                    ((DropDownList)userControl.FindControl("ddlStartPeriod")).Items.FindByValue(element.StartPeriod.ToString()).Selected = true;
                    //userControl.FindControl("pnlTypicalDuration").Visible = !element.TypicalDuration;
                    ((RadNumericTextBox)userControl.FindControl("txtDurationHours")).Text = element.DurationHours != null ? element.DurationHours.ToString() : "";
                    ((RadNumericTextBox)userControl.FindControl("txtDurationMinutes")).Text = element.DurationMinutes != null ? element.DurationMinutes.ToString() : "";
                    //userControl.FindControl("pnlRequiredControl").Visible = element.RequiredControl;
                    ((RadNumericTextBox)userControl.FindControl("txtControlAfter")).Text = element.ControlAfter != null ? element.ControlAfter.ToString() :  "";
                    if (element.ControlPeriod != null)
                        ((DropDownList)userControl.FindControl("ddlControlPeriod")).Items.FindByValue(element.ControlPeriod.ToString()).Selected = true;
                    ((CheckBox)userControl.FindControl("cbControlFromBeginProccess")).Checked = element.ControlFromBeginProccess;

                    switch ((WorkflowTemplateElementType)element.ElementType)
                    {
                        case  WorkflowTemplateElementType.Message:
                            //((DictionaryComboBox)userControl.FindControl("dcbMessageSiteActionTemplate")).SelectedId = (element.Parameters.SingleOrDefault(a => a.Key == "SiteActionTemplateID").Value).ToGuid();
                            ((UserControls.SiteActionTemplate)userControl.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId = (element.Parameters.FirstOrDefault(a => a.Key == "SiteActionTemplateID").Value).ToGuid();
                            break;
                        case WorkflowTemplateElementType.WaitingEvent:
                            ((DropDownList)(userControl.FindControl("ucWorkflowTemplateElementConditionEvent")).FindControl("ddlCondition")).Items.FindByValue(element.Condition != null ? element.Condition.ToString() : "").Selected = true;
                            ((WorkflowTemplateElementConditionEvent)userControl.FindControl("ucWorkflowTemplateElementConditionEvent")).WorkflowTemplateConditionEventList = element.ConditionEvent;
                            ((RadGrid)userControl.FindControl("ucWorkflowTemplateElementConditionEvent").FindControl("rgWorkflowTemplateConditionEvent")).Rebind();
                            break;
                        case WorkflowTemplateElementType.Activity:
                            ((TextBox)userControl.FindControl("txtContactActivityDescription")).Text = element.Parameters.FirstOrDefault(a => a.Key == "ContactActivityDescription").Value;
                            break;
                        case WorkflowTemplateElementType.Task:
                            ((DictionaryComboBox)userControl.FindControl("dcbTaskType")).SelectedId = element.Parameters.FirstOrDefault(a => a.Key == "TaskType").Value.ToGuid();
                            ((CheckBox)userControl.FindControl("cbIsImportantTask")).Checked = element.Parameters.FirstOrDefault(a => a.Key == "TaskIsImportantTask").Value == "0" ? false : true;
                            ((CheckBox)userControl.FindControl("cbIsUrgentTask")).Checked = element.Parameters.FirstOrDefault(a => a.Key == "TaskIsUrgentTask").Value == "0" ? false : true;
                            ((WorkflowTemplateElementResult)userControl.FindControl("ucWorkflowTemplateElementResult")).WorkflowTemplateElementResultList = element.ElementResult;
                            ((WorkflowTemplateElementResult)userControl.FindControl("ucWorkflowTemplateElementResult")).BindData();
                            break;
                    }

                    userControl.BindTabs();
                    userControl.BindPanels();
                }
            }
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            var userControl = (UserControl)item.FindControl(GridEditFormItem.EditFormUserControlID);
            var ddlElementType = (DropDownList)userControl.FindControl("ddlElementType");

            var selectedElementType = ddlElementType.SelectedValue.ToEnum<WorkflowTemplateElementType>();
            if ((selectedElementType == WorkflowTemplateElementType.StartProcess || selectedElementType == WorkflowTemplateElementType.EndProcess)
                && WorkflowTemplateElementList.SingleOrDefault(a => a.ElementType == (int)ddlElementType.SelectedValue.ToEnum<WorkflowTemplateElementType>()) != null)
            {
                e.Canceled = true;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ExistElementType", string.Format("alert(\"Элемент процесса с типом элемента '{0}' уже существует.\");", EnumHelper.GetEnumDescription(selectedElementType)), true);
                return;
            }

            SaveToViewState(Guid.Empty, item);

            rgWorkflowTemplateElement.MasterTableView.IsItemInserted = false;
            rgWorkflowTemplateElement.Rebind();
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var editId = item.GetDataKeyValue("ID").ToString().ToGuid();

            var userControl = (UserControl)item.FindControl(GridEditFormItem.EditFormUserControlID);
            var ddlElementType = (DropDownList)userControl.FindControl("ddlElementType");

            var selectedElementType = ddlElementType.SelectedValue.ToEnum<WorkflowTemplateElementType>();
            if ((selectedElementType == WorkflowTemplateElementType.StartProcess || selectedElementType == WorkflowTemplateElementType.EndProcess)
                && WorkflowTemplateElementList.SingleOrDefault(a => a.ID != editId && a.ElementType == (int)ddlElementType.SelectedValue.ToEnum<WorkflowTemplateElementType>()) != null)
            {
                e.Canceled = true;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ExistElementType", string.Format("alert(\"Элемент процесса с типом элемента '{0}' уже существует.\");", EnumHelper.GetEnumDescription(selectedElementType)), true);
                return;
            }

            SaveToViewState(editId, item);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = (e.Item as GridDataItem).GetDataKeyValue("ID").ToString().ToGuid();
            ((List<WorkflowTemplateElementMap>)ViewState["WorkflowTemplateElements"]).Remove(((List<WorkflowTemplateElementMap>)ViewState["WorkflowTemplateElements"]).Where(s => s.ID == id).FirstOrDefault());

            ((WorkflowTemplateElementRelation)Parent.FindControl("ucWorkflowTemplateElementRelation")).DeleteByElementId(id);
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <param name="item">The item.</param>
        protected void SaveToViewState(Guid workflowTemplateElementId, GridEditableItem item)
        {
            var userControl = (UserControl)item.FindControl(GridEditFormItem.EditFormUserControlID);

            var workflowTemplateElement = ((List<WorkflowTemplateElementMap>)ViewState["WorkflowTemplateElements"]).Where(s => s.ID == workflowTemplateElementId).FirstOrDefault() ?? new WorkflowTemplateElementMap();

            workflowTemplateElement.Name = ((TextBox)userControl.FindControl("txtName")).Text;
            workflowTemplateElement.ElementType = ((DropDownList)userControl.FindControl("ddlElementType")).SelectedValue.ToInt();
            workflowTemplateElement.Optional = ((CheckBox)userControl.FindControl("cbOptional")).Checked;
            workflowTemplateElement.ResultName = ((TextBox)userControl.FindControl("txtResultName")).Text;
            workflowTemplateElement.AllowOptionalTransfer = ((CheckBox)userControl.FindControl("cbAllowOptionalTransfer")).Checked;
            workflowTemplateElement.ShowCurrentUser = ((CheckBox)userControl.FindControl("cbShowCurrentUser")).Checked;
            workflowTemplateElement.Description = ((TextBox)userControl.FindControl("txtDescription")).Text;
            workflowTemplateElement.StartAfter = ((RadNumericTextBox)userControl.FindControl("txtStartAfter")).Text.ToInt();
            workflowTemplateElement.StartPeriod = ((DropDownList)userControl.FindControl("ddlStartPeriod")).SelectedValue.ToInt();
            workflowTemplateElement.DurationHours = userControl.FindControl("pnlTypicalDuration").Visible ? ((RadNumericTextBox)userControl.FindControl("txtDurationHours")).Text.ToInt() : (int?)null;
            workflowTemplateElement.DurationMinutes = userControl.FindControl("pnlTypicalDuration").Visible ? ((RadNumericTextBox)userControl.FindControl("txtDurationMinutes")).Text.ToInt() : (int?)null;
            workflowTemplateElement.ControlAfter = userControl.FindControl("pnlRequiredControl").Visible ? ((RadNumericTextBox)userControl.FindControl("txtControlAfter")).Text.ToInt() : (int?)null;
            workflowTemplateElement.ControlPeriod = userControl.FindControl("pnlRequiredControl").Visible ? ((DropDownList)userControl.FindControl("ddlControlPeriod")).SelectedValue.ToInt() : (int?)null;
            workflowTemplateElement.ControlFromBeginProccess = userControl.FindControl("pnlRequiredControl").Visible ? ((CheckBox)userControl.FindControl("cbControlFromBeginProccess")).Checked : false;

            workflowTemplateElement.Parameters = new Dictionary<string, string>();
            switch ((WorkflowTemplateElementType)workflowTemplateElement.ElementType)
            {
                case WorkflowTemplateElementType.Message:
                    workflowTemplateElement.Parameters.Add("SiteActionTemplateID", ((UserControls.SiteActionTemplate)userControl.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId.ToString());
                    //workflowTemplateElement.Parameters.Add("SiteActionTemplateID", ((DictionaryComboBox)userControl.FindControl("dcbMessageSiteActionTemplate")).SelectedId.ToString());
                    break;
                case WorkflowTemplateElementType.WaitingEvent:
                    workflowTemplateElement.Condition = ((DropDownList)(userControl.FindControl("ucWorkflowTemplateElementConditionEvent")).FindControl("ddlCondition")).SelectedValue.ToInt();
                    if (!string.IsNullOrEmpty(((RadNumericTextBox)(userControl.FindControl("ucWorkflowTemplateElementConditionEvent")).FindControl("txtActivityCount")).Text))
                        workflowTemplateElement.ActivityCount = ((RadNumericTextBox)(userControl.FindControl("ucWorkflowTemplateElementConditionEvent")).FindControl("txtActivityCount")).Text.ToInt();
                    else
                        workflowTemplateElement.ActivityCount = null;
                    workflowTemplateElement.ConditionEvent = ((WorkflowTemplateElementConditionEvent)userControl.FindControl("ucWorkflowTemplateElementConditionEvent")).WorkflowTemplateConditionEventList;
                    break;
                case WorkflowTemplateElementType.Activity:
                    workflowTemplateElement.Parameters.Add("ContactActivityDescription", ((TextBox)userControl.FindControl("txtContactActivityDescription")).Text);
                    break;
                case WorkflowTemplateElementType.Task:
                    workflowTemplateElement.Parameters.Add("TaskType", ((DictionaryComboBox)userControl.FindControl("dcbTaskType")).SelectedId.ToString());
                    workflowTemplateElement.Parameters.Add("TaskIsImportantTask", ((CheckBox)userControl.FindControl("cbIsImportantTask")).Checked ? "1" : "0");
                    workflowTemplateElement.Parameters.Add("TaskIsUrgentTask", ((CheckBox)userControl.FindControl("cbIsUrgentTask")).Checked ? "1" : "0");
                    workflowTemplateElement.ElementResult = ((WorkflowTemplateElementResult)userControl.FindControl("ucWorkflowTemplateElementResult")).WorkflowTemplateElementResultList;
                    break;
            }

            if (workflowTemplateElement.ID == Guid.Empty)
            {
                if (WorkflowTemplateElementList.Count > 0)
                    workflowTemplateElement.Order = WorkflowTemplateElementList.Max(a => a.Order) + 1;

                workflowTemplateElement.ID = Guid.NewGuid();
                ((List<WorkflowTemplateElementMap>)ViewState["WorkflowTemplateElements"]).Add(workflowTemplateElement);
            }

            ((WorkflowTemplateElementRelation)Parent.FindControl("ucWorkflowTemplateElementRelation")).RebindData();
        }



        /// <summary>
        /// Updates the order.
        /// </summary>
        protected void UpdateOrder()
        {
            for (int i = 0; i < rgWorkflowTemplateElement.Items.Count; i++)
                WorkflowTemplateElementList.SingleOrDefault(a => a.ID == rgWorkflowTemplateElement.Items[i].GetDataKeyValue("ID").ToString().ToGuid()).Order = i;
        }



        /// <summary>
        /// Saves the specified workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(Guid workflowTemplateId)
        {
            _dataManager.WorkflowTemplateElement.Save(WorkflowTemplateElementList, workflowTemplateId);
        }



        /// <summary>
        /// Handles the RowDrop event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridDragDropEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == rgWorkflowTemplateElement.ClientID)
                {
                    if (e.DestDataItem != null && e.DestDataItem.OwnerGridID == rgWorkflowTemplateElement.ClientID)
                    {
                        //reorder items in pending grid
                        var element = WorkflowTemplateElementList.SingleOrDefault(a => a.ID == e.DestDataItem.GetDataKeyValue("ID").ToString().ToGuid());
                        int destinationIndex = WorkflowTemplateElementList.IndexOf(element);

                        if (e.DropPosition == GridItemDropPosition.Above && e.DestDataItem.ItemIndex > e.DraggedItems[0].ItemIndex)
                        {
                            destinationIndex -= 1;
                        }
                        if (e.DropPosition == GridItemDropPosition.Below && e.DestDataItem.ItemIndex < e.DraggedItems[0].ItemIndex)
                        {
                            destinationIndex += 1;
                        }

                        var workflowTemplateElementListToMove = new List<WorkflowTemplateElementMap>();
                        foreach (GridDataItem draggedItem in e.DraggedItems)
                        {
                            var tmpElement = WorkflowTemplateElementList.SingleOrDefault(a => a.ID == draggedItem.GetDataKeyValue("ID").ToString().ToGuid());
                            if (tmpElement != null)
                                workflowTemplateElementListToMove.Add(tmpElement);
                        }

                        foreach (var elementToMove in workflowTemplateElementListToMove)
                        {
                            WorkflowTemplateElementList.Remove(elementToMove);
                            WorkflowTemplateElementList.Insert(destinationIndex, elementToMove);
                        }
                        rgWorkflowTemplateElement.Rebind();

                        int destinationItemIndex = destinationIndex - (rgWorkflowTemplateElement.PageSize * rgWorkflowTemplateElement.CurrentPageIndex);
                        e.DestinationTableView.Items[destinationItemIndex].Selected = true;
                    }
                }
            }

            UpdateOrder();
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dataItem = rgWorkflowTemplateElement.SelectedItems[0] as GridDataItem;
            //dataItem.GetDataKeyValue("ID")
            if (dataItem != null) 
            { 
                //var name = dataItem["ProductName"].Text; 
                //Literal1.Text += String.Format("{0}<br/>", name); 
            } 
        }
    }
}