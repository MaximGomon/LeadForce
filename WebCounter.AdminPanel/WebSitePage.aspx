<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSitePage.aspx.cs" Inherits="WebCounter.AdminPanel.WebSitePage" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="<%# ResolveUrl("~/App_Themes/Default/AdminPanel.css") %>" type="text/css" enableviewstate="false" />
    <title></title>    
    <script type="text/javascript">          
        function GetRadWindow() { var oWindow = null;if (window.radWindow) oWindow = window.radWindow;else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;return oWindow;}
        function Close() { GetRadWindow().Close(); GetRadWindow().BrowserWindow.refreshGrid(); }
        function OnClientSelectedIndexChanged(sender, args) {
            args.set_cancel(true);
        }        
    </script>     
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnablePageMethods="true" />
            
        <telerik:RadAjaxLoadingPanel ID="ajaxLoadingPanel" Skin="Windows7" runat="server"></telerik:RadAjaxLoadingPanel>
        
    <div class="website-page" style="padding: 10px">
        <uc:NotificationMessage ID="ucNotificationMessage" runat="server" MessageType="Warning" />
        <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		    <Tabs>
			    <telerik:RadTab Text="Основные данные" />                    
		        <telerik:RadTab Text="Содержимое страницы"/>
		    </Tabs>
	    </telerik:RadTabStrip>
	    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		    <telerik:RadPageView ID="RadPageView1" runat="server">
		        <div class="row">
                    <label>Название:</label>
                    <asp:TextBox runat="server" ID="txtWebSitePageTitle" CssClass="input-text" Width="550px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtWebSitePageTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupWebSitePage" runat="server" />
                </div>                                                                        
                <div class="row">
                    <label>Статус:</label>
                    <asp:DropDownList runat="server" CssClass="select-text" ID="ddlStatus" />
                </div>
                <div class="row">
                    <label>Главная страница</label>
                    <asp:CheckBox runat="server" ID="chxIsHomePage" />
                </div>
                <div class="row">
                    <label>Ссылка:</label>
                    <asp:Literal runat="server" ID="lrlDomain" />
                    <asp:TextBox runat="server" ID="txtLink" CssClass="input-text" Width="200px" />
                    <asp:Literal runat="server" ID="lrlWebSiteId" />
                </div>
                <h3>Мета данные</h3>
                <div class="row">
                    <label>Заголовок:</label>
                    <asp:TextBox runat="server" ID="txtMetaTitle" CssClass="input-text" Width="550px" />
                </div>
                <div class="row">
                    <label>Ключевые слова:</label>
                    <asp:TextBox runat="server" ID="txtMetaKeywords" CssClass="input-text" Width="550px" />
                </div>
                <div class="row">
                    <label>Описание:</label>
                    <asp:TextBox runat="server" CssClass="area-text" Width="550px" TextMode="MultiLine" ID="txtMetaDescription" />
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server">
                <telerik:RadAjaxPanel runat="server">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <uc:HtmlEditor runat="server" ID="ucHtmlEditor" ContentFilters="None" Width="100%" Height="500px" Module="MiniWebSite" />
                            </td>
                            <td width="320px" valign="top" align="right">
                                 <telerik:RadPanelBar ID="rpbInsertData" ExpandMode="FullExpandedItem" Skin="Windows7" Width="300px" Height="502px" runat="server">
                                    <Items>                                    
                                        <telerik:RadPanelItem Expanded="True" Text="Скрипты и стили">
                                            <ContentTemplate>                                            
                                                <telerik:RadListBox ID="rlbScriptsAndStyles" AutoPostBack="True" OnItemCheck="rlbScriptsAndStyles_OnItemCheck" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" SelectionMode="Single" CheckBoxes="True" Skin="Windows7" CssClass="radlistbox-noborder" Width="298px" Height="274px" runat="server" />
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>
                                    </Items>
                                </telerik:RadPanelBar>
                            </td>
                        </tr>
                    </table>                
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <br/>
		<div class="buttons">
			<asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupWebSitePage" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
			<asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" NavigateUrl="javascript:;" onclick="Close();" />
		</div>
    </div>
    </form>
</body>
</html>
