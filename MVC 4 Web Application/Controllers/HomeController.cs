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
        private IPowerteqContext PowerteqContext;

        //
        // GET: /Home/
        public HomeController(IPowerteqContext context)
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
        
        //Test of edit system Model
        [HttpGet]
        public ActionResult EditSystem(int Id)
        {
            //SystemModel System = PowerteqContext.Systems.Find(Id);
            SystemModel System = PowerteqContext.Systems.ElementAt<SystemModel>(Id);
            //Models.SystemViewModel sys = new Models.SystemViewModel();
            //sys.System = System;
            return View(System);
            //return View(System);
        }

        [HttpPost]
        public ActionResult EditSystem(SystemModel newSystem)
        {
            PowerteqContext.Systems.ElementAt<SystemModel>(newSystem.id).systemName = newSystem.systemName;
            //PowerteqContext.Systems.Remove(oldSystem);
            //PowerteqContext.Systems.Add(newSystem);
            return View();
            //return Systems();
        }


        //Department Controllers

        //public ActionResult Departments()
        //{
        //    return View("Departments");
        //}
        //public ActionResult DepartmentsEdit()
        //{
        //    return View("DepartmentsEdit");
        //}


        [HttpGet]
        public ActionResult Departments()
        {
            return View(SetupDepartmentViewModel());
        }

        private Models.DepartmentViewModel SetupDepartmentViewModel()
        {
            return new Models.DepartmentViewModel
            {
                Departments = PowerteqContext.Departments.AsEnumerable()
            };
        }
        [HttpPost]
        public ActionResult Departments(DepartmentModel department)
        {
            PowerteqContext.Departments.Add(department);
            PowerteqContext.SaveChanges();
            return View(SetupDepartmentViewModel());
        }

        [HttpGet]
        public ActionResult EditDepartment(int Id)
        {
            //SystemModel System = PowerteqContext.Systems.Find(Id);
            DepartmentModel Department = PowerteqContext.Departments.ElementAt<DepartmentModel>(Id);
            //Models.SystemViewModel sys = new Models.SystemViewModel();
            //sys.System = System;
            return View(Department);
            //return View(System);
        }

        [HttpPost]
        public ActionResult EditDepartment(DepartmentModel newDepartment)
        {
            PowerteqContext.Departments.ElementAt<DepartmentModel>(newDepartment.id).departmentName = newDepartment.departmentName;
            //PowerteqContext.Systems.Remove(oldSystem);
            //PowerteqContext.Systems.Add(newSystem);
            return View();
            //return Systems();
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
