<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisitorSource.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.VisitorSource" %>

<ul class="widget-container">
    <li><a href='<%= ContactsUrl + "?f=all" %>'>Анонимные посетители за период	<%= DataManager.StatisticData.VisitorSourceNewAnonymousSum.HtmlValue %></a></li>
    <li><a href='<%= ContactsUrl + "?f=new" %>'>Новые анонимные посетители за период&nbsp;<%= DataManager.StatisticData.VisitorSourceNewAnonymousTotal.HtmlValue %></a>
        <ul>
            <asp:Literal runat="server" ID="lrlVisitorSourceNewAnonymousAdvertisingPlatform" />            
            <li><a href='<%= ContactsUrl + "?f=reffer" %>'>Рекомендации <%= DataManager.StatisticData.VisitorSourceNewAnonymousReffer.HtmlValue %></a></li>
            <li><a href='<%= ContactsUrl + "?f=direct" %>'>Прямой заход <%= DataManager.StatisticData.VisitorSourceNewAnonymousDirect.HtmlValue %></a></li>
        </ul>
    </li>
    <li><a href='<%= ContactsUrl + "?f=repeat" %>'>Повторные анонимные посетители <%= DataManager.StatisticData.VisitorSourceRepeatedAnonymous.HtmlValue %></a></li>
</ul>