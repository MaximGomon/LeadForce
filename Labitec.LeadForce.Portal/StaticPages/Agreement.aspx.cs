using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.Portal.StaticPages
{
    public partial class Agreement : LeadForcePortalBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Пользовательское соглашение";
        }
    }
}