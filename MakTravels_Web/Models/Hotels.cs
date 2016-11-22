using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakTravels_Web.Models
{
    public class Hotels
    {
        public int HotelsId { get; set; }
        public string HotelName { get; set; }
        public string RoomType { get; set; }
        public string Location { get; set; }
    }
}