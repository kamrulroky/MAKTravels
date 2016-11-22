using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MakTravels_Web.Models
{
    public class Cart
    {
        [Key]
        public int PackageId { get; set; }
        public string CartId { get; set; }
        public System.DateTime DateOrdered { get; set; }
        public int PackageType { get; set; }
        public int Count { get; set; }

        public virtual TourPackages Tourpackage { get; set; }
    }
}