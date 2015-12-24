using System;

namespace WebCounter.AdminPanel.Wizards
{
    public partial class LeadForceWizard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }
    }
}