<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteActionTemplate_old.aspx.cs" Inherits="WebCounter.AdminPanel.SiteActionTemplate_old" %>
<%@ Register TagPrefix="uc" TagName="SiteActionLinkUsers" Src="~/UserControls/SiteActionLinkUsers.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>

<%@ Register TagPrefix="uc" TagName="SelectSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SelectSiteActionTemplate.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
    <Tabs>
        <telerik:RadTab Text="Основные данные" />
        <telerik:RadTab Text="Шаблон сообщения" />
        <telerik:RadTab Text="Переходы по ссылкам" Value="tab-action-links" />
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
    <telerik:RadPageView ID="RadPageView1" runat="server">
        <h3>Основные данные</h3>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:FormView ID="fvSiteActionTemplate" runat="server">
                    <ItemTemplate>
                        <div class="left-column">
                            <div class="row">
                                <label>Категория шаблона:</label>
                                <asp:DropDownList runat="server" ID="ddlSiteTemplateCategory" Enabled="false" CssClass="select-text" />
                            </div>
                            <asp:Panel runat="server" ID="plNotBase" Visible="false">
                                <div class="row">
                                    <label>Базовый шаблон:</label>
                                    <uc:DictionaryOnDemandComboBox ID="dcbParentTemplate" Enabled="false" DictionaryName="tbl_SiteActionTemplate" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Где используется:</label>
                                    <asp:HyperLink runat="server"  ID="hlUsage" />
                                </div>
                            </asp:Panel>
                            <div class="row">
                                <label>Название шаблона сообщения:</label>
                                <asp:TextBox ID="txtTitle" Text='<%# Eval("Title") %>' CssClass="input-text" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitle" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="row">
                                <label>Тип сообщения:</label>
                                <asp:DropDownList ID="ddlActionType" ClientIDMode="AutoID" OnSelectedIndexChanged="ddlActionType_SelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlActionType" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="row">
                                <label>Подменять ссылки:</label>
                                <asp:DropDownList ID="ddlReplaceLinks" ClientIDMode="AutoID" CssClass="select-text" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="right-column">
                            <asp:Panel ID="pToEmail" Visible="false" runat="server">
                                <div class="row">
                                    <label>Email получателя:</label>
                                    <asp:TextBox ID="txtToEmail" Text='<%# Eval("ToEmail") %>' CssClass="input-text" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtToEmail" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Неверный формат Email." ControlToValidate="txtToEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="valGroup" runat="server" />
                                </div>
                            </asp:Panel>
                            <div class="row">
                                <label>Email отправителя:</label>
                                <asp:TextBox ID="txtFromEmail" Text='<%# Eval("FromEmail") %>' CssClass="input-text" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtFromEmail" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ErrorMessage="Неверный формат Email." ControlToValidate="txtFromEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="valGroup" runat="server" />
                            </div>
                            <div class="row">
                                <label>Имя отправителя:</label>
                                <asp:TextBox ID="txtFromName" Text='<%# Eval("FromName") %>' CssClass="input-text" runat="server" />
                            </div>
                            <div class="row">
                                <label>Email для ответа:</label>
                                <asp:TextBox ID="txtReplyEmail" Text='<%# Eval("ReplyToEmail") %>' CssClass="input-text" runat="server" />                            
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ErrorMessage="Неверный формат Email." ControlToValidate="txtReplyEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="valGroup" runat="server" Display="Dynamic" />
                            </div>
                            <div class="row">
                                <label>Имя для ответа:</label>
                                <asp:TextBox ID="txtReplyName" Text='<%# Eval("ReplyToName") %>' CssClass="input-text" runat="server" />
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="row">
                            <label>Тема сообщения:</label>
                            <asp:TextBox ID="txtMessageCaption" Text='<%# Eval("MessageCaption") %>' CssClass="input-text" runat="server" Width="760px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtMessageCaption" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                        </div>
                    </ItemTemplate>
                </asp:FormView>

                <asp:Panel ID="pStats" runat="server">
                    <h3>Статистика</h3>
                    <div class="row">
                        <label>Выслано:</label> <asp:Literal ID="litSending" Text="0" runat="server" />
                    </div>
                    <div class="row">
                        <label>Результат:</label> <asp:Literal ID="litResponse" Text="0" runat="server" />
                    </div>
                    <div class="row">
                        <label>Конверсия:</label> <asp:Literal ID="litConversion" Text="0%" runat="server" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView2" runat="server">
        <div class="clearfix">
            <div class="html-editor">
                <%--<uc:HtmlEditor runat="server" ID="ucHtmlEditor" Width="680px" Content='<%# Eval("MessageBody") %>' Height="400px" Module="SiteActionTemplates" />--%>                
            </div>
            <div class="html-legend">
                Редактор поддерживает специальные теги:<br />
                - Вывод реквизита посетителя #User.Код поля 1#
                <asp:Repeater ID="rUserColumnValues" runat="server">
                    <HeaderTemplate><span>(#User.UserFullName# #User.LastName# #User.FirstName# #User.MiddleName# #User.Email# #User.Phone# #User.Score# #Text# #Unsubscribe# #Advert#</HeaderTemplate>
                    <ItemTemplate>
                        #User.<%# Eval("Code") %>#
                    </ItemTemplate>
                    <FooterTemplate>)</span></FooterTemplate>
                </asp:Repeater>
                <br />
                - Вывод ссылки #Link.Код ссылки 1#
            </div>
        </div>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView3" runat="server">
        <asp:ListView ID="lvSiteActionLink" OnItemDataBound="lvSiteActionLink_ItemDataBound" runat="server">
            <LayoutTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tbl-action-link">
                    <tr id="itemPlaceholder" runat="server" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td valign="top">
                        <label>Ссылка:</label> <asp:HyperLink ID="hlActionLink" Target="_blank" runat="server" />
                    </td>
                    <td valign="top">
                        <label style="margin-left: 20px;">Количество переходов:</label> <asp:Literal ID="litCountConversions" Text="0" runat="server" />
                    </td>
                    <td valign="top">
                        <uc:SiteActionLinkUsers ID="ucSiteActionLinkUsers" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <br />
                Нет данных.
                <br />
            </EmptyDataTemplate>
        </asp:ListView>
    </telerik:RadPageView>
</telerik:RadMultiPage>

<br />

    <uc:SelectSiteActionTemplate ID="ucSelectSiteActionTemplate" runat="server" />

    <br />

<div class="buttons">
    <asp:LinkButton ID="BtnUpdate" OnClientClick="UpdateContent()" OnClick="BtnUpdate_Click" CssClass="btn" ValidationGroup="valGroup" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
    <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
</div>
</asp:Content>