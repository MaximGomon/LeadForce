<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="MassWorkflow.aspx.cs" Inherits="WebCounter.AdminPanel.MassWorkflow" %>
<%@ Register TagPrefix="uc" TagName="SelectWorkflowTemplate" Src="~/UserControls/WorkflowTemplate/SelectWorkflowTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectContacts" Src="~/UserControls/Contact/SelectContacts.ascx" %>
<%@ Register TagPrefix="uc" TagName="Chart" Src="~/UserControls/Analytics/Chart.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#hfWidth').val($(document).width());
        });
        function OnTabSelected(sender, args) {
            if (args.get_tab().get_value() == "Charts") {
                $find('<%= radAjaxManager.ClientID %>').ajaxRequest("BuildCharts");
            }
        }
    </script>

    <asp:HiddenField runat="server" ID="hfWidth" ClientIDMode="Static" />
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar2" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsMassWorkflow" ObjectTypeName="tbl_MassWorkflow" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <div>
	                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                HeaderText="Заполните все поля корректно:"
						                ValidationGroup="groupMassWorkflow" />
                            
                    <div class="row">
                        <label>Название:</label>
                        <asp:TextBox ID="txtName" CssClass="input-text" runat="server" Width="645px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Вы не ввели 'Название'" ValidationGroup="groupMassWorkflow" runat="server">*</asp:RequiredFieldValidator>
                    </div>
        
        
	                <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server" OnClientTabSelected="OnTabSelected">
		                <Tabs>
			                <telerik:RadTab Text="Основная информация" />
                            <telerik:RadTab Text="Участники программы" />
			                <telerik:RadTab Text="Статистика программы" Value="Charts" />              
		                </Tabs>
	                </telerik:RadTabStrip>
        
                    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">--%>
                        <telerik:RadMultiPage ID="RadMultiPage1" ClientIDMode="Static" SelectedIndex="0" CssClass="multiPage" runat="server">
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                                    <div class="row">
                                        <label>Цель мероприятия:</label>
                                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" CssClass="area-text" Width="635px" />
                                    </div>
                    
                                    <div class="row">
                                        <label>Шаблон:</label>
                                        <uc:SelectWorkflowTemplate ID="ucSelectWorkflowTemplate" ValidationGroup="groupMassWorkflow" runat="server" />
                                    </div>
                    
                                    <div class="row">
                                        <label>Статус:</label>
                                        <asp:Literal ID="litStatus" runat="server" />
                                        <telerik:RadButton ID="btnRun" Text="Запустить" OnClick="btnRun_OnClick" Skin="Windows7" runat="server"/> <telerik:RadButton ID="btnCancel" Text="Отменить" OnClick="btnCancel_OnClick" Skin="Windows7" runat="server"/>
                                        <asp:HiddenField ID="hdnStatus" runat="server"/>
                                    </div>
                                    <div class="row">
                                        <label>Тип мероприятия:</label>
                                        <asp:DropDownList runat="server" CssClass="select-text" ID="ddlMassWorkflowType" />
                                    </div>
                        
                                    <asp:Panel ID="pnlStartDate" CssClass="row" Visible="False" runat="server">
                                        <label>Дата запуска:</label>
                                        <asp:TextBox ID="txtStartDate" Enabled="False" ReadOnly="True" Width="130px" CssClass="input-text" style="text-align: center" runat="server" />
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </telerik:RadPageView>
            
                            <telerik:RadPageView ID="RadPageView2" runat="server">
                                <telerik:RadAjaxPanel runat="server">
                                    <uc:SelectContacts ID="ucSelectContacts" OnSelectedChanged="ucSelectContacts_OnSelectedChanged" runat="server" />
                                </telerik:RadAjaxPanel>
                                
                                <br />
                                <labitec:Grid ID="gridContacts" TableName="tbl_Contact" AccessCheck="true" OnItemDataBound="gridContacts_OnItemDataBound" ClassName="WebCounter.AdminPanel.MassWorkflowContacts" Export="true" runat="server">
                                    <Columns>
                                        <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата создания" DataType="DateTime" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn2" DataField="LastActivityAt" HeaderText="Последняя активность" DataType="DateTime" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn3" DataField="UserFullName" HeaderText="Ф.И.О." runat="server"/>
                                        <labitec:GridColumn ID="GridColumn4" DataField="Email" HeaderText="E-mail" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn5" DataField="tbl_ReadyToSell.Title" HeaderText="Готовность к продаже" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn6" DataField="tbl_Priorities.Title" HeaderText="Важность" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn8" DataField="Score" HeaderText="Общий балл" DataType="Int32" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn11" DataField="ID" HeaderText="Действия" Width="100px" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" HorizontalAlign="Center" runat="server">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnDelete" OnCommand="ibtnDelete_OnCommand" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" ToolTip="Удалить" AlternateText="Удалить" runat="server"/>
                                            </ItemTemplate>
                                        </labitec:GridColumn>
                                    </Columns>
                                    <Joins>
                                        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ReadyToSell" JoinTableKey="ID" TableKey="ReadyToSellID" runat="server" />
                                        <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Priorities" JoinTableKey="ID" TableKey="PriorityID" runat="server" />
                                    </Joins>
                                </labitec:Grid>
                            </telerik:RadPageView>
            
                            <telerik:RadPageView ID="RadPageView3" runat="server">
                                <uc:Chart runat="server" ID="ucChart1" ShowDatePeriod="true" Height="400px" Visible="false" Title="Конверсия по элементам процесса" />
                                <uc:Chart runat="server" ID="ucChart2" ShowDatePeriod="true" Height="400px" Visible="false" Title="Конверсия по переходам в процессе" />
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    <%--</telerik:RadAjaxPanel>--%>
        
                    <br/>
	                <div class="buttons">
		                <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupMassWorkflow" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		                <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	                </div>
                </div>
            </td>
        </tr>
    </table>            
</asp:Content>
