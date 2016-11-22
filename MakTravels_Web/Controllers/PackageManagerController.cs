using MakTravels_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakTravels_Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PackageManagerController : Controller
    {
        private TourPackagesEntities db = new TourPackagesEntities();
        
        // GET: PackageManager
        public ActionResult Index()
        {
            var packages = db.TourPackage.Include(a => a.Airline).Include(a => a.Hotel);
            return View(packages.ToList());
        }

        //
        // GET: /PackageManager/Details/5

        public ViewResult Details(int id)
        {
            TourPackages package = db.TourPackage.Find(id);
            return View(package);
        }

        //
        // GET: /packageManager/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Airline, "AirLineId", "Name");
            ViewBag.ArtistId = new SelectList(db.Hotel, "HotelId", "Name");
            return View();
        }

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(TourPackages package)
        {
            if (ModelState.IsValid)
            {
                db.TourPackage.Add(package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Airline, "AirlineId", "Name", package.AirLinesId);
            ViewBag.ArtistId = new SelectList(db.Hotel, "ArtistId", "Name", package.HotelsId);
            return View(package);
        }

        //
        // GET: /StoreManager/Edit/5

        public ActionResult Edit(int id)
        {
            TourPackages package = db.TourPackage.Find(id);
            ViewBag.GenreId = new SelectList(db.Airline, "GenreId", "Name", package.AirLinesId);
            ViewBag.ArtistId = new SelectList(db.Hotel, "ArtistId", "Name", package.HotelsId);
            return View(package);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(TourPackages package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Airline, "AirLinesId", "Name", package.AirLinesId);
            ViewBag.ArtistId = new SelectList(db.Hotel, "HotelsId", "Name", package.HotelsId);
            return View(package);
        }

        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id)
        {
            TourPackages package = db.TourPackage.Find(id);
            return View(package);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TourPackages package = db.TourPackage.Find(id);
            db.TourPackage.Remove(package);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}