using System;
using System.Reflection;
using System.Xml.Linq;
using Labitec.LeadForce.API.Core;
using Labitec.LeadForce.API.Core.Enumerations;
using Labitec.LeadForce.API.DataAccessLayer;
using Labitec.LeadForce.API.Interface;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Services;

namespace Labitec.LeadForce.API
{
    public class Invoice : IInvoice
    {
        private const string Version = "1.0";
        private XDocument _result = new XDocument();
        private readonly APIAuthorization _authorization = new APIAuthorization();

        Invoice()
        {
            if (_result.Document != null)
                _result.Document.Add(new XElement("LeadForceResponse", new XAttribute("version", Version)));
        }


        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string GetInvoices(Guid siteId, string username, string password, string xml)
        {
            var user = _authorization.Authorize(siteId, username, password);
            if (user == null || !Access.Check(user, "API").Read)
                return StatusHelper.FormatMessage(StatusCodes.AccessDenied, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);

            XDocument inputXml;

            try
            {
                inputXml = XDocument.Parse(xml);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.GetInvoices error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                var invoices = Invoices.GetInvoices(siteId, inputXml);
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);
                _result.Document.Element("LeadForceResponse").Add(new XElement("Result", invoices.Document.FirstNode));
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.GetInvoices error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }



        /// <summary>
        /// Updates the invoice.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string UpdateInvoice(Guid siteId, string username, string password, string xml)
        {
            var user = _authorization.Authorize(siteId, username, password);
            if (user == null || !Access.Check(user, "API").Write)
                return StatusHelper.FormatMessage(StatusCodes.AccessDenied, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);

            XDocument inputXml;

            try
            {
                inputXml = XDocument.Parse(xml);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.UpdateInvoice error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                var result = Invoices.UpdateInvoice(siteId, user.ContactID, inputXml.Document);
                if (result.IsNew)
                {
                    var dataManager = new DataManager();
                    var invoice = dataManager.Invoice.SelectById(siteId, result.InvoiceId);
                    var invoiceType = dataManager.InvoiceType.SelectById(siteId, invoice.InvoiceTypeID);
                    var documentNumerator = DocumentNumerator.GetNumber((Guid)invoiceType.NumeratorID, invoice.CreatedAt, invoiceType.tbl_Numerator.Mask, "tbl_Invoice");
                    invoice.Number = documentNumerator.Number;
                    invoice.SerialNumber = documentNumerator.SerialNumber;
                    dataManager.Invoice.Update(invoice);                    
                }
                if (result.IsPendingPayment && !result.IsNew)                
                    InvoiceNotificationService.PendingPayment(result.InvoiceId);                
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.UpdateInvoice error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }
    }
}
