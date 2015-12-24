<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTemplateGoalElement.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.WorkflowTemplateGoalElement" %>


<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<telerik:RadGrid ID="rgWorkflowTemplateGoalElement" runat="server" OnItemDataBound="rgWorkflowTemplateGoalElement_OnItemDataBound" OnNeedDataSource="rgWorkflowTemplateGoalElement_OnNeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgWorkflowTemplateGoalElement_OnDeleteCommand" OnInsertCommand="rgWorkflowTemplateGoalElement_OnInsertCommand" OnUpdateCommand="rgWorkflowTemplateGoalElement_OnUpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>            			
			    <telerik:GridBoundColumn UniqueName="Name" DataField="Name" HeaderText="Название"/>
                <telerik:GridTemplateColumn UniqueName="ElementType" HeaderText="Тип элемента">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="lrlElementType" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Элемент" CaptionFormatString="Элемент">
				<PopUpSettings Modal="true" Width="420px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm" CssClass="radwindow-popup-inner">
							<div class="row">
								<label>Элемент:</label>
							    <telerik:RadComboBox runat="server" ID="rcbWorkflowTemplateElements" EmptyMessage="Выберите значение" AllowCustomText="false" Filter="Contains" EnableEmbeddedSkins="false" skin="Labitec" Width="230px" ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None" />
                                <asp:RequiredFieldValidator ID="rfvDictionary"  ValidationGroup="groupUpdateGoalElement" ControlToValidate="rcbWorkflowTemplateElements" CssClass="required" Text="*" ErrorMessage="Вы не выбрали значение" runat="server" InitialValue="Выберите значение"/>
							</div>							
							<br/>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateGoalElement" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
								<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
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