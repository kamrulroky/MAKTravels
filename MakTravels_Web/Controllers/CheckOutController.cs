using MakTravels_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakTravels_Web.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        TourPackagesEntities storeDB = new TourPackagesEntities();
        const string PromoCode = "Free";

        //
        // GET: /Checkout/AddressAndPayment

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var booking = new Booking();
            TryUpdateModel(booking);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(booking);
                }
                else
                {
                    booking.CustomerName = User.Identity.Name;
                    booking.BookingTime = DateTime.Now;

                    //Save Order
                    storeDB.Booking.Add(booking);
                    storeDB.SaveChanges();

                    //Process the order
                    var cart = BookingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(booking);

                    return RedirectToAction("Complete",
                        new { id = booking.PackageId });
                }

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(booking);
            }
        }

        //
        // GET: /Checkout/Complete

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Booking.Any(
                o => o.PackageId == id &&
                o.CustomerName == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}