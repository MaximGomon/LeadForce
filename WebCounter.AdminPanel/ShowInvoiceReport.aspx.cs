using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.LeadForce.Report;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel
{
    public partial class ShowInvoiceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Печатная форма счета";

            Guid invoiceId;
            bool showStamp;

            if (!string.IsNullOrEmpty(Request.QueryString["invoiceId"]) && Guid.TryParse(Request.QueryString["invoiceId"], out invoiceId)
                && !string.IsNullOrEmpty(Request.QueryString["ss"]) && bool.TryParse(Request.QueryString["ss"], out showStamp))
                invoiceReportViewer.Report = new InvoiceReport(CurrentUser.Instance.SiteID, invoiceId, showStamp);
        }
    }
}