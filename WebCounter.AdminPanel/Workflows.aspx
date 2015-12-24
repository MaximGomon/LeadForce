<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Workflows.aspx.cs" Inherits="WebCounter.AdminPanel.Workflows" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsWorkflows" GridControlID="gridWorkflows" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersWorkflows" GridControlID="gridWorkflows" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>    
    <labitec:Grid ID="gridWorkflows" TableName="tbl_Workflow" OnItemDataBound="gridWorkflows_OnItemDataBound" ClassName="WebCounter.AdminPanel.Workflows" TagsControlID="tagsWorkflows" FiltersControlID="filtersWorkflows" Export="true" runat="server">
        <Columns>
            <labitec:GridColumn ID="GridColumn1" DataField="tbl_WorkflowTemplate.Name" HeaderText="Название" runat="server"/>
            <labitec:GridColumn ID="GridColumn5" DataField="WorkflowTemplateID" HeaderText="Параметр" runat="server">
                    <ItemTemplate>
                        <asp:Literal ID="litParameter" runat="server" />
                    </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn ID="GridColumn2" DataField="StartDate" DataType="DateTime" HeaderText="Дата запуска" runat="server"/>
            <labitec:GridColumn ID="GridColumn3" DataField="EndDate" DataType="DateTime" HeaderText="Дата завершения" runat="server"/>
            <labitec:GridColumn ID="GridColumn4" DataField="Status" HeaderText="Статус" runat="server">
                    <ItemTemplate>
                        <asp:Literal ID="litStatus" runat="server" />
                    </ItemTemplate>
            </labitec:GridColumn>
        </Columns>
        <Joins>
            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_WorkflowTemplate" JoinTableKey="ID" TableKey="WorkflowTemplateID" runat="server" />
        </Joins>
    </labitec:Grid>
</asp:Content>