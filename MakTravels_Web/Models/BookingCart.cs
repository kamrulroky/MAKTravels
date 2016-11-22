using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakTravels_Web.Models
{
    public partial class BookingCart
    {
        TourPackagesEntities storeDB = new TourPackagesEntities();

        string BookingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static BookingCart GetCart(HttpContextBase context)
        {
            var cart = new BookingCart();
            cart.BookingCartId = cart.GetCartId(context);
            return cart;
        }

        //Helper Method to simplify Bookingcart Calls
        public static BookingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(TourPackages tourpackages)
        {
            //get the matching cart and tourpackages instances
            var cartItem = storeDB.Cart.SingleOrDefault(
                c => c.CartId == BookingCartId && c.PackageId == tourpackages.PackageId);

            if(cartItem == null)
            {
                //create new cartItem to cart
                cartItem = new Cart
                {
                    PackageId = tourpackages.PackageId,
                    CartId = BookingCartId,
                    DateOrdered = DateTime.Now,
                    Count=1
                };

                storeDB.Cart.Add(cartItem);

            }
            else 
            {
                //if the item does exist in the cart then add count
                cartItem.Count++;
            }

            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            //get the cart
            var cartItem= storeDB.Cart.Single
            (
                c => c.CartId == BookingCartId && c.PackageId == id
            );

            int packageCount = 0;

            if(cartItem != null)
            {
                if(cartItem.Count > 1)
                {
                    cartItem.Count--;
                    packageCount = cartItem.Count;
                }
                else
                {
                    storeDB.Cart.Remove(cartItem);
                }
            }

            //save changes
            storeDB.SaveChanges();

            return packageCount;
        }

        public void EmptyCart()
        {
            var cartItem = storeDB.Cart.Where(cart => cart.CartId == BookingCartId);

            foreach(var item in cartItem)
            {
                storeDB.Cart.Remove(item);
            }

            storeDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return storeDB.Cart.Where(cart => cart.CartId == BookingCartId).ToList();
        }

        public int GetCount()
        {
            //get the count of the packages and sum it up

            int? count = (from cartItems in storeDB.Cart
                          where cartItems.CartId == BookingCartId
                          select (int?)cartItems.Count).Sum();


            //return 0 if all entities null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply packages price by count of that package to get 
            // the current price for each of those package in the cart
            // sum all package price totals to get the cart total

            decimal? total = (from cartItems in storeDB.Cart
                              where cartItems.CartId == BookingCartId
                              select (int?)cartItems.Count * cartItems.Tourpackage.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Booking booking)
        {
            decimal BookingTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var BookingDetail = new BookingDetails
                {
                    PackageId = item.PackageId,
                    CustomerName = booking.CustomerName,
                    Bill = item.Tourpackage.Price,
                    DepurtureTime = booking.DeputureTime,

                };

                // Set the order total of the shopping cart
                BookingTotal += (item.Count * item.Tourpackage.Price);

                storeDB.BookingDetails.Add(BookingDetail);

            }

            // Set the order's total to the orderTotal count
            booking.Total = BookingTotal;

            // Save the order
            storeDB.SaveChanges();

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return booking.PackageId;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var bookingCart = storeDB.Cart.Where(c => c.CartId == BookingCartId);

            foreach (Cart item in bookingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}