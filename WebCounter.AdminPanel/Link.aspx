<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Link.aspx.cs" Inherits="WebCounter.AdminPanel.Link" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table class="smb-files" width="100%">
        <tr>
            <td width="195px" valign="top">
                <div class="aside">
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td valign="top" width="100%">
                <uc:NotificationMessage runat="server" ID="ucMessage" MessageType="Warning" Style="margin-top: 0" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
                    CssClass="validation-summary"
                    runat="server"
                    EnableClientScript="true"
                    HeaderText="Заполните все поля корректно:"
                    ValidationGroup="valGroupUpdate" />
                <div class="row">
					<label>Название правила:</label>
					<asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" ErrorMessage="Вы не ввели название правила" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
				</div>
                <div class="row">
					<label>Код:</label>
					<asp:TextBox ID="txtCode" CssClass="input-text" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCode" ErrorMessage="Вы не ввели код" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Неверный формат кода" ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_-]+$" ValidationGroup="valGroupUpdate" runat="server" />
				</div>
                <div class="row">
						<label>Ссылка:</label>
						<asp:TextBox ID="txtURL" CssClass="input-text" runat="server" />
				</div>
                <br />
	            <div class="buttons">
		            <asp:LinkButton ID="btnUpdate" CssClass="btn" OnClick="btnUpdate_OnClick" ValidationGroup="valGroupUpdate" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>                    
		            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	            </div>
            </td>
        </tr>
    </table>
</asp:Content>
