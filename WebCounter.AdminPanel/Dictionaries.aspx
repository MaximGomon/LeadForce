<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dictionaries.aspx.cs" Inherits="WebCounter.AdminPanel.Dictionaries" %>
<%@ Register TagPrefix="uc" TagName="SelectContacts" Src="~/UserControls/Contact/SelectContacts.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">
<telerik:RadAjaxPanel runat="server">
    <uc:SelectContacts ID="ucSelectContacts" HideButton="True" runat="server" />
    <labitec:Dictionaries ID="lbcDictionaries" runat="server" DataAccessAssemblyName="WebCounter.DataAccessLayer" ConnectionStringName="WebCounterEntities" DefaultContainerName="WebCounterEntities"/>
</telerik:RadAjaxPanel>
</asp:Content>