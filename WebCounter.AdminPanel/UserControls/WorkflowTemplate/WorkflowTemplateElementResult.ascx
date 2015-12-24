<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTemplateElementResult.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.WorkflowTemplateElementResult" %>

<telerik:RadGrid ID="rgWorkflowTemplateElementResult"
                    AutoGenerateColumns="false"
                    ShowStatusBar="true"
                    AllowSorting="true"
                    Skin="Windows7"
                    OnNeedDataSource="rgWorkflowTemplateElementResult_NeedDataSource"
                    OnItemCreated="rgWorkflowTemplateElementResult_OnItemCreated"
                    OnItemDataBound="rgWorkflowTemplateElementResult_ItemDataBound"
                    OnInsertCommand="rgWorkflowTemplateElementResult_InsertCommand"
                    OnUpdateCommand="rgWorkflowTemplateElementResult_UpdateCommand"
                    OnDeleteCommand="rgWorkflowTemplateElementResult_DeleteCommand"
                    runat="server">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="EditForms">
        <Columns>
            <telerik:GridBoundColumn HeaderText="Результат" DataField="Name" UniqueName="Name" />
			<telerik:GridEditCommandColumn UniqueName="EditColumn" ButtonType="ImageButton" ItemStyle-Width="20px" />
			<telerik:GridButtonColumn UniqueName="DeleteColumn" ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
				ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
				ConfirmDialogWidth="420px" /> 
        </Columns>
        <%--<EditItemTemplate>
                <div class="row">
                    <label>Результат:</label>
                    <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Вы не ввели 'Результат'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                </div>
        </EditItemTemplate>--%>
        <EditFormSettings EditFormType="Template" InsertCaption="Результат элемента процесса" CaptionFormatString="Результат элемента процесса">
            <FormTemplate>
                <div style="padding: 5px">
                    <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Вы не ввели 'Результат'" ValidationGroup="groupUpdateElementResult" runat="server">*</asp:RequiredFieldValidator>
	                <asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateElementResult" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' runat="server">Сохранить</asp:LinkButton>
	                <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
                </div>
            </FormTemplate>
        </EditFormSettings>
    </MasterTableView>
</telerik:RadGrid>