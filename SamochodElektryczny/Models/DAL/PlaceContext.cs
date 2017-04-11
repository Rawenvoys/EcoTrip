using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SamochodElektryczny.Models.DAL
{
    public class PlaceContext : DbContext
    {
        public PlaceContext() : base("PlaceContext")
        {
        }

        public DbSet<Place> Places { get; set; }

    }
}