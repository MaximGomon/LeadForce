<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="SiteActivityRules.aspx.cs" Inherits="WebCounter.AdminPanel.SiteActivityRules" %>
<%@ Register TagPrefix="uc" TagName="SiteActivityRules" Src="~/UserControls/SiteActivityRules.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:Panel runat="server" ID="plPageContainer">    
    <uc:SiteActivityRules runat="server" Section="Evaluation" SectionTitle="Оценка и развитие" />
    </asp:Panel>
</asp:Content>