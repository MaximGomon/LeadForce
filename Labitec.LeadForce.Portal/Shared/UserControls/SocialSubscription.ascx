<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialSubscription.ascx.cs" Inherits="Labitec.LeadForce.Portal.Shared.UserControls.SocialSubscription" %>

<asp:Panel runat="server" CssClass="block" ID="plSocialSubscription" Visible="false">
    <br/>
    <h4>Подписка</h4>
    <div class="block-content">
        <asp:Literal runat="server" ID="lrlVkontakteProfile" />
        <asp:Literal runat="server" ID="lrlTwitterProfile" />
        <asp:Literal runat="server" ID="lrlFacebookProfile" />                
    </div>                    
</asp:Panel>