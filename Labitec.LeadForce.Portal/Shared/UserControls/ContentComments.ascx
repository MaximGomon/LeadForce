<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentComments.ascx.cs" Inherits="Labitec.LeadForce.Portal.Shared.UserControls.ContentComments" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">    
    <script src="<%= ResolveUrl("~/Scripts/jquery.tmpl.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/Comments.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var Comments = new LeadForceComment('<%= ResolveUrl("~/Handlers/Comments.aspx") %>', '<%= ResolveUrl("~/Handlers/CommentsFileUpload.ashx") %>', '<%= ContentId %>', '<%= (int)CommentType %>');
    </script>

    <script src="<%= ResolveUrl("~/Scripts/ajaxfileupload.js")%>" type="text/javascript"></script>    
    <script src="<%= ResolveUrl("~/Scripts/jquery.textarea-expander.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Comments.InitCommentInputs();
        });
        function pageLoad() {
            Comments.InitCommentInputs();
        }        
    </script>
</telerik:RadScriptBlock>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

</telerik:RadCodeBlock>

<div class="comments-container">    
        <div class="item" id='<%= ContentId %>'>
            <%= string.IsNullOrEmpty(OfficialComment) ? "<div class=\"off-comment\" style=\"display:none\"><h4>Официальный ответ:</h4><p></p></div>" : "<div class=\"off-comment\"><h4>Официальный ответ:</h4><p>" + OfficialComment.ToHtml() + "</p></div>"%>    
            <div class="stats clearfix" id='<%= "stats" + ContentId %>'>
                <ul>            
                    <li class="comments"><span class="bg">&nbsp;</span><a href="javascript:;"  onclick='Comments.SelectComments()'>Посмотреть все комментарии</a> (<span class="count"><%= CommentsCount %></span>)</li>
                </ul>
            </div>            
            <div class="comments-ribbon" style="display:none" id='<%= "cr" + ContentId %>'></div>    
            <div class="leave-comment">
                <div class="destinations">
                    <span>Для [ФИО]: </span>
                    <span class="items"></span>
                </div>
                <div id="editorContainer" class="clearfix">
                    <textarea class="expander" id="textEditor" style="float: left">Оставить комментарий...</textarea>
                    <div style="float:left;margin: 3px 0 0 5px">               
                        <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="Адресат" CssClass="show-destination" NavigateUrl="javascript:;" onclick="Comments.ShowDestinations();" ImageUrl="~/App_Themes/Default/images/icoDestination.png"/>
                        <asp:HyperLink ID="HyperLink2" runat="server" ToolTip="Добавить вложение" NavigateUrl="javascript:;" onclick="Comments.ShowHideUpload();" ImageUrl="~/App_Themes/Default/images/icoAttachment.png"/>                        
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
                <h3>{%= UserFullName %} => {%= DestinationUserFullName %}</h3>                
            {% } else { %}
                <h3>{%= UserFullName %}</h3>
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
            </ul>                    
        </div>
    </script>
    <script id="destination-template" type="text/html">
        <span><a href="javascript:;" id="{%= UserID %}">{%= UserFullName %}</a></span>
    </script>
</div>