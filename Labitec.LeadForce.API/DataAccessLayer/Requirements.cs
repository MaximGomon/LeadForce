using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Labitec.LeadForce.API.DataAccessLayer
{
    public static class Requirements
    {
        /// <summary>
        /// Gets the requirements.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static XDocument GetRequirements(Guid siteId, XDocument inputXml)
        {
            XDocument result;

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("API_GetRequirements", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    using (var reader = command.ExecuteXmlReader())
                    {
                        result = reader.IsStartElement()
                                     ? XDocument.Load(reader)
                                     : XDocument.Parse("<Requirements></Requirements>");
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Updates the requirement.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static Guid? UpdateRequirement(Guid siteId, Guid userId, XDocument inputXml)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();

                using (var command = new SqlCommand("API_UpdateRequirement", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    var result = command.ExecuteScalar();
                    if (result is DBNull)
                        return null;

                    return (Guid) result;
                }
            }
        }
    }
}