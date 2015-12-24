<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentComments.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Shared.ContentComments" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>

<telerik:RadScriptBlock runat="server">    
    <script src="<%= ResolveUrl("~/Scripts/jquery.tmpl.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/Comments.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var Comments = new LeadForceComment('<%= ResolveUrl("~/Handlers/Comments.aspx") %>', '<%= ResolveUrl("~/Handlers/CommentsFileUpload.ashx") %>', '<%= ContentId %>', '<%= (int)CommentType %>', '<%= EnableHtmlCommentEditor.ToString().ToLower() %>', '<%= ucHtmlEditor.ClientID %>');
        var destinationWindow;
    </script>

    <script src="<%= ResolveUrl("~/Scripts/ajaxfileupload.js")%>" type="text/javascript"></script>    
    <%--<script src="<%= ResolveUrl("~/Scripts/jquery.textarea-expander.js")%>" type="text/javascript"></script>--%>
    <script src="<%= ResolveUrl("~/Scripts/textarea-expander.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Comments.InitCommentInputs();
            if ('<%= ExpandComments %>' == 'True')
                Comments.SelectComments();
        });
        function pageLoad() {
            Comments.InitCommentInputs();
            destinationWindow = $find('<%= destinationsRadWindow.ClientID %>');
        }        
    </script>
</telerik:RadScriptBlock>

<telerik:RadCodeBlock runat="server">
<% if (EnableHtmlCommentEditor) { %>
<div id="htmlEditorContainer">
    <div id="htmlEditor" style="display: none;float:left;width: 92%">
        <uc:HtmlEditor runat="server" Module="Requirements" Width="100%" ID="ucHtmlEditor" CallFunctionOnKeyUp="Comments.HTMLOnKeyUp(e, editor);"  CallFunctionOnKeyPress="Comments.HTMLOnKeyPress(e, editor);" />
    </div>
</div>
<% } %>
</telerik:RadCodeBlock>

<div class="comments-container">    
        <div class="item" id='<%= ContentId %>'>
            <%= string.IsNullOrEmpty(OfficialComment) ? "<div class=\"off-comment\" style=\"display:none\"><h4>Официальный ответ:</h4><p></p></div>" : "<div class=\"off-comment\"><h4>Официальный ответ:</h4><p>" + OfficialComment.ToHtml() + "</p></div>"%>    
            <div class="stats clearfix" id='<%= "stats" + ContentId %>'>
                <ul>            
                    <li class="comments"><span class="bg">&nbsp;</span><a href="javascript:;"  onclick='Comments.SelectComments()'>Посмотреть все комментарии</a> (<span class="count"><%= CommentsCount %></span>)</li>
                </ul>
            </div>
            <br/>
            <div class="comments-ribbon" style="display:none" id='<%= "cr" + ContentId %>'></div>    
            <div class="leave-comment">
                <div class="destinations clearfix">                    
                    <b>Для: <span class="items"></span></b>
                    <a href="javascript:;" title="Очистить" class="clear-reply" onclick="Comments.ClearReply()">
                        <asp:Image runat="server" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Очистить"/>
                    </a>
                </div>
                <div class="internal-comment">
                    <asp:CheckBox runat="server" Text="Внутренний комментарий" ClientIDMode="Static" ID="chxIsInternal" EnableViewState="false" />
                </div>
                <div id="editorContainer" class="clearfix">
                    <asp:TextBox runat="server" ID="textEditor" ClientIDMode="Static" TextMode="MultiLine" CssClass="expander" style="float: left">Оставить комментарий...</asp:TextBox>                    
                    <div style="float:left;margin: 3px 0 0 5px">               
                        <asp:HyperLink runat="server" ToolTip="Адресат" CssClass="show-destination" NavigateUrl="javascript:;" onclick="destinationWindow.show();$('#chxIsInternal').removeAttr('checked')" ImageUrl="~/App_Themes/Default/images/icoDestination.png"/>
                        <asp:HyperLink runat="server" ToolTip="Добавить вложение" NavigateUrl="javascript:;" onclick="Comments.ShowHideUpload();" ImageUrl="~/App_Themes/Default/images/icoAttachment.png"/>
                        <% if (EnableHtmlCommentEditor) { %>
                        <asp:HyperLink runat="server" ToolTip="HTML редактор" NavigateUrl="javascript:;" onclick="Comments.ShowHideAddEditor();" ImageUrl="~/App_Themes/Default/images/icoHtmlEditBig.png"/>
                        <% } %>
                    </div>
                    <div class="clear"></div>
                    <i>Нажмите ctrl+enter, чтобы опубликовать свой комментарий.</i>
                    <div class="upload-file">
                        <input id='<%= "fu" + ContentId %>' type="file" name='<%= "fu" + ContentId %>' />
                    </div>
                </div>                
            </div>
        </div>
    <script id="comment-template" type="text/html">
        <div class="comment-item" id="comment{%= ID %}">
            {% if (DestinationUserFullName != null && DestinationUserFullName != '') { %}
                <h4><a href="javascript:;" onclick="Comments.ReplyToUser(this)" rel="{%= UserID %}">{%= UserFullName %}</a> => {%= DestinationUserFullName %}{% if (IsInternal == true) { %} (Внутренний){% } %}</h4>
            {% } else { %}
                <h4><a href="javascript:;" onclick="Comments.ReplyToUser(this)" rel="{%= UserID %}">{%= UserFullName %}</a> {% if (IsInternal == true) { %} (Внутренний){% } %}</h4>
            {% } %}            
            <div class="comment-text">{%= Comment %}</div>
            {% if (VirtualFileName != null && VirtualFileName != '') { %}
                <div class="downloadFile">
                    <a href='{%= VirtualFileName %}' target="_blank">Скачать файл</a>
                </div>
            {% } %}
            <ul class="comment-operations clearfix">
                <li>{%= FormattedDate %}</li>
                <li class="like"><a href="javascript:;" onclick="Comments.LikeComment(this, '{%= ID %}')">{% if (ContactLike == 1) { %}Больше не нравится{% } else { %}Мне нравится{% } %}</a><span class="bg"></span><span class="count">{%= SumLike %}</span></li>
                <li class="reply"><a href="javascript:;" title="Ответить" onclick="Comments.Reply('{%= ID %}')" >Ответить</a></li>
                {% if (IsOfficialAnswer != true) { %}
                        <li class="official-answer"><a href="javascript:;" onclick="Comments.CheckOfficialAnswer(this, '{%= ID %}')">Официальный ответ</a></li>
                {% } %}
                <li class="edit"><a href="javascript:;" title="Редактировать" onclick="Comments.EditComment('{%= ID %}')"><span>Редактировать</span></a></li>
                {% if ($context.options.IsHtmlEnabled == 'true') { %}
                    <li class="html-edit"><a href="javascript:;" onclick="Comments.EditHTMLComment('{%= ID %}')"><span>Редактировать HTML</span></a></li>
                {% } %}
                <li class="delete"><a href="javascript:;" title="Удалить" onclick="Comments.DeleteComment('{%= ID %}')"><span>Удалить</span></a></li>
            </ul>                    
        </div>
    </script>
    <script id="destination-template" type="text/html">
        <span><a href="javascript:;" id="{%= UserID %}">{%= UserFullName %}</a></span>
    </script>
</div>

<telerik:RadWindow runat="server" Title="Выбор адресата" Width="436px" Height="340px" ID="destinationsRadWindow" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>              
        <div class="radwindow-popup-inner bottom-buttons">
            <ul class="destination-list">
                <asp:Repeater runat="server" ID="rprDesitnations">
                    <ItemTemplate>
                        <li><a href="javascript:;" rel='<%# Eval("UserID") %>' onclick="Comments.ReplyToUser(this);destinationWindow.close();" ><%# Eval("UserFullName") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>            
            <div class="buttons">
                <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="destinationWindow.close();" />
            </div>
        </div>
    </ContentTemplate>
</telerik:RadWindow>