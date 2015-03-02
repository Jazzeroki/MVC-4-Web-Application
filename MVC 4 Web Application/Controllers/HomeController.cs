using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerteqDTModels;
using PowerteqDtData;

namespace PowerteqDTReport.Controllers
{
    public class HomeController : Controller
    {
        private PowerteqContext PowerteqContext;

        //
        // GET: /Home/
        public HomeController(PowerteqContext context)
        {
            PowerteqContext = context;
        }

        public ActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        //public ActionResult Systems()
        //{
        //    return View("Systems");
        //}


        [HttpGet]
        public ActionResult Systems()
        {
            return View(SetupSystemViewModel());
        }

        private Models.SystemViewModel SetupSystemViewModel()
        {
            return new Models.SystemViewModel
            {
                Systems = PowerteqContext.Systems.AsEnumerable()
            };
        }
        [HttpPost]
        public ActionResult Systems(SystemModel system)
        {
            PowerteqContext.Systems.Add(system);
            PowerteqContext.SaveChanges();
            return View(SetupSystemViewModel());
        }


        public ActionResult Departments()
        {
            return View("Departments");
        }
        public ActionResult DepartmentsEdit()
        {
            return View("DepartmentsEdit");
        }
        public ActionResult DowntimeEvent()
        {
            return View("DowntimeEvent");
        }
        public ActionResult Reports()
        {
            return View("Reports");
        }
        [HttpGet]
        public ActionResult Locations()
        {
            return  View(SetupLocationViewModel());
        }

        private Models.LocationViewModel SetupLocationViewModel()
        {
            return new Models.LocationViewModel
            {
                Locations = PowerteqContext.Locations.AsEnumerable()                
            };
        }
        [HttpPost]
        public ActionResult Locations(LocationModel location)
        {
            PowerteqContext.Locations.Add(location);
            PowerteqContext.SaveChanges();
            return View(SetupLocationViewModel());
        }

    }
}
