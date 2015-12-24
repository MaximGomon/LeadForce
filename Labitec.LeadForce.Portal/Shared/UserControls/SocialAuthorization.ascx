<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialAuthorization.ascx.cs" Inherits="Labitec.LeadForce.Portal.Shared.UserControls.SocialAuthorization" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function VKLogin() {
            var leftvar = (screen.width-700)/2;
            var topvar = (screen.height-400)/2;
            window.open('<%= VkontakteAPIOAuthUrl %>', "Vkontakte", "menubar=0,resizable=0,left=" + leftvar + ",top=" + topvar + ",width=700,height=400");
            return false;
        }
        function FBLogin() {
            var leftvar = (screen.width - 950) / 2;
            var topvar = (screen.height - 600) / 2;
            window.open('<%= FacebookAPIOAuthUrl %>', "Facebook", "menubar=0,resizable=0,left=" + leftvar + ",top=" + topvar + ",width=950,height=500");
            return false;
        }
    </script>
</telerik:RadScriptBlock>

<div class="social-authorization">
    <ul class="clearfix">
        <li><label>Войти через:</label></li>
        <li><a href="javascript:;" id="vk-login-button" onclick="VKLogin();" ><img src='<%= ResolveUrl("~/App_Themes/Default/images/btnVkontakte.png") %>' alt="Vkontakte" /></a></li>
        <li><a href="javascript:;" id="fb-login-button" onclick="FBLogin();"><img src='<%= ResolveUrl("~/App_Themes/Default/images/btnFacebook.png") %>' alt="Facebook" /></a></li>
    </ul>
</div>