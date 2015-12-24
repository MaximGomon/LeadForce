<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="WebCounter.AdminPanel.Requests" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsRequests" GridControlID="gridRequests" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersRequests" GridControlID="gridRequests" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>                   
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>
    <labitec:Grid ID="gridRequests" AccessCheck="true" TableName="tbl_Request" ClassName="WebCounter.AdminPanel.Requests" Fields="tbl_Contact.ID, tbl_Company.ID" TagsControlID="tagsRequests" FiltersControlID="filtersRequests" Export="true" runat="server" OnItemDataBound="gridRequests_OnItemDataBound">
        <Columns>
            <labitec:GridColumn ID="GridColumn1" DataField="Number" HeaderText="Номер" runat="server"/>
            <labitec:GridColumn ID="GridColumn8" DataField="ShortDescription" HeaderText="Запрос кратко" runat="server"/>
            <labitec:GridColumn ID="GridColumn2" DataField="CreatedAt" HeaderText="Дата регистрации" runat="server"/>
            <labitec:GridColumn ID="GridColumn7" DataField="ReactionDatePlanned" HeaderText="Дата реакции, план" runat="server"/>            
            <labitec:GridColumn ID="GridColumn3" DataField="tbl_RequestSourceType.Title" HeaderText="Тип источника" runat="server"/>
            <labitec:GridColumn ID="GridColumn4" DataField="tbl_RequestStatus.Title" HeaderText="Состояние запроса" runat="server"/>
            <labitec:GridColumn ID="GridColumn5" DataField="tbl_Company.Name" HeaderText="Компания" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlCompanyName" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn ID="GridColumn6" DataField="tbl_Contact.UserFullName" HeaderText="Контакт" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlUserFullName" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="tbl_ServiceLevel.Title" HeaderText="Уровень обслуживания" runat="server"/>
        </Columns>    
        <Joins>
            <labitec:GridJoin ID="GridJoin5" JoinTableName="tbl_RequestStatus" JoinTableKey="ID" TableName="tbl_Request" TableKey="RequestStatusID" runat="server" />        
            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_RequestSourceType" JoinTableKey="ID" TableName="tbl_Request" TableKey="RequestSourceTypeID" runat="server" />
            <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_ServiceLevel" JoinTableKey="ID" TableName="tbl_Request" TableKey="ServiceLevelID" runat="server" />
            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Company" JoinTableKey="ID" TableName="tbl_Request" TableKey="CompanyID" runat="server" />
            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Request" TableKey="ContactID" runat="server" />
        </Joins>
    </labitec:Grid>
</asp:Content>
