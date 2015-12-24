<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResultContainer.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Shared.SearchResultContainer" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            leadForceSearch.handlerPath = '<%= ResolveUrl("~/Handlers/ActivityRibbon.aspx") %>';
        });
    </script>
</telerik:RadScriptBlock>
<asp:HiddenField runat="server" ID="hfPublicationKind" ClientIDMode="Static" Value="" />
<div id="search-result-container">
    <h3>Результаты поиска:</h3>
    <div id="search-result">
    </div>   
    <div id="more-result">
        <a href="javascript:;" onclick="leadForceSearch.more();">Еще <span></span></a>
    </div>
</div>
<asp:Panel runat="server" ID="plNotSelectAnswer" Visible="false">
    <script id="search-item-template" type="text/html">
        <div class="search-item" id="search{%= ID %}">
            <h4><a href="{%= PublicationUrl %}">{%= Title %}</a></h4>
            <p>{%= Noun %}</p>
        </div>
    </script>
</asp:Panel>
<asp:Panel runat="server" ID="plIsSelectAnswer" Visible="false">
    <script id="search-item-template" type="text/html">
        <div class="search-item" id="search{%= ID %}">
            <h4><a href="{%= PublicationUrl %}">{%= Title %}</a> <a class="RadButton RadButton_Windows7 rbSkinnedButton" href="javascript:;" onclick="leadForceSearch.toComment('{%= PortalUrl %}')"><input type="button" value="В комментарий" class="rbDecorated"></a></h4>
            <p>{%= Noun %}</p>
        </div>
    </script>
</asp:Panel>