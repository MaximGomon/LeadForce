<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Discussion.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.Discussions.Discussion" %>
<%@ Register TagPrefix="uc" TagName="PublicationCategory" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationComment" Src="~/Shared/UserControls/PublicationComment.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationTypes" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationTypes.ascx" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/Shared/UserControls/NotificationMessage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="portal-two-column">
        <div class="left-column">
            <div class="aside">
                <div class="block">    
                    <h4>Лента активности</h4>
                    <div class="block-content">
                        <uc:PublicationTypes runat="server" ID="ucPublicationTypes" />
                    </div>
                </div>
                <hr/>
                <uc:PublicationCategory runat="server" ID="ucPublicationCategory" />
            </div>
        </div>
        <div class="right-column">
            <div class="b-block">
                <h4 class="top-radius">Обсуждения</h4>
                <div class="block-content bottom-radius">
                    <asp:Panel runat="server" ID="plArticle" CssClass="article">
                        <h2><asp:Literal runat="server" ID="lrlTitle" /></h2>                        
                        <asp:Panel runat="server" ID="plImage" CssClass="image" Visible="false">
                            <telerik:RadBinaryImage runat="server" ID="rbiImage" AutoAdjustImageControlSize="false" />
                        </asp:Panel>
                        <p><asp:Literal runat="server" ID="lrlText" /></p>
                        <asp:Panel runat="server" ID="plOffComment" Visible="false" CssClass="off-comment">
                            <h4>Официальный ответ:</h4>
                            <p><asp:Literal runat="server" ID="lrlOffComment" /></p>
                        </asp:Panel>
                        <uc:PublicationComment runat="server" ID="ucPublicationComment" />                    
                    </asp:Panel>
                    <uc:NotificationMessage runat="server" ID="ucNotificationMessage" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

