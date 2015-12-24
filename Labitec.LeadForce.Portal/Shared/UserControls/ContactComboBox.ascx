<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactComboBox.ascx.cs" Inherits="Labitec.LeadForce.Portal.Shared.UserControls.ContactComboBox" %>
<telerik:RadComboBox runat="server" ID="rcbContact" 
                    CssClass="contact-combobox"
                    AllowCustomText="false" 
                    Filter="Contains" 
                    EnableEmbeddedSkins="false" 
                    skin="Labitec"
                    Width="234px"
                    ZIndex="50001"
                    EnableLoadOnDemand="True" 
                    ShowMoreResultsBox="true"
                    EnableVirtualScrolling="true"                     
                    OnItemsRequested="rcbContact_ItemsRequested"
                    MaxHeight="150px"
                     />
<asp:RequiredFieldValidator ID="rfvDictionary" Display="Dynamic" ControlToValidate="rcbContact" CssClass="required" Text="*" ErrorMessage="Вы не выбрали значение" runat="server" InitialValue=""/>