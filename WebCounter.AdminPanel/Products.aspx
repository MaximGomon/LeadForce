<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Products.aspx.cs" Inherits="WebCounter.AdminPanel.Products" %>
<%@ Register TagPrefix="uc" TagName="ProductCategories" Src="~/UserControls/ProductCategories.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:Panel runat="server" ID="plPageContainer">  
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsProducts" GridControlID="gridProducts" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersProducts" GridControlID="gridProducts" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Категории товаров">
                    <ContentTemplate>
                        <uc:ProductCategories ID="ucCategoryID" runat="server" SiteID='<%# Eval("SiteID")%>' CategoryID='<%# Eval("CategoryId")%>'/>
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>

    <labitec:Grid ID="gridProducts" TableName="tbl_Product" AccessCheck="true" TagsControlID="tagsProducts" FiltersControlID="filtersProducts" ClassName="WebCounter.AdminPanel.Products" Export="true" runat="server" OnItemDataBound="gridProducts_ItemDataBound">
        <Columns>
            <labitec:GridColumn ID="GridColumn1" DataField="Title" HeaderText="Наименование" runat="server"/>
            <labitec:GridColumn ID="GridColumn2" DataField="SKU" HeaderText="Артикул" runat="server"/>
            <labitec:GridColumn ID="GridColumn3" DataField="ProductCategoryID" HeaderText="Категория" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lProductCategory" runat="server"/>
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn ID="GridColumn4" DataField="tbl_Company.Name" HeaderText="Поставщик" runat="server"/>
            <labitec:GridColumn ID="GridColumn5" DataField="ProductStatusID" HeaderText="Статус" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lProductStatus" runat="server"/>
                </ItemTemplate>
            </labitec:GridColumn>
        </Columns>
        <Joins>
            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_Company" JoinTableKey="ID" TableKey="SupplierID" runat="server" />
        </Joins>
    </labitec:Grid>
    </asp:Panel>
</asp:Content>