using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard
{
    public partial class RunCondition : WorkflowTemplateWizardStep
    {
        protected RadAjaxManager radAjaxManager = null;



        //Store the info about the added docks in the session.
        private List<DockState> CurrentDockStates
        {
            get
            {
                List<DockState> _currentDockStates = (List<DockState>)Session["CurrentDockStates"];
                if (Object.Equals(_currentDockStates, null))
                {
                    _currentDockStates = new List<DockState>();
                    Session["CurrentDockStates"] = _currentDockStates;
                }
                return _currentDockStates;
            }
            set
            {
                Session["CurrentDockStates"] = value;
            }
        }

        private List<DocksCondition> DocksConditions
        {
            get
            {
                if (Session["DocksConditions"] == null)
                    Session["DocksConditions"] = new List<DocksCondition>();
                return (List<DocksCondition>)(Session["DocksConditions"]);
            }
            set
            {
                Session["DocksConditions"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFilterText.Attributes.Add("onkeyup", "FilterList()");
        }



        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsEditMode && DocksConditions.Count == 0)
            {
                tbl_WorkflowTemplate workflowTemplate = null;

                var workflowTemplateConditionEvents = DataManager.WorkflowTemplateConditionEvent.SelectAllByWorkflowTemplateId(EditObjectId.Value);
                foreach (var workflowTemplateConditionEvent in workflowTemplateConditionEvents)
                {
                    var condition = new DocksCondition
                                        {
                                            Id = Guid.NewGuid(),
                                            ActivityType = (ActivityType) workflowTemplateConditionEvent.ActivityType,
                                            ActualPeriod = (int) workflowTemplateConditionEvent.ActualPeriod
                                        };
                    if (workflowTemplateConditionEvent.ActivityType != (int)ActivityType.ViewPage)
                    {
                        if (workflowTemplateConditionEvent.ActivityType == (int)ActivityType.OpenForm ||
                        workflowTemplateConditionEvent.ActivityType == (int)ActivityType.FillForm ||
                        workflowTemplateConditionEvent.ActivityType == (int)ActivityType.CancelForm)
                        {
                            var codeAndParameter = workflowTemplateConditionEvent.Code.Split('#');
                            var siteActivityRule = DataManager.SiteActivityRules.SelectFormByCode(CurrentUser.Instance.SiteID, codeAndParameter[0]);
                            condition.SiteActivityRuleId = siteActivityRule.ID;
                            if (codeAndParameter.Length > 1 && !string.IsNullOrEmpty(codeAndParameter[1]))
                                condition.Parameter = codeAndParameter[1];
                        }
                        else if (workflowTemplateConditionEvent.ActivityType == (int)ActivityType.DownloadFile)
                        {
                            var link = DataManager.Links.SelectFormByCode(CurrentUser.Instance.SiteID, workflowTemplateConditionEvent.Code);
                            condition.SiteActivityRuleId = link.ID;
                        }
                    }
                    else
                        condition.Code = workflowTemplateConditionEvent.Code;

                    DocksConditions.Add(condition);
                }
            }

            foreach (var docksCondition in DocksConditions)
                AddRadDock(docksCondition);
        }




        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();

            EnumHelper.EnumToDropDownList<WorkflowTemplateCondition>(ref ddlCondition);

            if (IsEditMode)
            {
                var workflowTemplate = DataManager.WorkflowTemplate.SelectById(EditObjectId.Value);
                ddlCondition.Items.FindByValue(((int)workflowTemplate.Condition).ToString()).Selected = true;
                txtActivityCount.Value = workflowTemplate.ActivityCount;
                if ((int)workflowTemplate.Condition == (int)WorkflowTemplateCondition.NEvent)
                    pnlActivityCount.Visible = true;
            }

            var lastDays = DateTime.Now.AddDays(-30);
            var viewPages =
                DataManager.ContactActivity.SelectAll(CurrentUser.Instance.SiteID).Where(
                    a => a.ActivityTypeID == (int)ActivityType.ViewPage && a.CreatedAt > lastDays).
                    GroupBy(g => g.ActivityCode).Select(p => new { Code = p.Key, Count = p.Count() }).OrderBy(o => o.Count);

            rblViewPages.Items.Clear();
            foreach (var viewPage in viewPages)
                rblViewPages.Items.Add(new RadListBoxItem(viewPage.Code, viewPage.Code));

            var forms = DataManager.SiteActivityRules.SelectByRuleType(CurrentUser.Instance.SiteID, new List<int> { (int)RuleType.Form });
            rlbForms.Items.Clear();
            foreach (var form in forms)
                rlbForms.Items.Add(new RadListBoxItem(form.Name, form.ID.ToString()));

            BindFiles();
        }



        /// <summary>
        /// Binds the files.
        /// </summary>
        protected void BindFiles()
        {
            var files = DataManager.Links.SelectByRuleType(CurrentUser.Instance.SiteID, new List<int> { (int)RuleType.File });
            rlbFiles.Items.Clear();
            foreach (var file in files)
                rlbFiles.Items.Add(new RadListBoxItem(file.Name, file.ID.ToString()));
        }



        /// <summary>
        /// Adds the RAD dock.
        /// </summary>
        /// <param name="docksCondition">The docks condition.</param>
        /// <returns></returns>
        protected RadDock AddRadDock(DocksCondition docksCondition)
        {
            dynamic data = null;

            var radDock = new RadDock
            {
                Text = string.Empty,
                ID = "RadDock" + docksCondition.Id,
                Skin = "Windows7",
                DockMode = DockMode.Docked,
                Width = new Unit(500, UnitType.Pixel),
                UniqueName = docksCondition.Id.ToString()
            };
            radDock.Attributes.Add("ActivityType", ((int)docksCondition.ActivityType).ToString());
            switch (docksCondition.ActivityType)
            {
                case ActivityType.DownloadFile:
                    var link = DataManager.Links.SelectById(docksCondition.SiteActivityRuleId);
                    data = new { Id = link.ID, link.Name };
                    radDock.Attributes.Add("Code", link.Code);
                    break;
                case ActivityType.OpenForm:
                case ActivityType.FillForm:
                case ActivityType.CancelForm:
                    var siteActivityRule = DataManager.SiteActivityRules.SelectById(docksCondition.SiteActivityRuleId);
                    data = new { Id = siteActivityRule.ID, siteActivityRule.Name };
                    radDock.Attributes.Add("Code", siteActivityRule.Code);
                    break;
                case ActivityType.ViewPage:
                    radDock.Attributes.Add("Code", docksCondition.Code);
                    break;
            }

            var labelName = string.Empty;
            switch (docksCondition.ActivityType)
            {
                case ActivityType.ViewPage:
                    radDock.Title = string.Format("Просмотр страницы \"{0}\"", docksCondition.Code);
                    labelName = "URL:";
                    break;
                case ActivityType.FillForm:
                    radDock.Title = string.Format("Заполнение формы \"{0}\"", data.Name);
                    labelName = "Форма:";
                    break;
                case ActivityType.OpenForm:
                    radDock.Title = string.Format("Открытие формы \"{0}\"", data.Name);
                    labelName = "Форма:";
                    break;
                case ActivityType.CancelForm:
                    radDock.Title = string.Format("Отмена формы \"{0}\"", data.Name);
                    labelName = "Форма:";
                    break;
                case ActivityType.DownloadFile:
                    radDock.Title = string.Format("Скачивание файла \"{0}\"", data.Name);
                    labelName = "Файл:";
                    break;
            }

            var table = new Table();
            var tableRow1 = new TableRow();
            var tableRow2 = new TableRow();
            var tableRow3 = new TableRow();

            var tableCell1_1 = new TableCell { ColumnSpan = 2 };
            var tableCell2_1 = new TableCell { Width = new Unit(246), VerticalAlign = VerticalAlign.Top };
            var tableCell2_2 = new TableCell { VerticalAlign = VerticalAlign.Top };
            var tableCell3_1 = new TableCell { Width = new Unit(246), VerticalAlign = VerticalAlign.Top };
            var tableCell3_2 = new TableCell();

            tableCell1_1.Attributes.CssStyle.Add("padding", "3px 0");
            tableCell2_1.Attributes.CssStyle.Add("padding", "3px 0");
            tableCell3_1.Attributes.CssStyle.Add("padding", "3px 0");

            tableCell1_1.Controls.Add(new Label { Text = labelName });

            if (docksCondition.ActivityType != ActivityType.ViewPage)
            {
                var dcbForms = (DictionaryComboBox) LoadControl("~/UserControls/DictionaryComboBox.ascx");
                dcbForms.ID = "dcbForms";
                dcbForms.DictionaryName = docksCondition.ActivityType == ActivityType.DownloadFile ? "tbl_Links" : "tbl_SiteActivityRules";
                dcbForms.DataTextField = "Name";
                dcbForms.CssClass = "select-text";
                dcbForms.ShowEmpty = false;
                dcbForms.SiteID = CurrentUser.Instance.SiteID;
                dcbForms.Width = new Unit(480);
                dcbForms.AutoPostBack = true;
                var filter = new DictionaryComboBox.DictionaryFilterColumn();
                filter = new DictionaryComboBox.DictionaryFilterColumn { Name = "RuleTypeID", DbType = DbType.Int32, Value = docksCondition.ActivityType != ActivityType.DownloadFile ? ((int)RuleType.Form).ToString() : ((int)RuleType.File).ToString() };
                dcbForms.Filters.Add(filter);
                dcbForms.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(dcbForms_SelectedIndexChanged);
                dcbForms.BindData();
                dcbForms.SelectedValue = data.Id;

                tableCell1_1.Controls.Add(dcbForms);
                tableRow1.Controls.Add(tableCell1_1);

                if (docksCondition.ActivityType != ActivityType.DownloadFile && docksCondition.ActivityType != ActivityType.ViewPage)
                {
                    tableCell2_1.Controls.Add(new Label { Text = "Действие:" });
                    var ddlActivityType = new DropDownList { ID = "ddlActivityType", AutoPostBack = true, CssClass = "select-text" };
                    ddlActivityType.Items.Add(new ListItem("Заполнение", ((int)ActivityType.FillForm).ToString()));
                    ddlActivityType.Items.Add(new ListItem("Открытие", ((int)ActivityType.OpenForm).ToString()));
                    ddlActivityType.Items.Add(new ListItem("Отмена", ((int)ActivityType.CancelForm).ToString()));
                    ddlActivityType.Items.FindByValue(((int)docksCondition.ActivityType).ToString()).Selected = true;
                    ddlActivityType.SelectedIndexChanged += new EventHandler(ddlActivityType_SelectedIndexChanged);
                    tableCell2_1.Controls.Add(ddlActivityType);
                    tableRow2.Controls.Add(tableCell2_1);
                }
            }
            else
            {
                tableCell1_1.Controls.Add(new TextBox { ID = "txtCode", CssClass = "input-text", Text = docksCondition.Code, Width = new Unit(466) });
                tableCell1_1.Controls.Add(new RequiredFieldValidator { ControlToValidate = "txtCode", Text = "*", ValidationGroup = "RunCondition", CssClass = "required", Display = ValidatorDisplay.Dynamic });
                tableRow1.Controls.Add(tableCell1_1);
            }


            if (docksCondition.ActivityType != ActivityType.DownloadFile && docksCondition.ActivityType != ActivityType.ViewPage)
            {
                tableCell2_2.Attributes.CssStyle.Add("padding-bottom", "5px");
                tableCell2_2.Controls.Add(new Label { Text = "Параметр:" });
                tableCell2_2.Controls.Add(new TextBox { ID = "txtParameter", CssClass = "input-text", Text = docksCondition.Parameter });
                tableRow2.Controls.Add(tableCell2_2);
            }

            var simpleMode = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "SimpleMode");

            if (simpleMode == null && !((LeadForceBasePage)Page).IsDefaultEdition)
                tableCell3_1.Controls.Add(new Label { Text = "Период актуальности, дней:" });

            var txtActualPeriod = new RadNumericTextBox { ID = "txtActualPeriod", Type = NumericType.Number, CssClass = "input-text", Value = docksCondition.ActualPeriod, ValidationGroup = "RunCondition" };
            txtActualPeriod.NumberFormat.GroupSeparator = "";
            txtActualPeriod.NumberFormat.AllowRounding = false;
            
            if (simpleMode != null || ((LeadForceBasePage)Page).IsDefaultEdition)
                txtActualPeriod.Visible = false;

            tableCell3_1.Controls.Add(txtActualPeriod);

            var rfvActualPeriod = new RequiredFieldValidator { ControlToValidate = "txtActualPeriod", Text = "*", CssClass = "required", ValidationGroup = "RunCondition" };
            if (simpleMode == null && !((LeadForceBasePage)Page).IsDefaultEdition)
                tableCell3_1.Controls.Add(rfvActualPeriod);
            tableRow3.Controls.Add(tableCell3_1);

            table.Controls.Add(tableRow1);

            if (docksCondition.ActivityType == ActivityType.DownloadFile || docksCondition.ActivityType == ActivityType.ViewPage)
            {
                if (simpleMode == null && !((LeadForceBasePage)Page).IsDefaultEdition)
                {
                    tableCell3_2.Controls.Add(new Literal { Text = "&nbsp;" });
                    tableRow3.Controls.Add(tableCell3_2);
                }

            }
            else
                table.Controls.Add(tableRow2);

            table.Controls.Add(tableRow3);

            radDock.ContentContainer.Controls.Add(table);
            radDockZone.Controls.Add(radDock);

            return radDock;
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlActivityType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ddlActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dropDownList = (DropDownList)sender;
            var dock = FindControlParent(dropDownList, typeof(RadDock)) as RadDock;
            var condition = DocksConditions.FirstOrDefault(a => a.Id == dock.UniqueName.ToGuid());
            condition.ActivityType = (ActivityType)dropDownList.SelectedValue.ToInt();
            dock.Attributes["ActivityType"] = ((int)condition.ActivityType).ToString();
            var siteActivityRule = DataManager.SiteActivityRules.SelectById(condition.SiteActivityRuleId);
            switch (condition.ActivityType)
            {
                case ActivityType.FillForm:
                    dock.Title = string.Format("Заполнение формы \"{0}\"", siteActivityRule.Name);
                    break;
                case ActivityType.OpenForm:
                    dock.Title = string.Format("Открытие формы \"{0}\"", siteActivityRule.Name);
                    break;
                case ActivityType.CancelForm:
                    dock.Title = string.Format("Отмена формы \"{0}\"", siteActivityRule.Name);
                    break;
            }
            ((DropDownList)dock.ContentContainer.FindControl("ddlActivityType")).Items.FindByValue(dropDownList.SelectedValue).Selected = true;
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the dcbForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        void dcbForms_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var code = string.Empty;
            var dictionaryComboBox = (DictionaryComboBox)sender;
            var dock = FindControlParent(dictionaryComboBox, typeof(RadDock)) as RadDock;
            var condition = DocksConditions.FirstOrDefault(a => a.Id == dock.UniqueName.ToGuid());
            condition.SiteActivityRuleId = dictionaryComboBox.SelectedValue;
            dock.Attributes["Code"] = condition.ActivityType == ActivityType.DownloadFile
                                          ? DataManager.Links.SelectById(condition.SiteActivityRuleId).Code
                                          : DataManager.SiteActivityRules.SelectById(condition.SiteActivityRuleId).Code;
            switch (condition.ActivityType)
            {
                case ActivityType.FillForm:
                    dock.Title = string.Format("Заполнение формы \"{0}\"", dictionaryComboBox.SelectedText);
                    break;
                case ActivityType.OpenForm:
                    dock.Title = string.Format("Открытие формы \"{0}\"", dictionaryComboBox.SelectedText);
                    break;
                case ActivityType.CancelForm:
                    dock.Title = string.Format("Отмена формы \"{0}\"", dictionaryComboBox.SelectedText);
                    break;
                case ActivityType.DownloadFile:
                    dock.Title = string.Format("Скачивание файла \"{0}\"", dictionaryComboBox.SelectedText);
                    break;
            }
            ((DictionaryComboBox)dock.ContentContainer.FindControl("dcbForms")).SelectedValue = dictionaryComboBox.SelectedValue;
        }


        /// <summary>
        /// Handles the OnDropped event of the rlb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadListBoxDroppedEventArgs"/> instance containing the event data.</param>
        protected void rlb_OnDropped(object sender, RadListBoxDroppedEventArgs e)
        {
            var activityType = ActivityType.FillForm;
            switch (((RadListBox)sender).ID)
            {
                case "rblViewPages":
                    activityType = ActivityType.ViewPage;
                    break;
                case "rlbForms":
                    activityType = ActivityType.FillForm;
                    break;
                case "rlbFiles":
                    activityType = ActivityType.DownloadFile;
                    break;
            }

            foreach (var item in e.SourceDragItems)
            {
                var docksCondition = new DocksCondition
                                         {
                                             Id = Guid.NewGuid(),
                                             ActivityType = activityType,
                                             ActualPeriod = 180
                                         };
                if (((RadListBox)sender).ID != "rblViewPages")
                    docksCondition.SiteActivityRuleId = item.Value.ToGuid();
                else
                    docksCondition.Code = item.Value;
                DocksConditions.Add(docksCondition);
                AddRadDock(docksCondition);
            }
        }



        /// <summary>
        /// Handles the OnClick event of the rbAddViewPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbAddViewPage_OnClick(object sender, EventArgs e)
        {
            var docksCondition = new DocksCondition
            {
                Id = Guid.NewGuid(),
                ActivityType = ActivityType.ViewPage,
                ActualPeriod = 180,
                Code = txtFilterText.Text
            };

            DocksConditions.Add(docksCondition);
            AddRadDock(docksCondition);
            txtFilterText.Text = string.Empty;
        }



        /// <summary>
        /// Handles the OnClick event of the lbAddFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbAddFile_OnClick(object sender, EventArgs e)
        {
            var siteId = CurrentUser.Instance.SiteID;
            var fsp = new FileSystemProvider();
            if (rauFile.UploadedFiles.Count > 0)
            {
                string fileName = null;
                if (rauFile.UploadedFiles.Count > 0)
                {                    
                    IFileProvider fileProvider = new FSProvider();
                    fileName = fileProvider.GetFilename(siteId, rauFile.UploadedFiles[0].FileName);
                    fsp.Upload(siteId, fileName, rauFile.UploadedFiles[0].InputStream);

                }
                var link = new tbl_Links();
                link.SiteID = siteId;
                link.Name = fileName;
                link.RuleTypeID = (int)RuleType.File;
                link.URL = fileName;
                link.FileSize = rauFile.UploadedFiles[0].InputStream.Length;
                string code = String.Format("file_[{0}]_[{1}]", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("mmss"));
                int maxCode = DataManager.Links.SelectByCode(siteId, code);
                if (maxCode != 0) maxCode++;
                link.Code = code + (maxCode != 0 ? String.Format("[{0}]", maxCode >= 10 ? maxCode.ToString() : "0" + maxCode.ToString()) : "");

                DataManager.Links.Add(link);

                BindFiles();
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlCondition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlCondition_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCondition.SelectedValue) && ddlCondition.SelectedValue.ToEnum<WorkflowTemplateCondition>() == WorkflowTemplateCondition.NEvent)
                pnlActivityCount.Visible = true;
            else
                pnlActivityCount.Visible = false;
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


    public class DocksCondition
    {
        public Guid Id { get; set; }
        public Guid SiteActivityRuleId { get; set; }
        public string Code { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Parameter { get; set; }
        public int ActualPeriod { get; set; }
    }
}