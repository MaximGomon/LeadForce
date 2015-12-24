<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="SiteActivityRuleFiles.aspx.cs" Inherits="WebCounter.AdminPanel.SiteActivityRuleFiles" %>
<%@ Register TagPrefix="uc" TagName="SiteActivityRuleFiles" Src="~/UserControls/SiteActivityRuleFiles.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:Panel runat="server" ID="plPageContainer">    
    <uc:SiteActivityRuleFiles runat="server" Section="Evaluation" SectionTitle="Оценка и развитие" />
    </asp:Panel>
</asp:Content>