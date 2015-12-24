<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstructionAndText.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.FormWizard.InstructionAndText" %>

<div class="wizard-step">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function pageLoad() {
                var editors = get_allRadEditors();
                for (i = 0; i < editors.length; i++) {                    
                    var editor = editors[i];
                    editor.setSize(editor.get_element().style.width, 75);
                    editor.get_contentAreaElement().style.height = "75px";
                }
            }
            function get_allRadEditors() {
                var allRadEditors = [];
                var allRadControls = $telerik.radControls;
                for (var i = 0; i < allRadControls.length; i++) {
                    var element = allRadControls[i];
                    if (Telerik.Web.UI.RadEditor && element instanceof Telerik.Web.UI.RadEditor && element.get_id().toLowerCase().indexOf('htmleditor') == -1) { //для эдитора                        
                        Array.add(allRadEditors, element);
                    }
                }
                return allRadEditors;
            }
        </script>
    </telerik:RadScriptBlock>
    <div class="row">
        <label>Название формы:</label>
        <asp:TextBox runat="server" ID="txtFormTitle" CssClass="input-text" Width="390px" />
        <asp:RequiredFieldValidator ID="txtFormTitleRequiredFieldValidator" ErrorMessage="Введите название формы" ControlToValidate="txtFormTitle" runat="server" ValidationGroup="InstructionAndText">*</asp:RequiredFieldValidator>
    </div>
    <asp:Panel runat="server" ID="plTextBlocksContainer">
        
    </asp:Panel>
    <div class="row">
        <label>Текст кнопки:</label>
        <asp:TextBox runat="server" ID="txtTextButton" CssClass="input-text" Width="390px" />
    </div>
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server" ValidationGroup="InstructionAndText"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>