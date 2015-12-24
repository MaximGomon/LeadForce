<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebCounter.AdminPanel.Contact" %>
<%@ Register TagPrefix="uc" TagName="Contact" Src="~/UserControls/Contact/Contact.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%#ResolveUrl("~/Scripts/form.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    function HideTooltip() {
        var tooltip = Telerik.Web.UI.RadToolTip.getCurrent();
        if (tooltip) tooltip.hide();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
        <asp:Panel runat="server" ID="plPageContainer">
<uc:Contact Section="Monitoring" runat="server" />
</asp:Panel>
</asp:Content>