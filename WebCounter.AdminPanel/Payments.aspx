<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="WebCounter.AdminPanel.Payments" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filterPayments" GridControlID="gridPayments" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>

    <labitec:Grid ID="gridPayments" TableName="tbl_Payment" AccessCheck="true"  FiltersControlID="filterPayments" ClassName="WebCounter.AdminPanel.Payments" Export="true" runat="server">
        <Columns>
            <labitec:GridColumn ID="GridColumn1" DataField="Assignment" HeaderText="Назначение" runat="server"/>
            <labitec:GridColumn ID="GridColumn2" DataField="DatePlan" HeaderText="Дата план" runat="server"/>
            <labitec:GridColumn ID="GridColumn3" DataField="DateFact" HeaderText="Дата факт" runat="server"/>
            <labitec:GridColumn ID="GridColumn4" DataField="tbl_PaymentType.Title" HeaderText="Тип" runat="server"/>
            <labitec:GridColumn ID="GridColumn5" DataField="tbl_PaymentStatus.Title" HeaderText="Состояние" runat="server"/>
        </Columns>
        <Joins>
            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_PaymentStatus" JoinTableKey="ID" TableKey="StatusID" runat="server" />
            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_PaymentType" JoinTableKey="ID" TableKey="PaymentTypeID" runat="server" />
        </Joins>
    </labitec:Grid>
</asp:Content>