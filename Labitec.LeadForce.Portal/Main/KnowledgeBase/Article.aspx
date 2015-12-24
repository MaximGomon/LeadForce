<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.KnowledgeBase.Article" %>
<%@ Register TagPrefix="uc" TagName="PublicationCategory" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationComment" Src="~/Shared/UserControls/PublicationComment.ascx" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/Shared/UserControls/NotificationMessage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="portal-two-column">
        <div class="left-column">
            <uc:PublicationCategory runat="server" ID="ucPublicationCategory" />
        </div>
        <div class="right-column">
            <div class="b-block">
                <h4 class="top-radius">База знаний</h4>
                <div class="block-content bottom-radius">
                    <asp:Panel runat="server" ID="plArticle" CssClass="article">
                        <h2><asp:Literal runat="server" ID="lrlTitle" /></h2>                        
                        <p><i><asp:Literal runat="server" ID="lrlNoun" /></i></p>
                        <br/>
                        <asp:Panel runat="server" ID="plImage" CssClass="image" Visible="false">
                            <telerik:RadBinaryImage runat="server" ID="rbiImage" AutoAdjustImageControlSize="false" />
                        </asp:Panel>
                        <p><asp:Literal runat="server" ID="lrlText" /></p>
                        <uc:PublicationComment runat="server" ID="ucPublicationComment" />
                    </asp:Panel>
                    <uc:NotificationMessage runat="server" ID="ucNotificationMessage" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
