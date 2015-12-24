<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTemplateParameter.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.WorkflowTemplateParameter" %>

<telerik:RadGrid ID="rgWorkflowTemplateParameter"
                    AutoGenerateColumns="false"
                    ShowStatusBar="true"
                    AllowSorting="true"
                    Skin="Windows7"
                    OnNeedDataSource="rgWorkflowTemplateParameter_NeedDataSource"
                    OnItemCreated="rgWorkflowTemplateParameter_OnItemCreated"
                    OnItemDataBound="rgWorkflowTemplateParameter_ItemDataBound"
                    OnInsertCommand="rgWorkflowTemplateParameter_InsertCommand"
                    OnUpdateCommand="rgWorkflowTemplateParameter_UpdateCommand"
                    OnDeleteCommand="rgWorkflowTemplateParameter_DeleteCommand"
                    runat="server">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
        <Columns>
            <telerik:GridBoundColumn HeaderText="Параметр" DataField="Name" UniqueName="Name" />
			<telerik:GridEditCommandColumn UniqueName="EditColumn" ButtonType="ImageButton" ItemStyle-Width="20px" />
			<telerik:GridButtonColumn UniqueName="DeleteColumn" ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
				ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
				ConfirmDialogWidth="420px" /> 
        </Columns>
        <EditFormSettings EditFormType="Template" InsertCaption="Параметр процесса" CaptionFormatString="Параметр процесса">
            <PopUpSettings Modal="true" Width="437px" />
            <FormTemplate>
                <div class="edit-window">
                    <div class="row">
                        <label>Параметр:</label>
                        <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Вы не ввели 'Параметр'" ValidationGroup="groupUpdateParameter" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <label>Модуль:</label>
                        <asp:DropDownList ID="ddlModule" CssClass="select-text" runat="server" />
                    </div>
                    <div class="buttons clearfix">
	                    <asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateParameter" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
	                    <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
                    </div>
                </div>
            </FormTemplate>
        </EditFormSettings>
        <NoRecordsTemplate><div>Нет записей</div></NoRecordsTemplate>
    </MasterTableView>
	<ClientSettings>
		<ClientEvents OnPopUpShowing="PopUpShowingTop" />
        <Selecting AllowRowSelect="true" />
	</ClientSettings>
</telerik:RadGrid>