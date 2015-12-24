<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WebCounter.AdminPanel.Users" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsUsers" GridControlID="gridUsers" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersUsers" GridControlID="gridUsers" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>

    <labitec:Grid ID="gridUsers" TableName="tbl_User" OnItemDataBound="gridUsers_OnItemDataBound" ClassName="WebCounter.AdminPanel.Users" TagsControlID="tagsUsers" FiltersControlID="filtersUsers" Export="true" runat="server">
        <Columns>
            <labitec:GridColumn DataField="Login" HeaderText="Логин" runat="server" />
            <labitec:GridColumn DataField="tbl_Sites.Name" HeaderText="Сайт" runat="server" />
            <labitec:GridColumn DataField="tbl_AccessProfile.Title" HeaderText="Профиль" runat="server"/>
            <labitec:GridColumn DataField="AccessLevelID" HeaderText="Уровень доступа" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="litAccessLevel" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="IsActive" HeaderText="Активен" DataType="Boolean" runat="server"/>
        </Columns>
        <Joins>
            <labitec:GridJoin JoinTableName="tbl_AccessProfile" JoinTableKey="ID" TableKey="AccessProfileID" runat="server" />
            <labitec:GridJoin JoinTableName="tbl_Sites" JoinTableKey="ID" TableKey="SiteID" runat="server" />
        </Joins>
    </labitec:Grid>
</asp:Content>