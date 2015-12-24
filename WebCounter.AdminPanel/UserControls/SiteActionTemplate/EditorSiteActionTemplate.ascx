<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditorSiteActionTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.EditorSiteActionTemplate" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        var activeControl = "messageBody";
        var textboxPosition = 0;
    
        function OnClientSelectedIndexChanged(sender, args) {
            if (activeControl == "messageBody") {
                var editor = <%=ucHtmlEditor.ClientID %>_Editor();
                editor.pasteHtml(sender.get_selectedItem().get_value());
            }
        
            if (activeControl == "messageTitle") {
                var textTitle = $("#txtMessageCaption").val();
                var textTitleOutput = textTitle.substring(0, textboxPosition) + sender.get_selectedItem().get_value() + textTitle.substring(textboxPosition);
                $("#txtMessageCaption").val(textTitleOutput);
                setCaretPosition(document.getElementById("txtMessageCaption"), textboxPosition + sender.get_selectedItem().get_value().length);
                textboxPosition = getCaretPosition(document.getElementById("txtMessageCaption"));
            }
             
        }
        
        function OnClientDragStart(sender, e) {
            e.set_cancel(true);
        }

        function getCaretPosition(el) {
            if (el.selectionStart) {
                return el.selectionStart;
            } else if (document.selection) {
                el.focus();

                var r = document.selection.createRange();
                if (r == null) {
                    return 0;
                }

                var re = el.createTextRange(),
                    rc = re.duplicate();
                re.moveToBookmark(r.getBookmark());
                rc.setEndPoint('EndToStart', re);

                return rc.text.length;
            }
            return 0;
        }

        function setCaretPosition(ctrl, pos) {

            if (ctrl.setSelectionRange) {
                ctrl.focus();
                ctrl.setSelectionRange(pos, pos);
            }
            else if (ctrl.createTextRange) {
                var range = ctrl.createTextRange();
                range.collapse(true);
                range.moveEnd('character', pos);
                range.moveStart('character', pos);
                range.select();
            }
        }
    
        function fileUploaded(sender, args) {
            $('#<%= lbAddFile.ClientID %>').show();
        }
        function fileUploadRemoved(sender, args) {
            $('#<%= lbAddFile.ClientID %>').hide();
        }

        $(document).ready(function () {
            $("#txtMessageCaption").live('click', function () {
                textboxPosition = getCaretPosition(document.getElementById("txtMessageCaption"));
                activeControl = "messageTitle";
            });

            $("#txtMessageCaption").live('keyup', function () {
                textboxPosition = getCaretPosition(document.getElementById("txtMessageCaption"));
                activeControl = "messageTitle";
            });
        });
    
            window.onload = function() {
                <%= ucHtmlEditor.ClientID %>_setupEditor();
            };
    </script>
</telerik:RadScriptBlock>

<asp:Panel ID="pnlMessageCaption" CssClass="row" runat="server">
    <label>Тема сообщения:</label>
    <asp:TextBox ID="txtMessageCaption" ClientIDMode="Static" CssClass="input-text" runat="server" Width="717px" />
    <asp:RequiredFieldValidator ID="rfvMessageCaption" ControlToValidate="txtMessageCaption" ErrorMessage="Вы не ввели 'Тема сообщения'" runat="server">*</asp:RequiredFieldValidator>
</asp:Panel>

<table width="900px">
    <tr>
        <td valign="top" width="600px">
            <uc:HtmlEditor runat="server" ID="ucHtmlEditor" Width="580px" Height="400px" CallFunctionOnClick="activeControl='messageBody';" Module="SiteActionTemplates" />
        </td>
        <td valign="top" align="right">
            <telerik:RadPanelBar ID="rpbInsertData" ExpandMode="FullExpandedItem" Skin="Windows7" Width="300px" Height="402px" runat="server">
                <Items>
                    <telerik:RadPanelItem Expanded="True" Text="Реквизиты посетителя">
                        <ContentTemplate>
                            <telerik:RadListBox ID="rlbSiteColumns" EnableDragAndDrop="True" OnClientDragStart="OnClientDragStart" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" Skin="Windows7" CssClass="radlistbox-noborder" Width="298px" Height="346px" runat="server" />
                        </ContentTemplate>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Text="Файлы">
                        <ContentTemplate>
                            <div class="siteactiontemplate-addfile clearfix">
                                <label>Выберите файл:</label><telerik:RadAsyncUpload ID="rauFile" runat="server" OnClientFileUploadRemoved="fileUploadRemoved" OnClientFileUploaded="fileUploaded" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена" />
                                <telerik:RadButton ID="lbAddFile" OnClick="lbAddFile_OnClick" Text="Добавить" Skin="Windows7" ClientIDMode="AutoID" style="display:none" runat="server" />
                            </div>
                            <telerik:RadListBox ID="rlbFiles" EnableDragAndDrop="True" OnClientDragStart="OnClientDragStart" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" Skin="Windows7" CssClass="radlistbox-noborder" Width="298px" Height="274px" runat="server" />
                        </ContentTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelBar>
        </td>
    </tr>
</table>