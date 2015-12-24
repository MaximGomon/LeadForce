<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="WebCounter.AdminPanel.Company" %>
<%@ Register TagPrefix="uc" TagName="Company" Src="~/UserControls/Company/Company.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <uc:Company ID="Company1" runat="server" />
</asp:Content>
