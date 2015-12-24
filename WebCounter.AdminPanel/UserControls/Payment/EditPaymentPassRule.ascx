<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPaymentPassRule.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Payment.EditPaymentPassRule" %>
<%@ Register TagPrefix="uc" TagName="PaymentPass" Src="~/UserControls/Payment/PaymentPass.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentPassRuleCompany" Src="~/UserControls/Payment/PaymentPassRuleCompany.ascx" %>

<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />


<div class="radwindow-popup-inner">
<asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" 
						CssClass="validation-summary"
						runat="server"
						EnableClientScript="true"
						HeaderText="Заполните все поля корректно:"
						ValidationGroup="groupEditPaymentPassRule" />
    <div class="row">
		<label>Название:</label>
        <telerik:RadTextBox ID="txtTitle" Skin="Windows7" Wrap="false" Width="300px" runat="server"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="required" Display="Dynamic" ControlToValidate="txtTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupEditPaymentPassRule" runat="server" />
	</div>
    <div class="row">
		<label>Тип:</label>
	    <asp:DropDownList runat="server" ID="ddlPaymentType" CssClass="select-text"/>
	</div>
    <div class="row">
		<label>Активное:</label>
        <asp:CheckBox runat="server" ID="chxIsActive" />
	</div>
    <div class="row">
		<label>Проводить автоматически:</label>
        <asp:CheckBox runat="server" ID="chxIsAutomatic" />
	</div>
    <br/>
    <h3>Контрагенты</h3>
    <uc:PaymentPassRuleCompany ID="ucPaymentPassRuleCompany" ShowAmount="false" runat="server"/>
    <br/>
    <h3>Проводки</h3>
    <uc:PaymentPass ID="ucPaymentPass" ShowAmount="false" runat="server"/>
    <br/>
    <div class="buttons clearfix">  
    	<asp:LinkButton ID="btnUpdate" ValidationGroup="groupEditPaymentPassRule" OnClick="btnUpdate_OnClick"  CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
        <a href="javascript:;" class="cancel" onclick="CloseEditPaymentPassRuleRadWindow(); return false;">Отмена</a>
    </div>
</div>