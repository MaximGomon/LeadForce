<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Publications.aspx.cs" Inherits="WebCounter.AdminPanel.Publications" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsPublications" GridControlID="gridPublications" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersPublications" GridControlID="gridPublications" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>
    <labitec:Grid ID="gridPublications" TableName="tbl_Publication" AccessCheck="true" TagsControlID="tagsPublications" FiltersControlID="filtersPublications" ClassName="WebCounter.AdminPanel.Publications" Export="true" runat="server">
        <Columns>
            <labitec:GridColumn ID="GridColumn1" DataField="Title" HeaderText="Заголовок" runat="server"/>
            <labitec:GridColumn ID="GridColumn2" DataField="tbl_PublicationType.Title" HeaderText="Тип" runat="server"/>
            <labitec:GridColumn ID="GridColumn3" DataField="tbl_PublicationCategory.Title" HeaderText="Категория" runat="server"/>
            <labitec:GridColumn ID="GridColumn4" DataField="tbl_PublicationStatus.Title" HeaderText="Статус" runat="server"/>
        </Columns>
        <Joins>
            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_PublicationType" JoinTableKey="ID" TableKey="PublicationTypeID" runat="server" />
            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_PublicationCategory" JoinTableKey="ID" TableKey="PublicationCategoryID" runat="server" />
            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_PublicationStatus" JoinTableKey="ID" TableKey="PublicationStatusID" runat="server" />
        </Joins>
    </labitec:Grid>
</asp:Content>