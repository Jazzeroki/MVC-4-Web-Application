using PowerteqDTModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerteqDTReport.Models
{
    public class LocationViewModel
    {
        public IEnumerable<LocationModel> Locations { get; set;}
        public LocationModel Location { get; set; }
    }
}