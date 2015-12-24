<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactAdd.aspx.cs" Inherits="WebCounter.AdminPanel.ContactAdd" %>
<%@ Register TagPrefix="uc" TagName="ContactAdd" Src="~/UserControls/Contact/ContactAdd.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:Panel runat="server" ID="plPageContainer">
        <uc:ContactAdd Section="Monitoring" runat="server" ID="ucContactAdd"/> 
    </asp:Panel>
</asp:Content>