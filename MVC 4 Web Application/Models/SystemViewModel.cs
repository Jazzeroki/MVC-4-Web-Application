using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PowerteqDTModels;

namespace PowerteqDTReport.Models
{
    public class SystemViewModel
    {
        public IEnumerable<SystemModel> Systems { get; set; }
        public SystemModel System { get; set; }
    }
}