using System;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;
using System.Linq;

namespace WebCounter.AdminPanel
{
    public partial class Invoices : LeadForceBasePage
    {
        public Access access;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Счета - LeadForce";

            access = Access.Check();

            //RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ddlFilter, gridInvoices);

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(All, gridInvoices);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ToPay, gridInvoices);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(Paid, gridInvoices);

            rbAddInvoice.NavigateUrl = UrlsData.AP_InvoiceAdd();            
            gridInvoices.Where.Add(new GridWhere { CustomQuery = string.Format("(InvoiceStatusID = {0} OR InvoiceStatusID = {1})", (int)InvoiceStatus.PendingPayment, (int)InvoiceStatus.PartialPaid) });
            gridInvoices.SiteID = SiteId;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridInvoices control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridInvoices_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var hlTitle = (HyperLink) item.FindControl("hlTitle");
                hlTitle.Text = string.Format("Счет №{0} от {1}", data["tbl_Invoice_Number"], DateTime.Parse(data["tbl_Invoice_CreatedAt"].ToString()).ToString("dd/MM/yyyy"));
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = hlTitle.NavigateUrl = UrlsData.AP_InvoiceEdit(Guid.Parse(data["tbl_Invoice_ID"].ToString()));
                ((Literal)item.FindControl("lrlInvoiceAmount")).Text = decimal.Parse(data["tbl_Invoice_InvoiceAmount"].ToString()).ToString("F");
                ((Literal) item.FindControl("lrlNote")).Text = data["tbl_Invoice_Note"].ToString();
                ((Literal)item.FindControl("lrlInvoiceStatus")).Text = EnumHelper.GetEnumDescription((InvoiceStatus)int.Parse(data["tbl_Invoice_InvoiceStatusID"].ToString()));
                if (!string.IsNullOrEmpty(data["tbl_Invoice_PaymentDateActual"].ToString()))
                    ((Literal)item.FindControl("lrlPaymentDateActual")).Text = DateTime.Parse(data["tbl_Invoice_CreatedAt"].ToString()).ToString("dd/MM/yyyy");
                else
                    ((Literal) item.FindControl("lrlPaymentDateActual")).Text = "---";

                if (!string.IsNullOrEmpty(data["tbl_Company_Name"].ToString()))
                    ((Literal)item.FindControl("lrlCompany")).Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Company(Guid.Parse(data["tbl_Company_ID"].ToString())), data["tbl_Company_Name"]);

                if (!string.IsNullOrEmpty(data["tbl_CompanyLegalAccount_Title"].ToString()))
                    ((Literal)item.FindControl("lrlCompanyLegalAccount")).Text = data["tbl_CompanyLegalAccount_Title"].ToString();

                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();
                lbDelete.Command += lbDelete_OnCommand;

                lbDelete.Visible = access.Delete;

                var invoice = DataManager.Invoice.SelectById(Guid.Parse(data["tbl_Invoice_ID"].ToString()));
                if (invoice.tbl_Shipment.Any())
                {
                    ((Literal)item.FindControl("lrlShipments")).Text =
                        string.Format("<div class=\"span-url\">Отгрузки: {0}</div>",
                                      string.Join(", ", invoice.tbl_Shipment.Select(
                                          o =>
                                          string.Format("<a href=\"{0}\">Отгрузка №{1} от {2}</a>", UrlsData.AP_ShipmentEdit(o.ID), o.Number, o.CreatedAt.ToString("dd.MM.yyyy")))));
                }
            }
        }


        /// <summary>
        /// Handles the OnCheckedChanged event of the filters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void filters_OnCheckedChanged(object sender, EventArgs e)
        {
            gridInvoices.Where.Clear();

            var radioButton = (RadioButton)sender;
            switch (radioButton.ID)
            {
                case "ToPay":
                    gridInvoices.Where.Add(new GridWhere()
                    {
                        CustomQuery =
                            string.Format(
                                "(tbl_Invoice.InvoiceStatusID = {0} OR tbl_Invoice.InvoiceStatusID = {1})",
                                (int)InvoiceStatus.PendingPayment,
                                (int)InvoiceStatus.PartialPaid)
                    });
                    break;
                case "Paid":
                    gridInvoices.Where.Add(new GridWhere()
                    {
                        CustomQuery =
                            string.Format(
                                "(tbl_Invoice.InvoiceStatusID = {0} )",
                                (int)InvoiceStatus.Paid)
                    });
                    break;
                case "All":
                    gridInvoices.Where.Add(new GridWhere()
                    {
                        CustomQuery =
                            string.Format(
                                "(tbl_Invoice.InvoiceStatusID = {0} OR tbl_Invoice.InvoiceStatusID = {1} OR tbl_Invoice.InvoiceStatusID = {2})",
                                (int)InvoiceStatus.PendingPayment,
                                (int)InvoiceStatus.Paid,
                                (int)InvoiceStatus.PartialPaid)
                    });
                    break;
            }        

            gridInvoices.Rebind();
        }


        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {            
            DataManager.Invoice.DeleteById(SiteId, Guid.Parse(e.CommandArgument.ToString()));
            gridInvoices.Rebind();
        }
    }
}