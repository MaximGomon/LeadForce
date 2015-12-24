<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Product.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ModuleEditionAction.Product.Product" %>
<%@ Register TagPrefix="labitec" Namespace="Labitec.UI.Photo.Controls" Assembly="Labitec.UI.Photo" %>
<%@ Register TagPrefix="uc" TagName="SelectCategoryControl" Src="~/UserControls/SelectCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProductComplectation" Src="~/UserControls/ProductComplectation.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProductPrice" Src="~/UserControls/ProductPrice.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>
<div class="grid-edit-form">
<asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" 
						CssClass="validation-summary"
						runat="server"
						EnableClientScript="true"
						HeaderText="Заполните все поля корректно:"
						ValidationGroup="valGroupUpdate" />
<table width="100%"><tr><td>
    <div class="row">
            <label>Название:</label>
            <asp:TextBox runat="server" ID="txtTitle" CssClass="input-text"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage='Укажите название'/>
    </div>
    </td><td align="right">
        <div class="row">
			<label>Артикул:</label>
            <asp:TextBox runat="server" ID="txtSKU" CssClass="input-text" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSKU" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage='Укажите артикул'/>
        </div>
    </td></tr>
    <tr><td width="100%" colspan="2">
    <div class="row">
		<label>Цена:</label>
		<asp:TextBox runat="server" ID="txtPrice" CssClass="input-text"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage='Укажите цену'/>
        <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="txtPrice" Operator="DataTypeCheck" Type="Double" CssClass="input-text" ValidationGroup="valGroupUpdate" ErrorMessage='Неверный формат цены' Text="*"  />
    </div>
    </td></tr><tr><td width="100%" colspan="2">
    <div class="row">
		<label>Описание:</label>
		<asp:TextBox runat="server" ID="txtDescription" CssClass="area-text" Width="86%" Height="45px" TextMode="MultiLine" />
	</div>
    </td></tr></table>
    <div class="update-file-btns">
        <telerik:RadButton OnClick="lbUpdateFile_OnClick" ID="BtnUpdate" Text="Сохранить" Skin="Windows7" runat="server" ValidationGroup="valGroupUpdate"  CssClass="left"/>
        <telerik:RadButton ID="lbCancel" Text="Отмена" Skin="Windows7" runat="server" CssClass="left"/>
    </div>
</div>