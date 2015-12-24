<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FIOTest.aspx.cs" Inherits="WebCounter.AdminPanel.FIOTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

<div class="row">
    <label>Введите ФИО:</label>
    <asp:TextBox runat="server" ID="txtFIO" CssClass="input-text" Width="500px" />
</div>
<div class="row">
    <label>Формат:</label>
    <asp:RadioButtonList runat="server" ID="rblFormat" ClientIDMode="Static" CssClass="list" RepeatLayout="OrderedList" />
</div>
<div class="row">
    <label>Корректировка:</label>
    <asp:RadioButtonList runat="server" ID="rblCorrection" ClientIDMode="Static" CssClass="list row-wide-label" RepeatLayout="Table" />
</div>
<br />
<asp:Panel runat="server" ID="plError" Visible="false">
Поле заполнено некорректно
<br /><br />
</asp:Panel>
<asp:Panel runat="server" ID="plSuccess" Visible="false">
<div class="row">
    <label>Результат:</label>
    <asp:Literal runat="server" ID="lrlResult" />
</div>
<div class="row">
    <label>Пол:</label>
    <asp:Literal runat="server" ID="lrlGender" />
</div>
<div class="row">
    <label>Фамилия:</label>
    <asp:Literal runat="server" ID="lrlSurname" />
</div>
<div class="row">
    <label>Имя:</label>
    <asp:Literal runat="server" ID="lrlName" />
</div>
<div class="row">
    <label>Отчество:</label>
    <asp:Literal runat="server" ID="lrlPatronymic" />
</div>
<div class="row">
    <label>IsNameCorrect:</label>
    <asp:Literal runat="server" ID="lrlIsNameCorrect" />
</div>
</asp:Panel>

<div class="buttons">
    <asp:LinkButton ID="lbtnSave" OnClick="lbtnCheck_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Проверить</span></asp:LinkButton>
</div>
<div class="clear"></div>
<br/><br/>

<div class="row">
    <label>IP:</label>
    <asp:Literal runat="server" ID="lrlIP" />
</div>
<div class="row">
    <label>Страна:</label>
    <asp:Literal runat="server" ID="lrlCountry" />
</div>
<div class="row">
    <label>Регион:</label>
    <asp:Literal runat="server" ID="lrlRegion" />
</div>
<div class="row">
    <label>Область:</label>
    <asp:Literal runat="server" ID="lrlDistrict" />
</div>
<div class="row">
    <label>Город:</label>
    <asp:Literal runat="server" ID="lrlCity" />
</div>
<div class="row">
    <label>Широта:</label>
    <asp:Literal runat="server" ID="lrlLatitude" />
</div>
<div class="row">
    <label>Долгота:</label>
    <asp:Literal runat="server" ID="lrlLongitude" />
</div>

</asp:Content>
