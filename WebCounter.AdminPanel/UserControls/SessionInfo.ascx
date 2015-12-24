<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionInfo.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SessionInfo" %>

<div class="session-info">
    <div class="row">
        <label>Порядковый номер:</label>
        <asp:Literal ID="lrlNumber" runat="server"></asp:Literal>
    </div>
    <div class="row">
        <label>Дата сессии:</label>
        <asp:Literal ID="lrlSessionDate" runat="server"></asp:Literal>
    </div>
    <div class="row">
        <label>IP пользователя:</label>
        <asp:Literal ID="lrlUserIP" runat="server"></asp:Literal>
    </div>
    <div class="row">
        <label>Браузер:</label>
        <asp:Literal ID="lrlBrowser" runat="server"></asp:Literal>
    </div>
    <div class="row">
        <label>Операционная система:</label>
        <asp:Literal ID="lrlOS" runat="server"></asp:Literal>
    </div>
    <div class="row">
        <label>Разрешение экрана:</label>
        <asp:Literal ID="lrlResolution" runat="server"></asp:Literal>
    </div>
    <div class="row">
        <label>Мобильное устройство:</label>
        <asp:Literal ID="lrlMobileDevice" runat="server"></asp:Literal>
    </div>
    <div class="row">
        <label>URL источника:</label>
        <asp:Literal ID="lrlRefferURL" runat="server"></asp:Literal>
    </div>
</div>