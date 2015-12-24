using System;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel
{
    public partial class SiteActionTemplates : LeadForceBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Шаблоны сообщений - LeadForce";
        }
    }
}