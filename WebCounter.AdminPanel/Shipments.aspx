<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shipments.aspx.cs" Inherits="WebCounter.AdminPanel.Shipments" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <script type="text/javascript">
        function OnClientClicking(button, args) {
            window.location = button.get_navigateUrl();
            args.set_cancel(true);
        }         
        $(document).ready(function () {
            $('#radioFilters input').first().parent().find('label').css('font-weight', 'bold');
        });
        function Checked(element) {
            $('#radioFilters input:radio').parent().find('label').css('font-weight', 'normal');
            $(element).parent().find('label').css('font-weight', 'bold');
        }            
    </script>
    <table class="smb-files">
        <tr>
            <td width="195px" valign="top" ID="leftColumn" runat="server">
                <div class="aside">    
                    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsShipments" GridControlID="gridShipments" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                                <ContentTemplate>
                                    <labitec:Filters ID="filtersShipments" GridControlID="gridShipments" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>                            
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td valign="top" width="100%">
                <labitec:Search ID="searchShipments" GridControlID="gridShipments" OnDemand="True" SearchBy="tbl_Company.Name,tbl_CompanyLegalAccount.Title" runat="server" />                
                <div class="add-block-buttons">
                    <telerik:RadButton ID="rbAddShipment" Text="Добавить" OnClientClicking="OnClientClicking" Skin="Windows7" runat="server"/>                    
                </div>
                <div class="clear"></div>    
                <div class="smb-file-grid">
                    <labitec:Grid ID="gridShipments" AccessCheck="true" ShowHeader="false" Toolbar="false" SearchControlID="searchShipments" TableName="tbl_Shipment" ClassName="WebCounter.AdminPanel.Shipments" Fields="tbl_CompanyLegalAccount.Title, ShipmentStatusID, ID, Note,ShipmentAmount, CreatedAt, tbl_Company.ID, tbl_Company.Name" TagsControlID="tagsShipments" FiltersControlID="filtersShipments" Export="true" runat="server" OnItemDataBound="gridShipments_OnItemDataBound">
                        <Columns>
                            <labitec:GridColumn ID="GridColumn1" DataField="Number" HeaderText="Номер" runat="server">
                                <ItemTemplate>
                                    <div class="grid-left-column">
                                        <asp:HyperLink id="hlTitle" class="span-name" runat="server" />
                                        <div class="span-url">Заказчик: <asp:Literal ID="lrlCompany" runat="server" /></div> 
                                        <div class="span-url">Юридическое лицо: <asp:Literal ID="lrlCompanyLegalAccount" runat="server" /></div>
                                        <asp:Literal ID="lrlInvoices" runat="server" />
                                        <div class="span-url"><asp:Literal ID="lrlNote" runat="server" /></div> 
                                    </div>
                                    <div class="grid-right-column">
                                        <div class="span-url">Состояние: <asp:Literal ID="lrlShipmentStatus" runat="server"/></div>              
                                        <div class="span-url">Сумма: <asp:Literal ID="lrlShipmentAmount" runat="server" /></div>                           
                                    </div>
                                    <div class="clear"></div>
                                </ItemTemplate>
                            </labitec:GridColumn>
                            <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="130px" Height="65px" HorizontalAlign="Left" VerticalAlign="Middle" runat="server">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEdit" Text="Изменить" CssClass="action-edit" runat="server"/>
                                    <asp:LinkButton ID="lbDelete" Text="Удалить" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" runat="server" CssClass="action-delete"/>
                                </ItemTemplate>
                            </labitec:GridColumn>                            
                        </Columns>    
                        <Joins>        
                            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ShipmentType" JoinTableKey="ID" TableName="tbl_Shipment" TableKey="ShipmentTypeID" runat="server" />
                            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Company" JoinTableKey="ID" TableName="tbl_Shipment" TableKey="BuyerCompanyID" runat="server" />                            
                            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_CompanyLegalAccount" JoinTableKey="ID" TableName="tbl_Shipment" TableKey="BuyerCompanyLegalAccountID" runat="server" />
                        </Joins>
                   </labitec:Grid>
                </div>
            </td>
        </tr>
  </table>    
</asp:Content>
