using System;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.CMS
{
    public partial class Default : LeadForceCMSBasePage
    {
        protected tbl_WebSitePage currentPage = new tbl_WebSitePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            currentPage = DataManager.WebSitePage.SelectHomePage(WebSiteId);
            if (currentPage != null)
            {
                SetMetaData(currentPage);
                ProceedResources(currentPage);
                lrlBody.Text = currentPage.Body;
            }
        }
    }
}