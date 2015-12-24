<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analytics.aspx.cs" Inherits="WebCounter.AdminPanel.Analytics" %>
<%@ Register TagPrefix="uc" TagName="Chart" Src="~/UserControls/Analytics/Chart.ascx" %>
<%@ Register TagPrefix="uc" TagName="TableReport" Src="~/UserControls/Analytics/TableReport.ascx" %>
<%@ Register TagPrefix="uc" TagName="AnalyticReportFilters" Src="~/UserControls/Analytics/AnalyticReportFilters.ascx" %>
<%@ Register TagPrefix="uc" TagName="AnalyticReportAxisValue" Src="~/UserControls/Analytics/AnalyticReportAxisValue.ascx" %>
<%@ Register TagPrefix="uc" TagName="XAxis" Src="~/UserControls/Analytics/XAxis.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#hfWidth').val($(document).width());            
        });
        $(window).load(function () {
            var panel = $find('<%= rpbReports.ClientID %>');
            var item = panel.findItemByValue('<%= CurrentItem %>');
            item.click();
        });
        function OnClientItemClicking(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function pnlRequestStarted(ajaxPanel, eventArgs) {                
                if (eventArgs.EventTarget.indexOf('ExportToExcelButton') != -1) {
                    eventArgs.EnableAjax = false;
                }
            }
        </script>
    </telerik:RadScriptBlock>    

    <telerik:RadAjaxPanel runat="server" ClientEvents-OnRequestStart="pnlRequestStarted">        
        <asp:HiddenField runat="server" ID="hfWidth" ClientIDMode="Static" />
        <table width="100%" class="table-analytic">
            <tr>
                <td width="189px" valign="top" class="aside">
                    <telerik:RadPanelBar ID="rpbReports" Width="189px" Skin="Windows7" runat="server" OnItemClick="rpbReports_OnItemClick" />
                    <uc:LeftColumn runat="server" />
                </td>
                <td width="100%" valign="top">
                    <h1><asp:Literal runat="server" ID="lrlReportTitle" /></h1>                    
                    <telerik:RadPanelBar ID="RadPanelBar1" CssClass="not-expandable" runat="server" Width="100%" Skin="Windows7" OnClientItemClicking="OnClientItemClicking">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Условия отчета">
                                <ContentTemplate>
                                    <div class="date-picker-container">
                                        <asp:Panel runat="server" ID="plPeriods">                                        
                                            <telerik:RadDatePicker runat="server" Width="95px" ShowPopupOnFocus="true" ID="rdpStartDate" Skin="Windows7" AutoPostBack="true" OnSelectedDateChanged="rdpDate_OnSelectedDateChanged">
                                            </telerik:RadDatePicker> 
                                            <span>&nbsp;-</span>
                                            <telerik:RadDatePicker runat="server" Width="95px" ShowPopupOnFocus="true" ID="rdpEndDate" Skin="Windows7" AutoPostBack="true" OnSelectedDateChanged="rdpDate_OnSelectedDateChanged">
                                            </telerik:RadDatePicker>                                            
                                            <asp:Panel runat="server" ID="plPeriodButtons" CssClass="period-buttons">
                                            </asp:Panel>
                                        </asp:Panel>                                    
                                        <uc:AnalyticReportFilters runat="server" ID="ucAnalyticReportFilters" />
                                        <uc:AnalyticReportAxisValue runat="server" ID="ucAnalyticReportAxisValue" />                                        
                                        <uc:XAxis runat="server" ID="ucXAxis" />
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <br/>
                    <telerik:RadPanelBar ID="RadPanelBar2" runat="server" Width="100%" Skin="Windows7">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Данные">
                                <ContentTemplate>
                                    <uc:TableReport runat="server" ID="ucTableReport" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <br/>
                    <telerik:RadPanelBar ID="RadPanelBar3" runat="server" Width="100%" Skin="Windows7">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="График">
                                <ContentTemplate>
                                    <uc:Chart runat="server" ID="ucChart" ShowDatePeriod="false" Visible="false" Height="700px" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>                    
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
