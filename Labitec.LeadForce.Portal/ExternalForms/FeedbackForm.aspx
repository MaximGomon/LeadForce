<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedbackForm.aspx.cs" Inherits="Labitec.LeadForce.Portal.ExternalForms.FeedbackForm" %>
<%@ Register TagPrefix="uc" TagName="SelectCategoryControl" Src="~/Shared/UserControls/SelectCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="LoggedAs" Src="~/Shared/UserControls/LoggedAs.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="<%# ResolveUrl("~/App_Themes/Default/forms/feedbackform.css") %>" type="text/css" enableviewstate="false" />    
    <script src="<%#ResolveUrl("~/Scripts/jquery-1.9.0.min.js")%>" type="text/javascript"></script>
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
    <script src="<%# ResolveUrl("~/Scripts/jquery.tmpl.js")%>" type="text/javascript"></script>
    <script src="<%# ResolveUrl("~/Scripts/Search.js")%>" type="text/javascript"></script>    
    <script src="<%#ResolveUrl("~/Scripts/easyXDM.min.js")%>" type="text/javascript"></script>
</head>
<body>    
    <form id="form1" runat="server">   
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>
        <telerik:RadAjaxManager runat="server"/>
        <asp:UpdateProgress ID="PageUpdateProgress" runat="server">
            <ProgressTemplate>                
                <div class="ajax-loader"></div>
            </ProgressTemplate>
        </asp:UpdateProgress>        
        <telerik:RadScriptBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">
                    var currentHeight = 0;
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
//                    $(window).load(function () {
//                        leadForceSearch.handlerPath = '<%= ResolveUrl("~/Handlers/ActivityRibbon.aspx") %>';
//                        if ($('#plThirdStep').length > 0) 
//                            leadForceSearch.input($('#searchText').val());
//                    });
//                    function KeyUp(e, value) { var evtobj = window.event ? event : e; if (!evtobj.altKey && !evtobj.ctrlKey && !evtobj.shiftKey) leadForceSearch.input(value);}
//                    var width = 0;var height = 0;                    
//                    function pageLoad() {
//                        window.setInterval(publishSize, 100);
//                        if ($('#plThirdStep').length > 0) {                            
//                            leadForceSearch.input(searchText);
//                        }
//                    }
//                    function publishSize() {                        
//                        if (window.location.hash.length == 0) return;
//                        var frameId = getFrameId();
//                        if (frameId == '') return;
//                        if (height != $(".container").height() || width != $(".container").width()) {
//                            height = $(".container").height();
//                            width = $(".container").width();                            
//                            var hostUrl = window.location.hash.substring(1);
//                            hostUrl += '#frameId=' + frameId;
//                            hostUrl += '&height=' + height;
//                            hostUrl += '&width=' + width;
//                            window.top.location = hostUrl;
//                        }
//                    }

//                    function getFrameId() {var qs = parseQueryString(window.location.href);var frameId = qs["frameId"];var hashIndex = frameId.indexOf('#');if (hashIndex > -1) {frameId = frameId.substring(0, hashIndex);} return frameId; }
//                    function parseQueryString(url) { url = new String(url);var queryStringValues = new Object(),querystring = url.substring((url.indexOf('?') + 1), url.length),querystringSplit = querystring.split('&');for (i = 0; i < querystringSplit.length; i++) {var pair = querystringSplit[i].split('='),name = pair[0],value = pair[1];queryStringValues[name] = value;} return queryStringValues;}
                </script>
        </telerik:RadScriptBlock>
        
        <telerik:RadAjaxPanel runat="server" ID="rapWrapper">
            <telerik:RadScriptBlock runat="server">
                <script type="text/javascript">
                    var searchText = '<%= searchText.Text %>';
                </script>
            </telerik:RadScriptBlock>                            
                <asp:HiddenField runat="server" ID="hfPublicationType" ClientIDMode="Static" Value="" />
                <asp:HiddenField runat="server" ID="hfSiteId" ClientIDMode="Static" Value="" />
                <asp:Panel runat="server" ID="plContainer" ClientIDMode="Static" CssClass="container clearfix">
                    <div class="title">
                        <h1>Обратная связь</h1>
                    </div>
                    <div id="steps">
                        <asp:Panel runat="server" ID="plFirstStep" Visible="false">
                            <h2>Что вы хотите?</h2>                
                            <ul class="publication-types-list">
                                <asp:ListView runat="server" ID="lvPublicationTypes">
                                    <ItemTemplate>
                                        <li><asp:Image runat="server" ImageUrl='<%# WebCounter.BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(_siteId, "tbl_PublicationType") + Eval("Logo") %>' /><asp:LinkButton runat="server" ID="lbtnPublicationType" ClientIDMode="AutoID" Text='<%# Eval("TextAdd") %>' CommandArgument='<%# Eval("ID") %>' OnClick="lbtnPublicationType_OnClick" /></li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="plAddPublicationContainer" CssClass="add-publication-container" Width="500px">
                            <telerik:RadTabStrip ID="rtsPublicationTypes" runat="server" Skin="Windows7" 
                            CausesValidation="false" PerTabScrolling="true" ScrollChildren="true" ScrollButtonsPosition="Right" AutoPostBack="true" OnTabClick="rtsPublicationTypes_OnTabClick" />                        
                            <asp:Panel runat="server" ID="plSecondStep" CssClass="one-step">
                                        <asp:Literal runat="server" ID="lrlPublicationTypeTitle" />                    
                                        <div class="row">
                                            <label>Заголовок:</label>
                                            <asp:TextBox runat="server" ID="searchText" ClientIDMode="Static" CssClass="input-text" Width="360px" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="searchText" CssClass="required" ValidationGroup="groupAddPublication" Text="*" ErrorMessage='Укажите заголовок'/>
                                        </div>
                                        <div class="row">
                                            <label>Комментарий:</label>
                                            <asp:TextBox runat="server" ID="txtComment" ClientIDMode="Static" CssClass="area-text" TextMode="MultiLine" Width="360px" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComment" CssClass="required" ValidationGroup="groupAddPublication" Text="*" ErrorMessage='Укажите комментарий'/>
                                            <div class="hide-from-public">
                                                <asp:CheckBox runat="server" ID="chxIsHideFromPublic" Text="Скрыть с публичного портала" />                                    
                                            </div>
                                        </div>                            
                                        <div class="row">
                                            <label>Категория:</label>
                                            <uc:SelectCategoryControl runat="server" SelectDefault="true" ID="sccPublicationCategory" ClientIDMode="Static" CategoryType="Publication" ShowEmpty="true" CssClass="select-text" ValidationGroup="groupAddPublication" Width="372px" />
                                        </div>                                                        
                                        <div class="row">
                                            <label>Файл:</label>
                                            <telerik:RadAsyncUpload ID="rauFile" Width="250px" runat="server" MaxFileSize="5242880" 
                                                        AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                                            Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
                                        </div>                            
                                        <br/>
                                        <div class="buttons clearfix">
                                            <asp:LinkButton runat="server" ID="lbtnSecondStepBack" CssClass="cancel" Text="&laquo;Назад" OnClick="lbtnSecondStepBack_OnClick" />                                        
			                                <asp:LinkButton ID="lbtnSecondStepNext" ClientIDMode="Static" OnClick="lbtnSecondStepNext_OnClick" CssClass="btn margin-left" ValidationGroup="groupAddPublication" runat="server">
			                                    <em>&nbsp;</em><span>Далее</span>
			                                </asp:LinkButton>                                        
		                                </div>                                
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="plThirdStep" ClientIDMode="Static" runat="server" CssClass="publications-ribbon" Visible="false">                        
                            <div id="search-result-container" style="display: none">
                                <h3>Результаты поиска:</h3>
                                <div id="search-result">
                                </div>   
                                <div id="more-result">
                                    <a href="javascript:;" onclick="leadForceSearch.more();" class="link">Еще <span></span></a>
                                </div>
                            </div>
                            <script id="search-item-template" type="text/html">
                                <div class="search-item" id="search{%= ID %}">
                                    <h4><a href="javascript:;" onclick="leadForceSearch.getPublication(this, '{%= ID %}')">{%= Title %}</a></h4>
                                    <p>{%= Noun %}</p>
                                </div>
                            </script>
                            <asp:Panel runat="server" ID="plThirdStepButtons" CssClass="buttons clearfix" Visible="false">
                                <asp:LinkButton runat="server" ID="lbtnThirdStepBack" CssClass="cancel" Text="&laquo;Назад" OnClick="lbtnThirdStepBack_OnClick" />                                                        
			                    <asp:LinkButton ID="lbtnThirdStepNext" ClientIDMode="Static" OnClick="lbtnThirdStepNext_OnClick" CssClass="btn margin-left" ValidationGroup="groupAddPublication" runat="server">
			                        <em>&nbsp;</em><span>Далее</span>
			                    </asp:LinkButton>                            
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="plFourthStep" Visible="false" CssClass="step">
                            <uc:LoggedAs runat="server" ID="ucLoggedAs" OnUserAuthorized="ucLoggedAs_OnUserAuthorized" IsPopup="false" />
                            <div class="buttons clearfix">
                                <asp:LinkButton runat="server" ID="lbtnFourthStepBack" CssClass="cancel" Text="&laquo;Назад" OnClick="lbtnFourthStepBack_OnClick" />
                            </div>
                        </asp:Panel>       
                        <asp:Panel runat="server" ID="plSuccess" Visible="false" CssClass="step">
                            <div class="success">
                                Запрос успешно добавлен в систему.
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="publication-text" class="step" style="display: none">
                        <h3></h3>
                        <p></p>                        
                        <a href="javascript:;" class="link" onclick="$('#publication-text').hide();$('#steps').show();">&laquo;Назад</a>
                    </div>
                    <div class="clear"></div>
                    <div class="copyright clearfix">
                        <div style="float: left"><asp:HyperLink runat="server" ID="hlPortal" Target="_blank" CssClass="link">Посмотреть все записи&raquo;</asp:HyperLink></div>
                        <div style="float: right">Разработано на технологии <a href="http://www.leadforce.ru" target="_blank" class="link">LeadForce</a></div>
                    </div>
                </asp:Panel>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
