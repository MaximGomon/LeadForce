<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditAccessModuleRecord.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.EditAccessModuleRecord" %>
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<div class="edit-record">     
    <div class="row">
        <label>Модуль</label><asp:DropDownList runat="server" ID="ddlModules" CssClass="select-text" />
    </div>
    <h3>Область доступа</h3>    
    <div class="row">
        <label>Правило по компании</label><asp:DropDownList ID="ddlCompanyRules" runat="server" CssClass="select-text" />
    </div>
    <div class="row">    
        <label>Компания</label><telerik:RadComboBox runat="server" ID="rcbCompany" Filter="Contains" ZIndex="50001" Width="234px" EnableEmbeddedSkins="false" Skin="Labitec" />
    </div>           
    <div class="row">        
        <label>Правило по ответственному</label><asp:DropDownList ID="ddlOwnerRules" runat="server" CssClass="select-text" />
    </div>
    <div class="row">
        <label>Ответственный</label><telerik:RadComboBox runat="server" ID="rcbOwner" EnableItemCaching="false" Filter="Contains" Text="" ZIndex="50001" Width="234px" EnableEmbeddedSkins="false" Skin="Labitec" />
    </div>    
    <h3>Права доступа</h3>
    <div class="row">
        <asp:CheckBox ID="chxRead" runat="server" Text="&nbsp;Чтение" CssClass="access"/>
        <asp:CheckBox ID="chxWrite" runat="server" Text="&nbsp;Изменение" CssClass="access"/>
        <asp:CheckBox ID="chxDelete" runat="server" Text="&nbsp;Удаление" CssClass="access"/>
    </div>
    <div class="buttons">    
        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn" OnClick="lnkSave_Click"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" OnClientClick="CloseTooltip(); return false;">Отмена</asp:LinkButton>
    </div>
</div>
