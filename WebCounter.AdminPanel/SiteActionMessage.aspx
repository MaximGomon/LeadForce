<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteActionMessage.aspx.cs" Inherits="WebCounter.AdminPanel.SiteActionMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="error-message-container">
        <div class="row">
            <label>Тема:</label>
            <span><asp:Literal runat="server" ID="lrlMessageTitle" /></span>
        </div>
        <div class="row">
            <label>Сообщение:</label>
            <div class="text"><asp:Literal runat="server" ID="lrlMessageText" /></div>
        </div>
	    <br/>
	    <div class="buttons">		
		    <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	    </div>
    </div>
</asp:Content>
