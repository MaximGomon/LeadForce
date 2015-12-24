<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentPass.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Payment.PaymentPass" %>
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


<telerik:RadGrid ID="rgPaymentPass" runat="server" OnItemDataBound="rgPaymentPass_OnItemDataBound" OnNeedDataSource="rgPaymentPass_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgPaymentPass_DeleteCommand" OnInsertCommand="rgPaymentPass_InsertCommand" OnUpdateCommand="rgPaymentPass_UpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>         			
				<telerik:GridTemplateColumn UniqueName="OutgoPaymentPassCategoryID" HeaderText="Категория расход">			
					<ItemTemplate>
					    <%# Eval("OutgoPaymentPassCategory") %>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="OutgoCFOID" HeaderText="ЦФО расход">			
					<ItemTemplate>
					    <%# Eval("OutgoCFO") %>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="OutgoPaymentArticleID" HeaderText="Статья расход">			
					<ItemTemplate>
					    <%# Eval("OutgoPaymentArticle") %>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="IncomePaymentPassCategoryID" HeaderText="Категория приход">			
					<ItemTemplate>
					    <%# Eval("IncomePaymentPassCategory") %>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="IncomeCFOID" HeaderText="ЦФО приход">			
					<ItemTemplate>
					    <%# Eval("IncomeCFO") %>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="IncomePaymentArticleID" HeaderText="Статья приход">			
					<ItemTemplate>
					    <%# Eval("IncomePaymentArticle") %>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="FormulaID" HeaderText="Формула расчета">			
					<ItemTemplate>
						<%# Eval("FormulaID") != null ? EnumHelper.GetEnumDescription((PaymentPassFormula)int.Parse(Eval("FormulaID").ToString())):""%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridNumericColumn UniqueName="Value" HeaderText="Значение для расчета" DataField="Value" DataFormatString="{0:F}" />
				<telerik:GridNumericColumn UniqueName="Amount" HeaderText="Сумма" DataField="Amount" DataFormatString="{0:F}" />                
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Проводки " CaptionFormatString="Проводки ">
				<PopUpSettings Modal="true" Width="820px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">
                            <asp:HiddenField ID="hdnID" runat="server"/>
							<div class="two-columns order-product">                        						
								<div class="left-column">
								    <div class="row row-dictionary">
									    <label>Категория расход:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbOutgoPaymentPassCategory" DictionaryName="tbl_PaymentPassCategory" AutoPostBack="true" OnSelectedIndexChanged="dcbOutgoPaymentPassCategory_OnSelectedIndexChanged"   DataTextField="Title" ShowEmpty="true" Width="230px" />
								    </div>
								    <div class="row row-dictionary">
									    <label>ЦФО расход:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbOutgoCFO" DictionaryName="tbl_PaymentCFO" DataTextField="Title" ShowEmpty="true" Width="230px" />
								    </div>
								    <div class="row row-dictionary">
									    <label>Статья расходд:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbOutgoPaymentArticle" DictionaryName="tbl_PaymentArticle" DataTextField="Title" ShowEmpty="true" Width="230px" />
								    </div>
								</div>
								<div class="right-column">
								    <div class="row row-dictionary">
									    <label>Категория приход:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbIncomePaymentPassCategory" DictionaryName="tbl_PaymentPassCategory" AutoPostBack="true" OnSelectedIndexChanged="dcbIncomePaymentPassCategory_OnSelectedIndexChanged"   DataTextField="Title" ShowEmpty="true" Width="230px" />
								    </div>
								    <div class="row row-dictionary">
									    <label>ЦФО приход:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbIncomeCFO" DictionaryName="tbl_PaymentCFO" DataTextField="Title" ShowEmpty="true" Width="230px" />
								    </div>
								    <div class="row row-dictionary">
									    <label>Статья приход:</label>
									    <uc:DictionaryComboBox runat="server" ID="dcbIncomePaymentArticle" DictionaryName="tbl_PaymentArticle" DataTextField="Title" ShowEmpty="true" Width="230px" />
								    </div>
								</div>
								<div class="clear"></div>
								<div class="row">
									<label>Формула расчета:</label>
                                    <asp:DropDownList ID="ddlFormula" CssClass="select-text" AutoPostBack="true"  OnSelectedIndexChanged="ddlFormula_OnSelectedIndexChanged" runat="server" />
								</div>
								<div class="row">
									<label>Значение для расчета:</label>
									<telerik:RadNumericTextBox runat="server" ID="rntxtValue" Value="0" EmptyMessage="" CssClass="input-text" Type="Number" OnTextChanged="rntxtValue_OnTextChanged" AutoPostBack="true">
										<NumberFormat GroupSeparator="" DecimalDigits="2" />
									</telerik:RadNumericTextBox>
								</div>
                                <asp:Panel runat="server" ID="plAmount">
								<div class="row">
									<label>Сумма:</label>
									<telerik:RadNumericTextBox runat="server" ID="rntxtAmount" Value="0" EmptyMessage="" CssClass="input-text" Type="Number" Enabled="false">
										<NumberFormat GroupSeparator="" DecimalDigits="2" /> 
									</telerik:RadNumericTextBox>
								</div>
                                </asp:Panel>
								<div class="clear"></div>
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