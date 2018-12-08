using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tourist.Models;

namespace Tourist.Controllers
{
    public class HomeController : Controller
    {
        TouristicEntities tourDb = new TouristicEntities();
        public ActionResult Index()
        {
            return View(tourDb.Routes);
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Edit()
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
    }
}