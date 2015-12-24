<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteActivityRule.aspx.cs" Inherits="WebCounter.AdminPanel.SiteActivityRule" %>
<%@ Register TagPrefix="uc" TagName="SiteActivityRule" Src="~/UserControls/SiteActivityRule.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<table class="smb-files" width="100%"><tr>
<td width="195px" valign="top">
<div class="aside">
    <uc:LeftColumn runat="server" />
</div>
</td>
<td valign="top" width="100%">
    <uc:SiteActivityRule runat="server" Section="Evaluation" SectionTitle="Оценка и развитие" />
</td>
</tr></table>

</asp:Content>