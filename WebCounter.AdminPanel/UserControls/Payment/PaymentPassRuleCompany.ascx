<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentPassRuleCompany.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Payment.PaymentPassRuleCompany" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Enumerations" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
      function PopUpShowing(sender, eventArgs) {
          var popUp = eventArgs.get_popUp();
          $(popUp).css("position", "fixed");
          popUp.style.left = Math.round((($(document).width() - $(popUp).width()) / 2)).toString() + "px";
          popUp.style.top = "20px";
      }
  </script>
</telerik:RadCodeBlock>


<telerik:RadGrid ID="rgPaymentPassRuleCompany" runat="server" OnItemDataBound="rgPaymentPassRuleCompany_OnItemDataBound" OnNeedDataSource="rgPaymentPassRuleCompany_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgPaymentPassRuleCompany_DeleteCommand" OnInsertCommand="rgPaymentPassRuleCompany_InsertCommand" OnUpdateCommand="rgPaymentPassRuleCompany_UpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>         			
				<telerik:GridTemplateColumn UniqueName="PayerID" HeaderText="Плательщик">			
					<ItemTemplate>
					    <%# Eval("Payer")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="PayerLegalAccountID" HeaderText="ЮЛ Плательщика">			
					<ItemTemplate>
					    <%# Eval("PayerLegalAccount")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="RecipientID" HeaderText="Получатель">			
					<ItemTemplate>
					    <%# Eval("Recipient")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="RecipientLegalAccountID" HeaderText="ЮЛ Получателя">			
					<ItemTemplate>
					    <%# Eval("RecipientLegalAccount")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Контрагенты" CaptionFormatString="Контрагенты">
				<PopUpSettings Modal="true" Width="820px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">
                            <asp:HiddenField ID="hdnID" runat="server"/>
							<div class="two-columns order-product">                        						
								<div class="left-column">
								    <div class="row row-dictionary">
									    <label>Плательщик:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbPayer" DictionaryName="tbl_Company" AutoPostBack="true" OnSelectedIndexChanged="dcbPayer_OnSelectedIndexChanged"   DataTextField="Name" ShowEmpty="true" Width="230px" />
								    </div>
								    <div class="row row-dictionary">
									    <label>ЮЛ Плательщика:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbPayerLegalAccount" DictionaryName="tbl_CompanyLegalAccount" DataTextField="Title" ShowEmpty="true" Width="230px" />
								    </div>
								</div>
								<div class="right-column">
								    <div class="row row-dictionary">
									    <label>Получатель:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbRecipient" DictionaryName="tbl_Company" AutoPostBack="true" OnSelectedIndexChanged="dcbRecipient_OnSelectedIndexChanged"   DataTextField="Name" ShowEmpty="true" Width="230px" />
								    </div>
								    <div class="row row-dictionary">
									    <label>ЮЛ Получателя:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbRecipientLegalAccount" DictionaryName="tbl_CompanyLegalAccount" DataTextField="Title" ShowEmpty="true" Width="230px" />
								    </div>
								</div>
								<div class="clear"></div>
							<br/>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="groupPaymentPassRuleCompany" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
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