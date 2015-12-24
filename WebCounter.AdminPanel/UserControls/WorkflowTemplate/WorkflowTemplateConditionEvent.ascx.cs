using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class WorkflowTemplateConditionEvent : System.Web.UI.UserControl
    {
        private DataManager _dataManager;



        public List<WorkflowTemplateConditionEventMap> WorkflowTemplateConditionEventList
        {
            get
            {
                if (ViewState["WorkflowTemplateConditionEvents"] == null)
                    ViewState["WorkflowTemplateConditionEvents"] = new List<WorkflowTemplateConditionEventMap>();
                return (List<WorkflowTemplateConditionEventMap>)ViewState["WorkflowTemplateConditionEvents"];
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

            rgWorkflowTemplateConditionEvent.Culture = new CultureInfo("ru-RU");
    
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
                ViewState["WorkflowTemplateConditionEvents"] =
                    _dataManager.WorkflowTemplateConditionEvent.SelectAllByWorkflowTemplateId(WorkflowTemplateId).Select(
                        a =>
                        new WorkflowTemplateConditionEventMap
                            {
                                ID = a.ID,
                                WorkflowTemplateID = a.WorkflowTemplateID,
                                Category = a.Category,
                                ActivityType = a.ActivityType,
                                Code = a.Code,
                                ActualPeriod = a.ActualPeriod,
                                Requisite = a.Requisite,
                                Formula = a.Formula,
                                Value = a.Value
                            }).ToList();
            }
            else
                ViewState["WorkflowTemplateConditionEvents"] = new List<WorkflowTemplateConditionEventMap>();

            rgWorkflowTemplateConditionEvent.Rebind();
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgWorkflowTemplateConditionEvent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateConditionEvent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkflowTemplateConditionEvent.DataSource = ViewState["WorkflowTemplateConditionEvents"];
        }



        /// <summary>
        /// Handles the OnInsertCommand event of the rgWorkflowTemplateConditionEvent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateConditionEvent_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgWorkflowTemplateConditionEvent.MasterTableView.IsItemInserted = false;
            rgWorkflowTemplateConditionEvent.Rebind();
        }



        /// <summary>
        /// Handles the OnUpdateCommand event of the rgWorkflowTemplateConditionEvent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateConditionEvent_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Handles the OnDeleteCommand event of the rgWorkflowTemplateConditionEvent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateConditionEvent_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = (e.Item as GridDataItem).GetDataKeyValue("ID").ToString().ToGuid();
            ((List<WorkflowTemplateConditionEventMap>)ViewState["WorkflowTemplateConditionEvents"]).Remove(((List<WorkflowTemplateConditionEventMap>)ViewState["WorkflowTemplateConditionEvents"]).Where(s => s.ID == id).FirstOrDefault());
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgWorkflowTemplateConditionEvent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateConditionEvent_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (WorkflowTemplateConditionEventMap)e.Item.DataItem;
                ((Literal)item.FindControl("litCategory")).Text = EnumHelper.GetEnumDescription((WorkflowTemplateConditionEventCategory)data.Category);
                ((Literal)item.FindControl("litActivityType")).Text = data.ActivityType != null ? EnumHelper.GetEnumDescription((ActivityType)data.ActivityType) : "";
                if (data.ActivityType.HasValue)
                {
                    switch ((ActivityType)data.ActivityType)
                    {
                        case ActivityType.ViewPage:
                        case ActivityType.InboxMessage:
                        case ActivityType.ReturnMessage:
                        case ActivityType.UserEvent:
                            ((Literal)item.FindControl("litCode")).Text = data.Code;
                            break;
                        case ActivityType.OpenForm:
                        case ActivityType.FillForm:
                        case ActivityType.CancelForm:
                            var codeForm = Regex.Replace(data.Code, @"^(.*)(#(.*))$", "$1");
                            var codeParameter = string.Empty;
                            if (data.Code.Contains("#"))
                                codeParameter = Regex.Replace(data.Code, @"^(.*)#(.*)$", "$2");
                            var siteActivityRuleForm = _dataManager.SiteActivityRules.Select(CurrentUser.Instance.SiteID, codeForm);
                            if (siteActivityRuleForm != null)
                                ((Literal)item.FindControl("litCode")).Text = siteActivityRuleForm.Name + (!string.IsNullOrEmpty(codeParameter) ? "#" + codeParameter : "");
                            else
                                ((Literal) item.FindControl("litCode")).Text = string.Format("<span style=\"color:red\">{0}</span>", "Формы не существует");
                            break;
                        case ActivityType.Link:
                        case ActivityType.DownloadFile:
                            var siteActivityRule = _dataManager.Links.Select(CurrentUser.Instance.SiteID, data.Code);
                            ((Literal)item.FindControl("litCode")).Text = siteActivityRule.Name;
                            break;
                        /*case ActivityType.Event:
                            var siteEventTemplate = _dataManager.SiteEventTemplates.SelectById(data.Code.ToGuid());
                            ((Literal)item.FindControl("litCode")).Text = siteEventTemplate.Title;
                            break;*/
                        case ActivityType.Impact:
                            var siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(data.Code.ToGuid());
                            ((Literal)item.FindControl("litCode")).Text = siteActionTemplate.Title;
                            break;
                    }
                }
                ((Literal)item.FindControl("litFormula")).Text = data.Formula != null ? EnumHelper.GetEnumDescription((FormulaType)data.Formula) : "";
                switch ((WorkflowTemplateConditionEventCategory)data.Category)
                {
                    case WorkflowTemplateConditionEventCategory.ColumnValue:
                        var siteColumn = _dataManager.SiteColumns.SelectById(CurrentUser.Instance.SiteID, data.Requisite.ToGuid());
                        switch ((ColumnType)siteColumn.TypeID)
                        {
                            case ColumnType.String:
                            case ColumnType.Text:
                            case ColumnType.Number:
                                ((Literal)item.FindControl("litValue")).Text = data.Value;
                                break;
                            case ColumnType.Date:
                                ((Literal)item.FindControl("litValue")).Text = DateTime.Parse(data.Value).ToString("dd.MM.yyyy");
                                break;
                            case ColumnType.Enum:
                                if ((FormulaType)data.Formula == FormulaType.SelectFromList)
                                {
                                    var siteColumnValue = _dataManager.SiteColumnValues.SelectById(data.Value.ToGuid());
                                    ((Literal)item.FindControl("litValue")).Text = siteColumnValue.Value;
                                }
                                break;
                        }
                        break;
                    case WorkflowTemplateConditionEventCategory.BehaviorScore:
                        var siteActivityScoreType = _dataManager.SiteActivityScoreType.SelectById(CurrentUser.Instance.SiteID, data.Requisite.ToGuid());
                        ((Literal)item.FindControl("litRequisite")).Text = siteActivityScoreType.Title;
                        ((Literal)item.FindControl("litValue")).Text = data.Value;
                        break;
                    case WorkflowTemplateConditionEventCategory.CharacteristicsScore:
                        ((Literal)item.FindControl("litValue")).Text = data.Value;
                        break;
                }
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var userControl = (SaveWorkflowTemplateConditionEvent)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                userControl.BindData();

                var gridEditFormItem = (GridEditFormItem)e.Item;

                //var item = e.Item as GridEditableItem;
                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var conditionEvent = (WorkflowTemplateConditionEventMap)gridEditFormItem.DataItem;
                    ((DropDownList)userControl.FindControl("ddlCategory")).Items.FindByValue(conditionEvent.Category.ToString()).Selected = true;
                    userControl.BindCategory();
                    if ((WorkflowTemplateConditionEventCategory)conditionEvent.Category == WorkflowTemplateConditionEventCategory.Activity)
                    {
                        userControl.BindCodes((ActivityType)conditionEvent.ActivityType);
                        var rcbCode = (RadComboBox) userControl.FindControl("rcbCode");
                        if (conditionEvent.ActivityType == (int)ActivityType.ViewPage || conditionEvent.ActivityType == (int)ActivityType.InboxMessage)
                        {
                            rcbCode.SelectedIndex = rcbCode.Items.FindItemIndexByText(conditionEvent.Code);
                            if (rcbCode.SelectedIndex == -1)
                                rcbCode.Items.Insert(0, new RadComboBoxItem(conditionEvent.Code) { Selected = true });
                        }
                        else if (conditionEvent.ActivityType == (int)ActivityType.UserEvent)
                        {
                            ((TextBox) userControl.FindControl("txtCode")).Text = conditionEvent.Code;
                        }
                        else
                        {
                            if (conditionEvent.ActivityType == (int)ActivityType.OpenForm || conditionEvent.ActivityType == (int)ActivityType.FillForm || conditionEvent.ActivityType == (int)ActivityType.CancelForm)
                            {
                                rcbCode.SelectedIndex = rcbCode.Items.FindItemIndexByValue(Regex.Replace(conditionEvent.Code, @"^(.*)(#(.*))$", "$1"));
                                if (conditionEvent.Code.Contains("#"))
                                    ((TextBox)userControl.FindControl("txtParameter")).Text = Regex.Replace(conditionEvent.Code, @"^(.*)#(.*)$", "$2");
                            }
                            else
                            {
                                rcbCode.Items.FindItemIndexByValue(conditionEvent.Code);
                                ((TextBox)userControl.FindControl("txtParameter")).Text = string.Empty;
                            }
                        }
                            
                    }
                    ((DropDownList)userControl.FindControl("ddlActivityType")).Items.FindByValue(conditionEvent.ActivityType.HasValue ? conditionEvent.ActivityType.ToString() : "").Selected = true;
                    ((RadNumericTextBox)userControl.FindControl("txtActualPeriod")).Text = conditionEvent.ActualPeriod.HasValue ? conditionEvent.ActualPeriod.ToString() : "";
                    if ((WorkflowTemplateConditionEventCategory)conditionEvent.Category == WorkflowTemplateConditionEventCategory.ColumnValue)
                        ((DictionaryComboBox)userControl.FindControl("dcbSiteColumns")).SelectedId = !string.IsNullOrEmpty(conditionEvent.Requisite) ? conditionEvent.Requisite.ToGuid() : Guid.Empty;
                    if ((WorkflowTemplateConditionEventCategory)conditionEvent.Category == WorkflowTemplateConditionEventCategory.BehaviorScore)
                        ((DictionaryComboBox)userControl.FindControl("dbcSiteActivityScoreType")).SelectedId = !string.IsNullOrEmpty(conditionEvent.Requisite) ? conditionEvent.Requisite.ToGuid() : Guid.Empty;
                    userControl.BindFormula();
                    ((DropDownList)userControl.FindControl("ddlFormula")).Items.FindByValue(conditionEvent.Formula.HasValue ? conditionEvent.Formula.ToString() : "").Selected = true;
                    switch ((WorkflowTemplateConditionEventCategory)conditionEvent.Category)
                    {
                        case WorkflowTemplateConditionEventCategory.ColumnValue:
                            var siteColumn = _dataManager.SiteColumns.SelectById(CurrentUser.Instance.SiteID, conditionEvent.Requisite.ToGuid());
                            switch ((ColumnType)siteColumn.TypeID)
                            {
                                case ColumnType.String:
                                case ColumnType.Text:
                                    ((TextBox)userControl.FindControl("txtValue")).Text = conditionEvent.Value;
                                    break;
                                case ColumnType.Number:
                                    ((RadNumericTextBox)userControl.FindControl("txtValueNumeric")).Text = conditionEvent.Value;
                                    break;
                                case ColumnType.Date:
                                    ((RadDatePicker)userControl.FindControl("txtValueDate")).SelectedDate = DateTime.Parse(conditionEvent.Value);
                                    break;
                                case ColumnType.Enum:
                                    if ((FormulaType)conditionEvent.Formula == FormulaType.SelectFromList)
                                        ((DictionaryComboBox)userControl.FindControl("txtValueEnum")).SelectedId = conditionEvent.Value.ToGuid();
                                    break;
                            }
                            break;
                        case WorkflowTemplateConditionEventCategory.BehaviorScore:
                            ((DictionaryComboBox)userControl.FindControl("dbcSiteActivityScoreType")).SelectedId = conditionEvent.Requisite.ToGuid();
                            ((RadNumericTextBox)userControl.FindControl("txtValueNumeric")).Text = conditionEvent.Value;
                            break;
                        case WorkflowTemplateConditionEventCategory.CharacteristicsScore:
                            ((RadNumericTextBox)userControl.FindControl("txtValueNumeric")).Text = conditionEvent.Value;
                            break;
                    }
                    userControl.BindEnumValue();
                }
            }
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="workflowTemplateConditionEventId">The workflow template condition event id.</param>
        /// <param name="item">The item.</param>
        protected void SaveToViewState(Guid workflowTemplateConditionEventId, GridEditableItem item)
        {
            var userControl = (UserControl)item.FindControl(GridEditFormItem.EditFormUserControlID);

            var workflowTemplateConditionEvent = ((List<WorkflowTemplateConditionEventMap>)ViewState["WorkflowTemplateConditionEvents"]).Where(s => s.ID == workflowTemplateConditionEventId).FirstOrDefault() ?? new WorkflowTemplateConditionEventMap();
            workflowTemplateConditionEvent.Category = ((DropDownList)userControl.FindControl("ddlCategory")).SelectedValue.ToInt();
            workflowTemplateConditionEvent.ActivityType =
                !string.IsNullOrEmpty(((DropDownList)userControl.FindControl("ddlActivityType")).SelectedValue)
                    ? (int?)((DropDownList)userControl.FindControl("ddlActivityType")).SelectedValue.ToInt()
                    : null;

            if (workflowTemplateConditionEvent.ActivityType == (int)ActivityType.ViewPage
                || workflowTemplateConditionEvent.ActivityType == (int)ActivityType.InboxMessage)
                workflowTemplateConditionEvent.Code = ((RadComboBox)userControl.FindControl("rcbCode")).Text;
            else if (workflowTemplateConditionEvent.ActivityType == (int)ActivityType.UserEvent)
                workflowTemplateConditionEvent.Code = ((TextBox)userControl.FindControl("txtCode")).Text;
            else
            {
                if ((workflowTemplateConditionEvent.ActivityType == (int)ActivityType.OpenForm || workflowTemplateConditionEvent.ActivityType == (int)ActivityType.FillForm || workflowTemplateConditionEvent.ActivityType == (int)ActivityType.CancelForm)
                    && !string.IsNullOrEmpty(((TextBox)userControl.FindControl("txtParameter")).Text))
                {
                    workflowTemplateConditionEvent.Code = ((RadComboBox)userControl.FindControl("rcbCode")).SelectedValue + "#" + ((TextBox)userControl.FindControl("txtParameter")).Text;
                }
                else
                    workflowTemplateConditionEvent.Code = ((RadComboBox)userControl.FindControl("rcbCode")).SelectedValue;
                
            }
                

            workflowTemplateConditionEvent.ActualPeriod = !string.IsNullOrEmpty(((RadNumericTextBox)userControl.FindControl("txtActualPeriod")).Text)
                    ? (int?)((RadNumericTextBox)userControl.FindControl("txtActualPeriod")).Text.ToInt()
                    : null;
            workflowTemplateConditionEvent.Formula =
                !string.IsNullOrEmpty(((DropDownList)userControl.FindControl("ddlFormula")).SelectedValue)
                    ? (int?)((DropDownList)userControl.FindControl("ddlFormula")).SelectedValue.ToInt()
                    : null;

            switch ((WorkflowTemplateConditionEventCategory)workflowTemplateConditionEvent.Category)
            {
                case WorkflowTemplateConditionEventCategory.Activity:
                    workflowTemplateConditionEvent.Requisite = null;
                    workflowTemplateConditionEvent.Value = null;
                    break;
                case WorkflowTemplateConditionEventCategory.ColumnValue:
                    workflowTemplateConditionEvent.Requisite = ((DictionaryComboBox)userControl.FindControl("dcbSiteColumns")).SelectedId.ToString();
                    var siteColumn = _dataManager.SiteColumns.SelectById(CurrentUser.Instance.SiteID, workflowTemplateConditionEvent.Requisite.ToGuid());
                    switch ((ColumnType)siteColumn.TypeID)
                    {
                        case ColumnType.String:
                        case ColumnType.Text:
                            workflowTemplateConditionEvent.Value = ((TextBox)userControl.FindControl("txtValue")).Text;
                            break;
                        case ColumnType.Number:
                            workflowTemplateConditionEvent.Value = ((RadNumericTextBox)userControl.FindControl("txtValueNumeric")).Text;
                            break;
                        case ColumnType.Date:
                            workflowTemplateConditionEvent.Value = ((RadDatePicker)userControl.FindControl("txtValueDate")).SelectedDate.ToString();
                            break;
                        case ColumnType.Enum:
                            if ((FormulaType)workflowTemplateConditionEvent.Formula == FormulaType.SelectFromList)
                                workflowTemplateConditionEvent.Value = ((DictionaryComboBox)userControl.FindControl("txtValueEnum")).SelectedId.ToString();
                            else
                                workflowTemplateConditionEvent.Value = null;
                            break;
                    }
                    break;
                case WorkflowTemplateConditionEventCategory.BehaviorScore:
                    workflowTemplateConditionEvent.Requisite = ((DictionaryComboBox)userControl.FindControl("dbcSiteActivityScoreType")).SelectedId.ToString();
                    workflowTemplateConditionEvent.Value = ((RadNumericTextBox)userControl.FindControl("txtValueNumeric")).Text;
                    break;
                case WorkflowTemplateConditionEventCategory.CharacteristicsScore:
                    workflowTemplateConditionEvent.Value = ((RadNumericTextBox)userControl.FindControl("txtValueNumeric")).Text;
                    break;
            }

            if (workflowTemplateConditionEvent.ID == Guid.Empty)
            {
                workflowTemplateConditionEvent.ID = Guid.NewGuid();
                ((List<WorkflowTemplateConditionEventMap>)ViewState["WorkflowTemplateConditionEvents"]).Add(workflowTemplateConditionEvent);
            }     
        }



        /// <summary>
        /// Saves the specified workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(Guid workflowTemplateId)
        {
            _dataManager.WorkflowTemplateConditionEvent.Save(WorkflowTemplateConditionEventList, workflowTemplateId);
        }



        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {
            rgWorkflowTemplateConditionEvent.Rebind();
        }
    }
}