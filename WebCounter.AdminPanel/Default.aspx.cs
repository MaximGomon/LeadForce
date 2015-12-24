using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.AdminPanel.UserControls.Analytics;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;
using UnitConverter = WebCounter.BusinessLogicLayer.Common.UnitConverter;

namespace WebCounter.AdminPanel
{
    public partial class Default : LeadForceBasePage
    {
        protected RadAjaxManager AjaxManager = null;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.Url.ToString().ToLower().Contains("leadforce/main/list"))                           
                    Response.Redirect(UrlsData.AP_Home());            

            Title = "LeadForce";
            
            AjaxManager = RadAjaxManager.GetCurrent(Page);
            AjaxManager.AjaxSettings.AddAjaxSetting(AjaxManager, rcbReports, null, UpdatePanelRenderMode.Inline);
            AjaxManager.AjaxSettings.AddAjaxSetting(AjaxManager, txtName, null, UpdatePanelRenderMode.Inline);
            AjaxManager.AjaxSettings.AddAjaxSetting(rcbReports, txtName, null, UpdatePanelRenderMode.Inline);
            AjaxManager.AjaxSettings.AddAjaxSetting(rcbReports, plSelectAxis, null, UpdatePanelRenderMode.Inline);
            AjaxManager.AjaxSettings.AddAjaxSetting(rcbReports, plSelectedAxisValues, null, UpdatePanelRenderMode.Inline);
            AjaxManager.AjaxSettings.AddAjaxSetting(ddlSelectAxis, chxlSeries, null, UpdatePanelRenderMode.Inline);
            
            RadAjaxManager.GetCurrent(Page).AjaxRequest += Default_AjaxRequest;
            
            var showDomainStatusSettings = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "ShowDomainStatusSettings");
            if (showDomainStatusSettings == null && !((LeadForceBasePage)Page).IsDefaultEdition)
            {
                plShowDomainStatusSettings.Visible = false;
            }
            else
            {
                var siteDomain = DataManager.SiteDomain.SelectBySiteId(CurrentUser.Instance.SiteID);
                plShowDomainStatusSettings.Visible = ucNotificationMessage.Visible = siteDomain == null;
                if (ucNotificationMessage.Visible)
                {
                    var site = DataManager.Sites.SelectById(CurrentUser.Instance.SiteID);
                    if (site != null)
                        ucNotificationMessage.Text =
                            string.Format(
                                "Для начала работы Вам необходимо привязать {0} к домену. Для этого укажите Ваш домен в форме ниже и нажмите кнопку [Проверить и сохранить]",
                                site.Name);

                    if (siteDomain != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            txtDomain.Text = siteDomain.Domain;
                            txtAliases.Text = siteDomain.Aliases;
                            lrlNote.Text = siteDomain.Note;
                        }
                    }
                }                
            }

            if (!string.IsNullOrEmpty(Request.QueryString["tab"]) && Request.QueryString["tab"] == "chart")
            {
                RadTabStrip1.Tabs[1].Selected = true;
                RadMultiPage1.PageViews[1].Selected = true;
                plCharts.Visible = true;
            }
        }



        /// <summary>
        /// Handles the AjaxRequest event of the Default control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void Default_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            var dockStates = radDockLayout.GetRegisteredDocksState();

            if (e.Argument == "Close")
            {
                var firstOrDefault = dockStates.FirstOrDefault(o => o.Closed);
                if (firstOrDefault != null)
                {
                    radDockZone.Docks.Remove(radDockLayout.FindControl(firstOrDefault.UniqueName) as RadDock);
                    var userSettings = DataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID, "WebCounter.AdminPanel.HomePageChart" + firstOrDefault.UniqueName);
                    if (userSettings != null)
                    {
                        userSettings.UserSettings = string.Empty;
                        DataManager.UserSettings.Save(userSettings);
                    }                    
                }
            }

            if (e.Argument == "Move" || e.Argument == "Close")
                SaveSettings();

            if (e.Argument == "InitRadWindow")
            {                
                var serializer = new JavaScriptSerializer();
                var converters = new List<JavaScriptConverter> { new UnitConverter() };
                serializer.RegisterConverters(converters);

                var reports = DataManager.AnalyticReport.SelectAll(CurrentUser.Instance.ID);
                var existsReports = dockStates.Where(o => !o.Closed).Select(state => Guid.Parse(state.UniqueName)).ToList();
                rcbReports.ClearSelection();
                txtName.Text = string.Empty;
                rcbReports.SelectedValue = string.Empty;
                rcbReports.Text = string.Empty;
                reports = reports.Where(o => !existsReports.Contains(o.ID));

                rcbReports.DataSource = reports;
                rcbReports.DataBind();                
            }
        }



        /// <summary>
        /// Handles the OnLoadDockLayout event of the radDockLayout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.DockLayoutEventArgs"/> instance containing the event data.</param>
        protected void radDockLayout_OnLoadDockLayout(object sender, DockLayoutEventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] != "chart") return;

            radDockZone.Controls.Clear();

            var userSettings = DataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID,
                                                                          "WebCounter.AdminPanel.HomePageWidgets");

            var serializer = new JavaScriptSerializer();
            var converters = new List<JavaScriptConverter> {new UnitConverter()};
            serializer.RegisterConverters(converters);
            var reportInSettings = new List<Guid>();

            if (userSettings != null)
                reportInSettings.AddRange(from str in userSettings.UserSettings.Split('|')
                                          where str != String.Empty
                                          select serializer.Deserialize<DockState>(str)
                                          into state
                                          where !state.Closed
                                          select Guid.Parse(state.UniqueName));

            var reports = DataManager.AnalyticReport.SelectAll(CurrentUser.Instance.ID);
            foreach (var analyticReport in reports)
            {
                if (reportInSettings.Count == 0 ||
                    (reportInSettings.Count > 0 && !reportInSettings.Contains(analyticReport.ID)))
                    continue;

                AddReport(analyticReport);
            }

            if (radDockZone.Controls.Count == 0)
            {
                radDockLayout.Visible = false;
                plEmptyMessage.Visible = true;
            }
            else
            {
                radDockLayout.Visible = true;
                plEmptyMessage.Visible = false;
            }

            if (userSettings == null)
                return;

            foreach (string str in userSettings.UserSettings.Split('|'))
            {
                if (str != String.Empty)
                {
                    var state = serializer.Deserialize<DockState>(str);
                    var dock = radDockLayout.FindControl(state.UniqueName) as RadDock;
                    if (dock != null) dock.ApplyState(state);
                    e.Positions[state.UniqueName] = state.DockZoneID;
                    e.Indices[state.UniqueName] = state.Index;
                }
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rcbReports control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemEventArgs"/> instance containing the event data.</param>
        protected void rcbReports_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var text = string.Empty;
            if (!string.IsNullOrEmpty(((tbl_AnalyticReport)e.Item.DataItem).Title) && !string.IsNullOrEmpty(((tbl_AnalyticReport)e.Item.DataItem).Title.Trim()))
                text += ((tbl_AnalyticReport)e.Item.DataItem).Title + " | ";
            if (!string.IsNullOrEmpty(((tbl_AnalyticReport)e.Item.DataItem).tbl_Module.Title) && !string.IsNullOrEmpty(((tbl_AnalyticReport)e.Item.DataItem).tbl_Module.Title.Trim()))
                text += ((tbl_AnalyticReport)e.Item.DataItem).tbl_Module.Title + " | ";
            if (!string.IsNullOrEmpty(((tbl_AnalyticReport)e.Item.DataItem).Description) && !string.IsNullOrEmpty(((tbl_AnalyticReport)e.Item.DataItem).Description.Trim()))
                text += ((tbl_AnalyticReport)e.Item.DataItem).Description + " | ";

            text = text.Trim(new[] { ' ', '|' });

            e.Item.Text = text;
            e.Item.Value = ((tbl_AnalyticReport)e.Item.DataItem).ID.ToString();    
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rcbReports control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void rcbReports_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(rcbReports.SelectedValue))
            {
                txtName.Text = rcbReports.Text.Split('|')[0];
                var analyticFilters =
                    DataManager.AnalyticReportSystem.SelectFiltersByAnalyticReportId(Guid.Parse(rcbReports.SelectedValue));

                var changeAxis = (from analyticFilter in analyticFilters
                                  where !string.IsNullOrEmpty(analyticFilter.tbl_AnalyticAxis.DataSet)
                                  select analyticFilter.tbl_AnalyticAxis).ToList();

                if (changeAxis.Any())
                {
                    ddlSelectAxis.DataSource = changeAxis;
                    ddlSelectAxis.DataTextField = "Title";
                    ddlSelectAxis.DataValueField = "ID";
                    ddlSelectAxis.DataBind();
                    plSelectAxis.Visible = true;
                    plSelectedAxisValues.Visible = true;
                    ddlSelectAxis.SelectedIndex = 0;
                    RefreshSeriesList();
                }
                else
                {
                    plSelectAxis.Visible = false;
                    plSelectedAxisValues.Visible = false;
                }
            }
            else
            {
                txtName.Text = string.Empty;
                plSelectAxis.Visible = false;
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnAdd_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            var analyticReportId = Guid.Parse(rcbReports.SelectedValue);
            var analyticReport = DataManager.AnalyticReport.SelectById(CurrentUser.Instance.SiteID, analyticReportId);

            if (plSelectAxis.Visible)
            {
                Guid? axisToBuild = Guid.Parse(ddlSelectAxis.SelectedValue);
                var reportUserSettings = DataManager.AnalyticReportUserSettings.SelectByAnalyticReportId(CurrentUser.Instance.ID, analyticReportId) ?? new tbl_AnalyticReportUserSettings();
                
                reportUserSettings.AxisToBuildID = axisToBuild;

                if (plSelectedAxisValues.Visible)
                {
                    var selectedItems = (from ListItem listItem in chxlSeries.Items where listItem.Selected select listItem.Value).ToList();
                    reportUserSettings.DataSetValues = string.Join("#", selectedItems);                    
                }

                if (reportUserSettings.ID == Guid.Empty)
                {
                    reportUserSettings.UserID = CurrentUser.Instance.ID;
                    reportUserSettings.AnalyticReportID = analyticReportId;
                    DataManager.AnalyticReportUserSettings.Add(reportUserSettings);
                }
                else                
                    DataManager.AnalyticReportUserSettings.Update(reportUserSettings);                
            }

            RadDock radDock = null;

            radDock = (RadDock)radDockLayout.FindControl(analyticReportId.ToString());
            if (radDock == null)
            {                

                radDock = AddReport(analyticReport);
                radDockLayout.GetRegisteredDocksState().Add(radDock.GetState());
            }

            radDock.GetState().Closed = false;
            radDock.Title = txtName.Text;
            
            SaveSettings();

            
            Response.Redirect(UrlsData.AP_Home() + "?tab=chart");
        }



        /// <summary>
        /// Saves the settings.
        /// </summary>
        protected void SaveSettings()
        {
            var dockStates = radDockLayout.GetRegisteredDocksState();
            var serializer = new JavaScriptSerializer();
            var converters = new List<JavaScriptConverter> { new UnitConverter() };
            serializer.RegisterConverters(converters);


            string stateString = String.Empty;
            foreach (DockState state in dockStates)
            {
                string ser = serializer.Serialize(state);
                stateString = stateString + "|" + ser;
            }
            var userSettings = DataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID,
                                                                            "WebCounter.AdminPanel.HomePageWidgets");
            if (userSettings == null)
            {
                userSettings = new tbl_UserSettings
                {
                    UserID = CurrentUser.Instance.ID,
                    ClassName = "WebCounter.AdminPanel.HomePageWidgets",
                    ShowAlternativeControl = false,
                    ShowFilterPanel = false,
                    ShowGroupPanel = false
                };
            }
            userSettings.UserSettings = stateString;
            DataManager.UserSettings.Save(userSettings);
        }



        /// <summary>
        /// Adds the report.
        /// </summary>
        /// <param name="analyticReport">The analytic report.</param>
        /// <returns></returns>
        protected RadDock AddReport(tbl_AnalyticReport analyticReport)
        {            
            var radDock = new RadDock
            {
                Skin = "Windows7",
                Width = new Unit(430, UnitType.Pixel),
                EnableAnimation = true,
                Title = analyticReport.Title,
                ID = analyticReport.ID.ToString()
            };

            var chart = (Chart)LoadControl("~/UserControls/Analytics/Chart.ascx");
            chart.UserSettingsClassName = "WebCounter.AdminPanel.HomePageChart";
            chart.IsLoadFromSettings = true;
            chart.AnalyticReportId = analyticReport.ID;            
            radDock.ContentContainer.Controls.Add(chart);                        
            radDock.OnClientDockPositionChanged = "RadDockPositionChanged";
            radDock.OnClientCommand = "RadDockClientCommand";
            radDockZone.Controls.Add(radDock);            

            return radDock;            
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlSelectAxis control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlSelectAxis_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSeriesList();
        }



        /// <summary>
        /// Refreshes the series list.
        /// </summary>
        protected void RefreshSeriesList()
        {
            chxlSeries.Items.Clear();
            var axis = DataManager.AnalyticAxis.SelectById(Guid.Parse(ddlSelectAxis.SelectedValue));
            if (axis != null)
            {
                chxlSeries.DataSource = DataManager.AnalyticAxis.SelectSeriesByDataSet(axis, CurrentUser.Instance.SiteID);
                chxlSeries.DataValueField = "Key";
                chxlSeries.DataTextField = "Value";
                chxlSeries.DataBind();
                
                foreach (ListItem item in chxlSeries.Items)                
                    item.Selected = true;
                
                plSelectedAxisValues.Visible = true;
            }
            else            
                plSelectedAxisValues.Visible = false;            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSaveAndCheckDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSaveAndCheckDomain_OnClick(object sender, EventArgs e)
        {
            ucNotificationMessageDomainExists.Text = string.Empty;

            var siteDomainId = Guid.Empty;

            var result = DataManager.SiteDomain.Save(SiteId, txtDomain.Text, txtAliases.Text, true, ref siteDomainId);

            if (!string.IsNullOrEmpty(result))
            {
                ucNotificationMessage.Text = result;
                return;
            }

            Session["RunCheck"] = "true";
            Response.Redirect(UrlsData.AP_SiteDomainsEdit("Settings", siteDomainId, "Settings"));            
        }
    }
}