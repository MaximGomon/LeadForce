<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShipmentInvoices.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Shipment.ShipmentInvoices" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
      function PopUpShowingInvoices(sender, eventArgs) {
          var popUp = eventArgs.get_popUp();
          $(popUp).css("position", "fixed");
          popUp.style.left = Math.round((($(document).width() - $(popUp).width()) / 2)).toString() + "px";
          popUp.style.top = Math.round($(popUp).height() / 2).toString() + "px";
      }	 
  </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<telerik:RadGrid ID="rgShipmentInvoices" runat="server" OnItemDataBound="rgShipmentInvoices_OnItemDataBound" OnNeedDataSource="rgShipmentInvoices_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgShipmentInvoices_DeleteCommand" OnInsertCommand="rgShipmentInvoices_InsertCommand" >
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>            			
				<telerik:GridBoundColumn UniqueName="Number" DataField="Number" HeaderText="Номер"/>
                <telerik:GridDateTimeColumn HeaderText="Дата счета" DataField="CreatedAt" DataFormatString="{0:dd.MM.yyyy}" UniqueName="CreatedAt"/>
                <telerik:GridTemplateColumn UniqueName="InvoiceType" HeaderText="Тип счета">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="lrlInvoiceType" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>                
				<telerik:GridBoundColumn UniqueName="Note" DataField="Note" HeaderText="Примечание"/>
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Счет" CaptionFormatString="Счет">
				<PopUpSettings Modal="true" Width="820px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">					       
							<div class="two-columns order-product">							    
								<div class="row">
									<label>Счет:</label>
									<uc:DictionaryOnDemandComboBox runat="server" ID="dcbInvoices" CssClass="select-text" ValidationErrorMessage="Вы не выбрали счет" ValidationGroup="groupUpdateShipmentInvoice" Mask="Счет № #Number от #CreatedAt" DictionaryName="tbl_Invoice" DataTextField="Number" ShowEmpty="true" Width="230px" />
                                    <span style="font-weight: normal">Сумма:</span>&nbsp;
                                    <telerik:RadNumericTextBox runat="server" ID="rntxtShipmentAmount" AutoPostBack="True" OnTextChanged="rntxtShipmentAmount_OnTextChanged" EmptyMessage="" Value="0" MinValue="0" CssClass="input-text w100" Type="Number" Width="100px">
							            <NumberFormat GroupSeparator="" DecimalDigits="2" /> 							
						            </telerik:RadNumericTextBox>
								</div>
								<div class="clear"></div>						
							<br/>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateShipmentInvoice" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
								<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
							</div>                        
						</div>                              
					</asp:Panel> 
				</FormTemplate>
			</EditFormSettings>
		</MasterTableView>
		<ClientSettings>
		    <ClientEvents OnPopUpShowing="PopUpShowingInvoices" />			
			<Selecting AllowRowSelect="true" />
		</ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>