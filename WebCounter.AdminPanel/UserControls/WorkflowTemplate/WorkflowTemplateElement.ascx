<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTemplateElement.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.WorkflowTemplateElement" %>

<telerik:RadGrid ID="rgWorkflowTemplateElement"
                    AutoGenerateColumns="false"
                    ShowStatusBar="true"
                    AllowSorting="true"
                    Skin="Windows7"
                    OnNeedDataSource="rgWorkflowTemplateElement_NeedDataSource"
                    OnItemCreated="rgWorkflowTemplateElement_OnItemCreated"
                    OnItemDataBound="rgWorkflowTemplateElement_ItemDataBound"
                    OnInsertCommand="rgWorkflowTemplateElement_InsertCommand"
                    OnUpdateCommand="rgWorkflowTemplateElement_UpdateCommand"
                    OnDeleteCommand="rgWorkflowTemplateElement_DeleteCommand"
                    OnRowDrop="rgWorkflowTemplateElement_RowDrop"
                    runat="server">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
        <Columns>
            <telerik:GridDragDropColumn HeaderStyle-Width="18px" />
            <telerik:GridBoundColumn HeaderText="Название" DataField="Name" UniqueName="Name" />
			<telerik:GridEditCommandColumn UniqueName="EditColumn" ButtonType="ImageButton" ItemStyle-Width="20px" />
			<telerik:GridButtonColumn UniqueName="DeleteColumn" ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
				ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
				ConfirmDialogWidth="420px" /> 
        </Columns>
        <EditFormSettings EditFormType="WebUserControl" UserControlName="~/UserControls/WorkflowTemplate/SaveWorkflowTemplateElement.ascx" InsertCaption="Элемент процесса" CaptionFormatString="Элемент процесса">
            <PopUpSettings Modal="true" Width="986px" />
        </EditFormSettings>
        <NoRecordsTemplate><div>Нет записей</div></NoRecordsTemplate>
    </MasterTableView>
	<ClientSettings AllowRowsDragDrop="true"><%--  EnablePostBackOnRowClick="true" --%>
		<ClientEvents OnPopUpShowing="PopUpShowingTop" />
        <Selecting AllowRowSelect="true" />
	</ClientSettings>
</telerik:RadGrid>