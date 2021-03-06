﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTwitterApp.Extensions;
using MyTwitterApp.Models;
using MyTwitterApp.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyTwitterApp.Controllers
{
    public class HomeController : Controller
    {
        private ITwitterRepo _repo;

        public HomeController(ITwitterRepo repo)
        {
            this._repo = repo;

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetTwitterFeed()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()

            };
            
            var tweets = _repo.GetTweets();

            var twitterfeed =JsonConvert.SerializeObject(tweets,settings);

            return Content(twitterfeed, "application/json");
            //return new JsonNetResult() { Data = tweets };


        }
    }
}