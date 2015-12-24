<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RepeatSalesPotentialClient.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.RepeatSalesPotentialClient" %>

<ul class="widget-container">
    <li>Потенциальных клиентов <%= DataManager.StatisticData.RepeatSalesPotentialClientCount.HtmlValue %><br/>
        Из них
        <ul>
            <li>Активных за период <%= DataManager.StatisticData.RepeatSalesPotentialClientActiveCount.HtmlValue %></li>
        </ul>
    </li>
</ul>