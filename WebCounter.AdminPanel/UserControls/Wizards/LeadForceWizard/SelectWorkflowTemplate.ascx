<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectWorkflowTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard.SelectWorkflowTemplate" %>
<%@ Register TagPrefix="uc" TagName="SiteTemplateComboBox" Src="~/UserControls/Wizards/LeadForceWizard/SiteTemplateComboBox.ascx" %>

<div class="wizard-step">
    <h3>Выбор процессов</h3>
    <div class="row">
        <label>Шаблон</label>
        <uc:SiteTemplateComboBox runat="server" ID="ucSiteTemplateComboBox" AutoPostBack="true" OnSelectedIndexChanged="ucSiteTemplateComboBox_OnSelectedIndexChanged" />
    </div>
    <h3>Процессы</h3>
    <table>
        <tr>
            <td>
                <telerik:RadListBox runat="server" ID="rlbSource" 
                Skin="Windows7"  
                Height="300px"              
                Width="300px"
                AllowTransfer="true" 
                AllowTransferOnDoubleClick="true"
                TransferToID="rlbDestination" 
                TransferMode="Copy"
                AllowTransferDuplicates="false"   
                SelectionMode="Single"                                                                      
                AutoPostBackOnTransfer="true"  
                OnTransferring="rlbSource_OnTransferring"              
                >
                    <ButtonSettings TransferButtons="TransferFrom" />                        
                </telerik:RadListBox>
            </td>
            <td width="35px">&nbsp;</td>
            <td>
                <telerik:RadListBox runat="server" ID="rlbDestination" AllowTransferDuplicates="false" OnItemCreated="rlbDestination_OnItemCreated"
                 Skin="Windows7" AllowTransfer="false" Height="300px" Width="450px" AllowDelete="true"  />
            </td>
        </tr>
    </table>        
    <br/>
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>
