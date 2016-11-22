using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MakTravels_Web.Models
{
    public class TourPackagesEntities : DbContext
    {
        public DbSet<TourPackages> TourPackage { get; set; }
        public DbSet<AirLines> Airline { get; set; }
        public DbSet<Hotels> Hotel { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }
    }
}