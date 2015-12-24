<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Links.aspx.cs" Inherits="WebCounter.AdminPanel.Links" %>
<%@ Register TagPrefix="uc" TagName="Links" Src="~/UserControls/Links.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:Panel runat="server" ID="plPageContainer">    
        <uc:Links runat="server" />
    </asp:Panel>
</asp:Content>
