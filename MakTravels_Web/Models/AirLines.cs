using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakTravels_Web.Models
{
    public class AirLines
    {
        public int AirLinesId { get; set; }
        public string AirLinesName { get; set; }
        public string DepurtureTime { get; set; }
        public string Class { get; set; }
        public string Route { get; set; }

        public List<TourPackages> TourPackage { get; set; }
    }
}