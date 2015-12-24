<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Labitec.LeadForce.Portal.Default" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>
<%@ Register TagPrefix="uc" TagName="Welcome" Src="~/Shared/UserControls/Welcome.ascx" %>
<%@ Register TagPrefix="uc" TagName="CreatePublication" Src="~/Main/Discussions/UserControls/ActivityRibbon/CreatePublication.ascx" %>
<%@ Register TagPrefix="uc" TagName="ComingEvents" Src="~/Main/Discussions/UserControls/ComingEvents.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationsRibbon" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationsRibbon.ascx" %>
<%@ Register TagPrefix="uc" TagName="SocialSubscription" Src="~/Shared/UserControls/SocialSubscription.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="main-page">
        <uc:Welcome runat="server" ID="ucWelcome" Visible="false" />
        <asp:Panel runat="server" ID="plDiscussions" Visible="false">        
            <div class="aside">            
                <uc:ComingEvents runat="server" ID="ucComingEvents" Visible="true" />
            </div>
            <div class="activity-ribbon-container">        
                <uc:CreatePublication runat="server" ID="ucCreatePublication" />
                <br/>
                <uc:PublicationsRibbon runat="server" ID="ucPublicationsRibbon" />
            </div>
            <div class="rside">
                <div class="block">    
                    <h4>Сообщение от компании</h4>
                    <div class="block-content">
                        <p>
                            <telerik:RadCodeBlock runat="server">
                                <%= ((LeadForcePortalBasePage)Page).PortalSettings.CompanyMessage %>
                            </telerik:RadCodeBlock>
                        </p>
                    </div>
                </div>
                <uc:SocialSubscription runat="server" ID="ucSocialSubscription" />                
            </div>
        </asp:Panel>
        <div style="display: none">LeadForcePortal</div>
    </div>
</asp:Content>
