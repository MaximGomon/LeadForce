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

namespace WebCounter.BusinessLogicLayer.Providers.SocialNetwork.Vkontakte
{
    public class VkontakteAPI
    {
        public VkontakteAccessTokenInfo AccessTokenInfo;

        public VkontakteAPI(string code)
        {
            GetAccessToken(code);
        }



        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <returns></returns>
        public VkontakteProfile GetProfile()
        {
            var args = new Dictionary<string, string>();
            args["fields"] = "uid, first_name, last_name, nickname, screen_name, bdate, city, country";
            var responseObj = Call("getProfiles", args);

            return JsonConvert.DeserializeObject<List<VkontakteProfile>>(responseObj["response"].ToString())[0];
        }



        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <returns></returns>
        public VkontakteLocation GetCountry(string cid)
        {
            var args = new Dictionary<string, string>();
            args["cids"] = cid;
            var responseObj = Call("getCountries", args);

            return JsonConvert.DeserializeObject<List<VkontakteLocation>>(responseObj["response"].ToString())[0];
        }



        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <returns></returns>
        public VkontakteLocation GetCity(string cid)
        {
            var args = new Dictionary<string, string>();
            args["cids"] = cid;
            var responseObj = Call("getCities", args);

            return JsonConvert.DeserializeObject<List<VkontakteLocation>>(responseObj["response"].ToString())[0];
        }



        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="code">The code.</param>
        protected void GetAccessToken(string code)
        {            
            var url = new Uri(Settings.Vkontakte.OAuthUrl + "/access_token");            
            var args = new Dictionary<string, string>();
            args["client_id"] = Settings.Vkontakte.ClientId;
            args["client_secret"] = Settings.Vkontakte.ClientSecret;
            args["code"] = code;

            string responseText = MakeRequest(url, args);
            JObject obj = JObject.Parse(responseText);
            JToken errorToken;
            if ((obj.Type == JTokenType.Object) && obj.TryGetValue("error", out errorToken))            
                throw new VKontakteAPIException(errorToken.Value<string>("error"), errorToken.Value<string>("error_description"));            

            AccessTokenInfo = JsonConvert.DeserializeObject<VkontakteAccessTokenInfo>(responseText);
        }



        /// <summary>
        /// Calls the specified method name.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public JObject Call(string methodName, Dictionary<string, string> args = null)
        {
            var url = new Uri(Settings.Vkontakte.APIUrl + "/method/" + methodName);
            if (args == null)
                args = new Dictionary<string, string>();

            args["uid"] = AccessTokenInfo.user_id;
            args["access_token"] = AccessTokenInfo.access_token;            

            string responseText = MakeRequest(url, args);
            JObject obj = JObject.Parse(responseText);
            JToken errorToken;
            if ((obj.Type == JTokenType.Object) && obj.TryGetValue("error", out errorToken))
            {
                throw new VKontakteAPIException(errorToken.Value<string>("error"), errorToken.Value<string>("error_description"));
            }

            return obj;
        }



        /// <summary>
        /// Makes the request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public string MakeRequest(Uri url, Dictionary<string, string> args)
        {
            if (args != null && args.Keys.Count > 0)
            {
                url = new Uri(url + EncodeDictionary(args, true));
            }

            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    return reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                throw new VKontakteAPIException("Server Error", e.Message);
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