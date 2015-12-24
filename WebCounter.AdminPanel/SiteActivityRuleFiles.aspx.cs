using System;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel
{
    public partial class SiteActivityRuleFiles : LeadForceBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Правила обработки - LeadForce";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProcessModuleEdition(plPageContainer);
        }
    }
}