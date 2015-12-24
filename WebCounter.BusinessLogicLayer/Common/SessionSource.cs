using System;
using System.Text.RegularExpressions;
using System.Web;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class SessionSource
    {
        public string EnterPointURL { get; set; }
        public string AdvertisingPlatform { get; set; }
        public string AdvertisingType { get; set; }
        public string Keywords { get; set; }
        public string AdvertisingCampaign { get; set; }
        public string Content { get; set; }        
        public string CameFromUrl { get; set; }

        public SessionSource()
        {
            EnterPointURL = null;
            AdvertisingPlatform = null;
            AdvertisingType = null;
            Keywords = null;
            AdvertisingCampaign = null;
            Content = null;
            CameFromUrl = null;            
        }


        public bool IsNull()
        {
            if (EnterPointURL == null && AdvertisingPlatform == null && AdvertisingType == null && Keywords == null && AdvertisingCampaign == null && Content == null && CameFromUrl == null)
                return true;

            return false;
        }


        /// <summary>
        /// Selects the session source object.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="referrerUrl">The referrer URL.</param>
        /// <returns></returns>
        public static SessionSource Select(string url, string referrerUrl)
        {
            var result = new SessionSource();

            var repository = new WebCounterServiceRepository();
            var sessionSourceRules = repository.SessionSourceRule_SelectAll();

            if (!string.IsNullOrEmpty(url))
                url = HttpUtility.UrlDecode(url);
            referrerUrl = HttpUtility.UrlDecode(referrerUrl);

            foreach (var sessionSourceRule in sessionSourceRules)
            {
                var regex = new Regex(sessionSourceRule.Pattern);
                var matches = regex.Matches((SessionRuleType) sessionSourceRule.SessionRuleTypeID == SessionRuleType.SourceUrl && !string.IsNullOrEmpty(url) ? url : referrerUrl);

                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        switch ((SessionRuleType)sessionSourceRule.SessionRuleTypeID)
                        {
                            case SessionRuleType.SourceUrl:
                                result.AdvertisingPlatform = match.Groups["source"].Value != string.Empty ? match.Groups["source"].Value : result.AdvertisingPlatform;
                                result.AdvertisingType = match.Groups["medium"].Value != string.Empty ? match.Groups["medium"].Value : result.AdvertisingType;
                                result.AdvertisingCampaign = match.Groups["campaign"].Value != string.Empty ? match.Groups["campaign"].Value : result.AdvertisingCampaign;
                                result.Content = match.Groups["content"].Value != string.Empty ? match.Groups["content"].Value : result.Content;
                                result.Keywords = match.Groups["keywords"].Value != string.Empty ? match.Groups["keywords"].Value : result.Keywords;
                                break;
                            case SessionRuleType.ReferrerUrl:                            
                                result.Keywords = match.Groups["keywords"].Value != string.Empty ? match.Groups["keywords"].Value : result.Keywords;

                                if (sessionSourceRule.Title == "Google Adwords" || sessionSourceRule.Title == "Yandex Direct")
                                    result.AdvertisingType = sessionSourceRule.Title;
                                if (sessionSourceRule.Title == "Yandex Services")
                                {
                                    result.AdvertisingPlatform = "Yandex";
                                    if (!string.IsNullOrEmpty(referrerUrl))
                                    {
                                        if (referrerUrl.ToLower().Contains("images.yandex"))
                                            result.AdvertisingType = "Images";
                                        else if (referrerUrl.ToLower().Contains("news.yandex"))
                                            result.AdvertisingType = "News";
                                        else if (referrerUrl.ToLower().Contains("rabota.yandex"))
                                            result.AdvertisingType = "Rabota";
                                    }
                                }

                                result.CameFromUrl = match.Groups["camefrom"].Value != string.Empty
                                                 ? match.Groups["camefrom"].Value
                                                 : result.CameFromUrl;
                                Uri cameFromUrl;
                                if (!string.IsNullOrEmpty(result.CameFromUrl) && string.IsNullOrEmpty(result.AdvertisingPlatform) && Uri.TryCreate(result.CameFromUrl, UriKind.Absolute, out cameFromUrl))
                                    result.AdvertisingPlatform = cameFromUrl.Host;                        

                                break;
                        }                        
                    }

                    if ((SessionRuleType)sessionSourceRule.SessionRuleTypeID == SessionRuleType.ReferrerUrl)
                    {
                        if (string.IsNullOrEmpty(result.AdvertisingPlatform))
                            result.AdvertisingPlatform = sessionSourceRule.Title;
                        if (string.IsNullOrEmpty(result.AdvertisingType))
                            result.AdvertisingType = "search";
                        if (string.IsNullOrEmpty(result.CameFromUrl) && !string.IsNullOrEmpty(referrerUrl))
                        {
                            Uri refferUri;
                            if (Uri.TryCreate(referrerUrl, UriKind.RelativeOrAbsolute, out refferUri) && !string.IsNullOrEmpty(refferUri.Query))
                                result.CameFromUrl = refferUri.ToString().Replace(refferUri.Query, string.Empty);
                        }
                    }

                    break;
                }
            }

            if (result.IsNull())
            {                
                result.CameFromUrl = referrerUrl;
                if (!string.IsNullOrEmpty(referrerUrl))
                {
                    result.AdvertisingType = "Сайт";

                    var host = new Uri(referrerUrl).Host.Replace("www.", string.Empty);
                    if (host.ToLower().Contains("google."))
                    {
                        result.AdvertisingType = string.Empty;
                        host = "Google";
                    }
                    result.AdvertisingPlatform = host;
                }
            }

            result.EnterPointURL = url;

            return result;
        }
    }
}