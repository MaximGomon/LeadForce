<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteEventTemplates.aspx.cs" Inherits="WebCounter.AdminPanel.SiteEventTemplates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <labitec:Grid ID="gridSiteEventTemplates" TableName="tbl_SiteEventTemplates" AccessCheck="true" ClassName="WebCounter.AdminPanel.SiteEventTemplates" runat="server">
        <Columns>
            <labitec:GridColumn DataField="Title" HeaderText="Название события" runat="server"/>
        </Columns>
    </labitec:Grid>
</asp:Content>