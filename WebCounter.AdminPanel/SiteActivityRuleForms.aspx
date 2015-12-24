<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="SiteActivityRuleForms.aspx.cs" Inherits="WebCounter.AdminPanel.SiteActivityRuleForms" %>
<%@ Register TagPrefix="uc" TagName="SiteActivityRuleForms" Src="~/UserControls/SiteActivityRuleForms.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:Panel runat="server" ID="plPageContainer">    
    <uc:SiteActivityRuleForms runat="server" Section="Evaluation" SectionTitle="Оценка и развитие" />
    </asp:Panel>
</asp:Content>