<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActionAfterProcessing.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.FormWizard.ActionAfterProcessing" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="PopupSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/PopupSiteActionTemplate.ascx" %>

<div class="wizard-step">
    <div class="row">
		<label>Перейти на страницу:</label>
        <asp:TextBox runat="server" ID="txtUrl" CssClass="input-text" />
	</div>    
    <asp:Panel runat="server" ID="plInviteFriendSettings" Visible="false">
        <uc:PopupSiteActionTemplate ID="ucPopupSiteActionTemplate" SiteActionTemplateCategory="Workflow" runat="server" ValidationGroup="ActionAfterProcessing" />        
        <div class="row">
            <label>Процесс:</label>
            <uc:DictionaryOnDemandComboBox ID="dcbWorkflowTemplate" DictionaryName="tbl_WorkflowTemplate" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
        </div>
    </asp:Panel>
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server" ValidationGroup="ActionAfterProcessing"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>        
    </div>    
</div>