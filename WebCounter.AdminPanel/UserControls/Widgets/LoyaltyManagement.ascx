<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoyaltyManagement.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.LoyaltyManagement" %>

<ul class="widget-container">
    <li>Форм обратной связи <b><asp:Literal runat="server" ID="lrlTotalForms" /></b> на <b><asp:Literal runat="server" ID="lrlTotalPages" /></b> страниц<br/>
    <asp:Literal runat="server" ID="lrlMessage1" />
    <li>Форма "Пригласи друга": подключено <b><%= DataManager.StatisticData.LoyaltyManagementInviteFriendFormCount.DbValue.ToString("F0") %></b> форм</li>
    <asp:Literal runat="server" ID="lrlMessage2" />
    <asp:Literal runat="server" ID="lrlPortal" />
    <li>Заявок без ответа <b><%= DataManager.StatisticData.LoyaltyManagementUnansweredRequest.DbValue.ToString("F0") %></b></li>
</ul>