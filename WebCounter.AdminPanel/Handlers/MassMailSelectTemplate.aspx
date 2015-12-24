<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MassMailSelectTemplate.aspx.cs" Inherits="WebCounter.AdminPanel.Handlers.MassMailSelectTemplate" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%= ResolveUrl("~/App_Themes/Default/AdminPanel.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function returnToParent(id, title) {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            oArg.siteActionTemplateId = id;
            oArg.siteActionTemplateTitle = title;

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
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvSiteActionTemplates" 
                                runat="server" 
                                OnPageIndexChanging="gvSiteActionTemplates_PageIndexChanging" 
                                OnSorting="gvSiteActionTemplates_Sorting"
                                OnSorted="gvSiteActionTemplates_Sorted"
                                AutoGenerateColumns="False" 
                                Width="700" 
                                AllowPaging="true" 
                                PageSize="5" 
                                CssClass="grid"  
                                GridLines="None"   
                                AllowSorting="true"            
                                >    
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="200px" HeaderText="Название шаблона сообщения" HeaderStyle-CssClass="first" ItemStyle-CssClass="first" SortExpression="Title">
                            <ItemTemplate>
                                <%# Eval("Title").ToString()%>      
                                <asp:HiddenField ID="hdnContactID" runat="server" Value='<%# Eval("ID").ToString()%>'/>              
                            </ItemTemplate>  
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="200px" HeaderText="Тип сообщения" SortExpression="tbl_ActionTypes.Title">
                            <ItemTemplate>
                                <%# Eval("tbl_ActionTypes.Title").ToString()%>                 
                            </ItemTemplate>  
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="200px" HeaderText="Тема сообщения" SortExpression="MessageCaption">
                            <ItemTemplate>
                                <%# Eval("MessageCaption").ToString()%>                 
                            </ItemTemplate>  
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Действия" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="last" ItemStyle-CssClass="last">
                            <ItemTemplate>
                                <a href='#' onclick='returnToParent("<%# Eval("ID").ToString()%>", "<%# Eval("Title").ToString()%>"); return false;' title="Выбрать">Выбрать</a>    
                            </ItemTemplate>
                        </asp:TemplateField>     
                    </Columns>
                    <EmptyDataTemplate>
                        Нет данных.
                    </EmptyDataTemplate>
                    <PagerSettings Mode="Numeric" PreviousPageText="Предыдущая" NextPageText="Следующая" />
                    <PagerStyle CssClass="grid-pager" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>