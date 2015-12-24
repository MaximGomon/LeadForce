<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyComboBox.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.CompanyComboBox" %>

<telerik:RadComboBox 
    runat="server" 
    ID="rcbCompany" 
    CssClass="company-combobox"
    AllowCustomText="false" 
    Filter="Contains" 
    EnableEmbeddedSkins="false" 
    skin="Labitec"
    Width="234px" 
    ZIndex="50001"
    EnableLoadOnDemand="True" 
    ShowMoreResultsBox="true"
    EnableVirtualScrolling="true"                     
    OnItemsRequested="rcbCompany_ItemsRequested"
    MaxHeight="150px"
/>