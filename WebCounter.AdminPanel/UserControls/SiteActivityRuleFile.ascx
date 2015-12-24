<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActivityRuleFile.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActivityRuleFile" %>
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
    <table width="100%">
        <tr>
            <td valign="top"><label>Выберите файл:</label></td>
            <td width="88%"><telerik:RadAsyncUpload ID="rauFile" runat="server" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/></td>
        </tr>
        <tr>
            <td valign="top"><label>Описание файла:</label></td>
            <td><asp:TextBox ID="txtDescription" Width="100%" TextMode="MultiLine" runat="server" Rows="2"/></td>
        </tr>        
        <tr>
            <td>&nbsp;</td>
            <td>
                <div class="update-file-btns">
                    <telerik:RadButton OnClick="lbUpdateFile_OnClick" ID="lbUpdateFile" Text="Сохранить" Skin="Windows7" runat="server"  CssClass="left"/>
                    <telerik:RadButton ID="lbCancel" Text="Отмена" Skin="Windows7" runat="server" CssClass="left"/>
                </div>
            </td>
        </tr>
    </table>
    
</div>