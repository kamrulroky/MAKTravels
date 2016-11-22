using MakTravels_Web.Models;
using MakTravels_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakTravels_Web.Controllers
{
    public class BookingCartController : Controller
    {
        TourPackagesEntities storeDB = new TourPackagesEntities();
        
        // GET: BookingCart
        public ActionResult Index()
        {
            var cart = BookingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new BookingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            // Retrieve the album from the database
            var addedPackage = storeDB.TourPackage
                .Single(package => package.PackageId == id);

            // Add it to the shopping cart
            var cart = BookingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedPackage);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = BookingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string packageName = storeDB.Cart
                .Single(item => item.PackageId == id).Tourpackage.HotelsId;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new BookingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(packageName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = BookingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
    }
}