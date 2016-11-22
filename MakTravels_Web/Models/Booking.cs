using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakTravels_Web.Models
{
    [Bind(Exclude="Order Id")]
    public partial class Booking
    {
        [ScaffoldColumn(false)]
        public int PackageId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime BookingTime { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime DeputureTime { get; set; }

        [ScaffoldColumn(false)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "First Name Id Required")]
        [DisplayName("First Name")]
        [StringLength(150)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DisplayName("Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<BookingDetails> BookingDetail { get; set; }


    }
}