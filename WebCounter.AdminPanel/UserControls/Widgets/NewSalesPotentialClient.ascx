<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewSalesPotentialClient.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.NewSalesPotentialClient" %>

<ul class="widget-container">
    <li>Потенциальных клиентов <%= DataManager.StatisticData.NewSalesPotentialClientCount.HtmlValue %><br/>
        Из них
        <ul>
            <li>Активных за период <%= DataManager.StatisticData.NewSalesPotentialClientActiveCount.HtmlValue %></li>
        </ul>
    </li>
</ul>