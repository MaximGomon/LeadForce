using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Invoice : LeadForceBasePage
    {
        private Guid _invoiceId;
        public Access access;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Site)Page.Master).HideInaccessibleTabs(ref RadTabStrip1, ref RadMultiPage1);

            access = Access.Check();
            if (!access.Write)
                lbtnSave.Visible = false;

            Title = "Счет - LeadForce";

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPriceList, ucPriceList);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rdpInvoiceDate, rdpInvoiceDate, null,
                                                                        UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucInvoiceProducts, lrlInvoiceAmount, null,
                                                                        UpdatePanelRenderMode.Inline);

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucInvoiceProducts, ucInvoiceShipments, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbBuyerCompanyLegalAccount, ucInvoiceShipments, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbExecutorCompanyLegalAccount, ucInvoiceShipments, null, UpdatePanelRenderMode.Inline);

            ucPriceList.SelectedIndexChanged += ucPriceList_SelectedIndexChanged;
            rdpInvoiceDate.SelectedDateChanged += rdpInvoiceDate_SelectedDateChanged;
            ucInvoiceProducts.SiteId = SiteId;
            ucInvoiceProducts.InvoiceProductsChanged += ucInvoiceProducts_OrderProductsChanged;

            if (Page.RouteData.Values["id"] != null)
                _invoiceId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_Invoices();

            tagsInvoices.ObjectID = _invoiceId;

            ucContentComments.ContentId = _invoiceId;

            gridInvoiceHistory.Where.Add(new GridWhere() {Column = "InvoiceID", Value = _invoiceId.ToString()});

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            EnumHelper.EnumToDropDownList<InvoiceStatus>(ref ddlInvoiceStatus);

            ddlInvoiceStatus.SelectedIndex =
                ddlInvoiceStatus.FindItemIndexByValue(((int) InvoiceStatus.NotSend).ToString());

            dcbInvoiceType.SiteID =
                dcbExecutorCompany.SiteID =
                dcbBuyerCompany.SiteID =
                dcbBuyerCompanyLegalAccount.SiteID =
                dcbExecutorCompanyLegalAccount.SiteID = ucPriceList.SiteId = dcbOrders.SiteID = SiteId;
            rdpInvoiceDate.SelectedDate = DateTime.Now;
            ucInvoiceProducts.InvoiceDate = DateTime.Now;

            var filter = new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                             {
                                 Name = "IsActive",
                                 DbType = DbType.Boolean,
                                 Value = "TRUE"
                             };

            dcbBuyerCompanyLegalAccount.Filters.Add(filter);
            dcbExecutorCompanyLegalAccount.Filters.Add(filter);

            dcbExecutorCompany.BindData();
            dcbBuyerCompanyLegalAccount.BindData();
            dcbExecutorCompanyLegalAccount.BindData();

            var invoice = DataManager.Invoice.SelectById(SiteId, _invoiceId);
            if (invoice != null)
            {
                plComments.Visible = true;
                lbtnShowSendInvoiceRadWindow.Visible = true;

                rpbLeft.FindItemByValue("Reports", true).Visible = true;

                ucInvoiceShipments.InvoiceId = invoice.ID;
                ucInvoiceShipments.BuyerCompanyLegalAccountId = invoice.BuyerCompanyLegalAccountID;
                ucInvoiceShipments.ExecutorCompanyLegalAccountId = invoice.ExecutorCompanyLegalAccountID;
                ucInvoiceShipments.InvoiceAmount = invoice.InvoiceAmount;


                hlWithStamp.NavigateUrl =
                    ResolveUrl(string.Format("~/ShowInvoiceReport.aspx?invoiceId={0}&ss={1}", invoice.ID, true));
                hlWithoutStamp.NavigateUrl =
                    ResolveUrl(string.Format("~/ShowInvoiceReport.aspx?invoiceId={0}&ss={1}", invoice.ID, false));

                ucInvoiceProducts.InvoiceId = _invoiceId;
                ucInvoiceProducts.InvoiceDate = invoice.CreatedAt;
                lrlNumber.Text = invoice.Number;
                rdpInvoiceDate.SelectedDate = invoice.CreatedAt;
                dcbInvoiceType.SelectedId = invoice.InvoiceTypeID;
                ddlInvoiceStatus.SelectedIndex =
                    ddlInvoiceStatus.Items.IndexOf(ddlInvoiceStatus.Items.FindByValue(invoice.InvoiceStatusID.ToString()));
                txtNote.Text = invoice.Note;
                if (invoice.ExecutorCompanyID.HasValue)
                {
                    dcbExecutorCompany.SelectedId = (Guid) invoice.ExecutorCompanyID;
                    dcbExecutorCompany.SelectedText =
                        DataManager.Company.SelectById(SiteId, (Guid) invoice.ExecutorCompanyID).Name;
                }

                if (invoice.BuyerCompanyID.HasValue)
                {
                    dcbBuyerCompany.SelectedId = (Guid) invoice.BuyerCompanyID;
                    dcbBuyerCompany.SelectedText =
                        DataManager.Company.SelectById(SiteId, (Guid) invoice.BuyerCompanyID).Name;
                    dcbBuyerCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                                {
                                                                    Name = "CompanyID",
                                                                    DbType = DbType.Guid,
                                                                    Value = invoice.BuyerCompanyID.ToString()
                                                                });
                }

                if (invoice.ExecutorCompanyLegalAccountID.HasValue)
                {
                    dcbExecutorCompanyLegalAccount.SelectedId = (Guid) invoice.ExecutorCompanyLegalAccountID;
                    dcbExecutorCompanyLegalAccount.SelectedText =
                        DataManager.CompanyLegalAccount.SelectById((Guid) invoice.ExecutorCompanyLegalAccountID).Title;
                    dcbExecutorCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                                   {
                                                                       Name = "CompanyID",
                                                                       DbType = DbType.Guid,
                                                                       Value = invoice.ExecutorCompanyID.ToString()
                                                                   });
                }

                if (invoice.BuyerCompanyLegalAccountID.HasValue)
                {
                    dcbBuyerCompanyLegalAccount.SelectedId = (Guid) invoice.BuyerCompanyLegalAccountID;
                    dcbBuyerCompanyLegalAccount.SelectedText =
                        DataManager.CompanyLegalAccount.SelectById((Guid) invoice.BuyerCompanyLegalAccountID).Title;
                }

                ucBuyerContact.SelectedValue = invoice.BuyerContactID;
                ucExecutorContact.SelectedValue = invoice.ExecutorContactID;

                ucSendInvoiceBuyerContact.SelectedValue = invoice.BuyerContactID;
                ucSendInvoiceExecutorContact.SelectedValue = invoice.ExecutorContactID;

                if (invoice.PriceListID.HasValue)
                    ucPriceList.SelectedId = (Guid) invoice.PriceListID;

                if (invoice.OrderID.HasValue)
                {
                    dcbOrders.SelectedId = (Guid) invoice.OrderID;
                    dcbOrders.SelectedText = invoice.tbl_Order.Number;
                }

                rntxtPaid.Value = (double) invoice.Paid;
                lrlInvoiceAmount.Text = invoice.InvoiceAmount.ToString("F");

                rdpPaymentDateActual.SelectedDate = invoice.PaymentDateActual;
                rdpPaymentDatePlanned.SelectedDate = invoice.PaymentDatePlanned;

                chxIsPaymentDateFixedByContract.Checked = invoice.IsPaymentDateFixedByContract;
                chxIsExistBuyerComplaint.Checked = invoice.IsExistBuyerComplaint;
            }
            else
            {
                dcbExecutorCompany.SelectedIdNullable = CurrentUser.Instance.CompanyID;
                if (CurrentUser.Instance.CompanyID.HasValue)
                {
                    var company = DataManager.Company.SelectById(CurrentUser.Instance.SiteID,
                                                                 CurrentUser.Instance.CompanyID.Value);
                    if (company != null)
                    {
                        dcbExecutorCompany.SelectedText = company.Name;
                        var companyLegalAccount = DataManager.CompanyLegalAccount.SelectPrimary(company.ID);
                        if (companyLegalAccount != null)
                        {
                            dcbExecutorCompanyLegalAccount.SelectedIdNullable = companyLegalAccount.ID;
                            dcbExecutorCompanyLegalAccount.SelectedText = companyLegalAccount.Title;
                        }
                    }
                }

                ucExecutorContact.SelectedValue = CurrentUser.Instance.ContactID;
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var invoice = DataManager.Invoice.SelectById(SiteId, _invoiceId) ?? new tbl_Invoice();

            invoice.CreatedAt = (DateTime) rdpInvoiceDate.SelectedDate;
            invoice.InvoiceTypeID = dcbInvoiceType.SelectedId;
            invoice.InvoiceStatusID = int.Parse(ddlInvoiceStatus.SelectedValue);
            invoice.Note = txtNote.Text;

            if (dcbExecutorCompany.SelectedId != Guid.Empty)
                invoice.ExecutorCompanyID = dcbExecutorCompany.SelectedId;
            else
                invoice.ExecutorCompanyID = null;

            if (dcbBuyerCompany.SelectedId != Guid.Empty)
                invoice.BuyerCompanyID = dcbBuyerCompany.SelectedId;
            else
                invoice.BuyerCompanyID = null;

            if (dcbExecutorCompanyLegalAccount.SelectedId != Guid.Empty)
                invoice.ExecutorCompanyLegalAccountID = dcbExecutorCompanyLegalAccount.SelectedId;
            else
                invoice.ExecutorCompanyLegalAccountID = null;

            if (dcbBuyerCompanyLegalAccount.SelectedId != Guid.Empty)
                invoice.BuyerCompanyLegalAccountID = dcbBuyerCompanyLegalAccount.SelectedId;
            else
                invoice.BuyerCompanyLegalAccountID = null;

            invoice.BuyerContactID = ucBuyerContact.SelectedValue;
            invoice.ExecutorContactID = ucExecutorContact.SelectedValue;

            invoice.PaymentDatePlanned = rdpPaymentDatePlanned.SelectedDate;
            invoice.PaymentDateActual = rdpPaymentDateActual.SelectedDate;
            invoice.IsPaymentDateFixedByContract = chxIsPaymentDateFixedByContract.Checked;
            invoice.IsExistBuyerComplaint = chxIsExistBuyerComplaint.Checked;

            if (ucPriceList.SelectedId != Guid.Empty)
                invoice.PriceListID = ucPriceList.SelectedId;
            else
                invoice.PriceListID = null;

            if (dcbOrders.SelectedId != Guid.Empty)
                invoice.OrderID = dcbOrders.SelectedId;
            else
                invoice.OrderID = null;

            invoice.InvoiceAmount =
                ucInvoiceProducts.InvoiceProductsList.Sum(invoiceProduct => invoiceProduct.TotalAmount);

            if (rntxtPaid.Value.HasValue)
                invoice.Paid = (decimal) rntxtPaid.Value;
            else
                invoice.Paid = 0;

            invoice.tbl_Shipment.Clear();
            var shipmentIds = ucInvoiceShipments.InvoiceShipmentsList.Select(x => x.ID).ToList();
            var shipments = DataManager.Shipment.SelectAll(SiteId).Where(
                    o => shipmentIds.Contains(o.ID));

            foreach (var shipment in shipments)
                invoice.tbl_Shipment.Add(shipment);            

            if (invoice.ID == Guid.Empty)
            {
                invoice.SiteID = SiteId;
                invoice.OwnerID = CurrentUser.Instance.ContactID;
                var invoiceType = DataManager.InvoiceType.SelectById(SiteId, invoice.InvoiceTypeID);
                if (invoiceType != null && invoiceType.NumeratorID.HasValue)
                {
                    var documentNumerator = DocumentNumerator.GetNumber((Guid) invoiceType.NumeratorID,
                                                                        invoice.CreatedAt,
                                                                        invoiceType.tbl_Numerator.Mask, "tbl_Invoice");
                    invoice.Number = documentNumerator.Number;
                    invoice.SerialNumber = documentNumerator.SerialNumber;
                }

                DataManager.Invoice.Add(invoice);
            }
            else
                DataManager.Invoice.Update(invoice);

            DataManager.InvoiceProducts.Update(ucInvoiceProducts.InvoiceProductsList, invoice.ID);

            tagsInvoices.SaveTags(invoice.ID);

            Response.Redirect(UrlsData.AP_Invoices());
        }



        /// <summary>
        /// Handles the SelectedDateChanged event of the rdpInvoiceDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpInvoiceDate_SelectedDateChanged(object sender,
                                                          Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            ucInvoiceProducts.InvoiceDate = rdpInvoiceDate.SelectedDate;
        }



        /// <summary>
        /// Handles the OrderProductsChanged event of the ucInvoiceProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ucInvoiceProducts_OrderProductsChanged(object sender, EventArgs e)
        {
            lrlInvoiceAmount.Text =
                ucInvoiceProducts.InvoiceProductsList.Sum(invoiceProduct => invoiceProduct.TotalAmount).ToString("F");
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ucPriceList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucPriceList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucInvoiceProducts.PriceListId = ucPriceList.SelectedId;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbBuyerCompany control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbBuyerCompany_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            dcbBuyerCompanyLegalAccount.SelectedId = Guid.Empty;
            dcbBuyerCompanyLegalAccount.SelectedText = string.Empty;
            dcbBuyerCompanyLegalAccount.Filters.Clear();
            dcbBuyerCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                        {
                                                            Name = "IsActive",
                                                            DbType = DbType.Boolean,
                                                            Value = "TRUE"
                                                        });

            if (!string.IsNullOrEmpty(e.Value) && Guid.Parse(e.Value) != Guid.Empty)
                dcbBuyerCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                            {Name = "CompanyID", DbType = DbType.Guid, Value = e.Value});
            dcbBuyerCompanyLegalAccount.BindData();

            if (!string.IsNullOrEmpty(e.Value) && Guid.Parse(e.Value) != Guid.Empty)
            {
                var companyLegalAccount = DataManager.CompanyLegalAccount.SelectPrimary(Guid.Parse(e.Value));
                if (companyLegalAccount != null)
                {
                    dcbBuyerCompanyLegalAccount.SelectedIdNullable = companyLegalAccount.ID;
                    dcbBuyerCompanyLegalAccount.SelectedText = companyLegalAccount.Title;
                }
            }


            ucBuyerContact.CompanyId = dcbBuyerCompany.SelectedIdNullable;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbExecutorCompany control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbExecutorCompany_OnSelectedIndexChanged(object sender,
                                                                 RadComboBoxSelectedIndexChangedEventArgs e)
        {
            dcbExecutorCompanyLegalAccount.SelectedId = Guid.Empty;
            dcbExecutorCompanyLegalAccount.SelectedText = string.Empty;
            dcbExecutorCompanyLegalAccount.Filters.Clear();
            dcbExecutorCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                           {
                                                               Name = "IsActive",
                                                               DbType = DbType.Boolean,
                                                               Value = "TRUE"
                                                           });
            dcbExecutorCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                           {Name = "CompanyID", DbType = DbType.Guid, Value = e.Value});
            dcbExecutorCompanyLegalAccount.BindData();

            if (!string.IsNullOrEmpty(e.Value) && Guid.Parse(e.Value) != Guid.Empty)
            {
                var companyLegalAccount = DataManager.CompanyLegalAccount.SelectPrimary(Guid.Parse(e.Value));
                if (companyLegalAccount != null)
                {
                    dcbExecutorCompanyLegalAccount.SelectedIdNullable = companyLegalAccount.ID;
                    dcbExecutorCompanyLegalAccount.SelectedText = companyLegalAccount.Title;
                }
            }
        }




        /// <summary>
        /// Handles the OnItemDataBound event of the gridRequirementHistory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridInvoiceHistory_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;

                var lrlUserFullName = (Literal) item.FindControl("lrlUserFullName");
                var lrlInvoiceStatus = (Literal) item.FindControl("lrlInvoiceStatus");

                if (!string.IsNullOrEmpty(data["tbl_InvoiceHistory_InvoiceStatusID"].ToString()))
                    lrlInvoiceStatus.Text =
                        EnumHelper.GetEnumDescription(
                            (InvoiceStatus) int.Parse(data["tbl_InvoiceHistory_InvoiceStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>",
                                                         UrlsData.AP_Contact(
                                                             Guid.Parse(data["tbl_Contact_ID"].ToString())),
                                                         data["tbl_Contact_UserFullName"]);
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSendInvoice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSendInvoice_OnClick(object sender, EventArgs e)
        {
            if (chxForBuyer.Checked && ucSendInvoiceBuyerContact.SelectedValue.HasValue)
                InvoiceNotificationService.SendToContact(_invoiceId, ucSendInvoiceBuyerContact.SelectedValue.Value);

            if (chxForExecutor.Checked && ucSendInvoiceExecutorContact.SelectedValue.HasValue)
                InvoiceNotificationService.SendToContact(_invoiceId, ucSendInvoiceExecutorContact.SelectedValue.Value);

            Response.Redirect(UrlsData.AP_InvoiceEdit(_invoiceId));
        }

        protected void dcbBuyerCompanyLegalAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucInvoiceShipments.BuyerCompanyLegalAccountId = dcbBuyerCompanyLegalAccount.SelectedIdNullable;
        }

        protected void dcbExecutorCompanyLegalAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucInvoiceShipments.ExecutorCompanyLegalAccountId = dcbExecutorCompanyLegalAccount.SelectedIdNullable;
        }
    }
}