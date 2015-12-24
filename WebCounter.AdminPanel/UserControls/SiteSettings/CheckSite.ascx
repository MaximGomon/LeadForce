<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckSite.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteSettings.CheckSite" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script src="<%= ResolveUrl("~/Scripts/highlight.pack.js")%>" type="text/javascript"></script>    
    <script type="text/javascript">
        function <%= ClientID %>_CheckDomains() {            
            $('#<%= "startCheckMessage" + ClientID %>').show();
            $('#<%= "result" + ClientID %>').hide();
            eval($("#<%=lbtnSendRequest.ClientID%>").attr('href'));            
        }
        function pageLoad() {
            $('pre code').each(function (i, e) { hljs.highlightBlock(e); });                                    
        }
        $(window).load(function () {
            <% if (IsCheckOnLoad) { %>
                <%= ClientID %>_CheckDomains();
            <% } %>
        });
    </script>
</telerik:RadScriptBlock>
<uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />
<telerik:RadCodeBlock runat="server">
<div id='<%= "startCheckMessage" + ClientID %>' style="display: none">
    <table>
        <tr>
            <td>Идет проверка домена. Подождите.</td>
            <td><div class="ajax-loader-bar"></div></td>
        </tr>
    </table>
</div>
</telerik:RadCodeBlock>
<div id='<%= "result" + ClientID %>' style="display: none">
<asp:ListView runat="server" ID="lvResult">
    <LayoutTemplate>
        <table>
            <tr runat="server" id="itemPlaceholder" />
        </table>
    </LayoutTemplate>        
    <ItemTemplate>
        <tr class="check-list-item">
            <td width="24px" valign="top">
                <span class='<%# "status " + Eval("ErrorTypeClass") %>'></span>
            </td>
            <td>
                <%# Eval("Message") %>
            </td>
        </tr>
    </ItemTemplate>
</asp:ListView>    
</div>
<asp:LinkButton runat="server" ID="lbtnSendRequest" OnClick="lbtnSendRequest_OnClick" style="display: none" />