<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationTerms.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.PublicationTerms" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
	<telerik:RadGrid ID="rgPublicationTerms" Skin="Windows7" Width="100%" runat="server" OnNeedDataSource="rgPublicationTerms_NeedDataSource" OnDeleteCommand="rgPublicationTerms_DeleteCommand"
		OnInsertCommand="rgPublicationTerms_InsertCommand" OnUpdateCommand="rgPublicationTerms_UpdateCommand" OnItemDataBound="rgPublicationTerms_DataBound">
		<MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" DataKeyNames="ID" InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="PopUp">
			<EditFormSettings>
				<EditColumn ButtonType="ImageButton"/>
			</EditFormSettings>
			<Columns>                                    
				<telerik:GridTemplateColumn HeaderText="Термин">
					<ItemTemplate>
                        <%# Eval("Term")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn HeaderText="Код публикации">
					<ItemTemplate>
                        <%# Eval("PublicationCode")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn HeaderText="Код элемента">
					<ItemTemplate>
                        <%# Eval("ElementCode")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn HeaderText="Описание">
					<ItemTemplate>
                        <%# Eval("Description")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
            <EditFormSettings EditFormType="Template" InsertCaption="Термин в публикации" CaptionFormatString="Термин в публикации">
				<PopUpSettings Modal="true" Width="820px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">
                       	<div class="two-columns order-product">                        						
                            <asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" 
						    CssClass="validation-summary"
						    runat="server"
						    EnableClientScript="true"
						    HeaderText="Заполните все поля корректно:"
						    ValidationGroup="valPublicationTermGroup" />

                            <div class="row">
								<label>Термин:</label>
								<asp:TextBox runat="server" ID="txtTerm" CssClass="input-text" Width="630px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTerm" CssClass="input-text" ValidationGroup="valPublicationTermGroup" Text="*" ErrorMessage='Укажите термин'/>
							</div>
                            <div class="row">
								<label>Код публикации:</label>
								<asp:TextBox runat="server" ID="txtPublicationCode" CssClass="input-text" Width="630px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPublicationCode" CssClass="input-text" ValidationGroup="valPublicationTermGroup" Text="*" ErrorMessage='Укажите код публикации'/>
							</div>
                            <div class="row">
								<label>Код элемента:</label>
								<asp:TextBox runat="server" ID="txtElementCode" CssClass="input-text" Width="630px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtElementCode" CssClass="input-text" ValidationGroup="valPublicationTermGroup" Text="*" ErrorMessage='Укажите код элемента'/>
							</div>
                            <div class="row">
								<label>Описание:</label>
								<asp:TextBox runat="server" ID="txtDescription" CssClass="area-text" Width="630px" TextMode="MultiLine"/>
							</div>
                            <div class="clear"></div>						
							<br/>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="valPublicationTermGroup" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
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