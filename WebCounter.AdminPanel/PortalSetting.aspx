<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PortalSetting.aspx.cs" Inherits="WebCounter.AdminPanel.PortalSetting" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">        

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">    
    <script type="text/javascript">
        function fileUploaded(sender, args) {
            $find('<%= RadAjaxManager.ClientID %>').ajaxRequest();
            setTimeout(function () { sender.deleteFileInputAt(0); }, 10);
        }
        function ShowHideInput(checkbox) {
            if ($(checkbox).is(":checked")) {
                $(checkbox).parent().find("input[type=text]").show();
            } else {
                $(checkbox).parent().find("input[type=text]").hide();
            }
        }
    </script>
    </telerik:RadCodeBlock>

    <div class="portal-settings">
        <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />
        <div class="row">
            <label>Название сообщества:</label>
            <asp:TextBox ID="txtTitle" CssClass="input-text" runat="server" Width="500px" />    
        </div>
        <div class="row">
            <label>Приветствие:</label>
            <asp:TextBox ID="txtWelcomeMessage" CssClass="input-text" runat="server" Width="500px" />    
        </div>
        <div class="row">
            <label>Домен:</label>
            <asp:TextBox ID="txtDomain" CssClass="input-text" runat="server" />    
        </div>
        <div class="row">
            <label>Сообщение от компании:</label>
            <asp:TextBox ID="txtCompanyMessage" CssClass="area-text" runat="server" TextMode="MultiLine" Width="500px" />    
        </div>
        <div class="row">
            <label>Страница Facebook:</label>
            <asp:CheckBox runat="server" ID="chxFacebookProfile" onclick="ShowHideInput(this);" />
            <asp:TextBox runat="server" CssClass="input-text" ID="txtFacebookProfile" Width="476px" />
        </div>
        <div class="row">
            <label>Страница ВКонтакте:</label>
            <asp:CheckBox runat="server" ID="chxVkontakteProfile" onclick="ShowHideInput(this);" />
            <asp:TextBox runat="server" CssClass="input-text" ID="txtVkontakteProfile" Width="476px" />
        </div>
        <div class="row">
            <label>Страница Twitter:</label>
            <asp:CheckBox runat="server" ID="chxTwitterProfile" onclick="ShowHideInput(this);" />
            <asp:TextBox runat="server" CssClass="input-text" ID="txtTwitterProfile" Width="476px" />
        </div>
        <div class="row row-color-picker clearfix">
            <label>Фон главного меню:</label>
            <telerik:RadColorPicker ShowIcon="true" ID="rcpMainMenu" runat="server" PaletteModes="All" />
        </div>
        <div class="row row-color-picker clearfix">
            <label>Фон заголовков блоков:</label>
            <telerik:RadColorPicker ShowIcon="true" ID="rcpBlockTitleBackground" runat="server" PaletteModes="All" />
        </div>
        <div class="row clearfix">
            <label class="block-label">Логотип:</label>
            <div class="portal-logo-container">
                <telerik:RadBinaryImage runat="server" ID="rbiLogo" AlternateText="Thumbnail" CssClass="binary-image" />
                <br/>
                <telerik:RadAsyncUpload runat="server" ID="rauLogo" MaxFileInputsCount="1" OnClientFileUploaded="fileUploaded" OnFileUploaded="rauLogo_OnFileUploaded" AllowedFileExtensions="jpeg,jpg,gif,png,bmp" />            
            </div>
        </div>
        <div class="row clearfix">
            <label class="block-label">Шаблон шапки:</label>
            <uc:HtmlEditor runat="server" ID="ucHtmlEditor" Width="1050px" Height="400px" Module="Portal" IsUploadEnabled="false" />        
        </div>
        <br/>
	    <div class="buttons">
		    <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		    <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	    </div>
    </div>

</asp:Content>
