<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Design.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.FormWizard.Design" %>
<%@ Register TagPrefix="uc" TagName="CssEditor" Src="~/UserControls/CssEditor.ascx" %>

<div class="wizard-step">
    <h3>Настройка формы</h3>
    <div class="row row-color-picker clearfix">        
        <label>Фон формы:</label>
        <telerik:RadColorPicker ShowIcon="true" ID="rcpBackgroundColor" runat="server" PaletteModes="All" style="margin-left: 4px" />
    </div>
    <div class="row">
		<label>Ширина формы:</label>
		<telerik:RadNumericTextBox ID="txtFormWidth" Type="Number" CssClass="input-text" runat="server">
			<NumberFormat GroupSeparator="" AllowRounding="false" />
		</telerik:RadNumericTextBox>
	</div>
    <asp:Panel runat="server" ID="plInstruction">
        <h3>Стиль инструкций</h3>
        <uc:CssEditor ID="ucCssEditorInstruction" runat="server" />
    </asp:Panel>
    <asp:Panel runat="server" ID="plColumns">
        <h3>Стиль полей ввода данных</h3>
        <uc:CssEditor ID="ucCssEditorColumns" runat="server" />
    </asp:Panel>
    <h3>Стиль кнопки</h3>
    <uc:CssEditor ID="ucCssEditorButton" runat="server" />
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server" ValidationGroup="Design"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>