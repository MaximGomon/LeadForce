using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.SiteSettings
{
    public partial class WidgetSettings : System.Web.UI.UserControl
    {
        protected RadAjaxManager radAjaxManager = null;
        protected DataManager dataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid CurrentModuleId
        {
            get
            {
                if (Session["Widget-CurrentModuleId"] == null)
                    Session["Widget-CurrentModuleId"] = Guid.Empty;

                return (Guid)Session["Widget-CurrentModuleId"];
            }
            set { Session["Widget-CurrentModuleId"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid AccessProfileId
        {
            get
            {
                object o = Session["Widget-AccessProfileID"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                Session["Widget-AccessProfileID"] = value;
            }
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
                BindData();                
            }

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxRequest += radAjaxManager_AjaxRequest;            
            radAjaxManager.AjaxSettings.AddAjaxSetting(rcbModule, plLeftColumn);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rcbModule, rtwWidgets);                        
            radAjaxManager.AjaxSettings.AddAjaxSetting(rtwWidgets, radDockLayout);            
        }



        /// <summary>
        /// Handles the AjaxRequest event of the radAjaxManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void radAjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            var dockStates = radDockLayout.GetRegisteredDocksState();            

            if (e.Argument == "Move")
            {
                foreach (var dockState in dockStates)
                {
                    var widgetToAccessProfile = dataManager.WidgetToAccessProfile.SelectById(Guid.Parse(dockState.UniqueName));
                    widgetToAccessProfile.Order = dockState.Index * 4 + int.Parse(dockState.DockZoneID.Substring(dockState.DockZoneID.Length - 1)) - 1;
                    dataManager.WidgetToAccessProfile.Update(widgetToAccessProfile);
                }
            }            
            if (e.Argument == "Close")
            {
                var module = dataManager.Module.SelectById(CurrentModuleId);
                
                var firstOrDefault = dockStates.FirstOrDefault(o => o.Closed);
                if (firstOrDefault != null)
                {
                    var dock = radDockLayout.FindControl(firstOrDefault.UniqueName) as RadDock;

                    if (module != null && module.Name == "Main")
                    {
                        switch (int.Parse(firstOrDefault.DockZoneID.Substring(firstOrDefault.DockZoneID.Length - 1)))
                        {
                            case 1:
                                radDockZone1.Docks.Remove(dock);
                                radDockZone1.Controls.Remove(dock);
                                break;
                            case 2:
                                radDockZone2.Docks.Remove(dock);
                                radDockZone2.Controls.Remove(dock);
                                break;
                            case 3:
                                radDockZone3.Docks.Remove(dock);
                                radDockZone3.Controls.Remove(dock);
                                break;
                            case 4:
                                radDockZone4.Docks.Remove(dock);
                                radDockZone4.Controls.Remove(dock);
                                break;
                        }
                    }
                    else
                    {                        
                        radDockZone1.Docks.Remove(dock);
                        radDockZone1.Controls.Remove(dock);
                    }

                    dataManager.WidgetToAccessProfile.Delete(dataManager.WidgetToAccessProfile.SelectById(Guid.Parse(firstOrDefault.UniqueName)));
                }
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            CurrentModuleId = Guid.Empty;

            var accessProfile = dataManager.AccessProfile.SelectById(AccessProfileId);
            var accessProfileModules = dataManager.AccessProfileModule.SelectByAccessProfileID(AccessProfileId).Where(a => a.Read).OrderBy(a => a.tbl_Module.Title).ToList();
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
            
            rcbModule.DataSource = modulesFiltered;            
            rcbModule.DataValueField = "ID";
            rcbModule.DataTextField = "Title";
            rcbModule.DataBind();
            rcbModule.ClearSelection();
            rcbModule.Items.Insert(0, new RadComboBoxItem("Во всех модулях", string.Empty) { Selected = true});
            rcbModule.Text = "Во всех модулях";

            BindWidgets();

            BindRadDocks();
        }



        /// <summary>
        /// Binds the widgets.
        /// </summary>        
        protected void BindWidgets()
        {            
            if (CurrentModuleId != Guid.Empty)
            {
                var module = dataManager.Module.SelectById(CurrentModuleId);
                if (module != null && module.Name == "Main")
                {                    
                    radDockZone2.Visible = true;
                    radDockZone3.Visible = true;
                    radDockZone4.Visible = true;
                }
                else
                {
                    radDockZone2.Visible = false;
                    radDockZone3.Visible = false;
                    radDockZone4.Visible = false;
                }
            }
            else
            {
                radDockZone2.Visible = false;
                radDockZone3.Visible = false;
                radDockZone4.Visible = false;
            }


            var widgetCategories = dataManager.WidgetCategory.SelectAll();
            var widgetsDataSource = new List<Widgets>();

            foreach (tbl_WidgetCategory widgetCategory in widgetCategories)
            {
                var widget = new Widgets
                {
                    Id = widgetCategory.ID,
                    ParentId = widgetCategory.ParentID,
                    Title = widgetCategory.Title,
                    Type = WidgetTypeInTreeView.Category
                };
                widgetsDataSource.Add(widget);

                foreach (tbl_Widget tblWidget in widgetCategory.tbl_Widget)
                {
                    widget = new Widgets
                    {
                        Id = tblWidget.ID,
                        ParentId = widgetCategory.ID,
                        Title = tblWidget.Title,
                        Type = WidgetTypeInTreeView.Widget
                    };

                    widgetsDataSource.Add(widget);
                }
            }

            rtwWidgets.DataSource = widgetsDataSource;
            rtwWidgets.DataTextField = "Title";
            rtwWidgets.DataValueField = "Id";
            rtwWidgets.DataFieldID = "Id";
            rtwWidgets.DataFieldParentID = "ParentId";
            rtwWidgets.DataBind();            
        }



        /// <summary>
        /// Binds the RAD docks.
        /// </summary>
        protected void BindRadDocks()
        {
            radDockZone1.Controls.Clear();
            radDockZone2.Controls.Clear();
            radDockZone3.Controls.Clear();
            radDockZone4.Controls.Clear();

            var widgetAccessProfile = dataManager.WidgetToAccessProfile.SelectByAccessProfileAndModuleIds(AccessProfileId, CurrentModuleId).OrderBy(o => o.Order);

            var module = dataManager.Module.SelectById(CurrentModuleId);            
            foreach (var widget in widgetAccessProfile)
            {
                var radDock = AddRadDock(widget.tbl_Widget.Title, widget.ID.ToString());
                if (radDock != null)
                {
                    if (module != null && module.Name == "Main")
                        radDockLayout.RegisteredZones[widget.Order % 4].Controls.Add(radDock);                    
                    else
                        radDockLayout.RegisteredZones[0].Controls.Add(radDock);
                }
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rcbModule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void rcbModule_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)        
        {
            if (!string.IsNullOrEmpty(rcbModule.SelectedValue))
                CurrentModuleId = Guid.Parse(rcbModule.SelectedValue);
            else
                CurrentModuleId = Guid.Empty;

            BindWidgets();
            BindRadDocks();
        }



        /// <summary>
        /// Handles the OnNodeDrop event of the rtwWidgets control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeDragDropEventArgs"/> instance containing the event data.</param>
        protected void rtwWidgets_OnNodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {                      
            if (e.HtmlElementID == radDockZone1.ClientID)            
                ProceedDrop(radDockZone1, e);
            if (e.HtmlElementID == radDockZone2.ClientID)
                ProceedDrop(radDockZone2, e);
            if (e.HtmlElementID == radDockZone3.ClientID)
                ProceedDrop(radDockZone3, e);
            if (e.HtmlElementID == radDockZone4.ClientID)
                ProceedDrop(radDockZone4, e);
        }



        /// <summary>
        /// Proceeds the drop.
        /// </summary>
        /// <param name="radDockZone">The RAD dock zone.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeDragDropEventArgs"/> instance containing the event data.</param>
        private void ProceedDrop(RadDockZone radDockZone, RadTreeNodeDragDropEventArgs e)
        {
            if ((WidgetTypeInTreeView)int.Parse(e.SourceDragNode.Attributes["Type"]) != WidgetTypeInTreeView.Category)
            {
                var widgetToAccessProfile = new tbl_WidgetToAccessProfile
                {
                    AccessProfileID = AccessProfileId,
                    WidgetID = Guid.Parse(e.SourceDragNode.Value),
                    Order = radDockLayout.RegisteredDocks.Count,
                    ModuleID = CurrentModuleId == Guid.Empty ? null : (Guid?)CurrentModuleId
                };

                dataManager.WidgetToAccessProfile.Add(widgetToAccessProfile);

                radDockZone.Controls.Add(AddRadDock(e.SourceDragNode.Text, widgetToAccessProfile.ID.ToString()));
            }
            else
            {
                foreach (RadTreeNode node in e.SourceDragNode.Nodes)
                {
                    var widgetToAccessProfile = new tbl_WidgetToAccessProfile
                    {
                        AccessProfileID = AccessProfileId,
                        WidgetID = Guid.Parse(node.Value),
                        Order = radDockLayout.RegisteredDocks.Count,
                        ModuleID = CurrentModuleId == Guid.Empty ? null : (Guid?)CurrentModuleId
                    };

                    dataManager.WidgetToAccessProfile.Add(widgetToAccessProfile);

                    radDockZone.Controls.Add(AddRadDock(node.Text, widgetToAccessProfile.ID.ToString()));
                }
            }                         
        }



        /// <summary>
        /// Adds the RAD dock.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected RadDock AddRadDock(string text, string value)
        {
            var radDock = new RadDock
                              {
                                  Text = string.Empty,
                                  ID = value,
                                  Skin = "Windows7",
                                  DockMode = DockMode.Docked,
                                  Width = new Unit(200, UnitType.Pixel),
                                  Title = text,
                                  UniqueName = value,
                                  Closed = false                                  
                              };

            radDock.OnClientDockPositionChanged = "RadDockPositionChanged";
            radDock.OnClientCommand = "RadDockClientCommand";

            return radDock;
        }




        /// <summary>
        /// Handles the OnNodeDataBound event of the rtwWidgets control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeEventArgs"/> instance containing the event data.</param>
        protected void rtwWidgets_OnNodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            e.Node.Attributes.Add("Type", ((int)((Widgets)e.Node.DataItem).Type).ToString());
        }



        /// <summary>
        /// Handles the OnLoadDockLayout event of the radDockLayout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.DockLayoutEventArgs"/> instance containing the event data.</param>
        protected void radDockLayout_OnLoadDockLayout(object sender, DockLayoutEventArgs e)
        {
            if (!Page.IsPostBack)            
                CurrentModuleId = Guid.Empty;

            BindRadDocks();
        }
    }


    public class Widgets
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
        public WidgetTypeInTreeView Type { get; set; }
    }


    public enum WidgetTypeInTreeView
    {
        Category,
        Widget
    }
}