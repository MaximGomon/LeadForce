using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Labitec.LeadForce.API.DataAccessLayer
{
    public static class Contacts
    {
        /// <summary>
        /// Gets the contacts.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static XDocument GetContacts(Guid siteId, XDocument inputXml)
        {
            XDocument result;

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("API_GetContacts", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    using (var reader = command.ExecuteXmlReader())
                    {
                        result = reader.IsStartElement()
                                     ? XDocument.Load(reader)
                                     : XDocument.Parse("<Contacts></Contacts>");
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Updates the contact.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="inputXml">The input XML.</param>
        public static void UpdateContact(Guid siteId, Guid userId, XDocument inputXml)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("API_UpdateContact", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}