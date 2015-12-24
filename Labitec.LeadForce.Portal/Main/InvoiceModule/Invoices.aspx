<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.InvoiceModule.Invoices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#requestFilters input').first().parent().find('label').css('font-weight', 'bold');
        });
        function Checked(element) {
            $('#requestFilters input:radio').parent().find('label').css('font-weight', 'normal');
            $(element).parent().find('label').css('font-weight', 'bold');
        }        
    </script>
        <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Фильтр по статусам">
                    <ContentTemplate>
                        <ul id="requestFilters">
                            <li><asp:RadioButton ID="ToPay" runat="server" GroupName="filters" Text="К оплате" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                            <li><asp:RadioButton ID="Paid" runat="server" GroupName="filters" Text="Оплачены" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                            <li><asp:RadioButton ID="All" runat="server" GroupName="filters" Text="Все счета" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                        </ul>
                    </ContentTemplate>
                </telerik:RadPanelItem>                
            </Items>
        </telerik:RadPanelBar>
    </div>
    <div class="grid-container-margin">        
                <labitec:Search ID="searchInvoices" GridControlID="gridInvoices" OnDemand="True" SearchBy="tbl_Company.Name,tbl_CompanyLegalAccount.Title" runat="server" />                                
                <div class="list-grid">
                    <labitec:Grid ID="gridInvoices" AccessCheck="true" ShowHeader="false" Toolbar="false" SearchControlID="searchInvoices" TableName="tbl_Invoice" ClassName="Labitec.LeadForce.Portal.InvoicesList" Fields="tbl_CompanyLegalAccount.Title, PaymentDateActual, InvoiceStatusID, ID, Note,InvoiceAmount, CreatedAt, tbl_Company.ID, tbl_Company.Name" Export="true" runat="server" OnItemDataBound="gridInvoices_OnItemDataBound">
                        <Columns>
                            <labitec:GridColumn ID="GridColumn1" DataField="Number" HeaderText="Номер" runat="server">
                                <ItemTemplate>
                                    <div class="main-description">
                                        <div class="grid-left-column">
                                            <asp:HyperLink id="hlTitle" class="url" runat="server" />
                                            <div class="title">Заказчик: <asp:Literal ID="lrlCompany" runat="server" /></div> 
                                            <div class="title">Юридическое лицо: <asp:Literal ID="lrlCompanyLegalAccount" runat="server" /></div> 
                                            <div class="title">Сумма: <asp:Literal ID="lrlInvoiceAmount" runat="server" /></div> 
                                            <div class="title"><asp:Literal ID="lrlNote" runat="server" /></div> 
                                        </div>
                                        <div class="grid-right-column">
                                            <div class="title">Статус: <asp:Literal ID="lrlInvoiceStatus" runat="server"/></div>
                                            <div class="title">Дата оплаты: <asp:Literal ID="lrlPaymentDateActual" runat="server"/></div>                                        
                                        </div>
                                        <div class="clear"></div>
                                    </div>
                                </ItemTemplate>
                            </labitec:GridColumn>
                            <labitec:GridColumn ID="GridColumn3" CssClass="actions" DataField="ID" HeaderText="Операции" Height="65px" HorizontalAlign="Left" VerticalAlign="Middle" runat="server">
                                <ItemTemplate>                                    
                                    <asp:HyperLink ID="hlEdit" Text="Просмотреть" CssClass="action-edit" runat="server"/>                                    
                                    <asp:HyperLink runat="server" ID="hlPrint" CssClass="action-print" Text="Распечатать" ToolTip="Печать" Target="_blank"/>                                                                
                                </ItemTemplate>
                            </labitec:GridColumn>                            
                        </Columns>    
                        <Joins>        
                            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_InvoiceType" JoinTableKey="ID" TableName="tbl_Invoice" TableKey="InvoiceTypeID" runat="server" />
                            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Company" JoinTableKey="ID" TableName="tbl_Invoice" TableKey="BuyerCompanyID" runat="server" />                            
                            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_CompanyLegalAccount" JoinTableKey="ID" TableName="tbl_Invoice" TableKey="BuyerCompanyLegalAccountID" runat="server" />
                        </Joins>
                   </labitec:Grid>                   
                </div>                        
    </div>
</asp:Content>
