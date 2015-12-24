<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactCategory.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.ContactCategory" %>

<ul class="widget-container">
    <li class='<%= Category == "known" ? "selected" : string.Empty %>'><a href='<%= Url + "?c=known" %>'>Известные контакты <span><b>(<%= DataManager.StatisticData.ContactCategoryKnownCount.DbValue.ToString("F0")%>)</b></span></a></li>
    <li class='<%= Category == "active" ? "selected" : string.Empty %>'><a href='<%= Url + "?c=active" %>'>Активные контакты (<asp:Literal runat="server" ID="lrlActiveCount" />)</a></li>
    <li class='<%= Category == "anonymous" ? "selected" : string.Empty %>'><a href='<%= Url + "?c=anonymous" %>'>Анонимные посетители <span><b>(<%= DataManager.StatisticData.ContactCategoryAnonymousCount.DbValue.ToString("F0")%>)</b></span></a></li>
    <li class='<%= Category == "all" ? "selected" : string.Empty %>'><a href='<%= Url + "?c=all" %>'>Все контакты <span><b>(<%= DataManager.StatisticData.ContactCategoryTotalCount.DbValue.ToString("F0")%>)</b></span></a></li>
    <li class='<%= Category == "deleted" ? "selected" : string.Empty %>'><a href='<%= Url + "?c=deleted" %>'>Удаленные контакты <span><b>(<%= DataManager.StatisticData.ContactCategoryDeletedCount.DbValue.ToString("F0")%>)</b></span></a></li>
</ul>