using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Labitec.LeadForce.API.DataAccessLayer
{
    public class Actions
    {
        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static XDocument GetActions(Guid siteId, XDocument inputXml)
        {
            XDocument result;

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("API_GetActions", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    using (var reader = command.ExecuteXmlReader())
                    {
                        result = reader.IsStartElement()
                                     ? XDocument.Load(reader)
                                     : XDocument.Parse("<Actions></Actions>");
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Creates the action.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="inputXml">The input XML.</param>
        public static void CreateAction(Guid siteId, XDocument inputXml)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using(var command = new SqlCommand("API_CreateAction", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@ParamsXml", inputXml.ToString(SaveOptions.DisableFormatting));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}