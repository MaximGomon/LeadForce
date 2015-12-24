<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewSalesInvoiceForPayment.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.NewSalesInvoiceForPayment" %>

<ul class="widget-container">
    <li>Счетов к оплате <%= DataManager.StatisticData.NewSalesInvoiceForPaymentCount.HtmlValue %></li>
    <li>Сумма к оплате <%= DataManager.StatisticData.NewSalesInvoiceForPaymentAmount.HtmlValue %></li>
    <li>Выставлено за период <%= DataManager.StatisticData.NewSalesInvoiceForPaymentExposedCount.HtmlValue %></li>
    <li>Выставлено за период, рублей <%= DataManager.StatisticData.NewSalesInvoiceForPaymentExposedAmount.HtmlValue %></li>
    <li>Оплачено за период <%= DataManager.StatisticData.NewSalesInvoiceForPaymentPayedCount.HtmlValue %></li>
    <li>Оплачено за период, рублей <%= DataManager.StatisticData.NewSalesInvoiceForPaymentPayedAmount.HtmlValue %></li>
</ul>