using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTwitterApp.Models
{
    public class Hashtag
    {
        public string text { get; set; }
        public List<int> indices { get; set; }
    }

    public class Url
    {
        public string url { get; set; }
        public string expanded_url { get; set; }
        public string display_url { get; set; }
        public List<int> indices { get; set; }
    }


    public class Medium
    {
        public long id { get; set; }
        public string id_str { get; set; }
        public List<int> indices { get; set; }
        public string media_url { get; set; }
        public string media_url_https { get; set; }
        public string url { get; set; }
        public string display_url { get; set; }
        public string expanded_url { get; set; }
        public string type { get; set; }
  
    }

    public class Entities
    {
        public List<Hashtag> hashtags { get; set; }
        public List<object> symbols { get; set; }
        public List<User_Mention> user_mentions { get; set; }
      
        public List<Url> urls { get; set; }
        public List<Medium> media { get; set; }
    }



    public class Medium3
    {

        public string media_url { get; set; }
   
        public string type { get; set; }
  
    }

    public class ExtendedEntities
    {
        public List<Medium3> media { get; set; }
    }

    public class User
    {
       
        
        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }

        public string created_at { get; set; }

        public string profile_image_url { get; set; }




    }

    public class RootObject
    {
        public string created_at { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
    
        public Entities entities { get; set; }
        public ExtendedEntities extended_entities { get; set; }
        public string source { get; set; }

        public User user { get; set; }

        public int retweet_count { get; set; }

    }

    public class User_Mention
   
    {
      
        
        public string screen_name { get; set; }
       

        public int[] indices { get; set; }
       

    }


}