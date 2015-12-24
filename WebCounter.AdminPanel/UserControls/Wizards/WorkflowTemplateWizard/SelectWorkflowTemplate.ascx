<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectWorkflowTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard.SelectWorkflowTemplate" %>

<div class="wizard-step">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">        
        <script type="text/javascript">
            var pageViewsCount = '<%= PageViewsCount %>';
            function rblWorkflowTemplatesOnClientSelectedIndexChanging() {
                if (pageViewsCount > 1) {
                    if (!confirm('При изменении будут удалены все введенные данные. Продолжить?'))
                        return false;
                }
                return true;
            }
            function GoToNextStep() {
                eval($('#<%= lbtnNext.ClientID %>').attr('href'));
            }
        </script>
    </telerik:RadScriptBlock>
    <div id="radioButtons">        
        <asp:RadioButtonList runat="server" ID="rblWorkflowTemplates" ClientIDMode="AutoID" AutoPostBack="true" OnSelectedIndexChanged="rblWorkflowTemplates_OnSelectedIndexChanged" CssClass="rbl-middle" RepeatLayout="OrderedList" />
    </div>
    <div class="buttons clearfix" style="display: none">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>