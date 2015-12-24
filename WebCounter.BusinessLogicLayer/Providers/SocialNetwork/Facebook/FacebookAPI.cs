using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.BusinessLogicLayer.Providers.SocialNetwork.Facebook
{
    public enum HttpVerb
    {
        GET,
        POST,
        DELETE
    }

    public class FacebookAPI
    {
        public FacebookAccesTokenInfo AccessTokenInfo;

        public FacebookAPI(string code, Guid siteId, string domain)
        {
            GetAccessToken(code, siteId, domain);
        }



        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <returns></returns>
        public FacebookProfile GetProfile()
        {
            var result = Get("/me");
            return JsonConvert.DeserializeObject<FacebookProfile>(result.ToString());
        }



        /// <summary>
        /// Gets the specified relative path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns></returns>
        protected JObject Get(string relativePath)
        {
            return Call(relativePath, HttpVerb.GET, null);
        }



        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="siteId">The site id.</param>
        /// <param name="domain">The domain.</param>
        protected void GetAccessToken(string code, Guid siteId, string domain)
        {            
            var url = new Uri(Settings.Facebook.APIUrl + "/oauth/access_token");
            var args = new Dictionary<string, string>();
            args["client_id"] =  Settings.Facebook.ClientId;
            args["client_secret"] = Settings.Facebook.ClientSecret;
            args["code"] = code;
            args["redirect_uri"] = Settings.Facebook.RedirectUri(siteId, domain);

            string responseText = MakeRequest(url, HttpVerb.GET, args);

            var tokens = responseText.Split('&').ToDictionary(token => token.Substring(0, token.IndexOf("=")), token => token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
            AccessTokenInfo = new FacebookAccesTokenInfo {access_token = tokens["access_token"]};
        }



        /// <summary>
        /// Calls the specified relative path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="httpVerb">The HTTP verb.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        protected JObject Call(string relativePath, HttpVerb httpVerb, Dictionary<string, string> args)
        {
            Uri baseURL = new Uri(Settings.Facebook.APIUrl);
            Uri url = new Uri(baseURL, relativePath);
            if (args == null)
            {
                args = new Dictionary<string, string>();
            }
            if (!string.IsNullOrEmpty(AccessTokenInfo.access_token))
            {
                args["access_token"] = AccessTokenInfo.access_token;
            }
            string responseText = MakeRequest(url, httpVerb, args);
            JObject obj = JObject.Parse(responseText);
            JToken errorToken;
            if ((obj.Type == JTokenType.Object) && obj.TryGetValue("error", out errorToken))
            {
                throw new FacebookAPIException(errorToken.Value<string>("type"), errorToken.Value<string>("message"));
            }
            return obj;
        }



        /// <summary>
        /// Makes the request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="httpVerb">The HTTP verb.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public string MakeRequest(Uri url, HttpVerb httpVerb, Dictionary<string, string> args)
        {
            if (args != null && args.Keys.Count > 0 && httpVerb == HttpVerb.GET)
            {
                url = new Uri(url + EncodeDictionary(args, true));
            }            

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Method = httpVerb.ToString();

            if (httpVerb == HttpVerb.POST)
            {
                string postData = EncodeDictionary(args, false);

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(postData);

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postDataBytes.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postDataBytes, 0, postDataBytes.Length);
                requestStream.Close();
            }

            try
            {
                using (HttpWebResponse response
                        = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader
                        = new StreamReader(response.GetResponseStream());

                    return reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                throw new FacebookAPIException("Server Error", e.Message);
            }
        }



        /// <summary>
        /// Encodes the dictionary.
        /// </summary>
        /// <param name="dict">The dict.</param>
        /// <param name="questionMark">if set to <c>true</c> [question mark].</param>
        /// <returns></returns>
        private string EncodeDictionary(Dictionary<string, string> dict, bool questionMark)
        {
            var sb = new StringBuilder();
            if (questionMark)
            {
                sb.Append("?");
            }
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                sb.Append(HttpUtility.UrlEncode(kvp.Key));
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(kvp.Value));
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1); // Remove trailing &
            return sb.ToString();
        }
    }
}