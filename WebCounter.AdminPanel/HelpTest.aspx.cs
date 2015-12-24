using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel
{
    public partial class HelpTest : LeadForceBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hlHelp.OnClientClick = "openHelpWindow('"+SiteId.ToString()+"','',''); return false;";
        }
    }
}