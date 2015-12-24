using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.LeadForce.Report;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.Reports
{
    public partial class RequiremetReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Отчет по статусу работ";

            Guid? companyId = null;

            if (!string.IsNullOrEmpty(Request.QueryString["companyId"]))
                companyId = Guid.Parse(Request.QueryString["companyId"]);

            requirementReportViewer.ReportSource = new RequirementReport(CurrentUser.Instance.SiteID, companyId,
                                                                         DateTime.Parse(Request.QueryString["startDate"]),
                                                                         DateTime.Parse(Request.QueryString["endDate"]));
        }
    }
}