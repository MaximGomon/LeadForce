using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Labitec.DataFeed;
using Labitec.DataFeed.Common;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WebCounterServiceRepository
    {
        protected LocationInfo GetLocation(string userIp)
        {
            var locationInfo = HttpContext.Current != null ? (LocationInfo)HttpContext.Current.Cache[userIp] : null;
            if (locationInfo == null)
            {
                var locationDetect = new LocationDetection(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                locationDetect.Detect(userIp);
                locationInfo = locationDetect.LocationInfo;
                if (HttpContext.Current != null && locationInfo != null)
                    HttpContext.Current.Cache.Insert(userIp, locationInfo, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
            }                        
            return locationInfo;
        }


        public void Contact_Add(AddContactParams contactParams)
        {
            var locationInfo = GetLocation(contactParams.UserIP);            

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("Contact_Add", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@ID", contactParams.ID);
                    command.Parameters.AddWithValue("@SiteID", contactParams.SiteID);

                    if (contactParams.RefferURL != null)
                        command.Parameters.AddWithValue("@RefferURL", HttpUtility.UrlDecode(contactParams.RefferURL));
                    else
                        command.Parameters.AddWithValue("@RefferURL", string.Empty);

                    if (contactParams.UserIP != null)
                        command.Parameters.AddWithValue("@UserIP", contactParams.UserIP);
                    else
                        command.Parameters.AddWithValue("@UserIP", DBNull.Value);

                    if (contactParams.UserAgent != null)
                        command.Parameters.AddWithValue("@UserAgent", contactParams.UserAgent);
                    else
                        command.Parameters.AddWithValue("@UserAgent", DBNull.Value);

                    command.Parameters.AddWithValue("@BrowserName", contactParams.BrowserName);

                    if (contactParams.BrowserVersion != null)
                        command.Parameters.AddWithValue("@BrowserVersion", contactParams.BrowserVersion);
                    else
                        command.Parameters.AddWithValue("@BrowserVersion", DBNull.Value);

                    command.Parameters.AddWithValue("@OperatingSystemName", contactParams.OperatingSystemName);

                    if (contactParams.OperatingSystemVersion != null)
                        command.Parameters.AddWithValue("@OperatingSystemVersion", contactParams.OperatingSystemVersion);
                    else
                        command.Parameters.AddWithValue("@OperatingSystemVersion", DBNull.Value);

                    if (contactParams.OperatingSystemVersion != null)
                        command.Parameters.AddWithValue("@Resolution", contactParams.Resolution);
                    else
                        command.Parameters.AddWithValue("@Resolution", DBNull.Value);

                    if (contactParams.MobileDevice != null)
                        command.Parameters.AddWithValue("@MobileDevice", contactParams.MobileDevice);
                    else
                        command.Parameters.AddWithValue("@MobileDevice", DBNull.Value);

                    command.Parameters.AddWithValue("@ActivityTypeID", (int) contactParams.ActivityTypeID);

                    if (contactParams.ActivityCode != null)
                        command.Parameters.AddWithValue("@ActivityCode", contactParams.ActivityCode);
                    else
                        command.Parameters.AddWithValue("@ActivityCode", DBNull.Value);

                    if (contactParams.SessionSource != null)
                    {
                        if (contactParams.SessionSource.EnterPointURL != null)
                            command.Parameters.AddWithValue("@EnterPointURL",
                                                            HttpUtility.UrlDecode(
                                                                contactParams.SessionSource.EnterPointURL));
                        else
                            command.Parameters.AddWithValue("@EnterPointURL", DBNull.Value);

                        if (contactParams.SessionSource.AdvertisingPlatform != null)
                            command.Parameters.AddWithValue("@AdvertisingPlatform",
                                                            contactParams.SessionSource.AdvertisingPlatform);
                        else
                            command.Parameters.AddWithValue("@AdvertisingPlatform", DBNull.Value);

                        if (contactParams.SessionSource.AdvertisingType != null)
                            command.Parameters.AddWithValue("@AdvertisingType",
                                                            contactParams.SessionSource.AdvertisingType);
                        else
                            command.Parameters.AddWithValue("@AdvertisingType", DBNull.Value);

                        if (contactParams.SessionSource.Keywords != null)
                            command.Parameters.AddWithValue("@Keywords",
                                                            HttpUtility.UrlDecode(contactParams.SessionSource.Keywords));
                        else
                            command.Parameters.AddWithValue("@Keywords", DBNull.Value);

                        if (contactParams.SessionSource.AdvertisingCampaign != null)
                            command.Parameters.AddWithValue("@AdvertisingCampaign",
                                                            HttpUtility.UrlDecode(
                                                                contactParams.SessionSource.AdvertisingCampaign));
                        else
                            command.Parameters.AddWithValue("@AdvertisingCampaign", DBNull.Value);

                        if (contactParams.SessionSource.Content != null)
                            command.Parameters.AddWithValue("@Content",
                                                            HttpUtility.UrlDecode(contactParams.SessionSource.Content));
                        else
                            command.Parameters.AddWithValue("@Content", DBNull.Value);

                        if (contactParams.SessionSource.CameFromUrl != null)
                            command.Parameters.AddWithValue("@CameFromUrl",
                                                            HttpUtility.UrlDecode(
                                                                contactParams.SessionSource.CameFromUrl));
                        else
                            command.Parameters.AddWithValue("@CameFromUrl", DBNull.Value);
                    }

                    if (locationInfo != null)
                    {
                        if (locationInfo.CityId != null)
                            command.Parameters.AddWithValue("@CityId", locationInfo.CityId);
                        else
                            command.Parameters.AddWithValue("@CityId", DBNull.Value);

                        if (locationInfo.CountryId != null)
                            command.Parameters.AddWithValue("@CountryId", locationInfo.CountryId);
                        else
                            command.Parameters.AddWithValue("@CountryId", DBNull.Value);
                    }

                    if (contactParams.RefferID != null)
                        command.Parameters.AddWithValue("@RefferID", contactParams.RefferID);
                    else
                        command.Parameters.AddWithValue("@RefferID", DBNull.Value);

                    command.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void ContactActivity_Add(AddContactParams contactParams)
        {
            ContactActivity_Add(contactParams, DateTime.Now);
        }


        public void ContactActivity_Add(AddContactParams contactParams, DateTime actionDate)
        {
            var locationInfo = GetLocation(contactParams.UserIP);
            
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("Contact_Add", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@ID", contactParams.ID);
                    command.Parameters.AddWithValue("@SiteID", contactParams.SiteID);

                    if (contactParams.RefferURL != null)
                        command.Parameters.AddWithValue("@RefferURL", HttpUtility.UrlDecode(contactParams.RefferURL));
                    else
                        command.Parameters.AddWithValue("@RefferURL", DBNull.Value);

                    if (contactParams.UserIP != null)
                        command.Parameters.AddWithValue("@UserIP", contactParams.UserIP);
                    else
                        command.Parameters.AddWithValue("@UserIP", DBNull.Value);

                    if (contactParams.UserAgent != null)
                        command.Parameters.AddWithValue("@UserAgent", contactParams.UserAgent);
                    else
                        command.Parameters.AddWithValue("@UserAgent", DBNull.Value);

                    command.Parameters.AddWithValue("@BrowserName", contactParams.BrowserName);

                    if (contactParams.BrowserVersion != null)
                        command.Parameters.AddWithValue("@BrowserVersion", contactParams.BrowserVersion);
                    else
                        command.Parameters.AddWithValue("@BrowserVersion", DBNull.Value);

                    command.Parameters.AddWithValue("@OperatingSystemName", contactParams.OperatingSystemName);

                    if (contactParams.OperatingSystemVersion != null)
                        command.Parameters.AddWithValue("@OperatingSystemVersion", contactParams.OperatingSystemVersion);
                    else
                        command.Parameters.AddWithValue("@OperatingSystemVersion", DBNull.Value);

                    if (contactParams.OperatingSystemVersion != null)
                        command.Parameters.AddWithValue("@Resolution", contactParams.Resolution);
                    else
                        command.Parameters.AddWithValue("@Resolution", DBNull.Value);

                    if (contactParams.MobileDevice != null)
                        command.Parameters.AddWithValue("@MobileDevice", contactParams.MobileDevice);
                    else
                        command.Parameters.AddWithValue("@MobileDevice", DBNull.Value);

                    command.Parameters.AddWithValue("@ActivityTypeID", (int) contactParams.ActivityTypeID);

                    if (contactParams.ActivityCode != null)
                        command.Parameters.AddWithValue("@ActivityCode", contactParams.ActivityCode);
                    else
                        command.Parameters.AddWithValue("@ActivityCode", DBNull.Value);

                    if (contactParams.SessionSource != null)
                    {
                        if (contactParams.SessionSource.EnterPointURL != null)
                            command.Parameters.AddWithValue("@EnterPointURL",
                                                            HttpUtility.UrlDecode(
                                                                contactParams.SessionSource.EnterPointURL));
                        else
                            command.Parameters.AddWithValue("@EnterPointURL", DBNull.Value);

                        if (contactParams.SessionSource.AdvertisingPlatform != null)
                            command.Parameters.AddWithValue("@AdvertisingPlatform",
                                                            contactParams.SessionSource.AdvertisingPlatform);
                        else
                            command.Parameters.AddWithValue("@AdvertisingPlatform", DBNull.Value);

                        if (contactParams.SessionSource.AdvertisingType != null)
                            command.Parameters.AddWithValue("@AdvertisingType",
                                                            contactParams.SessionSource.AdvertisingType);
                        else
                            command.Parameters.AddWithValue("@AdvertisingType", DBNull.Value);

                        if (contactParams.SessionSource.Keywords != null)
                            command.Parameters.AddWithValue("@Keywords",
                                                            HttpUtility.UrlDecode(contactParams.SessionSource.Keywords));
                        else
                            command.Parameters.AddWithValue("@Keywords", DBNull.Value);

                        if (contactParams.SessionSource.AdvertisingCampaign != null)
                            command.Parameters.AddWithValue("@AdvertisingCampaign",
                                                            HttpUtility.UrlDecode(
                                                                contactParams.SessionSource.AdvertisingCampaign));
                        else
                            command.Parameters.AddWithValue("@AdvertisingCampaign", DBNull.Value);

                        if (contactParams.SessionSource.Content != null)
                            command.Parameters.AddWithValue("@Content",
                                                            HttpUtility.UrlDecode(contactParams.SessionSource.Content));
                        else
                            command.Parameters.AddWithValue("@Content", DBNull.Value);

                        if (contactParams.SessionSource.CameFromUrl != null)
                            command.Parameters.AddWithValue("@CameFromUrl",
                                                            HttpUtility.UrlDecode(
                                                                contactParams.SessionSource.CameFromUrl));
                        else
                            command.Parameters.AddWithValue("@CameFromUrl", DBNull.Value);
                    }

                    if (locationInfo != null)
                    {
                        if (locationInfo.CityId != null)
                            command.Parameters.AddWithValue("@CityId", locationInfo.CityId);
                        else
                            command.Parameters.AddWithValue("@CityId", DBNull.Value);

                        if (locationInfo.CountryId != null)
                            command.Parameters.AddWithValue("@CountryId", locationInfo.CountryId);
                        else
                            command.Parameters.AddWithValue("@CountryId", DBNull.Value);
                    }

                    if (contactParams.RefferID != null)
                        command.Parameters.AddWithValue("@RefferID", contactParams.RefferID);
                    else
                        command.Parameters.AddWithValue("@RefferID", DBNull.Value);

                    command.Parameters.AddWithValue("@CurrentDate", actionDate);

                    command.ExecuteNonQuery();
                }
            }
        }



        public LinkProcessingMap LinkProcessing(Guid siteActionLinkID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                LinkProcessingMap result = null;

                connection.Open();
                using (var command = new SqlCommand("LinkProcessing", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteActionLinkID", siteActionLinkID);
                    command.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new LinkProcessingMap
                            {
                                ID = (Guid)reader["ID"],
                                ContactID = (Guid)reader["ContactID"],
                                SiteActionID = reader["SiteActionID"] != DBNull.Value ? (Guid?)reader["SiteActionID"] : null,
                                SiteActionTemplateID = reader["SiteActionTemplateID"] != DBNull.Value ? (Guid?)reader["SiteActionTemplateID"] : null,
                                SiteActivityRuleID = reader["SiteActivityRuleID"] != DBNull.Value ? (Guid?)reader["SiteActivityRuleID"] : null,
                                LinkURL = reader["LinkURL"] != DBNull.Value ? (string)reader["LinkURL"] : null,
                                ActionLinkDate = reader["ActionLinkDate"] != DBNull.Value ? (DateTime?)reader["ActionLinkDate"] : null,
                                SiteID = (Guid)reader["SiteID"],
                                SiteActivityRuleTypeID = reader["SiteActivityRuleTypeID"] != DBNull.Value ? (int?)reader["SiteActivityRuleTypeID"] : null,
                                SiteActivityRuleCode = reader["SiteActivityRuleCode"] != DBNull.Value ? (string)reader["SiteActivityRuleCode"] : null,
                                SiteActivityRuleURL = reader["SiteActivityRuleURL"] != DBNull.Value ? (string)reader["SiteActivityRuleURL"] : null
                            };
                        }
                    }
                }
                return result;
            }
        }



        public tbl_SiteActionLink SiteActionLink_SelectById(Guid siteActionLinkID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_SiteActionLink result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_SiteActionLink WHERE ID='{0}'", siteActionLinkID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_SiteActionLink
                            {
                                ID = (Guid)reader["ID"],
                                ContactID = (Guid)reader["ContactID"],
                                SiteActionID = reader["SiteActionID"] != DBNull.Value ? (Guid?)reader["SiteActionID"] : null,
                                SiteActionTemplateID = reader["SiteActionTemplateID"] != DBNull.Value ? (Guid?)reader["SiteActionTemplateID"] : null,
                                SiteActivityRuleID = reader["SiteActivityRuleID"] != DBNull.Value ? (Guid?)reader["SiteActivityRuleID"] : null,
                                LinkURL = reader["LinkURL"] != DBNull.Value ? (string)reader["LinkURL"] : null,
                                ActionLinkDate = reader["ActionLinkDate"] != DBNull.Value ? (DateTime?)reader["ActionLinkDate"] : null
                            };
                        }
                    }
                }
                return result;
            }
        }



        public bool CheckForm(Guid siteID, Guid contactID, string ActivityCode, int Mode, int? FromVisit, int? Through, int? Period, FormContactCategory ContactCategory)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                using (var command = new SqlCommand("CheckForm", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteID);
                    command.Parameters.AddWithValue("@ContactID", contactID);
                    command.Parameters.AddWithValue("@ActivityCode", ActivityCode);
                    command.Parameters.AddWithValue("@Mode", Mode);

                    if (FromVisit != null)
                        command.Parameters.AddWithValue("@FromVisit", FromVisit);
                    else
                        command.Parameters.AddWithValue("@FromVisit", DBNull.Value);

                    if (Through != null)
                        command.Parameters.AddWithValue("@Through", Through);
                    else
                        command.Parameters.AddWithValue("@Through", DBNull.Value);

                    if (Period != null)
                        command.Parameters.AddWithValue("@Period", Period);
                    else
                        command.Parameters.AddWithValue("@Period", DBNull.Value);

                    command.Parameters.AddWithValue("@ContactCategory", (int)ContactCategory);

                    command.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                    return (bool) command.ExecuteScalar();
                }
            }
        }



        public tbl_SiteActivityRules SiteActivityRules_SelectById(Guid siteActivityRuleID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_SiteActivityRules result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_SiteActivityRules WHERE ID='{0}'", siteActivityRuleID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_SiteActivityRules
                                         {
                                             ID = (Guid) reader["ID"],
                                             SiteID = (Guid) reader["SiteID"],
                                             Name = (string) reader["Name"],
                                             RuleTypeID = (int) reader["RuleTypeID"],
                                             Code = reader["Code"] != DBNull.Value ? (string) reader["Code"] : null,
                                             URL = reader["URL"] != DBNull.Value ? (string) reader["URL"] : null,
                                             UserFullName = (bool) reader["UserFullName"],
                                             Email = (bool) reader["Email"],
                                             Phone = (bool) reader["Phone"],
                                             FormWidth =
                                                 reader["FormWidth"] != DBNull.Value ? (int?) reader["FormWidth"] : null,
                                             CountExtraFields =
                                                 reader["CountExtraFields"] != DBNull.Value
                                                     ? (int?) reader["CountExtraFields"]
                                                     : null,
                                             ExternalFormURL =
                                                 reader["ExternalFormURL"] != DBNull.Value
                                                     ? (string) reader["ExternalFormURL"]
                                                     : null,
                                             RepostURL =
                                                 reader["RepostURL"] != DBNull.Value
                                                     ? (string) reader["RepostURL"]
                                                     : null
                                         };
                        }
                    }
                }
                return result;
                
            }
        }


        public tbl_SiteActivityRules SiteActivityRules_SelectByCode(Guid siteID, string code)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_SiteActivityRules result = null;
 
                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_SiteActivityRules WHERE SiteID='{0}' AND Code=N'{1}'", siteID, code);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_SiteActivityRules
                                         {
                                             ID = (Guid) reader["ID"],
                                             SiteID = (Guid) reader["SiteID"],
                                             Name = (string) reader["Name"],
                                             RuleTypeID = (int) reader["RuleTypeID"],
                                             Code = reader["Code"] != DBNull.Value ? (string) reader["Code"] : null,
                                             URL = reader["URL"] != DBNull.Value ? (string) reader["URL"] : null,
                                             UserFullName = (bool) reader["UserFullName"],
                                             Email = (bool) reader["Email"],
                                             Phone = (bool) reader["Phone"],
                                             FormWidth =
                                                 reader["FormWidth"] != DBNull.Value ? (int?) reader["FormWidth"] : null,
                                             CountExtraFields =
                                                 reader["CountExtraFields"] != DBNull.Value
                                                     ? (int?) reader["CountExtraFields"]
                                                     : null,
                                             ExternalFormURL =
                                                 reader["ExternalFormURL"] != DBNull.Value
                                                     ? (string) reader["ExternalFormURL"]
                                                     : null,
                                             RepostURL =
                                                 reader["RepostURL"] != DBNull.Value
                                                     ? (string) reader["RepostURL"]
                                                     : null,
                                             CSSButton =
                                                 reader["CSSButton"] != DBNull.Value
                                                     ? (string) reader["CSSButton"]
                                                     : null,
                                             CSSForm =
                                                 reader["CSSForm"] != DBNull.Value
                                                     ? (string) reader["CSSForm"]
                                                     : null,
                                             TextButton =
                                                 reader["TextButton"] != DBNull.Value
                                                     ? (string) reader["TextButton"]
                                                     : null,
                                             ErrorMessage =
                                                 reader["ErrorMessage"] != DBNull.Value
                                                     ? (string) reader["ErrorMessage"]
                                                     : null,
                                             Skin = (int) reader["Skin"],
                                             ActionOnFillForm = (int) reader["ActionOnFillForm"],
                                             SendFields = (bool) reader["SendFields"],
                                             SuccessMessage =
                                                 reader["SuccessMessage"] != DBNull.Value
                                                     ? (string) reader["SuccessMessage"]
                                                     : null,
                                             YandexGoals = reader["YandexGoals"] != DBNull.Value
                                             ? (string)reader["YandexGoals"]
                                             : null
                                            
                                         };
                        }
                    }
                }
                return result;
            }
        }



        public tbl_Links Links_SelectByCode(Guid siteID, string code)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_Links result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_Links WHERE SiteID='{0}' AND Code=N'{1}'", siteID, code);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_Links
                            {
                                ID = (Guid)reader["ID"],
                                SiteID = (Guid)reader["SiteID"],
                                Name = (string)reader["Name"],
                                RuleTypeID = (int)reader["RuleTypeID"],
                                Code = reader["Code"] != DBNull.Value ? (string)reader["Code"] : null,
                                URL = reader["URL"] != DBNull.Value ? (string)reader["URL"] : null,                                
                            };
                        }
                    }
                }
                return result;
            }
        }



        public List<SiteActivityRuleLayoutParams> SiteActivityRuleLayout_SelectBySiteActivityRuleID(Guid siteActivityRuleID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<SiteActivityRuleLayoutParams>();

                connection.Open();
                var query = string.Format("SELECT tbl_SiteActivityRuleLayout.*, tbl_SiteColumns.Name AS SiteColumnName, tbl_SiteColumns.TypeID AS SiteColumnTypeID FROM tbl_SiteActivityRuleLayout LEFT JOIN tbl_SiteColumns ON tbl_SiteColumns.ID=tbl_SiteActivityRuleLayout.SiteColumnID WHERE tbl_SiteActivityRuleLayout.SiteActivityRuleID='{0}'", siteActivityRuleID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new SiteActivityRuleLayoutParams
                                               {
                                                   ID = (Guid)reader["ID"],
                                                   SiteID = (Guid)reader["SiteID"],
                                                   SiteActivityRuleID = (Guid)reader["SiteActivityRuleID"],
                                                   ParentID = reader["ParentID"] != DBNull.Value ? (Guid?)reader["ParentID"] : null,
                                                   SiteColumnID = reader["SiteColumnID"] != DBNull.Value ? (Guid?)reader["SiteColumnID"] : null,
                                                   IsRequired = (bool)reader["IsRequired"],
                                                   IsExtraField = (bool)reader["IsExtraField"],
                                                   IsAdmin = (bool)reader["IsAdmin"],
                                                   CSSStyle = reader["CSSStyle"] != DBNull.Value ? (string)reader["CSSStyle"] : null,                                                  
                                                   DefaultValue = reader["DefaultValue"] != DBNull.Value ? (string)reader["DefaultValue"] : null,
                                                   Name = reader["Name"] != DBNull.Value ? (string)reader["Name"] : null,
                                                   Order = reader["Order"] != DBNull.Value ? (int?)reader["Order"] : null,
                                                   ShowName = (bool)reader["ShowName"],
                                                   Orientation = reader["Orientation"] != DBNull.Value ? (int?)reader["Orientation"] : null,
                                                   OutputFormat = reader["OutputFormat"] != DBNull.Value ? (int?)reader["OutputFormat"] : null,
                                                   OutputFormatFields = reader["OutputFormatFields"] != DBNull.Value ? (int?)reader["OutputFormatFields"] : null,
                                                   Description = reader["Description"] != DBNull.Value ? (string)reader["Description"] : null,
                                                   LayoutType = (int)reader["LayoutType"],
                                                   SiteColumnName = reader["SiteColumnName"] != DBNull.Value ? (string)reader["SiteColumnName"] : null,
                                                   SiteColumnTypeID = reader["SiteColumnTypeID"] != DBNull.Value ? (int?)reader["SiteColumnTypeID"] : null,
                                                   LayoutParams = reader["LayoutParams"] != DBNull.Value ? (string)reader["LayoutParams"] : null,
                                                   SysField = reader["SysField"] != DBNull.Value ? (string)reader["SysField"] : null,
                                                   ColumnTypeExpressionID = reader["ColumnTypeExpressionID"] != DBNull.Value ? (Guid?)reader["ColumnTypeExpressionID"] : null
                                               });
                            }
                            reader.NextResult();
                        }
                    }
                }
                return result;
            }
        }



        public tbl_ContactColumnValues ContactColumnValues_Select(Guid contactID, Guid siteColumnID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_ContactColumnValues result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_ContactColumnValues WHERE ContactID='{0}' AND SiteColumnID='{1}'", contactID, siteColumnID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_ContactColumnValues
                            {
                                ID = (Guid)reader["ID"],
                                ContactID = (Guid)reader["ContactID"],
                                SiteColumnID = (Guid)reader["SiteColumnID"],
                                StringValue = reader["StringValue"] != DBNull.Value ? (string)reader["StringValue"] : null,
                                DateValue = reader["DateValue"] != DBNull.Value ? (DateTime?)reader["DateValue"] : null,
                                SiteColumnValueID = reader["SiteColumnValueID"] != DBNull.Value ? (Guid?)reader["SiteColumnValueID"] : null
                            };
                        }
                    }
                }

                return result;
            }
        }



        public List<tbl_ContactColumnValues> ContactColumnValues_SelectByContactID(Guid contactID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<tbl_ContactColumnValues>();

                connection.Open();
                var query = string.Format("SELECT * FROM tbl_ContactColumnValues WHERE ContactID='{0}'", contactID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new tbl_ContactColumnValues
                                {
                                    ID = (Guid)reader["ID"],
                                    ContactID = (Guid)reader["ContactID"],
                                    SiteColumnID = (Guid)reader["SiteColumnID"],
                                    StringValue = reader["StringValue"] != DBNull.Value ? (string)reader["StringValue"] : null,
                                    DateValue = reader["DateValue"] != DBNull.Value ? (DateTime?)reader["DateValue"] : null,
                                    SiteColumnValueID = reader["SiteColumnValueID"] != DBNull.Value ? (Guid?)reader["SiteColumnValueID"] : null
                                });
                            }
                            reader.NextResult();
                        }
                    }
                }
                return result;
            }
        }



        public List<ContactColumnValueMap> GetContactColumnValues(Guid contactID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<ContactColumnValueMap>();

                connection.Open();
                var query = string.Format("SELECT tbl_ContactColumnValues.*, tbl_SiteColumns.TypeID FROM tbl_ContactColumnValues LEFT JOIN tbl_SiteColumns ON tbl_SiteColumns.ID=tbl_ContactColumnValues.SiteColumnID WHERE ContactID='{0}'", contactID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new ContactColumnValueMap
                                {
                                    ID = (Guid)reader["ID"],
                                    ContactID = (Guid)reader["ContactID"],
                                    SiteColumnID = (Guid)reader["SiteColumnID"],
                                    StringValue = reader["StringValue"] != DBNull.Value ? (string)reader["StringValue"] : null,
                                    DateValue = reader["DateValue"] != DBNull.Value ? (DateTime?)reader["DateValue"] : null,
                                    SiteColumnValueID = reader["SiteColumnValueID"] != DBNull.Value ? (Guid?)reader["SiteColumnValueID"] : null,
                                    LogicalValue = reader["LogicalValue"] != DBNull.Value ? (bool?)reader["LogicalValue"] : null,
                                    TypeID = (int)reader["TypeID"]
                                });
                            }
                            reader.NextResult();
                        }
                    }
                }
                return result;
            }
        }


        /// <summary>
        /// Sites the domain_ select by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public List<CounterServiceDomainAccessMap> SiteDomain_SelectBySiteId(Guid siteId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<CounterServiceDomainAccessMap>();

                connection.Open();
                var query = string.Format(@"
SELECT IsBlockAccessFromDomainsOutsideOfList
FROM tbl_Sites
WHERE ID='{0}'
SELECT Domain, Aliases 
FROM tbl_SiteDomain
WHERE SiteID='{0}'
UPDATE tbl_SiteDomain SET StatusID = CASE WHEN StatusID <> {2} THEN {1} ELSE StatusID END WHERE SiteID='{0}'", siteId, (int)SiteDomainStatus.Attached, (int)SiteDomainStatus.LeadForceDomain);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {                            
                            reader.Read();

                            var isBlockAccessFromDomainsOutsideOfList =
                                bool.Parse(reader["IsBlockAccessFromDomainsOutsideOfList"].ToString());

                            reader.NextResult();

                            while (reader.Read())
                            {
                                var item = new CounterServiceDomainAccessMap
                                               {
                                                   SiteId = siteId,
                                                   IsBlockAccessFromDomainsOutsideOfList = isBlockAccessFromDomainsOutsideOfList,
                                                   Domains = new List<string>()
                                               };
                                Uri uriTemp = null;
                                if (reader["Domain"] != DBNull.Value && !string.IsNullOrEmpty((string)reader["Domain"]))
                                {
                                    var domain = (string) reader["Domain"];
                                    if (Uri.TryCreate(domain, UriKind.Absolute, out uriTemp))                                    
                                        domain = uriTemp.Host;
                                    item.Domains.Add(domain);
                                }

                                if (reader["Aliases"] != DBNull.Value && !string.IsNullOrEmpty((string)reader["Aliases"]))
                                {
                                    var aliases = (string) reader["Aliases"];
                                    foreach (string alias in aliases.Split(';'))
                                    {
                                        var localAlias = string.Empty;
                                        if (Uri.TryCreate(alias, UriKind.Absolute, out uriTemp))
                                            localAlias = uriTemp.Host;
                                        else if (Uri.TryCreate("http://" + alias, UriKind.Absolute, out uriTemp))
                                            localAlias = uriTemp.Host;

                                        item.Domains.Add(localAlias);
                                    }
                                }

                                result.Add(item);
                            }
                            reader.NextResult();
                                                        
                            if (result.Count == 0)
                                result.Add(new CounterServiceDomainAccessMap()
                                               {
                                                   IsBlockAccessFromDomainsOutsideOfList = isBlockAccessFromDomainsOutsideOfList,
                                                   SiteId = siteId,
                                                   Domains = new List<string>()
                                               });
                        }
                    }
                }
                return result;
            }
        }
        


        public List<tbl_SiteColumnValues> SiteColumnValues_SelectAll(Guid siteColumnID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<tbl_SiteColumnValues>();

                connection.Open();
                var query = string.Format("SELECT * FROM tbl_SiteColumnValues WHERE SiteColumnID='{0}'", siteColumnID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new tbl_SiteColumnValues
                                               {
                                                   ID = (Guid)reader["ID"],
                                                   SiteColumnID = (Guid)reader["SiteColumnID"],
                                                   Value = reader["Value"] != DBNull.Value ? (string)reader["Value"] : null
                                               });
                            }
                            reader.NextResult();
                        }
                    }
                }
                return result;
            }
        }



        public SiteActivityRuleLayoutParams SiteActivityRuleLayout_SelectById(Guid siteActivityRuleLayoutID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                SiteActivityRuleLayoutParams result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 tbl_SiteActivityRuleLayout.*, tbl_SiteColumns.Name AS SiteColumnName, tbl_SiteColumns.TypeID AS SiteColumnTypeID FROM tbl_SiteActivityRuleLayout LEFT JOIN tbl_SiteColumns ON tbl_SiteColumns.ID=tbl_SiteActivityRuleLayout.SiteColumnID WHERE tbl_SiteActivityRuleLayout.ID='{0}'", siteActivityRuleLayoutID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new SiteActivityRuleLayoutParams
                                         {
                                             ID = (Guid)reader["ID"],
                                             SiteID = (Guid)reader["SiteID"],
                                             SiteActivityRuleID = (Guid)reader["SiteActivityRuleID"],
                                             ParentID = reader["ParentID"] != DBNull.Value ? (Guid?)reader["ParentID"] : null,
                                             SiteColumnID = reader["SiteColumnID"] != DBNull.Value ? (Guid?)reader["SiteColumnID"] : null,
                                             IsRequired = (bool)reader["IsRequired"],
                                             IsExtraField = (bool)reader["IsExtraField"],
                                             IsAdmin = (bool)reader["IsAdmin"],
                                             CSSStyle = reader["CSSStyle"] != DBNull.Value ? (string)reader["CSSStyle"] : null,
                                             DefaultValue = reader["DefaultValue"] != DBNull.Value ? (string)reader["DefaultValue"] : null,
                                             Name = reader["Name"] != DBNull.Value ? (string)reader["Name"] : null,
                                             Order = reader["Order"] != DBNull.Value ? (int?)reader["Order"] : null,
                                             ShowName = (bool)reader["ShowName"],
                                             Orientation = reader["Orientation"] != DBNull.Value ? (int?)reader["Orientation"] : null,
                                             OutputFormat = reader["OutputFormat"] != DBNull.Value ? (int?)reader["OutputFormat"] : null,
                                             OutputFormatFields = reader["OutputFormatFields"] != DBNull.Value ? (int?)reader["OutputFormatFields"] : null,
                                             Description = reader["Description"] != DBNull.Value ? (string)reader["Description"] : null,
                                             LayoutType = (int)reader["LayoutType"],
                                             SiteColumnName = reader["SiteColumnName"] != DBNull.Value ? (string)reader["SiteColumnName"] : null,
                                             SiteColumnTypeID = reader["SiteColumnTypeID"] != DBNull.Value ? (int?)reader["SiteColumnTypeID"] : null
                                         };
                        }
                    }
                }
                return result;
            }
        }


        public tbl_Contact Contact_SelectById(Guid siteID, Guid contactID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_Contact result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_Contact WHERE SiteID='{0}' AND ID='{1}'", siteID, contactID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_Contact
                            {
                                ID = (Guid)reader["ID"],
                                SiteID = (Guid)reader["SiteID"],
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                LastActivityAt = reader["LastActivityAt"] != DBNull.Value ? (DateTime?)reader["LastActivityAt"] : null,
                                RefferURL = (string)reader["RefferURL"],
                                UserIP = (string)reader["UserIP"],
                                UserFullName = reader["UserFullName"] != DBNull.Value ? (string)reader["UserFullName"] : null,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                                Phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : null,
                                ReadyToSellID = reader["ReadyToSellID"] != DBNull.Value ? (Guid?)reader["ReadyToSellID"] : null,
                                PriorityID = reader["PriorityID"] != DBNull.Value ? (Guid?)reader["PriorityID"] : null,
                                StatusID = (Guid)reader["StatusID"],
                                Score = (int)reader["Score"],
                                BehaviorScore = (int)reader["BehaviorScore"],
                                CharacteristicsScore = (int)reader["CharacteristicsScore"],
                                IsNameChecked = (bool)reader["IsNameChecked"],
                                Surname = reader["Surname"] != DBNull.Value ? (string)reader["Surname"] : null,
                                Name = reader["Name"] != DBNull.Value ? (string)reader["Name"] : null,
                                Patronymic = reader["Patronymic"] != DBNull.Value ? (string)reader["Patronymic"] : null,
                                CellularPhone = reader["CellularPhone"] != DBNull.Value ? (string)reader["CellularPhone"] : null,
                                CellularPhoneStatusID = reader["CellularPhoneStatusID"] != DBNull.Value ? (int?)reader["CellularPhoneStatusID"] : null,
                                EmailStatusID = reader["EmailStatusID"] != DBNull.Value ? (int?)reader["EmailStatusID"] : null,
                                ContactTypeID = reader["ContactTypeID"] != DBNull.Value ? (Guid?)reader["ContactTypeID"] : null,
                                JobTitle = reader["JobTitle"] != DBNull.Value ? (string)reader["JobTitle"] : null,
                                CompanyID = reader["CompanyID"] != DBNull.Value ? (Guid?)reader["CompanyID"] : null,
                                OwnerID = reader["OwnerID"] != DBNull.Value ? (Guid?)reader["OwnerID"] : null,
                                BirthDate = reader["BirthDate"] != DBNull.Value ? (DateTime?)reader["BirthDate"] : null,
                                ContactFunctionInCompanyID = reader["ContactFunctionInCompanyID"] != DBNull.Value ? (Guid?)reader["ContactFunctionInCompanyID"] : null,
                                ContactJobLevelID = reader["ContactJobLevelID"] != DBNull.Value ? (Guid?)reader["ContactJobLevelID"] : null,
                                AddressID = reader["AddressID"] != DBNull.Value ? (Guid?)reader["AddressID"] : null,
                                RefferID = reader["RefferID"] != DBNull.Value ? (Guid?)reader["RefferID"] : null,
                                AdvertisingPlatformID = reader["AdvertisingPlatformID"] != DBNull.Value ? (Guid?)reader["AdvertisingPlatformID"] : null,
                                AdvertisingTypeID = reader["AdvertisingTypeID"] != DBNull.Value ? (Guid?)reader["AdvertisingTypeID"] : null,
                                AdvertisingCampaignID = reader["AdvertisingCampaignID"] != DBNull.Value ? (Guid?)reader["AdvertisingCampaignID"] : null,
                                Gender = reader["Gender"] != DBNull.Value ? (int?)reader["Gender"] : null,
                                Category = (int)reader["Category"],
                                RegistrationSourceID = (int)reader["RegistrationSourceID"],
                                Comment = reader["Comment"] != DBNull.Value ? (string)reader["Comment"] : null,
                                CameFromURL = reader["CameFromURL"] != DBNull.Value ? (string)reader["CameFromURL"] : null
                            };
                        }
                    }
                }
                return result;
            }
        }



        public tbl_SiteColumns SiteColumns_SelectById(Guid siteID, Guid siteColumnID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_SiteColumns result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_SiteColumns WHERE SiteID='{0}' AND ID='{1}'", siteID, siteColumnID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_SiteColumns
                            {
                                ID = (Guid)reader["ID"],
                                SiteID = (Guid)reader["SiteID"],
                                SiteActivityRuleID = reader["SiteActivityRuleID"] != DBNull.Value ? (Guid?)reader["SiteActivityRuleID"] : null,
                                Name = (string)reader["Name"],
                                CategoryID = (Guid)reader["CategoryID"],
                                TypeID = (int)reader["TypeID"],
                                Code = (string)reader["Code"]
                            };
                        }
                    }
                }
                return result;
            }
        }



        public tbl_SiteColumnValues SiteColumnValues_SelectById(Guid siteColumnValueID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_SiteColumnValues result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_SiteColumnValues WHERE ID='{0}'", siteColumnValueID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_SiteColumnValues
                                         {
                                             ID = (Guid) reader["ID"],
                                             SiteColumnID = (Guid) reader["SiteColumnID"],
                                             Value = reader["Value"] != DBNull.Value ? (string) reader["Value"] : null
                                         };
                        }
                    }
                }
                return result;
            }
        }



        public void ContactColumnValues_Update(tbl_ContactColumnValues contactColumnValue)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                var query = "UPDATE tbl_ContactColumnValues SET ContactID=@ContactID, SiteColumnID=@SiteColumnID, StringValue=@StringValue, DateValue=@DateValue, SiteColumnValueID=@SiteColumnValueID, LogicalValue=@LogicalValue WHERE ID=@ID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContactID", contactColumnValue.ContactID);
                    command.Parameters.AddWithValue("@SiteColumnID", contactColumnValue.SiteColumnID);

                    if (contactColumnValue.StringValue != null)
                        command.Parameters.AddWithValue("@StringValue", contactColumnValue.StringValue);
                    else
                        command.Parameters.AddWithValue("@StringValue", DBNull.Value);

                    if (contactColumnValue.DateValue != null)
                        command.Parameters.AddWithValue("@DateValue", contactColumnValue.DateValue);
                    else
                        command.Parameters.AddWithValue("@DateValue", DBNull.Value);

                    if (contactColumnValue.SiteColumnValueID != null)
                        command.Parameters.AddWithValue("@SiteColumnValueID", contactColumnValue.SiteColumnValueID);
                    else
                        command.Parameters.AddWithValue("@SiteColumnValueID", DBNull.Value);

                    if (contactColumnValue.LogicalValue != null)
                        command.Parameters.AddWithValue("@LogicalValue", contactColumnValue.LogicalValue);
                    else
                        command.Parameters.AddWithValue("@LogicalValue", DBNull.Value);

                    command.Parameters.AddWithValue("@ID", contactColumnValue.ID);

                    command.ExecuteNonQuery();
                }
            }
        }



        public tbl_ContactColumnValues ContactColumnValues_Add(tbl_ContactColumnValues contactColumnValue)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                contactColumnValue.ID = Guid.NewGuid();
                var query = "INSERT INTO tbl_ContactColumnValues (ID, ContactID, SiteColumnID, StringValue, DateValue, SiteColumnValueID, LogicalValue) VALUES (@ID, @ContactID, @SiteColumnID, @StringValue, @DateValue, @SiteColumnValueID, @LogicalValue)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", contactColumnValue.ID);
                    command.Parameters.AddWithValue("@ContactID", contactColumnValue.ContactID);
                    command.Parameters.AddWithValue("@SiteColumnID", contactColumnValue.SiteColumnID);

                    if (contactColumnValue.StringValue != null)
                        command.Parameters.AddWithValue("@StringValue", contactColumnValue.StringValue);
                    else
                        command.Parameters.AddWithValue("@StringValue", DBNull.Value);

                    if (contactColumnValue.DateValue != null)
                        command.Parameters.AddWithValue("@DateValue", contactColumnValue.DateValue);
                    else
                        command.Parameters.AddWithValue("@DateValue", DBNull.Value);

                    if (contactColumnValue.SiteColumnValueID != null)
                        command.Parameters.AddWithValue("@SiteColumnValueID", contactColumnValue.SiteColumnValueID);
                    else
                        command.Parameters.AddWithValue("@SiteColumnValueID", DBNull.Value);

                    if (contactColumnValue.LogicalValue != null)
                        command.Parameters.AddWithValue("@LogicalValue", contactColumnValue.LogicalValue);
                    else
                        command.Parameters.AddWithValue("@LogicalValue", DBNull.Value);

                    command.ExecuteNonQuery();

                }

                return contactColumnValue;
            }
        }


        public void Contact_Update(tbl_Contact contact)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                var query = "UPDATE tbl_Contact SET SiteID=@SiteID, CreatedAt=@CreatedAt, LastActivityAt=@LastActivityAt, RefferURL=@RefferURL, UserIP=@UserIP, UserFullName=@UserFullName, Email=@Email, Phone=@Phone, CellularPhone=@CellularPhone, ReadyToSellID=@ReadyToSellID, PriorityID=@PriorityID, StatusID=@StatusID, Score=@Score, BehaviorScore=@BehaviorScore, CharacteristicsScore=@CharacteristicsScore, IsNameChecked=@IsNameChecked, Surname=@Surname, Name=@Name, Patronymic=@Patronymic, JobTitle=@JobTitle, Comment=@Comment, AdvertisingPlatformID=@AdvertisingPlatformID, AdvertisingTypeID=@AdvertisingTypeID, AdvertisingCampaignID=@AdvertisingCampaignID, CompanyID=@CompanyID, ContactJobLevelID=@ContactJobLevelID, ContactFunctionInCompanyID=@ContactFunctionInCompanyID, AddressID=@AddressID WHERE ID=@ID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", contact.SiteID);
                    command.Parameters.AddWithValue("@CreatedAt", contact.CreatedAt);

                    if (contact.LastActivityAt != null)
                        command.Parameters.AddWithValue("@LastActivityAt", contact.LastActivityAt);
                    else
                        command.Parameters.AddWithValue("@LastActivityAt", DBNull.Value);

                    command.Parameters.AddWithValue("@RefferURL", contact.RefferURL);
                    command.Parameters.AddWithValue("@UserIP", contact.UserIP);

                    if (contact.UserFullName != null)
                        command.Parameters.AddWithValue("@UserFullName", contact.UserFullName);
                    else
                        command.Parameters.AddWithValue("@UserFullName", DBNull.Value);

                    if (contact.Email != null)
                        command.Parameters.AddWithValue("@Email", contact.Email);
                    else
                        command.Parameters.AddWithValue("@Email", DBNull.Value);

                    if (contact.Phone != null)
                        command.Parameters.AddWithValue("@Phone", contact.Phone);
                    else
                        command.Parameters.AddWithValue("@Phone", DBNull.Value);

                    if (contact.CellularPhone != null)
                        command.Parameters.AddWithValue("@CellularPhone", contact.CellularPhone);
                    else
                        command.Parameters.AddWithValue("@CellularPhone", DBNull.Value);

                    if (contact.ReadyToSellID != null)
                        command.Parameters.AddWithValue("@ReadyToSellID", contact.ReadyToSellID);
                    else
                        command.Parameters.AddWithValue("@ReadyToSellID", DBNull.Value);

                    if (contact.PriorityID != null)
                        command.Parameters.AddWithValue("@PriorityID", contact.PriorityID);
                    else
                        command.Parameters.AddWithValue("@PriorityID", DBNull.Value);

                    command.Parameters.AddWithValue("@StatusID", contact.StatusID);
                    command.Parameters.AddWithValue("@Score", contact.Score);
                    command.Parameters.AddWithValue("@BehaviorScore", contact.BehaviorScore);
                    command.Parameters.AddWithValue("@CharacteristicsScore", contact.CharacteristicsScore);
                    command.Parameters.AddWithValue("@ID", contact.ID);
                    command.Parameters.AddWithValue("@IsNameChecked", contact.IsNameChecked);

                    if (contact.Surname != null)
                        command.Parameters.AddWithValue("@Surname", contact.Surname);
                    else
                        command.Parameters.AddWithValue("@Surname", DBNull.Value);

                    if (contact.Name != null)
                        command.Parameters.AddWithValue("@Name", contact.Name);
                    else
                        command.Parameters.AddWithValue("@Name", DBNull.Value);

                    if (contact.Patronymic != null)
                        command.Parameters.AddWithValue("@Patronymic", contact.Patronymic);
                    else
                        command.Parameters.AddWithValue("@Patronymic", DBNull.Value);

                    if (contact.JobTitle != null)
                        command.Parameters.AddWithValue("@JobTitle", contact.JobTitle);
                    else
                        command.Parameters.AddWithValue("@JobTitle", DBNull.Value);

                    if (contact.Comment != null)
                        command.Parameters.AddWithValue("@Comment", contact.Comment);
                    else
                        command.Parameters.AddWithValue("@Comment", DBNull.Value);

                    /*if (contact.RefferURL != null)
                        command.Parameters.AddWithValue("@RefferURL", contact.RefferURL);
                    else
                        command.Parameters.AddWithValue("@RefferURL", DBNull.Value);*/

                    if (contact.AdvertisingPlatformID != null)
                        command.Parameters.AddWithValue("@AdvertisingPlatformID", contact.AdvertisingPlatformID);
                    else
                        command.Parameters.AddWithValue("@AdvertisingPlatformID", DBNull.Value);

                    if (contact.AdvertisingTypeID != null)
                        command.Parameters.AddWithValue("@AdvertisingTypeID", contact.AdvertisingTypeID);
                    else
                        command.Parameters.AddWithValue("@AdvertisingTypeID", DBNull.Value);

                    if (contact.AdvertisingCampaignID != null)
                        command.Parameters.AddWithValue("@AdvertisingCampaignID", contact.AdvertisingCampaignID);
                    else
                        command.Parameters.AddWithValue("@AdvertisingCampaignID", DBNull.Value);

                    if (contact.CompanyID != null)
                        command.Parameters.AddWithValue("@CompanyID", contact.CompanyID);
                    else
                        command.Parameters.AddWithValue("@CompanyID", DBNull.Value);

                    if (contact.ContactJobLevelID != null)
                        command.Parameters.AddWithValue("@ContactJobLevelID", contact.ContactJobLevelID);
                    else
                        command.Parameters.AddWithValue("@ContactJobLevelID", DBNull.Value);

                    if (contact.ContactFunctionInCompanyID != null)
                        command.Parameters.AddWithValue("@ContactFunctionInCompanyID", contact.ContactFunctionInCompanyID);
                    else
                        command.Parameters.AddWithValue("@ContactFunctionInCompanyID", DBNull.Value);

                    if (contact.AddressID != null)
                        command.Parameters.AddWithValue("@AddressID", contact.AddressID);
                    else
                        command.Parameters.AddWithValue("@AddressID", DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }



        public List<tbl_SiteEventTemplates> SiteEventTemplates_SelectAll(Guid siteID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<tbl_SiteEventTemplates>();

                connection.Open();
                var query = string.Format("SELECT * FROM tbl_SiteEventTemplates WHERE SiteID='{0}'", siteID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new tbl_SiteEventTemplates
                                {
                                    ID = (Guid)reader["ID"],
                                    SiteID = (Guid)reader["SiteID"],
                                    Title = (string)reader["Title"],
                                    LogicConditionID = (int)reader["LogicConditionID"],
                                    ActionCount = reader["ActionCount"] != DBNull.Value ? (int?)reader["ActionCount"] : null,
                                    EventConditions = reader["EventConditions"] != DBNull.Value ? (string)reader["EventConditions"] : null,
                                    FrequencyPeriod = (int)reader["FrequencyPeriod"]
                                });
                            }
                            reader.NextResult();
                        }
                    }
                }
                return result;
            }
        }



        public List<tbl_ContactActivity> ContactActivity_Select(Guid siteID, Guid contactID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<tbl_ContactActivity>();

                connection.Open();
                var query = string.Format("SELECT * FROM tbl_ContactActivity WHERE SiteID='{0}' AND ContactID='{1}'", siteID, contactID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new tbl_ContactActivity
                                {
                                    ID = (Guid)reader["ID"],
                                    SiteID = (Guid)reader["SiteID"],
                                    ContactID = (Guid)reader["ContactID"],
                                    CreatedAt = (DateTime)reader["CreatedAt"],
                                    ActivityTypeID = (int)reader["ActivityTypeID"],
                                    ActivityCode = reader["ActivityCode"] != DBNull.Value ? (string)reader["ActivityCode"] : null,
                                    ContactSessionID = reader["ContactSessionID"] != DBNull.Value ? (Guid?)reader["ContactSessionID"] : null
                                });
                            }
                            reader.NextResult();
                        }
                    }
                }

                return result;
            }
        }



        public List<tbl_SiteEventTemplateActivity> SiteEventTemplateActivity_SelectAll(Guid siteID, Guid siteEventTemplateID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<tbl_SiteEventTemplateActivity>();

                connection.Open();
                var query = string.Format("SELECT * FROM tbl_SiteEventTemplateActivity WHERE SiteID='{0}' AND SiteEventTemplateID='{1}'", siteID, siteEventTemplateID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new tbl_SiteEventTemplateActivity
                                {
                                    ID = (Guid)reader["ID"],
                                    SiteID = (Guid)reader["SiteID"],
                                    SiteEventTemplateID = (Guid)reader["SiteEventTemplateID"],
                                    EventCategoryID = reader["EventCategoryID"] != DBNull.Value ? (int?)reader["EventCategoryID"] : null,
                                    ActivityTypeID = reader["ActivityTypeID"] != DBNull.Value ? (int?)reader["ActivityTypeID"] : null,
                                    ActivityCode = reader["ActivityCode"] != DBNull.Value ? (string)reader["ActivityCode"] : null,
                                    ActualPeriod = reader["ActualPeriod"] != DBNull.Value ? (int?)reader["ActualPeriod"] : null,
                                    Option = reader["Option"] != DBNull.Value ? (string)reader["Option"] : null,
                                    FormulaID = reader["FormulaID"] != DBNull.Value ? (int?)reader["FormulaID"] : null,
                                    Value = reader["Value"] != DBNull.Value ? (string)reader["Value"] : null
                                });
                            }
                            reader.NextResult();
                        }
                    }
                }
                return result;
            }
        }



        public tbl_ContactActivityScore ContactActivityScore_SelectByScoreType(Guid siteID, Guid contactID, Guid siteActivityScoreTypeID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_ContactActivityScore result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_ContactActivityScore WHERE SiteID='{0}' AND ContactID='{1}' AND SiteActivityScoreTypeID='{2}'", siteID, contactID, siteActivityScoreTypeID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_ContactActivityScore
                                         {
                                             ID = (Guid) reader["ID"],
                                             SiteID = (Guid) reader["SiteID"],
                                             SiteActivityScoreTypeID = (Guid) reader["SiteActivityScoreTypeID"],
                                             ContactID = (Guid) reader["ContactID"],
                                             Score = (int)reader["Score"]
                                         };
                        }
                    }
                }
                return result;
            }
        }



        /// <summary>
        /// Select all session source rules.
        /// </summary>
        /// <returns></returns>
        public List<tbl_SessionSourceRule> SessionSourceRule_SelectAll()
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<tbl_SessionSourceRule>();

                connection.Open();

                var query = "SELECT * FROM tbl_SessionSourceRule ORDER BY SessionRuleTypeID";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new tbl_SessionSourceRule
                                               {
                                                   ID = (Guid) reader["ID"],
                                                   Title = (string) reader["Title"],
                                                   Pattern = (string) reader["Pattern"],
                                                   SessionRuleTypeID = (byte) reader["SessionRuleTypeID"]
                                               });
                            }
                            reader.NextResult();
                        }
                    }
                }

                return result;
            }
        }


        public tbl_ColumnTypesExpression ColumnTypesExpression_SelectById(Guid id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_ColumnTypesExpression result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_ColumnTypesExpression WHERE ID='{0}'", id);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_ColumnTypesExpression
                            {
                                ID = (Guid)reader["ID"],
                                Title = (string)reader["Title"],
                                ColumnTypesID = (int)reader["ColumnTypesID"],
                                Expression = (string)reader["Expression"]
                            };
                        }
                    }
                }
                return result;
            }
        }



        public Guid Company_SelectByNameAndCreate(Guid siteId, string name)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                Guid? companyId;

                connection.Open();
                var query = string.Format("SELECT TOP 1 ID FROM tbl_Company WHERE SiteID='{0}' AND Name=N'{1}'", siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    companyId = (Guid?)command.ExecuteScalar();
                    if (companyId.HasValue)
                        return (Guid)companyId;
                }

                Guid? statusId;
                query = string.Format("SELECT TOP 1 ID FROM tbl_Status WHERE SiteID='{0}' AND IsDefault=1", siteId);
                using (var command = new SqlCommand(query, connection))
                {
                    statusId = (Guid?)command.ExecuteScalar();
                }

                if (!statusId.HasValue)
                {
                    query = string.Format("SELECT TOP 1 ID FROM tbl_Status WHERE SiteID='{0}'", siteId);
                    using (var command = new SqlCommand(query, connection))
                    {
                        statusId = (Guid?)command.ExecuteScalar();
                    }
                }

                companyId = Guid.NewGuid();
                query = string.Format("INSERT INTO tbl_Company (ID, SiteID, Name, StatusID) VALUES (N'{0}', N'{1}', N'{2}', N'{3}')", companyId, siteId, name, statusId);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                return (Guid)companyId;
            }
        }



        public Guid ContactJobLevel_SelectByNameAndCreate(Guid siteId, string name)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                Guid? contactJobLevelId;

                connection.Open();
                var query = string.Format("SELECT TOP 1 ID FROM tbl_ContactJobLevel WHERE SiteID='{0}' AND Name=N'{1}'", siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    contactJobLevelId = (Guid?)command.ExecuteScalar();
                    if (contactJobLevelId.HasValue)
                        return (Guid)contactJobLevelId;
                }

                contactJobLevelId = Guid.NewGuid();
                query = string.Format("INSERT INTO tbl_ContactJobLevel (ID, SiteID, Name) VALUES (N'{0}', N'{1}', N'{2}')", contactJobLevelId, siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                return (Guid)contactJobLevelId;
            }
        }



        public Guid ContactFunctionInCompany_SelectByNameAndCreate(Guid siteId, string name)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                Guid? contactFunctionInCompanyId;

                connection.Open();
                var query = string.Format("SELECT TOP 1 ID FROM tbl_ContactFunctionInCompany WHERE SiteID='{0}' AND Name=N'{1}'", siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    contactFunctionInCompanyId = (Guid?)command.ExecuteScalar();
                    if (contactFunctionInCompanyId.HasValue)
                        return (Guid)contactFunctionInCompanyId;
                }

                contactFunctionInCompanyId = Guid.NewGuid();
                query = string.Format("INSERT INTO tbl_ContactFunctionInCompany (ID, SiteID, Name) VALUES (N'{0}', N'{1}', N'{2}')", contactFunctionInCompanyId, siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                return (Guid)contactFunctionInCompanyId;
            }
        }



        public Guid AdvertisingPlatform_SelectByTitleAndCreate(Guid siteId, string name)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                Guid? advertisingPlatformId;

                connection.Open();
                var query = string.Format("SELECT TOP 1 ID FROM tbl_AdvertisingPlatform WHERE SiteID='{0}' AND Title=N'{1}'", siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    advertisingPlatformId = (Guid?)command.ExecuteScalar();
                    if (advertisingPlatformId.HasValue)
                        return (Guid)advertisingPlatformId;
                }

                advertisingPlatformId = Guid.NewGuid();
                query = string.Format("INSERT INTO tbl_AdvertisingPlatform (ID, SiteID, Title) VALUES (N'{0}', N'{1}', N'{2}')", advertisingPlatformId, siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                return (Guid)advertisingPlatformId;
            }
        }



        public Guid AdvertisingType_SelectByTitleAndCreate(Guid siteId, string name)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                Guid? advertisingTypeId;

                connection.Open();
                var query = string.Format("SELECT TOP 1 ID FROM tbl_AdvertisingType WHERE SiteID='{0}' AND Title=N'{1}'", siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    advertisingTypeId = (Guid?)command.ExecuteScalar();
                    if (advertisingTypeId.HasValue)
                        return (Guid)advertisingTypeId;
                }

                advertisingTypeId = Guid.NewGuid();
                query = string.Format("INSERT INTO tbl_AdvertisingType (ID, SiteID, Title) VALUES (N'{0}', N'{1}', N'{2}')", advertisingTypeId, siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                return (Guid)advertisingTypeId;
            }
        }



        public Guid AdvertisingCampaign_SelectByTitleAndCreate(Guid siteId, string name)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                Guid? advertisingCampaignId;

                connection.Open();
                var query = string.Format("SELECT TOP 1 ID FROM tbl_AdvertisingCampaign WHERE SiteID='{0}' AND Title=N'{1}'", siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    advertisingCampaignId = (Guid?)command.ExecuteScalar();
                    if (advertisingCampaignId.HasValue)
                        return (Guid)advertisingCampaignId;
                }

                advertisingCampaignId = Guid.NewGuid();
                query = string.Format("INSERT INTO tbl_AdvertisingCampaign (ID, SiteID, Title) VALUES (N'{0}', N'{1}', N'{2}')", advertisingCampaignId, siteId, name);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                return (Guid)advertisingCampaignId;
            }
        }


        public Guid Address_Add(string address)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var addressId = Guid.NewGuid();

                connection.Open();
                var query = string.Format("INSERT INTO tbl_Address (ID, Address) VALUES (N'{0}', N'{1}')", addressId, address);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                return addressId;
            }
        }



        public void Address_Update(Guid addressId, string address)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                var query = !string.IsNullOrEmpty(address)
                                ? string.Format("UPDATE tbl_Address SET Address=N'{0}' WHERE ID=N'{1}'", address, addressId)
                                : string.Format("UPDATE tbl_Address SET Address=NULL WHERE ID=N'{0}'", addressId);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }



        public Guid AddressCountry_Add(Guid countryId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var addressId = Guid.NewGuid();

                connection.Open();
                var query = string.Format("INSERT INTO tbl_Address (ID, CountryID) VALUES (N'{0}', N'{1}')", addressId, countryId);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                return addressId;
            }
        }



        public void AddressCountry_Update(Guid addressId, Guid? countryId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                var query = countryId.HasValue
                                ? string.Format("UPDATE tbl_Address SET CountryID=N'{0}' WHERE ID=N'{1}'", countryId,addressId)
                                : string.Format("UPDATE tbl_Address SET CountryID=NULL WHERE ID=N'{0}'", addressId);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }



        public Guid AddressCity_Add(Guid countryId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var addressId = Guid.NewGuid();

                connection.Open();
                var query = string.Format("INSERT INTO tbl_Address (ID, CityID) VALUES (N'{0}', N'{1}')", addressId, countryId);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                return addressId;
            }
        }



        public void AddressCity_Update(Guid addressId, Guid? cityId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                var query = cityId.HasValue
                                ? string.Format("UPDATE tbl_Address SET CityID=N'{0}' WHERE ID=N'{1}'", cityId, addressId)
                                : string.Format("UPDATE tbl_Address SET CityID=NULL WHERE ID=N'{0}'", addressId);
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }



        public tbl_Address Address_SelectById(Guid addressId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_Address result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_Address WHERE ID='{0}'", addressId);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_Address
                            {
                                ID = (Guid)reader["ID"],
                                Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : null,
                                CountryID = reader["CountryID"] != DBNull.Value ? (Guid?)reader["CountryID"] : null,
                                CityID = reader["CityID"] != DBNull.Value ? (Guid?)reader["CityID"] : null,
                                DistrictID = reader["DistrictID"] != DBNull.Value ? (Guid?)reader["DistrictID"] : null,
                                RegionID = reader["RegionID"] != DBNull.Value ? (Guid?)reader["RegionID"] : null,
                            };
                        }
                    }
                }
                return result;
            }
        }


        public tbl_Company Company_SelectById(Guid companyId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                tbl_Company result = null;

                connection.Open();
                var query = string.Format("SELECT TOP 1 * FROM tbl_Company WHERE ID='{0}'", companyId);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = new tbl_Company
                            {
                                ID = (Guid)reader["ID"],
                                SiteID = (Guid)reader["SiteID"],
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                Name = (string)reader["Name"],
                                OwnerID = reader["OwnerID"] != DBNull.Value ? (Guid?)reader["OwnerID"] : null,
                                CompanyTypeID = reader["CompanyTypeID"] != DBNull.Value ? (Guid?)reader["CompanyTypeID"] : null,
                                ParentID = reader["ParentID"] != DBNull.Value ? (Guid?)reader["ParentID"] : null,
                                CompanySizeID = reader["CompanySizeID"] != DBNull.Value ? (Guid?)reader["CompanySizeID"] : null,
                                CompanySectorID = reader["CompanySectorID"] != DBNull.Value ? (Guid?)reader["CompanySectorID"] : null,
                                Phone1 = reader["Phone1"] != DBNull.Value ? (string)reader["Phone1"] : null,
                                Phone2 = reader["Phone2"] != DBNull.Value ? (string)reader["Phone2"] : null,
                                Fax = reader["Fax"] != DBNull.Value ? (string)reader["Fax"] : null,
                                Web = reader["Web"] != DBNull.Value ? (string)reader["Web"] : null,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                                EmailStatusID = reader["EmailStatusID"] != DBNull.Value ? (int?)reader["EmailStatusID"] : null,
                                LocationAddressID = reader["LocationAddressID"] != DBNull.Value ? (Guid?)reader["LocationAddressID"] : null,
                                PostalAddressID = reader["PostalAddressID"] != DBNull.Value ? (Guid?)reader["PostalAddressID"] : null,
                                ReadyToSellID = reader["ReadyToSellID"] != DBNull.Value ? (Guid?)reader["ReadyToSellID"] : null,
                                PriorityID = reader["PriorityID"] != DBNull.Value ? (Guid?)reader["PriorityID"] : null,
                                StatusID = (Guid)reader["StatusID"],
                                Score = (int)reader["Score"],
                                BehaviorScore = (int)reader["BehaviorScore"],
                                CharacteristicsScore = (int)reader["CharacteristicsScore"]
                            };
                        }
                    }
                }
                return result;
            }
        }


        public List<tbl_ExternalResource> ExternalResource_SelectByDestinationID(Guid destinationId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<tbl_ExternalResource>();

                connection.Open();
                var query = string.Format("SELECT * FROM tbl_ExternalResource WHERE DestinationID=N'{0}'", destinationId);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new tbl_ExternalResource
                                {
                                    ID = (Guid)reader["ID"],
                                    DestinationID = (Guid)reader["DestinationID"],
                                    Title = (string)reader["Title"],
                                    ResourcePlaceID = (int)reader["ResourcePlaceID"],
                                    ExternalResourceTypeID = (int)reader["ExternalResourceTypeID"],
                                    File = reader["File"] != DBNull.Value ? (string)reader["File"] : null,
                                    Text = reader["Text"] != DBNull.Value ? (string)reader["Text"] : null,
                                    Url = reader["Url"] != DBNull.Value ? (string)reader["Url"] : null
                                });
                            }
                            reader.NextResult();
                        }
                    }
                }
                return result;
            }
        }
    }
}