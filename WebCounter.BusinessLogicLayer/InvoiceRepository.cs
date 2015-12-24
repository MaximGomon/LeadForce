using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class InvoiceRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public InvoiceRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }




        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="invoiceId">The invoice id.</param>
        /// <returns></returns>
        public tbl_Invoice SelectById(Guid siteId, Guid invoiceId)
        {
            return _dataContext.tbl_Invoice.SingleOrDefault(i => i.SiteID == siteId && i.ID == invoiceId);
        }


        /// <summary>
        /// Selects for shipment.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="buyerCompanyLegalAccountId">The buyer company legal account id.</param>
        /// <param name="executorCompanyLegalAccountId">The executor company legal account id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice> SelectForShipment(decimal amount, Guid? buyerCompanyLegalAccountId, Guid? executorCompanyLegalAccountId)
        {
            return
                _dataContext.tbl_Invoice.Where(
                    o =>
                    o.InvoiceAmount == amount && o.BuyerCompanyLegalAccountID == buyerCompanyLegalAccountId &&
                    o.ExecutorCompanyLegalAccountID == executorCompanyLegalAccountId);
        }


        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_Invoice.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by buyer company id.
        /// </summary>
        /// <param name="buyerCompanyId">The buyer company id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice> SelectByBuyerCompanyId(Guid buyerCompanyId)
        {
            return _dataContext.tbl_Invoice.Where(o => o.BuyerCompanyID.HasValue && o.BuyerCompanyID == buyerCompanyId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="invoiceId">The invoice id.</param>
        /// <returns></returns>
        public tbl_Invoice SelectById(Guid invoiceId)
        {
            return _dataContext.tbl_Invoice.SingleOrDefault(i => i.ID == invoiceId);
        }



        /// <summary>
        /// Adds the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns></returns>
        public tbl_Invoice Add(tbl_Invoice invoice)
        {
            invoice.ID = Guid.NewGuid();
            invoice.CreatedAt = DateTime.Now;
            _dataContext.tbl_Invoice.AddObject(invoice);
            _dataContext.SaveChanges();

            AddHistory(new DataManager(), invoice);

            if (invoice.InvoiceStatusID == (int)InvoiceStatus.PendingPayment)
                InvoiceNotificationService.PendingPayment(invoice.ID);

            return invoice;
        }



        /// <summary>
        /// Selects the new sales potential client count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public int SelectNewSalesPotentialClientCount(Guid siteId, DateTime startDate, DateTime endDate)
        {
            var pSiteId = new SqlParameter("SiteID", siteId);
            var pStartDate = new SqlParameter("StartDate", startDate);
            var pEndDate = new SqlParameter("EndDate", endDate);
            var pInvoiceStatus = new SqlParameter("InvoiceStatus", (int) InvoiceStatus.Paid);

            return _dataContext.ExecuteStoreQuery<int>(@"
SELECT COUNT(DISTINCT C.ID) 
FROM tbl_Contact AS C LEFT JOIN tbl_Invoice AS I ON I.BuyerContactID = C.ID
WHERE C.SiteID = @SiteID AND C.CreatedAt >= @StartDate AND C.CreatedAt <= @EndDate AND
(I.InvoiceStatusID IS NULL OR I.InvoiceStatusID <> @InvoiceStatus) AND
C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)", pSiteId, pStartDate, pEndDate, pInvoiceStatus).FirstOrDefault();

            //var contacts = _dataContext.tbl_Contact.Where(
            //    o => o.SiteID == siteId && o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
            //         !_dataContext.tbl_Invoice.Any(x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.BuyerContactID == o.ID)).Select(o => o.ID).ToList();            

            //var contactIds = contacts.Select(contact => contact.ToString()).ToList();
            //return contactIds.Count(o => _dataContext.tbl_WorkflowParameter.Any(x => x.tbl_Workflow.SiteID == siteId && !string.IsNullOrEmpty(x.Value) && x.Value == o));
        }



        /// <summary>
        /// Selects the new sales potential client active count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public int SelectNewSalesPotentialClientActiveCount(Guid siteId, DateTime startDate, DateTime endDate)
        {
            //var contacts = _dataContext.tbl_Contact.Where(
            //                o => o.SiteID == siteId && o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
            //                !_dataContext.tbl_Invoice.Any(x => x.InvoiceStatusID == (int)InvoiceStatus.Paid && x.BuyerContactID == o.ID)
            //                && _dataContext.tbl_ContactActivity.Any(x => x.SiteID == siteId && x.CreatedAt >= startDate && x.CreatedAt <= endDate && x.ContactID == o.ID)).Select(o => o.ID).ToList();
            //var contactIds = contacts.Select(contact => contact.ToString()).ToList();
            //return contactIds.Count(o => _dataContext.tbl_WorkflowParameter.Any(x => !string.IsNullOrEmpty(x.Value) && x.Value == o));

            var pSiteId = new SqlParameter("SiteID", siteId);
            var pStartDate = new SqlParameter("StartDate", startDate);
            var pEndDate = new SqlParameter("EndDate", endDate);
            var pInvoiceStatus = new SqlParameter("InvoiceStatus", (int)InvoiceStatus.Paid);

            return _dataContext.ExecuteStoreQuery<int>(@"
SELECT COUNT(DISTINCT C.ID) 
FROM tbl_Contact AS C LEFT JOIN tbl_Invoice AS I ON I.BuyerContactID = C.ID 
LEFT JOIN tbl_ContactActivity AS CA ON CA.ContactID = C.ID
WHERE C.SiteID = @SiteID AND C.CreatedAt >= @StartDate AND C.CreatedAt <= @EndDate AND 
(I.InvoiceStatusID IS NULL OR I.InvoiceStatusID <> @InvoiceStatus) AND
CA.SiteID = @SiteID AND CA.CreatedAt >= @StartDate AND CA.CreatedAt <= @EndDate AND 
C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)", pSiteId, pStartDate, pEndDate, pInvoiceStatus).FirstOrDefault();
        }



        /// <summary>
        /// Selects the new sales invoice for payment.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice> SelectNewSalesInvoiceForPayment(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return SelectAll(siteId).Where(
                o => o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
                     (o.InvoiceStatusID == (int) InvoiceStatus.PendingPayment ||
                      o.InvoiceStatusID == (int) InvoiceStatus.PartialPaid) &&
                     !_dataContext.tbl_Invoice.Any(
                         x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.BuyerContactID == o.BuyerContactID));
        }



        /// <summary>
        /// Selects the new sales invoice for payment exposed.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice> SelectNewSalesInvoiceForPaymentExposed(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return SelectAll(siteId).Where(
                o =>
                o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
                o.InvoiceStatusID == (int) InvoiceStatus.Paid &&
                _dataContext.tbl_Invoice.Count(
                    x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.BuyerContactID == o.BuyerContactID) == 1);
        }



        /// <summary>
        /// Selects the new sales invoice for payment payed.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice> SelectNewSalesInvoiceForPaymentPayed(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return SelectAll(siteId).Where(
                o =>
                o.ID ==
                _dataContext.tbl_Invoice.Where(
                    x => x.SiteID == siteId && x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.PaymentDateActual.HasValue &&
                         x.PaymentDateActual.Value >= startDate && x.PaymentDateActual.Value <= endDate &&
                         x.BuyerContactID.HasValue && x.BuyerContactID == o.BuyerContactID).OrderBy(
                             x => x.PaymentDateActual).FirstOrDefault().ID);
        }



        /// <summary>
        /// Selects the repeat sales potential client count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public int SelectRepeatSalesPotentialClientCount(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return
                _dataContext.tbl_Contact.Count(
                    o => o.SiteID == siteId && o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
                         _dataContext.tbl_Invoice.Any(
                             x =>
                             x.SiteID == siteId && x.InvoiceStatusID == (int) InvoiceStatus.Paid &&
                             x.BuyerContactID == o.ID)
                         && _dataContext.tbl_MassWorkflowContact.Any(x => x.ContactID == o.ID));
        }



        /// <summary>
        /// Selects the repeat sales potential client active count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public int SelectRepeatSalesPotentialClientActiveCount(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return
                _dataContext.tbl_Contact.Count(
                    o => o.SiteID == siteId && o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
                         _dataContext.tbl_Invoice.Any(
                             x =>
                             x.SiteID == siteId && x.InvoiceStatusID == (int) InvoiceStatus.Paid &&
                             x.BuyerContactID == o.ID)
                         && _dataContext.tbl_MassWorkflowContact.Any(x => x.ContactID == o.ID)
                         &&
                         _dataContext.tbl_ContactActivity.Any(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate && x.ContactID == o.ID));
        }



        /// <summary>
        /// Selects the repeat sales invoice for payment.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice> SelectRepeatSalesInvoiceForPayment(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return _dataContext.tbl_Invoice.Where(
                o => o.SiteID == siteId && o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
                     (o.InvoiceStatusID == (int) InvoiceStatus.PendingPayment ||
                      o.InvoiceStatusID == (int) InvoiceStatus.PartialPaid) &&
                     _dataContext.tbl_Invoice.Any(
                         x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.BuyerContactID == o.BuyerContactID));
        }



        /// <summary>
        /// Selects the repeat sales invoice for payment exposed.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice> SelectRepeatSalesInvoiceForPaymentExposed(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return _dataContext.tbl_Invoice.Where(
                o => o.SiteID == siteId &&
                     o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
                     o.InvoiceStatusID == (int) InvoiceStatus.Paid &&
                     _dataContext.tbl_Invoice.Count(
                         x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.BuyerContactID == o.BuyerContactID) > 1
                     &&
                     o.ID ==
                     _dataContext.tbl_Invoice.Where(
                         x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.BuyerContactID == o.BuyerContactID).
                         OrderBy(x => x.CreatedAt).Skip(1).Take(1).FirstOrDefault().ID);
        }



        /// <summary>
        /// Selects the repeat sales invoice for payment payed.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IQueryable<tbl_Invoice> SelectRepeatSalesInvoiceForPaymentPayed(Guid siteId, DateTime startDate, DateTime endDate)
        {
            return _dataContext.tbl_Invoice.Where(
                o => o.SiteID == siteId &&
                o.ID ==
                _dataContext.tbl_Invoice.Where(
                    x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.PaymentDateActual.HasValue &&
                         x.PaymentDateActual.Value >= startDate && x.PaymentDateActual.Value <= endDate &&
                         x.BuyerContactID.HasValue && x.BuyerContactID == o.BuyerContactID).OrderBy(
                             x => x.PaymentDateActual).FirstOrDefault().ID);
        }



        /// <summary>
        /// Updates the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        public void Update(tbl_Invoice invoice)
        {
            var dataManager = new DataManager();
            var invoiceInDataBase = dataManager.Invoice.SelectById(invoice.SiteID, invoice.ID);
            invoice.ModifiedAt = DateTime.Now;
            _dataContext.SaveChanges();

            if (invoice.PaymentDatePlanned != invoiceInDataBase.PaymentDatePlanned || invoice.PaymentDateActual != invoiceInDataBase.PaymentDateActual ||
                invoice.InvoiceAmount != invoiceInDataBase.InvoiceAmount || invoice.InvoiceStatusID != invoiceInDataBase.InvoiceStatusID ||
                invoice.IsExistBuyerComplaint != invoiceInDataBase.IsExistBuyerComplaint || invoice.Note != invoiceInDataBase.Note)
            {
                AddHistory(dataManager, invoice);
            }

            if (invoice.InvoiceStatusID != invoiceInDataBase.InvoiceStatusID && invoice.InvoiceStatusID == (int)InvoiceStatus.PendingPayment)
                InvoiceNotificationService.PendingPayment(invoice.ID);
        }



        /// <summary>
        /// Adds the history.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="invoice">The invoice.</param>
        protected void AddHistory(DataManager dataManager, tbl_Invoice invoice)
        {
            var invoiceHistory = new tbl_InvoiceHistory
            {
                InvoiceID = invoice.ID,
                AuthorID = (Guid)CurrentUser.Instance.ContactID,
                PaymentDatePlanned = invoice.PaymentDatePlanned,
                PaymentDateActual = invoice.PaymentDateActual,
                InvoiceAmount = invoice.InvoiceAmount,
                InvoiceStatusID = invoice.InvoiceStatusID,
                IsExistBuyerComplaint = invoice.IsExistBuyerComplaint,
                Note = invoice.Note
            };

            dataManager.InvoiceHistory.Add(invoiceHistory);
        }



        /// <summary>
        /// Deletes the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="invoiceId">The invoice id.</param>
        public void DeleteById(Guid siteId, Guid invoiceId)
        {
            var invoice = SelectById(siteId, invoiceId);
            if (invoice != null)
            {
                _dataContext.tbl_Invoice.DeleteObject(invoice);
                _dataContext.SaveChanges();
            }
        }
    }
}