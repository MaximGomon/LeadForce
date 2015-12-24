<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MassMailSelectContacts.aspx.cs" Inherits="WebCounter.AdminPanel.Handlers.MassMailSelectContacts" %>
<%--<%@ Register TagPrefix="uc" TagName="Tags" Src="~/UserControls/Tags.ascx" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%= ResolveUrl("~/App_Themes/Default/AdminPanel.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%#ResolveUrl("~/Scripts/jquery-1.5.2.min.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function returnToParent() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            oArg.selected = true;

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager" EnableScriptGlobalization="true" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <div style="float:left; margin-bottom: 10px;">
            <asp:LinkButton ID="BtnInclude" OnClick="BtnInclude_Click" CssClass="btn" runat="server"><em>&nbsp;</em><span>Включить в список</span></asp:LinkButton>
        </div>
        <div class="clear"></div>
        
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <%--<uc:Tags ID="ucTags" TagType="Users" OnSelected="ucTags_Selected" Behaviors="None" runat="server" />--%>
                </td>
                <td valign="top" style="padding: 10px 0 0 10px;">
                    <asp:UpdatePanel ID="upContacts" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div class="filter">
                                Готовность к продаже
                                <asp:DropDownList ID="ddlReadyToSell" runat="server" OnSelectedIndexChanged="ddlReadyToSell_SelectedIndexChanged" AutoPostBack="true" CssClass="select-text" style="width:150px;"></asp:DropDownList>
                                Важность
                                <asp:DropDownList ID="ddlPriorities" runat="server" OnSelectedIndexChanged="ddlPriorities_SelectedIndexChanged" AutoPostBack="true" CssClass="select-text" style="width:80px;"></asp:DropDownList>
                                Статус
                                <asp:DropDownList ID="ddlStatusFilter" runat="server" OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged" AutoPostBack="true" CssClass="select-text" style="width:150px;"></asp:DropDownList>
                            </div>
                            <div class="clear"></div>

                            <asp:GridView ID="gvContacts"
                                            DataKeyNames="ID" 
                                            runat="server" 
                                            OnPageIndexChanging="gvContacts_PageIndexChanging" 
                                            AutoGenerateColumns="False" 
                                            Width="800" 
                                            AllowPaging="true" 
                                            PageSize="15" 
                                            OnSorting="gvContacts_Sorting"
                                            OnSorted="gvContacts_Sorted"
                                            CssClass="grid"
                                            GridLines="None"
                                            AllowSorting="true" 
                                            >    
                                <Columns>
                                    <asp:TemplateField HeaderText="Ф.И.О." ItemStyle-ForeColor="#1272f2" SortExpression="UserFullName" HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                                        <ItemTemplate>
                                            <%# Server.HtmlEncode(Eval("UserFullName") == null ? "" : Eval("UserFullName").ToString())%>                    
                                        </ItemTemplate>          
                                    </asp:TemplateField>        
                                    <asp:TemplateField HeaderText="E-mail" SortExpression="Email">
                                        <ItemTemplate>
                                            <%# Server.HtmlEncode(Eval("Email") == null ? "" : Eval("Email").ToString())%>                    
                                        </ItemTemplate>        
                                    </asp:TemplateField>         
                                    <asp:TemplateField HeaderText="Готовность к продаже" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" SortExpression="tbl_ReadyToSell.Title">
                                        <ItemTemplate>
                                            <%# Server.HtmlEncode(Eval("tbl_ReadyToSell.Title") == null ? "" : Eval("tbl_ReadyToSell.Title").ToString())%>                    
                                        </ItemTemplate>        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Важность" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" SortExpression="tbl_Priorities.Title">
                                        <ItemTemplate>
                                            <%# Server.HtmlEncode(Eval("tbl_Priorities.Title") == null ? "" : Eval("tbl_Priorities.Title").ToString())%>                    
                                        </ItemTemplate>        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Статус" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" SortExpression="tbl_Status.Title" HeaderStyle-CssClass="last" ItemStyle-CssClass="last">
                                        <ItemTemplate>
                                            <%# Eval("tbl_Status.Title").ToString()%>               
                                        </ItemTemplate>        
                                    </asp:TemplateField>     
                                </Columns>
                                <EmptyDataTemplate>
                                    Нет данных.
                                </EmptyDataTemplate>
                                <SortedAscendingHeaderStyle CssClass="grid-asc" Font-Bold="true" />
                                <SortedDescendingHeaderStyle CssClass="grid-desc" Font-Bold="true" />
                                <SortedAscendingCellStyle BorderColor="Red" />
                                <PagerSettings Mode="Numeric" PreviousPageText="Предыдущая" NextPageText="Следующая" />
                                <PagerStyle CssClass="grid-pager" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>