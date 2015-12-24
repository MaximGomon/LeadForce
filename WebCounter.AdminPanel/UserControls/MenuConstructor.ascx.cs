using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class MenuConstructor : System.Web.UI.UserControl
    {
        protected RadAjaxManager radAjaxManager = null;
        protected DataManager dataManager = new DataManager();



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid AccessProfileID
        {
            get
            {
                object o = ViewState["AccessProfileID"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["AccessProfileID"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsSiteProfiles
        {
            get
            {
                if (ViewState["IsSiteProfiles"] == null)
                    ViewState["IsSiteProfiles"] = false;

                return (bool)ViewState["IsSiteProfiles"];
            }
            set { ViewState["IsSiteProfiles"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindTreeModules();
                BindTreeMenu();
                BindToolbar();
            }

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rtvTreeModules, rtvTreeMenu);
            radAjaxManager.AjaxSettings.AddAjaxSetting(btnAddMenuTab, rtvTreeMenu);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rtbMenu, rttAddTab);                
            radAjaxManager.AjaxSettings.AddAjaxSetting(rtvTreeMenu, rtbMenu);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rtbMenu, rtvTreeMenu);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rtvTreeMenu, rttAddTab);

            if (IsSiteProfiles)
            {
                radAjaxManager.AjaxSettings.AddAjaxSetting(rtbMenu, rttEditMenu);
                radAjaxManager.AjaxSettings.AddAjaxSetting(rtvTreeMenu, rttEditMenu);
                radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnEditMenu, rtvTreeMenu);
            }
        }



        /// <summary>
        /// Binds the tree modules.
        /// </summary>
        public void BindTreeModules()
        {
            var accessProfile = dataManager.AccessProfile.SelectById(AccessProfileID);
            var accessProfileModules = dataManager.AccessProfileModule.SelectByAccessProfileID(AccessProfileID).Where(a => a.Read).OrderBy(a => a.tbl_Module.Title).ToList();
            var modulesFiltered = new List<tbl_Module>();

            if (accessProfile != null && accessProfile.SiteID != null)
            {
                var site = dataManager.Sites.SelectById((Guid)accessProfile.SiteID);
                if (site.AccessProfileID != null)
                {
                    var siteAccessProfileModules = dataManager.AccessProfileModule.SelectByAccessProfileID((Guid)site.AccessProfileID).Where(a => a.Read).OrderBy(a => a.tbl_Module.Title);
                    foreach (var siteAccessProfileModule in siteAccessProfileModules)
                        if (accessProfileModules.SingleOrDefault(a => a.ModuleID == siteAccessProfileModule.ModuleID) != null)
                            modulesFiltered.Add(new tbl_Module { ID = siteAccessProfileModule.ModuleID, Title = siteAccessProfileModule.tbl_Module.Title, Name = siteAccessProfileModule.tbl_Module.Name });
                }
                else
                {
                    foreach (var accessProfileModule in accessProfileModules)
                        modulesFiltered.Add(new tbl_Module { ID = accessProfileModule.ModuleID, Title = accessProfileModule.tbl_Module.Title, Name = accessProfileModule.tbl_Module.Name });
                }
            }
            else
            {
                foreach (var accessProfileModule in accessProfileModules)
                    modulesFiltered.Add(new tbl_Module { ID = accessProfileModule.ModuleID, Title = accessProfileModule.tbl_Module.Title, Name = accessProfileModule.tbl_Module.Name });
            }

            if (modulesFiltered.Count > 0)
                ltrlEmptyModules.Visible = false;
            else
                ltrlEmptyModules.Visible = true;

            rtvTreeModules.DataSource = modulesFiltered;
            rtvTreeModules.DataFieldID = "ID";
            rtvTreeModules.DataValueField = "ID";
            rtvTreeModules.DataTextField = "Title";
            rtvTreeModules.DataBind();
        }



        /// <summary>
        /// Binds the tree menu.
        /// </summary>
        public void BindTreeMenu()
        {
            var accessProfile = dataManager.AccessProfile.SelectById(AccessProfileID);

            var menu = dataManager.Menu.SelectByAccessProfileID(AccessProfileID).Select(a => new
            {
                a.ID,
                ParentID = a.ParentID = a.ParentID ?? Guid.Empty,
                a.AccessProfileID,
                Title = (!string.IsNullOrEmpty(a.Title)) ? a.Title : a.tbl_Module.Title,
                a.ModuleID
            }).ToList();
            

            var accessProfileModule = dataManager.AccessProfileModule.SelectByAccessProfileID(AccessProfileID);
            var menuFiltered = new List<MenuClass>();
            foreach (var m in menu)
            {
                if (m.ModuleID != null)
                {
                    if (accessProfileModule.SingleOrDefault(a => a.ModuleID == m.ModuleID && a.Read) != null)
                    {
                        if (accessProfile != null && accessProfile.SiteID != null)
                        {
                            var site = dataManager.Sites.SelectById((Guid)accessProfile.SiteID);
                            if (site.AccessProfileID != null)
                            {
                                var siteAccessProfileModules = dataManager.AccessProfileModule.SelectByAccessProfileID((Guid)site.AccessProfileID).SingleOrDefault(a => a.ModuleID == m.ModuleID);
                                if (siteAccessProfileModules != null && siteAccessProfileModules.Read)
                                    menuFiltered.Add(new MenuClass { ID = m.ID, ParentID = m.ParentID, AccessProfileID = m.AccessProfileID, Title = m.Title, ModuleID = m.ModuleID });
                            }
                            else
                                menuFiltered.Add(new MenuClass { ID = m.ID, ParentID = m.ParentID, AccessProfileID = m.AccessProfileID, Title = m.Title, ModuleID = m.ModuleID });
                        }
                        else
                            menuFiltered.Add(new MenuClass { ID = m.ID, ParentID = m.ParentID, AccessProfileID = m.AccessProfileID, Title = m.Title, ModuleID = m.ModuleID });   
                    }
                }
                else
                    menuFiltered.Add(new MenuClass { ID = m.ID, ParentID = Guid.Empty, AccessProfileID = m.AccessProfileID, Title = m.Title, ModuleID = m.ModuleID });
            }

            menuFiltered.Add(new MenuClass { ID = Guid.Empty, ParentID = null, AccessProfileID = AccessProfileID, Title = "Рабочие столы", ModuleID = null });

            rtvTreeMenu.DataSource = menuFiltered;
            rtvTreeMenu.DataFieldID = "ID";
            rtvTreeMenu.DataValueField = "ModuleID";
            rtvTreeMenu.DataFieldParentID = "ParentID";
            rtvTreeMenu.DataTextField = "Title";
            rtvTreeMenu.DataBind();
        }



        /// <summary>
        /// Binds the toolbar.
        /// </summary>
        public void BindToolbar()
        {
            var copyMenu = (RadToolBarDropDown)rtbMenu.Items[6];
            copyMenu.Buttons.Clear();

            if (CurrentUser.Instance.AccessLevelID == 1)
            {
                var accessProfiles = dataManager.AccessProfile.SelectAll(CurrentUser.Instance.SiteID);
                if (accessProfiles != null)
                {
                    foreach (var accessProfile in accessProfiles)
                    {
                        if (accessProfile.ID != AccessProfileID)
                            copyMenu.Buttons.Add(new RadToolBarButton(accessProfile.Title) { CommandName = "CopyMenu", CommandArgument = accessProfile.ID.ToString() });
                    }
                }

                var site = dataManager.Sites.SelectById(CurrentUser.Instance.SiteID);
                if (site.AccessProfileID != null)
                {
                    var siteAccessProfile = dataManager.AccessProfile.SelectById((Guid)site.AccessProfileID);
                    copyMenu.Buttons.Add(new RadToolBarButton(siteAccessProfile.Title) { CommandName = "CopyMenu", CommandArgument = siteAccessProfile.ID.ToString() });
                }
            }

            if (CurrentUser.Instance.AccessLevelID == 2)
            {
                var accessProfiles = dataManager.AccessProfile.SelectAll();
                if (accessProfiles != null)
                {
                    foreach (var accessProfile in accessProfiles)
                    {
                        if (accessProfile.ID != AccessProfileID)
                            copyMenu.Buttons.Add(new RadToolBarButton(accessProfile.Title) { CommandName = "CopyMenu", CommandArgument = accessProfile.ID.ToString() });
                    }
                }
            }
        }



        /// <summary>
        /// Handles the OnNodeDrop event of the rtvTreeModules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeDragDropEventArgs"/> instance containing the event data.</param>
        protected void rtvTreeModules_OnNodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            // Only tabs
            if (e.DestDragNode.Level == 1)
            {
                e.DestDragNode.Nodes.Add(e.DraggedNodes[0]);
                e.DraggedNodes[0].Selected = true;

                var order = 0;
                var menus = dataManager.Menu.SelectByAccessProfileID(AccessProfileID).Where(a => a.ParentID == Guid.Parse(e.DestDragNode.Attributes["NodeID"])).ToList();
                if (menus.Count > 0)
                    order = menus.OrderByDescending(a => a.Order).First().Order + 1;

                var menuId = Guid.NewGuid();

                var menuAdd = new tbl_Menu();
                menuAdd.ID = menuId;
                menuAdd.ParentID = Guid.Parse(e.DestDragNode.Attributes["NodeID"]);
                menuAdd.AccessProfileID = AccessProfileID;
                menuAdd.ModuleID = Guid.Parse(e.DraggedNodes[0].Value);
                menuAdd.Order = order;
                dataManager.Menu.Add(menuAdd);


                BindTreeModules();
                BindTreeMenu();
            }
        }



        /// <summary>
        /// Handles the OnNodeDrop event of the rtvTreeMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeDragDropEventArgs"/> instance containing the event data.</param>
        protected void rtvTreeMenu_OnNodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            if (e.DestDragNode.TreeView.ID == "rtvTreeMenu")
            {
                RadTreeNode sourceNode = e.SourceDragNode;
                RadTreeNode destNode = e.DestDragNode;
                RadTreeViewDropPosition dropPosition = e.DropPosition;
                if (destNode != null)
                {
                    if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                    {
                        PerformDragAndDrop(dropPosition, sourceNode, destNode);
                    }
                    else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                    {
                        foreach (RadTreeNode node in sourceNode.TreeView.SelectedNodes)
                        {
                            PerformDragAndDrop(dropPosition, node, destNode);
                        }
                    }
                    destNode.Expanded = true;
                    sourceNode.TreeView.UncheckAllNodes();
                }

                SaveMenu();
            }
        }



        /// <summary>
        /// Performs the drag and drop.
        /// </summary>
        /// <param name="dropPosition">The drop position.</param>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destNode">The dest node.</param>
        private static void PerformDragAndDrop(RadTreeViewDropPosition dropPosition, RadTreeNode sourceNode, RadTreeNode destNode)
        {
            switch (dropPosition)
            {
                case RadTreeViewDropPosition.Over:
                    // child
                    if (!sourceNode.IsAncestorOf(destNode) && ((destNode.Level == 1 && sourceNode.Level == 2) || (destNode.Level == 0 && sourceNode.Level == 1)))
                    {
                        sourceNode.Owner.Nodes.Remove(sourceNode);
                        destNode.Nodes.Add(sourceNode);
                    }
                    break;
                case RadTreeViewDropPosition.Above:
                    // sibling - above
                    if ((destNode.Level == 1 && sourceNode.Level == 1) || (destNode.Level == 2 && sourceNode.Level == 2))
                    {
                        sourceNode.Owner.Nodes.Remove(sourceNode);
                        destNode.InsertBefore(sourceNode);
                    }
                    break;
                case RadTreeViewDropPosition.Below:
                    // sibling - below
                    if ((destNode.Level == 1 && sourceNode.Level == 1) || (destNode.Level == 2 && sourceNode.Level == 2))
                    {
                        sourceNode.Owner.Nodes.Remove(sourceNode);
                        destNode.InsertAfter(sourceNode);
                    }
                    break;
            }
        }



        /// <summary>
        /// Handles the OnClick event of the btnAddMenuTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAddMenuTab_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnMenuID.Value))
            {
                var menuUpdate = dataManager.Menu.SelectByID(Guid.Parse(hdnMenuID.Value));
                menuUpdate.Title = txtMenuTabTitle.Text;
                menuUpdate.TabName = txtMenuName.Text;
                dataManager.Menu.Update(menuUpdate);
            }
            else
            {
                var order = 0;
                var menus = dataManager.Menu.SelectByAccessProfileID(AccessProfileID);
                if (menus != null && menus.Count > 0)
                    order = menus.OrderByDescending(a => a.Order).First().Order + 1;

                var menuId = Guid.NewGuid();

                var menuAdd = new tbl_Menu();
                menuAdd.ID = menuId;
                menuAdd.AccessProfileID = AccessProfileID;
                menuAdd.Title = txtMenuTabTitle.Text;
                menuAdd.TabName = txtMenuName.Text;
                menuAdd.Order = order;
                dataManager.Menu.Add(menuAdd);
            }

            BindTreeMenu();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnEditMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnEditMenu_OnClick(object sender, EventArgs e)
        {
            var menuId = Guid.Parse(lbtnEditMenu.CommandArgument);            
            var menuUpdate = dataManager.Menu.SelectByID(menuId);
            menuUpdate.Title = txtMenuItemTitle.Text;
            if (!string.IsNullOrEmpty(ddlModuleEditionAction.SelectedValue))
                menuUpdate.ModuleEditionActionID = Guid.Parse(ddlModuleEditionAction.SelectedValue);
            else
                menuUpdate.ModuleEditionActionID = null;

            dataManager.Menu.Update(menuUpdate);            

            BindTreeMenu();
        }



        /// <summary>
        /// Saves the menu.
        /// </summary>
        protected void SaveMenu()
        {
            using (var scope = new TransactionScope())
            {
                var allNodes = rtvTreeMenu.GetAllNodes();
                var menus = dataManager.Menu.SelectByAccessProfileID(AccessProfileID);

                for (var i = 0; i < allNodes.Count; i++)
                {
                    var node = allNodes[i];
                    if (node.Level != 0)
                    {
                        var menuItem = menus.SingleOrDefault(a => a.ID == Guid.Parse(node.Attributes["NodeID"]));
                        if (menuItem != null)
                        {
                            var parentNodeID = Guid.Parse(node.ParentNode.Attributes["NodeID"]);
                            menuItem.ParentID = parentNodeID == Guid.Empty ? (Guid?)null : parentNodeID;
                            menuItem.Order = i;
                        }

                        dataManager.Menu.Update(menuItem);
                    }
                }

                scope.Complete();
            }
            BindTreeMenu();
        }



        /// <summary>
        /// Handles the OnNodeDataBound event of the rtvTreeMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeEventArgs"/> instance containing the event data.</param>
        protected void rtvTreeMenu_OnNodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            dynamic dataItem = e.Node.DataItem;
            e.Node.Attributes.Add("NodeID", (dataItem.ID).ToString());

            if (e.Node.Level == 0)
                e.Node.ContextMenuID = "ctxMenuAdd";
        }



        /// <summary>
        /// Handles the OnContextMenuItemClick event of the rtvTreeMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeViewContextMenuEventArgs"/> instance containing the event data.</param>
        protected void rtvTreeMenu_OnContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            MenuActions(e.Node, e.MenuItem.Value, "");
        }



        /// <summary>
        /// Called when [button click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadToolBarEventArgs"/> instance containing the event data.</param>
        protected void OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var button = (RadToolBarButton)e.Item;
            var selectedNode = rtvTreeMenu.SelectedNode;

            MenuActions(selectedNode, button.CommandName, button.CommandArgument);
        }



        /// <summary>
        /// Menus the actions.
        /// </summary>
        /// <param name="selectedNode">The selected node.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="commandArgument">The command argument.</param>
        protected void MenuActions(RadTreeNode selectedNode, string commandName, string commandArgument)
        {
            switch (commandName)
            {
                case "AddTab":
                    hdnMenuID.Value = "";
                    txtMenuTabTitle.Text = "";
                    txtMenuName.Text = "";
                    rttAddTab.Show();
                    break;
                case "Rename":
                    if (rtvTreeMenu.SelectedNode.Level == 1)
                    {
                        var menu = dataManager.Menu.SelectByID(Guid.Parse(rtvTreeMenu.SelectedNode.Attributes["NodeID"]));
                        if (menu != null)
                        {
                            hdnMenuID.Value = menu.ID.ToString();
                            txtMenuTabTitle.Text = menu.Title;
                            txtMenuName.Text = menu.TabName;
                            rttAddTab.Show();
                        }
                    }
                    if (IsSiteProfiles && rtvTreeMenu.SelectedNode.Level == 2)
                    {
                        var menu = dataManager.Menu.SelectByID(Guid.Parse(rtvTreeMenu.SelectedNode.Attributes["NodeID"]));
                        if (menu != null)
                        {
                            txtMenuItemTitle.Text = string.IsNullOrEmpty(menu.Title) ? menu.tbl_Module.Title : menu.Title;
                            var accessProfileModule = menu.tbl_AccessProfile.tbl_AccessProfileModule.SingleOrDefault(o => o.ModuleID == menu.ModuleID);

                            ddlModuleEditionAction.Items.Clear();

                            if (accessProfileModule != null && accessProfileModule.ModuleEditionID.HasValue)
                            {
                                var moduleEditionActions = dataManager.ModuleEditionAction.SelectByModuleEditionId(accessProfileModule.ModuleEditionID.Value);
                                ddlModuleEditionAction.DataSource = moduleEditionActions;
                                ddlModuleEditionAction.DataTextField = "Title";
                                ddlModuleEditionAction.DataValueField = "ID";
                                ddlModuleEditionAction.DataBind();
                                
                                ddlModuleEditionAction.Items.Insert(0, new ListItem("Выберите значение", ""));                                
                            }

                            if (menu.ModuleEditionActionID.HasValue)
                                ddlModuleEditionAction.SelectedIndex = ddlModuleEditionAction.FindItemIndexByValue(menu.ModuleEditionActionID.Value.ToString());

                            lbtnEditMenu.CommandArgument = menu.ID.ToString();
                            
                            rttEditMenu.Show();
                        }
                    }
                    break;
                case "Delete":
                    if (selectedNode.Level == 1 || selectedNode.Level == 2)
                        dataManager.Menu.DeleteByMenuID(Guid.Parse(selectedNode.Attributes["NodeID"]));
                    BindTreeMenu();
                    break;
                case "ResetMenu":
                    dataManager.Menu.DeleteAllByAccessProfileID(AccessProfileID);
                    BindTreeMenu();
                    break;
                case "CopyMenu":
                    using (var scope = new TransactionScope())
                    {
                        dataManager.Menu.DeleteAllByAccessProfileID(AccessProfileID);
                        var copyMenu = dataManager.Menu.SelectByAccessProfileID(Guid.Parse(commandArgument));
                        var copyMenuTabs = copyMenu.Where(a => a.ModuleID.Equals(null));
                        if (copyMenu != null)
                        {
                            foreach (var copyMenuTab in copyMenuTabs)
                            {
                                var menuTab = new tbl_Menu
                                                    {
                                                        AccessProfileID = AccessProfileID,
                                                        Title = copyMenuTab.Title,
                                                        TabName = copyMenuTab.TabName,                                                        
                                                        Order = copyMenuTab.Order
                                                    };
                                menuTab = dataManager.Menu.Add(menuTab);

                                var copyMenuModules = copyMenu.Where(a => a.ParentID == copyMenuTab.ID);
                                foreach (var copyMenuModule in copyMenuModules)
                                {
                                    var menuModule = new tbl_Menu
                                                            {
                                                                ParentID = menuTab.ID,
                                                                AccessProfileID = AccessProfileID,
                                                                ModuleID = copyMenuModule.ModuleID,
                                                                ModuleEditionActionID = copyMenuModule.ModuleEditionActionID,
                                                                Title = copyMenuModule.Title,
                                                                Order = copyMenuModule.Order
                                                            };
                                    dataManager.Menu.Add(menuModule);
                                }
                            }
                        }

                        scope.Complete();
                    }
                    BindTreeMenu();
                    break;
            }
        }



        /// <summary>
        /// Handles the OnNodeClick event of the rtvTreeMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeEventArgs"/> instance containing the event data.</param>
        protected void rtvTreeMenu_OnNodeClick(object sender, RadTreeNodeEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 0:
                    rtbMenu.FindItemByValue("AddTab").Enabled = true;
                    rtbMenu.FindItemByValue("Rename").Enabled = false;
                    rtbMenu.FindItemByValue("Delete").Enabled = false;
                    break;
                case 1:
                    rtbMenu.FindItemByValue("AddTab").Enabled = false;
                    rtbMenu.FindItemByValue("Rename").Enabled = true;
                    rtbMenu.FindItemByValue("Delete").Enabled = true;
                    break;
                case 2:
                    rtbMenu.FindItemByValue("AddTab").Enabled = false;
                    rtbMenu.FindItemByValue("Rename").Enabled = IsSiteProfiles;
                    rtbMenu.FindItemByValue("Delete").Enabled = true;
                    break;
            }
        }
    }




    public class MenuClass
    {
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid AccessProfileID { get; set; }
        public string Title { get; set; }
        public Guid? ModuleID { get; set; }
    }
}