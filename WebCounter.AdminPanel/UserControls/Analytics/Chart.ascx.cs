using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Charting;
using Telerik.Charting.Styles;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Analytic;
using WebCounter.DataAccessLayer;
using Unit = System.Web.UI.WebControls.Unit;
using UnitType = System.Web.UI.WebControls.UnitType;

namespace WebCounter.AdminPanel.UserControls.Analytics
{
    public partial class Chart : System.Web.UI.UserControl
    {
        protected WebCounter.BusinessLogicLayer.DataManager DataManager =
            new WebCounter.BusinessLogicLayer.DataManager();

        protected List<AnalyticSeries> AnalyticSeriesList = new List<AnalyticSeries>();
        protected List<AnalyticAxis> AnalyticXAxisList = new List<AnalyticAxis>();
        protected List<AnalyticAxis> AnalyticYAxisList = new List<AnalyticAxis>();
        protected List<double> AnalyticSeriesSum = new List<double>();

        public DataTable Data { get; set; }

        /// <summary>
        /// Gets or sets the analytic report id.
        /// </summary>
        /// <value>
        /// The analytic report id.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? AnalyticReportId
        {
            get { return (Guid?) ViewState["AnalyticReportId"]; }
            set { ViewState["AnalyticReportId"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string UserSettingsClassName
        {
            get { return (string) ViewState["UserSettingsClassName"]; }
            set { ViewState["UserSettingsClassName"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public int? Width
        {
            get { return (int?) ViewState["Width"]; }
            set { ViewState["Width"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public ChartSeriesType CurrentChartSeriesType
        {
            get
            {
                if (ViewState["CurrentChartSeriesType"] == null)
                    ViewState["CurrentChartSeriesType"] = ChartSeriesType.Line;

                return (ChartSeriesType) ViewState["CurrentChartSeriesType"];
            }
            set { ViewState["CurrentChartSeriesType"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool ShowDatePeriod
        {
            get
            {
                if (ViewState["ShowDatePeriod"] == null)
                    ViewState["ShowDatePeriod"] = true;

                return (bool) ViewState["ShowDatePeriod"];
            }
            set { ViewState["ShowDatePeriod"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsLoadFromSettings
        {
            get
            {
                if (ViewState["IsLoadFromSettings"] == null)
                    ViewState["IsLoadFromSettings"] = false;

                return (bool) ViewState["IsLoadFromSettings"];
            }
            set { ViewState["IsLoadFromSettings"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<AnalyticReportFilter> ReportFilters
        {
            get
            {
                if (ViewState["ReportFilters"] == null)
                    ViewState["ReportFilters"] = new List<AnalyticReportFilter>();

                return (List<AnalyticReportFilter>) ViewState["ReportFilters"];
            }
            set { ViewState["ReportFilters"] = value; }
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
            get { return (Guid?) ViewState["AxisToBuildChart"]; }
            set { ViewState["AxisToBuildChart"] = value; }
        }



        /// <summary>
        /// Gets or sets the value to build chart.
        /// </summary>
        /// <value>
        /// The value to build chart.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ValueToBuildChart
        {
            get { return (Guid?) ViewState["AxisValue"]; }
            set { ViewState["AxisValue"] = value; }
        }
        


        /// <summary>
        /// Gets or sets the column value to build chart.
        /// </summary>
        /// <value>
        /// The column value to build chart.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string ColumnSystemNameValueToBuildChart
        {
            get { return (string)ViewState["ColumnSystemNameValueToBuildChart"]; }
            set { ViewState["ColumnSystemNameValueToBuildChart"] = value; }
        }



        /// <summary>
        /// Gets or sets the X axis.
        /// </summary>
        /// <value>
        /// The X axis.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? XAxis
        {
            get { return (Guid?)ViewState["XAxis"]; }
            set { ViewState["XAxis"] = value; }
        }


        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Unit Height
        {
            get { return rcChart.Height; }
            set { rcChart.Height = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Title
        {
            set { lrlTitle.Text = string.Format("<h3>{0}</h3>", value); }
        }


        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rcChart.HttpHandlerUrl = ResolveUrl("~/ChartImage.axd");

            plPeriods1.Visible = plPeriods2.Visible = ShowDatePeriod;

            rtsFilters.Tabs.Clear();

            foreach (var item in EnumHelper.EnumToList<DateTimePeriod>())
            {
                rtsFilters.Tabs.Add(new RadTab()
                                        {
                                            Text =
                                                EnumHelper.GetEnumDescription(
                                                    (Enum) Enum.Parse(typeof (DateTimePeriod), item.ToString())),
                                            Value =
                                                ((int) Enum.Parse(typeof (DateTimePeriod), item.ToString())).ToString()
                                        });
            }

            if (!Page.IsPostBack)
                BindData();
        }


        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        public void BindData(DateTime? startDate = null, DateTime? endDate = null)
        {
            if (!AnalyticReportId.HasValue)
                return;

            var reportUserSettings =
                DataManager.AnalyticReportUserSettings.SelectByAnalyticReportId(CurrentUser.Instance.ID,
                                                                                (Guid) AnalyticReportId);

            if (IsLoadFromSettings && Page != null && !Page.IsPostBack && !string.IsNullOrEmpty(UserSettingsClassName))
            {                                
                var userSettings = DataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID,
                                                                              UserSettingsClassName + AnalyticReportId);
                if (userSettings != null && !string.IsNullOrEmpty(userSettings.UserSettings))
                {                
                    var settings = userSettings.UserSettings.Split('$');
                    CurrentChartSeriesType = (ChartSeriesType) int.Parse(settings[0]);
                    startDate = DateTime.Parse(settings[1]);
                    endDate = DateTime.Parse(settings[2]).AddDays(1);
                    rdpStartDate.SelectedDate = startDate;
                    rdpEndDate.SelectedDate = endDate.Value.AddDays(-1);

                    var tab = rtsFilters.Tabs.FindTabByValue(settings[3]);
                    if (tab != null)
                        rtsFilters.SelectedIndex = tab.Index;
                    else
                        rtsFilters.SelectedIndex = -1;

                    if (tab != null)
                    {
                        var stDate = DateTime.Now;
                        var enDate = DateTime.Now;
                        DateTimeHelper.GetPeriod((DateTimePeriod) int.Parse(rtsFilters.SelectedTab.Value), ref stDate,
                                                 ref enDate);
                        startDate = stDate;
                        endDate = enDate;
                        rdpStartDate.SelectedDate = startDate;
                        rdpEndDate.SelectedDate = endDate.Value.AddDays(-1);
                    }
                }
            }

            if (IsLoadFromSettings && reportUserSettings != null)
                AxisToBuildChart = reportUserSettings.AxisToBuildID;

            if (!startDate.HasValue && !endDate.HasValue)
            {
                var date = DateTime.Now;
                var firstDayOfWeek = DateTimeHelper.GetFirstDayOfWeek(date);
                startDate = firstDayOfWeek.Date;
                endDate = DateTime.Now.AddDays(1).Date;
            }

            rcChart.DefaultType = CurrentChartSeriesType;

            if (Width.HasValue)
                rcChart.Width = new System.Web.UI.WebControls.Unit(((int) Width), UnitType.Pixel);

            var dataManager = new BusinessLogicLayer.DataManager();
            var analyticReport = dataManager.AnalyticReport.SelectById(CurrentUser.Instance.SiteID, (Guid) AnalyticReportId);

            rdpStartDate.SelectedDate = startDate;
            rdpEndDate.SelectedDate = endDate.Value.AddDays(-1);

            rcChart.Clear();
            rcChart.ChartTitle.TextBlock.Text = string.Empty;
            rcChart.Series.Clear();

            var analyticReportBase =
                dataManager.AnalyticReportSystem.SelectByAnalyticReportId(analyticReport.ID).ToList();


            var analyticReportYAxis = analyticReportBase.Where(o => o.AxisTypeID == (int) AxisType.YAxis &&
                                                                    (o.tbl_AnalyticAxis.AxisRoleID ==
                                                                     (int) AxisRole.IsLegend ||
                                                                     o.tbl_AnalyticAxis.AxisRoleID ==
                                                                     (int) AxisRole.IsFilterAndLegend)).ToList();

            if (analyticReportYAxis.Any() && !string.IsNullOrEmpty(analyticReportYAxis[0].tbl_AnalyticAxis.DataSet))
            {
                tbl_AnalyticAxis defaultYAxis = null;

                if (AxisToBuildChart.HasValue)
                    defaultYAxis = DataManager.AnalyticAxis.SelectById((Guid) AxisToBuildChart);
                else
                    defaultYAxis = analyticReportYAxis.FirstOrDefault(o => o.IsDefault).tbl_AnalyticAxis;

                var seriesValues = new List<string>();
                if (IsLoadFromSettings && reportUserSettings != null &&
                    !string.IsNullOrEmpty(reportUserSettings.DataSetValues))
                    seriesValues = reportUserSettings.DataSetValues.Split('#').ToList();

                tbl_AnalyticReportSystem valueAxis = null;

                if (!string.IsNullOrEmpty(ColumnSystemNameValueToBuildChart) && AnalyticReportId.HasValue)
                {
                    ValueToBuildChart = dataManager.AnalyticAxis.SelectByReportAndSystemName(AnalyticReportId.Value, ColumnSystemNameValueToBuildChart).ID;
                }

                if (ValueToBuildChart.HasValue && ValueToBuildChart.Value != Guid.Empty)
                    valueAxis = analyticReportBase.SingleOrDefault(o => o.tbl_AnalyticAxis.ID == ValueToBuildChart);
                else
                {
                    valueAxis =
                        analyticReportBase.FirstOrDefault(
                            x => x.tbl_AnalyticAxis.AxisRoleID == (int) AxisRole.Value && x.IsDefault);

                    if (valueAxis == null)
                        valueAxis =
                            analyticReportBase.FirstOrDefault(x => x.tbl_AnalyticAxis.AxisRoleID == (int) AxisRole.Value);
                }

                AnalyticSeriesList =
                    DataManager.AnalyticAxis.SelectSeriesByDataSet(defaultYAxis, CurrentUser.Instance.SiteID).Where(
                            o => seriesValues.Count == 0 || (seriesValues.Contains(o.Key))).
                        Select(o => new AnalyticSeries()
                                        {
                                            Title = o.Value,
                                            SystemNameValue = valueAxis.tbl_AnalyticAxis.SystemName,
                                            Dictionary = defaultYAxis.SystemName
                                        }).ToList();
            }
            else
            {
                AnalyticSeriesList =
                    analyticReportYAxis.Select(o => new AnalyticSeries() {Title = o.tbl_AnalyticAxis.Title, SystemNameValue = o.tbl_AnalyticAxis.SystemName}).ToList();
            }

            AnalyticXAxisList = new List<AnalyticAxis>();
            AnalyticYAxisList = new List<AnalyticAxis>();

            GetData(analyticReport, startDate, endDate);                        

            //Определение оси X
            AnalyticAxis xAxis = null;
            tbl_AnalyticReportSystem analyticReportSystem = null;

            if (XAxis.HasValue)
                analyticReportSystem = analyticReportBase.FirstOrDefault(o => o.AnalyticAxisID == (Guid)XAxis);
            else
                analyticReportSystem = analyticReportBase.FirstOrDefault(o => (o.AxisTypeID == (int)AxisType.XAxis && o.IsDefault));

            if (analyticReportSystem == null)
                analyticReportSystem = analyticReportBase.FirstOrDefault(o => o.AxisTypeID == (int)AxisType.XAxis);

            xAxis = new AnalyticAxis { SystemName = dataManager.AnalyticAxis.SelectById(analyticReportSystem.AnalyticAxisID).SystemName };


            foreach (var analyticSeries in AnalyticSeriesList)
            {                
                if (CurrentChartSeriesType != ChartSeriesType.Pie)
                {
                    var chartSeries = new ChartSeries
                                          {
                                              Name = analyticSeries.Title,
                                              Type = CurrentChartSeriesType,
                                              DefaultLabelValue = string.Empty
                                          };
                    chartSeries.Appearance.PointMark.Visible = true;
                    chartSeries.Appearance.PointMark.Dimensions.AutoSize = false;
                    chartSeries.Appearance.PointMark.Dimensions.Width = 4;
                    chartSeries.Appearance.PointMark.Dimensions.Height = 4;
                    chartSeries.Appearance.PointMark.FillStyle.FillType = FillType.Solid;
                    chartSeries.Appearance.PointMark.FillStyle.MainColor = Color.DodgerBlue;

                    rcChart.Series.Add(chartSeries);
                }

                AnalyticXAxisList.Add(xAxis);
                AnalyticYAxisList.Add(new AnalyticAxis() {SystemName = analyticSeries.SystemNameValue});
                //Для того чтобы в легенде не отображать надписи для которых нет данных
                AnalyticSeriesSum.Add(-1);
            }            

            rcChart.PlotArea.XAxis.Items.Clear();
            rcChart.PlotArea.YAxis.Items.Clear();

            if (CurrentChartSeriesType != ChartSeriesType.Pie)
                ProceedNonPieType(startDate.Value, endDate.Value, Data);
            else
                ProceedPieType(Data);

            var newSeries = rcChart.Series.ToArray();
            foreach (ChartSeries chartSeries in newSeries)
            {
                if (chartSeries.Items.Count == 0)
                    rcChart.Series.Remove(chartSeries);
            }

            if (Page != null &&
                !Page.ClientScript.IsStartupScriptRegistered(string.Concat("UpdateTypesBtn", AnalyticReportId)))
                ScriptManager.RegisterStartupScript(Page, typeof (Page),
                                                    string.Concat("UpdateTypesBtn", AnalyticReportId),
                                                    string.Format("UpdateTypesBtn($('#{0}'));",
                                                                  this.FindControl(CurrentChartSeriesType.ToString()).
                                                                      ClientID), true);
        }



        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="analyticReport">The analytic report.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        private void GetData(tbl_AnalyticReport analyticReport, DateTime? startDate, DateTime? endDate)
        {
            var query = analyticReport.tbl_Analytic.Query.Replace("#SiteID#", CurrentUser.Instance.SiteID.ToString());

            query = query.Replace("#StartDate#", ((DateTime)startDate).ToString("yyyy-MM-dd"))
                .Replace("#EndDate#", ((DateTime)endDate).ToString("yyyy-MM-dd"))
                .Replace("#GroupByDays#", (endDate - startDate).Value.Days == 1 ? "0" : "1");

            if (ReportFilters.Any())
            {
                var columns = new List<string>();
                var values = new List<string>();
                var operators = new List<int>();
                var queryString = string.Empty;

                foreach (var reportFilter in ReportFilters)
                {
                    if (string.IsNullOrEmpty(reportFilter.Query) && reportFilter.Operator != FilterOperator.None)
                    {
                        columns.Add(reportFilter.ColumnName);
                        values.Add(reportFilter.Value);
                        operators.Add((int) reportFilter.Operator);
                    }
                    queryString += reportFilter.Query;
                }

                query = query.Replace("#FilterColumnName#", string.Join(",", columns))
                    .Replace("#FilterColumnValue#", string.Join(",", values))
                    .Replace("#FilterOperator#", string.Join(",", operators))
                    .Replace("#FilterQuery#", queryString);
            }
            else
            {
                query = query.Replace("#FilterColumnName#", string.Empty)
                    .Replace("#FilterColumnValue#", string.Empty)
                    .Replace("#FilterOperator#", string.Empty)
                    .Replace("#FilterQuery#", string.Empty);
            }

            query = query.Replace("#FilterQuery#", string.Empty);

            Data = DataManager.AnalyticReport.SelectReportData(query);
        }


        /// <summary>
        /// Proceeds the type of the pie.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        private void ProceedPieType(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < AnalyticSeriesList.Count; i++)
                {
                    if (row[AnalyticSeriesList[i].Dictionary] != DBNull.Value &&
                        !string.IsNullOrEmpty(AnalyticSeriesList[i].Dictionary) &&
                        (string) row[AnalyticSeriesList[i].Dictionary] != AnalyticSeriesList[i].Title)
                        continue;

                    var yAxis = AnalyticYAxisList[i];
                    if (AnalyticSeriesSum[i] == -1) AnalyticSeriesSum[i] = 0;
                    AnalyticSeriesSum[i] += double.Parse(row[yAxis.SystemName].ToString());
                }
            }

            var chartSeries = new ChartSeries
                                  {
                                      Name = "",
                                      Type = CurrentChartSeriesType,
                                      DefaultLabelValue = string.Empty
                                  };

            rcChart.Series.Add(chartSeries);

            chartSeries.Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;

            for (int i = 0; i < AnalyticSeriesSum.Count; i++)
            {
                if (AnalyticSeriesSum[i] == -1)
                    continue;

                var chartSeriesItem = new ChartSeriesItem
                                          {YValue = AnalyticSeriesSum[i], Name = AnalyticSeriesList[i].Title};
                chartSeriesItem.Label.TextBlock.Text = AnalyticSeriesSum[i].ToString();
                chartSeries.Items.Add(chartSeriesItem);
            }
        }



        /// <summary>
        /// Proceeds the type of the non pie.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="dataTable">The data table.</param>
        private void ProceedNonPieType(DateTime startDate, DateTime endDate, DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0 || AnalyticSeriesList.Count == 0)
                return;            

            foreach (DataRow row in dataTable.Select("1=1", AnalyticXAxisList[0].SystemName + " ASC"))
            {
                for (int i = 0; i < AnalyticSeriesList.Count; i++)
                {
                    if (!string.IsNullOrEmpty(AnalyticSeriesList[i].Dictionary) &&
                        (row[AnalyticSeriesList[i].Dictionary] == DBNull.Value ||
                         (string) row[AnalyticSeriesList[i].Dictionary] != AnalyticSeriesList[i].Title))
                        continue;

                    var chartSeriesItem = new ChartSeriesItem();

                    double value = 0;
                    ProceedAxis(row, AnalyticXAxisList[i].SystemName, rcChart.PlotArea.XAxis, startDate, endDate, dataTable.Rows.Count, ref value);
                    if (value != -1) chartSeriesItem.XValue = value;

                    value = 0;
                    ProceedAxis(row, AnalyticYAxisList[i].SystemName, rcChart.PlotArea.YAxis, startDate, endDate, dataTable.Rows.Count, ref value);
                    
                    if (!(row[AnalyticXAxisList[i].SystemName] is DateTime))
                    {
                        if (AnalyticXAxisList[i].SystemName != "Period")
                        {
                            var rows =
                                dataTable.Select(string.Format("{0} = '{1}' AND {2} = '{3}'",
                                                               AnalyticXAxisList[i].SystemName,
                                                               row[AnalyticXAxisList[i].SystemName],
                                                               AnalyticSeriesList[i].Dictionary,
                                                               AnalyticSeriesList[i].Title));
                            if (rows.Count() > 1)
                            {
                                value = rows.Sum(dataRow => double.Parse(dataRow[AnalyticSeriesList[i].SystemNameValue].ToString()));
                            }
                        }
                    }

                    if (!(row[AnalyticYAxisList[i].SystemName] is DateTime))
                    {
                        if (AnalyticYAxisList[i].SystemName != "Period" && !string.IsNullOrEmpty(AnalyticSeriesList[i].Dictionary))
                        {
                            var rows = dataTable.Select(string.Format("{0} = '{1}' AND {2} = '{3}'",
                                                               AnalyticSeriesList[i].Dictionary,
                                                               AnalyticSeriesList[i].Title,
                                                               AnalyticXAxisList[i].SystemName,
                                                               row[AnalyticXAxisList[i].SystemName]));
                            if (rows.Count() > 1)                            
                                value = rows.Sum(dataRow => double.Parse(dataRow[AnalyticSeriesList[i].SystemNameValue].ToString()));                            
                        }
                    }

                    if (value != -1)
                        chartSeriesItem.YValue = value;
                    rcChart.Series[i].Items.Add(chartSeriesItem);
                }
            }            
        }



        /// <summary>
        /// Proceeds the axis.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="systemName">Name of the system.</param>
        /// <param name="axis">The axis.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="value">The value.</param>
        protected void ProceedAxis(DataRow row, string systemName, ChartAxis axis, DateTime startDate, DateTime endDate, int rowsCount,
                                   ref double value)
        {
            if (row[systemName] is DateTime)
            {
                value = ((DateTime) row[systemName]).ToOADate();
                axis.AutoScale = true;

                if (rowsCount == 1)
                {
                    axis.AutoScale = false;
                    axis.MinValue = value;
                    axis.MaxValue = value;
                }

                axis.IsZeroBased = false;
                axis.Appearance.ValueFormat = ChartValueFormat.ShortDate;
                axis.Appearance.CustomFormat = "dd.MM.yy";
                axis.Appearance.LabelAppearance.RotationAngle = 90;
                if ((endDate - startDate).Days <= 7)
                {
                    axis.AutoScale = false;
                    axis.MinValue = startDate.ToOADate();
                    axis.MaxValue = endDate.AddDays(-1).ToOADate();
                }
            }
            else if (row[systemName] is int || row[systemName] is double || row[systemName] is decimal)
            {
                axis.Appearance.LabelAppearance.RotationAngle = 0;
                value = double.Parse(row[systemName].ToString());
                if (systemName == "Period" && (endDate - startDate).Days == 1)
                {
                    axis.AutoScale = false;
                    axis.MinValue = 0;
                    axis.MaxValue = 23;                    

                    axis.Appearance.LabelAppearance.RotationAngle = 90;
                }
                axis.IsZeroBased = true;
                axis.Appearance.ValueFormat = ChartValueFormat.None;
                axis.Appearance.CustomFormat = "";
            }
            else
            {
                axis.AutoScale = false;
                axis.Appearance.LabelAppearance.RotationAngle = 90;

                var text = (string.IsNullOrEmpty(row[systemName].ToString())
                                ? "Не определено"
                                : row[systemName].ToString());

                if (!axis.Items.Any(o => o.TextBlock.Text == text))
                {
                    var chartAxisItem = new ChartAxisItem(string.IsNullOrEmpty(row[systemName].ToString())
                                                              ? "Не определено"
                                                              : row[systemName].ToString());

                    axis.AddItem(chartAxisItem);
                }

                axis.IsZeroBased = true;
                axis.Appearance.ValueFormat = ChartValueFormat.None;
                axis.Appearance.CustomFormat = "";

                var item = axis.Items.SingleOrDefault(o => o.TextBlock.Text == text);
                item.Value = axis.Items.IndexOf(item);
                value = (double) item.Value;
            }
        }



        /// <summary>
        /// Handles the OnTabClick event of the rtsFilters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTabStripEventArgs"/> instance containing the event data.</param>
        protected void rtsFilters_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            var date = DateTime.Now.Date;
            var startDate = date;
            var endDate = date;

            DateTimeHelper.GetPeriod((DateTimePeriod) int.Parse(rtsFilters.SelectedTab.Value), ref startDate,
                                     ref endDate);

            BindData(startDate, endDate);

            SaveUserSettings();
        }




        /// <summary>
        /// Handles the OnSelectedDateChanged event of the rdpDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (rdpStartDate.SelectedDate.HasValue && rdpEndDate.SelectedDate.HasValue &&
                rdpEndDate.SelectedDate.Value >= rdpStartDate.SelectedDate.Value)
            {
                BindData(rdpStartDate.SelectedDate, rdpEndDate.SelectedDate.Value.AddDays(1));
                rtsFilters.SelectedIndex = -1;

                SaveUserSettings();
            }
        }



        /// <summary>
        /// Handles the OnClick event of the btnChangeChartType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void btnChangeChartType_OnClick(object sender, EventArgs e)
        {
            var linkButton = (LinkButton) sender;
            switch (linkButton.CommandArgument)
            {
                case "Pie":
                    CurrentChartSeriesType = ChartSeriesType.Pie;
                    break;
                case "Area":
                    CurrentChartSeriesType = ChartSeriesType.Area;
                    break;
                case "Bar":
                    CurrentChartSeriesType = ChartSeriesType.Bar;
                    break;
                case "Line":
                    CurrentChartSeriesType = ChartSeriesType.Line;
                    break;
            }

            if (CurrentChartSeriesType != rcChart.DefaultType)
                BindData(rdpStartDate.SelectedDate, rdpEndDate.SelectedDate.Value.AddDays(1));

            SaveUserSettings();
        }



        /// <summary>
        /// Saves the user settings.
        /// </summary>
        public void SaveUserSettings()
        {
            var userSettings = DataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID,
                                                                          UserSettingsClassName + AnalyticReportId);
            if (userSettings == null)
            {
                userSettings = new tbl_UserSettings
                                   {
                                       UserID = CurrentUser.Instance.ID,
                                       ClassName = UserSettingsClassName + AnalyticReportId,
                                       ShowAlternativeControl = false,
                                       ShowFilterPanel = false,
                                       ShowGroupPanel = false
                                   };
            }

            userSettings.UserSettings = string.Concat((int) CurrentChartSeriesType, "$", rdpStartDate.SelectedDate.Value,
                                                      "$", rdpEndDate.SelectedDate.Value, "$",
                                                      rtsFilters.SelectedTab != null
                                                          ? rtsFilters.SelectedTab.Value
                                                          : string.Empty);

            DataManager.UserSettings.Save(userSettings);
        }
    }


    public class AnalyticSeries
    {
        public string Title { get; set; }
        public string SystemNameValue { get; set; }
        public string Dictionary { get; set; }
    }


    public class AnalyticAxis
    {
        public string SystemName { get; set; }
    }
}