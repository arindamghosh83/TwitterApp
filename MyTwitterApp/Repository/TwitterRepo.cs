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
    public class TwitterRepo : ITwitterRepo
    {
        private readonly ITwitterHttpHandler _handler;

        public TwitterRepo(ITwitterHttpHandler handler)
        {
            this._handler = handler;
        }        
        public string OAuthConsumerSecret { get; set; }
        public string OAuthConsumerKey { get; set; }

        public async Task<List<RootObject>> GetTwitts()
        {
           
             var accessToken =  this.GetAccessToken().Result;
            var url = "https://api.twitter.com/1.1/statuses/user_timeline.json?count=10&screen_name=salesforce";

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, url);
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            //HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline).ConfigureAwait(false);
            var tweets = await this._handler.GetTweetsAsync(accessToken,url).ConfigureAwait(false);
            return tweets;
        }

        public async  Task<string> GetAccessToken()
        {
        
            var accessToken =
                await _handler.GetAccessTokenAsync("https://api.twitter.com/oauth2/token").ConfigureAwait(false);
            return accessToken;
        }
    }
}