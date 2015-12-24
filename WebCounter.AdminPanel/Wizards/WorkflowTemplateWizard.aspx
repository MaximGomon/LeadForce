<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkflowTemplateWizard.aspx.cs" Inherits="WebCounter.AdminPanel.Wizards.WorkflowTemplateWizard" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateWizard" Src="~/UserControls/Wizards/WorkflowTemplateWizard/WorkflowTemplateWizard.ascx" %>
<%@ Register TagPrefix="uc" TagName="PopupSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/PopupSiteActionTemplate.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function ShowSiteActionTemplate(id) {
            <%# ucPopupSiteActionTemplate.ClientID %>_ShowSiteActionTemplateRadWindow(id);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server">
        <uc:PopupSiteActionTemplate ID="ucPopupSiteActionTemplate" SiteActionTemplateCategory="Workflow" FromSession="True" ValidationGroup="groupWorkflowTemplate" runat="server" />
    </telerik:RadAjaxPanel>

    <table width="100%">
        <tr>
            <td width="195px" valign="top">&nbsp;</td>
            <td><uc:WorkflowTemplateWizard runat="server" ID="ucWorkflowTemplateWizard" /></td>
        </tr>
    </table>
</asp:Content>