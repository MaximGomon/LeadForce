<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormsWizard.aspx.cs" Inherits="WebCounter.AdminPanel.Wizards.FormsWizard" ValidateRequest="false" %>
<%@ Register TagPrefix="uc" TagName="FormWizard" Src="~/UserControls/Wizards/FormWizard/FormWizard.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr>
            <td width="195px" valign="top">&nbsp;</td>
            <td><uc:FormWizard runat="server" ID="ucFormWizard" /></td>
        </tr>
    </table>
</asp:Content>
