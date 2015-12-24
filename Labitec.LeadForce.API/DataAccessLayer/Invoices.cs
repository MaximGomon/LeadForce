using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Labitec.LeadForce.API.DataAccessLayer
{
    public static class Invoices
    {
        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static XDocument GetInvoices(Guid siteId, XDocument inputXml)
        {
            XDocument result;

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("API_GetInvoices", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    using (var reader = command.ExecuteXmlReader())
                    {
                        result = reader.IsStartElement()
                                     ? XDocument.Load(reader)
                                     : XDocument.Parse("<Invoices></Invoices>");
                    }
                }
            }

            return result;
        }




        /// <summary>
        /// Updates the invoice.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static InvoiceUpdateResult UpdateInvoice(Guid siteId, Guid? contactId, XDocument inputXml)
        {
            var result = new InvoiceUpdateResult();

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();

                using (var command = new SqlCommand("API_UpdateInvoice", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);                    
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    if (contactId.HasValue)
                        command.Parameters.AddWithValue("@ContactID", contactId.Value);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.InvoiceId = (Guid) reader["InvoiceID"];
                            result.IsPendingPayment = bool.Parse(reader["IsPendingPayment"].ToString());
                            result.IsNew = bool.Parse(reader["IsNew"].ToString());                            
                        }
                    }
                    
                    return result;
                }
            }
        }
    }

    public class InvoiceUpdateResult
    {
        public bool IsPendingPayment { get; set; }
        public bool IsNew { get; set; }
        public Guid InvoiceId { get; set; }        
    }
}