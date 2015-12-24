using System;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;

namespace Labitec.LeadForce.Portal.Main.InvoiceModule
{
    public partial class Invoices : LeadForcePortalBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Счета";

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(All, gridInvoices);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ToPay, gridInvoices);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(Paid, gridInvoices);

            gridInvoices.ModuleName = "Invoices";            

            gridInvoices.Where.Add(new GridWhere()
                                       {
                                           CustomQuery =
                                               string.Format(
                                                   "(tbl_Invoice.InvoiceStatusID = {0} OR tbl_Invoice.InvoiceStatusID = {1})",
                                                   (int) InvoiceStatus.PendingPayment,                                                   
                                                   (int) InvoiceStatus.PartialPaid)
                                       });
            AddSystemWhere();
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
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var hlTitle = (HyperLink)item.FindControl("hlTitle");
                hlTitle.Text = string.Format("Счет №{0} от {1}", data["tbl_Invoice_Number"], DateTime.Parse(data["tbl_Invoice_CreatedAt"].ToString()).ToString("dd/MM/yyyy"));
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = hlTitle.NavigateUrl = UrlsData.LFP_InvoiceEdit(Guid.Parse(data["tbl_Invoice_ID"].ToString()), PortalSettingsId);
                ((Literal)item.FindControl("lrlInvoiceAmount")).Text = decimal.Parse(data["tbl_Invoice_InvoiceAmount"].ToString()).ToString("F");
                ((Literal)item.FindControl("lrlNote")).Text = data["tbl_Invoice_Note"].ToString();
                ((Literal)item.FindControl("lrlInvoiceStatus")).Text = EnumHelper.GetEnumDescription((InvoiceStatus)int.Parse(data["tbl_Invoice_InvoiceStatusID"].ToString()));
                if (!string.IsNullOrEmpty(data["tbl_Invoice_PaymentDateActual"].ToString()))
                    ((Literal)item.FindControl("lrlPaymentDateActual")).Text = DateTime.Parse(data["tbl_Invoice_CreatedAt"].ToString()).ToString("dd/MM/yyyy");
                else
                    ((Literal)item.FindControl("lrlPaymentDateActual")).Text = "---";

                if (!string.IsNullOrEmpty(data["tbl_Company_Name"].ToString()))
                    ((Literal)item.FindControl("lrlCompany")).Text = data["tbl_Company_Name"].ToString();

                if (!string.IsNullOrEmpty(data["tbl_CompanyLegalAccount_Title"].ToString()))
                    ((Literal)item.FindControl("lrlCompanyLegalAccount")).Text = data["tbl_CompanyLegalAccount_Title"].ToString();
                
                ((HyperLink)item.FindControl("hlPrint")).NavigateUrl =
                    UrlsData.LFP_InvoicePrint(data["tbl_Invoice_ID"].ToString().ToGuid(), PortalSettingsId);
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

            AddSystemWhere();

            gridInvoices.Rebind();
        }


        private void AddSystemWhere()
        {            
            if (CurrentUser.Instance.CompanyID.HasValue)
            {
                gridInvoices.Where.Add(new GridWhere()
                {
                    CustomQuery =
                        string.Format(
                            "(BuyerCompanyID = '{0}' OR ExecutorContactID = '{1}' OR BuyerContactID = '{1}' OR BuyerCompanyID IN (SELECT tbl_ServiceLevelClient.ClientID FROM tbl_ServiceLevelContact LEFT JOIN tbl_ServiceLevelClient ON tbl_ServiceLevelContact.ServiceLevelClientID = tbl_ServiceLevelClient.ID WHERE tbl_ServiceLevelContact.ContactID = '{1}'))",
                            CurrentUser.Instance.CompanyID.Value,
                            CurrentUser.Instance.ContactID.Value)
                });
            }
            else
            {
                gridInvoices.Where.Add(new GridWhere()
                {
                    CustomQuery =
                        string.Format(
                            "(ExecutorContactID = '{0}' OR BuyerContactID = '{0}' OR BuyerCompanyID IN (SELECT tbl_ServiceLevelClient.ClientID FROM tbl_ServiceLevelContact LEFT JOIN tbl_ServiceLevelClient ON tbl_ServiceLevelContact.ServiceLevelClientID = tbl_ServiceLevelClient.ID WHERE tbl_ServiceLevelContact.ContactID = '{0}'))",
                            CurrentUser.Instance.ContactID.Value)
                });
            }
        }        
    }
}