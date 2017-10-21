using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using MyTwitterApp.Helpers;
using MyTwitterApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyTwitterApp.Repository
{
    public class TwitterHttpHandler : ITwitterHttpHandler
    {
        private HttpClient _client = new HttpClient();
        public string OAuthConsumerSecret { get; set; }
        public string OAuthConsumerKey { get; set; }
        public async Task<string> GetAccessTokenAsync(string url)
        {
            OAuthConsumerKey = ConfigurationManager.AppSettings["OAuthConsumerKey"];
            OAuthConsumerSecret = ConfigurationManager.AppSettings["OAuthConsumerSecret"];
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            //var clientCredentilas = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret);
            var clientCredentilas = System.Convert.ToBase64String(plainTextBytes);
            httpRequest.Headers.Add("Authorization", "Basic " + clientCredentilas);
            httpRequest.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await _client.SendAsync(httpRequest).ConfigureAwait(false);

            string json = await response.Content.ReadAsStringAsync();
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(json);
            var accessToken = item["access_token"];
            return item["access_token"];
            //return await _client.GetAsync(url);
        }

        public async Task<List<RootObject>> GetTweetsAsync(string accessToken, string url)
        {
            var settings = new JsonSerializerSettings
            {
               // ContractResolver = new DynamicMappingResolver(map)
                //ContractResolver = new CamelCasePropertyNamesContractResolver()
                
            };
            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, url);
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            //var httpClient = new HttpClient();
            //HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline).ConfigureAwait(false);
            HttpResponseMessage responseUserTimeLine = await _client.SendAsync(requestUserTimeline).ConfigureAwait(false);
            List<RootObject> ourlisting = JsonConvert.DeserializeObject<List<RootObject>>(await responseUserTimeLine.Content.ReadAsStringAsync());
            //List<RootObject> ourlisting = JsonConvert.DeserializeObject<List<RootObject>>(await responseUserTimeLine.Content.ReadAsStringAsync(),settings);

            return ourlisting;
        }

        //private Dictionary<Type, Dictionary<string, string>> map = new Dictionary<Type, Dictionary<string, string>>
        //{
        //    {
        //        typeof(UserMention),
        //        new Dictionary<string, string>
        //        {
        //            {"ScreenName", "screen_name"}
        //        }
        //    },
        //    {
        //        typeof(Entities),
        //        new Dictionary<string, string>
        //        {
        //            {"UserMentions", "user_mentions"}
        //        }
        //    }

        //};
    }
}