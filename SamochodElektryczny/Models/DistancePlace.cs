using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamochodElektryczny.Models
{
    public class DistancePlace
    {
        public Place place { get; set; }
        public Double distance { get; set; }

        public DistancePlace(Double distance, Place place)
        {
            this.distance = distance;
            this.place = place;
        }
    }
}