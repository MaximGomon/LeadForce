<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="WebCounter.AdminPanel.Settings" %>
<%@ Register TagPrefix="uc" TagName="PortalSettings" Src="~/UserControls/Portal/PortalSettings.ascx" %>
<%@ Register TagPrefix="uc" TagName="SiteDomains" Src="~/UserControls/SiteSettings/SiteDomains.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">
<script src="<%=ResolveUrl("~/Scripts/highlight.pack.js")%>" type="text/javascript"></script>
<script type="text/javascript">hljs.initHighlightingOnLoad();</script>    

    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
                        CssClass="validation-summary"
                        runat="server"
                        EnableClientScript="true"
                        HeaderText="Заполните все поля корректно:"
                        ValidationGroup="valSmtp" />

    <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
        <Tabs>
            <telerik:RadTab Text="Основные настройки" />
            <telerik:RadTab Text="Параметры работы с почтой" />
            <telerik:RadTab Text="Настройки портала" />
            <telerik:RadTab Text="Настройки доменов" />
            <telerik:RadTab Text="Интерфейс" />            
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <div class="row">
                <label>Название:</label>
                <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtName" CssClass="input-text" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="valSmtp" runat="server" />
            </div>
			<div class="row">
				<label>Страница обработки ссылок:</label>
				<asp:TextBox ID="txtLinkProcessingURL" CssClass="input-text" runat="server" />
			</div>
            <div class="row">
                <label>Код счетчика (часть 1):</label><br />
                <span class="note">(размещать в &lt;head&gt; или в начале документа)</span>
                <table class="tbl-counter-code">
                    <tr>
                        <td><pre><code><asp:Literal runat="server" ID="lrlCounterCode"></asp:Literal></code></pre></td>
                    </tr>
                </table>
            </div>
            <div class="row">
                <label>Код счетчика (часть 2):</label><br />
                <span class="note">(размещать в конце файла перед &lt;/body&gt;)</span>
                <table class="tbl-counter-code">
                    <tr>
                        <td><pre><code><asp:Literal runat="server" ID="lrlScriptCode"></asp:Literal></code></pre></td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView1" runat="server">
            <asp:FormView ID="fvSmtp" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <label>SMTP сервер:</label>
                        <asp:TextBox ID="txtSmtpHost" CssClass="input-text" Text='<%# Server.HtmlEncode(Eval("SmtpHost") == null ? "" : Eval("SmtpHost").ToString())%>' runat="server" />                        
                    </div>
                    <div class="row">
                        <label>Логин для SMTP сервера:</label>
                        <asp:TextBox ID="txtSmtpUsername" CssClass="input-text" Text='<%# Server.HtmlEncode(Eval("SmtpUsername") == null ? "" : Eval("SmtpUsername").ToString())%>' runat="server" />                        
                    </div>
                    <div class="row">
                        <label>Пароль для SMTP сервера:</label>
                        <asp:TextBox ID="txtSmtpPassword" TextMode="Password" OnPreRender="txtSmtpPassword_PreRender" CssClass="input-text" value='<%# Server.HtmlEncode(Eval("SmtpPassword") == null ? "" : Eval("SmtpPassword").ToString())%>' runat="server" autocomplete="off" />                        
                    </div>
                    <div class="row">
                        <label>SMTP порт:</label>
                        <asp:TextBox ID="txtSmtpPort" CssClass="input-text" Text='<%# Server.HtmlEncode(Eval("SmtpPort") == null ? "" : Eval("SmtpPort").ToString())%>' runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtSmtpPort" Text="*" ErrorMessage="Неверный формат порта SMTP." ValidationExpression="^[0-9]+$" ValidationGroup="valSmtp" runat="server" />
                    </div>
                    <div class="row">
                        <label>Системный Email:</label>
                        <asp:TextBox ID="txtSystemEmail" CssClass="input-text" Text='<%# Server.HtmlEncode(Eval("SystemEmail") == null ? "" : Eval("SystemEmail").ToString())%>' runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtSystemEmail" Text="*" ErrorMessage="Неверный формат Email." ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="valSmtp" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:FormView>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView3" runat="server">
            <uc:PortalSettings runat="server" ID="ucPortal" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView5" runat="server">
            <asp:Panel runat="server" CssClass="row" ID="plAccessBlock">
                <label>Запрещать доступ с доменов вне списка:</label>
                <asp:CheckBox runat="server" ID="chxIsBlockAccessFromDomainsOutsideOfList" />
            </asp:Panel>
            <uc:SiteDomains runat="server" ID="ucSiteDomains" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView4" runat="server">
            <div class="row">
                <label>Показывать скрытые сообщения:</label>
                <asp:CheckBox runat="server" ID="chxShowHiddenMessages" />
            </div>
            <div class="row">
                <label>Режим HTML редактора</label>
                <asp:DropDownList runat="server" ID="ddlHtmlEditorMode" CssClass="select-text"/>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br/>
    <asp:LinkButton ID="BtnUpdateSmtp" OnClick="BtnUpdateSmtp_Click" CssClass="btn" ValidationGroup="valSmtp" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
</asp:Content>