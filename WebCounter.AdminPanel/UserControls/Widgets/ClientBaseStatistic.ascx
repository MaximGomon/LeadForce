<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientBaseStatistic.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.ClientBaseStatistic" %>

<ul class="widget-container">
    <li>В базе контактов <%= DataManager.StatisticData.ClientBaseStatisticCountInBase.HtmlValue %></li>
    <li>Из них активных за период <%= DataManager.StatisticData.ClientBaseStatisticActiveCount.HtmlValue %></li>
    <asp:Literal runat="server" ID="lrlMessage" />
</ul>