<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="WebCounter.AdminPanel.AccessDenied" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Доступ запрещен</title>
    <link rel="stylesheet" href="<%# ResolveUrl("~/App_Themes/Default/AdminPanel.css") %>" type="text/css" enableviewstate="false" />
    <script src="<%#ResolveUrl("~/Scripts/jquery-1.5.2.min.js")%>" type="text/javascript"></script>
    <script src="<%#ResolveUrl("~/Scripts/jquery.corner.js")%>" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="header clearfix">
            <a class="logo" href="/">
                <img title="LeadForce" alt="LeadForce" src='<%= ResolveUrl("~/App_Themes/Default/images/logo.png") %>'></a>
        </div>
        <div class="content clearfix">
            <div class="b-block">            
                <div class="block-content bottom-radius">
                    <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning"
                        Text="Вам отказано в доступе, обратитесь к администратору." />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
