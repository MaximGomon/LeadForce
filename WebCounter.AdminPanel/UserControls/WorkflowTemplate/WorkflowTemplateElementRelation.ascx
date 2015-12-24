<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTemplateElementRelation.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.WorkflowTemplateElementRelation" %>

<telerik:RadGrid ID="rgWorkflowTemplateElementRelation"
                    AutoGenerateColumns="false"
                    ShowStatusBar="true"
                    AllowSorting="true"
                    Skin="Windows7"
                    OnNeedDataSource="rgWorkflowTemplateElementRelation_NeedDataSource"
                    OnItemCreated="rgWorkflowTemplateElementRelation_OnItemCreated"
                    OnItemDataBound="rgWorkflowTemplateElementRelation_ItemDataBound"
                    OnInsertCommand="rgWorkflowTemplateElementRelation_InsertCommand"
                    OnUpdateCommand="rgWorkflowTemplateElementRelation_UpdateCommand"
                    OnDeleteCommand="rgWorkflowTemplateElementRelation_DeleteCommand"
                    runat="server">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
        <Columns>
            <telerik:GridTemplateColumn HeaderText="Начальный элемент"  UniqueName="StartElementName">
                <ItemTemplate>
                    <asp:Literal ID="litStartElementName" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Результат начального элемента" DataField="StartElementResultName" UniqueName="StartElementResultName">
                <ItemTemplate>
                    <asp:Literal ID="litStartElementResultName" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Конечный элемент" DataField="EndElementName" UniqueName="EndElementName">
                <ItemTemplate>
                    <asp:Literal ID="litEndElementName" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
			<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
			<telerik:GridButtonColumn UniqueName="DeleteColumn" ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
				ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
				ConfirmDialogWidth="420px" /> 
        </Columns>
        <EditFormSettings EditFormType="Template" InsertCaption="Связь элементов процесса" CaptionFormatString="Связь элементов процесса">
            <PopUpSettings Modal="true" Width="437px" />
            <FormTemplate>
                <div class="edit-window">
                    <div class="row">
                        <label>Начальный элемент:</label>
                        <asp:DropDownList ID="ddlStartElement" AutoPostBack="true" OnSelectedIndexChanged="ddlStartElement_OnSelectedIndexChanged" CssClass="select-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlStartElement" Display="Dynamic" ErrorMessage="Вы не ввели 'Начальный элемент'" ValidationGroup="groupUpdateElementRelation" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <asp:Panel ID="pnlResultStartElement" CssClass="row" Visible="false" runat="server">
                        <label>Результат начального элемента:</label>
                        <asp:DropDownList ID="ddlResultStartElement" CssClass="select-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlResultStartElement" Display="Dynamic" ErrorMessage="Вы не ввели 'Результат начального элемента'" ValidationGroup="groupUpdateElementRelation" runat="server">*</asp:RequiredFieldValidator>
                    </asp:Panel>
                    <div class="row">
                        <label>Конечный элемент:</label>
                        <asp:DropDownList ID="ddlEndElement" CssClass="select-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlEndElement" Display="Dynamic" ErrorMessage="Вы не ввели 'Конечный элемент'" ValidationGroup="groupUpdateElementRelation" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="buttons clearfix">
	                    <asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateElementRelation" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
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