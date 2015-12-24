using System;
using System.Web;
using Labitec.LeadForce.Report;
using Telerik.Reporting.Processing;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.Portal.Main.InvoiceModule
{
    public partial class PrintInvoice : LeadForcePortalBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Печатная форма счета";

            Guid invoiceId = Guid.Empty;

            var id = Page.RouteData.Values["id"] as string;
            Guid guidId;
            if (Guid.TryParse(id, out guidId))
                invoiceId = guidId;                        

            if (!string.IsNullOrEmpty(Request.QueryString["autoexport"]))
            {
                switch (Request.QueryString["autoexport"])
                {
                    case "pdf":
                        ExportToPDF(new InvoiceReport(SiteId, invoiceId, true));
                        break;
                }
            }
            else
                invoiceReportViewer.Report = new InvoiceReport(SiteId, invoiceId, true);
        }



        /// <summary>
        /// Exports to PDF.
        /// </summary>
        /// <param name="reportToExport">The report to export.</param>
        protected void ExportToPDF(Telerik.Reporting.Report reportToExport)
        {
            var reportProcessor = new ReportProcessor();
            var instanceReportSource = new Telerik.Reporting.InstanceReportSource {ReportDocument = reportToExport};
            RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, null);

            string fileName = result.DocumentName + "." + result.Extension;

            Response.Clear();
            Response.ContentType = result.MimeType;
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Expires = -1;
            Response.Buffer = true;

            Response.AddHeader("Content-Disposition",
                               string.Format("{0};FileName=\"{1}\"",
                                             "attachment",
                                             fileName));

            Response.BinaryWrite(result.DocumentBytes);
            Response.End();
        }

    }
}