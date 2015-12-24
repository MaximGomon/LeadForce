<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderProducts.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Order.OrderProducts" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="PriceListComboBox" Src="~/UserControls/Order/PriceListComboBox.ascx" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">	  
	  function PopUpShowing(sender, eventArgs) {
	      var popUp = eventArgs.get_popUp();
	      $(popUp).css("position", "fixed");
	      popUp.style.left = Math.round((($(document).width() - $(popUp).width()) / 2)).toString() + "px";
		  popUp.style.top = "20px";
	  }
	  function Blur(sender, args) {
		  $find("<%= ajaxPanel.ClientID%>").ajaxRequestWithTarget("<%= ajaxPanel.UniqueID %>", sender.get_id());
	  }
  </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<telerik:RadGrid ID="rgOrderProducts" runat="server" OnItemDataBound="rgOrderProducts_OnItemDataBound" OnNeedDataSource="rgOrderProducts_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgOrderProducts_DeleteCommand" OnInsertCommand="rgOrderProducts_InsertCommand" OnUpdateCommand="rgOrderProducts_UpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>            			
				<telerik:GridTemplateColumn UniqueName="Product" HeaderText="Продукт">			
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlProductName" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridNumericColumn UniqueName="Quantity" HeaderText="Количество" DataField="Quantity" DataFormatString="{0:F}" />
				<telerik:GridTemplateColumn UniqueName="Unit" HeaderText="Единица измерения">			
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlUnitName" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="StockQuantity" HeaderText="Остаток">
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlStockQuantity" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridNumericColumn UniqueName="CurrencyPrice" HeaderText="Цена в валюте" DataField="CurrencyPrice" DataFormatString="{0:F}" />
				<telerik:GridNumericColumn UniqueName="CurrencyAmount" HeaderText="Сумма в валюте" DataField="CurrencyAmount" DataFormatString="{0:F}" />                
				<telerik:GridTemplateColumn UniqueName="Currency" HeaderText="Валюта">
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlCurrencyName" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridNumericColumn UniqueName="Price" HeaderText="Цена" DataField="Price" DataFormatString="{0:F}" />
				<telerik:GridNumericColumn UniqueName="Amount" HeaderText="Сумма" DataField="Amount" DataFormatString="{0:F}" />
				<telerik:GridNumericColumn UniqueName="CurrencyTotalAmount" HeaderText="Итого в валюте" DataField="CurrencyTotalAmount" DataFormatString="{0:F}" />
				<telerik:GridNumericColumn UniqueName="TotalAmount" HeaderText="Итого" NumericType="Number" DataField="TotalAmount" DataFormatString="{0:F}" />
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Продукт в заказе" CaptionFormatString="Продукт в заказе">
				<PopUpSettings Modal="true" Width="820px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">
							<div class="two-columns order-product">                        						
								<div class="row row-dictionary">
									<label>Продукт:</label>
									<uc:DictionaryComboBox runat="server" ID="dcbProducts" AutoPostBack="true" OnSelectedIndexChanged="dcbProducts_OnSelectedIndexChanged" ValidationErrorMessage="Вы не выбрали продукт" ValidationGroup="groupUpdateOrderProduct" DictionaryName="tbl_Product" DataTextField="Title" ShowEmpty="true" Width="230px" />
								</div>
								<div class="row">
									<label>Произвольный продукт:</label>
									<asp:TextBox runat="server" ID="txtAnyProductName" CssClass="input-text" Width="630px" />
								</div>
								<div class="row">
									<label>Серийный номер:</label>
									<asp:TextBox runat="server" ID="txtSerialNumber" CssClass="input-text" />
								</div>
								<h3>Стоимость в заказе</h3>
								<div class="row row-dictionary">
									<label>Прайс-лист:</label>
									<uc:PriceListComboBox runat="server" ID="dcbPriceList" AutoPostBack="true" ValidationGroup="groupUpdateOrderProduct" OnSelectedIndexChanged="dcbPriceList_SelectedIndexChanged" ValidationErrorMessage="Вы не выбрали прайс-лист" ShowEmpty="true" />
								</div>
								<div class="left-column">
									<div class="row">
										<label>Количество:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtQuantity" EmptyMessage="" Value="1" MinValue="1" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
											<ClientEvents OnBlur="Blur"/>
										</telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rntxtQuantity" CssClass="required" Text="*" ErrorMessage="Вы не ввели количество" ValidationGroup="groupUpdateOrderProduct" runat="server" />
									</div>
									<div class="row">
										<label>Валюта:</label>
										<uc:DictionaryComboBox runat="server" ID="dcbCurrency" ValidationGroup="groupUpdateOrderProduct" ValidationErrorMessage="Вы не выбрали валюту" DictionaryName="tbl_Currency" DataTextField="Name" ShowEmpty="true" />
									</div>                            
									<div class="row">
										<label>Цена в валюте:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtCurrencyPrice" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" />
											<ClientEvents OnBlur="Blur"/> 
										</telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtCurrencyPrice" CssClass="required" Text="*" ErrorMessage="Вы не ввели цену в валюте" ValidationGroup="groupUpdateOrderProduct" runat="server" />
									</div>
									<div class="row row-dictionary">
										<label>Сумма в валюте:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtCurrencyAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
											<ClientEvents OnBlur="Blur"/>
										</telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rntxtCurrencyAmount" CssClass="required" Text="*" ErrorMessage="Вы не ввели сумму в валюте" ValidationGroup="groupUpdateOrderProduct" runat="server" />
									</div>
								</div>
								<div class="right-column">
									<div class="row row-dictionary">
										<label>Единица измерения:</label>
										<uc:DictionaryComboBox runat="server" ID="dcbUnit" ValidationGroup="groupUpdateOrderProduct" ValidationErrorMessage="Вы не выбрали единицу измерения" DictionaryName="tbl_Unit" DataTextField="Title" ShowEmpty="true" />
									</div>
									<div class="row">
										<label>Курс:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtRate" EmptyMessage="" Value="1" MinValue="1" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" />
											<ClientEvents OnBlur="Blur"/> 
										</telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtRate" CssClass="required" Text="*" ErrorMessage="Вы не ввели курс" ValidationGroup="groupUpdateOrderProduct" runat="server" />
									</div>
									<div class="row">
										<label>Цена:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtPrice" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
											<ClientEvents OnBlur="Blur"/>
										</telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtPrice" CssClass="required" Text="*" ErrorMessage="Вы не ввели цену" ValidationGroup="groupUpdateOrderProduct" runat="server" />
									</div>
									<div class="row">
										<label>Сумма:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
											<ClientEvents OnBlur="Blur"/>
										</telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rntxtAmount" CssClass="required" Text="*" ErrorMessage="Вы не ввели сумму" ValidationGroup="groupUpdateOrderProduct" runat="server" />
									</div>  
								</div>
								<div class="clear"></div>
								<h3>Скидки</h3>
								<div class="left-column">
									<div class="row row-dictionary">
										<label>Спецпредложение:</label>
										<uc:PriceListComboBox runat="server" ID="dcbSpecialOfferPriceList" PriceListType="Discount" ShowEmpty="true" />
									</div>
									<div class="row">
										<label>Сумма скидки в валюте:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtCurrencyDiscountAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
											<ClientEvents OnBlur="Blur"/>
										</telerik:RadNumericTextBox>                                
									</div>
								</div>
								<div class="right-column">
									<div class="row">
										<label>Сумма скидки:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtDiscountAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
											<ClientEvents OnBlur="Blur"/>
										</telerik:RadNumericTextBox>
									</div>
									<div class="row">
										<label>Процент скидки:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtDiscount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
											<ClientEvents OnBlur="Blur"/>
										</telerik:RadNumericTextBox>
									</div>
								</div>
								<div class="clear"></div>
								<h3>Итоги</h3>
								<div class="left-column">
									<div class="row">
										<label>Итого в валюте:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtCurrencyTotalAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" />
											<ClientEvents OnBlur="Blur"/> 
										</telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rntxtCurrencyTotalAmount" CssClass="required" Text="*" ErrorMessage="Вы не ввели иотого в валюте" ValidationGroup="groupUpdateOrderProduct" runat="server" />
									</div>
								</div>
								<div class="right-column">
									<div class="row">
										<label>Итого:</label>
										<telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtTotalAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
											<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
											<ClientEvents OnBlur="Blur"/>
										</telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rntxtTotalAmount" CssClass="required" Text="*" ErrorMessage="Вы не ввели иотого" ValidationGroup="groupUpdateOrderProduct" runat="server" />
									</div>
								</div>
								<div class="clear"></div>						
							<br/>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateOrderProduct" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
								<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
							</div>                        
						</div>                              
					</asp:Panel> 
				</FormTemplate>
			</EditFormSettings>
		</MasterTableView>
		<ClientSettings>
			<ClientEvents OnPopUpShowing="PopUpShowing" />
			<Selecting AllowRowSelect="true" />
		</ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>