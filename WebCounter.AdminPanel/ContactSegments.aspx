<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactSegments.aspx.cs" Inherits="WebCounter.AdminPanel.ContactSegments" %>
<%@ Register TagPrefix="uc" TagName="Segments" Src="~/UserControls/Segment/Segments.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:Panel runat="server" ID="plPageContainer">    
    <uc:Segments ObjectId="1" runat="server" />
    </asp:Panel>
</asp:Content>