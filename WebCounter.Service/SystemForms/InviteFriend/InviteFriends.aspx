<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InviteFriends.aspx.cs" Inherits="WebCounter.Service.SystemForms.InviteFriend.InviteFriends" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="<%# ResolveUrl("~/SystemForms/InviteFriend/css/styles.css") %>" type="text/css" enableviewstate="false" />    
    <script src="<%#ResolveUrl("~/SystemForms/Shared/Scripts/jquery-1.9.0.min.js")%>" type="text/javascript"></script>
    <script src="<%#ResolveUrl("~/SystemForms/Shared/Scripts/easyXDM.min.js")%>" type="text/javascript"></script>
    <link href='<%# ResolveUrl("~/SystemForms/Shared/Skins/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"/>
    <asp:UpdateProgress ID="PageUpdateProgress" runat="server">
        <ProgressTemplate>                
            <div class="ajax-loader"></div>
        </ProgressTemplate>
    </asp:UpdateProgress> 
    
        <script type="text/javascript">     
            function ValidateTextBox(source, args) {
                if (args.Value == '') {
                    $('#' + source.controltovalidate).parent().attr('class', 'row required');
                    args.IsValid = false;
                } else {
                    $('#' + source.controltovalidate).parent().attr('class', 'row');
                    args.IsValid = true;
                }
            }
            function ValidateEmailTextBox(source, args) {
                if (args.Value == '' || !ValidateEmail(args.Value)) {
                    $('#' + source.controltovalidate).parent().attr('class', 'row required');
                    args.IsValid = false;
                } else {
                    $('#' + source.controltovalidate).parent().attr('class', 'row');
                    args.IsValid = true;
                }
            }
            function ValidateEmail(email) {
                var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/ ;
                if (!emailReg.test(email)) return false;
                else return true;           
            }
        </script>        
        <script type="text/javascript">
            var currentHeight = 1;
            var currentWidth = 0;
            function PostMessage() {
                var socket = new easyXDM.Socket({
                    onReady: function () {
                        var mainForm = '<%= form1.ClientID %>';
                        var height = document.body.clientHeight || document.body.offsetHeight || document.body.scrollHeight;
                        var heightStr = 'height@@' + height + '&&';
                        var width = $(".container").width();
                        var widthStr = 'width@@' + width + '&&';
                        var backgroundStr = 'backgroundcolor@@' + document.getElementById(mainForm).style.backgroundColor + '&&';
                        if (currentHeight != height || currentWidth != width) {
                            var msg = heightStr + widthStr + backgroundStr;
                            currentHeight = height;
                            currentWidth = width;
                            socket.postMessage(msg);
                        }
                    }
                });
            }
            
            function pageLoad() { window.setInterval(PostMessage, 100); }
            
        /*var width = 0; var height = 0;
            function pageLoad() { window.setInterval(publishSize, 100);}
            function publishSize() {
                if (window.location.hash.length == 0) return;
                var frameId = getFrameId();
                if (frameId == '') return;
                if (height != $(".container").height() || width != $(".container").width()) {
                    height = $(".container").height();
                    width = $(".container").width();
                    var hostUrl = window.location.hash.substring(1);
                    hostUrl += '#frameId=' + frameId;
                    hostUrl += '&height=' + (height - 15);
                    hostUrl += '&width=' + width;
                    window.top.location = hostUrl;
                }
            }

            function getFrameId() { var qs = parseQueryString(window.location.href); var frameId = qs["frameId"]; var hashIndex = frameId.indexOf('#'); if (hashIndex > -1) { frameId = frameId.substring(0, hashIndex); } return frameId; }
            function parseQueryString(url) { url = new String(url); var queryStringValues = new Object(), querystring = url.substring((url.indexOf('?') + 1), url.length), querystringSplit = querystring.split('&'); for (i = 0; i < querystringSplit.length; i++) { var pair = querystringSplit[i].split('='), name = pair[0], value = pair[1]; queryStringValues[name] = value; } return queryStringValues; }*/
        </script>

    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
        <asp:Panel runat="server" CssClass="container" ID="plContainer">        
            <h1>Пригласи друга</h1>        
                <asp:Panel runat="server" ID="plForm">
                    <asp:Literal runat="server" ID="lrlHeaderInstruction" />
                    <div class="row">
                        <label>ФИО<span class="required">*</span></label>
                        <asp:TextBox ClientIDMode="Static" runat="server" CssClass="input-text" ID="txtFullName" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage=""
                            ControlToValidate="txtFullName" ClientValidationFunction="ValidateTextBox" ValidateEmptyText="True"></asp:CustomValidator>
                    </div>
                    <div class="row">
                        <label>E-mail<span class="required">*</span></label>
                        <asp:TextBox ClientIDMode="Static" runat="server" CssClass="input-text" ID="txtEmail" />                    
                            <asp:CustomValidator ID="CustomValidator5" runat="server" ErrorMessage=""
                            ControlToValidate="txtEmail" ClientValidationFunction="ValidateEmailTextBox" ValidateEmptyText="True"></asp:CustomValidator>
                    </div>
                    <div class="row">
                        <label>Комментарий для друга</label>
                        <asp:TextBox ClientIDMode="Static" runat="server" TextMode="MultiLine" CssClass="area-text" ID="txtComment" />
                    </div>
                    <div class="row">
                        <label>Имя друга<span class="required">*</span></label>
                        <asp:TextBox ClientIDMode="Static" runat="server" CssClass="input-text" ID="txtFriendName" />
                        <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage=""
                            ControlToValidate="txtFriendName" ClientValidationFunction="ValidateTextBox" ValidateEmptyText="True"></asp:CustomValidator>
                    </div>
                    <div class="row">
                        <label>E-mail друга<span class="required">*</span></label>
                        <asp:TextBox ClientIDMode="Static" runat="server" CssClass="input-text" ID="txtFriendEmail" />                    
                        <asp:CustomValidator ID="CustomValidator6" runat="server" ErrorMessage=""
                        ControlToValidate="txtFriendEmail" ClientValidationFunction="ValidateEmailTextBox" ValidateEmptyText="True"></asp:CustomValidator>
                    </div>
                    <asp:LinkButton ID="lbtnInviteFriend" ClientIDMode="Static" CssClass="btn" runat="server" OnClick="lbtnInviteFriend_OnClick">
                        <em>&nbsp;</em><span><asp:Literal runat="server" ID="lrlButtonText" Text="Пригласить друга!" /></span>
		            </asp:LinkButton>
                    <asp:Literal runat="server" ID="lrlFooterInstruction" />
                </asp:Panel>
                <asp:Panel runat="server" ID="plSuccess" Visible="false">
                    <div class="success">
                        Ваш друг <asp:Literal runat="server" ID="lrlFriendName" /> приглашен. <asp:LinkButton runat="server" ID="lbtnAddAnotherFriend" Text="Добавить еще друга" CssClass="link" OnClick="lbtnAddAnotherFriend_OnClick" />
                    </div>
                </asp:Panel>        
                <asp:Panel runat="server" ID="plMessage" Visible="false">
                    <p style="height: 50px">Некорректно настроена форма. Укажите шаблон сообщения.</p>
                </asp:Panel>
         </asp:Panel>            
        </ContentTemplate>
        </asp:UpdatePanel>     
    </form>
</body>
</html>