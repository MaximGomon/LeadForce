<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebSites.aspx.cs" Inherits="WebCounter.AdminPanel.WebSites" %>
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
    <table class="smb-files domain">
        <tr>
            <td width="195px" valign="top" ID="leftColumn" runat="server">
                <div class="aside" ID="asideDiv" runat="server">    
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td valign="top" width="100%">
                <labitec:Search ID="searchWebSites" GridControlID="gridWebSites" OnDemand="True" SearchBy="Title,Description" runat="server" />
                <div class="add-block-buttons">
                    <telerik:RadButton ID="rbAddWebSite" Text="Добавить" OnClientClicking="OnClientClicking" Skin="Windows7" runat="server"/>        
                </div>
                <div class="clear"></div>    
                <div class="smb-file-grid">
                    <labitec:Grid ID="gridWebSites" ShowHeader="false" Toolbar="false" SearchControlID="searchWebSites" Fields="ID, Description, tbl_SiteDomain.Domain" OnItemDataBound="gridWebSites_OnItemDataBound" ClassName="WebCounter.AdminPanel.WebSites" TableName="tbl_WebSite" runat="server">
                        <Columns>
                            <labitec:GridColumn ID="GridColumn1" DataField="Title" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" runat="server" Height="65px">
                            <ItemTemplate>                                
                                <span class="span-name"><asp:Literal runat="server" ID="lrlTitle" /></span>
                                <div class="span-url"><asp:Literal ID="lrlDescription" runat="server" /></div> 
                                <div id="spanUrl" class="span-url" runat="server"><asp:Literal runat="server" ID="lrlUrl" /></div>                                                                      
                                </ItemTemplate>
                            </labitec:GridColumn>
                            <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="100px" Height="65px" HorizontalAlign="Left" VerticalAlign="Middle" runat="server">
                                <ItemTemplate>

                                    <asp:HyperLink ID="hlEdit" Text="Изменить" CssClass="action-edit" runat="server"/>
                                    <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="smb-action"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /><span style="padding-left: 5px">Удалить</span></asp:LinkButton>
                                </ItemTemplate>
                            </labitec:GridColumn>
                        </Columns>
                        <Joins>
                            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_SiteDomain" JoinTableKey="ID" TableKey="SiteDomainID" runat="server" />                                                        
                        </Joins>
                    </labitec:Grid>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
