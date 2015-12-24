<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeadForceWizard.aspx.cs" Inherits="WebCounter.AdminPanel.Wizards.LeadForceWizard" %>
<%@ Register TagPrefix="uc" TagName="LeadForceWizard" Src="~/UserControls/Wizards/LeadForceWizard/LeadForceWizard.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Мастер настройки LeadForce</title>
    <link href="<%# ResolveUrl("~/App_Themes/Default/AdminPanel.css") %>" rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
    <script src="<%#ResolveUrl("~/Scripts/jquery-1.5.2.min.js")%>" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnablePageMethods="true" />                    
    <telerik:RadAjaxManager runat="server"></telerik:RadAjaxManager>
    <asp:UpdateProgress ID="PageUpdateProgress" runat="server">
        <ProgressTemplate>                
            <div class="ajax-loader"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
        <uc:LeadForceWizard runat="server" ID="ucLeadForceWizard" />
    </form>
</body>
</html>
