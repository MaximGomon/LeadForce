<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteActionTemplates.aspx.cs" Inherits="WebCounter.AdminPanel.SiteActionTemplates" %>
<%@ Register TagPrefix="uc" TagName="SiteActionTemplates" Src="~/UserControls/SiteActionTemplate/SiteActionTemplates.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <uc:SiteActionTemplates runat="server" Section="Evaluation" SectionTitle="Оценка и развитие" />
</asp:Content>