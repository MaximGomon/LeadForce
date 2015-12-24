using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class SaveWorkflowTemplateConditionEvent : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        private Guid SiteId;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;
            SiteId = CurrentUser.Instance.SiteID;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            EnumHelper.EnumToDropDownList<WorkflowTemplateConditionEventCategory>(ref ddlCategory);

            ddlActivityType.Items.Add(new ListItem("Выберите значение", ""));
            foreach (var activityType in EnumHelper.EnumToList<ActivityType>())
                if (activityType != ActivityType.OpenLandingPage) // !!!
                    ddlActivityType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(activityType), ((int)activityType).ToString()));

            dcbSiteColumns.SiteID = SiteId;
            dcbSiteColumns.BindData();

            dbcSiteActivityScoreType.SiteID = SiteId;
            dbcSiteActivityScoreType.BindData();

            //lbtnSave.Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategory();
        }



        /// <summary>
        /// Binds the category.
        /// </summary>
        public void BindCategory()
        {
            pnlActivity.Visible = false;
            pnlColumnValue.Visible = false;
            pnlBehaviorScore.Visible = false;
            pnlFormula.Visible = false;

            if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
            {
                var selectedValue = ddlCategory.SelectedValue.ToEnum<WorkflowTemplateConditionEventCategory>();
                switch (selectedValue)
                {
                    case WorkflowTemplateConditionEventCategory.Activity:
                        pnlActivity.Visible = true;
                        break;
                    case WorkflowTemplateConditionEventCategory.ColumnValue:
                        pnlColumnValue.Visible = true;
                        pnlFormula.Visible = true;
                        break;
                    case WorkflowTemplateConditionEventCategory.BehaviorScore:
                        pnlBehaviorScore.Visible = true;
                        pnlFormula.Visible = true;
                        break;
                    case WorkflowTemplateConditionEventCategory.CharacteristicsScore:
                        pnlFormula.Visible = true;
                        break;
                }

                BindFormula();
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbSiteColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbSiteColumns_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindFormula();
        }



        /// <summary>
        /// Binds the formula.
        /// </summary>
        public void BindFormula()
        {
            var categoryValue = ddlCategory.SelectedValue;

            pnlValue.Visible = false;
            txtValue.Visible = false;
            reqValue.Visible = false;
            txtValueNumeric.Visible = false;
            reqValueNumeric.Visible = false;
            txtValueDate.Visible = false;
            reqValueDate.Visible = false;
            txtValueEnum.Visible = false;

            ddlFormula.Items.Clear();
            ddlFormula.Items.Add(new ListItem("Выберите значение", ""));

            if (!string.IsNullOrEmpty(categoryValue))
            {
                var category = (WorkflowTemplateConditionEventCategory)int.Parse(categoryValue);

                switch (category)
                {
                    case WorkflowTemplateConditionEventCategory.ColumnValue:
                        if (dcbSiteColumns.SelectedId != Guid.Empty)
                        {
                            var siteColumn = _dataManager.SiteColumns.SelectById(SiteId, dcbSiteColumns.SelectedId);
                            switch ((ColumnType)siteColumn.TypeID)
                            {
                                case ColumnType.String:
                                case ColumnType.Text:
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.StartWith), ((int)FormulaType.StartWith).ToString()));
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.Mask), ((int)FormulaType.Mask).ToString()));
                                    txtValue.Visible = true;
                                    reqValue.Visible = true;
                                    pnlValue.Visible = true;
                                    break;
                                case ColumnType.Number:
                                case ColumnType.Date:
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.Greater), ((int)FormulaType.Greater).ToString()));
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.Less), ((int)FormulaType.Less).ToString()));
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.GreaterOrEqual), ((int)FormulaType.GreaterOrEqual).ToString()));
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.LessOrEqual), ((int)FormulaType.LessOrEqual).ToString()));
                                    if ((ColumnType)siteColumn.TypeID == ColumnType.Number)
                                    {
                                        txtValueNumeric.Visible = true;
                                        reqValueNumeric.Visible = true;
                                    }
                                    if ((ColumnType)siteColumn.TypeID == ColumnType.Date)
                                    {
                                        txtValueDate.Visible = true;
                                        reqValueDate.Visible = true;
                                    }
                                    pnlValue.Visible = true;
                                    break;
                                case ColumnType.Enum:
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.NotEmpty), ((int)FormulaType.NotEmpty).ToString()));
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.Empty), ((int)FormulaType.Empty).ToString()));
                                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.SelectFromList), ((int)FormulaType.SelectFromList).ToString()));
                                    txtValueEnum.Filters = new List<DictionaryComboBox.DictionaryFilterColumn>();
                                    txtValueEnum.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn { Name = "SiteColumnID", DbType = DbType.Guid, Value = siteColumn.ID.ToString() });
                                    txtValueEnum.BindData();
                                    break;
                            }
                        }
                        break;
                    case WorkflowTemplateConditionEventCategory.BehaviorScore:
                    case WorkflowTemplateConditionEventCategory.CharacteristicsScore:
                        ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.Greater), ((int)FormulaType.Greater).ToString()));
                        ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.Less), ((int)FormulaType.Less).ToString()));
                        ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.GreaterOrEqual), ((int)FormulaType.GreaterOrEqual).ToString()));
                        ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(FormulaType.LessOrEqual), ((int)FormulaType.LessOrEqual).ToString()));
                        txtValueNumeric.Visible = true;
                        reqValueNumeric.Visible = true;
                        pnlValue.Visible = true;
                        break;
                }
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlFormula control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlFormula_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindEnumValue();
        }



        public void BindEnumValue()
        {
            if (!string.IsNullOrEmpty(ddlCategory.SelectedValue) && ddlCategory.SelectedValue.ToEnum<WorkflowTemplateConditionEventCategory>() == WorkflowTemplateConditionEventCategory.ColumnValue)
            {
                if (dcbSiteColumns.SelectedId != Guid.Empty)
                {
                    var siteColumn = _dataManager.SiteColumns.SelectById(SiteId, dcbSiteColumns.SelectedId);
                    if (siteColumn != null && (ColumnType)siteColumn.TypeID == ColumnType.Enum)
                    {
                        if (string.IsNullOrEmpty(ddlFormula.SelectedValue) || (FormulaType)int.Parse(ddlFormula.SelectedValue) != FormulaType.SelectFromList)
                        {
                            txtValueEnum.Visible = false;
                            pnlValue.Visible = false;
                        }
                        else
                        {
                            txtValueEnum.Visible = true;
                            pnlValue.Visible = true;
                        }
                    }
                }
            } 
        }



        /// <summary>
        /// Binds the codes.
        /// </summary>
        /// <param name="activityType">Type of the activity.</param>
        public void BindCodes(ActivityType activityType)
        {
            rcbCode.Items.Clear();
            rcbCode.Text = string.Empty;
            rcbCode.AllowCustomText = true;
            rcbCode.Filter = RadComboBoxFilter.None;
            rcbCode.AllowCustomText = false;

            pnlComboBoxCode.Visible = true;
            pnlTextBoxCode.Visible = false;

            switch (activityType)
            {
                case ActivityType.ViewPage:
                    rcbCode.Filter = RadComboBoxFilter.Contains;
                    var contactActivities = _dataManager.ContactActivity.Select(SiteId, null, activityType).Select(sua => new { sua.ActivityCode }).Distinct();
                    foreach (var contactActivity in contactActivities)
                        rcbCode.Items.Add(new RadComboBoxItem(HttpUtility.UrlDecode(contactActivity.ActivityCode)));
                    break;
                case ActivityType.Link:
                    rcbCode.DataSource = _dataManager.Links.SelectByRuleType(SiteId, new List<int>() { (int)RuleType.Link });
                    rcbCode.DataTextField = "Name";
                    rcbCode.DataValueField = "Code";
                    break;
                case ActivityType.OpenForm:
                case ActivityType.FillForm:
                case ActivityType.CancelForm:
                    rcbCode.DataSource = _dataManager.SiteActivityRules.SelectByRuleType(SiteId, new List<int>() { (int)RuleType.Form, (int)RuleType.ExternalForm, (int)RuleType.WufooForm });
                    rcbCode.DataTextField = "Name";
                    rcbCode.DataValueField = "Code";

                    pnlParameter.Visible = true;
                    break;
                case ActivityType.DownloadFile:
                    rcbCode.DataSource = _dataManager.Links.SelectByRuleType(SiteId, new List<int>() { (int)RuleType.File });
                    rcbCode.DataTextField = "Name";
                    rcbCode.DataValueField = "Code";
                    break;
                /*case ActivityType.Event:
                    rcbCode.DataSource = _dataManager.SiteEventTemplates.SelectAll(SiteId);
                    rcbCode.DataTextField = "Title";
                    rcbCode.DataValueField = "ID";
                    break;*/
                case ActivityType.UserEvent:
                    pnlComboBoxCode.Visible = false;
                    pnlTextBoxCode.Visible = true;
                    break;
                case ActivityType.Impact:
                    rcbCode.DataSource = _dataManager.SiteActionTemplate.SelectAll(SiteId);
                    rcbCode.DataTextField = "Title";
                    rcbCode.DataValueField = "ID";
                    break;
                case ActivityType.InboxMessage:
                    rcbCode.AllowCustomText = true;
                    break;
            }

            rcbCode.DataBind();

            if (activityType == ActivityType.ViewPage || activityType == ActivityType.InboxMessage)
                rcbCode.InputCssClass = string.Empty;
            else
                rcbCode.InputCssClass = "readonly";
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlActivityType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlActivityType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            pnlParameter.Visible = false;

            if (!string.IsNullOrEmpty(ddlActivityType.SelectedValue))
                BindCodes(ddlActivityType.SelectedValue.ToEnum<ActivityType>());
        }
    }
}