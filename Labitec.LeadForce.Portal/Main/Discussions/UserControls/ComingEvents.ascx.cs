using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.Portal.Main.Discussions.UserControls
{
    public partial class ComingEvents : System.Web.UI.UserControl
    {
        private readonly DataManager _dataManager = new DataManager();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            lvComingEvents.DataSource = _dataManager.Task.SelectPublicTasks(((LeadForcePortalBasePage) Page).SiteId).Where(pt => pt.StartDate >= DateTime.Now).OrderByDescending(pt => pt.StartDate);
            lvComingEvents.DataBind();
        }
    }
}