<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebCounter.AdminPanel.Login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="AdminPanelCss" runat="server" rel="stylesheet" type="text/css" enableviewstate="false" />
    <script type="text/javascript">
        function keyPress(evt) {
            if ((evt.which || evt.keyCode) && (evt.which == 13 || evt.keyCode == 13) && Page_ClientValidate()) {
                window.location = document.getElementById('<%= btnLogin.ClientID %>').href;
                return false;
            }
            return true;
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-form">
            <div class="top"></div>
            <div class="middle">
                <asp:Label id="ErrorMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                <label for="txtLogin">Email (логин):<asp:RequiredFieldValidator ID="valRequireUserName" CssClass="req" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="txtLogin" ValidationGroup="Login" /></label>
                <asp:TextBox ID="txtLogin" CssClass="input-text" onkeydown="keyPress(event);" runat="server"></asp:TextBox>
                <label for="txtPassword">Пароль:<asp:RequiredFieldValidator ID="valRequirePassword" CssClass="req" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="txtPassword" ValidationGroup="Login" /></label>
                <asp:TextBox ID="txtPassword" CssClass="input-text" onkeydown="keyPress(event);" runat="server" TextMode="Password"></asp:TextBox>
                <asp:Panel ID="pSites" Visible="false" runat="server">
                    <label for="ddlSites">Выберите сайт:<asp:RequiredFieldValidator CssClass="req" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="ddlSites" ValidationGroup="Login" /></label>
                    <asp:DropDownList ID="ddlSites" CssClass="select-text" runat="server" />
                </asp:Panel>
                <div class="group">
                    <asp:CheckBox ID="Persist" runat="server" /><asp:Label ID="Label3" runat="server" AssociatedControlID="Persist">&nbsp;Запомнить меня</asp:Label>
                </div>
                <asp:LinkButton ID="btnLogin" OnClick="btnLogin_Click" ValidationGroup="Login" CssClass="btn" runat="server"><em>&nbsp;</em><span>Войти</span></asp:LinkButton>
            </div>
            <div class="bottom"></div>
        </div>
    </form>
</body>
</html>