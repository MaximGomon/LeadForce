<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationsRibbon.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.PublicationsRibbon" %>
<%@ Register TagPrefix="labitec" Namespace="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.Code" Assembly="Labitec.LeadForce.Portal" %>
<%@ Register TagPrefix="uc" TagName="PublicationFilter" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationFilter.ascx" %>

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

<uc:PublicationFilter runat="server" ID="ucPublicationFilter" />
<div class="publications-ribbon">
    <asp:HiddenField runat="server" ID="hfPublicationFilter" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfPublicationType" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfPublicationCategory" ClientIDMode="Static" />

    <telerik:RadListView runat="server" ID="rlvPublications">
        <LayoutTemplate>
            <div id="publicationsContainer">
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="item" id='<%# Eval("ID") %>'>
                <div class="logo">
                    <img src='<%# Eval("PublicationTypeLogo") %>' alt='<%# Eval("Title") %>'/>
                </div>
                <div class="publication-container">
                    <h3 class="title"><a href='<%# Eval("PublicationUrl") %>'><%# Eval("Title") %></a></h3>
                    <p class="description"><%# Eval("Text") %></p>
                    <%# string.IsNullOrEmpty((string)Eval("OfficialComment")) ? "<div class=\"off-comment\" style=\"display:none\"><h4>Официальный ответ:</h4><p></p></div>" : "<div class=\"off-comment\"><h4>Официальный ответ:</h4><p>" + Eval("OfficialComment") + "</p></div>"%>
                    <%# string.IsNullOrEmpty((string)Eval("FileName")) ? string.Empty : "<div class=\"downloadFile\"><a href='" + Eval("FileName") + "' target=\"_blank\">Скачать файл</a></div>" %>
                    <ul class="operations clearfix">
                        <li class="like"><a href="javascript:;" onclick='LikePublication(this, "<%# Eval("ID") %>")'><%# Eval("ContactLike") != null && int.Parse(Eval("ContactLike").ToString()) == 1 ? "Больше не нравится" : "Мне нравится"%></a><span>&nbsp;</span></li>
                        <li class="comment"><a href="javascript:;" onclick='LeaveComment("<%# Eval("ID") %>")'>Комментировать</a><span>&nbsp;</span></li>
                        <li class="date"><%# Eval("FormattedDate")%></li>                        
                    </ul>
                    <ul class="operations clearfix">
                        <li class="category"><%# Eval("Category")%><span>&nbsp;</span></li>
                        <li>Статус: <span class="status"><%# Eval("Status")%></span></li>
                    </ul>
                    <div class="stats clearfix" id='<%# "stats" + Eval("ID") %>'>
                        <ul>
                            <li class="like"><span class="count"><%# Eval("SumLike") %></span> <span class="users"><%# Eval("ContactLikeUserText")%></span> это нравится</li>
                            <li class="comments"><span class="bg">&nbsp;</span><a href="javascript:;" onclick='SelectComments("<%# Eval("ID") %>")'>Посмотреть все комментарии</a> (<span class="count"><%# Eval("CommentsCount")%></span>)</li>
                        </ul>
                    </div>
                    <div class="comments-ribbon" style="display:none" id='<%# "cr" + Eval("ID") %>'></div>
                    <div class="leave-comment" id='<%# "lc" + Eval("ID") %>'>
                        <textarea class="expander">Оставить комментарий...</textarea>
                        <input type="hidden" class="hf-publicationid" value='<%# Eval("ID") %>' />
                        <a title="Добавить вложение" href="javascript:;" onclick="ShowHideUpload(this);">
                            <img src='<%# ResolveUrl("~/App_Themes/Default/images/icoAttachment.png") %>' />
                        </a>
                        <i>Нажмите ctrl+enter, чтобы опубликовать свой комментарий.</i>
                        <div class="upload-file">
                            <input id='<%# "fu" + Eval("ID") %>' type="file" name='<%# "fu" + Eval("ID") %>' />
                        </div>
                    </div>
                </div>                
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="empty">
                Пусто
            </div>
        </EmptyDataTemplate>
    </telerik:RadListView>    
    <labitec:RadListViewClientBindingExtender runat="server" ID="rlvcbePublications" TargetControlID="rlvPublications">
        <ClientItemTemplate>
            <div class="item" id='{%= ID %}'>
                <div class="logo">
                    <img src='{%= PublicationTypeLogo %}' alt='{%= Title %}'/>
                </div>
                <div class="publication-container">
                    <h3 class="title"><a href="{%= PublicationUrl %}">{%= Title %}</a></h3>
                    <p class="description">{%= Text %}</p>
                    {% if (OfficialComment != null && OfficialComment != '') { %}
                        <div class="off-comment">
                            <h4>Официальный ответ:</h4>
                            <p>{%= OfficialComment %}</p>
                        </div>
                    {% } else { %}
                        <div class="off-comment" style="display:none"><h4>Официальный ответ:</h4><p></p></div>
                    {% } %}                
                    {% if (FileName != null && FileName != '') { %}
                        <div class="downloadFile">
                            <a href='{%= FileName %}' target="_blank">Скачать файл</a>
                        </div>
                    {% } %}
                    <ul class="operations clearfix">    
                        <li class="like"><a href="javascript:;" onclick='LikePublication(this, "{%=  ID %}")'>{% if (ContactLike == 1) { %}Больше не нравится{% } else { %}Мне нравится{% } %}</a><span>&nbsp;</span></li>
                        <li class="comment"><a href="javascript:;" onclick='LeaveComment("{%=  ID %}")'>Комментировать</a><span>&nbsp;</span></li>
                        <li class="date">{%= FormattedDate %}</li>
                    </ul>
                    <ul class="operations clearfix">
                        <li class="category">{%= Category %}<span>&nbsp;</span></li>
                        <li>Статус: {%= Status %}</li>
                    </ul>
                    <div class="stats clearfix" id='stats{%= ID %}'>
                        <ul>
                            <li class="like"><span class="count">{%= SumLike %}</span> <span class="users">{%= ContactLikeUserText %}</span> это нравится</li>
                            <li class="comments"><span class="bg">&nbsp;</span><a href="javascript:;" onclick='SelectComments("{%=  ID %}")'>Посмотреть все комментарии</a> (<span class="count">{%= CommentsCount %}</span>)</li>
                        </ul>
                    </div>
                    <div class="comments-ribbon" style="display:none" id='cr{%= ID %}'>
                    </div>
                    <div class="leave-comment" id='lc{%= ID %}'>
                        <textarea class="expander">Оставить комментарий...</textarea>
                        <input type="hidden" class="hf-publicationid" value='{%= ID %}' />
                        <a title="Добавить вложение" href="javascript:;" onclick="ShowHideUpload(this);">
                            <img src='/App_Themes/Default/images/icoAttachment.png' />
                        </a>
                        <i>Нажмите ctrl+enter, чтобы опубликовать свой комментарий.</i>
                        <div class="upload-file">
                            <input id='fu{%= ID %}' type="file" name='fu{%= ID %}' />
                        </div>
                    </div>
                </div>
            </div>
        </ClientItemTemplate>
        <DataBindingSettings ClientItemContainerID="publicationsContainer" CallBackFunction="InitCommentInputs()" MethodName="GetPublications" EnableAutoLoad="false" />
    </labitec:RadListViewClientBindingExtender>
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
</div>
