<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="WebCounter.AdminPanel.Companies" %>
<%@ Register TagPrefix="uc" TagName="Companies" Src="~/UserControls/Company/Companies.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <uc:Companies ID="Companies1" runat="server" />
</asp:Content>
