<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationCategory.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ActivityRibbon.PublicationCategory" %>

<div class="publication-categories">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <telerik:RadTreeView runat="server" ID="rtvPublicationCategory" Skin="Windows7" OnNodeClick="rtvPublicationCategory_OnNodeClick" />
    </telerik:RadAjaxPanel>
</div>