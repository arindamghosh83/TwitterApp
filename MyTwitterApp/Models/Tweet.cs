using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace MyTwitterApp.Models
{
    public class Hashtag
    {
       
        public string Text { get; set; }
      
        public List<int> Indices { get; set; }
    }

    public class Url
    {
       
        public string url { get; set; }
        public string expanded_url { get; set; }
        public string display_url { get; set; }
        public List<int> Indices { get; set; }
        
    }


    public class Medium
    {
        public long id { get; set; }
        
        public List<int> Indices { get; set; }
       
        [JsonProperty("media_url")]
        public string MediaUrl { get; set; }
        
        [JsonProperty("media_url_https")]
        public string MediaUrlHttps { get; set; }
        
        public string Url { get; set; }
        
        public string Type { get; set; }

    }

    public class Entities
    {
        
        public List<Hashtag> Hashtags { get; set; }
       
        public List<object> Symbols { get; set; }
        
        [JsonProperty("user_mentions")]
        public List<UserMention> UserMentions { get; set; }

       
        public List<Url> Urls { get; set; }
       
        public List<Medium> Media { get; set; }
    }



    public class Medium3
    {


        [JsonProperty("media_url")]
        public string MediaUrl { get; set; }


        public string Type { get; set; }



    }

    public class ExtendedEntities
    {
        public List<Medium> media { get; set; }
    }

    public class User
    {
       
        
       
        public string Name { get; set; }
        
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
        
        public string Location { get; set; }
       
        public string Description { get; set; }
        
        public string Url { get; set; }

        
        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }




    }

    public class RootObject
    {
        
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        
        public long Id { get; set; }
        
        public string Text { get; set; }

        public Entities Entities { get; set; }
        
        [JsonProperty("extended_entities")]
        public ExtendedEntities ExtendedEntities { get; set; }


      public User User { get; set; }

        
        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

    }

  
    public class UserMention

    {
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
        

        public int[] Indices { get; set; }
        
       

    }


}