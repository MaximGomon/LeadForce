<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormWizard.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.FormWizard.FormWizard" %>

<div class="wizard">
    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						CssClass="validation-summary"
						runat="server"
						EnableClientScript="true"
						HeaderText="Заполните все поля корректно:" ValidationGroup="groupEdit"/>                                 
    <telerik:RadAjaxPanel ID="rapRefreshWizard" runat="server" OnAjaxRequest="rapFormWizard_OnAjaxRequest">            
            <telerik:RadTabStrip ID="rtsWizard" PerTabScrolling="true" ScrollChildren="true" ScrollButtonsPosition="Right" SelectedIndex="0" runat="server" MultiPageID="rmpWizard" Skin="Windows7">
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="rmpWizard" runat="server" SelectedIndex="0" OnPageViewCreated="rmpWizard_OnPageViewCreated" CssClass="multiPage">
            </telerik:RadMultiPage>        
    </telerik:RadAjaxPanel>        
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
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
    <br/>
    <asp:Panel runat="server" ID="plButtons" CssClass="buttons" Visible="false">
		<asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" runat="server" ValidationGroup="groupEdit"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		<asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />	
    </asp:Panel>
</div>