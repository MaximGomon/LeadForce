<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Discussions.aspx.cs" Inherits="WebCounter.AdminPanel.Discussions" %>
<%@ Register TagPrefix="uc" TagName="PublicationTypes" Src="~/UserControls/ActivityRibbon/PublicationTypes.ascx" %>
<%@ Register TagPrefix="uc" TagName="CreatePublication" Src="~/UserControls/ActivityRibbon/CreatePublication.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationCategory" Src="~/UserControls/ActivityRibbon/PublicationCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationsRibbon" Src="~/UserControls/ActivityRibbon/PublicationsRibbon.ascx" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />    
<asp:Panel runat="server" ID="plActivityRibbon">
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Лента активности">
                    <ContentTemplate>
                        <uc:PublicationTypes runat="server" ID="ucPublicationTypes" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Категории">
                    <ContentTemplate>
                        <uc:PublicationCategory runat="server" ID="ucPublicationCategory" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>
    <div class="activity-ribbon-container">        
        <uc:CreatePublication runat="server" ID="ucCreatePublication" />
        <br/>
        <uc:PublicationsRibbon runat="server" ID="ucPublicationsRibbon" />
    </div>
</asp:Panel>
</asp:Content>

