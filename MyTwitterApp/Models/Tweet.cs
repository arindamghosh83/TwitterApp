﻿using System;
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

    public class Medium2
    {
        public int w { get; set; }
        public int h { get; set; }
        public string resize { get; set; }
    }

    public class Thumb
    {
        public int w { get; set; }
        public int h { get; set; }
        public string resize { get; set; }
    }

    public class Large
    {
        public int w { get; set; }
        public int h { get; set; }
        public string resize { get; set; }
    }

    public class Small
    {
        public int w { get; set; }
        public int h { get; set; }
        public string resize { get; set; }
    }

    //public class Sizes
    //{
    //    public Medium2 medium { get; set; }
    //    public Thumb thumb { get; set; }
    //    public Large large { get; set; }
    //    public Small small { get; set; }
    //}

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
        //public Sizes sizes { get; set; }
    }

    public class Entities
    {
        public List<Hashtag> hashtags { get; set; }
        public List<object> symbols { get; set; }
        public List<User_Mention> user_mentions { get; set; }
        //public List<UserMention> UserMentions { get; set; }
        public List<Url> urls { get; set; }
        public List<Medium> media { get; set; }
    }

    //public class Medium4
    //{
    //    public int w { get; set; }
    //    public int h { get; set; }
    //    public string resize { get; set; }
    //}

    //public class Thumb2
    //{
    //    public int w { get; set; }
    //    public int h { get; set; }
    //    public string resize { get; set; }
    //}

    //public class Large2
    //{
    //    public int w { get; set; }
    //    public int h { get; set; }
    //    public string resize { get; set; }
    //}

    //public class Small2
    //{
    //    public int w { get; set; }
    //    public int h { get; set; }
    //    public string resize { get; set; }
    //}

    //public class Sizes2
    //{
    //    public Medium4 medium { get; set; }
    //    public Thumb2 thumb { get; set; }
    //    public Large2 large { get; set; }
    //    public Small2 small { get; set; }
    //}

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
        
        public string created_at { get; set; }

        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_banner_url { get; set; }



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
    //public class UserMention
    {
        public string name { get; set; }
        public string screen_name { get; set; }
        //public string ScreenName { get; set; }

        public int[] indices { get; set; }

    }


}