<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyLegalAccount.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.CompanyLegalAccount" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="Contact" Src="~/UserControls/Contact/ContactComboBox.ascx" %>

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
<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<telerik:RadGrid ID="rgCompanyLegalAccounts" runat="server" OnItemDataBound="rgCompanyLegalAccounts_OnItemDataBound" OnNeedDataSource="rgCompanyLegalAccounts_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgCompanyLegalAccounts_DeleteCommand" OnInsertCommand="rgCompanyLegalAccounts_InsertCommand" OnUpdateCommand="rgCompanyLegalAccounts_UpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>
			    <telerik:GridBoundColumn UniqueName="Title" DataField="Title" HeaderText="Название" />
                <telerik:GridBoundColumn UniqueName="OfficialTitle" DataField="OfficialTitle" HeaderText="Официальное название" />
				<telerik:GridCheckBoxColumn UniqueName="IsPrimary" DataField="IsPrimary" HeaderText="Основной" />
                <telerik:GridCheckBoxColumn UniqueName="IsActive" DataField="IsActive" HeaderText="Активный" />
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Юридическое лицо" CaptionFormatString="Юридическое лицо">
				<PopUpSettings Modal="true" Width="820px" Height="620px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">
							<div class="two-columns company-legal">                        														
								<div class="row">
									<label>Название:</label>
									<asp:TextBox runat="server" ID="txtTitle" CssClass="input-text" Width="630px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtTitle" CssClass="required" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupUpdateCompanyLegalAccount" runat="server" />
								</div>
								<div class="row">
									<label>Официальное название:</label>
									<asp:TextBox runat="server" ID="txtOfficialTitle" CssClass="input-text" Width="630px" />
								</div>
                                <div class="row">
									<label>Юридический адрес:</label>
									<asp:TextBox runat="server" ID="txtLegalAddress" CssClass="input-text" Width="630px" />
								</div>
                                <div class="row clearfix">
				                    <label>Дата регистрации:</label>
				                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpRegistrationDate" ShowPopupOnFocus="true" Width="110px">
						                <DateInput Enabled="true" />
						                <DatePopupButton Enabled="true" />
					                </telerik:RadDatePicker>
			                    </div>
								<div class="left-column">									
                                    <div class="row">
				                        <label>ОГРН:</label>
				                        <asp:TextBox runat="server" ID="txtOGRN" CssClass="input-text" />
                                    </div>
                                    <div class="row">
				                        <label>ИНН:</label>
				                        <asp:TextBox runat="server" ID="txtINN" CssClass="input-text" />
                                    </div>
                                    <div class="row">
				                        <label>КПП:</label>
				                        <asp:TextBox runat="server" ID="txtKPP" CssClass="input-text" />
                                    </div>                                    
								</div>
								<div class="right-column">
									<div class="row">
				                        <label>Р/с:</label>
				                        <asp:TextBox runat="server" ID="txtRS" CssClass="input-text" />
                                    </div>
                                    <div class="row row-dictionary">
									    <label>Банк:</label>
									    <uc:DictionaryOnDemandComboBox runat="server" ID="dcbBanks" CssClass="select-text" DictionaryName="tbl_Bank" DataTextField="Title" ShowEmpty="true" />
								    </div>
                                    <div class="row">
				                        <label>Основной:</label>
				                        <asp:CheckBox runat="server" ID="chxIsPrimary" />
                                    </div>
                                    <div class="row">
				                        <label>Активный:</label>
				                        <asp:CheckBox runat="server" ID="chxIsActive" Checked="true" />
                                    </div>
								</div>								
								<div class="clear"></div>						
                                <div class="left-column">
                                    <div class="row">
                                        <label>Руководитель:</label>
                                        <uc:Contact runat="server" ID="ucHeadContact" FilterByFullName="True" />
                                    </div>
                                    <div class="row">
                                        <label>Подпись:</label>
                                        <telerik:RadAsyncUpload ID="rauHeadSignature" runat="server" Width="100px"
                                            MaxFileSize="524288"  AllowedFileExtensions="jpg,png,gif,bmp"
                                            AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                            Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>                                                                                    
                                    </div>
                                    <div class="row">
                                        <label>&nbsp;</label>
                                        <telerik:RadBinaryImage runat="server" ID="rbiHeadSignature" Width="70px"/>
                                    </div>
                                    <div class="row">
                                        <label>Печать:</label>
                                        <telerik:RadAsyncUpload ID="rauStamp" runat="server" Width="100px"
                                            MaxFileSize="524288"  AllowedFileExtensions="jpg,png,gif,bmp"
                                            AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                            Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>                                                                                    
                                    </div>
                                    <div class="row">
                                        <label>&nbsp;</label>
                                        <telerik:RadBinaryImage runat="server" ID="rbiStamp" Height="70px"/>
                                    </div>
                                </div>
                                <div class="right-column">
                                    <div class="row">
                                        <label>Бухгалтер:</label>
                                        <uc:Contact runat="server" ID="ucAccountantContact" FilterByFullName="True" />
                                    </div>
                                    <div class="row">
                                        <label>Подпись:</label>
                                        <telerik:RadAsyncUpload ID="rauAccountantSignature" runat="server" Width="100px"
                                            MaxFileSize="524288"  AllowedFileExtensions="jpg,png,gif,bmp"
                                            AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                            Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>                                                                                    
                                    </div>
                                    <div class="row">
                                        <label>&nbsp;</label>
                                        <telerik:RadBinaryImage runat="server" ID="rbiAccountantSignature" Width="70px"/>
                                    </div>                                    
                                </div>
                                <div class="clear"></div>
							    <br/>
							    <div class="buttons clearfix">
								    <asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateCompanyLegalAccount" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
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