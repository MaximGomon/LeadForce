<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="Labitec.LeadForce.Portal.StaticPages.AccessDenied" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/Shared/UserControls/NotificationMessage.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="b-block">
        <h4 class="top-radius">Доступ запрещен</h4>
        <div class="block-content bottom-radius">
            <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" Text="Вам отказано в доступе, обратитесь к администратору." />
        </div>
    </div>
</asp:Content>
