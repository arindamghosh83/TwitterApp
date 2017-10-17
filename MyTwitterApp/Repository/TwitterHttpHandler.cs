using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using MyTwitterApp.Models;
using Newtonsoft.Json;

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

        public async Task<RootObject> GetTweetsAsync(string accessToken)
        {
            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, "https://api.twitter.com/1.1/statuses/user_timeline.json?count=3&screen_name=salesforce");
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            //var httpClient = new HttpClient();
            //HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline).ConfigureAwait(false);
            HttpResponseMessage responseUserTimeLine = await _client.SendAsync(requestUserTimeline).ConfigureAwait(false);
            //var serializer = new JavaScriptSerializer();
            //dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            //var enumerableTwitts = (json as IEnumerable<dynamic>);
            List<RootObject> ourlisting = JsonConvert.DeserializeObject<List<RootObject>>(await responseUserTimeLine.Content.ReadAsStringAsync());
            //var tweet = enumerableTwitts[0];


            //if (enumerableTwitts == null)
            //{
            //    return null;
            //}
            //return enumerableTwitts.Select(t => (string)(t["text"].ToString()));
            return ourlisting[2];
        }
    }
}