<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NameCheckerTooltip.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.NameCheckerTooltip" %>

<div class="tooltip-namechecker clearfix">
	<div class="row">
		<label>Фамилия:</label>
		<asp:TextBox runat="server" ID="txtSurname" CssClass="input-text" />		
	</div>
	<div class="row">
		<label>Имя:</label>
		<asp:TextBox runat="server" ID="txtName" CssClass="input-text" />		
	</div>
	<div class="row">
		<label>Отчество:</label>		
		<asp:TextBox runat="server" ID="txtPatronymic" CssClass="input-text" />
	</div>
    <div class="row">
		<label>Пол:</label>		
		<asp:DropDownList runat="server" CssClass="select-text" ID="ddlGender">
            <asp:ListItem Value="">Не выбран</asp:ListItem>
            <asp:ListItem Value="0">Мужской</asp:ListItem>
            <asp:ListItem Value="1">Женский</asp:ListItem>
        </asp:DropDownList>
	</div>
	<asp:Panel runat="server" CssClass="buttons clearfix" ID="plButtons">	
		<asp:LinkButton ID="lbtnConfirm" CssClass="btn" runat="server" OnClick="lbtnConfirm_OnClick"><em>&nbsp;</em><span>Подтвердить</span></asp:LinkButton>
		<asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" NavigateUrl="javascript:HideTooltip();" />	
	</asp:Panel>
</div>