<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadForceWizard.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard.LeadForceWizard" %>

<div class="wizard">    
    <h2>Мастер настройки LeadForce</h2>   
    <telerik:RadAjaxPanel ID="rapRefreshWizard" runat="server" OnAjaxRequest="rapLeadForceWizard_OnAjaxRequest">    
        <telerik:RadAjaxPanel ID="rapLeadForceWizard" runat="server">
            <telerik:RadTabStrip ID="rtsWizard" CausesValidation="false" PerTabScrolling="true" ScrollChildren="true" ScrollButtonsPosition="Right" SelectedIndex="0" runat="server" MultiPageID="rmpWizard" Skin="Windows7">
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="rmpWizard" runat="server" SelectedIndex="0" OnPageViewCreated="rmpWizard_OnPageViewCreated" CssClass="multiPage">
            </telerik:RadMultiPage>
        </telerik:RadAjaxPanel>
    </telerik:RadAjaxPanel>
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            var radAjaxPanel; 
            $(window).load(function () {
                radAjaxPanel = $find('<%= rapRefreshWizard.ClientID %>');
            });                     
            function ClearTabs() {
                radAjaxPanel.ajaxRequest('Clear');
            }
        </script>
    </telerik:RadScriptBlock>        
</div>