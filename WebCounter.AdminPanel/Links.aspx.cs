using System;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel
{
    public partial class Links : LeadForceBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Ссылки - LeadForce";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProcessModuleEdition(plPageContainer);
        }
    }
}