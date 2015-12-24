<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Companies.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Companies" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsCompanies" GridControlID="gridCompanies" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersCompanies" GridControlID="gridCompanies" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>

    <labitec:Grid ID="gridCompanies" TableName="tbl_Company" AccessCheck="true" TagsControlID="tagsCompanies" FiltersControlID="filtersCompanies" ClassName="WebCounter.AdminPanel.Companies" OnItemDataBound="gridCompanies_OnItemDataBound" Export="true" runat="server">
        <Columns>
            <labitec:GridColumn ID="GridColumn1" DataField="Name" HeaderText="Наименование" runat="server"/>
            <%--<labitec:GridColumn ID="GridColumn2" DataField="tbl_Company.Name" HeaderText="Входит в холдинг" runat="server"/>--%>
            <labitec:GridColumn ID="GridColumn3" DataField="tbl_Contact.UserFullName" HeaderText="Ответственный" runat="server"/>
            <labitec:GridColumn ID="GridColumn4" DataField="tbl_CompanyType.Name" HeaderText="Тип компании" runat="server"/>
            <labitec:GridColumn ID="GridColumn5" DataField="tbl_CompanySize.Name" HeaderText="Размер компании" runat="server"/>
            <labitec:GridColumn ID="GridColumn6" DataField="tbl_CompanySector.Name" HeaderText="Отрасль" runat="server"/>
            <labitec:GridColumn ID="GridColumn7" DataField="tbl_ReadyToSell.Title" HeaderText="Готовность к продаже" runat="server"/>
            <labitec:GridColumn ID="GridColumn8" DataField="tbl_Priorities.Title" HeaderText="Важность" runat="server"/>
            <labitec:GridColumn ID="GridColumn9" DataField="tbl_Status.Title" HeaderText="Статус" runat="server"/>
            <labitec:GridColumn ID="GridColumn10" DataField="Score" HeaderText="Общий балл" DataType="Int32" runat="server"/>
            <labitec:GridColumn DataField="ID" HeaderText="Действия" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink ID="hlEdit" ImageUrl="~/App_Themes/Default/images/icoView.png" ToolTip="Карточка компании" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
        </Columns>
        <Joins>
            <%--<labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_Company" JoinTableKey="ID" TableKey="ParentID" runat="server" />--%>
            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Contact" JoinTableKey="ID" TableKey="OwnerID" runat="server" />            
            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_CompanyType" JoinTableKey="ID" TableKey="CompanyTypeID" runat="server" />
            <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_CompanySize" JoinTableKey="ID" TableKey="CompanySizeID" runat="server" />
            <labitec:GridJoin ID="GridJoin5" JoinTableName="tbl_CompanySector" JoinTableKey="ID" TableKey="CompanySectorID" runat="server" />
            <labitec:GridJoin ID="GridJoin6" JoinTableName="tbl_ReadyToSell" JoinTableKey="ID" TableKey="ReadyToSellID" runat="server" />
            <labitec:GridJoin ID="GridJoin7" JoinTableName="tbl_Priorities" JoinTableKey="ID" TableKey="PriorityID" runat="server" />
            <labitec:GridJoin ID="GridJoin8" JoinTableName="tbl_Status" JoinTableKey="ID" TableKey="StatusID" runat="server" />
        </Joins>
    </labitec:Grid>