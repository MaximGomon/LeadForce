using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Controls;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel
{
    public partial class SiteEventTemplate : LeadForceBasePage
    {
        public Access access;
        private Guid _siteEventTemplateId;        

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Шаблоны событий - LeadForce";

            access = Access.Check();
            if (!access.Write)
                BtnUpdate.Visible = false;            

            string siteEventTemplateId = Page.RouteData.Values["ID"] as string;            

            hlCancel.NavigateUrl = UrlsData.AP_SiteEventTemplates();

            if (!string.IsNullOrEmpty(siteEventTemplateId))
            {
                Guid.TryParse(siteEventTemplateId, out _siteEventTemplateId);
            }            

            if (!Page.IsPostBack)
            {
                var logicConditions = DataManager.LogicConditions.SelectAll();
                foreach (var logicCondition in logicConditions)
                    ddlLogicCondition.Items.Add(new ListItem { Text = logicCondition.Title, Value = logicCondition.ID.ToString() });

                if (_siteEventTemplateId != Guid.Empty)
                {
                    ViewState["SiteEventTemplateActivity"] =
                        DataManager.SiteEventTemplateActivity.SelectAll(SiteId, _siteEventTemplateId).Select(
                            a =>
                            new SiteEventTemplateActivityMap
                                {
                                    ID = a.ID,
                                    SiteID = a.SiteID,
                                    SiteEventTemplateID = a.SiteEventTemplateID,
                                    EventCategoryID = a.EventCategoryID,
                                    ActivityTypeID = a.ActivityTypeID,
                                    ActivityCode = a.ActivityCode,
                                    ActualPeriod = a.ActualPeriod,
                                    Option = a.Option,
                                    FormulaID = a.FormulaID,
                                    Value = a.Value
                                }).ToList();

                    var siteEventTemplates = DataManager.SiteEventTemplates.SelectById(_siteEventTemplateId);
                    txtTitle.Text = siteEventTemplates.Title;                    
                    txtActionCount.Text = siteEventTemplates.ActionCount.ToString();
                    ddlLogicCondition.Items.FindByValue(siteEventTemplates.LogicConditionID.ToString()).Selected = true;
                    txtFrequencyPeriod.Text = siteEventTemplates.FrequencyPeriod.ToString();
                    if ((LogicConditionType)siteEventTemplates.LogicConditionID == LogicConditionType.NEvents)
                        spanActionCount.Visible = true;

                    ViewState["SiteEventActionTemplate"] =
                        DataManager.SiteEventActionTemplate.Select(SiteId, _siteEventTemplateId).Select(
                            a =>
                            new SiteEventActionTemplateMap
                                {
                                    ID = a.ID,
                                    SiteID = a.SiteID,
                                    SiteEventTemplateID = a.SiteEventTemplateID,
                                    SiteActionTemplateID = a.SiteActionTemplateID,
                                    StartAfter = a.StartAfter,
                                    StartAfterTypeID = a.StartAfterTypeID,
                                    MessageText = a.MessageText
                                }).ToList();

                    ViewState["SiteEventTemplateScore"] =
                        DataManager.SiteEventTemplateScore.Select(SiteId, _siteEventTemplateId).Select(
                            a => new SiteEventTemplateScoreMap
                                     {
                                         ID = a.ID,
                                         SiteID = a.SiteID,
                                         SiteEventTemplateID = a.SiteEventTemplateID,
                                         SiteActivityScoreTypeID = a.SiteActivityScoreTypeID,
                                         OperationID = a.OperationID,
                                         Score = a.Score
                                     }).ToList();

                    ViewState["SiteActionTemplateUserColumn"] =
                        DataManager.SiteActionTemplateUserColumn.SelectByEventTemplateId(SiteId, _siteEventTemplateId).
                            Select(
                                a =>
                                new SiteActionTemplateUserColumnMap
                                    {
                                        ID = a.ID,
                                        SiteID = a.SiteID,
                                        SiteEventTemplateID = a.SiteEventTemplateID,
                                        SiteColumnID = a.SiteColumnID,
                                        StringValue = a.StringValue,
                                        DateValue = a.DateValue,
                                        SiteColumnValueID = a.SiteColumnValueID
                                    }).ToList();
                }
                else
                {
                    ViewState["SiteEventTemplateActivity"] = new List<SiteEventTemplateActivityMap>();
                    ViewState["SiteEventActionTemplate"] = new List<SiteEventActionTemplateMap>();
                    ViewState["SiteEventTemplateScore"] = new List<SiteEventTemplateScoreMap>();
                    ViewState["SiteActionTemplateUserColumn"] = new List<SiteActionTemplateUserColumnMap>();
                }

                BindEventTemplateActivity();
                BindSiteEventActionTemplate();
                BindSiteEventTemplateScore();
                BindSiteActionTemplateUserColumn();
            }
        }



        /// <summary>
        /// Binds the event template activity.
        /// </summary>
        protected void BindEventTemplateActivity()
        {
            lvSiteEventTemplateActivity.DataSource = (List<SiteEventTemplateActivityMap>)ViewState["SiteEventTemplateActivity"];
            lvSiteEventTemplateActivity.DataBind();
        }



        /// <summary>
        /// Binds the site action.
        /// </summary>
        protected void BindSiteEventActionTemplate()
        {
            lvSiteEventActionTemplate.DataSource = (List<SiteEventActionTemplateMap>)ViewState["SiteEventActionTemplate"];
            lvSiteEventActionTemplate.DataBind();
        }



        /// <summary>
        /// Binds the site event template score.
        /// </summary>
        protected void BindSiteEventTemplateScore()
        {
            lvSiteEventTemplateScore.DataSource = (List<SiteEventTemplateScoreMap>)ViewState["SiteEventTemplateScore"];
            lvSiteEventTemplateScore.DataBind();
        }



        /// <summary>
        /// Binds the site action template user column.
        /// </summary>
        protected void BindSiteActionTemplateUserColumn()
        {
            lvSiteActionTemplateUserColumn.DataSource = (List<SiteActionTemplateUserColumnMap>)ViewState["SiteActionTemplateUserColumn"];
            lvSiteActionTemplateUserColumn.DataBind();
        }



        /// <summary>
        /// Handles the Click event of the BtnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            UpdateViewState();

            var siteEventTemplates = new tbl_SiteEventTemplates();

            if (_siteEventTemplateId != Guid.Empty)
            {
                siteEventTemplates = DataManager.SiteEventTemplates.SelectById(_siteEventTemplateId);
            }
            
            siteEventTemplates.SiteID = SiteId;
            siteEventTemplates.Title = txtTitle.Text;            
            siteEventTemplates.LogicConditionID = int.Parse(ddlLogicCondition.SelectedValue);
            siteEventTemplates.FrequencyPeriod = int.Parse(txtFrequencyPeriod.Text);
            if (!string.IsNullOrEmpty(txtActionCount.Text))
                siteEventTemplates.ActionCount = int.Parse(txtActionCount.Text);
            if (_siteEventTemplateId != Guid.Empty)
                DataManager.SiteEventTemplates.Update(siteEventTemplates);
            else
            {
                siteEventTemplates.OwnerID = CurrentUser.Instance.ContactID;
                DataManager.SiteEventTemplates.Add(siteEventTemplates);
            }

            var siteEventTemplateActivitiesOld = DataManager.SiteEventTemplateActivity.SelectAll(SiteId, _siteEventTemplateId);
            var siteEventTemplateActivities = (List<SiteEventTemplateActivityMap>)ViewState["SiteEventTemplateActivity"];
            var siteEventTemplateActivity = new SiteEventTemplateActivityMap();
            foreach (var item in siteEventTemplateActivities)
            {
                siteEventTemplateActivity = new SiteEventTemplateActivityMap();

                if (item.ID != Guid.Empty)
                    siteEventTemplateActivity.ID = item.ID;
                siteEventTemplateActivity.SiteID = SiteId;
                siteEventTemplateActivity.SiteEventTemplateID = siteEventTemplates.ID;
                siteEventTemplateActivity.EventCategoryID = item.EventCategoryID;
                switch ((EventCategory)item.EventCategoryID)
                {
                    case EventCategory.Action:
                        siteEventTemplateActivity.ActivityTypeID = item.ActivityTypeID;
                        siteEventTemplateActivity.ActivityCode = item.ActivityCode;
                        siteEventTemplateActivity.ActualPeriod = item.ActualPeriod;
                        break;
                    case EventCategory.ColumnValue:
                    case EventCategory.ScoreByType:
                        siteEventTemplateActivity.Option = item.Option;
                        siteEventTemplateActivity.FormulaID = item.FormulaID;
                        siteEventTemplateActivity.Value = item.Value;
                        break;
                    case EventCategory.ScoreByCharacteristics:
                        siteEventTemplateActivity.FormulaID = item.FormulaID;
                        siteEventTemplateActivity.Value = item.Value;
                        break;
                }

                var tblSiteEventTemplateActivity = new tbl_SiteEventTemplateActivity
                                                       {
                                                           ID = siteEventTemplateActivity.ID,
                                                           SiteID = siteEventTemplateActivity.SiteID,
                                                           SiteEventTemplateID = siteEventTemplateActivity.SiteEventTemplateID,
                                                           EventCategoryID = siteEventTemplateActivity.EventCategoryID,
                                                           ActivityTypeID = siteEventTemplateActivity.ActivityTypeID,
                                                           ActivityCode = siteEventTemplateActivity.ActivityCode,
                                                           ActualPeriod = siteEventTemplateActivity.ActualPeriod,
                                                           Option = siteEventTemplateActivity.Option,
                                                           FormulaID = siteEventTemplateActivity.FormulaID,
                                                           Value = siteEventTemplateActivity.Value
                                                       };

                var removeItemSiteEventTemplateActivity = siteEventTemplateActivitiesOld.SingleOrDefault(a => a.ID == siteEventTemplateActivity.ID);
                if (removeItemSiteEventTemplateActivity != null)
                {
                    DataManager.SiteEventTemplateActivity.Update(tblSiteEventTemplateActivity);
                    siteEventTemplateActivitiesOld.Remove(removeItemSiteEventTemplateActivity);
                }
                else
                {
                    DataManager.SiteEventTemplateActivity.Add(tblSiteEventTemplateActivity);
                }
            }

            if (siteEventTemplateActivitiesOld != null && siteEventTemplateActivitiesOld.Count > 0)
            {
                foreach (var item in siteEventTemplateActivitiesOld)
                    DataManager.SiteEventTemplateActivity.Delete(item);
            }

            // Actions
            var siteEventActionTemplateOld = DataManager.SiteEventActionTemplate.Select(SiteId, _siteEventTemplateId);
            var siteEventActionTemplates = (List<SiteEventActionTemplateMap>)ViewState["SiteEventActionTemplate"];
            var siteEventActionTemplate = new SiteEventActionTemplateMap();
            foreach (var item in siteEventActionTemplates)
            {
                siteEventActionTemplate = new SiteEventActionTemplateMap();

                if (item.ID != Guid.Empty)
                    siteEventActionTemplate.ID = item.ID;

                siteEventActionTemplate.SiteID = SiteId;
                siteEventActionTemplate.SiteEventTemplateID = siteEventTemplates.ID;
                siteEventActionTemplate.SiteActionTemplateID = item.SiteActionTemplateID;

                var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(((LeadForceBasePage)Page).SiteId, item.SiteActionTemplateID);
                if (siteActionTemplate != null && siteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.BaseTemplate)
                {
                    siteActionTemplate.UsageID = siteEventTemplates.ID;
                    DataManager.SiteActionTemplate.Update(siteActionTemplate);
                }

                siteEventActionTemplate.StartAfter = item.StartAfter;
                siteEventActionTemplate.StartAfterTypeID = item.StartAfterTypeID;
                siteEventActionTemplate.MessageText = item.MessageText;

                var tblSiteEventActionTemplate = new tbl_SiteEventActionTemplate
                {
                    ID = siteEventActionTemplate.ID,
                    SiteID = siteEventActionTemplate.SiteID,
                    SiteEventTemplateID = siteEventActionTemplate.SiteEventTemplateID,
                    SiteActionTemplateID = siteEventActionTemplate.SiteActionTemplateID,
                    StartAfter = siteEventActionTemplate.StartAfter,
                    StartAfterTypeID = siteEventActionTemplate.StartAfterTypeID,
                    MessageText = siteEventActionTemplate.MessageText
                };

                var removeSiteEventActionTemplate = siteEventActionTemplateOld.SingleOrDefault(a => a.ID == siteEventActionTemplate.ID);
                if (removeSiteEventActionTemplate != null)
                {
                    DataManager.SiteEventActionTemplate.Update(tblSiteEventActionTemplate);
                    siteEventActionTemplateOld.Remove(removeSiteEventActionTemplate);
                }
                else
                {
                    DataManager.SiteEventActionTemplate.Add(tblSiteEventActionTemplate);
                }
            }

            if (siteEventActionTemplateOld != null && siteEventActionTemplateOld.Count > 0)
            {
                foreach (var item in siteEventActionTemplateOld)
                    DataManager.SiteEventActionTemplate.Delete(item);
            }

            // Scores
            var SiteEventTemplateScoresOld = DataManager.SiteEventTemplateScore.Select(SiteId, _siteEventTemplateId);
            var SiteEventTemplateScores = (List<SiteEventTemplateScoreMap>)ViewState["SiteEventTemplateScore"];
            var SiteEventTemplateScore = new SiteEventTemplateScoreMap();
            foreach (var item in SiteEventTemplateScores)
            {
                SiteEventTemplateScore = new SiteEventTemplateScoreMap();

                if (item.ID != Guid.Empty)
                    SiteEventTemplateScore.ID = item.ID;
                SiteEventTemplateScore.SiteID = SiteId;
                SiteEventTemplateScore.SiteEventTemplateID = siteEventTemplates.ID;
                SiteEventTemplateScore.SiteActivityScoreTypeID = item.SiteActivityScoreTypeID;
                SiteEventTemplateScore.OperationID = item.OperationID;
                SiteEventTemplateScore.Score = item.Score;

                var tblSiteEventTemplateScore = new tbl_SiteEventTemplateScore
                {
                    ID = SiteEventTemplateScore.ID,
                    SiteID = SiteEventTemplateScore.SiteID,
                    SiteEventTemplateID = SiteEventTemplateScore.SiteEventTemplateID,
                    SiteActivityScoreTypeID = SiteEventTemplateScore.SiteActivityScoreTypeID,
                    OperationID = SiteEventTemplateScore.OperationID,
                    Score = SiteEventTemplateScore.Score
                };

                var removeItemSiteEventTemplateScore = SiteEventTemplateScoresOld.SingleOrDefault(a => a.ID == SiteEventTemplateScore.ID);
                if (removeItemSiteEventTemplateScore != null)
                {
                    DataManager.SiteEventTemplateScore.Update(tblSiteEventTemplateScore);
                    SiteEventTemplateScoresOld.Remove(removeItemSiteEventTemplateScore);
                }
                else
                {
                    DataManager.SiteEventTemplateScore.Add(tblSiteEventTemplateScore);
                }
            }

            if (SiteEventTemplateScoresOld != null && SiteEventTemplateScoresOld.Count > 0)
            {
                foreach (var item in SiteEventTemplateScoresOld)
                    DataManager.SiteEventTemplateScore.Delete(item);
            }

            // Columns
            var siteActionTemplateUserColumnOld = DataManager.SiteActionTemplateUserColumn.SelectByEventTemplateId(SiteId, _siteEventTemplateId);
            var siteActionTemplateUserColumns = (List<SiteActionTemplateUserColumnMap>)ViewState["SiteActionTemplateUserColumn"];
            var siteActionTemplateUserColumn = new SiteActionTemplateUserColumnMap();
            foreach (var item in siteActionTemplateUserColumns)
            {
                siteActionTemplateUserColumn = new SiteActionTemplateUserColumnMap();

                if (item.ID != Guid.Empty)
                    siteActionTemplateUserColumn.ID = item.ID;

                siteActionTemplateUserColumn.SiteID = SiteId;
                siteActionTemplateUserColumn.SiteEventTemplateID = _siteEventTemplateId;
                siteActionTemplateUserColumn.SiteColumnID = item.SiteColumnID;
                var siteColumn = DataManager.SiteColumns.SelectById(SiteId, item.SiteColumnID);
                switch ((ColumnType)siteColumn.TypeID)
                {
                    case ColumnType.String:
                    case ColumnType.Number:
                    case ColumnType.Text:
                        siteActionTemplateUserColumn.StringValue = item.StringValue;
                        break;
                    case ColumnType.Date:
                        siteActionTemplateUserColumn.DateValue = item.DateValue;
                        break;
                    case ColumnType.Enum:
                        siteActionTemplateUserColumn.SiteColumnValueID = item.SiteColumnValueID;
                        break;
                }

                var tblSiteActionTemplateUserColumn = new tbl_SiteActionTemplateUserColumn
                {
                    ID = siteActionTemplateUserColumn.ID,
                    SiteID = siteActionTemplateUserColumn.SiteID,
                    SiteEventTemplateID = siteActionTemplateUserColumn.SiteEventTemplateID,
                    SiteColumnID = siteActionTemplateUserColumn.SiteColumnID,
                    StringValue = siteActionTemplateUserColumn.StringValue,
                    DateValue = siteActionTemplateUserColumn.DateValue,
                    SiteColumnValueID = siteActionTemplateUserColumn.SiteColumnValueID
                };

                var removeSiteActionTemplateUserColumn = siteActionTemplateUserColumnOld.SingleOrDefault(a => a.ID == siteActionTemplateUserColumn.ID);
                if (removeSiteActionTemplateUserColumn != null)
                {
                    DataManager.SiteActionTemplateUserColumn.Update(tblSiteActionTemplateUserColumn);
                    siteActionTemplateUserColumnOld.Remove(removeSiteActionTemplateUserColumn);
                }
                else
                {
                    DataManager.SiteActionTemplateUserColumn.Add(tblSiteActionTemplateUserColumn);
                }
            }

            if (siteActionTemplateUserColumnOld != null && siteActionTemplateUserColumnOld.Count > 0)
            {
                foreach (var item in siteActionTemplateUserColumnOld)
                    DataManager.SiteActionTemplateUserColumn.Delete(item);
            }

            Response.Redirect(UrlsData.AP_SiteEventTemplates());
        }



        /// <summary>
        /// Handles the ItemCreated event of the lvSiteEventTemplateActivity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewItemEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventTemplateActivity_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem || e.Item.ItemType == ListViewItemType.InsertItem)
            {                
                var eventCategories = DataManager.EventCategories.SelectAll();
                foreach (var eventCategory in eventCategories)
                    ((DropDownList)e.Item.FindControl("ddlEventCategories")).Items.Add(new ListItem { Text = eventCategory.Title, Value = eventCategory.ID.ToString() });

                var activityTypes = DataManager.ActivityTypes.SelectAll();
                foreach (var activityType in activityTypes)
                    ((DropDownList)e.Item.FindControl("ddlActivityTypes")).Items.Add(new ListItem { Text = activityType.Title, Value = activityType.ID.ToString() });

                var siteColumns = DataManager.SiteColumns.SelectAll(SiteId);
                foreach (var siteColumn in siteColumns)
                    ((DropDownList)e.Item.FindControl("ddlSiteColumns")).Items.Add(new ListItem { Text = siteColumn.Name, Value = siteColumn.ID.ToString() });

                /*var formulaTypes = dataManager.Formula.Select(ColumnType.String);
                foreach (var formula in formulaTypes)
                    ((DropDownList)e.Item.FindControl("ddlFormulaSiteColumns")).Items.Add(new ListItem { Text = formula.Title, Value = formula.ID.ToString() });*/

                var formulaTypes = DataManager.Formula.Select(ColumnType.Number);
                foreach (var formula in formulaTypes)
                    ((DropDownList)e.Item.FindControl("ddlFormulaScoreByType")).Items.Add(new ListItem { Text = formula.Title, Value = formula.ID.ToString() });

                formulaTypes = DataManager.Formula.Select(ColumnType.Number);
                foreach (var formula in formulaTypes)
                    ((DropDownList)e.Item.FindControl("ddlFormulaScoreByCharacteristics")).Items.Add(new ListItem { Text = formula.Title, Value = formula.ID.ToString() });

                var siteActivityScoreTypes = DataManager.SiteActivityScoreType.SelectAll(SiteId);
                foreach (var siteActivityScoreType in siteActivityScoreTypes)
                    ((DropDownList)e.Item.FindControl("ddlSiteActivityScoreType")).Items.Add(new ListItem { Text = siteActivityScoreType.Title, Value = siteActivityScoreType.ID.ToString() });

                ((DropDownList)e.Item.FindControl("ddlEventCategories")).SelectedIndexChanged += new EventHandler(ddlEventCategories_SelectedIndexChanged);
            }
        }



        /// <summary>
        /// Handles the ItemDataBound event of the lvSiteEventTemplateActivity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewItemEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventTemplateActivity_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (SiteEventTemplateActivityMap)e.Item.DataItem;

                if (item != null)
                {                    
                    var formulaTypes = new List<tbl_Formula>();

                    switch ((EventCategory)item.EventCategoryID)
                    {
                        case EventCategory.Action:
                            e.Item.FindControl("spanAction").Visible = true;
                            var rcbActivityCode = (RadComboBox)e.Item.FindControl("rcbActivityCode");
                            BindActivityCodes(rcbActivityCode, (ActivityType)item.ActivityTypeID);

                            if (item.ActivityTypeID == (int)ActivityType.ViewPage || item.ActivityTypeID == (int)ActivityType.InboxMessage)
                            {
                                rcbActivityCode.SelectedIndex = rcbActivityCode.Items.FindItemIndexByText(item.ActivityCode);
                                if (rcbActivityCode.SelectedIndex == -1)                                
                                    rcbActivityCode.Items.Insert(0, new RadComboBoxItem(item.ActivityCode) { Selected = true});                                
                            }
                            else
                                rcbActivityCode.SelectedIndex = rcbActivityCode.Items.FindItemIndexByValue(item.ActivityCode);
                            break;
                        case EventCategory.ColumnValue:
                            var siteColumn = DataManager.SiteColumns.SelectById(SiteId, Guid.Parse(item.Option));

                            if (siteColumn != null)
                            {
                                switch ((ColumnType)siteColumn.tbl_ColumnTypes.ID)
                                {
                                    case ColumnType.String:
                                    case ColumnType.Text:
                                        formulaTypes = DataManager.Formula.Select(ColumnType.String);
                                        ((RadTextBox)e.Item.FindControl("txtValueStringSiteColumns")).Text = item.Value;
                                        e.Item.FindControl("txtValueStringSiteColumns").Visible = true;
                                        e.Item.FindControl("rfvValueStringSiteColumns").Visible = true;
                                        if (e.Item.FindControl("rfvValueStringSiteColumns2") != null)
                                            e.Item.FindControl("rfvValueStringSiteColumns2").Visible = true;
                                        break;
                                    case ColumnType.Date:
                                        formulaTypes = DataManager.Formula.Select(ColumnType.Date);
                                        ((RadDatePicker)e.Item.FindControl("txtValueDateSiteColumns")).SelectedDate = DateTime.Parse(item.Value);
                                        e.Item.FindControl("txtValueDateSiteColumns").Visible = true;
                                        e.Item.FindControl("rfvValueDateSiteColumns").Visible = true;
                                        if (e.Item.FindControl("rfvValueDateSiteColumns2") != null)
                                            e.Item.FindControl("rfvValueDateSiteColumns2").Visible = true;
                                        break;
                                    case ColumnType.Enum:
                                        formulaTypes = DataManager.Formula.Select(ColumnType.Enum);
                                        var siteColumnValues = DataManager.SiteColumnValues.SelectAll(siteColumn.ID);
                                        foreach (var siteColumnValue in siteColumnValues)
                                            ((DropDownList)e.Item.FindControl("ddlSiteColumnValues")).Items.Add(new ListItem { Text = siteColumnValue.Value, Value = siteColumnValue.ID.ToString() });

                                        if ((FormulaType)item.FormulaID == FormulaType.SelectFromList)
                                        {
                                            ((DropDownList)e.Item.FindControl("ddlSiteColumnValues")).Items.FindByValue(item.Value).Selected = true;
                                            e.Item.FindControl("ddlSiteColumnValues").Visible = true;
                                            e.Item.FindControl("rfvSiteColumnValues").Visible = true;
                                            if (e.Item.FindControl("rfvSiteColumnValues2") != null)
                                                e.Item.FindControl("rfvSiteColumnValues2").Visible = true;
                                        }
                                        break;
                                    case ColumnType.Number:
                                        formulaTypes = DataManager.Formula.Select(ColumnType.Number);
                                        ((RadNumericTextBox)e.Item.FindControl("txtValueNumberSiteColumns")).Text = item.Value;
                                        e.Item.FindControl("txtValueNumberSiteColumns").Visible = true;
                                        e.Item.FindControl("rfvValueNumberSiteColumns").Visible = true;
                                        if (e.Item.FindControl("rfvValueNumberSiteColumns2") != null)
                                            e.Item.FindControl("rfvValueNumberSiteColumns2").Visible = true;
                                        break;
                                }
                            }

                            foreach (var formula in formulaTypes)
                                ((DropDownList)e.Item.FindControl("ddlFormulaSiteColumns")).Items.Add(new ListItem { Text = formula.Title, Value = formula.ID.ToString() });

                            ((DropDownList)e.Item.FindControl("ddlSiteColumns")).Items.FindByValue(item.Option).Selected = true;
                            ((DropDownList)e.Item.FindControl("ddlFormulaSiteColumns")).Items.FindByValue(item.FormulaID.ToString()).Selected = true;

                            e.Item.FindControl("ddlFormulaSiteColumns").Visible = true;
                            e.Item.FindControl("rfvFormulaSiteColumns").Visible = true;
                            if (e.Item.FindControl("rfvFormulaSiteColumns2") != null)
                                e.Item.FindControl("rfvFormulaSiteColumns2").Visible = true;
                            e.Item.FindControl("spanColumnValue").Visible = true;
                            break;
                        case EventCategory.ScoreByType:
                            ((DropDownList)e.Item.FindControl("ddlSiteActivityScoreType")).Items.FindByValue(item.Option).Selected = true;
                            ((DropDownList)e.Item.FindControl("ddlFormulaScoreByType")).Items.FindByValue(item.FormulaID.ToString()).Selected = true;
                            ((RadNumericTextBox)e.Item.FindControl("txtValueScoreByType")).Text = item.Value;
                            e.Item.FindControl("spanScoreByType").Visible = true;
                            break;
                        case EventCategory.ScoreByCharacteristics:
                            ((DropDownList)e.Item.FindControl("ddlFormulaScoreByCharacteristics")).Items.FindByValue(item.FormulaID.ToString()).Selected = true;
                            ((RadNumericTextBox)e.Item.FindControl("txtValueScoreByCharacteristics")).Text = item.Value;
                            e.Item.FindControl("spanScoreByCharacteristics").Visible = true;
                            break;
                    }
                }
            }
        }



        /// <summary>
        /// Handles the ItemInserting event of the lvSiteEventTemplateActivity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewInsertEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventTemplateActivity_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            var siteEventTemplateActivity = (List<SiteEventTemplateActivityMap>)ViewState["SiteEventTemplateActivity"];
            var newSiteEventTemplateActivity = new SiteEventTemplateActivityMap();
            newSiteEventTemplateActivity.SiteID = SiteId;
            newSiteEventTemplateActivity.EventCategoryID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlEventCategories")).SelectedValue);
            switch ((EventCategory)newSiteEventTemplateActivity.EventCategoryID)
            {
                case EventCategory.Action:
                    newSiteEventTemplateActivity.ActivityTypeID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlActivityTypes")).SelectedValue);

                    if (newSiteEventTemplateActivity.ActivityTypeID == (int)ActivityType.ViewPage || newSiteEventTemplateActivity.ActivityTypeID == (int)ActivityType.InboxMessage)
                        newSiteEventTemplateActivity.ActivityCode = ((RadComboBox)lvSiteEventTemplateActivity.InsertItem.FindControl("rcbActivityCode")).Text;
                    else
                        newSiteEventTemplateActivity.ActivityCode = ((RadComboBox)lvSiteEventTemplateActivity.InsertItem.FindControl("rcbActivityCode")).SelectedValue;

                    newSiteEventTemplateActivity.ActualPeriod = int.Parse(((RadNumericTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtActualPeriod")).Text);
                    break;
                case EventCategory.ColumnValue:
                    newSiteEventTemplateActivity.Option = ((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlSiteColumns")).SelectedValue;
                    newSiteEventTemplateActivity.FormulaID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlFormulaSiteColumns")).SelectedValue);
                    if (lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueStringSiteColumns").Visible)
                        newSiteEventTemplateActivity.Value = ((RadTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueStringSiteColumns")).Text;
                    if (lvSiteEventTemplateActivity.InsertItem.FindControl("ddlSiteColumnValues").Visible)
                        newSiteEventTemplateActivity.Value = ((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlSiteColumnValues")).SelectedValue;
                    if (lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueDateSiteColumns").Visible)
                        newSiteEventTemplateActivity.Value = ((RadDatePicker)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueDateSiteColumns")).SelectedDate.ToString();
                    if (lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueNumberSiteColumns").Visible)
                        newSiteEventTemplateActivity.Value = ((RadNumericTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueNumberSiteColumns")).Text;
                    break;
                case EventCategory.ScoreByType:
                    newSiteEventTemplateActivity.Option = ((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlSiteActivityScoreType")).SelectedValue;
                    newSiteEventTemplateActivity.FormulaID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlFormulaScoreByType")).SelectedValue);
                    newSiteEventTemplateActivity.Value = ((RadNumericTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueScoreByType")).Text;
                    break;
                case EventCategory.ScoreByCharacteristics:
                    newSiteEventTemplateActivity.FormulaID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlFormulaScoreByCharacteristics")).SelectedValue);
                    newSiteEventTemplateActivity.Value = ((RadNumericTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueScoreByCharacteristics")).Text;
                    break;
            }

            siteEventTemplateActivity.Add(newSiteEventTemplateActivity);
            ViewState["SiteEventTemplateActivity"] = siteEventTemplateActivity;

            BindEventTemplateActivity();
        }



        protected void lvSiteEventTemplateActivity_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var siteEventTemplateActivity = (List<SiteEventTemplateActivityMap>)ViewState["SiteEventTemplateActivity"];
            siteEventTemplateActivity.RemoveAt(e.ItemIndex);
            ViewState["SiteEventTemplateActivity"] = siteEventTemplateActivity;
            BindEventTemplateActivity();
        }



        /// <summary>
        /// Updates the state of the view.
        /// </summary>
        protected void UpdateViewState()
        {
            var siteEventTemplateActivity = (List<SiteEventTemplateActivityMap>)ViewState["SiteEventTemplateActivity"];
            int lastIndex = -1;

            if (lvSiteEventTemplateActivity != null)//&& lvSiteEventTemplateActivity.Items.Count > 0
            {
                foreach (var item in lvSiteEventTemplateActivity.Items)
                {
                    if (item.ItemType == ListViewItemType.DataItem)
                    {
                        siteEventTemplateActivity[item.DataItemIndex].SiteID = SiteId;
                        siteEventTemplateActivity[item.DataItemIndex].EventCategoryID = int.Parse(((DropDownList)item.FindControl("ddlEventCategories")).SelectedValue);
                        switch ((EventCategory)siteEventTemplateActivity[item.DataItemIndex].EventCategoryID)
                        {
                            case EventCategory.Action:
                                siteEventTemplateActivity[item.DataItemIndex].ActivityTypeID = int.Parse(((DropDownList)item.FindControl("ddlActivityTypes")).SelectedValue);

                                if (siteEventTemplateActivity[item.DataItemIndex].ActivityTypeID == (int)ActivityType.ViewPage || siteEventTemplateActivity[item.DataItemIndex].ActivityTypeID == (int)ActivityType.InboxMessage)
                                    siteEventTemplateActivity[item.DataItemIndex].ActivityCode = ((RadComboBox)item.FindControl("rcbActivityCode")).Text;
                                else
                                    siteEventTemplateActivity[item.DataItemIndex].ActivityCode = ((RadComboBox)item.FindControl("rcbActivityCode")).SelectedValue;
                                
                                siteEventTemplateActivity[item.DataItemIndex].ActualPeriod = int.Parse(((RadNumericTextBox)item.FindControl("txtActualPeriod")).Text);
                                break;
                            case EventCategory.ColumnValue:
                                siteEventTemplateActivity[item.DataItemIndex].Option = ((DropDownList)item.FindControl("ddlSiteColumns")).SelectedValue;
                                siteEventTemplateActivity[item.DataItemIndex].FormulaID = int.Parse(((DropDownList)item.FindControl("ddlFormulaSiteColumns")).SelectedValue);
                                if (item.FindControl("txtValueStringSiteColumns").Visible)
                                    siteEventTemplateActivity[item.DataItemIndex].Value = ((RadTextBox)item.FindControl("txtValueStringSiteColumns")).Text;
                                if (item.FindControl("ddlSiteColumnValues").Visible)
                                    siteEventTemplateActivity[item.DataItemIndex].Value = ((DropDownList)item.FindControl("ddlSiteColumnValues")).SelectedValue;
                                if (item.FindControl("txtValueDateSiteColumns").Visible)
                                    siteEventTemplateActivity[item.DataItemIndex].Value = ((RadDatePicker)item.FindControl("txtValueDateSiteColumns")).SelectedDate.ToString();
                                if (item.FindControl("txtValueNumberSiteColumns").Visible)
                                    siteEventTemplateActivity[item.DataItemIndex].Value = ((RadNumericTextBox)item.FindControl("txtValueNumberSiteColumns")).Text;
                                break;
                            case EventCategory.ScoreByType:
                                siteEventTemplateActivity[item.DataItemIndex].Option = ((DropDownList)item.FindControl("ddlSiteActivityScoreType")).SelectedValue;
                                siteEventTemplateActivity[item.DataItemIndex].FormulaID = int.Parse(((DropDownList)item.FindControl("ddlFormulaScoreByType")).SelectedValue);
                                siteEventTemplateActivity[item.DataItemIndex].Value = ((RadNumericTextBox)item.FindControl("txtValueScoreByType")).Text;
                                break;
                            case EventCategory.ScoreByCharacteristics:
                                siteEventTemplateActivity[item.DataItemIndex].FormulaID = int.Parse(((DropDownList)item.FindControl("ddlFormulaScoreByCharacteristics")).SelectedValue);
                                siteEventTemplateActivity[item.DataItemIndex].Value = ((RadNumericTextBox)item.FindControl("txtValueScoreByCharacteristics")).Text;
                                break;
                        }
                    }
                    lastIndex = item.DataItemIndex;
                }

                
                if (!string.IsNullOrEmpty(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlEventCategories")).SelectedValue))
                {
                    lastIndex++;
                    siteEventTemplateActivity.Add(new SiteEventTemplateActivityMap());
                    siteEventTemplateActivity[lastIndex].SiteID = SiteId;
                    siteEventTemplateActivity[lastIndex].EventCategoryID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlEventCategories")).SelectedValue);
                    switch ((EventCategory)siteEventTemplateActivity[lastIndex].EventCategoryID)
                    {
                        case EventCategory.Action:
                            siteEventTemplateActivity[lastIndex].ActivityTypeID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlActivityTypes")).SelectedValue);

                            if (siteEventTemplateActivity[lastIndex].ActivityTypeID == (int)ActivityType.ViewPage || siteEventTemplateActivity[lastIndex].ActivityTypeID == (int)ActivityType.InboxMessage)
                                siteEventTemplateActivity[lastIndex].ActivityCode = ((RadComboBox)lvSiteEventTemplateActivity.InsertItem.FindControl("rcbActivityCode")).Text;
                            else
                                siteEventTemplateActivity[lastIndex].ActivityCode = ((RadComboBox)lvSiteEventTemplateActivity.InsertItem.FindControl("rcbActivityCode")).SelectedValue;

                            siteEventTemplateActivity[lastIndex].ActualPeriod = int.Parse(((RadNumericTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtActualPeriod")).Text);
                            break;
                        case EventCategory.ColumnValue:
                            siteEventTemplateActivity[lastIndex].Option = ((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlSiteColumns")).SelectedValue;
                            siteEventTemplateActivity[lastIndex].FormulaID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlFormulaSiteColumns")).SelectedValue);
                            if (lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueStringSiteColumns").Visible)
                                siteEventTemplateActivity[lastIndex].Value = ((RadTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueStringSiteColumns")).Text;
                            if (lvSiteEventTemplateActivity.InsertItem.FindControl("ddlSiteColumnValues").Visible)
                                siteEventTemplateActivity[lastIndex].Value = ((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlSiteColumnValues")).SelectedValue;
                            if (lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueDateSiteColumns").Visible)
                                siteEventTemplateActivity[lastIndex].Value = ((RadDatePicker)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueDateSiteColumns")).SelectedDate.ToString();
                            if (lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueNumberSiteColumns").Visible)
                                siteEventTemplateActivity[lastIndex].Value = ((RadNumericTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueNumberSiteColumns")).Text;
                            break;
                        case EventCategory.ScoreByType:
                            siteEventTemplateActivity[lastIndex].Option = ((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlSiteActivityScoreType")).SelectedValue;
                            siteEventTemplateActivity[lastIndex].FormulaID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlFormulaScoreByType")).SelectedValue);
                            siteEventTemplateActivity[lastIndex].Value = ((RadNumericTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueScoreByType")).Text;
                            break;
                        case EventCategory.ScoreByCharacteristics:
                            siteEventTemplateActivity[lastIndex].FormulaID = int.Parse(((DropDownList)lvSiteEventTemplateActivity.InsertItem.FindControl("ddlFormulaScoreByCharacteristics")).SelectedValue);
                            siteEventTemplateActivity[lastIndex].Value = ((RadNumericTextBox)lvSiteEventTemplateActivity.InsertItem.FindControl("txtValueScoreByCharacteristics")).Text;
                            break;
                    }
                }
            }

            ViewState["SiteEventTemplateActivity"] = siteEventTemplateActivity;

            // Actions
            var siteEventActionTemplates = (List<SiteEventActionTemplateMap>)ViewState["SiteEventActionTemplate"];
            if (lvSiteEventActionTemplate != null)
            {
                lastIndex = -1;
                foreach (var item in lvSiteEventActionTemplate.Items)
                {
                    if (item.ItemType == ListViewItemType.DataItem)
                    {
                        //siteEventActionTemplates[item.DataItemIndex].SiteActionTemplateID = Guid.Parse(((DropDownList)item.FindControl("ddlSiteActionTemplates")).SelectedValue);
                        siteEventActionTemplates[item.DataItemIndex].SiteActionTemplateID = ((UserControls.SiteActionTemplate)item.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId;
                        siteEventActionTemplates[item.DataItemIndex].StartAfter = int.Parse(((RadNumericTextBox)item.FindControl("txtStartAfter")).Text);
                        siteEventActionTemplates[item.DataItemIndex].StartAfterTypeID = int.Parse(((DropDownList)item.FindControl("ddlStartAfterType")).SelectedValue);                        
                        lastIndex = item.DataItemIndex;
                    }
                }

                //if (!string.IsNullOrEmpty(((DropDownList)lvSiteEventActionTemplate.InsertItem.FindControl("ddlSiteActionTemplates")).SelectedValue))
                if (((UserControls.SiteActionTemplate)lvSiteEventActionTemplate.InsertItem.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId != Guid.Empty)
                {
                    lastIndex++;
                    siteEventActionTemplates.Add(new SiteEventActionTemplateMap());
                    //siteEventActionTemplates[lastIndex].SiteActionTemplateID = Guid.Parse(((DropDownList)lvSiteEventActionTemplate.InsertItem.FindControl("ddlSiteActionTemplates")).SelectedValue);
                    siteEventActionTemplates[lastIndex].SiteActionTemplateID = ((UserControls.SiteActionTemplate)lvSiteEventActionTemplate.InsertItem.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId;
                    siteEventActionTemplates[lastIndex].StartAfter = int.Parse(((RadNumericTextBox)lvSiteEventActionTemplate.InsertItem.FindControl("txtStartAfter")).Text);
                    siteEventActionTemplates[lastIndex].StartAfterTypeID = int.Parse(((DropDownList)lvSiteEventActionTemplate.InsertItem.FindControl("ddlStartAfterType")).SelectedValue);                    
                }
            }

            ViewState["SiteEventActionTemplate"] = siteEventActionTemplates;

            //Scores
            var siteEventTemplateScore = (List<SiteEventTemplateScoreMap>)ViewState["SiteEventTemplateScore"];
            if (lvSiteEventTemplateScore != null)
            {
                lastIndex = -1;
                foreach (var item in lvSiteEventTemplateScore.Items)
                {
                    if (item.ItemType == ListViewItemType.DataItem)
                    {
                        siteEventTemplateScore[item.DataItemIndex].SiteActivityScoreTypeID = Guid.Parse(((DropDownList)item.FindControl("ddlSiteActivityScoreType")).SelectedValue);
                        siteEventTemplateScore[item.DataItemIndex].OperationID = int.Parse(((DropDownList)item.FindControl("ddlOperation")).SelectedValue);
                        siteEventTemplateScore[item.DataItemIndex].Score = int.Parse(((RadNumericTextBox)item.FindControl("txtScore")).Text);
                        lastIndex = item.DataItemIndex;
                    }
                }

                if (!string.IsNullOrEmpty(((DropDownList)lvSiteEventTemplateScore.InsertItem.FindControl("ddlSiteActivityScoreType")).SelectedValue))
                {
                    lastIndex++;
                    siteEventTemplateScore.Add(new SiteEventTemplateScoreMap());
                    siteEventTemplateScore[lastIndex].SiteActivityScoreTypeID = Guid.Parse(((DropDownList)lvSiteEventTemplateScore.InsertItem.FindControl("ddlSiteActivityScoreType")).SelectedValue);
                    siteEventTemplateScore[lastIndex].OperationID = int.Parse(((DropDownList)lvSiteEventTemplateScore.InsertItem.FindControl("ddlOperation")).SelectedValue);
                    siteEventTemplateScore[lastIndex].Score = int.Parse(((RadNumericTextBox)lvSiteEventTemplateScore.InsertItem.FindControl("txtScore")).Text);
                }
            }

            ViewState["SiteEventTemplateScore"] = siteEventTemplateScore;

            // Columns
            var siteActionTemplateUserColumn = (List<SiteActionTemplateUserColumnMap>)ViewState["SiteActionTemplateUserColumn"];
            if (lvSiteActionTemplateUserColumn != null)
            {
                lastIndex = -1;
                foreach (var item in lvSiteActionTemplateUserColumn.Items)
                {
                    if (item.ItemType == ListViewItemType.DataItem)
                    {
                        siteActionTemplateUserColumn[item.DataItemIndex].SiteEventTemplateID = _siteEventTemplateId;
                        siteActionTemplateUserColumn[item.DataItemIndex].SiteColumnID = Guid.Parse(((DropDownList)item.FindControl("ddlSiteColumns")).SelectedValue);
                        var siteColumn = DataManager.SiteColumns.SelectById(SiteId, Guid.Parse(((DropDownList)item.FindControl("ddlSiteColumns")).SelectedValue));
                        switch ((ColumnType)siteColumn.TypeID)
                        {
                            case ColumnType.Number:
                                siteActionTemplateUserColumn[item.DataItemIndex].StringValue = ((RadNumericTextBox)item.FindControl("txtValueNumber")).Text;
                                break;
                            case ColumnType.String:
                            case ColumnType.Text:
                                siteActionTemplateUserColumn[item.DataItemIndex].StringValue = ((RadTextBox)item.FindControl("txtValue")).Text;
                                break;
                            case ColumnType.Date:
                                siteActionTemplateUserColumn[item.DataItemIndex].DateValue = (DateTime)(((RadDatePicker)item.FindControl("txtValueDate")).SelectedDate);
                                break;
                            case ColumnType.Enum:
                                siteActionTemplateUserColumn[item.DataItemIndex].SiteColumnValueID = Guid.Parse(((DropDownList)item.FindControl("ddlSiteColumnValues")).SelectedValue);
                                break;
                        }
                        lastIndex = item.DataItemIndex;
                    }
                }

                if (!string.IsNullOrEmpty(((DropDownList)lvSiteActionTemplateUserColumn.InsertItem.FindControl("ddlSiteColumns")).SelectedValue))
                {
                    lastIndex++;
                    siteActionTemplateUserColumn.Add(new SiteActionTemplateUserColumnMap());
                    siteActionTemplateUserColumn[lastIndex].SiteEventTemplateID = _siteEventTemplateId;
                    siteActionTemplateUserColumn[lastIndex].SiteColumnID = Guid.Parse(((DropDownList)lvSiteActionTemplateUserColumn.InsertItem.FindControl("ddlSiteColumns")).SelectedValue);
                    var siteColumn = DataManager.SiteColumns.SelectById(SiteId, Guid.Parse(((DropDownList)lvSiteActionTemplateUserColumn.InsertItem.FindControl("ddlSiteColumns")).SelectedValue));
                    switch ((ColumnType)siteColumn.TypeID)
                    {
                        case ColumnType.Number:
                            siteActionTemplateUserColumn[lastIndex].StringValue = ((RadNumericTextBox)lvSiteActionTemplateUserColumn.InsertItem.FindControl("txtValueNumber")).Text;
                            break;
                        case ColumnType.String:
                        case ColumnType.Text:
                            siteActionTemplateUserColumn[lastIndex].StringValue = ((RadTextBox)lvSiteActionTemplateUserColumn.InsertItem.FindControl("txtValue")).Text;
                            break;
                        case ColumnType.Date:
                            siteActionTemplateUserColumn[lastIndex].DateValue = (DateTime)(((RadDatePicker)lvSiteActionTemplateUserColumn.InsertItem.FindControl("txtValueDate")).SelectedDate);
                            break;
                        case ColumnType.Enum:
                            siteActionTemplateUserColumn[lastIndex].SiteColumnValueID = Guid.Parse(((DropDownList)lvSiteActionTemplateUserColumn.InsertItem.FindControl("ddlSiteColumnValues")).SelectedValue);
                            break;
                    }
                }
            }

            ViewState["SiteActionTemplateUserColumn"] = siteActionTemplateUserColumn;
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlEventCategories control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlEventCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (ListViewItem)((DropDownList)sender).Parent;

            item.FindControl("spanAction").Visible = false;
            item.FindControl("spanColumnValue").Visible = false;
            item.FindControl("spanScoreByType").Visible = false;
            item.FindControl("spanScoreByCharacteristics").Visible = false;

            if (!string.IsNullOrEmpty(((DropDownList)sender).SelectedValue))
            {
                switch ((EventCategory) int.Parse(((DropDownList) sender).SelectedValue))
                {
                    case EventCategory.Action:
                        item.FindControl("spanAction").Visible = true;
                        break;
                    case EventCategory.ColumnValue:
                        item.FindControl("spanColumnValue").Visible = true;
                        break;
                    case EventCategory.ScoreByType:
                        item.FindControl("spanScoreByType").Visible = true;
                        break;
                    case EventCategory.ScoreByCharacteristics:
                        item.FindControl("spanScoreByCharacteristics").Visible = true;
                        break;
                }
            }
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlSiteColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlSiteColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (ListViewItem)((DropDownList)sender).Parent.Parent;
            Guid _siteColumnId;

            Guid.TryParse(((DropDownList)sender).SelectedValue, out _siteColumnId);

            var formulaTypes = new List<tbl_Formula>();
            var siteColumn = DataManager.SiteColumns.SelectById(SiteId, _siteColumnId);

            item.FindControl("txtValueStringSiteColumns").Visible = false;
            item.FindControl("rfvValueStringSiteColumns").Visible = false;
            if (item.FindControl("rfvValueStringSiteColumns2") != null)
                item.FindControl("rfvValueStringSiteColumns2").Visible = false;
            item.FindControl("ddlSiteColumnValues").Visible = false;
            item.FindControl("rfvSiteColumnValues").Visible = false;
            if (item.FindControl("rfvSiteColumnValues2") != null)
                item.FindControl("rfvSiteColumnValues2").Visible = false;
            item.FindControl("txtValueDateSiteColumns").Visible = false;
            item.FindControl("rfvValueDateSiteColumns").Visible = false;
            if (item.FindControl("rfvValueDateSiteColumns2") != null)
                item.FindControl("rfvValueDateSiteColumns2").Visible = false;
            item.FindControl("txtValueNumberSiteColumns").Visible = false;
            item.FindControl("rfvValueNumberSiteColumns").Visible = false;
            if (item.FindControl("rfvValueNumberSiteColumns2") != null)
                item.FindControl("rfvValueNumberSiteColumns2").Visible = false;
            item.FindControl("ddlFormulaSiteColumns").Visible = false;
            item.FindControl("rfvFormulaSiteColumns").Visible = false;
            if (item.FindControl("rfvFormulaSiteColumns2") != null)
                item.FindControl("rfvFormulaSiteColumns2").Visible = false;

            ((DropDownList)item.FindControl("ddlFormulaSiteColumns")).Items.Clear();
            ((DropDownListExt)item.FindControl("ddlFormulaSiteColumns")).PlaceHolderText = "Формула";
            ((DropDownList)item.FindControl("ddlFormulaSiteColumns")).Items.Add(new ListItem { Text = "Формула", Value = "" });
            if (siteColumn != null)
            {
                formulaTypes = DataManager.Formula.Select((ColumnType)siteColumn.TypeID);
                foreach (var formula in formulaTypes)
                    ((DropDownList)item.FindControl("ddlFormulaSiteColumns")).Items.Add(new ListItem { Text = formula.Title, Value = formula.ID.ToString() });

                item.FindControl("ddlFormulaSiteColumns").Visible = true;
                item.FindControl("rfvFormulaSiteColumns").Visible = true;
                if (item.FindControl("rfvFormulaSiteColumns2") != null)
                    item.FindControl("rfvFormulaSiteColumns2").Visible = true;

                switch ((ColumnType)siteColumn.TypeID)
                {
                    case ColumnType.Number:
                        item.FindControl("txtValueNumberSiteColumns").Visible = true;
                        item.FindControl("rfvValueNumberSiteColumns").Visible = true;
                        if (item.FindControl("rfvValueNumberSiteColumns2") != null)
                            item.FindControl("rfvValueNumberSiteColumns2").Visible = true;
                        break;
                    case ColumnType.String:
                    case ColumnType.Text:
                        item.FindControl("txtValueStringSiteColumns").Visible = true;
                        item.FindControl("rfvValueStringSiteColumns").Visible = true;
                        if (item.FindControl("rfvValueStringSiteColumns2") != null)
                            item.FindControl("rfvValueStringSiteColumns2").Visible = true;
                        break;
                    case ColumnType.Date:
                        item.FindControl("txtValueDateSiteColumns").Visible = true;
                        item.FindControl("rfvValueDateSiteColumns").Visible = true;
                        if (item.FindControl("rfvValueDateSiteColumns2") != null)
                            item.FindControl("rfvValueDateSiteColumns2").Visible = true;
                        break;
                    case ColumnType.Enum:
                        var siteColumnValues = DataManager.SiteColumnValues.SelectAll(siteColumn.ID);
                        ((DropDownList)item.FindControl("ddlSiteColumnValues")).Items.Clear();
                        ((DropDownListExt)item.FindControl("ddlSiteColumnValues")).PlaceHolderText = "Значение";
                        ((DropDownList)item.FindControl("ddlSiteColumnValues")).Items.Add(new ListItem { Text = "Значение", Value = "" });

                        foreach (var siteColumnValue in siteColumnValues)
                            ((DropDownList)item.FindControl("ddlSiteColumnValues")).Items.Add(new ListItem { Text = siteColumnValue.Value, Value = siteColumnValue.ID.ToString() });

                        item.FindControl("ddlSiteColumnValues").Visible = true;
                        item.FindControl("rfvSiteColumnValues").Visible = true;
                        if (item.FindControl("rfvSiteColumnValues2") != null)
                            item.FindControl("rfvSiteColumnValues2").Visible = true;
                        break;
                }
            }
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlFormulaSiteColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlFormulaSiteColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (ListViewItem)((DropDownList)sender).Parent.Parent;

            if (!string.IsNullOrEmpty(((DropDownList)sender).SelectedValue) && (FormulaType)int.Parse(((DropDownList)sender).SelectedValue) == FormulaType.SelectFromList)
            {
                item.FindControl("ddlSiteColumnValues").Visible = true;
                item.FindControl("rfvSiteColumnValues").Visible = true;
                if (item.FindControl("rfvSiteColumnValues2") != null)
                    item.FindControl("rfvSiteColumnValues2").Visible = true;
            }
            else
            {
                item.FindControl("ddlSiteColumnValues").Visible = false;
                item.FindControl("rfvSiteColumnValues").Visible = false;
                if (item.FindControl("rfvSiteColumnValues2") != null)
                    item.FindControl("rfvSiteColumnValues2").Visible = false;
            }
        }






        /// <summary>
        /// Handles the ItemCreated event of the SiteEventActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewItemEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventActionTemplate_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem || e.Item.ItemType == ListViewItemType.InsertItem)
            {                
                //var siteActionTemplates = DataManager.SiteActionTemplate.SelectAll(SiteId);
                //foreach (var siteActionTemplate in siteActionTemplates)
                //    ((DropDownList)e.Item.FindControl("ddlSiteActionTemplates")).Items.Add(new ListItem { Text = siteActionTemplate.Title, Value = siteActionTemplate.ID.ToString() });

                var startAfterTypes = DataManager.StartAfterTypes.SelectAll();
                foreach (var startAfterType in startAfterTypes)
                    ((DropDownList)e.Item.FindControl("ddlStartAfterType")).Items.Add(new ListItem { Text = startAfterType.Title, Value = startAfterType.ID.ToString() });

                //((DropDownList)e.Item.FindControl("ddlSiteActionTemplates")).SelectedIndexChanged += new EventHandler(ddlEventCategories_SelectedIndexChanged);                
            }
        }



        /// <summary>
        /// Handles the ItemInserting event of the SiteEventActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewInsertEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventActionTemplate_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            var siteEventActionTemplates = (List<SiteEventActionTemplateMap>)ViewState["SiteEventActionTemplate"];
            var newSiteEventActionTemplate = new SiteEventActionTemplateMap();
            newSiteEventActionTemplate.SiteID = SiteId;
            //newSiteEventActionTemplate.SiteActionTemplateID = Guid.Parse(((DropDownList)lvSiteEventActionTemplate.InsertItem.FindControl("ddlSiteActionTemplates")).SelectedValue);
            newSiteEventActionTemplate.SiteActionTemplateID = ((UserControls.SiteActionTemplate)lvSiteEventActionTemplate.InsertItem.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId;
            newSiteEventActionTemplate.StartAfter = int.Parse(((RadNumericTextBox)lvSiteEventActionTemplate.InsertItem.FindControl("txtStartAfter")).Text);
            newSiteEventActionTemplate.StartAfterTypeID = int.Parse(((DropDownList)lvSiteEventActionTemplate.InsertItem.FindControl("ddlStartAfterType")).SelectedValue);            

            siteEventActionTemplates.Add(newSiteEventActionTemplate);
            ViewState["SiteEventActionTemplate"] = siteEventActionTemplates;

            BindSiteEventActionTemplate();
        }



        /// <summary>
        /// Handles the ItemDeleting event of the lvSiteEventActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventActionTemplate_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var siteEventActionTemplate = (List<SiteEventActionTemplateMap>)ViewState["SiteEventActionTemplate"];
            siteEventActionTemplate.RemoveAt(e.ItemIndex);
            ViewState["SiteEventActionTemplate"] = siteEventActionTemplate;
            BindSiteEventActionTemplate();
        }



        /// <summary>
        /// Handles the ItemCreated event of the lvSiteEventTemplateScore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewItemEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventTemplateScore_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem || e.Item.ItemType == ListViewItemType.InsertItem)
            {
                var siteActivityScoreTypes = DataManager.SiteActivityScoreType.SelectAll(SiteId);
                foreach (var siteActivityScoreType in siteActivityScoreTypes)
                    ((DropDownList)e.Item.FindControl("ddlSiteActivityScoreType")).Items.Add(new ListItem { Text = siteActivityScoreType.Title, Value = siteActivityScoreType.ID.ToString() });

                var operations = DataManager.Operations.SelectAll();
                foreach (var operation in operations)
                    ((DropDownList)e.Item.FindControl("ddlOperation")).Items.Add(new ListItem { Text = operation.Title, Value = operation.ID.ToString() });
            }
        }



        /// <summary>
        /// Handles the ItemInserting event of the lvSiteEventTemplateScore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewInsertEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventTemplateScore_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            var siteEventTemplateScores = (List<SiteEventTemplateScoreMap>)ViewState["SiteEventTemplateScore"];
            var newSiteEventTemplateScore = new SiteEventTemplateScoreMap();
            newSiteEventTemplateScore.SiteID = SiteId;
            newSiteEventTemplateScore.SiteActivityScoreTypeID = Guid.Parse(((DropDownList)lvSiteEventTemplateScore.InsertItem.FindControl("ddlSiteActivityScoreType")).SelectedValue);
            newSiteEventTemplateScore.OperationID = int.Parse(((DropDownList)lvSiteEventTemplateScore.InsertItem.FindControl("ddlOperation")).SelectedValue);
            newSiteEventTemplateScore.Score = int.Parse(((RadNumericTextBox)lvSiteEventTemplateScore.InsertItem.FindControl("txtScore")).Text);

            siteEventTemplateScores.Add(newSiteEventTemplateScore);
            ViewState["SiteEventTemplateScore"] = siteEventTemplateScores;

            BindSiteEventTemplateScore();
        }



        /// <summary>
        /// Handles the ItemDeleting event of the lvSiteEventTemplateScore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void lvSiteEventTemplateScore_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var siteEventTemplateScore = (List<SiteEventTemplateScoreMap>)ViewState["SiteEventTemplateScore"];
            siteEventTemplateScore.RemoveAt(e.ItemIndex);
            ViewState["SiteEventTemplateScore"] = siteEventTemplateScore;
            BindSiteEventTemplateScore();
        }




        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlLogicCondition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlLogicCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlLogicCondition.SelectedValue) && (LogicConditionType)int.Parse(ddlLogicCondition.SelectedValue) == LogicConditionType.NEvents)
                spanActionCount.Visible = true;
            else
                spanActionCount.Visible = false;
        }






        /// <summary>
        /// Handles the ItemCreated event of the lvSiteActionTemplateUserColumn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewItemEventArgs"/> instance containing the event data.</param>
        protected void lvSiteActionTemplateUserColumn_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem || e.Item.ItemType == ListViewItemType.InsertItem)
            {
                var siteColumns = DataManager.SiteColumns.SelectAll(SiteId);
                foreach (var siteColumn in siteColumns)
                    ((DropDownList)e.Item.FindControl("ddlSiteColumns")).Items.Add(new ListItem { Text = siteColumn.Name, Value = siteColumn.ID.ToString() });
            }
        }



        /// <summary>
        /// Handles the ItemDataBound event of the lvSiteActionTemplateUserColumn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewItemEventArgs"/> instance containing the event data.</param>
        protected void lvSiteActionTemplateUserColumn_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (SiteActionTemplateUserColumnMap)e.Item.DataItem;

                if (item != null)
                {
                    var siteColumns = DataManager.SiteColumns.SelectById(SiteId, item.SiteColumnID);
                    switch ((ColumnType)siteColumns.TypeID)
                    {
                        case ColumnType.Number:
                            ((RadNumericTextBox)e.Item.FindControl("txtValueNumber")).Text = item.StringValue;
                            e.Item.FindControl("txtValueNumber").Visible = true;
                            e.Item.FindControl("rfvValueNumber").Visible = true;
                            if (e.Item.FindControl("rfvValueNumber2") != null)
                                e.Item.FindControl("rfvValueNumber").Visible = true;
                            break;
                        case ColumnType.String:
                        case ColumnType.Text:
                            ((RadTextBox)e.Item.FindControl("txtValue")).Text = item.StringValue;
                            e.Item.FindControl("txtValue").Visible = true;
                            e.Item.FindControl("rfvValue").Visible = true;
                            if (e.Item.FindControl("rfvValue2") != null)
                                e.Item.FindControl("txtValue2").Visible = true;
                            break;
                        case ColumnType.Date:
                            ((RadDatePicker)e.Item.FindControl("txtValueDate")).SelectedDate = item.DateValue;
                            e.Item.FindControl("txtValueDate").Visible = true;
                            e.Item.FindControl("rfvValueDate").Visible = true;
                            if (e.Item.FindControl("rfvValueDate2") != null)
                                e.Item.FindControl("rfvValueDate2").Visible = true;
                            break;
                        case ColumnType.Enum:
                            var siteColumnValues = DataManager.SiteColumnValues.SelectAll(item.SiteColumnID);
                            foreach (var siteColumnValue in siteColumnValues)
                                ((DropDownList)e.Item.FindControl("ddlSiteColumnValues")).Items.Add(new ListItem { Text = siteColumnValue.Value, Value = siteColumnValue.ID.ToString() });
                            ((DropDownList)e.Item.FindControl("ddlSiteColumnValues")).Items.FindByValue(item.SiteColumnValueID.ToString()).Selected = true;
                            e.Item.FindControl("ddlSiteColumnValues").Visible = true;
                            e.Item.FindControl("rfvSiteColumnValues").Visible = true;
                            if (e.Item.FindControl("rfvSiteColumnValues2") != null)
                                e.Item.FindControl("rfvSiteColumnValues2").Visible = true;
                            break;
                    } 
                }
            }
        }



        /// <summary>
        /// Handles the ItemInserting event of the lvSiteEventTemplateScore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewInsertEventArgs"/> instance containing the event data.</param>
        protected void lvSiteActionTemplateUserColumn_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            var siteActionTemplateUserColumns = (List<SiteActionTemplateUserColumnMap>)ViewState["SiteActionTemplateUserColumn"];
            var siteColumn = DataManager.SiteColumns.SelectById(SiteId, Guid.Parse(((DropDownList)lvSiteActionTemplateUserColumn.InsertItem.FindControl("ddlSiteColumns")).SelectedValue));
            var newSiteActionTemplateUserColumn = new SiteActionTemplateUserColumnMap();
            newSiteActionTemplateUserColumn.SiteID = SiteId;
            newSiteActionTemplateUserColumn.SiteEventTemplateID = _siteEventTemplateId;
            newSiteActionTemplateUserColumn.SiteColumnID = Guid.Parse(((DropDownList)lvSiteActionTemplateUserColumn.InsertItem.FindControl("ddlSiteColumns")).SelectedValue);
            switch ((ColumnType)siteColumn.TypeID)
            {
                case ColumnType.Number:
                    newSiteActionTemplateUserColumn.StringValue = ((RadNumericTextBox)lvSiteActionTemplateUserColumn.InsertItem.FindControl("txtValueNumber")).Text;
                    break;
                case ColumnType.String:
                case ColumnType.Text:
                    newSiteActionTemplateUserColumn.StringValue = ((RadTextBox)lvSiteActionTemplateUserColumn.InsertItem.FindControl("txtValue")).Text;
                    break;
                case ColumnType.Date:
                    newSiteActionTemplateUserColumn.DateValue = (DateTime)(((RadDatePicker)lvSiteActionTemplateUserColumn.InsertItem.FindControl("txtValueDate")).SelectedDate);
                    break;
                case ColumnType.Enum:
                    newSiteActionTemplateUserColumn.SiteColumnValueID = Guid.Parse(((DropDownList)lvSiteActionTemplateUserColumn.InsertItem.FindControl("ddlSiteColumnValues")).SelectedValue);
                    break;
            }

            siteActionTemplateUserColumns.Add(newSiteActionTemplateUserColumn);
            ViewState["SiteActionTemplateUserColumn"] = siteActionTemplateUserColumns;

            BindSiteActionTemplateUserColumn();
        }



        /// <summary>
        /// Handles the ItemDeleting event of the lvSiteActionTemplateUserColumn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void lvSiteActionTemplateUserColumn_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var siteActionTemplateUserColumn = (List<SiteActionTemplateUserColumnMap>)ViewState["SiteActionTemplateUserColumn"];
            siteActionTemplateUserColumn.RemoveAt(e.ItemIndex);
            ViewState["SiteActionTemplateUserColumn"] = siteActionTemplateUserColumn;
            BindSiteActionTemplateUserColumn();
        }



        protected void ddlSiteColumns_UserColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (ListViewItem)((DropDownList)sender).Parent;

            item.FindControl("txtValue").Visible = false;
            item.FindControl("ddlSiteColumnValues").Visible = false;
            item.FindControl("rfvSiteColumnValues").Visible = false;
            if (item.FindControl("rfvSiteColumnValues2") != null)
                item.FindControl("rfvSiteColumnValues2").Visible = false;
            item.FindControl("txtValueDate").Visible = false;
            item.FindControl("rfvValueDate").Visible = false;
            if (item.FindControl("rfvValueDate2") != null)
                item.FindControl("rfvValueDate2").Visible = false;
            item.FindControl("txtValueNumber").Visible = false;
            item.FindControl("rfvValueNumber").Visible = false;
            if (item.FindControl("rfvValueNumber2") != null)
                item.FindControl("rfvValueNumber2").Visible = false;

            if (!string.IsNullOrEmpty(((DropDownList)sender).SelectedValue))
            {
                var siteColumn = DataManager.SiteColumns.SelectById(SiteId, Guid.Parse(((DropDownList)sender).SelectedValue));
                switch ((ColumnType)siteColumn.TypeID)
                {
                    case ColumnType.String:
                    case ColumnType.Text:
                        item.FindControl("txtValue").Visible = true;
                        break;
                    case ColumnType.Number:
                        item.FindControl("txtValueNumber").Visible = true;
                        item.FindControl("rfvValueNumber").Visible = true;
                        if (item.FindControl("rfvValueNumber2") != null)
                            item.FindControl("rfvValueNumber2").Visible = true;
                        break;
                    case ColumnType.Date:
                        item.FindControl("txtValueDate").Visible = true;
                        item.FindControl("rfvValueDate").Visible = true;
                        if (item.FindControl("rfvValueDate2") != null)
                            item.FindControl("rfvValueDate2").Visible = true;
                        break;
                    case ColumnType.Enum:
                        var placeholderItem = ((DropDownList)item.FindControl("ddlSiteColumnValues")).Items[0];
                        ((DropDownList)item.FindControl("ddlSiteColumnValues")).Items.Clear();
                        var siteColumnValues = DataManager.SiteColumnValues.SelectAll(siteColumn.ID);
                        foreach (var siteColumnValue in siteColumnValues)
                            ((DropDownList)item.FindControl("ddlSiteColumnValues")).Items.Add(new ListItem { Text = siteColumnValue.Value, Value = siteColumnValue.ID.ToString() });
                        ((DropDownList)item.FindControl("ddlSiteColumnValues")).Items.Insert(0, placeholderItem);
                        item.FindControl("ddlSiteColumnValues").Visible = true;
                        item.FindControl("rfvSiteColumnValues").Visible = true;
                        if (item.FindControl("rfvSiteColumnValues2") != null)
                            item.FindControl("rfvSiteColumnValues2").Visible = true;
                        break;
                }
            }
        }



        protected void ddlActivityTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlActivityType = (DropDownList) sender;
            var item = (ListViewItem)(ddlActivityType).Parent.Parent;
            var activityType = (ActivityType)int.Parse(ddlActivityType.SelectedValue);
            var radComboBox = item.FindControl("rcbActivityCode") as RadComboBox;
            BindActivityCodes(radComboBox, activityType);
        }



        /// <summary>
        /// Binds the activity code.
        /// </summary>
        /// <param name="rcbActivityCode">The RCB activity code.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected void BindActivityCodes(RadComboBox rcbActivityCode, ActivityType activityType)
        {
            rcbActivityCode.Items.Clear();
            rcbActivityCode.Text = string.Empty;
            rcbActivityCode.AllowCustomText = true;
            rcbActivityCode.Filter = RadComboBoxFilter.None;
            rcbActivityCode.AllowCustomText = false;

            switch (activityType)
            {
                case ActivityType.ViewPage:                    
                    rcbActivityCode.Filter = RadComboBoxFilter.Contains;
                    var contactActivities = DataManager.ContactActivity.Select(SiteId, null, activityType).Select(sua => new {sua.ActivityCode}).Distinct();
                    foreach (var contactActivity in contactActivities)                    
                        rcbActivityCode.Items.Add(new RadComboBoxItem(HttpUtility.UrlDecode(contactActivity.ActivityCode)));                                        
                    break;
                case ActivityType.Link:
                    rcbActivityCode.DataSource = DataManager.Links.SelectByRuleType(SiteId, new List<int>() { (int)RuleType.Link });
                    rcbActivityCode.DataTextField = "Name";
                    rcbActivityCode.DataValueField = "Code";                    
                    break;
                case ActivityType.OpenForm:
                case ActivityType.FillForm:
                case ActivityType.CancelForm:
                    rcbActivityCode.DataSource = DataManager.SiteActivityRules.SelectByRuleType(SiteId, new List<int>() { (int)RuleType.Form });
                    rcbActivityCode.DataTextField = "Name";
                    rcbActivityCode.DataValueField = "Code";                    
                    break;
                case ActivityType.DownloadFile:
                    rcbActivityCode.DataSource = DataManager.Links.SelectByRuleType(SiteId, new List<int>() { (int)RuleType.File });
                    rcbActivityCode.DataTextField = "Name";
                    rcbActivityCode.DataValueField = "Code";
                    break;
                /*case ActivityType.Event:
                    rcbActivityCode.DataSource = DataManager.SiteEventTemplates.SelectAll(SiteId);
                    rcbActivityCode.DataTextField = "Title";
                    rcbActivityCode.DataValueField = "ID";
                    break;*/
                case ActivityType.Impact:
                    rcbActivityCode.DataSource = DataManager.SiteActionTemplate.SelectAll(SiteId);
                    rcbActivityCode.DataTextField = "Title";
                    rcbActivityCode.DataValueField = "ID";
                    break;
                case ActivityType.InboxMessage:
                    rcbActivityCode.AllowCustomText = true;
                    break;
            }

            rcbActivityCode.DataBind();

            if (activityType == ActivityType.ViewPage || activityType == ActivityType.InboxMessage)
                rcbActivityCode.InputCssClass = string.Empty;
            else
                rcbActivityCode.InputCssClass = "readonly";
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlSiteActionTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void ddlSiteActionTemplates_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var item = (ListViewItem)((DropDownList)sender).Parent;

        //    item.FindControl("txtStartAfter").Visible = false;
        //    item.FindControl("ddlStartAfterType").Visible = false;            

        //    if (!string.IsNullOrEmpty(((DropDownList)sender).SelectedValue))
        //    {

        //        item.FindControl("txtStartAfter").Visible = true;
        //        item.FindControl("ddlStartAfterType").Visible = true;                
        //    }
        //}



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlSiteActivityScoreType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlSiteActivityScoreType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (ListViewItem)((DropDownList)sender).Parent;

            item.FindControl("ddlOperation").Visible = false;
            item.FindControl("txtScore").Visible = false;

            if (!string.IsNullOrEmpty(((DropDownList)sender).SelectedValue))
            {

                item.FindControl("ddlOperation").Visible = true;
                item.FindControl("txtScore").Visible = true;
            }
        }



        /// <summary>
        /// Handles the OnSelectedTemplateChanged event of the ucSiteActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.SiteActionTemplate.SelectedTemplateChangedEventArgs"/> instance containing the event data.</param>
        protected void ucSiteActionTemplate_OnSelectedTemplateChanged(object sender, UserControls.SiteActionTemplate.SelectedTemplateChangedEventArgs e)
        {
            var item = (ListViewItem)((UserControls.SiteActionTemplate)sender).Parent;
            
            if (e.SelectedTemplateId != Guid.Empty)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "Show",
                                                        string.Format("$find('{0}').set_visible(true);$('#{1}').show();ValidatorEnable({2},true);ValidatorEnable({3},true);ValidatorEnable({4},true);ValidatorEnable({5},true);",
                                                                      item.FindControl("txtStartAfter").ClientID,
                                                                      item.FindControl("ddlStartAfterType").ClientID,
                                                                      item.FindControl("RequiredFieldValidator50").ClientID,
                                                                      item.FindControl("RequiredFieldValidator47").ClientID,
                                                                      item.FindControl("RequiredFieldValidator18").ClientID,
                                                                      item.FindControl("RequiredFieldValidator51").ClientID),
                                                        true);
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "Hide", string.Format("$find('{0}').set_visible(false);$('#{1}').hide();ValidatorEnable({2},false);ValidatorEnable({3},false);ValidatorEnable({4},false);ValidatorEnable({5},false);",
                                                                      item.FindControl("txtStartAfter").ClientID,
                                                                      item.FindControl("ddlStartAfterType").ClientID,
                                                                      item.FindControl("RequiredFieldValidator50").ClientID,
                                                                      item.FindControl("RequiredFieldValidator47").ClientID,
                                                                      item.FindControl("RequiredFieldValidator18").ClientID,
                                                                      item.FindControl("RequiredFieldValidator51").ClientID), true);
            }            
        }
    }
}