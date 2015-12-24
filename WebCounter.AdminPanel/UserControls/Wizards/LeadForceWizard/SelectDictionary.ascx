<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectDictionary.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard.SelectDictionary" %>
<%@ Register TagPrefix="uc" TagName="SiteTemplateComboBox" Src="~/UserControls/Wizards/LeadForceWizard/SiteTemplateComboBox.ascx" %>

<div class="wizard-step">
    <h3>Выбор справочников</h3>
    <div class="row">
        <label>Шаблон</label>
        <uc:SiteTemplateComboBox runat="server" ID="ucSiteTemplateComboBox" AutoPostBack="true" OnSelectedIndexChanged="ucSiteTemplateComboBox_OnSelectedIndexChanged" />
    </div>
    <h3>Справочники</h3>
    <table>
        <tr>
            <td><telerik:RadTreeView runat="server"                         
            BorderStyle="Solid" BorderWidth="1px" BorderColor="#C8D6E5" 
            Width="300px" Height="350px" 
            EnableDragAndDrop="true" Skin="Windows7" ID="rtvSource"
            OnNodeDrop="rtvDestination_OnNodeDrop" /></td>
            <td width="30px">&nbsp;</td>
            <td>
                <telerik:RadTreeView runat="server" 
                    BorderStyle="Solid" BorderWidth="1px" BorderColor="#C8D6E5" 
                    EnableDragAndDropBetweenNodes="true"
                    Height="350px" Width="450px"             
                    Skin="Windows7" ID="rtvDestination"
                 />                 
             </td>
             <td valign="top" style="padding-left:10px">
                <telerik:RadButton ID="rbtnRemove" Skin="Windows7" runat="server" Width="20px" OnClick="rbtnRemove_OnClick">
                    <Icon PrimaryIconUrl="~/App_Themes/Default/Images/icoRbtnDelete.png" PrimaryIconTop="6px" PrimaryIconLeft="7px" />
                </telerik:RadButton>
             </td>
        </tr>
    </table>    
    <br/>
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>
