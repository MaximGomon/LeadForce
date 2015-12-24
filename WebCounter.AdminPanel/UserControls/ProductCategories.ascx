<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCategories.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ProductCategories" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadMenu ID="radCategories" CssClass="category-widget" runat="server"  ClickToOpen="true" ExpandAnimation-Type="None" CollapseAnimation-Type="None" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false" EnableAjaxSkinRendering="false" />

<telerik:RadTreeView runat="server" ID="radProductCategoryTreeView" OnNodeClick="radProductCategoryTreeView_Click" Height="140" Skin="Windows7" />
