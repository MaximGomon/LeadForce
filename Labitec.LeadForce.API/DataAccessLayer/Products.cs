using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Labitec.LeadForce.API.DataAccessLayer
{
    public static class Products
    {
        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static XDocument GetProducts(Guid siteId, XDocument inputXml)
        {
            XDocument result;

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("API_GetProducts", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    using (var reader = command.ExecuteXmlReader())
                    {
                        result = reader.IsStartElement()
                                     ? XDocument.Load(reader)
                                     : XDocument.Parse("<Products></Products>");
                    }
                }
            }

            return result;
        }




        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="inputXml">The input XML.</param>
        public static void UpdateProduct(Guid siteId, Guid? contactId, XDocument inputXml)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();

                using (var command = new SqlCommand("API_UpdateProduct", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);                    
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));
                    if (contactId.HasValue)
                        command.Parameters.AddWithValue("@ContactID", contactId.Value);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}