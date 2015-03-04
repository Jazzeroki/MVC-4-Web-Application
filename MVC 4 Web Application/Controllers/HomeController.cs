﻿using System;
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
			// This is a disposible resource. There is a better way to get this, but we'll leave it for now.
			PowerteqContext = context;
		}

		public ActionResult Index()
		{
			var EndTime = DateTime.Now;
			var StartTime = DateTime.Now.AddDays(-28);
			
			var diff = StartTime - EndTime;
			var uptimeHours = diff;
			var Systems = PowerteqContext.Systems.AsEnumerable();
			var Locations = PowerteqContext.Locations.AsEnumerable();
			var Departments = PowerteqContext.Departments.AsEnumerable();
			var DowntimeEvents = PowerteqContext.DowntimeEvents.AsEnumerable();
			return View();
		}


		//[HttpGet]


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
			SystemModel System = PowerteqContext.Systems.Where(n => n.ID == Id).SingleOrDefault();
			return View(System);
		}

		[HttpPost]
		public ActionResult EditSystem(SystemModel modifiedSystem)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.Systems.Find(modifiedSystem.ID);
				PowerteqContext.Entry(existing).CurrentValues.SetValues(modifiedSystem);
				PowerteqContext.SaveChanges();
			} 
			else
			{
				return View(modifiedSystem);
			}

			return RedirectToAction("Systems");
		}
		public ActionResult DeleteSystem(SystemModel modifiedSystem)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.Systems.Find(modifiedSystem.ID);
				PowerteqContext.Systems.Remove(existing);
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedSystem);
			}

			return RedirectToAction("Systems");
		}


		[HttpGet]
		public ActionResult Departments()
		{
			return View(SetupDepartmentViewModel());
		}

		private Models.DepartmentViewModel SetupDepartmentViewModel()
		{
			return new Models.DepartmentViewModel
			{
				Departments = PowerteqContext.Departments.AsEnumerable(),
				Locations = PowerteqContext.Locations.AsEnumerable()
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
			DepartmentModel Department = PowerteqContext.Departments.Where(n => n.ID == Id).SingleOrDefault();
			
			//Departe
			IEnumerable<LocationModel> Locations = Locations = PowerteqContext.Locations.AsEnumerable(); 
			//return View(Department, Locations);
			return View(new { Department, Locations });
		}

		[HttpPost]
		public ActionResult EditDepartment(DepartmentModel modifiedDepartment)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.Departments.Find(modifiedDepartment.ID);
				PowerteqContext.Entry(existing).CurrentValues.SetValues(modifiedDepartment);
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedDepartment);
			}

			return RedirectToAction("Departments");
		}
		public ActionResult DeleteDepartment(DepartmentModel modifiedDepartment)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.Departments.Find(modifiedDepartment.ID);
				PowerteqContext.Departments.Remove(existing);
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedDepartment);
			}

			return RedirectToAction("Departments");
		}
		

		////Downtime Event Controllers




		[HttpGet]
		public ActionResult DowntimeEvent()
		{
			return View(SetupDowntimeEventViewModel());
		}

		private Models.DowntimeEventViewModel SetupDowntimeEventViewModel()
		{
			return new Models.DowntimeEventViewModel
			{
				DowntimeEvents = PowerteqContext.DowntimeEvents.AsEnumerable(),
				Locations = PowerteqContext.Locations.AsEnumerable(),
				Departments = PowerteqContext.Departments.AsEnumerable(),
				Systems = PowerteqContext.Systems.AsEnumerable()
			};
		}
		[HttpPost]
		public ActionResult DowntimeEvent(DowntimeEventModel downtimeEvent)
		{
			PowerteqContext.DowntimeEvents.Add(downtimeEvent);
			PowerteqContext.SaveChanges();
			return View(SetupDowntimeEventViewModel());
		}

		[HttpGet]
		public ActionResult EditDowntimeEvent(int Id)
		{
			DowntimeEventModel DowntimeEvent = PowerteqContext.DowntimeEvents.Where(n => n.ID == Id).SingleOrDefault();
			return View(DowntimeEvent);
		}

		[HttpPost]
		public ActionResult EditDowntimeEvent(DowntimeEventModel modifiedDowntimeEvent)
		{

			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.DowntimeEvents.Find(modifiedDowntimeEvent.ID);
				PowerteqContext.Entry(existing).CurrentValues.SetValues(modifiedDowntimeEvent);
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedDowntimeEvent);
			}

			return RedirectToAction("DowntimeEvents");
		}
		public ActionResult DeleteDowntimeEvent(DowntimeEventModel modifiedDowntimeEvent)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.DowntimeEvents.Find(modifiedDowntimeEvent.ID);
				//PowerteqContext.Entry(existing).CurrentValues.SetValues(modifiedLocation);
				PowerteqContext.DowntimeEvents.Remove(existing);
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedDowntimeEvent);
			}

			return RedirectToAction("DowntimeEvents");
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

		[HttpGet]
		public ActionResult EditLocation(int Id)
		{
			LocationModel Location = PowerteqContext.Locations.Where(n => n.ID == Id).SingleOrDefault();
			return View(Location);
		}

		[HttpPost]
		public ActionResult EditLocation(LocationModel modifiedLocation)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.Locations.Find(modifiedLocation.ID);
				PowerteqContext.Entry(existing).CurrentValues.SetValues(modifiedLocation);
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedLocation);
			}

			return RedirectToAction("Locations");
		}
		public ActionResult DeleteLocation(LocationModel modifiedLocation)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.Locations.Find(modifiedLocation.ID);
				//PowerteqContext.Entry(existing).CurrentValues.SetValues(modifiedLocation);
				PowerteqContext.Locations.Remove(existing);
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedLocation);
			}

			return RedirectToAction("Locations");
		}




	}
}
