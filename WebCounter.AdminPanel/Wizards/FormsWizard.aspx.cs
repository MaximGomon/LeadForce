using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.Wizards
{
    public partial class FormsWizard : LeadForceBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Формы - LeadForce";

            Guid formId = Guid.Empty;

            if (Page.RouteData.Values["id"] != null && Guid.TryParse(Page.RouteData.Values["id"] as string, out formId))
            {
                ucFormWizard.EditFormId = formId;
                ucFormWizard.IsEditMode = true;
            }
        }
    }
}