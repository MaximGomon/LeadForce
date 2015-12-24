<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="File.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ModuleEditionAction.File.File" %>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
    // <![CDATA[
        var $ = $telerik.$;
        function pageLoad() {
            if (!Telerik.Web.UI.RadAsyncUpload.Modules.FileApi.isAvailable()) {
                $(".wrapper").replaceWith(
                    $("<span><strong>Your browser does not support Drag and Drop. Please take a look at the info box for additional information.</strong></span>"));
            }
        }
        // ]]> 
    </script>
    </telerik:RadCodeBlock>

<div class="update-file clearfix">
    <div class="update-file-upload">
        <label>Выберите файл:</label><telerik:RadAsyncUpload ID="rauFile" runat="server" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
    </div>  
    <div class="update-file-btns">
        <telerik:RadButton OnClick="lbUpdateFile_OnClick" ID="lbUpdateFile" Text="Сохранить" Skin="Windows7" runat="server"  CssClass="left"/>
        <telerik:RadButton ID="lbCancel" Text="Отмена" Skin="Windows7" runat="server" CssClass="left"/>
    </div>
</div>
