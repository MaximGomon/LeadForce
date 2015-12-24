<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuConstructor.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.MenuConstructor" %>

<telerik:RadCodeBlock runat="server">
    <script type="text/javascript">
        function onClientContextMenuShowing(sender, args) {
            var treeNode = args.get_node();
            treeNode.set_selected(true);
            //enable/disable menu items
            setMenuItemsState(args.get_menu().get_items(), treeNode);
        }

        function onClientContextMenuItemClicking(sender, args) {
            /*var menuItem = args.get_menuItem();
            var treeNode = args.get_node();
            menuItem.get_menu().hide();

            switch (menuItem.get_value()) {
            case "Rename":
            treeNode.startEdit();
            break;
            case "Delete":
            /*var result = confirm("Вы действительно хотите удалить " + treeNode.get_text() + "?");
            args.set_cancel(!result);
            break;
            }*/
        }

        //this method disables the appropriate context menu items
        function setMenuItemsState(menuItems, treeNode) {
            for (var i = 0; i < menuItems.get_count(); i++) {
                var menuItem = menuItems.getItem(i);
                switch (menuItem.get_value()) {
                    case "Rename":
                        if (treeNode.get_level() == 1 || ('<%= IsSiteProfiles.ToString().ToLower() %>' == 'true' && treeNode.get_level() == 2) || treeNode.get_level() == 3) {
                            menuItem.set_enabled(true);
                        }
                        else {
                            menuItem.set_enabled(false);
                        }
                        break;
                }
            }
        }

        function HideAddTabTooltip() {
            var radToolTip = $find("<%= rttAddTab.ClientID %>");
            if (radToolTip != null) radToolTip.hide();
        }
        
        function HideEditMenuTooltip() {
            var radToolTip = $find("<%= rttEditMenu.ClientID %>");
            if (radToolTip != null) radToolTip.hide();
        }

        function OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            var commandName = button.get_commandName();

            var result;
            switch (commandName) {
                case "ResetMenu":
                    result = confirm("По действию существующие настройки будут удалены. Продолжить?");
                    args.set_cancel(!result);
                    break;
                case "CopyMenu":
                    result = confirm("По действию существующие настройки будут удалены. Продолжить?");
                    args.set_cancel(!result);
                    break;
            }
        }
    </script>
</telerik:RadCodeBlock>


<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td valign="top" width="250px">
            <h3>Модули</h3>
            <div class="tree-modules">
                <telerik:RadTreeView ID="rtvTreeModules" EnableDragAndDrop="true" OnNodeDrop="rtvTreeModules_OnNodeDrop" Skin="Windows7" runat="server">
				    <DataBindings>
					    <telerik:RadTreeNodeBinding Expanded="True" />
				    </DataBindings>
                </telerik:RadTreeView>
                <asp:Literal ID="ltrlEmptyModules" Text="Нет доступных модулей" Visible="false" runat="server" />
            </div>
        </td>
        <td valign="top">
            <h3>Рабочие столы</h3>
            <telerik:RadToolTip ID="rttAddTab" Modal="true" HideEvent="ManualClose" ManualCloseButtonText="Закрыть" RelativeTo="BrowserWindow" Position="Center" ShowEvent="FromCode" HideDelay="0" runat="server">
                <div class="tooltip-addtab">
	                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                ValidationGroup="vgAddTab" />
                    <asp:HiddenField ID="hdnMenuID" runat="server" />
                    <div class="row">
                        <asp:Label ID="Label1" AssociatedControlID="txtMenuTabTitle" runat="server">Название:</asp:Label>
                        <telerik:RadTextBox ID="txtMenuTabTitle" CssClass="input-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMenuTabTitle" Display="Static" ValidationGroup="vgAddTab" ErrorMessage="Вы не ввели название" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <asp:Label ID="Label2" AssociatedControlID="txtMenuName" runat="server">Системное имя:</asp:Label>
                        <telerik:RadTextBox ID="txtMenuName" CssClass="input-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMenuName" Display="Static" ValidationGroup="vgAddTab" ErrorMessage="Вы не ввели системное имя" runat="server">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="txtMenuName" ValidationExpression="[a-zA-Z0-9_-]+" Display="Static" ErrorMessage="Допустимые символы 'a-z', 'A-Z', '0-9', '-', '_'" ValidationGroup="vgAddTab" runat="server">*</asp:RegularExpressionValidator>
                    </div>
		            <div class="buttons">
			            <asp:LinkButton ID="btnAddMenuTab" OnClick="btnAddMenuTab_OnClick" CssClass="btn" ValidationGroup="vgAddTab" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
			            <a href="#" class="cancel" onclick="HideAddTabTooltip(); return false;">Отмена</a>
		            </div>
                </div>
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="rttEditMenu" Modal="true" HideEvent="ManualClose" ManualCloseButtonText="Закрыть" RelativeTo="BrowserWindow" Position="Center" ShowEvent="FromCode" HideDelay="0" runat="server">
                <div class="tooltip-addtab">
                    <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                ValidationGroup="vgEditMenu" />
                    <div class="row">
                        <asp:Label runat="server" AssociatedControlID="txtMenuItemTitle">Название:</asp:Label>
                        <asp:TextBox runat="server" ID="txtMenuItemTitle" CssClass="input-text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtMenuItemTitle" Display="Static" ValidationGroup="vgEditMenu" ErrorMessage="Вы не ввели название" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <asp:Label ID="Label3" runat="server" AssociatedControlID="ddlModuleEditionAction">Операция:</asp:Label>
                        <asp:DropDownList runat="server" ID="ddlModuleEditionAction" CssClass="select-text" />                        
                    </div>
                    <div class="buttons">
                        <asp:LinkButton ID="lbtnEditMenu" CssClass="btn" ValidationGroup="vgEditMenu" OnClick="lbtnEditMenu_OnClick" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                        <a href="#" class="cancel" onclick="HideEditMenuTooltip(); return false;">Отмена</a>
                    </div>
                </div>                
            </telerik:RadToolTip>

            <telerik:RadToolBar ID="rtbMenu" OnButtonClick="OnButtonClick" OnClientButtonClicking="OnClientButtonClicking" Skin="Windows7" Width="228px" runat="server">
                <Items>
                    <telerik:RadToolBarButton ToolTip="Создать" Value="AddTab" CommandName="AddTab" ImageUrl="~/App_Themes/Default/images/icoToolbarAdd.png" runat="server" />
                    <telerik:RadToolBarButton ToolTip="Изменить" Value="Rename" CommandName="Rename" ImageUrl="~/App_Themes/Default/images/icoToolbarRename.png" Enabled="false" runat="server" />
                    <telerik:RadToolBarButton ToolTip="Удалить" Value="Delete" CommandName="Delete" ImageUrl="~/App_Themes/Default/images/icoToolbarDelete.png" Enabled="false" runat="server" />
                    <telerik:RadToolBarButton IsSeparator="true" />
                    <telerik:RadToolBarButton ToolTip="По умолчанию" Value="ResetMenu" CommandName="ResetMenu" ImageUrl="~/App_Themes/Default/images/icoToolbarReset.png" runat="server" />
                    <telerik:RadToolBarButton IsSeparator="true" />
                    <telerik:RadToolBarDropDown ToolTip="Создать на основе" ImageUrl="~/App_Themes/Default/images/icoToolbarCopy.png" runat="server" />
                </Items>
            </telerik:RadToolBar>
            <div class="tree-menu">
                <telerik:RadTreeView ID="rtvTreeMenu"
                                        OnContextMenuItemClick="rtvTreeMenu_OnContextMenuItemClick"
                                        OnClientContextMenuItemClicking="onClientContextMenuItemClicking"
                                        OnClientContextMenuShowing="onClientContextMenuShowing"
                                        OnNodeDataBound="rtvTreeMenu_OnNodeDataBound"
                                        EnableDragAndDrop="true"
                                        EnableDragAndDropBetweenNodes="true"
                                        OnNodeDrop="rtvTreeMenu_OnNodeDrop"
                                        OnNodeClick="rtvTreeMenu_OnNodeClick"
                                        Skin="Windows7"
                                        runat="server">
				    <DataBindings>
					    <telerik:RadTreeNodeBinding Expanded="True" />
				    </DataBindings>
                    <ContextMenus>
                        <telerik:RadTreeViewContextMenu ID="RadTreeViewContextMenu1" Skin="Windows7" runat="server">
                            <Items>
                                <telerik:RadMenuItem Text="Изменить" Value="Rename" ImageUrl="~/App_Themes/Default/images/icoRename.gif" />
                                <telerik:RadMenuItem Text="Удалить" Value="Delete" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" />
                            </Items>
                        </telerik:RadTreeViewContextMenu>
                        <telerik:RadTreeViewContextMenu ID="ctxMenuAdd" Skin="Windows7" runat="server">
                            <Items>
                                <telerik:RadMenuItem Text="Создать" Value="AddTab" ImageUrl="~/App_Themes/Default/images/icoCtxAdd.gif" />
                            </Items>
                        </telerik:RadTreeViewContextMenu>
                    </ContextMenus>
                </telerik:RadTreeView>
            </div>
        </td>
    </tr>
</table>