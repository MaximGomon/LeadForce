using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Analytic;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Analytics
{
    public partial class AnalyticReportFilters : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        public event ReportFilterChangedEventHandler ReportFilterChanged;
        public delegate void ReportFilterChangedEventHandler(object sender);

        public event ReportFilterAxisChangedEventHandler ReportFilterAxisChanged;
        public delegate void ReportFilterAxisChangedEventHandler(object sender);        


        /// <summary>
        /// Gets or sets the analytic report id.
        /// </summary>
        /// <value>
        /// The analytic report id.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? AnalyticReportId
        {
            get { return (Guid?)ViewState["AnalyticReportId"]; }
            set { ViewState["AnalyticReportId"] = value; }
        }



        /// <summary>
        /// Gets or sets the axis to build chart.
        /// </summary>
        /// <value>
        /// The axis to build chart.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? AxisToBuildChart
        {
            get { return (Guid?)ViewState["AxisToBuildChart"]; }
            set { ViewState["AxisToBuildChart"] = value; }
        }



        /// <summary>
        /// Gets or sets the report filters.
        /// </summary>
        /// <value>
        /// The report filters.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<AnalyticReportFilter> ReportFilters
        {
            get
            {
                if (ViewState["ReportFilters"] == null)
                    ViewState["ReportFilters"] = new List<AnalyticReportFilter>();

                return (List<AnalyticReportFilter>)ViewState["ReportFilters"];
            }
            set { ViewState["ReportFilters"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if (!AnalyticReportId.HasValue)
                return;

            plFiltersContainer.Controls.Clear();

            var analyticFilters = DataManager.AnalyticReportSystem.SelectFiltersByAnalyticReportId((Guid)AnalyticReportId).Where(o => o.AxisTypeID != (int)AxisType.XAxis);

            if (!analyticFilters.Any())
            {
                Visible = false;
                return;
            }
            else
                Visible = true;

            var legendAxis = new List<tbl_AnalyticAxis>();
            var currentAxis = Guid.Empty;
            var checkBoxFilters = new List<CheckBox>();
            
            foreach (var analyticFilter in analyticFilters)
            {
                var label = new Label() { Text = string.Concat(analyticFilter.tbl_AnalyticAxis.Title, ":") };

                var dropDownList = new DropDownList() { CssClass = "select-text", AutoPostBack = true, ID = string.Concat("ddl", analyticFilter.ID) };
                dropDownList.SelectedIndexChanged += dropDownList_SelectedIndexChanged;
                dropDownList.Attributes.Add("AnalyticFilterID", analyticFilter.ID.ToString());

                //Обработка заранеее добавленных фильтров
                if (analyticFilter.tbl_AnalyticAxis.tbl_AnalyticAxisFilterValues.Count > 0)
                {
                    var axisFilterValues = analyticFilter.tbl_AnalyticAxis.tbl_AnalyticAxisFilterValues;

                    if (axisFilterValues.Any(o => o.FilterType == (int)FilterType.CheckBox))
                    {
                        var checkBox = new CheckBox
                                           {
                                               ID = string.Concat("chx", analyticFilter.ID),
                                               AutoPostBack = true,
                                               Text = analyticFilter.tbl_AnalyticAxis.Title
                                           };
                        checkBox.CheckedChanged += checkBox_CheckedChanged;
                        checkBox.Attributes.Add("AnalyticFilterID", analyticFilter.ID.ToString());

                        if (axisFilterValues.Any(o => o.IsDefault))
                        {
                            checkBox.Checked = axisFilterValues.FirstOrDefault(o => o.IsDefault).Value == "1";
                            ApplyCheckBoxFilter(checkBox);
                        }

                        //TODO: Добавить сортировку к филтьтрам
                        //Для того чтобы логические фильтры были в конце
                        checkBoxFilters.Add(checkBox);                        
                    }

                    if (axisFilterValues.Any(o => o.FilterType == (int)FilterType.DropDown))
                    {
                        dropDownList.DataSource = analyticFilter.tbl_AnalyticAxis.tbl_AnalyticAxisFilterValues;
                        dropDownList.DataTextField = "Title";
                        dropDownList.DataValueField = "ID";
                        dropDownList.DataBind();
                        AddDropDown(plFiltersContainer, label, dropDownList);
                    }
                }
                //Обработка осей справочников
                else if (!string.IsNullOrEmpty(analyticFilter.tbl_AnalyticAxis.DataSet))
                {
                    if (analyticFilter.tbl_AnalyticAxis.AxisRoleID != (int)AxisRole.IsFilter)
                        legendAxis.Add(analyticFilter.tbl_AnalyticAxis);

                    dropDownList.DataSource = DataManager.AnalyticAxis.SelectSeriesByDataSet(analyticFilter.tbl_AnalyticAxis,
                                                                           CurrentUser.Instance.SiteID);
                    dropDownList.DataTextField = "Value";
                    dropDownList.DataValueField = "Key";
                    dropDownList.DataBind();

                    dropDownList.Items.Insert(0, new ListItem("Выберите значение", Guid.Empty.ToString()));

                    AddDropDown(plFiltersContainer, label, dropDownList);
                    
                    if((!AxisToBuildChart.HasValue && analyticFilter.IsDefault) ||
                        (AxisToBuildChart.HasValue && analyticFilter.tbl_AnalyticAxis.ID == AxisToBuildChart.Value))
                    {
                        currentAxis = analyticFilter.tbl_AnalyticAxis.ID;
                    }
                }
            }

            foreach (CheckBox checkBoxFilter in checkBoxFilters)
            {
                plFiltersContainer.Controls.Add(checkBoxFilter);  
            }

            plAxis.Controls.Clear();
            plAxis.Visible = false;


            if (DataManager.AnalyticReportSystem.SelectAxisValuesByAnalyticReportId((Guid)AnalyticReportId).Count() > 1 || legendAxis.Count > 1 || DataManager.AnalyticReportSystem.SelectXAxisByAnalyticReportId((Guid)AnalyticReportId).Count() > 1)
            {
                plAxis.Visible = true;
                plAxis.Controls.Add(new Literal() {Text = "<h4>Параметры отчета:</h4>", ID = "lrlReportSettings"});
            }

            //Если для графика задано больше одной оси для построения тогда показываем список для выбора
            if (legendAxis.Count > 1)
            {                                
                var label = new Label() { Text = "Легенда:" };
                var dropDownList = new DropDownList { ID = "ddlChangeAxis", CssClass = "select-text", AutoPostBack = true, DataSource = legendAxis, DataTextField = "Title", DataValueField = "ID" };
                dropDownList.SelectedIndexChanged += ddlChangeAxis_SelectedIndexChanged;
                dropDownList.DataBind();
                AddDropDown(plAxis, label, dropDownList);                

                if (currentAxis != Guid.Empty)
                    dropDownList.SelectedValue = currentAxis.ToString();
            }

            if (analyticFilters.Count() == 1 && legendAxis.Count == 1)
                Visible = false;
            else
                Visible = true;
        }        



        protected void AddDropDown(Panel panel, Label label, DropDownList dropDown)
        {
            var wrapper = new Panel {ID = "pl" + dropDown.ID, CssClass = "filter-wrapper"};
            wrapper.Controls.Add(label);
            wrapper.Controls.Add(dropDown);
            panel.Controls.Add(wrapper);
        }



        /// <summary>
        /// Clears the filters.
        /// </summary>
        public void ClearFilters()
        {
            ReportFilters.Clear();
        }



        /// <summary>
        /// Applies the check box filter.
        /// </summary>
        /// <param name="checkBox">The check box.</param>
        private void ApplyCheckBoxFilter(CheckBox checkBox)
        {
            var analyticFilterId = Guid.Parse(checkBox.Attributes["AnalyticFilterID"]);
            var checkedValue = checkBox.Checked;

            var analyticFilter = DataManager.AnalyticReportSystem.SelectById(analyticFilterId);

            if (analyticFilter != null)
            {
                var filterValues = analyticFilter.tbl_AnalyticAxis.tbl_AnalyticAxisFilterValues;
                if (filterValues.Count > 0)
                {
                    var reportFilter = new AnalyticReportFilter()
                    {
                        FilterId = analyticFilterId,
                        Operator = FilterOperator.None,
                        Query = filterValues.SingleOrDefault(o => o.Value == (checkedValue ? "1" : "0")).Query
                    };

                    var existsReportFilter = ReportFilters.SingleOrDefault(o => o.FilterId == analyticFilterId);
                    if (existsReportFilter != null)
                        ReportFilters.Remove(existsReportFilter);

                    ReportFilters.Add(reportFilter);

                    ReportFilterChanged(this);
                }
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlChangeAxis control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlChangeAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReportFilterAxisChanged != null)
            {
                ReportFilters.Clear();
                AxisToBuildChart = Guid.Parse(((DropDownList) sender).SelectedValue);
                BindData();
                ReportFilterAxisChanged(this);
            }   
        }



        protected void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ReportFilterChanged != null)
            {
                var checkBox = (CheckBox) sender;
                ApplyCheckBoxFilter(checkBox);
            }
        }        



        /// <summary>
        /// Handles the SelectedIndexChanged event of the dropDownList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void dropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReportFilterChanged != null)
            {
                var dropDownList = (DropDownList)sender;

                var analyticFilterId = Guid.Parse(dropDownList.Attributes["AnalyticFilterID"]);
                var selectedValue = Guid.Parse(dropDownList.SelectedValue);

                var analyticFilter = DataManager.AnalyticReportSystem.SelectById(analyticFilterId);
                //Обработка заранеее добавленных фильтров
                if (analyticFilter.tbl_AnalyticAxis.tbl_AnalyticAxisFilterValues.Count > 0)
                {
                    var analyticFilterValue = analyticFilter.tbl_AnalyticAxis.tbl_AnalyticAxisFilterValues.SingleOrDefault(o => o.ID == selectedValue);
                    if (analyticFilterValue != null)
                    {
                        var reportFilter = new AnalyticReportFilter()
                                                {
                                                    FilterId = analyticFilterId,
                                                    ColumnName = analyticFilterValue.ColumnName,
                                                    Value = analyticFilterValue.Value,
                                                    Operator = (FilterOperator)analyticFilterValue.FilterOperatorID,
                                                    Query = analyticFilterValue.Query
                                                };

                        var existsReportFilter = ReportFilters.SingleOrDefault(o => o.FilterId == analyticFilterId);
                        if (existsReportFilter != null)
                            ReportFilters.Remove(existsReportFilter);

                        ReportFilters.Add(reportFilter);

                        ReportFilterChanged(this);
                    }
                }
                //Обработка фильтров справочников
                else if (!string.IsNullOrEmpty(analyticFilter.tbl_AnalyticAxis.DataSet))
                {                    
                    var reportFilter = new AnalyticReportFilter()
                    {
                        FilterId = analyticFilterId,
                        ColumnName = analyticFilter.tbl_AnalyticAxis.DataSet.Replace("_",string.Empty) + ".ID",
                        Value = selectedValue.ToString(),
                        Operator = FilterOperator.Equal
                    };

                    var existsReportFilter = ReportFilters.SingleOrDefault(o => o.FilterId == analyticFilterId);
                    if (existsReportFilter != null)
                        ReportFilters.Remove(existsReportFilter);

                    if (selectedValue != Guid.Empty)
                        ReportFilters.Add(reportFilter);

                    ReportFilterChanged(this);
                }
            }
        }
    }
}