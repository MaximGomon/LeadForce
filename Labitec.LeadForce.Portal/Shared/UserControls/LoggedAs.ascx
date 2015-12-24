<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoggedAs.ascx.cs" Inherits="Labitec.LeadForce.Portal.Shared.UserControls.LoggedAs" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/Shared/UserControls/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="SocialAuthorization" Src="~/Shared/UserControls/SocialAuthorization.ascx" %>
<%@ Register Assembly="skmValidators" Namespace="skmValidators" TagPrefix="skm" %>

<telerik:RadScriptBlock runat="server">
    <script src="<%=ResolveUrl("~/Scripts/jquery.simplemodal.js")%>" type="text/javascript"></script>
    <script type="text/javascript">        
        var loginPopupTitle = '<%= PopupTitle %>';
        var needLogin = <%= NeedLogin.ToString().ToLower() %>;        
        var siteId = '<%= SiteId %>';
        var portalSettingsId = '<%= PortalSettingsId %>';
        function NeedLogin() {                        
            $('#authorize-modal-content').modal();
            $("#simplemodal-container").appendTo($("#login-popup-container"));
        }
    </script>    
</telerik:RadScriptBlock>
<telerik:RadScriptBlock runat="server">
<script type="text/javascript">
    var loginTitle = "";
    function ShowReg() {
        $("#login-popup").hide();
        $("#reg-popup").show();
    }    
    function ShowLogin() {
        $("#login-popup").show();
        $("#reg-popup").hide();
        $("#remind-password-popup").hide();        
    }
    function ShowRemindPopup() {
        loginTitle = $("#authorize-modal-content .p-header h3").html();
        $("#authorize-modal-content .p-header h3").html("Восстановление пароля");        
        $("#remind-password-popup").show();
        $("#authorize-modal-content .p-footer").hide();
        $("#authorize-modal-content .p-remind-pass-footer").show();
        $("#login-popup").hide();
    }
    function CloseLoginPopup() {
        window.location.href = window.location.href;
    }
    function RemindBack() {
        ShowLogin();
        $("#authorize-modal-content .p-footer").show();
        $("#authorize-modal-content .p-remind-pass-footer").hide();
        $("#authorize-modal-content .p-header h3").html(loginTitle);
    }
</script>
</telerik:RadScriptBlock>
<div id="login-popup-container">    
<div id="authorize-modal-content">
    <div class="p-header">        
            <asp:Literal runat="server" ID="lrlPopupTitle"/>        
    </div>
    <div class="p-body">        
            <div class="authorize-popup">
                <div id="login-popup">
                    <telerik:RadAjaxPanel ID="rapLogin" runat="server">
                        <div class="caption">
                            <a href="javascript:;" onclick="ShowReg()">Зарегистрируйтесь</a> или Войдите
                        </div>
                        <uc:NotificationMessage runat="server" ID="ucLoginNotificationMessage" MessageType="Warning" />
                        <div class="row">
                            <label>Электронный адрес:<asp:RequiredFieldValidator ID="valRequireUserName" CssClass="required" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="txtEmail" ValidationGroup="groupLogin" /></label>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="input-text" />
                        </div>
                        <div class="row">
                            <label>Пароль:</label>
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="input-text" TextMode="Password" />
                        </div>
                        <br/>
	                    <div class="buttons clearfix">
		                    <asp:LinkButton ID="lbtnLogin" OnClick="lbtnLogin_OnClick" CssClass="btn" ValidationGroup="groupLogin" runat="server"><em>&nbsp;</em><span>Войти</span></asp:LinkButton>
		                    <a href="javascript:;" class="link" onclick="ShowRemindPopup()">Напомнить пароль</a>
	                    </div>
                    </telerik:RadAjaxPanel>
                </div>
                <div id="reg-popup">
                    <telerik:RadAjaxPanel ID="rapReg" runat="server">
                        <asp:Panel runat="server" ID="plRegistrtion">
                            <div class="caption">
                                Зарегистрируйтесь или <a href="javascript:;" onclick="ShowLogin()">Войдите</a>
                            </div>
                            <uc:NotificationMessage runat="server" ID="ucRegNotificationMessage" MessageType="Warning" />
                            <div class="row">
                                <label>Имя:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="required" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="txtRegName" ValidationGroup="groupReg" /></label>
                                <asp:TextBox runat="server" ID="txtRegName" CssClass="input-text" />
                            </div>
                            <div class="row">
                                <label>Электронный адрес:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="required" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="txtRegEmail" ValidationGroup="groupReg" /></label>
                                <asp:TextBox runat="server" ID="txtRegEmail" CssClass="input-text" />
                            </div>                        
                            <div class="row">
                                <label>Пароль:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="required" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="txtRegPassword" ValidationGroup="groupReg" /></label>
                                <asp:TextBox runat="server" ID="txtRegPassword" CssClass="input-text" TextMode="Password" ValidationGroup="groupReg" />
                            </div>                        
                            <br/>
	                        <div class="buttons clearfix">
		                        <asp:LinkButton ID="lbtnReg" OnClick="lbtnReg_OnClick" CssClass="btn" ValidationGroup="groupReg" runat="server"><em>&nbsp;</em><span>Зарегистрироваться</span></asp:LinkButton>
	                        </div>
                            <div class="agreement">
                                <label>
                                    <asp:CheckBox runat="server" ID="chxIsAgree" />Я согласен с <asp:HyperLink runat="server" ID="hlAgreement">пользовательским соглашением</asp:HyperLink>
                                    <skm:CheckBoxValidator ID="AgreementValidator" runat="server" ControlToValidate="chxIsAgree" ForeColor="#B91212" Display="Dynamic" ErrorMessage="" ValidationGroup="groupReg" MustBeChecked="true">*</skm:CheckBoxValidator>
                                </label>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="plRegClose" Visible="false">
                            <uc:NotificationMessage runat="server" Text="Вы успешно зарегистрировались на портале. Вам высланы инструкции для активации учетной записи." MessageType="Success" />
                            <telerik:RadCodeBlock runat="server">
                                <% if (IsPopup) { %>
                                    <a href="javascript:;" class="btn" onclick="CloseLoginPopup();"><em>&nbsp;</em><span>Продолжить</span></a>
                                <% } %>
                            </telerik:RadCodeBlock>
                            <br/>
                        </asp:Panel>
                    </telerik:RadAjaxPanel>
                </div>
                <div id="remind-password-popup">
                    <telerik:RadAjaxPanel ID="rapRemind" runat="server">
                        <uc:NotificationMessage runat="server" ID="ucRemindNotificationMessage" MessageType="Warning" />
                        <asp:Panel runat="server" ID="plRemindContainer">
                            <label>Введите ваш электронный адрес:<asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="required" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="txtRemindPasswordEmail" ValidationGroup="groupRemindPassword" /></label>
                            <div class="clearfix">
                                <asp:TextBox runat="server" ID="txtRemindPasswordEmail" CssClass="input-text" />
                                <asp:LinkButton ID="lbtnRemindPassword" OnClick="lbtnRemindPassword_OnClick" CssClass="btn" runat="server" ValidationGroup="groupRemindPassword"><em>&nbsp;</em><span>Продолжить</span></asp:LinkButton>
                                <a href="javascript:;" onclick="RemindBack();" class="link remind-cancel">Отмена</a>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="plRemindPasswordClose" Visible="false">
                            <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                <% if (IsPopup) { %>
                                    <a href="javascript:;" class="btn" onclick="CloseLoginPopup();"><em>&nbsp;</em><span>Продолжить</span></a>
                                <% } %>
                            </telerik:RadCodeBlock>
                            <br/>
                        </asp:Panel>                        
                    </telerik:RadAjaxPanel>
                </div>
            </div>        
    </div>
    <div class="p-footer">
        <uc:SocialAuthorization runat="server" ID="ucSocialAuthorization" />
    </div>
    <div class="p-remind-pass-footer"></div>
</div>
</div>
<asp:Panel runat="server" ID="plLogged" Visible="false">
    <telerik:RadCodeBlock runat="server">
        <div class="login-btn-container">
            <div class="logged-user"><%= Login %></div>
            <asp:LinkButton runat="server" CssClass="btn" ID="lbtnLogout" OnClick="lbtnLogout_OnClick"><em>&nbsp;</em><span>Выйти</span></asp:LinkButton>            
        </div>
    </telerik:RadCodeBlock>
</asp:Panel>
<asp:Panel runat="server" ID="plAnonymous" Visible="false">
    <div class="login-btn-container">
        <a id="loginBtn" class="btn" href="javascript:;" onclick="NeedLogin()"><em>&nbsp;</em><span>Войти</span></a>
    </div>
</asp:Panel>