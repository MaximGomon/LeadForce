<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectSiteTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard.SelectSiteTemplate" %>
<%@ Register TagPrefix="uc" TagName="SiteTemplateComboBox" Src="~/UserControls/Wizards/LeadForceWizard/SiteTemplateComboBox.ascx" %>

<div class="wizard-step">
    <h3>Выбор шаблона</h3>
    <div class="row">
        <label>Шаблон</label>
        <uc:SiteTemplateComboBox runat="server" AutoPostBack="true" OnSelectedIndexChanged="ucSiteTemplateComboBox_OnSelectedIndexChanged" ID="ucSiteTemplateComboBox" ValidationGroup="groupSelectSiteTemplate" ValidationErrorMessage="Вы не выбрали шаблон" />
    </div>
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" ValidationGroup="groupSelectSiteTemplate" runat="server"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>