<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="WebCounter.AdminPanel.Help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="<%# ResolveUrl("~/App_Themes/Default/Help.css") %>" type="text/css" enableviewstate="false" />
    
        <script src="<%#ResolveUrl("~/Scripts/jquery-1.5.2.min.js")%>" type="text/javascript"></script>
        <script type="text/javascript" language="javascript">
            function rwHelpSize() {
                var windowWidth = $(window).width();
                var windowHeight = $(window).height();

                $("#txtSearchQuery").width(windowWidth - 134);                
                
                $("#RAD_SPLITTER_RadSplitter1").height(windowHeight - 62);
                $("#RadPane1").height(windowHeight - 123);
                $("#RAD_SPLITTER_PANE_CONTENT_RadPane1").height(windowHeight - 62);
                $("#RadPane2").height(windowHeight - 123);
                $("#RAD_SPLITTER_PANE_CONTENT_RadPane2").height(windowHeight - 62);
            }
            function EndRequestHandler(sender, args) {

                rwHelpSize();
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ajaxLoadingPanel" Skin="Windows7" runat="server"></telerik:RadAjaxLoadingPanel>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
        <telerik:RadToolBar
                    ID="mainToolBar" runat="server"
                    style="z-index:90001" Width="100%" Height="100%"
                    Skin="Office2007" EnableRoundedCorners="true" EnableShadows="true" OnButtonClick="mainToolBar_ButtonClick">
                    <Items>
                        <telerik:RadToolBarButton>
                            <ItemTemplate>
                                <asp:TextBox ID="txtSearchQuery" runat="server" ClientIDMode="Static"/>
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                        <telerik:RadToolBarDropDown Text="Поиск">
                            <Buttons>
                            </Buttons>
                        </telerik:RadToolBarDropDown>
                    </Items>
        </telerik:RadToolBar>
        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Vertical">
            <telerik:RadPane ID="RadPane1" runat="server" Width="220px" BackColor="#D3E1F1">
                <telerik:RadTreeView runat="server" ShowLineImages="false" ID="radPublicationCategoryTreeView" OnNodeExpand="radPublicationCategoryTreeView_NodeExpand" OnNodeClick="radPublicationCategoryTreeView_NodeClick"/>
                <hr/>
                <telerik:RadTreeView runat="server" ID="rtwSearchResult" ShowLineImages="false" OnNodeClick="radPublicationCategoryTreeView_NodeClick" ClientIDMode="Static"/>
                <telerik:RadTreeView runat="server" ID="rtwRelated" ShowLineImages="false" OnNodeClick="radPublicationCategoryTreeView_NodeClick" ClientIDMode="Static"/>
                <div class="related">
            </telerik:RadPane>
            <telerik:RadSplitBar ID="RadSplitBar2" runat="server"></telerik:RadSplitBar>
            <telerik:RadPane ID="RadPane2" runat="server">
                                <div class="help">
                                    <h1><asp:Literal runat="server" ID="lTitle"/></h1>
                                    <telerik:RadBinaryImage ID="radBinaryImage" runat="server"/>
                                    <span class="noun"><asp:Literal runat="server" ID="lNoun"/></span>
                                    <br/><br/>
                                    <asp:Literal runat="server" ID="lText"/>
                                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
        </ContentTemplate></asp:UpdatePanel>
    </form>
</body>
</html>
