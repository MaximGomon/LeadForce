<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteDomain.aspx.cs" Inherits="WebCounter.AdminPanel.SiteDomain" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<%@ Register TagPrefix="uc" TagName="CheckSite" Src="~/UserControls/SiteSettings/CheckSite.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr>
            <td width="195px" valign="top">
                <div class="aside">
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td valign="top">
                <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                HeaderText="Заполните все поля корректно:"
                                        ValidationGroup="valGroup"
						                />
                <div class="row">
                    <label>Ссылка:</label>
                    <asp:TextBox runat="server" ID="txtDomain" CssClass="input-text" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtDomain" ErrorMessage="Вы не ввели домен" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>                    
                </div>
                <div class="row">
                    <label>Псевдонимы:</label>
                    <asp:TextBox runat="server" ID="txtAliases" CssClass="area-text" Width="700px" TextMode="MultiLine" Height="30px" />                    
                </div>
                <div class="row">
                    <label>Статус:</label>
                    <asp:Literal runat="server" ID="lrlSiteDomainStatus" />
                </div>
                <br/>
                <telerik:RadAjaxPanel runat="server">
	                <div class="buttons clearfix">
		                <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" runat="server" ValidationGroup="valGroup"><em>&nbsp;</em><span>Проверить и сохранить</span></asp:LinkButton>
		                <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	                </div>
                    <br/>
                    <uc:CheckSite runat="server" ID="ucCheckSite" />
                </telerik:RadAjaxPanel>
            </td>
        </tr>
    </table>
</asp:Content>
