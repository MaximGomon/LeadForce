<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskDurations.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.TaskModule.UserControls.TaskDurations" %>

<h3>История исполнения</h3>

<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<telerik:RadGrid ID="rgTaskDurations" runat="server" OnItemDataBound="rgTaskDurations_OnItemDataBound" OnNeedDataSource="rgTaskDurations_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet">
		<MasterTableView DataKeyNames="ID">
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
			</Columns>	
		</MasterTableView>
		<ClientSettings>			
			<Selecting AllowRowSelect="true" />
		</ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>
