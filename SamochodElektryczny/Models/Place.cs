using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamochodElektryczny.Models
{
    public class Place
    {
        public int PlaceID { get; set; }
        public string Name { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public double Rate { get; set; }
        public string Type { get; set; }
    }
}