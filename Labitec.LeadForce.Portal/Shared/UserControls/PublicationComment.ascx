<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationComment.ascx.cs" Inherits="Labitec.LeadForce.Portal.Shared.UserControls.PublicationComment" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var handlerPath = '<%= ResolveUrl("~/Handlers/ActivityRibbon.aspx") %>';
        var fileUploadHandlerPath = '<%= ResolveUrl("~/Handlers/CommentsFileUpload.ashx") %>';
    </script>
    <script src="<%= ResolveUrl("~/Scripts/jquery.tmpl.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/ajaxfileupload.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/ActivityRibbon.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/jquery.textarea-expander.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            InitCommentInputs();
        });
        function pageLoad() {
            InitCommentInputs();
        }
    </script>
</telerik:RadCodeBlock>
<div class="item" id='<%= PublicationId %>'>
    <ul class="operations clearfix">
        <li class="like"><a href="javascript:;" onclick='LikePublication(this, "<%= PublicationId %>")'><%= ContactLike == 1 ? "Больше не нравится" : "Мне нравится"%></a></li>
    </ul>
    <div class="stats clearfix" id='<%= "stats" + PublicationId %>'>
        <ul>
            <li class="like"><span class="count"><%= SumLike %></span> <span class="users"><%= ContactLikeUserText %></span> это нравится</li>
            <li class="comments"><span class="bg">&nbsp;</span><a href="javascript:;"  onclick='SelectComments("<%= PublicationId %>")'>Посмотреть все комментарии</a> (<span class="count"><%= CommentsCount %></span>)</li>
        </ul>
    </div>
    <div class="comments-ribbon" style="display:none" id='<%= "cr" + PublicationId %>'></div>
    <div class="leave-comment">
        <textarea class="expander">Оставить комментарий...</textarea>
        <input type="hidden" class="hf-publicationid" value='<%= PublicationId %>' />
        <a title="Добавить вложение" href="javascript:;" onclick="ShowHideUpload(this);">
            <img src='<%= ResolveUrl("~/App_Themes/Default/images/icoAttachment.png") %>' alt="Добавить вложение" />
        </a>
        <i>Нажмите ctrl+enter, чтобы опубликовать свой комментарий.</i>
        <div class="upload-file">
            <input id='<%= "fu" + PublicationId %>' type="file" name='<%= "fu" + PublicationId %>' />
        </div>
    </div>
</div>
<script id="comment-template" type="text/html">
    <div class="comment-item" id="comment{%= ID %}">
        <h3>{%= UserFullName %}</h3>
        <p>{%= Comment %}</p>
        {% if (VirtualFileName != null && VirtualFileName != '') { %}
            <div class="downloadFile">
                <a href='{%= VirtualFileName %}' target="_blank">Скачать файл</a>
            </div>
        {% } %}
        <ul class="comment-operations clearfix">
            <li>{%= FormattedDate %}</li>
            <li class="like"><a href="javascript:;" onclick="LikeComment(this, '{%= ID %}', '{%= PublicationID %}')">{% if (ContactLike == 1) { %}Больше не нравится{% } else { %}Мне нравится{% } %}</a><span class="bg"></span><span class="count">{%= SumLike %}</span></li>                
        </ul>        
    </div>
</script>