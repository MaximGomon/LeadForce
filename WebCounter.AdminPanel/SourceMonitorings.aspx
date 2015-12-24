<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SourceMonitorings.aspx.cs" Inherits="WebCounter.AdminPanel.SourceMonitorings" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

<div class="aside">
    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
        <Items>
            <telerik:RadPanelItem Expanded="true" Text="Теги">
                <ContentTemplate>
                    <labitec:Tags ID="tagsSourceMonitorings" GridControlID="gridSourceMonitorings" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                <ContentTemplate>
                    <labitec:Filters ID="filtersSourceMonitorings" GridControlID="gridSourceMonitorings" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
        </Items>
    </telerik:RadPanelBar>
    <uc:LeftColumn runat="server" />
</div>

<labitec:Grid ID="gridSourceMonitorings" TableName="tbl_SourceMonitoring" ClassName="WebCounter.AdminPanel.SourceMonitoring" TagsControlID="tagsSourceMonitorings" FiltersControlID="filtersSourceMonitorings" Export="true" runat="server" OnItemDataBound="gridSourceMonitorings_ItemDataBound">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="Name" HeaderText="Название" runat="server"/>
        <labitec:GridColumn ID="GridColumn2" DataField="SourceTypeID" HeaderText="Тип источника" runat="server">
            <ItemTemplate>
                <asp:Literal ID="lrlSourceType" runat="server"/>
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn3" DataField="StatusID" HeaderText="Статус" runat="server">
            <ItemTemplate>
                <asp:Literal ID="lrlStatus" runat="server"/>
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn4" DataField="Comment" HeaderText="Комментарий" runat="server"/>
    </Columns>
</labitec:Grid>


</asp:Content>
