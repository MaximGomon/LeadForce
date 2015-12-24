<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Workflow.aspx.cs" Inherits="WebCounter.AdminPanel.Workflow" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar2" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsWorkflow" ObjectTypeName="tbl_Workflow" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <div>
	                <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		                <Tabs>
			                <telerik:RadTab Text="Основная информация" />
                            <telerik:RadTab Text="Параметры процесса" />
			                <telerik:RadTab Text="Элементы процесса" />
		                </Tabs>
	                </telerik:RadTabStrip>
        
                    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <div class="row">
                                <label>Название:</label>
                                <asp:Literal ID="litName" runat="server" />
                            </div>
                            <div class="row">
                                <label>Автор:</label>
                                <asp:Literal ID="litAuthor" runat="server" />
                            </div>
                            <div class="row">
                                <label>Дата запуска:</label>
                                <asp:Literal ID="litStartDate" runat="server" />
                            </div>
                            <div class="row">
                                <label>Дата завершения:</label>
                                <asp:Literal ID="litEndDate" runat="server" />
                            </div>
                            <div class="row">
                                <label>Статус:</label>
                                <asp:Literal ID="litStatus" runat="server" />
                            </div>
                        </telerik:RadPageView>
            
                        <telerik:RadPageView ID="RadPageView3" runat="server">
                            <labitec:Grid ID="gridWorkflowParameter" TableName="tbl_WorkflowParameter" OnItemDataBound="gridWorkflowParameter_OnItemDataBound" ClassName="WebCounter.AdminPanel.WorkflowParameters" Export="true" runat="server">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn6" DataField="tbl_WorkflowTemplateParameter.Name" HeaderText="Параметр" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="litName" Text="Контакт" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn9" DataField="Value" HeaderText="Значение" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="litValue" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                </Columns>
                                <Joins>
                                    <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_WorkflowTemplateParameter" JoinTableKey="ID" TableKey="WorkflowTemplateParameterID" runat="server" />
                                </Joins>
                            </labitec:Grid>
                        </telerik:RadPageView>
            
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <labitec:Grid ID="gridWorkflowElements" TableName="tbl_WorkflowElement" Fields="tbl_WorkflowTemplateElement.ElementType" OnItemDataBound="gridWorkflowElements_OnItemDataBound" ClassName="WebCounter.AdminPanel.WorkflowElements" Export="true" runat="server">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn1" DataField="tbl_WorkflowTemplateElement.Name" HeaderText="Название" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn2" DataField="StartDate" DataType="DateTime" HeaderText="Дата запуска" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn4" DataField="ControlDate" DataType="DateTime" HeaderText="Дата контроля" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn3" DataField="EndDate" DataType="DateTime" HeaderText="Дата завершения" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn7" DataField="Result" HeaderText="Результат" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="litResult" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn5" DataField="Status" HeaderText="Статус" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="litStatus" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                </Columns>
                                <Joins>
                                    <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_WorkflowTemplateElement" JoinTableKey="ID" TableKey="WorkflowTemplateElementID" runat="server" />
                                </Joins>
                            </labitec:Grid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
	                <br/>
	                <div class="buttons">
		                <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	                </div>
                </div>
            </td>
        </tr>
    </table>                
</asp:Content>