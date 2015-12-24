using System;
using System.Linq;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Analytics : LeadForceBasePage
    {
        protected Guid CurrentItem = Guid.Empty;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Аналитика - LeadForce";            

            foreach (var item in EnumHelper.EnumToList<DateTimePeriod>())
            {
                var radButton = new RadButton
                                    {
                                        ToggleType = ButtonToggleType.Radio,
                                        ButtonType = RadButtonType.LinkButton,
                                        GroupName = "Periods",
                                        AutoPostBack = true,
                                        Skin = "Windows7"
                                    };
                var text = EnumHelper.GetEnumDescription((Enum) Enum.Parse(typeof (DateTimePeriod), item.ToString()));
                radButton.ToggleStates.Add(new RadButtonToggleState(text));
                radButton.ToggleStates.Add(new RadButtonToggleState(text));
                radButton.CommandArgument = ((int) Enum.Parse(typeof (DateTimePeriod), item.ToString())).ToString();
                radButton.Click += radButton_Click;              
                radButton.Checked = !Page.IsPostBack && item == DateTimePeriod.Week;

                plPeriodButtons.Controls.Add(radButton);
            }

            UpdatePeriodButtons(DateTimePeriod.Week);

            ucAnalyticReportFilters.ReportFilterChanged += ucAnalyticReportFilters_ReportFilterChanged;
            ucAnalyticReportFilters.ReportFilterAxisChanged += ucAnalyticReportFilters_ReportFilterAxisChanged;
            ucAnalyticReportAxisValue.ReportAxisValueChanged += ucAnalyticReportAxisValue_ReportAxisValueChanged;
            ucXAxis.XAxisChanged += ucXAxis_XAxisChanged;

            if (!Page.IsPostBack)            
                BindData();            
        }



        /// <summary>
        /// Ucs the X axis_ X axis changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void ucXAxis_XAxisChanged(object sender)
        {
            RefreshReports();
        }



        /// <summary>
        /// Ucs the analytic report axis value_ report axis value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void ucAnalyticReportAxisValue_ReportAxisValueChanged(object sender)
        {
            RefreshReports();
        }



        /// <summary>
        /// Ucs the analytic report filters_ report filter axis changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void ucAnalyticReportFilters_ReportFilterAxisChanged(object sender)
        {
            RefreshReports();
        }



        /// <summary>
        /// Ucs the analytic report filters_ report filter changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void ucAnalyticReportFilters_ReportFilterChanged(object sender)
        {
            RefreshReports();
        }



        /// <summary>
        /// Handles the Click event of the radButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void radButton_Click(object sender, EventArgs e)
        {
            var radButton = (RadButton) sender;

            var date = DateTime.Now.Date;
            var startDate = date;
            var endDate = date;

            DateTimeHelper.GetPeriod((DateTimePeriod)int.Parse(radButton.CommandArgument), ref startDate, ref endDate);

            rdpStartDate.SelectedDate = startDate;
            rdpEndDate.SelectedDate = endDate.AddDays(-1);

            RefreshReports();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var reports = DataManager.AnalyticReport.SelectAll(CurrentUser.Instance.ID);
            var modules = reports.Select(o => new {o.ModuleID, o.tbl_Module.Title}).Distinct().OrderBy(o => o.Title);

            foreach (var module in modules)
            {
                var radPanelItem = new RadPanelItem {Text = module.Title, Expanded = true };

                var reportsByModule = reports.Where(o => o.ModuleID == module.ModuleID).OrderBy(o => o.Title);
                foreach (var analyticReport in reportsByModule)
                {
                    if (CurrentItem == Guid.Empty)
                        CurrentItem = analyticReport.ID;

                    var childRadPanelItem = new RadPanelItem { Text = analyticReport.Title, Value = analyticReport.ID.ToString() };                    
                    radPanelItem.Items.Add(childRadPanelItem);
                }

                rpbReports.Items.Add(radPanelItem);
            }   
         
            if (!modules.Any())
            {
                RadPanelBar1.Visible = false;
                RadPanelBar2.Visible = false;
                RadPanelBar3.Visible = false;
            }
        }



        /// <summary>
        /// Handles the OnItemClick event of the rpbReports control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadPanelBarEventArgs"/> instance containing the event data.</param>
        protected void rpbReports_OnItemClick(object sender, RadPanelBarEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Item.Value))
            {
                if (!rdpStartDate.SelectedDate.HasValue || !rdpEndDate.SelectedDate.HasValue)
                {
                    var date = DateTime.Now;
                    var firstDayOfWeek = DateTimeHelper.GetFirstDayOfWeek(date);
                    rdpStartDate.SelectedDate = firstDayOfWeek.Date;
                    rdpEndDate.SelectedDate = DateTime.Now.AddDays(1).Date;
                }

                var analyticReportId = Guid.Parse(e.Item.Value);
                
                lrlReportTitle.Text = e.Item.Text;
                ucChart.Visible = true;
                ucAnalyticReportFilters.ClearFilters();
                ucAnalyticReportFilters.AxisToBuildChart = null;
                ucAnalyticReportFilters.AnalyticReportId = analyticReportId;
                ucXAxis.AnalyticReportId = analyticReportId;
                ucXAxis.BindData();
                ucAnalyticReportFilters.BindData();
                ucAnalyticReportAxisValue.ValueToBuildChart = null;
                ucAnalyticReportAxisValue.AnalyticReportId = analyticReportId;
                ucAnalyticReportAxisValue.BindData();
                ucChart.AnalyticReportId = analyticReportId;
                ucTableReport.AnalyticReportId = analyticReportId;
                if (!string.IsNullOrEmpty(hfWidth.Value))                
                    ucChart.Width = int.Parse(hfWidth.Value) - 280;
                
                var analyticReportSystems = DataManager.AnalyticReportSystem.SelectByAnalyticReportId(analyticReportId);
                plPeriods.Visible = analyticReportSystems.Any(o => o.tbl_AnalyticAxis.SystemName == "Period");

                RefreshReports();
            }
        }



        /// <summary>
        /// Handles the OnSelectedDateChanged event of the rdpDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (rdpStartDate.SelectedDate.HasValue && rdpEndDate.SelectedDate.HasValue)
            {
                RefreshReports();

                UpdatePeriodButtons();
            }
        }



        /// <summary>
        /// Refreshes the reports.
        /// </summary>
        protected void RefreshReports()
        {
            ucChart.ValueToBuildChart = ucAnalyticReportAxisValue.ValueToBuildChart;
            ucChart.ReportFilters = ucAnalyticReportFilters.ReportFilters;
            ucChart.AxisToBuildChart = ucAnalyticReportFilters.AxisToBuildChart;
            ucChart.XAxis = ucXAxis.XAxisToBuildChart;
            ucChart.BindData(rdpStartDate.SelectedDate, rdpEndDate.SelectedDate.Value.AddDays(1));
            ucTableReport.Data = ucChart.Data;
            ucTableReport.BindData();
        }



        /// <summary>
        /// Updates the period buttons.
        /// </summary>
        /// <param name="period">The period.</param>
        protected void UpdatePeriodButtons(DateTimePeriod? period = null)
        {
            if (period.HasValue)
            {
                if (!plPeriodButtons.Controls.OfType<RadButton>().Any(radButton => radButton.Checked))
                    foreach (var radButton in plPeriodButtons.Controls.OfType<RadButton>())
                        radButton.Checked = (DateTimePeriod) int.Parse(radButton.CommandArgument) == period;
            }
            else
                foreach (var button in plPeriodButtons.Controls.OfType<RadButton>())
                    button.Checked = false;
        }
    }
}