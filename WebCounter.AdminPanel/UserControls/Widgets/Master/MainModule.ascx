<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainModule.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.Master.MainModule" %>

<div class="main-widgets clearfix">
    <telerik:RadDockLayout runat="server" ID="radDockLayout" OnLoadDockLayout="radDockLayout_OnLoadDockLayout">
        <telerik:RadDockZone runat="server" ID="radDockZone1" FitDocks="true" Orientation="Horizontal" BorderWidth="0" Skin="Windows7" Width="290px" MinHeight="10px">    
        </telerik:RadDockZone>
        <telerik:RadDockZone runat="server" ID="radDockZone2" FitDocks="true" Orientation="Horizontal" BorderWidth="0" Skin="Windows7" Width="290px" MinHeight="10px">    
        </telerik:RadDockZone>
        <telerik:RadDockZone runat="server" ID="radDockZone3" FitDocks="true" Orientation="Horizontal" BorderWidth="0" Skin="Windows7" Width="290px" MinHeight="10px">    
        </telerik:RadDockZone>
        <telerik:RadDockZone runat="server" ID="radDockZone4" FitDocks="true" Orientation="Horizontal" BorderWidth="0" Skin="Windows7" Width="290px" MinHeight="10px">    
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</div>