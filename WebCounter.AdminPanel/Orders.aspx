<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="WebCounter.AdminPanel.Orders" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsOrders" GridControlID="gridOrders" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersOrders" GridControlID="gridOrders" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>
    <labitec:Grid ID="gridOrders" TableName="tbl_Order" AccessCheck="true" ClassName="WebCounter.AdminPanel.Order" Fields="tbl_Contact.ID, tbl_Company.ID" TagsControlID="tagsOrders" FiltersControlID="filtersOrders" Export="true" runat="server" OnItemDataBound="gridOrders_OnItemDataBound">
        <Columns>
            <labitec:GridColumn DataField="Number" HeaderText="Номер" runat="server"/>
            <labitec:GridColumn DataField="CreatedAt" HeaderText="Дата заказа" runat="server"/>
            <labitec:GridColumn DataField="tbl_OrderType.Name" HeaderText="Тип заказа" runat="server"/>
            <labitec:GridColumn DataField="OrderStatusID" HeaderText="Статус" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlOrderStatus" runat="server"/>
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="tbl_Company.Name" HeaderText="Заказчик" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlCompanyName" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="tbl_Contact.UserFullName" HeaderText="Контакт заказчика" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlUserFullName" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="Ordered" HeaderText="Заказано" runat="server"/>
        </Columns>    
        <Joins>        
            <labitec:GridJoin JoinTableName="tbl_OrderType" JoinTableKey="ID" TableName="tbl_Order" TableKey="OrderTypeID" runat="server" />
            <labitec:GridJoin JoinTableName="tbl_Company" JoinTableKey="ID" TableName="tbl_Order" TableKey="BuyerCompanyID" runat="server" />
            <labitec:GridJoin JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Order" TableKey="BuyerContactID" runat="server" />
        </Joins>
    </labitec:Grid>
</asp:Content>
