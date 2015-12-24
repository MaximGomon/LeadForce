using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.Handlers
{
    public partial class SiteActionTemplateThumbnail : System.Web.UI.Page
    {
        private DataManager _dataManager = new DataManager();



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["siteid"]) && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                var siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(Request.QueryString["siteid"].ToGuid(), Request.QueryString["id"].ToGuid());
                if (siteActionTemplate != null)
                    lMessageBody.Text = siteActionTemplate.MessageBody;
            }
        }
    }
}