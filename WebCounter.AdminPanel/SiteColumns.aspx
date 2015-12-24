<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteColumns.aspx.cs" Inherits="WebCounter.AdminPanel.SiteColumns" %>
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
<table class="smb-files">
        <tr>
            <td width="195px" valign="top" ID="leftColumn" runat="server">
                <div class="aside" ID="asideDiv" runat="server">    
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td valign="top" width="100%">
                <labitec:Search ID="searchSiteColumns" GridControlID="gridSiteColumns" OnDemand="True" SearchBy="Name,Code" runat="server" />                
                <div class="add-block-buttons">
                    <telerik:RadButton ID="rbAddSiteColumn" Text="Добавить" OnClientClicking="OnClientClicking" Skin="Windows7" runat="server"/>                    
                </div>
                <div class="clear"></div>    
                <div class="smb-file-grid">
                    <labitec:Grid ID="gridSiteColumns" ShowHeader="false" Toolbar="false" Fields="tbl_ColumnCategories.Title,tbl_ColumnTypes.Title" TableName="tbl_SiteColumns" ClassName="WebCounter.AdminPanel.SiteColumns" SearchControlID="searchSiteColumns" runat="server" OnItemDataBound="gridSiteColumns_OnItemDataBound">
                        <Columns>
                            <labitec:GridColumn ID="GridColumn1" DataField="Name" HeaderText="Название" runat="server">
                                <ItemTemplate>
                                    <asp:HyperLink id="hlTitle" class="span-name" runat="server" />
                                    <div class="span-url">Тип: <asp:Literal ID="lType" runat="server" /></div> 
                                    <div class="span-url">Категория: <asp:Literal ID="lCategory" runat="server" /></div> 
                                </ItemTemplate>
                            </labitec:GridColumn>
                            <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="130px" Height="65px" HorizontalAlign="Left" VerticalAlign="Middle" runat="server">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEdit" Text="Изменить" CssClass="action-edit" runat="server"/>
                                    <asp:LinkButton ID="lbDelete"  Text="Удалить" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" runat="server" CssClass="action-delete"/>
                                </ItemTemplate>
                            </labitec:GridColumn>
                        </Columns>
                        <Joins>
                            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ColumnCategories" JoinTableKey="ID" TableKey="CategoryID" runat="server" />
                            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_ColumnTypes" JoinTableKey="ID" TableKey="TypeID" runat="server" />
                        </Joins>
                    </labitec:Grid>
                </div>
            </td>
        </tr>
    </table>    
</asp:Content>
