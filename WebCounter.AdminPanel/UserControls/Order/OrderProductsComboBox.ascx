<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderProductsComboBox.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Order.OrderProductsComboBox" %>

<telerik:RadComboBox 
    runat="server" 
    ID="rcbOrderProducts" 
    CssClass="orderproducts-combobox"
    AllowCustomText="false" 
    Filter="Contains" 
    EnableEmbeddedSkins="false" 
    skin="Labitec"
    Width="234px" 
    ZIndex="50001"
    EnableLoadOnDemand="True" 
    ShowMoreResultsBox="true"
    EnableVirtualScrolling="true"                     
    OnItemsRequested="rcbOrderProducts_ItemsRequested"
    MaxHeight="150px"
/>