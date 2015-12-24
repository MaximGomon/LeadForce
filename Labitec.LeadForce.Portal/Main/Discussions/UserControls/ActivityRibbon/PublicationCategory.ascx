<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationCategory.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.PublicationCategory" %>

<div class="block">    
    <h4>Категории</h4>
    <div class="block-content">
        <div class="publication-categories">
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                <telerik:RadTreeView runat="server" ID="rtvPublicationCategory" Skin="Windows7" OnNodeClick="rtvPublicationCategory_OnNodeClick" />
            </telerik:RadAjaxPanel>
        </div>
    </div>
</div>
