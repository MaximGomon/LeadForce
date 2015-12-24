<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComingEvents.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ComingEvents" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>

<div class="block">    
    <h4>Ближайшие мероприятия</h4>
    <div class="block-content">        
        <asp:ListView runat="server" ID="lvComingEvents">
            <LayoutTemplate>
                <ul>
                    <li runat="server" id="itemPlaceholder"></li>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li><%# ((DateTime)Eval("StartDate")).ToString("dd.MM.yyyy") %> - <a href='<%# UrlsData.LFP_TaskEdit((Guid)Eval("ID"), ((LeadForcePortalBasePage) Page).PortalSettingsId) %>'><%# Eval("Title") %></a></li>
            </ItemTemplate>
            <EmptyDataTemplate>
                Ближайшие мероприятия отсутствуют
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
</div>