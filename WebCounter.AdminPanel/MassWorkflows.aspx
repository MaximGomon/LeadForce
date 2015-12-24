<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MassWorkflows.aspx.cs" Inherits="WebCounter.AdminPanel.MassWorkflows" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <script type="text/javascript">
        function OnClientClicking(button, args) {
            window.location = button.get_navigateUrl();
            args.set_cancel(true);
        }    
    </script>
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsMassWorkflows" GridControlID="gridMassWorkflows" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                                <ContentTemplate>
                                    <labitec:Filters ID="filtersMassWorkflows" GridControlID="gridMassWorkflows" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Expanded="true" Text="Статус мероприятия">
                                <ContentTemplate>
                                    <ul class="widget-container radpanel">
                                        <li class='<%= Filter == "current" ? "selected" : string.Empty %>'><a href='<%= Url + "?f=current" %>'>Текущие</a></li>
                                        <li class='<%= Filter == "done" ? "selected" : string.Empty %>'><a href='<%= Url + "?f=done" %>'>Проведены</a></li>
                                        <li class='<%= Filter == "cancel" ? "selected" : string.Empty %>'><a href='<%= Url + "?f=cancel" %>'>Отменены</a></li>
                                        <li class='<%= Filter == "all" ? "selected" : string.Empty %>'><a href='<%= Url + "?f=all" %>'>Все мероприятия</a></li>
                                    </ul>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <labitec:Search ID="searchMassWorkflows" GridControlID="gridMassWorkflows" OnDemand="True" SearchBy="tbl_MassWorkflow.Name" runat="server" />
                <div class="add-block-buttons">
                    <telerik:RadButton ID="rbAddMassWorkflow" Text="Добавить мероприятие" OnClientClicking="OnClientClicking" Skin="Windows7" runat="server"/>                            
                </div>
                <div class="clear"></div>    
                
                    <labitec:Grid ID="gridMassWorkflows" ShowPadding="False" ShowHeader="false" Toolbar="false" Fields="tbl_MassWorkflow.Status, tbl_MassWorkflowType.Title" TableName="tbl_MassWorkflow" OnItemDataBound="gridMassWorkflows_OnItemDataBound" ClassName="WebCounter.AdminPanel.MassWorkflows" TagsControlID="tagsMassWorkflows" FiltersControlID="filtersMassWorkflows" SearchControlID="searchMassWorkflows" Export="true" runat="server">
                        <Columns>
                            <labitec:GridColumn ID="GridColumn1" DataField="Name" runat="server">
                                <ItemTemplate>                                    
                                    <asp:HyperLink id="spanName" class="span-form-name" runat="server" />
                                    <div class="span-url" style="float:left">Статус: <asp:Literal ID="lStatus" runat="server" /></div>                                    
                                    <div class="span-url" style="float:right"><asp:Literal ID="lMassWorkflowType" runat="server" /></div>                                     
                                    <div class="clear"></div>
                                </ItemTemplate>
                            </labitec:GridColumn>
                            <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="100px" Height="65px" HorizontalAlign="Left" VerticalAlign="Top" runat="server">
                                <ItemTemplate>                                    
                                    <asp:HyperLink ID="hlEdit" CssClass="smb-action" runat="server"><asp:Image ID="Image2" ImageUrl="~/App_Themes/Default/images/icoView.png" AlternateText="Изменить" ToolTip="Изменить" runat="server"/><span style="padding-left: 3px">Изменить</span></asp:HyperLink><br/>
                                    <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="smb-action"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /><span style="padding-left: 5px">Удалить</span></asp:LinkButton>
                                </ItemTemplate>
                            </labitec:GridColumn>
                        </Columns>
                        <Joins>
                            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_WorkflowTemplate" JoinTableKey="ID" TableKey="WorkflowTemplateID" runat="server"/>
                            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_MassWorkflowType" JoinTableKey="ID" TableKey="MassWorkflowTypeID" runat="server"/>
                        </Joins>
                    </labitec:Grid>

            </td>
        </tr>
    </table>        
</asp:Content>