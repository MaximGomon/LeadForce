<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationComment.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.PublicationComment" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
	<telerik:RadGrid ID="rgPublicationComment" Skin="Windows7" Width="100%" runat="server" OnNeedDataSource="rgPublicationComment_NeedDataSource" OnDeleteCommand="rgPublicationComment_DeleteCommand"
		OnInsertCommand="rgPublicationComment_InsertCommand" OnUpdateCommand="rgPublicationComment_UpdateCommand" OnItemDataBound="rgPublicationComment_OnItemDataBound">
		<MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" DataKeyNames="ID" InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="PopUp">
			<EditFormSettings>
				<EditColumn ButtonType="ImageButton"/>
			</EditFormSettings>
			<Columns>             
            	<telerik:GridTemplateColumn HeaderText="Дата публикаций" UniqueName="Title">
					<ItemTemplate>
                        <%# Server.HtmlEncode(Eval("Date").ToString())%>                    
					</ItemTemplate>
				</telerik:GridTemplateColumn>                       
				<telerik:GridTemplateColumn HeaderText="Пользователь" UniqueName="Title">
					<ItemTemplate>
                        <%# Server.HtmlEncode(Eval("UserName").ToString())%>                    
					</ItemTemplate>
				</telerik:GridTemplateColumn>
            	<telerik:GridTemplateColumn HeaderText="Комментарий" UniqueName="Title">
					<ItemTemplate>
                        <%# Server.HtmlEncode(Eval("Comment").ToString())%>                    
					</ItemTemplate>
				</telerik:GridTemplateColumn>                       
            	<telerik:GridTemplateColumn HeaderText="Официальный ответ" UniqueName="Title">
					<ItemTemplate>
                        <%# Server.HtmlEncode(Eval("isOfficialAnswerStr").ToString())%>                    
					</ItemTemplate>
				</telerik:GridTemplateColumn>                       
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
            <EditFormSettings EditFormType="Template" InsertCaption="Комментарии" CaptionFormatString="Комментарии">
				<PopUpSettings Modal="true" Width="820px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">
                       	<div class="two-columns order-product">                        						
                            <asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" 
						    CssClass="validation-summary"
						    runat="server"
						    EnableClientScript="true"
						    HeaderText="Заполните все поля корректно:"
						    ValidationGroup="valRelatedPublicationCommentGroup" />
		                    <div class="row date-picker-autopostback clearfix">
			                    <label>Дата публикации:</label>
			                    <div class="date-picker-container">
				                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpDate" ShowPopupOnFocus="true" Width="110px">
					                    <DateInput Enabled="true" />
					                    <DatePopupButton Enabled="true" />
				                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdpDate" Text="*" ErrorMessage="Вы не ввели дату" ValidationGroup="valRelatedPublicationCommentGroup" runat="server" />
			                    </div>
		                    </div>
                            <div class="row">
								<label>Пользователь:</label>
                                <uc:ContactComboBox ID="ucUser" CssClass="select-text" runat="server" FilterByFullName="true"/>
							</div>
                            <div class="row">
                                <label>Комментарий:</label>
                                <asp:TextBox runat="server" ID="txtComment" CssClass="area-text" TextMode="MultiLine" Rows="5" Width="760px"/>
                            </div>
                            <div class="row">
                                <label>Дополнительный файл:</label>
                                <telerik:RadAsyncUpload ID="RadUpload1" runat="server" OnFileUploaded="AsyncUpload1_FileUploaded"
                                            MaxFileSize="524288" OnClientFileUploaded="fileUploaded"
                                            AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                             Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
                            </div>
                            <asp:panel ID="pOfficial" Visible="false" runat="server">
                            <div class="row">
                                <label>Официальный ответ:</label>
                                <asp:CheckBox ID="cbOfficial" runat="server"/>
                            </div>
                            </asp:panel>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="valRelatedPublicationCommentGroup" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
								<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
							</div> 
                        </div>
                       </asp:Panel>
                    </FormTemplate>
            </EditFormSettings>
		</MasterTableView>
        <ClientSettings>
			<ClientEvents OnPopUpShowing="PopUpShowing" />
		</ClientSettings>

	</telerik:RadGrid>
</telerik:RadAjaxPanel>