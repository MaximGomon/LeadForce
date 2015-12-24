<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TableReport.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Analytics.TableReport" %>

<div class="table-report">
    <telerik:RadGrid runat="server" ID="rgData" 
    AutoGenerateColumns="true" 
    Skin="Windows7" 
    PageSize="20" 
    AllowPaging="true"    
    ShowStatusBar="true"    
    OnItemDataBound="rgData_OnItemDataBound"
    OnPageIndexChanged="rgData_OnPageIndexChanged"   
    OnPageSizeChanged="rgData_OnPageSizeChanged"    
    >        
        <ExportSettings ExportOnlyData="true" IgnorePaging="true" FileName="Report">
        </ExportSettings>
        <MasterTableView Width="100%" CommandItemDisplay="Top">                        
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"/>
        </MasterTableView>        
    </telerik:RadGrid>
</div>