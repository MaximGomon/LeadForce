<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RepeatSalesInvoiceForPayment.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.RepeatSalesInvoiceForPayment" %>

<ul class="widget-container">
    <li>Счетов к оплате <%= DataManager.StatisticData.RepeatSalesInvoiceForPaymentCount.HtmlValue %></li>
    <li>Сумма к оплате <%= DataManager.StatisticData.RepeatSalesInvoiceForPaymentAmount.HtmlValue%></li>
    <li>Выставлено за период <%= DataManager.StatisticData.RepeatSalesInvoiceForPaymentExposedCount.HtmlValue%></li>
    <li>Выставлено за период, рублей <%= DataManager.StatisticData.RepeatSalesInvoiceForPaymentExposedAmount.HtmlValue%></li>
    <li>Оплачено за период <%= DataManager.StatisticData.RepeatSalesInvoiceForPaymentPayedCount.HtmlValue%></li>
    <li>Оплачено за период, рублей <%= DataManager.StatisticData.RepeatSalesInvoiceForPaymentPayedAmount.HtmlValue%></li>
</ul>