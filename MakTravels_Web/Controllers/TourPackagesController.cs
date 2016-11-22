using MakTravels_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakTravels_Web.Controllers
{
    public class TourPackagesController : Controller
    {
        TourPackagesEntities storeDB = new TourPackagesEntities();

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var Airlines = storeDB.Airline.ToList();

            return View(Airlines);
        }

        //
        // GET: /Store/Browse?genre=Disco

        public ActionResult Browse(string Name)
        {
            // Retrieve Genre and its Associated Albums from database
            var packageModel = storeDB.Airline.Include("TourPackage")
                .Single(p => p.AirLinesName == Name);

            return View(packageModel);
        }

        //
        // GET: /Store/Details/5

        public ActionResult Details(int id)
        {
            var package = storeDB.TourPackage.Find(id);

            return View(package);
        }

        //
        // GET: /Store/GenreMenu

        [ChildActionOnly]
        public ActionResult AirLinesMenu()
        {
            var airLine = storeDB.Airline.ToList();

            return PartialView(airLine);
        }
    }
}