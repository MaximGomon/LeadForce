using System;
using System.Configuration;

namespace WebCounter.BusinessLogicLayer.Configuration
{
    public class Settings
    {
        public static string POP3MonitoringProcessingOfReturnsFromFilter
        {
            get { return ConfigurationManager.AppSettings["POP3MonitoringProcessingOfReturnsFromFilter"]; }
        }

        public static string POP3MonitoringProcessingOfReturnsBodyFilter
        {
            get { return ConfigurationManager.AppSettings["POP3MonitoringProcessingOfReturnsBodyFilter"]; }
        }        

        public static string POP3MonitoringProcessingOfAutoRepliesSubjectFilter
        {
            get { return ConfigurationManager.AppSettings["POP3MonitoringProcessingOfAutoRepliesSubjectFilter"]; }
        }


        public static string POP3MonitoringProcessingOfAutoRepliesBodyFilter
        {
            get { return ConfigurationManager.AppSettings["POP3MonitoringProcessingOfAutoRepliesBodyFilter"]; }
        }


        public static string POP3MonitoringAttachmentFilesPath
        {
            get { return ConfigurationManager.AppSettings["POP3MonitoringAttachmentFilesPath"]; }
        }


        public static string ADONetConnectionString
        {
            get { return ConfigurationManager.AppSettings["ADONETConnectionString"]; }
        }

        public static string LabitecDictionaryUploadImagesAbsoluteFilePath
        {
            get { return ConfigurationManager.AppSettings["LabitecDictionaryUploadImagesAbsoluteFilePath"]; }
        }

        public static string LabitecDictionaryViewImagesVirtualFilePath
        {
            get { return ConfigurationManager.AppSettings["LabitecDictionaryViewImagesVirtualFilePath"]; }
        }

        public static string LeadForceSiteUrl
        {
            get { return ConfigurationManager.AppSettings["LeadForceSiteUrl"]; }
        }

        public static string LabitecLeadForcePortalUrl
        {
            get { return ConfigurationManager.AppSettings["LabitecLeadForcePortalUrl"]; }
        }

        public static string LabitecLeadForcePortalActivateUserUrl(Guid portalSettingsId, Guid userId)
        {
            return LabitecLeadForcePortalUrl + ConfigurationManager.AppSettings["LabitecLeadForcePortalActivateUserUrl"].Replace("$portalSettingsId", portalSettingsId.ToString())
                                                                                                                        .Replace("$userId", userId.ToString());
        }

        public static string DictionaryLogoPath(Guid siteId, string dictionary)
        {
            return string.Format("{0}dictionaries/{1}/", LabitecDictionaryViewImagesVirtualFilePath.Replace("$siteId", siteId.ToString()), dictionary);
        }

        public static string WorkflowApiUrl
        {
            get { return ConfigurationManager.AppSettings["WorkflowApiUrl"]; }
        }

        public static string ProductImageVirtualPath(Guid? siteId = null)
        {
            if (siteId.HasValue)
                return string.Concat(ConfigurationManager.AppSettings["ProductImageVirtualPath"], siteId.ToString(), "/");
            return ConfigurationManager.AppSettings["ProductImageVirtualPath"];
        }

        public static string ProductImageAbsolutePath(Guid siteId)
        {
            return string.Concat(ConfigurationManager.AppSettings["ProductImageAbsolutePath"], siteId.ToString(), "\\");
        }

        public static string MiniSiteUrl(Guid id)
        {
            return string.Format("http://{0}/{1}",ConfigurationManager.AppSettings["CMSSiteHost"], id);
        }
        
        public static string CMSSiteHost
        {
            get { return ConfigurationManager.AppSettings["CMSSiteHost"]; }
        }


        public struct Vkontakte
        {
            public static string ClientId
            {
                get { return ConfigurationManager.AppSettings["VkontakteAPIClientId"]; }
            }

            public static string ClientSecret
            {
                get { return ConfigurationManager.AppSettings["VkontakteAPIClientSecret"]; }
            }

            public static string RedirectUri(Guid portalSettingsId, string domain)
            {
                return ConfigurationManager.AppSettings["VkontakteAPIRedirectUri"].Replace("$portalSettingsId", portalSettingsId.ToString())
                                                                                  .Replace("$domain", domain);
            }

            public static string Display
            {
                get { return ConfigurationManager.AppSettings["VkontakteAPIDisplay"]; }
            }

            public static string ResponseType
            {
                get { return ConfigurationManager.AppSettings["VkontakteAPIResponseType"]; }
            }

            public static string Scope
            {
                get { return ConfigurationManager.AppSettings["VkontakteAPIScope"]; }
            }

            public static string APIUrl
            {
                get { return ConfigurationManager.AppSettings["VkontakteAPIUrl"]; }
            }

            public static string OAuthUrl
            {
                get { return ConfigurationManager.AppSettings["VkontakteAPIOAuthUrl"]; }
            }                        

            public static string OAuthFullUrl(Guid portalSettingsId, string domain)
            {
                
                    return ConfigurationManager.AppSettings["VkontakteAPIOAuthFullUrl"]
                                                                                    .Replace("$apiOAuthUrl", OAuthUrl)
                                                                                    .Replace("$clientId", ClientId)
                                                                                    .Replace("$redirectUri", RedirectUri(portalSettingsId, domain))
                                                                                    .Replace("$scope", Scope)
                                                                                    .Replace("$responseType", ResponseType)
                                                                                    .Replace("$display", Display);
                
            }
        }

        public struct Facebook
        {
            public static string ClientId
            {
                get { return ConfigurationManager.AppSettings["FacebookAPIClientId"]; }
            }

            public static string ClientSecret
            {
                get { return ConfigurationManager.AppSettings["FacebookAPIClientSecret"]; }
            }

            public static string RedirectUri(Guid portalSettingsId, string domain)
            {
                return ConfigurationManager.AppSettings["FacebookAPIRedirectUri"].Replace("$portalSettingsId", portalSettingsId.ToString()).Replace("$domain", domain);
            }

            public static string Scope
            {
                get { return ConfigurationManager.AppSettings["FacebookAPIScope"]; }
            }

            public static string APIUrl
            {
                get { return ConfigurationManager.AppSettings["FacebookAPIUrl"]; }
            }

            public static string OAuthUrl
            {
                get { return ConfigurationManager.AppSettings["FacebookAPIOAuthUrl"]; }
            }

            public static string OAuthFullUrl(Guid portalSettingsId, string domain)
            {

                return ConfigurationManager.AppSettings["FacebookAPIOAuthFullUrl"]
                    .Replace("$apiOAuthUrl", OAuthUrl)
                    .Replace("$clientId", ClientId)
                    .Replace("$redirectUri", RedirectUri(portalSettingsId, domain))
                    .Replace("$scope", Scope);
            }
        }
    }
}
