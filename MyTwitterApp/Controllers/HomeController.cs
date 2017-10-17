using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTwitterApp.Repository;

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
            //TwitterRepo repo = new TwitterRepo();
            var access_token = _repo.GetTwitts();
            //var access_token = _repo.GetAccessToken();
            return Json(access_token, JsonRequestBehavior.AllowGet);
            
        }
    }
}