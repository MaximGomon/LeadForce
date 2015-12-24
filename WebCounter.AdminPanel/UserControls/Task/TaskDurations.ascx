<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskDurations.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Task.TaskDurations" %>

<h3>История исполнения</h3>

<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<telerik:RadGrid ID="rgTaskDurations" runat="server" OnItemDataBound="rgTaskDurations_OnItemDataBound" OnNeedDataSource="rgTaskDurations_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgTaskDurations_DeleteCommand" OnInsertCommand="rgTaskDurations_InsertCommand" OnUpdateCommand="rgTaskDurations_UpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>  
                <telerik:GridDateTimeColumn UniqueName="SectionDateStart" DataField="SectionDateStart" HeaderText="Дата начала отрезка"/>
                <telerik:GridDateTimeColumn UniqueName="SectionDateEnd" DataField="SectionDateEnd" HeaderText="Дата окончания отрезка"/>
                <telerik:GridNumericColumn UniqueName="ActualDurationHours" DataField="ActualDurationHours" HeaderText="Длительность факт, часов" />
                <telerik:GridNumericColumn UniqueName="ActualDurationMinutes" DataField="ActualDurationMinutes" HeaderText="Длительность факт, минут" />				
				<telerik:GridTemplateColumn UniqueName="Responsible" HeaderText="Ответственный">			
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlResponsible" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>				
				<telerik:GridBoundColumn UniqueName="Comment" HeaderText="Комментарий" DataField="Comment"/>				
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="420px" /> 
			</Columns>
			<EditFormSettings EditFormType="WebUserControl" UserControlName="~/UserControls/Task/SaveTaskDuration.ascx" InsertCaption="История исполнения" CaptionFormatString="История исполнения">
				<PopUpSettings Modal="true" Width="420px" />					
			</EditFormSettings>
		</MasterTableView>
		<ClientSettings>
			<ClientEvents OnPopUpShowing="PopUpShowing" />
			<Selecting AllowRowSelect="true" />
		</ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>
