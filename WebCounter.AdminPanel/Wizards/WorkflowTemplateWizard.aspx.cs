using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.Wizards
{
    public partial class WorkflowTemplateWizard : LeadForceBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Шаблоны процессов - LeadForce";

            var workflowTemplateId = Guid.Empty;

            if (Page.RouteData.Values["id"] != null && Guid.TryParse(Page.RouteData.Values["id"] as string, out workflowTemplateId))
            {
                ucWorkflowTemplateWizard.EditFormId = workflowTemplateId;
                ucWorkflowTemplateWizard.IsEditMode = true;
            }
        }
    }
}