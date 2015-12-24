<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteTemplateComboBox.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard.SiteTemplateComboBox" %>
<telerik:RadComboBox runat="server" ID="rcbSiteTemplate" 
                    CssClass="select-text"
                    AllowCustomText="false" 
                    EmptyMessage="Выберите значение"
                    Filter="Contains" 
                    EnableEmbeddedSkins="false" 
                    skin="Labitec"
                    Width="234px"
                    ZIndex="50001"                    
                    MaxHeight="150px"
                     />
<asp:RequiredFieldValidator ID="rfvDictionary" Display="Dynamic" ControlToValidate="rcbSiteTemplate" CssClass="required" Text="*" ErrorMessage="Вы не выбрали значение" runat="server" InitialValue=""/>