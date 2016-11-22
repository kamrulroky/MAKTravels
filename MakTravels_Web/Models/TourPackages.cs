using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakTravels_Web.Models
{
    [Bind(Exclude = "PackageId")]
    public class TourPackages
    {
        [ScaffoldColumn(false)]
        public int PackageId { get; set; }

        [DisplayName("Airlines")]
        public int AirLinesId { get; set; }

        [DisplayName("Hotels")]
        public string HotelsId { get; set; }

        [DisplayName("Depurture Time")]
        public int DepurtureTime { get; set; }


        [Required(ErrorMessage = "Price is Required")]
        [Range(5000.00, 1000000.00, ErrorMessage = "Price Must be Between 5000.00-1000000.00")]
        public decimal Price { get; set; }

        public virtual Hotels Hotel { get; set; }
        public virtual AirLines Airline { get; set; }
        public List<BookingDetails> BookingDetail { get; set; }


    }
}