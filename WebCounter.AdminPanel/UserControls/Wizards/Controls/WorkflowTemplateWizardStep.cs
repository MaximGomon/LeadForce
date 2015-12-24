using System;
using System.Web;
using WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.Controls
{
    public class WorkflowTemplateWizardStep : BaseWizardStep
    {
        /// <summary>
        /// Gets or sets the current workflow template.
        /// </summary>
        /// <value>
        /// The current workflow template.
        /// </value>
        protected Guid CurrentWorkflowTemplate
        {
            get
            {
                if (HttpContext.Current.Session["CurrentWorkflowTemplate"] == null)
                    HttpContext.Current.Session["CurrentWorkflowTemplate"] = Guid.Empty;

                return (Guid)HttpContext.Current.Session["CurrentWorkflowTemplate"];
            }
            set { HttpContext.Current.Session["CurrentWorkflowTemplate"] = value; }
        }
    }
}