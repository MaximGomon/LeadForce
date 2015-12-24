using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebCounter.BusinessLogicLayer.DocumentManagement
{
    public class DocumentNumerator
    {
        public int SerialNumber { get; set; }
        public string Number { get; set; }



        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <param name="numeratorId">The numerator id.</param>
        /// <param name="documentDate">The document date.</param>
        /// <param name="mask">The mask.</param>
        /// <param name="dataSet">The data set.</param>
        /// <returns></returns>
        public static DocumentNumerator GetNumber(Guid numeratorId, DateTime documentDate, string mask, string dataSet)
        {
            var result = new DocumentNumerator();
            
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                var command = new SqlCommand("Numerator_GetNumber", connection) { CommandType = CommandType.StoredProcedure };

                command.Parameters.AddWithValue("@NumeratorID", numeratorId);
                command.Parameters.AddWithValue("@Date", documentDate);
                command.Parameters.AddWithValue("@DataSet", dataSet);

                using(var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        result.SerialNumber = (int)reader["MaxNumber"];
                    }   
                }                
            }

            result.Number = mask.Replace("NUMBER", result.SerialNumber.ToString())
                              .Replace("YYYY", documentDate.ToString("yyyy"))
                              .Replace("MM", documentDate.ToString("MM"))
                              .Replace("DD", documentDate.ToString("dd"))
                              .Replace("YY", documentDate.ToString("yy"));

            return result;
        }
    }
}
