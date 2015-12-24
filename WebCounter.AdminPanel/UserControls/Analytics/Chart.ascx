<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Chart.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Analytics.Chart" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function UpdateTypesBtn(element) {            
            $(element).parent().find('a').removeAttr('class');
            $(element).attr('class', 'selected');
        }
    </script>
</telerik:RadScriptBlock>

<div class="inner-raddock">
    <telerik:RadAjaxPanel runat="server">
        <asp:Literal runat="server" ID="lrlTitle" />        
        <table width="100%">
            <tr>
                <td align="left" valign="middle">
                    <div class="chart-types clearfix">
                        <asp:LinkButton runat="server" ID="Pie" CommandArgument="Pie" OnClick="btnChangeChartType_OnClick">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Default/images/icoPieChart.png" />
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="Bar" CommandArgument="Bar" OnClick="btnChangeChartType_OnClick">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Default/images/icoBarChart.png" />
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" CssClass="selected" ID="Line" CommandArgument="Line" OnClick="btnChangeChartType_OnClick">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Default/images/icoLineChart.png" />
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="Area" CommandArgument="Area" OnClick="btnChangeChartType_OnClick">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Default/images/icoAreaChart.png" />
                        </asp:LinkButton>            
                    </div>
                </td>
                <td align="right" valign="middle">
                    <asp:Panel runat="server" ID="plPeriods1">
                        <div class="date-picker-container">
                        <telerik:RadDatePicker runat="server" Width="95px" ShowPopupOnFocus="true" ID="rdpStartDate" Skin="Windows7" AutoPostBack="true" OnSelectedDateChanged="rdpDate_OnSelectedDateChanged">
                        </telerik:RadDatePicker> 
                        -
                        <telerik:RadDatePicker runat="server" Width="95px" ShowPopupOnFocus="true" ID="rdpEndDate" Skin="Windows7" AutoPostBack="true" OnSelectedDateChanged="rdpDate_OnSelectedDateChanged">
                        </telerik:RadDatePicker>
                        </div>                        
                    </asp:Panel>
                </td>
            </tr>
        </table>        

        <asp:Panel runat="server" ID="plPeriods2">
            <div class="charts-tabs-container">
                <telerik:RadTabStrip ID="rtsFilters" CssClass="charts" runat="server" SelectedIndex="2" Skin="Windows7" AutoPostBack="true" OnTabClick="rtsFilters_OnTabClick"/>                                    
            </div>
        </asp:Panel>            
        <telerik:RadChart runat="server" ID="rcChart" Width="395px" Skin="Vista" DefaultType="Line" AutoLayout="true">
            <Legend>
                <Appearance Overflow="Row">
                    <Position AlignedPosition="BottomLeft"/>                       
                </Appearance>
            </Legend>
            <Appearance>                
                <FillStyle FillType="Gradient"/>
            </Appearance>
            <PlotArea>                
                <EmptySeriesMessage>
                    <TextBlock Text="Нет данных">
                        <Appearance TextProperties-Color="Black"></Appearance>
                    </TextBlock>
                </EmptySeriesMessage>
            </PlotArea>    
            <ChartTitle>     
                <TextBlock>
                        <Appearance TextProperties-Font="Tahoma, 12px">                       
                        </Appearance>  
                </TextBlock>  
            </ChartTitle>       
        </telerik:RadChart>
    </telerik:RadAjaxPanel>
</div>    