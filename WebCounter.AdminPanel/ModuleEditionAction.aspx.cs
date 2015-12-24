using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel
{
    public partial class ModuleEditionAction : LeadForceBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentModuleEditionAction != null)
                Title = CurrentModuleEditionAction.Title;

            ProcessModuleEdition(plPageContainer);
        }
    }
}