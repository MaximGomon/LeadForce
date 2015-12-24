<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftColumn.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.Master.LeftColumn" %>

<div class="left-column-widgets">
    <telerik:RadDockLayout runat="server" ID="radDockLayout" OnLoadDockLayout="radDockLayout_OnLoadDockLayout">
        <telerik:RadDockZone runat="server" ID="radDockZone" FitDocks="true" Orientation="Horizontal" BorderWidth="0" Skin="Windows7" Width="189px" MinHeight="10px">    
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</div>