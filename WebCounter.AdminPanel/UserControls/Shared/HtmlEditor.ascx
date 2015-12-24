<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HtmlEditor.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Shared.HtmlEditor" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var <%= ClientID %>_isEnabled = true;
        var <%= ClientID %>_CallFunctionOnKeyUp = "<%= CallFunctionOnKeyUp %>";
        var <%= ClientID %>_CallFunctionOnKeyPress = "<%= CallFunctionOnKeyPress %>";
        var <%= ClientID %>_CallFunctionOnClick = "<%= CallFunctionOnClick %>";
        var <%= ClientID %>_IsDirtyEnabled = '<%= IsDirtyEnabled ? "1" : "0" %>';
        function <%= ClientID %>_setupEditor() {            
            var editor = $find("<%=reHtmlEditor.ClientID%>");
            editor.onParentNodeChanged();
            
            if (<%= ClientID %>_CallFunctionOnKeyUp != '') {
                editor.attachEventHandler("onkeyup", function(e) {
                    return eval(<%= ClientID %>_CallFunctionOnKeyUp);
                });
            }
            if (<%= ClientID %>_CallFunctionOnKeyPress != '') {
                editor.attachEventHandler("onkeypress", function(e) {
                    return eval(<%= ClientID %>_CallFunctionOnKeyPress);
                });
            }
            if (<%= ClientID %>_CallFunctionOnClick != '') {
                editor.attachEventHandler("click", function(e) {
                    return eval(<%= ClientID %>_CallFunctionOnClick);
                });
            }
        };        
        function <%= ClientID %>_OnClientLoad(editor, args) {            
            <%= ClientID %>_EnableEditor(<%= ClientID %>_isEnabled);            
            if (<%= ClientID %>_IsDirtyEnabled == '1') {
                editor.attachEventHandler("onkeypress", function(e) {
                    $('.editor-changed').val('true');                    
                });
            }
        }
        function <%= ClientID %>_EnableEditor(isEnabled) {
            var editor = $find('<%=reHtmlEditor.ClientID%>');            
            if (!isEnabled) editor.set_mode(4); else editor.set_mode(1);
            editor.enableEditing(isEnabled);                        
            <%= ClientID %>_isEnabled = isEnabled;
        }
        function <%= ClientID %>_OnClientCommandExecuting() {            
            return <%= ClientID %>_isEnabled;
        }
        function <%= ClientID %>_GetSelection() {
            var editor = $find('<%=reHtmlEditor.ClientID%>'); 
            return editor.getSelectionHtml();
        }
        function <%= ClientID %>_Editor() {
            return $find('<%=reHtmlEditor.ClientID%>');            
        }        
    </script>
</telerik:RadScriptBlock>
<telerik:RadEditor runat="server" ID="reHtmlEditor" EditModes="Design,Html" Skin="Windows7" EnableResize="false" ToolsFile="~/RadEditor/HtmlEditorToolsSimple.xml" CssClass="rad-editor">
    <ImageManager EnableImageEditor="false" EnableThumbnailLinking="false"></ImageManager>    
</telerik:RadEditor>
<input type="text" class="editor-changed" value="" style="height: 0; border:0; line-height: 0;font-size: 1px" />
