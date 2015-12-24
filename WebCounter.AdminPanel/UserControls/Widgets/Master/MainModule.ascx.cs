using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Widgets.Master
{
    public partial class MainModule : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void radDockLayout_OnLoadDockLayout(object sender, DockLayoutEventArgs e)
        {
            if (!CurrentUser.Instance.SiteAccessProfileID.HasValue)
            {
                this.Visible = false;
                return;
            }

            var page = ((LeadForceBasePage)Page);
            var dataManager = new DataManager();
            var widgets = dataManager.WidgetToAccessProfile.SelectByAccessProfileAndModuleIds(CurrentUser.Instance.SiteAccessProfileID.Value, page.CurrentModuleId).ToList();
            //widgets.AddRange(dataManager.WidgetToAccessProfile.SelectByAccessProfileForAllModules(CurrentUser.Instance.SiteAccessProfileID.Value));

            if (!widgets.Any())
            {
                this.Visible = false;
                return;
            }

            foreach (var widget in widgets)
            {
                var radDock= AddRadDock(widget);
                if (radDock != null)
                {
                    radDockLayout.RegisteredZones[widget.Order%4].Controls.Add(radDock);                    
                }
            }
        }



        protected RadDock AddRadDock(tbl_WidgetToAccessProfile widget)
        {
            if (string.IsNullOrEmpty(widget.tbl_Widget.UserControl))
                return null;

            var radDock = new RadDock
            {
                Text = string.Empty,
                ID = Guid.NewGuid().ToString(),
                Skin = "Windows7",
                DockMode = DockMode.Docked,
                Width = new Unit(290, UnitType.Pixel),                
                Title = widget.tbl_Widget.Title,
                UniqueName = Guid.NewGuid().ToString()
            };
            radDock.Commands.Add(new DockExpandCollapseCommand());

            var widgetControl = (WidgetBase)LoadControl(widget.tbl_Widget.UserControl);
            widgetControl.WidgetId = widget.ID;
            widgetControl.AccessProfile = widget.tbl_AccessProfile;
            radDock.ContentContainer.Controls.Add(widgetControl);

            //radDock.OnClientDockPositionChanged = "RadDockPositionChanged";
            //radDock.OnClientCommand = "RadDockClientCommand";

            return radDock;
            //radDockZone.Controls.Add(radDock);
        }
    }
}