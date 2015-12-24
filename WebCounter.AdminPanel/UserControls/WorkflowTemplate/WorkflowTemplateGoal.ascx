<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTemplateGoal.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.WorkflowTemplateGoal" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateGoalElement" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateGoalElement.ascx" %>

<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<telerik:RadGrid ID="rgWorkflowTemplateGoal" runat="server" OnItemDataBound="rgWorkflowTemplateGoal_OnItemDataBound" OnNeedDataSource="rgWorkflowTemplateGoal_OnNeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgWorkflowTemplateGoal_OnDeleteCommand" OnInsertCommand="rgWorkflowTemplateGoal_OnInsertCommand" OnUpdateCommand="rgWorkflowTemplateGoal_OnUpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>            			
			    <telerik:GridBoundColumn UniqueName="Title" DataField="Title" HeaderText="Название"/>
                <telerik:GridBoundColumn UniqueName="Description" DataField="Description" HeaderText="Описание"/>                
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Цель процесса" CaptionFormatString="Цель процесса">
				<PopUpSettings Modal="true" Width="820px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm" CssClass="radwindow-popup-inner">
							<div class="row">
								<label>Название:</label>
								<asp:TextBox runat="server" ID="txtTitle" CssClass="input-text" Width="230px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitle" CssClass="required" Text="*" ErrorMessage="Вы не название" ValidationGroup="groupUpdateGoal" runat="server" />
							</div>
							<div class="row">
								<label>Описание:</label>
								<asp:TextBox runat="server" ID="txtDescription" CssClass="area-text" TextMode="MultiLine" Width="615px" />
							</div>        
					        <uc:WorkflowTemplateGoalElement runat="server" ID="ucWorkflowTemplateGoalElement" />
							<br/>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateGoal" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
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
