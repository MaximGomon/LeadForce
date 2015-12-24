<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="ActivateUser.aspx.cs" Inherits="Labitec.LeadForce.Portal.Handlers.ActivateUser" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/Shared/UserControls/NotificationMessage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="b-block">
        <h4 class="top-radius">Активация пользователя</h4>
        <div class="block-content bottom-radius">
            <div class="static-page">
                <uc:NotificationMessage runat="server" ID="ucNotificationMessage" />
            </div>
        </div>
    </div>
</asp:Content>