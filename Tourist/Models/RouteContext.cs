using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Tourist.Models
{
    public class RouteContext : DbContext
    {
        public DbSet<Routes> Routes { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Destination> Destinations { get; set; }
    }
}