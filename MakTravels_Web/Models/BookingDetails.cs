using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MakTravels_Web.Models
{
    public class BookingDetails
    {
        [Key]
        public int PackageId { get; set; }
        public string PackageType { get; set; }
        public int CartId { get; set; }
        public string CustomerName { get; set; }
        public decimal Bill { get; set; }
        public System.DateTime DepurtureTime { get; set; }
        public int PackageDuration { get; set; }

    }
}