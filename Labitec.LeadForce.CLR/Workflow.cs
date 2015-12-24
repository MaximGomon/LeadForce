using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.SqlServer.Server;

public class StoredProcedures
{
    /// <summary>
    /// Does the external request.
    /// </summary>
    /// <param name="workflowTemplateElementId">The workflow template element id.</param>
    /// <param name="workflowParameterId">The workflow parameter id.</param>
    /// <returns></returns>
    [SqlProcedure]
    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static string DoExternalRequest(SqlGuid workflowTemplateElementId, SqlGuid workflowParameterId)
    {
        var status = "0";
        var parameters = new Dictionary<string, string>();
        var externalRequests = new Dictionary<string, string>();
        var contact = new DataTable();

        try
        {
            using (SqlConnection conn = new SqlConnection("context connection=true"))
            {
                conn.Open();
                var query = string.Empty;

                query = string.Format("SELECT * FROM tbl_WorkflowTemplateElementParameter WHERE WorkflowTemplateElementID=N'{0}'", workflowTemplateElementId);
                using (var command = new SqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                parameters.Add((string)reader["Name"], (string)reader["Value"]);
                            }
                            reader.NextResult();
                        }
                    }
                }

                query = string.Format("SELECT * FROM tbl_WorkflowTemplateElementExternalRequest WHERE WorkflowTemplateElementID=N'{0}'", workflowTemplateElementId);
                using (var command = new SqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                externalRequests.Add((string)reader["Name"], (string)reader["Value"]);
                            }
                            reader.NextResult();
                        }
                    }
                }

                query = string.Format("SELECT TOP 1 * FROM tbl_Contact WHERE ID='{0}'", workflowParameterId);
                using (var command = new SqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            contact.Load(reader);
                    }
                }

                conn.Close();
                conn.Dispose();
            }

            WebRequest request = WebRequest.Create(parameters.FirstOrDefault(a => a.Key == "ExternalRequestURL").Value);
            request.Method = parameters.FirstOrDefault(a => a.Key == "ExternalRequestType").Value;

            var postData = string.Empty;
            if (externalRequests.Count > 0)
            {
                foreach (var externalRequest in externalRequests)
                {
                    //postData += string.Format("{0}={1}&", externalRequest.Key, HttpUtility.UrlEncode(externalRequest.Value, Encoding.GetEncoding(1251)));
                    postData += string.Format("{0}={1}&", externalRequest.Key, externalRequest.Value);
                }
                postData = postData.TrimEnd(new[] { '&' });
            }

            postData = postData.Replace("#User.UserFullName#", contact.Rows[0]["UserFullName"].ToString())
            .Replace("#User.LastName#", contact.Rows[0]["Surname"].ToString())
            .Replace("#User.FirstName#", contact.Rows[0]["Name"].ToString())
            .Replace("#User.MiddleName#", contact.Rows[0]["Patronymic"].ToString())
            .Replace("#User.Email#", contact.Rows[0]["Email"].ToString())
            .Replace("#User.Phone#", contact.Rows[0]["Phone"].ToString())
            .Replace("#User.Score#", contact.Rows[0]["Score"].ToString());

            var r = new Regex(@"#User.[\S]+?#");
            var results = r.Matches(postData);
            foreach (Match result in results)
            {
                var siteColumnId = GetSiteColumnId(contact.Rows[0]["SiteID"].ToString(), result.Value.Replace("#User.", "").Replace("#", ""));
                if (!string.IsNullOrEmpty(siteColumnId))
                {
                    var contactColumnValue = GetContactColumnValues(workflowParameterId.ToString(), siteColumnId);
                    if (contactColumnValue != null && contactColumnValue.Rows.Count > 0)
                    {
                        switch (contactColumnValue.Rows[0]["TypeID"].ToString())
                        {
                            case "1":
                            case "4":
                            case "5":
                                postData = postData.Replace(result.Value, contactColumnValue.Rows[0]["StringValue"].ToString().Replace("[BR]", "\n"));
                                break;
                            case "2":
                                postData = postData.Replace(result.Value, ((DateTime)contactColumnValue.Rows[0]["DateValue"]).ToString("dd.MM.yyyy HH:mm"));
                                break;
                            case "3":
                                postData = postData.Replace(result.Value, contactColumnValue.Rows[0]["Value"].ToString());
                                break;
                        }
                    }
                    else
                    {
                        postData = postData.Replace(result.Value, "");
                    }
                }
            }
            
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            if ((int) (((HttpWebResponse) response)).StatusCode >= 200 &&
                (int) (((HttpWebResponse) response)).StatusCode < 400)
                status = "1";

            dataStream.Close();
            response.Close();
        }
        catch (Exception)
        {
        }

        return status;
    }



    /// <summary>
    /// Gets the site column id.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static string GetSiteColumnId(string siteId, string code)
    {
        string result;

        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            conn.Open();

            var query = string.Format("SELECT TOP 1 ID FROM tbl_SiteColumns WHERE SiteID=N'{0}' AND Code=N'{1}'", siteId, code);
            using (var command = new SqlCommand(query, conn))
            {
                result = command.ExecuteScalar().ToString();
            }

            conn.Close();
            conn.Dispose();
        }

        return result;
    }



    /// <summary>
    /// Gets the contact column values.
    /// </summary>
    /// <param name="contactId">The contact id.</param>
    /// <param name="siteColumnId">The site column id.</param>
    /// <returns></returns>
    public static DataTable GetContactColumnValues(string contactId, string siteColumnId)
    {
        var dt = new DataTable();

        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            conn.Open();

            var query = string.Format(@"SELECT TOP 1 tbl_SiteColumns.TypeID, tbl_ContactColumnValues.StringValue, tbl_ContactColumnValues.DateValue, tbl_SiteColumnValues.Value
                                        FROM tbl_ContactColumnValues
                                        LEFT JOIN tbl_SiteColumns ON tbl_SiteColumns.ID=tbl_ContactColumnValues.SiteColumnID
                                        LEFT JOIN tbl_SiteColumnValues ON tbl_SiteColumnValues.ID=tbl_ContactColumnValues.SiteColumnValueID
                                        WHERE tbl_ContactColumnValues.ContactID=N'{0}' AND tbl_ContactColumnValues.SiteColumnID=N'{1}'", contactId, siteColumnId);
            using (var command = new SqlCommand(query, conn))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        dt.Load(reader);
                }
            }

            conn.Close();
            conn.Dispose();
        }

        return dt;
    }
}