<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="WebCounter.AdminPanel.Invoice" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="InvoiceProducts" Src="~/UserControls/Invoice/InvoiceProducts.ascx" %>
<%@ Register TagPrefix="uc" TagName="PriceListComboBox" Src="~/UserControls/Order/PriceListComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContentComments" Src="~/UserControls/Shared/ContentComments.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<%@ Register TagPrefix="uc" TagName="InvoiceShipments" Src="~/UserControls/Invoice/InvoiceShipments.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function ShowSendInvoiceRadWindow() {
                $find('<%= rwSendInvoice.ClientID %>').show();

            }
            function CloseSendInvoiceRadWindow() {
                $find('<%= rwSendInvoice.ClientID %>').close();
            }
        </script>
    </telerik:RadScriptBlock>
    
<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <telerik:RadPanelBar ID="rpbLeft" Width="189px" Skin="Windows7" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Теги">
                            <ContentTemplate>
                                <labitec:Tags ID="tagsInvoices" ObjectTypeName="tbl_Invoice" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Expanded="true" Text="Отчеты" Visible="false" Value="Reports">
                            <ContentTemplate>
                                <ul class="reports">
                                    <li><asp:HyperLink runat="server" Text="Счет без печати" ID="hlWithoutStamp" Target="_blank" /></li>
                                    <li><asp:HyperLink runat="server" Text="Счет с печатью" ID="hlWithStamp" Target="_blank" /></li>
                                </ul>                                        
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <div class="two-columns">	    
	            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						            CssClass="validation-summary"
						            runat="server"
						            EnableClientScript="true"
						            HeaderText="Заполните все поля корректно:"
						            ValidationGroup="groupInvoice" />
                        
                    <table>
                        <tr>
                            <td>
                                <div class="row">
				                    <label>Номер:</label>				
			                        <asp:Literal runat="server" ID="lrlNumber"/>
			                    </div>
                            </td>
                            <td>
                                <div style="position: relative" class="row clearfix">
				                    <label>Дата счета:</label>
				                    <telerik:RadDatePicker runat="server" AutoPostBack="True" MinDate="01.01.1900" CssClass="date-picker" ID="rdpInvoiceDate" ShowPopupOnFocus="true" Width="110px">
						                    <DateInput Enabled="true" />
						                    <DatePopupButton Enabled="true" />
					                    </telerik:RadDatePicker>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdpInvoiceDate" Text="*" ErrorMessage="Вы не ввели дату счета" ValidationGroup="groupInvoice" runat="server" />
				
			                    </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row row-dictionary">
				                    <label>Тип счета:</label>
				                    <uc:DictionaryComboBox ID="dcbInvoiceType" SelectDefaultValue="true" DictionaryName="tbl_InvoiceType" ValidationGroup="groupInvoice" ValidationErrorMessage="Вы не выбрали тип счета" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
			                    </div>
                            </td>
                            <td>
                                <div class="row">
				                    <label>Состояние счета:</label>
				                    <asp:DropDownList runat="server" ID="ddlInvoiceStatus" CssClass="select-text" />
				                    <asp:RequiredFieldValidator ID="rfvOrderstatus" ControlToValidate="ddlInvoiceStatus" ValidationGroup="groupInvoice" Text="*" ErrorMessage="Вы не выбрали состояние счета" runat="server" />
			                    </div>
                            </td>
                        </tr>
                    </table>        
		            <div class="row">
			            <label>Примечание:</label>
			            <asp:TextBox runat="server" ID="txtNote" CssClass="area-text" Width="630px" Height="30px" TextMode="MultiLine" />
		            </div>
		            <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			            <Tabs>
				            <telerik:RadTab Text="Основные данные" />
                            <telerik:RadTab Text="Продукты" />	
                            <telerik:RadTab Text="История изменений" />			
                            <telerik:RadTab Text="Закрывающие документы" Value="Shipments" />	
			            </Tabs>
		            </telerik:RadTabStrip>
		            <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			            <telerik:RadPageView ID="RadPageView1" runat="server">
				            <div class="left-column">
				                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
					                <div class="row">
						                <label>Заказчик:</label>						
                                        <uc:DictionaryOnDemandComboBox ID="dcbBuyerCompany" AutoPostBack="True" OnSelectedIndexChanged="dcbBuyerCompany_OnSelectedIndexChanged" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>
                                    <div class="row">
						                <label>ЮЛ заказчика:</label>						
                                        <uc:DictionaryOnDemandComboBox ID="dcbBuyerCompanyLegalAccount" AutoPostBack="True" OnSelectedIndexChanged="dcbBuyerCompanyLegalAccount_OnSelectedIndexChanged" DictionaryName="tbl_CompanyLegalAccount" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>                                
					                <div class="row">
						                <label>Контакт заказчика:</label>
						                <uc:ContactComboBox ID="ucBuyerContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
					                </div>
                                </telerik:RadAjaxPanel>
				            </div>
				            <div class="right-column">
				                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
					                <div class="row">
						                <label>Исполнитель:</label>
						                <uc:DictionaryOnDemandComboBox ID="dcbExecutorCompany" AutoPostBack="true" OnSelectedIndexChanged="dcbExecutorCompany_OnSelectedIndexChanged" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>
                                    <div class="row">
						                <label>ЮЛ исполнителя:</label>						
                                        <uc:DictionaryOnDemandComboBox ID="dcbExecutorCompanyLegalAccount" AutoPostBack="True" OnSelectedIndexChanged="dcbExecutorCompanyLegalAccount_OnSelectedIndexChanged" DictionaryName="tbl_CompanyLegalAccount" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>                                
					                <div class="row">
						                <label>Контакт исполнителя:</label>
						                <uc:ContactComboBox ID="ucExecutorContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
					                </div>
                                </telerik:RadAjaxPanel>
				            </div>		
                            <div class="clear"></div>
                            <h3>Финансы</h3>
                            <div class="left-column">
                                <div class="row">
						            <label>Сумма счета:</label>
                                    <asp:Literal runat="server" ID="lrlInvoiceAmount" Text="0"/>
                                </div>					
                            </div>
                            <div class="right-column">
                                <div class="row">
						            <label>Оплачено:</label>
                                    <telerik:RadNumericTextBox runat="server" ID="rntxtPaid" EmptyMessage="" Value="0" MinValue="0" CssClass="input-text" Type="Number">
							            <NumberFormat GroupSeparator="" DecimalDigits="2" /> 							
						            </telerik:RadNumericTextBox>
                                </div>					
                            </div>
                            <div class="clear"></div>
                            <div class="left-column">
                                <div class="row">
						            <label>Дата оплаты, план:</label>
						            <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpPaymentDatePlanned" ShowPopupOnFocus="true" Width="110px">
							            <DateInput Enabled="true" />
							            <DatePopupButton Enabled="true" />
						            </telerik:RadDatePicker>
					            </div>
                                <div class="row">
						            <label>Дата оплаты фиксирована договором:</label>
                                    <asp:CheckBox runat="server" ID="chxIsPaymentDateFixedByContract"/>						
					            </div>                    
                            </div>
                            <div class="right-column">
                                <div class="row">
						            <label>Дата оплаты, факт:</label>                        
						            <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpPaymentDateActual" ShowPopupOnFocus="true" Width="110px">
							            <DateInput Enabled="true" />
							            <DatePopupButton Enabled="true" />
						            </telerik:RadDatePicker>
					            </div>
                                <div class="row">
						            <label>Есть претензия заказчика:</label>                        
						            <asp:CheckBox runat="server" ID="chxIsExistBuyerComplaint"/>
					            </div>
                            </div>
                            <div class="clear"></div>
				            <h3>Связи счета</h3>
                            <div class="row row-dictionary">
                                <label>Заказы:</label>    
                                <uc:DictionaryOnDemandComboBox ID="dcbOrders" DictionaryName="tbl_Order" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>
                            </div>
				            <div class="row row-dictionary">
					            <label>Прайс-лист:</label>
                                <uc:PriceListComboBox runat="server" ID="ucPriceList" ShowEmpty="true" />					
				            </div>
			            </telerik:RadPageView>			
                        <telerik:RadPageView ID="RadPageView2" runat="server">
				            <uc:InvoiceProducts runat="server" ID="ucInvoiceProducts" />
			            </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">
				            <labitec:Grid ID="gridInvoiceHistory" OnItemDataBound="gridInvoiceHistory_OnItemDataBound" Toolbar="false" TableName="tbl_InvoiceHistory" Fields="tbl_Contact.ID" ClassName="WebCounter.AdminPanel.InvoiceHistory" runat="server">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата изменения" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn3" DataField="tbl_Contact.UserFullName" HeaderText="Автор изменения" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlUserFullName" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn2" DataField="PaymentDatePlanned" HeaderText="Дата оплаты, план" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn4" DataField="PaymentDateActual" HeaderText="Дата оплаты, факт" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn5" DataField="InvoiceAmount" HeaderText="Сумма счета" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn6" DataField="InvoiceStatusID" HeaderText="Состояние счета" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlInvoiceStatus" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn7" DataField="IsExistBuyerComplaint" HeaderText="Есть претензия заказчика" runat="server" DataType="Boolean"/>
                                    <labitec:GridColumn ID="GridColumn8" DataField="Note" HeaderText="Примечание" runat="server"/>
                                </Columns>
                                <Joins>                        
                                    <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_InvoiceHistory" TableKey="AuthorID" runat="server" />                
                                </Joins>
                            </labitec:Grid>
                            <asp:Panel runat="server" ID="plComments" Visible="false">
                                <br/><br/>
                                <uc:ContentComments runat="server" ID="ucContentComments" ExpandComments="false" CommentType="tbl_InvoiceComment" EnableHtmlCommentEditor="true" />                    
                            </asp:Panel>
			            </telerik:RadPageView>
                        <telerik:RadPageView runat="server">
                            <uc:InvoiceShipments runat="server" ID="ucInvoiceShipments" />
                        </telerik:RadPageView>
		            </telerik:RadMultiPage>
		            <br/>
		            <div class="buttons">
			            <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupInvoice" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                        <asp:LinkButton ID="lbtnShowSendInvoiceRadWindow" OnClientClick="ShowSendInvoiceRadWindow();return false;" CssClass="btn" Visible="false" runat="server"><em>&nbsp;</em><span>Направить счет по email</span></asp:LinkButton>
			            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		            </div>
	            </div>
        </td>
    </tr>
</table>	

<telerik:RadWindow ID="rwSendInvoice" runat="server" Title="Направить счет" AutoSize="false" Height="170px" Width="900px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>        
        <div class="radwindow-popup-inner send-invoice-popup">                    
            <div class="left-column">
                <div class="row">
                    <label>Заказчику:</label>
                    <asp:CheckBox runat="server" ID="chxForBuyer" Checked="True"/>
                </div>
                <div class="row">
					<label>Контакт заказчика:</label>
					<uc:ContactComboBox ID="ucSendInvoiceBuyerContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
				</div>
            </div>
            <div class="right-column">
                <div class="row">
                    <label>Ответственному:</label>
                    <asp:CheckBox runat="server" ID="chxForExecutor" Checked="True" />
                </div>
                <div class="row">
					<label>Контакт исполнителя:</label>
					<uc:ContactComboBox ID="ucSendInvoiceExecutorContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
				</div>
            </div>
            <div class="clear"></div>
            <div class="buttons clearfix">                        
				<asp:LinkButton ID="lbtnSendInvoice" OnClick="lbtnSendInvoice_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Направить счет</span></asp:LinkButton>
				<a href="javascript:;" class="cancel" onclick="CloseSendInvoiceRadWindow(); return false;">Отмена</a>
            </div>
        </div>        
    </ContentTemplate>
</telerik:RadWindow>


</asp:Content>
