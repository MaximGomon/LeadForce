using System;
using System.Web;
using WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard;

namespace WebCounter.AdminPanel.UserControls.Wizards.Controls
{
    public class FormWizardStep : BaseWizardStep
    {
        /// <summary>
        /// Gets or sets the type of the current form.
        /// </summary>
        /// <value>
        /// The type of the current form.
        /// </value>
        protected Guid CurrentForm
        {
            get
            {
                if (HttpContext.Current.Session["CurrentForm"] == null)
                    HttpContext.Current.Session["CurrentForm"] = Guid.Empty;

                return (Guid)HttpContext.Current.Session["CurrentForm"];
            }
            set { HttpContext.Current.Session["CurrentForm"] = value; }
        }
    }
}