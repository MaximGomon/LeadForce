<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteActionTemplate.aspx.cs" Inherits="WebCounter.AdminPanel.SiteActionTemplate" %>
<%@ Register TagPrefix="uc" TagName="EditorSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/EditorSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="SettingsSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SettingsSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="StatsSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/StatsSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						CssClass="validation-summary"
						runat="server"
						EnableClientScript="true"
						HeaderText="Заполните все поля корректно:"
						ValidationGroup="valGroup" />

                <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
                    <Tabs>
                        <telerik:RadTab Text="Настройки сообщения" Value="Settings" />
                        <telerik:RadTab Text="Шаблон сообщения" Value="Template" />
                        <telerik:RadTab Text="Статистика переходов" Value="Stats" />
                    </Tabs>
                </telerik:RadTabStrip>
    
                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <uc:SettingsSiteActionTemplate ID="ucSettingsSiteActionTemplate" ValidationGroup="valGroup" runat="server" />
                    </telerik:RadPageView>
        
                    <telerik:RadPageView ID="rpvTemplate" runat="server">
                        <asp:Panel ID="pnlTitle" CssClass="row" Visible="False" runat="server">
                            <label>Название шаблона:</label>
                            <asp:TextBox ID="txtTitle" CssClass="input-text" Width="717px" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitle" ErrorMessage="Вы не ввели 'Название шаблона'" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                        </asp:Panel>
                        <uc:EditorSiteActionTemplate ID="ucEditorSiteActionTemplate" ValidationGroup="valGroup" runat="server" />
                    </telerik:RadPageView>
        
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                        <uc:StatsSiteActionTemplate ID="ucStatsSiteActionTemplate" runat="server" />
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
    
                <br />
                <div class="buttons">
                    <asp:LinkButton ID="BtnUpdate" OnClick="BtnUpdate_Click" CssClass="btn" ValidationGroup="valGroup" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                    <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
                </div>
            </td>
        </tr>        
    </table>	
</asp:Content>