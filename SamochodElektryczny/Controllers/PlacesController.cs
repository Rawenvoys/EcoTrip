using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SamochodElektryczny.Models;
using SamochodElektryczny.Models.DAL;

namespace SamochodElektryczny.Controllers
{
    public class PlacesController : Controller
    {
        private PlaceContext db = new PlaceContext();

        // GET: Places
        public ActionResult Index()
        {
            return View(db.Places.ToList());
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaceID,Name,Lat,Lng,Rate,Type")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(place);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaceID,Name,Lat,Lng,Rate,Type")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(place);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
            db.SaveChanges();
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

        [HttpGet]
        public ActionResult CalculateDistances()
        {
            Dictionary<Place, List<DistancePlace>> distanceMap = new Dictionary<Place, List<DistancePlace>>();
            List<Place> places = db.Places.ToList();
            List<DistancePlace> distances;
            foreach (Place place in places)
            {
                distances = new List<DistancePlace>();
                DistancePlace dp;
                Double distance;
                foreach (Place p in places)
                {
                    if (p.Lat != place.Lat && p.Lng != place.Lng)
                    {
                        //distance = Math.Sqrt(Math.Pow(place.Lat - p.Lat, 2) + Math.Pow(place.Lng - p.Lng,2));
                        distance = (Math.Acos(Math.Sin(p.Lat * Math.PI / 180) * Math.Sin(place.Lat * Math.PI / 180) + Math.Cos(p.Lat * Math.PI / 180) * Math.Cos(place.Lat * Math.PI / 180) * Math.Cos((p.Lng* Math.PI / 180) - (place.Lng*Math.PI/180))) * 6371);
                        dp = new DistancePlace(distance, p);
                        distances.Add(dp);
                    }
                }

                distanceMap.Add(place, distances);
            }
            ViewBag.DistancesToPlaces = distanceMap;
            Dictionary<Place, List<DistancePlace>> nearestPlaces = new Dictionary<Place, List<DistancePlace>>();
            List<DistancePlace> tempPlaces;
            foreach (var i in distanceMap)
            {
                tempPlaces = new List<DistancePlace>();
                List<DistancePlace> d = i.Value.OrderBy(p => p.distance).ToList();
                tempPlaces.AddRange(d.Take(4));
           
                nearestPlaces.Add(i.Key, tempPlaces);
            }

            ViewBag.NearestPlaces = nearestPlaces.Take(4);
            return View();
        }
    }
}
