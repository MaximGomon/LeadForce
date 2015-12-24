<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTMLEditor.ascx.cs" Inherits="Labitec.LeadForce.Portal.Shared.UserControls.HTMLEditor" %>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var <%= ClientID %>_isEnabled = true;
        var <%= ClientID %>_CallFunctionOnKeyUp = "<%= CallFunctionOnKeyUp %>";
        var <%= ClientID %>_CallFunctionOnKeyPress = "<%= CallFunctionOnKeyPress %>";        
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
        };        
        function <%= ClientID %>_OnClientLoad(editor, args) {
            <%= ClientID %>_EnableEditor(<%= ClientID %>_isEnabled);            
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

<telerik:RadEditor runat="server" ID="reHtmlEditor" Skin="Windows7" EnableResize="false" ToolsFile="~/RadEditor/HtmlEditorToolsSimple.xml" CssClass="rad-editor">
    <ImageManager EnableImageEditor="false" EnableThumbnailLinking="false"></ImageManager>    
</telerik:RadEditor>