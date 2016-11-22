using MakTravels_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakTravels_Web.ViewModels
{
    public class BookingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}