<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceShipments.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Invoice.InvoiceShipments" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
      function PopUpShowingShipments(sender, eventArgs) {
          var popUp = eventArgs.get_popUp();
          $(popUp).css("position", "fixed");
          popUp.style.left = Math.round((($(document).width() - $(popUp).width()) / 2)).toString() + "px";
          popUp.style.top = Math.round($(popUp).height() / 2).toString() + "px";
      }	
  </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
    <uc:NotificationMessage runat="server" ID="ucMessage" MessageType="Warning" Style="margin-top: 0;margin-bottom: 10px" />
    <telerik:RadButton runat="server" ID="rbtnGenerateShipment" Skin="Windows7" ButtonType="StandardButton" Text="Сформировать документ" OnClick="rbtnGenerateShipment_OnClick" />
    <br/><br/>
	<telerik:RadGrid ID="rgInvoiceShipments" runat="server" OnItemDataBound="rgInvoiceShipments_OnItemDataBound" OnNeedDataSource="rgInvoiceShipments_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgInvoiceShipments_DeleteCommand" OnInsertCommand="rgInvoiceShipments_InsertCommand" >
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>            			
				<telerik:GridBoundColumn UniqueName="Number" DataField="Number" HeaderText="Номер"/>
                <telerik:GridDateTimeColumn HeaderText="Дата документа" DataField="CreatedAt" DataFormatString="{0:dd.MM.yyyy}" UniqueName="CreatedAt"/>
                <telerik:GridTemplateColumn UniqueName="ShipmentType" HeaderText="Тип документа">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="lrlShipmentType" />
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
									<label>Документ:</label>
									<uc:DictionaryOnDemandComboBox runat="server" ID="dcbShipments" CssClass="select-text" ValidationErrorMessage="Вы не выбрали счет" ValidationGroup="groupUpdateInvoiceShipment" Mask="Счет № #Number от #CreatedAt" DictionaryName="tbl_Shipment" DataTextField="Number" ShowEmpty="true" Width="230px" />
                                    <span style="font-weight: normal">Сумма:</span>&nbsp;
                                    <telerik:RadNumericTextBox runat="server" ID="rntxtShipmentAmount" AutoPostBack="True" OnTextChanged="rntxtShipmentAmount_OnTextChanged" EmptyMessage="" Value="0" MinValue="0" CssClass="input-text w100" Type="Number" Width="100px">
							            <NumberFormat GroupSeparator="" DecimalDigits="2" /> 							
						            </telerik:RadNumericTextBox>
								</div>
								<div class="clear"></div>						
							<br/>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateInvoiceShipment" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
								<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
							</div>                        
						</div>                              
					</asp:Panel> 
				</FormTemplate>
			</EditFormSettings>
		</MasterTableView>
		<ClientSettings>
		    <ClientEvents OnPopUpShowing="PopUpShowingShipments" />			
			<Selecting AllowRowSelect="true" />
		</ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>