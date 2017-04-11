using SamochodElektryczny.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamochodElektryczny.Controllers
{
    public class MapController : Controller
    {
        private PlaceContext db = new PlaceContext();
        // GET: Map
        public ActionResult Index()
        {
            var places = db.Places.ToList();
            return View(places);
        }
    }
}