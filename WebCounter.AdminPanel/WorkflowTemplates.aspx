<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkflowTemplates.aspx.cs" Inherits="WebCounter.AdminPanel.WorkflowTemplates" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                $('#asideFilters input').first().parent().find('label').css('font-weight', 'bold');
            });
            function Checked(element) {
                $('#asideFilters input:radio').parent().find('label').css('font-weight', 'normal');
                $(element).parent().find('label').css('font-weight', 'bold');
            }
            function OnClientClicking(button, args) {
                window.location = button.get_navigateUrl();
                args.set_cancel(true);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsWorkflowTemplates" GridControlID="gridWorkflowTemplates" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                                <ContentTemplate>
                                    <labitec:Filters ID="filtersWorkflowTemplates" GridControlID="gridWorkflowTemplates" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Expanded="true" Text="Статус процесса">
                                <ContentTemplate>
                                    <ul id="asideFilters">
                                        <li><asp:RadioButton ID="InPlansAndActive" runat="server" GroupName="filters" Text="В планах + действующие" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                                        <li><asp:RadioButton ID="InPlans" runat="server" GroupName="filters" Text="В планах" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                                        <li><asp:RadioButton ID="Active" runat="server" GroupName="filters" Text="Действующие" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                                        <li><asp:RadioButton ID="Archive" runat="server" GroupName="filters" Text="Архив" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                                        <li><asp:RadioButton ID="All" runat="server" GroupName="filters" Text="Все" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                                    </ul>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <labitec:Search ID="searchWorkflowTemplates" GridControlID="gridWorkflowTemplates" OnDemand="True" SearchBy="tbl_WorkflowTemplate.Name" runat="server" />
                <div class="add-block-buttons">
                    <telerik:RadButton ID="rbAddWorkflowTemplate" Text="Новый процесс" OnClientClicking="OnClientClicking" Skin="Windows7" runat="server"/>                     
                    <telerik:RadButton ID="rbAddWithMaster" Text="Мастер процесса" OnClientClicking="OnClientClicking" Skin="Windows7" runat="server"/>                     
                </div>
                <div class="clear"></div>    
                <div class="smb-file-grid">
                    <labitec:Grid ID="gridWorkflowTemplates" ShowHeader="false" Toolbar="false" Fields="Status" TableName="tbl_WorkflowTemplate" OnItemDataBound="gridWorkflowTemplates_OnItemDataBound" ClassName="WebCounter.AdminPanel.WorkflowTemplates" TagsControlID="tagsWorkflowTemplates" FiltersControlID="filtersWorkflowTemplates" SearchControlID="searchWorkflowTemplates" runat="server">
                        <Columns>
                            <labitec:GridColumn ID="GridColumn1" DataField="Name" HeaderText="Название" runat="server">
                                <ItemTemplate>
                                    <asp:HyperLink id="hlName" class="span-form-name" runat="server" />
                                    <div class="span-url">Статус: <asp:Literal ID="lrlWorkflowTemplateStatus" runat="server"/></div>                     
                                </ItemTemplate>
                            </labitec:GridColumn>            
                            <labitec:GridColumn ID="GridColumn2" DataField="ID" HeaderText="Операции" Width="130px" HorizontalAlign="Left" Sortable="false" Reorderable="false" Groupable="false" Resizable="false" runat="server">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEdit" Text="Изменить" CssClass="action-edit" runat="server"/>
                                    <asp:LinkButton ID="lbCopy" OnCommand="lbCopy_OnCommand"  CssClass="smb-action" runat="server"><asp:Image ID="Image3" ImageUrl="~/App_Themes/Default/images/icoCopy.png" AlternateText="Копировать" ToolTip="Копировать" runat="server"/><span style="padding-left: 3px">Копировать</span></asp:LinkButton>
                                    <asp:LinkButton ID="lbDelete" Text="Удалить" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="action-delete"/>
                                </ItemTemplate>
                            </labitec:GridColumn>
                        </Columns>
                    </labitec:Grid>
                </div>
            </td>
        </tr>
    </table>    
</asp:Content>