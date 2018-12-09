using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tourist.Models;

namespace Tourist.Controllers
{
    public class RoutesController : Controller
    {
        private TouristicEntities db = new TouristicEntities();

        // GET: Routes
        public async Task<ActionResult> Index()
        {
            var routes = db.Routes.Include(r => r.Departure).Include(r => r.Destination);
            return View(await routes.ToListAsync());            
        }

        // GET: Routes/Details/5
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

        // GET: Routes/Create
        public ActionResult Create()
        {
            ViewBag.departure_id = new SelectList(db.Departure, "departutre_id", "departure_name");
            ViewBag.destination_id = new SelectList(db.Destination, "destination_id", "destination_name");
            return View();
        }

        // POST: Routes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "route_id,route_name,departure_id,destination_id,price,sights_id,flight")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(routes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.departure_id = new SelectList(db.Departure, "departutre_id", "departure_name", routes.departure_id);
            ViewBag.destination_id = new SelectList(db.Destination, "destination_id", "destination_name", routes.destination_id);
            return View(routes);
        }

        // GET: Routes/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.departure_id = new SelectList(db.Departure, "departutre_id", "departure_name", routes.departure_id);
            ViewBag.destination_id = new SelectList(db.Destination, "destination_id", "destination_name", routes.destination_id);
            return View(routes);
        }

        // POST: Routes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "route_id,route_name,departure_id,destination_id,price,sights_id,flight")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.departure_id = new SelectList(db.Departure, "departutre_id", "departure_name", routes.departure_id);
            ViewBag.destination_id = new SelectList(db.Destination, "destination_id", "destination_name", routes.destination_id);
            return View(routes);
        }

        // GET: Routes/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Routes routes = await db.Routes.FindAsync(id);
            db.Routes.Remove(routes);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult FilterView(string departure, string destination, int? price, int? flight)
        {
            RouteContext rc = new RouteContext();
            IQueryable<Routes> routes= rc.Routes.Include(r => r.Departure).Include(r => r.Destination);
            if (departure != null)
            {
                routes = routes.Where(r => r.Departure.departure_name == departure);
            }
            if (destination != null)
            {
                routes = routes.Where(r => r.Destination.destination_name == destination);
            }
           // if(price!=null && price != 0)
           // {
           //     routes = routes.Where(r => r.price == price);
           // }
           // if(flight!=null && flight != 0)
           // {
           //     routes = routes.Where(r => r.flight == flight);
           // }
            List<Routes> selectedRoutes = rc.Routes.ToList();

            RoutesList rl = new RoutesList()
            {
                Departure = new SelectList(departure),
                Destination = new SelectList(destination)
            };

            return View(rl);
        }

    }
}
