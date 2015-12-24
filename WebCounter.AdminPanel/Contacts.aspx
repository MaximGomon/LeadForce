<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="WebCounter.AdminPanel.Contacts" %>
<%@ Register TagPrefix="uc" TagName="Contacts" Src="~/UserControls/Contact/Contacts.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:Panel runat="server" ID="plPageContainer">    
    <uc:Contacts Section="Monitoring" runat="server" />
    </asp:Panel>
</asp:Content>