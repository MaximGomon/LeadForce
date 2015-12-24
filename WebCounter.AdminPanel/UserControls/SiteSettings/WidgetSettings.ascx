<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WidgetSettings.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteSettings.WidgetSettings" %>

<div class="widget-settings clearfix">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function RadDockPositionChanged(dock) {
                ajaxManager = $find("<%= radAjaxManager.ClientID %>");
                ajaxManager.ajaxRequest('Move');
            }
            function RadDockClientCommand(sender, eventArgs) {
                ajaxManager = $find("<%= radAjaxManager.ClientID %>");                
                ajaxManager.ajaxRequest(eventArgs.command.get_name());                
            }
            var listBoxDragInProgress = false;
            var treeViewDragInProgress = false;            
            function onTreeViewMouseOut(sender, args) {
                if (listBoxDragInProgress) {
                    args.get_node().unselect();
                }
            }            
            function onTreeViewMouseOver(sender, args) {
                if (listBoxDragInProgress) {
                    args.get_node().select();
                }
            }
            function onTreeViewDragStart(sender, args) {                
                treeViewDragInProgress = true;
            }
            function onTreeViewDropping(sender, args) {                
                treeViewDragInProgress = false;
                document.body.style.cursor = "";
                var target = args.get_htmlElement();                
                var target = isOverElement(target, "<%= radDockZone1.ClientID %>") || isOverElement(target, "<%= radDockZone2.ClientID %>")
                    || isOverElement(target, "<%= radDockZone3.ClientID %>") || isOverElement(target, "<%= radDockZone4.ClientID %>");
                if (!target) {
                    args.set_cancel(true);                    
                    return;
                }                
                args.set_htmlElement(target);
            }
            function isOverElement(target, id) {
                while (target) {
                    if (target.id == id) break;
                    target = target.parentNode;
                }
                return target;
            }
            function checkDropTargets(target) {
                if (isOverElement(target, "<%= rtwWidgets.ClientID %>") || isOverElement(target, "<%= radDockZone1.ClientID %>") || isOverElement(target, "<%= radDockZone2.ClientID %>")
                     || isOverElement(target, "<%= radDockZone3.ClientID %>") || isOverElement(target, "<%= radDockZone4.ClientID %>")) {
                    document.body.style.cursor = "";
                } else {                    
                    document.body.style.cursor = "no-drop";
                }
            }
            function onTreeViewDragging(sender, args) {
                checkDropTargets(args.get_htmlElement());
            }
        </script>        
    </telerik:RadScriptBlock>
    <div class="row">
        <label>Модуль:</label>
        <telerik:RadComboBox ID="rcbModule" OnSelectedIndexChanged="rcbModule_OnSelectedIndexChanged" MaxHeight="200px" runat="server" CssClass="select-text" EnableLoadOnDemand="true" AutoPostBack="true" skin="Labitec" Width="234px" EnableEmbeddedSkins="false" ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None"/>        
    </div>
    <asp:Panel runat="server" ID="plLeftColumn">        
        <table>
            <tr>
                <td width="270px" valign="top">
                    <h4>Все виджеты</h4>
                    <br/>
                    <telerik:RadTreeView runat="server" ID="rtwWidgets"                        
                        Skin="Windows7"  
                        Height="300px"              
                        Width="240px"                                                
                        BorderWidth="1px"
                        BorderColor="#C1DBFC"                                                
                        EnableDragAndDrop="true" 
                        OnClientMouseOver="onTreeViewMouseOver"
                        OnClientMouseOut="onTreeViewMouseOut" 
                        OnClientNodeDragStart="onTreeViewDragStart"                        
                        OnClientNodeDragging="onTreeViewDragging"
                        OnClientNodeDropping="onTreeViewDropping" 
                        OnNodeDrop="rtwWidgets_OnNodeDrop"                                              
                        OnNodeDataBound="rtwWidgets_OnNodeDataBound"
                        >                            
                        </telerik:RadTreeView>            
                </td>
                <td valign="top">
                    <h4>Выбранные виджеты</h4>
                    <br/>
                    <telerik:RadDockLayout runat="server" ID="radDockLayout" OnLoadDockLayout="radDockLayout_OnLoadDockLayout">
                        <telerik:RadDockZone runat="server" ID="radDockZone1" Orientation="Horizontal" Skin="Windows7" Width="200px" MinHeight="300px">    
                        </telerik:RadDockZone>
                        <telerik:RadDockZone runat="server" ID="radDockZone2" Orientation="Horizontal" Skin="Windows7" Width="200px" MinHeight="300px">    
                        </telerik:RadDockZone>
                        <telerik:RadDockZone runat="server" ID="radDockZone3" Orientation="Horizontal" Skin="Windows7" Width="200px" MinHeight="300px">    
                        </telerik:RadDockZone>
                        <telerik:RadDockZone runat="server" ID="radDockZone4" Orientation="Horizontal" Skin="Windows7" Width="200px" MinHeight="300px">    
                        </telerik:RadDockZone>
                    </telerik:RadDockLayout>
                </td>
            </tr>
        </table>
    </asp:Panel>    
</div>