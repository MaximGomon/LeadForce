<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CssEditor.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.CssEditor" %>

<telerik:RadCodeBlock runat="server">
    <link href='<%= ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</telerik:RadCodeBlock>

<div class="css-editor clearfix">
    <div class="left-column">
        <div class="row">
            <label>Шрифт:</label>
            <telerik:RadComboBox ID="rcbFontFamily" Width="234px" CssClass="select-text" Skin="Labitec" EnableEmbeddedSkins="False" runat="server" />
        </div>
        <div class="row row-color-picker clearfix">
            <label>Цвет текста:</label>
            <telerik:RadColorPicker ShowIcon="true" ID="rcpColor" runat="server" PaletteModes="All" />
            <label style="width: 80px; margin-left: 61px;">Фон:</label>
            <telerik:RadColorPicker ShowIcon="true" ID="rcpBackgroundColor" runat="server" PaletteModes="All" />
        </div>
    </div>
    <div class="right-column">
        <div class="row">
            <label>Размер:</label>
            <telerik:RadComboBox ID="rcbFontSize" Width="120px" CssClass="select-text" Skin="Labitec" EnableEmbeddedSkins="False" runat="server" />
        </div>
        <div class="row">
            <label>Жирный:</label>
            <asp:CheckBox ID="cbBold" runat="server" />
        </div>
    </div>
</div>