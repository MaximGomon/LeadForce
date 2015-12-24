<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="ActivityRibbon.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.Discussions.ActivityRibbon" %>
<%@ Register TagPrefix="uc" TagName="PublicationTypes" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationTypes.ascx" %>
<%@ Register TagPrefix="uc" TagName="CreatePublication" Src="~/Main/Discussions/UserControls/ActivityRibbon/CreatePublication.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationCategory" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationsRibbon" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationsRibbon.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<div class="activity-page">
    <div class="aside">        
        <div class="block">    
            <h4>Лента активности</h4>
            <div class="block-content">
                <uc:PublicationTypes runat="server" ID="ucPublicationTypes" />
            </div>
        </div>
        <hr/>        
        <uc:PublicationCategory runat="server" ID="ucPublicationCategory" />
    </div>
    <div class="activity-ribbon-container">        
        <uc:CreatePublication runat="server" ID="ucCreatePublication" />
        <br/>
        <uc:PublicationsRibbon runat="server" ID="ucPublicationsRibbon" />
    </div>
</div>
</asp:Content>

