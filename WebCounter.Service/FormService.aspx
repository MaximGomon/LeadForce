<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormService.aspx.cs" Inherits="WebCounter.Service.FormService" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="<%#ResolveUrl("~/SystemForms/Shared/Scripts/easyXDM.min.js")%>" type="text/javascript"></script>
    <link href='<%# ResolveUrl("~/style.css")  %>' rel="stylesheet" type="text/css" />
    <telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function toggleForm(className) {
            var div = document.getElementsByClassName(className)[0];
            if (div.className.indexOf('hide') == -1)
                div.className = div.className + ' hide';
            else
                div.className = div.className.replace(' hide', '');

            PostMessage();
        }

        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }

            try {
                if (!Page_IsValid) {
                    if (document.getElementById("LFErrorMessage")) {
                        if (document.getElementById("LFErrorMessageTextBlock")) {
                            document.getElementById("LFErrorMessageTextBlock").innerHTML = document.getElementById("LFErrorMessage").innerHTML;
                            document.getElementById("LFErrorMessageTextBlock").style.display = 'block';
                        } else {
                            alert(document.getElementById("LFErrorMessage").innerHTML);
                        }
                    }

                    Validate();

                    PostMessage();
                    return false;
                } else if (document.getElementById("LFErrorMessageTextBlock")) {
                    document.getElementById("LFErrorMessageTextBlock").style.display = 'none';
                }
            } catch(e) {

            } 


            PostMessage();
            return true;
        }

        function Validate() {
            var validation = Page_ClientValidate();
            if (!validation) {
                for (var i = 0; i < Page_Validators.length; i++) {
                    if (!Page_Validators[i].isvalid) {
                        if (Page_Validators[i].controltovalidate.indexOf("rcbDictionary") != -1) {
                            document.getElementById(Page_Validators[i].controltovalidate).className += " error";
                        }
                        else {
                            var el = $find(Page_Validators[i].controltovalidate);
                            if (el.get_element().className.indexOf("rdfd") == -1) {
                                el.get_styles().EnabledStyle[1] += " error";
                                el.get_styles().HoveredStyle[1] += " error";
                                el.get_styles().EmptyMessageStyle[1] += " error";
                                el.updateCssClass();
                            } else {
                                el = $find(Page_Validators[i].controltovalidate + "_dateInput");
                                el.get_styles().EnabledStyle[1] += " error";
                                el.get_styles().HoveredStyle[1] += " error";
                                el.get_styles().EmptyMessageStyle[1] += " error";
                                el.updateCssClass();
                            }
                        }
                    }
                }
            }
            return validation;
        }

        var currentHeight = 1;
        function PostMessage() {
            var socket = new easyXDM.Socket({
                onReady: function () {
                    var mainForm = '<%= form1.ClientID %>';
                    var height = document.body.clientHeight || document.body.offsetHeight || document.body.scrollHeight;
                    var heightStr = 'height@@' + height + '&&';
                    var backgroundStr = 'backgroundcolor@@' + document.getElementById(mainForm).style.backgroundColor + '&&';
                    if (currentHeight != height) {
                        currentHeight = height;
                        var msg = heightStr + backgroundStr;
                        if (document.getElementById('hdnUrl').value != '') {
                            var urlStr = 'url@@' + document.getElementById('hdnUrl').value;
                            msg = msg + urlStr + '&&';
                            document.getElementById('hdnUrl').value = '';
                        }
                        if (document.getElementById('hdnSuccessMessage').value != '') {
                            var successmessage = 'successmessage@@' + document.getElementById('hdnSuccessMessage').value;
                            msg = msg + successmessage + '&&';
                            document.getElementById('hdnSuccessMessage').value = '';
                        }
                        if (document.getElementById('hdnYandexGoals').value != '') {
                            var yandexgoals = 'yandexgoals@@' + document.getElementById('hdnYandexGoals').value;
                            msg = msg + yandexgoals + '&&';
                            document.getElementById('hdnYandexGoals').value = '';
                        }
                        socket.postMessage(msg);
                    }
                }
            });
        }

        function pageLoad() { window.setInterval(PostMessage, 100); }

        function ClientSelectedIndexChanged(sender, args) {
            var css = document.getElementById(sender.get_id()).className;
            if (css.indexOf("error") != -1)
                document.getElementById(sender.get_id()).className = css.replace("error", "");
        }

        function TextboxFocus(textbox) {
            var tb = $find(textbox.id);
            tb.get_styles().EnabledStyle[1] = tb.get_styles().EnabledStyle[1].replace("error", "");
            tb.get_styles().HoveredStyle[1] = tb.get_styles().HoveredStyle[1].replace("error", "");
        }

        function PopupOpening(sender, args) {
            var di = $find(sender.get_id() + "_dateInput");
            di.get_styles().EnabledStyle[1] = di.get_styles().EnabledStyle[1].replace("error", "");
            di.get_styles().HoveredStyle[1] = di.get_styles().HoveredStyle[1].replace("error", "");
        }
    </script>
    </telerik:RadScriptBlock>
    <asp:Literal runat="server" ID="lrlResourcesHead"/>
</head>
<body>
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <asp:HiddenField ID="hdnUrl" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hdnSuccessMessage" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hdnYandexGoals" ClientIDMode="Static" runat="server" />
        
        <telerik:RadFormDecorator ID="FormDecorator" DecorationZoneID="container" DecoratedControls="CheckBoxes" runat="server" />
        
        <div id="container" class="container">
            <asp:Panel ID="pnlFormContainer" runat="server"></asp:Panel>
            <asp:LinkButton ID="lbtnSave" OnClientClick="if (!ValidatePage()) return false;" OnClick="OnClick" CssClass="btn" ValidationGroup="vg" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>                
        </div>
        
        <asp:Literal runat="server" ID="lrlResourcesBody"/> 
    </form>
</body>
</html>