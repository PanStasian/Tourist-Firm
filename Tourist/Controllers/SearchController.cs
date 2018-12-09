using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tourist.Models;

namespace Tourist.Controllers
{
    public class SearchController : Controller
    {
        private TouristicEntities db = new TouristicEntities();
        // GET: Search
        public ActionResult Index(string search_dest, string search_dep, int? search_price)
        {
            var result = from t in db.Routes
                         select t;
            if (!String.IsNullOrEmpty(search_dest))
            {
                result = result.Where(t=>t.Destination.destination_name.Contains(search_dest));
            }
            if (!String.IsNullOrEmpty(search_dep))
            {
                result = result.Where(t => t.Departure.departure_name.Contains(search_dep));
            }
            if (!String.IsNullOrEmpty(search_price.ToString()))
            {
                result = result.Where(t => t.price <= search_price);
            }
            return View(result.ToList());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = await db.Routes.FindAsync(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }
    }
}
