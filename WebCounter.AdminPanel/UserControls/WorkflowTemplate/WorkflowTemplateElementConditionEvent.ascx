<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTemplateElementConditionEvent.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.WorkflowTemplateElementConditionEvent" %>

<h3>Параметры ожидания</h3>
<div class="row">
    <label>Условие:</label>
    <asp:DropDownList ID="ddlCondition" AutoPostBack="true" OnSelectedIndexChanged="ddlCondition_OnSelectedIndexChanged" CssClass="select-text" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCondition" Display="Dynamic" ErrorMessage="Вы не выбрали 'Условие'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
</div>
<asp:Panel ID="pnlActivityCount" CssClass="row" Visible="false" runat="server">
    <label>Кол-во действий:</label>
    <telerik:RadNumericTextBox ID="txtActivityCount" Type="Number" CssClass="input-text" runat="server">
        <NumberFormat GroupSeparator="" AllowRounding="false" />
    </telerik:RadNumericTextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtActivityCount" Display="Dynamic" ErrorMessage="Вы не ввели 'Кол-во действий'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>                            
</asp:Panel>

<telerik:RadGrid ID="rgWorkflowTemplateConditionEvent"
                    AutoGenerateColumns="false"
                    ShowStatusBar="true"
                    AllowSorting="true"
                    Skin="Windows7"
                    OnNeedDataSource="rgWorkflowTemplateConditionEvent_NeedDataSource"
                    OnItemCreated="rgWorkflowTemplateConditionEvent_OnItemCreated"
                    OnItemDataBound="rgWorkflowTemplateConditionEvent_OnItemDataBound"
                    OnInsertCommand="rgWorkflowTemplateConditionEvent_OnInsertCommand"
                    OnUpdateCommand="rgWorkflowTemplateConditionEvent_OnUpdateCommand"
                    OnDeleteCommand="rgWorkflowTemplateConditionEvent_OnDeleteCommand"
                    runat="server">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
        <Columns>
            <telerik:GridTemplateColumn HeaderText="Категория" UniqueName="Category">
                <ItemTemplate>
                    <asp:Literal ID="litCategory" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Тип действия" UniqueName="ActivityType">
                <ItemTemplate>
                    <asp:Literal ID="litActivityType" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="URL или код" UniqueName="Code">
                <ItemTemplate>
                    <asp:Literal ID="litCode" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridNumericColumn HeaderText="Период актуальности, дней" DataField="ActualPeriod" UniqueName="ActualPeriod" />
            <telerik:GridTemplateColumn HeaderText="Реквизит" UniqueName="Requisite">
                <ItemTemplate>
                    <asp:Literal ID="litRequisite" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Формула" UniqueName="Formula">
                <ItemTemplate>
                    <asp:Literal ID="litFormula" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Значение" UniqueName="Value">
                <ItemTemplate>
                    <asp:Literal ID="litValue" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
			<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
			<telerik:GridButtonColumn UniqueName="DeleteColumn" ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
				ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
				ConfirmDialogWidth="420px" /> 
        </Columns>
        <EditFormSettings EditFormType="WebUserControl" UserControlName="~/UserControls/WorkflowTemplate/SaveWorkflowTemplateConditionEvent.ascx" InsertCaption="Условие события" CaptionFormatString="Условие события">
            <PopUpSettings Modal="true" Width="485px" />
        </EditFormSettings>
        <NoRecordsTemplate><div>Нет записей</div></NoRecordsTemplate>
    </MasterTableView>
	<ClientSettings>
		<ClientEvents OnPopUpShowing="PopUpShowingTop" />
        <Selecting AllowRowSelect="true" />
	</ClientSettings>
</telerik:RadGrid>