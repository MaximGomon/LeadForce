using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.CMS
{
    public partial class StaticPage : LeadForceCMSBasePage
    {
        protected tbl_WebSitePage currentPage = new tbl_WebSitePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["link"] != null)
            {
                currentPage = DataManager.WebSitePage.SelectByUrl(WebSiteId, Page.RouteData.Values["link"] as string);
                if (currentPage != null)
                {
                    SetMetaData(currentPage);
                    ProceedResources(currentPage);
                    lrlBody.Text = currentPage.Body;
                }
            }
        }
    }
}