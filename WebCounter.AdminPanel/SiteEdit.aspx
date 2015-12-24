<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteEdit.aspx.cs" Inherits="WebCounter.AdminPanel.SiteEdit" ValidateRequest="false" %>
<%@ Register TagPrefix="uc" TagName="PortalSettings" Src="~/UserControls/Portal/PortalSettings.ascx" %>
<%@ Register TagPrefix="uc" TagName="SiteDomains" Src="~/UserControls/SiteSettings/SiteDomains.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<script src="<%=ResolveUrl("~/Scripts/highlight.pack.js")%>" type="text/javascript"></script>
<script type="text/javascript">hljs.initHighlightingOnLoad();</script>    
<table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsSite" ObjectTypeName="tbl_Sites" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                </div>
            </td>
            <td>
                <div>
	<asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						CssClass="validation-summary"
						runat="server"
						EnableClientScript="true"
						HeaderText="Заполните все поля корректно:"
						ValidationGroup="valSiteUpdate" />
	<telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		<Tabs>
			<telerik:RadTab Text="Общие настройки" />
			<telerik:RadTab Text="Параметры работы с почтой" />
            <telerik:RadTab Text="Настройки портала" />
            <telerik:RadTab Text="Настройки доменов" />
			<telerik:RadTab Text="Управление контентом" />
			<telerik:RadTab Text="Статистика" />
		</Tabs>
	</telerik:RadTabStrip>
	<telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		<telerik:RadPageView ID="RadPageView1" runat="server">
            <div class="site-edit">
                <h3>Общие настройки</h3>
			    <div class="row">
				    <label>Название:</label>
				    <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
				    <asp:RequiredFieldValidator ControlToValidate="txtName" CssClass="input-text" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="valSiteUpdate" runat="server" />
			    </div>
			    <div class="row">
				    <label>Страница обработки ссылок:</label>
				    <asp:TextBox ID="txtLinkProcessingURL" CssClass="input-text" runat="server" />
			    </div>
                <asp:Panel runat="server" CssClass="row" ID="plCounterCode" Visible="false">			
                    <label>Код счетчика (часть 1):</label><br />
                    <span class="note">(размещать в &lt;head&gt; или в начале документа)</span>
                    <table class="tbl-counter-code">
                        <tr>
                            <td><pre><code><asp:Literal runat="server" ID="lrlCounterCode"></asp:Literal></code></pre></td>
                        </tr>
                    </table>
                    <br />
                    <label>Код счетчика (часть 2):</label><br />
                    <span class="note">(размещать в конце файла перед &lt;/body&gt;)</span>
                    <table class="tbl-counter-code">
                        <tr>
                            <td><pre><code><asp:Literal runat="server" ID="lrlScriptCode"></asp:Literal></code></pre></td>
                        </tr>
                    </table>		    
                </asp:Panel>
			    <%--<div class="row">
				    <label>Логин:</label>
				    <asp:TextBox ID="txtUserName" CssClass="input-text" runat="server" />
				    <asp:RequiredFieldValidator ControlToValidate="txtUserName" CssClass="input-text" Text="*" ErrorMessage="Вы не ввели логин" ValidationGroup="valSiteUpdate" runat="server" />
			    </div>
			    <div class="row">
				    <label>Пароль:</label>
				    <asp:TextBox ID="txtPassword" CssClass="input-text" runat="server" TextMode="Password" autocomplete="off" />
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPassword" CssClass="input-text" Text="*" ErrorMessage="Вы не ввели пароль" ValidationGroup="valSiteUpdate" runat="server" />
			    </div>--%>
                <div class="left-column">
			        <div class="row">
				        <label>Активен:</label>
				        <asp:CheckBox runat="server" ID="chxIsActive" Checked="True" />
			        </div>
                    <div class="row">
				        <label>Шаблон:</label>
				        <asp:CheckBox runat="server" ID="chxIsTemplate" />
			        </div>
                    <div class="row">
                        <label>Профиль:</label>
                        <telerik:RadComboBox ID="rcbAccessProfile" Filter="Contains" Width="234px" AutoPostBack="true" OnSelectedIndexChanged="rcbAccessProfile_OnSelectedIndexChanged" EnableEmbeddedSkins="false" Skin="Labitec" runat="server" />
                    </div>
			        <%--<div class="row">
				        <label>Системный администратор:</label>
				        <asp:CheckBox runat="server" ID="chxIsSystemAdministrator" />
			        </div>--%>
			        <div class="row">
				        <label>Таймаут сессии для счетчика, минут:</label>
				        <telerik:RadNumericTextBox runat="server" ID="txtSessionTimeout" EmptyMessage="" MinValue="0" MaxValue="100000000" CssClass="input-text" Width="60px" Height="18px" Type="Number">
					        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
				        </telerik:RadNumericTextBox>
			        </div>
                
                    <div class="row">
				        <label>Таймаут для сессии пользователя, минут:</label>
				        <telerik:RadNumericTextBox runat="server" ID="txtUserSessionTimeout" EmptyMessage="" MinValue="0" MaxValue="1440" CssClass="input-text" Width="60px" Height="18px" Type="Number">
					        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
				        </telerik:RadNumericTextBox>
			        </div>
                </div>
                <div class="right-column">
                    <asp:Panel ID="plMainUser" runat="server" CssClass="row">
                        <label>Основной пользователь:</label>
                        <uc:DictionaryOnDemandComboBox runat="server" ID="ucMainUser" DictionaryName="tbl_User" DataTextField="Login" ShowEmpty="true" CssClass="select-text"  />
                    </asp:Panel>
                    <div class="row">
                        <label>Активен до:</label>                        
				        <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpActiveUntilDate" ShowPopupOnFocus="true" Width="110px">
					        <DateInput Enabled="true" />
					        <DatePopupButton Enabled="true" />
				        </telerik:RadDatePicker>                        
                    </div>
                    <div class="row">
                        <label>Прайс-лист:</label>                        
                        <uc:DictionaryOnDemandComboBox runat="server" ID="ucPriceList" DictionaryName="tbl_PriceList" DataTextField="Title" ShowEmpty="true" CssClass="select-text"  />
                    </div>
                    <div class="row">
                        <label>Плательщик:</label>                        
                        <uc:DictionaryOnDemandComboBox runat="server" ID="ucPayerCompany" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text"  />
                    </div>
                </div>
                <div class="clear"></div>
                <asp:Panel ID="pUser" Visible="false" runat="server">
                    <h3>Настройки пользователя</h3>
                    <div class="row">
                        <label>Email (логин):</label>
				        <asp:TextBox ID="txtEmail" CssClass="input-text" runat="server" />
				        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmail" Text="*" ErrorMessage="Вы не ввели Email (логин)" Display="Dynamic" ValidationGroup="valSiteUpdate" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ErrorMessage="Вы ввели неправильный Email (логин)" ValidationGroup="valSiteUpdate" runat="server">*</asp:RegularExpressionValidator>
                    </div>
                    <div class="row">
                        <label>Пароль:</label>
				        <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="input-text" autocomplete="off" runat="server" />
				        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPassword" CssClass="input-text" Text="*" ErrorMessage="Вы не ввели пароль" ValidationGroup="valSiteUpdate" runat="server" />
                    </div>
                </asp:Panel>
            </div>
		</telerik:RadPageView>
		<telerik:RadPageView ID="RadPageView2" runat="server">
			<div class="email-settings-container clearfix">				
				<div class="block">
					<h3>Настройки SMTP сервера</h3>
					<div class="row row-wide-label">
						<label>SMTP сервер:</label>
						<asp:TextBox ID="txtSmtpHost" CssClass="input-text"  runat="server" />                    
					</div>
					<div class="row row-wide-label">
						<label>Логин для SMTP сервера:</label>
						<asp:TextBox ID="txtSmtpUsername" CssClass="input-text" runat="server" />                    
					</div>
					<div class="row row-wide-label">
						<label>Пароль для SMTP сервера:</label>
						<asp:TextBox ID="txtSmtpPassword" TextMode="Password" CssClass="input-text" runat="server" autocomplete="off" />                    
					</div>
					<div class="row row-wide-label">
						<label>SMTP порт:</label>
						<asp:TextBox ID="txtSmtpPort" CssClass="input-text" runat="server" />
						<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtSmtpPort" Text="*" ErrorMessage="Неверный формат порта SMTP." ValidationExpression="^[0-9]+$" ValidationGroup="valSiteUpdate" runat="server" />
					</div>
					<div class="row row-wide-label">
						<label>Системный Email:</label>
						<asp:TextBox ID="txtSystemEmail" CssClass="input-text" runat="server" />
						<asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtSystemEmail" Text="*" ErrorMessage="Неверный формат Email." ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="valSiteUpdate" runat="server" />
					</div>
					<div class="row row-wide-label">
						<label>Разрешить использовать системный SMTP:</label>
						<asp:CheckBox runat="server" ID="chxIsAllowUseSystemEmail" />
					</div>
                    <div class="row row-wide-label">
						<label>Отправлять от LeadForce:</label>
						<asp:CheckBox runat="server" ID="chxIsSendFromLeadForce" Checked="true" />
					</div>
				</div>				
			</div>
			<div class="block">
				<h3>Правила для Email</h3>
				<div class="row row-wide-label">
					<label>Отправлять письма подтвердившим подписку:</label>
					<asp:CheckBox runat="server" ID="chxIsSendEmailToSubscribedUser"/>
				</div>
				<div class="row row-wide-label">
					<label>Действия при отсутствии тега &laquo;Блок Отписаться&raquo;:</label>
					<asp:RadioButtonList runat="server" ID="rblUnsubscribeActions" CssClass="list" RepeatLayout="OrderedList" />
				</div>
				<div class="row row-wide-label">
					<label>Действия при отсутствии тега &laquo;Блок Реклама сервиса&raquo;:</label>
					<asp:RadioButtonList runat="server" ID="rblServiceAdvertisingActions" CssClass="list" RepeatLayout="OrderedList" />
				</div>
			</div>
		</telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView5" runat="server">
            <uc:PortalSettings runat="server" ID="ucPortal" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView6" runat="server">
            <asp:Panel runat="server" CssClass="row" ID="plAccessBlock">
                <label>Запрещать доступ с доменов вне списка:</label>
                <asp:CheckBox runat="server" ID="chxIsBlockAccessFromDomainsOutsideOfList" />
            </asp:Panel>
            <uc:SiteDomains runat="server" ID="ucSiteDomains" />
        </telerik:RadPageView>
		<telerik:RadPageView ID="RadPageView3" runat="server">
			<div class="row row-wide-label">
				<label>Максимальный размер одного файла, Кбайт:</label>
				<telerik:RadNumericTextBox runat="server" ID="txtMaxFileSize" EmptyMessage="" MinValue="0" MaxValue="100000000" CssClass="input-text" Width="60px" Height="18px" Type="Number">
					<NumberFormat GroupSeparator="" DecimalDigits="0" /> 
				</telerik:RadNumericTextBox>
			</div>
			<div class="row row-wide-label">
				<label>Максимальный суммарный размер файлов, Мбайт:</label>
				<telerik:RadNumericTextBox runat="server" ID="txtFileQuota" EmptyMessage="" MinValue="0" MaxValue="100000000" CssClass="input-text" Width="60px" Height="18px" Type="Number">
					<NumberFormat GroupSeparator="" DecimalDigits="0" /> 
				</telerik:RadNumericTextBox>
			</div>
		</telerik:RadPageView>
		<telerik:RadPageView ID="RadPageView4" runat="server">
			<div class="row row-wide-label">
				<label>Количество клиентов:</label>
				<asp:Literal runat="server" ID="lrlClientCount" Text="0" />
			</div>
			<div class="row row-wide-label">
				<label>Суммарный размер файлов, Мбайт:</label>
				<asp:Literal runat="server" ID="lrlTotalFileSize" Text="0" />
			</div>
			<table class="stats-table">
				<tr>
					<th>&nbsp;</th>
					<asp:Repeater runat="server" ID="rprMonthes" EnableViewState="false">
						<ItemTemplate>                       
							<td><%# ((DateTime)Eval("StartDate")).ToString("MMMM")%></td>
						</ItemTemplate>
					</asp:Repeater>                    
				</tr>
				<tr>
					<td class="left-align">Просмотров страниц</td>
					<asp:Repeater runat="server" ID="rprPageViews" EnableViewState="false">
						<ItemTemplate>                       
							<td><%# Eval("PageViewCount") %></td>
						</ItemTemplate>
					</asp:Repeater>
				</tr>
				<tr>
					<td class="left-align">Отправлено Email</td>
					<asp:Repeater runat="server" ID="rprEmailSendCount" EnableViewState="false">
						<ItemTemplate>                       
							<td><%# Eval("EmailSendCount")%></td>
						</ItemTemplate>
					</asp:Repeater>
				</tr>
				<tr>
					<td class="left-align">Отправлено SMS</td>
					<asp:Repeater runat="server" ID="rprSmsSendCount" EnableViewState="false">
						<ItemTemplate>                       
							<td><%# Eval("SmsSendCount")%></td>
						</ItemTemplate>
					</asp:Repeater>
				</tr>
			</table>
		</telerik:RadPageView>
	</telerik:RadMultiPage>
	<br/>
	<div class="buttons">
		<asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="valSiteUpdate" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		<asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	</div>
</div>
            </td>
        </tr>
  </table>
</asp:Content>