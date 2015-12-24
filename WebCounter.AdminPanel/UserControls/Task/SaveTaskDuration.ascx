<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveTaskDuration.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Task.SaveTaskDuration" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<div class="task-popup">
	<div class="row date-picker-autopostback clearfix">
		<label>Дата начала:</label>
		<div class="date-picker-container">
			<telerik:RadDateTimePicker ID="rdpStartDate" AutoPostBack="true" AutoPostBackControl="Both" OnSelectedDateChanged="rdpStartDate_OnSelectedDateChanged" runat="server" MinDate="01.01.1900" CssClass="datetime-picker" ShowPopupOnFocus="true" Width="190px">
				<DateInput Enabled="true" />
				<DatePopupButton Enabled="true" />
			</telerik:RadDateTimePicker>
			<asp:RequiredFieldValidator ID="rfvStartDate" Display="Dynamic" ControlToValidate="rdpStartDate" Text="*" ErrorMessage="Вы не ввели дату начала" runat="server" />
		</div>
	</div>
	<div class="row date-picker-autopostback clearfix">
		<label>Дата окончания:</label>
		<div class="date-picker-container">
			<telerik:RadDateTimePicker ID="rdpEndDate" AutoPostBack="true" AutoPostBackControl="Both" OnSelectedDateChanged="rdpEndDate_OnSelectedDateChanged" runat="server" MinDate="01.01.1900" CssClass="datetime-picker" ShowPopupOnFocus="true"  Width="190px" >
				<DateInput Enabled="true" />
				<DatePopupButton Enabled="true" />
			</telerik:RadDateTimePicker>
			<asp:RequiredFieldValidator ID="rfvEndDate" Display="Dynamic" ControlToValidate="rdpEndDate" Text="*" ErrorMessage="Вы не ввели дату окончания" runat="server" />
		</div>
	</div>
	<div class="row">
		<label>Длительность план, часов:</label>
		<telerik:RadNumericTextBox runat="server" ID="rntxtActualDurationHours" AutoPostBack="true" OnTextChanged="rntxtDurationHours_OnTextChanged" MinValue="0" Value="0" EmptyMessage="" CssClass="input-text" Type="Number">
			<NumberFormat GroupSeparator="" DecimalDigits="0" />                           
		</telerik:RadNumericTextBox>
		<asp:RequiredFieldValidator ID="rfvActualDurationHours" Display="Dynamic" ControlToValidate="rntxtActualDurationHours" Text="*" ErrorMessage="Вы не ввели длительность факт, часов" runat="server" />
	</div>
	<div class="row">
		<label>Длительность план, минут:</label>
		<telerik:RadNumericTextBox runat="server" ID="rntxtActualDurationMinutes" AutoPostBack="true" OnTextChanged="rntxtDurationHours_OnTextChanged" MinValue="0" Value="0" EmptyMessage="" CssClass="input-text" Type="Number">
			<NumberFormat GroupSeparator="" DecimalDigits="0" />                             
		</telerik:RadNumericTextBox>
		<asp:RequiredFieldValidator ID="rfvActualDurationMinutes" Display="Dynamic" ControlToValidate="rntxtActualDurationMinutes" Text="*" ErrorMessage="Вы не ввели длительность факт, минут" runat="server" />
	</div>
	<div class="row">
		<label>Ответственный:</label>
		<uc:ContactComboBox ID="ucResponsible" ValidationErrorMessage="Вы не выбрали контакт" CssClass="select-text" runat="server" FilterByFullName="true"/>
	</div>                            							
	<div class="row">
		<label>Комментарий:</label>
		<asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="area-text" />
	</div>
	<br/>
	<div class="buttons clearfix">
		<asp:LinkButton ID="lbtnSave" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>        
		<asp:LinkButton ID="lbtnSaveNonGrid" OnClick="lbtnSaveNonGrid_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />        
	</div>
</div>