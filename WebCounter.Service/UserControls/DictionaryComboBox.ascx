﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DictionaryComboBox.ascx.cs" Inherits="WebCounter.Service.UserControls.DictionaryComboBox" %>
<telerik:RadComboBox ID="rcbDictionary" Width="98%" CausesValidation="false" DataSourceID="edsDictionary" runat="server" AllowCustomText="false" Filter="Contains" ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None" />
<asp:RequiredFieldValidator ID="rfvDictionary" ControlToValidate="rcbDictionary" CssClass="required" Text="Поле обязательное для заполнения" Display="None" ErrorMessage="Вы не выбрали значение" runat="server" InitialValue="Выберите значение"/>
<asp:LinkButton runat="server" ID="lbtnEditDictionary" CssClass="edit-button" OnClick="lbtnEditDictionary_Click" Visible="false"><span>Редактировать</span></asp:LinkButton>
<asp:EntityDataSource ID="edsDictionary" runat="server" ConnectionString="name=WebCounterEntities" DefaultContainerName="WebCounterEntities" />