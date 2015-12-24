<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sites.aspx.cs" Inherits="WebCounter.AdminPanel.Sites" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

<div class="aside">
    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
        <Items>
            <telerik:RadPanelItem Expanded="true" Text="Теги">
                <ContentTemplate>
                    <labitec:Tags ID="tagsSites" GridControlID="gridSites" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                <ContentTemplate>
                    <labitec:Filters ID="filtersSites" GridControlID="gridSites" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
        </Items>
    </telerik:RadPanelBar>
</div>

<labitec:Grid ID="gridSites" TableName="tbl_Sites" ClassName="WebCounter.AdminPanel.Sites" TagsControlID="tagsSites" FiltersControlID="filtersSites" Export="true" runat="server">
    <Columns>
        <labitec:GridColumn DataField="Name" HeaderText="Название" runat="server"/>
        <labitec:GridColumn DataField="tbl_AccessProfile.Title" HeaderText="Профиль" runat="server"/>
        <labitec:GridColumn DataField="IsActive" HeaderText="Активен" DataType="Boolean" runat="server"/>
    </Columns>
    <Joins>
        <labitec:GridJoin JoinTableName="tbl_AccessProfile" JoinTableKey="ID" TableKey="AccessProfileID" runat="server" />
    </Joins>
</labitec:Grid>

</asp:Content>
