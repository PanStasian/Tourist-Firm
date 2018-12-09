using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tourist.Models;

namespace Tourist.Controllers
{
    public class SearchController : Controller
    {
        private TouristicEntities db = new TouristicEntities();
        // GET: Search
        public ActionResult Index(string search_dest, string search_dep)
        {
            var result = from t in db.Routes
                         select t;
            if (!String.IsNullOrEmpty(search_dest))
            {
                result = result.Where(t=>t.Destination.destination_name.Contains(search_dest) && t.Departure.departure_name.Contains(search_dep));
            }
            return View(result.ToList());
        }
    }
}