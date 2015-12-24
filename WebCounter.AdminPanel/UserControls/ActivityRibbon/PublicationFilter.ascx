<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationFilter.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ActivityRibbon.PublicationFilter" %>

<div class="publication-filter">
     <telerik:RadToolBar id="rtbPublicationFilter" runat="server" AutoPostBack="true" OnButtonClick="rtbPublicationFilter_OnButtonClick" Skin="Windows7" EnableRoundedCorners="true" EnableShadows="true" Width="100%">
        <Items>            
            <telerik:RadToolBarButton Text="Новые" CheckOnClick="true" Group="Filter" CommandArgument="New" Checked="true" />
            <telerik:RadToolBarButton IsSeparator="true" />
            <telerik:RadToolBarButton Text="Популярные" CheckOnClick="true" Group="Filter" CommandArgument="Top" />
        </Items>
    </telerik:RadToolBar>
</div>