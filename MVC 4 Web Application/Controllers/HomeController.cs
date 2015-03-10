﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerteqDTModels;
using PowerteqDtData;
using PowerteqDTReport.Models;

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
		[HttpGet]
		public ActionResult Index()
		{
			Models.UptimesViewModel UptimesModel = new Models.UptimesViewModel();
			var EndTime = DateTime.Now;
			var StartTime = DateTime.Now.AddDays(-28);
			var uptimeHours = new TimeSpan(0);
			if (EndTime != StartTime)
				uptimeHours = EndTime - StartTime;

			var Systems = PowerteqContext.Systems.AsEnumerable();
			var Locations = PowerteqContext.Locations.AsEnumerable();
			var Departments = PowerteqContext.Departments.AsEnumerable();
			var DowntimeEvents = PowerteqContext.DowntimeEvents.AsEnumerable();

			List<DowntimeEventModel> DowntimesInScope = new List<DowntimeEventModel>();
			foreach (var down in DowntimeEvents)
			{
				if (down.StartDateTime >= StartTime && down.EndDateTime <= EndTime)
					DowntimesInScope.Add(down);
			}
			foreach (var dep in Departments)
			{
				var i = new Models.IndexSystemUptime();
				i.Department = dep.DepartmentName;
				var l = from loc in Locations
						where dep.LocationID == loc.ID
						select loc.LocationName;
				i.Location = l.First().ToString();
				i.AmountTimeUp = uptimeHours;

				//getting downtime data and calculating down time
				foreach (var devent in DowntimeEvents)
				{

					//if (dep.ID == devent.DepartmentID)
					//{
					//	i.AmountTimeUp -= devent.EndDateTime - devent.StartDateTime;
					//}
				}
				if (uptimeHours.TotalMilliseconds != 0)
					i.UptimePercentage = i.AmountTimeUp.TotalMinutes / uptimeHours.TotalMinutes;
				else
					i.UptimePercentage = 100;
				//if(DowntimeEvents.Contains)
				UptimesModel.UptimesList.Add(i);
				//UptimeModel..Add(i);

			}

			return View(UptimesModel);
		}


		[HttpPost]
		public ActionResult Index(Models.UptimesViewModel post)
		{
			Models.UptimesViewModel UptimesModel = new Models.UptimesViewModel();
			var EndTime = post.EndDateTime;
			var StartTime = post.StartDateTime;
			TimeSpan uptimeHours =  new TimeSpan(0);
			if(StartTime != EndTime)
				uptimeHours = EndTime - StartTime;
			

			var Systems = PowerteqContext.Systems.AsEnumerable();
			var Locations = PowerteqContext.Locations.AsEnumerable();
			var Departments = PowerteqContext.Departments.AsEnumerable();
			var DowntimeEvents = PowerteqContext.DowntimeEvents.AsEnumerable();

			List<DowntimeEventModel> DowntimesInScope = new List<DowntimeEventModel>();
			foreach (var down in DowntimeEvents)
			{
				if (down.StartDateTime >= StartTime && down.EndDateTime <= EndTime)
					DowntimesInScope.Add(down);
			}
			foreach (var dep in Departments)
			{
				var i = new Models.IndexSystemUptime();
				i.Department = dep.DepartmentName;
				var l = from loc in Locations
						where dep.LocationID == loc.ID
						select loc.LocationName;
				i.Location = l.First().ToString();
				i.AmountTimeUp = uptimeHours;

				//getting downtime data and calculating down time
				foreach (var devent in DowntimeEvents)
				{
					//if (dep.ID == devent.DepartmentID)
					//{
					//	i.AmountTimeUp -= devent.EndDateTime - devent.StartDateTime;
					//}
				}

				if(uptimeHours.TotalMilliseconds !=0)
					i.UptimePercentage = i.AmountTimeUp.TotalMinutes / uptimeHours.TotalMinutes;
				else
					i.UptimePercentage = 100;
				//if(DowntimeEvents.Contains)
				UptimesModel.UptimesList.Add(i);
				//UptimeModel..Add(i);

			}

			return View(UptimesModel);
		}

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
			//DepartmentModel Department = PowerteqContext.Departments.Where(n => n.ID == Id).SingleOrDefault();
			EditDepartmentViewModel model = new EditDepartmentViewModel();
			model.Department = PowerteqContext.Departments.Where(n => n.ID == Id).SingleOrDefault();
			
			//Departe
			model.Locations = PowerteqContext.Locations.AsEnumerable(); 
			//return View(Department, Locations);
			return View(model);
		}

		[HttpPost]
		//public ActionResult EditDepartment(DepartmentModel modifiedDepartment)
		public ActionResult EditDepartment(EditDepartmentViewModel modifiedDepartment)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.Departments.Find(modifiedDepartment.Department.ID);
				PowerteqContext.Entry(existing).CurrentValues.SetValues(modifiedDepartment.Department);
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
			DowntimeEventModel dt = new DowntimeEventModel();
			dt.AffectedSystems = new List<int>();
			dt.AffectedDepartments = new List<int>();



			return new Models.DowntimeEventViewModel
			{
			//	downtimeEvent.AffectedSystems = new List<int>();
			//downtimeEvent.AffectedDepartments = new List<int>();
				DowntimeEvent = dt,
				DowntimeEvents = PowerteqContext.DowntimeEvents.AsEnumerable(),
				Locations = PowerteqContext.Locations.AsEnumerable(),
				Departments = PowerteqContext.Departments.AsEnumerable(),
				Systems = PowerteqContext.Systems.AsEnumerable()
			};
		}
		[HttpPost]
		public ActionResult DowntimeEvent(DowntimeEventModel downtimeEvent)
		{
			
			//This line is meant to populate affected Departments
			//downtimeEvent.AffectedDepartments = new List<int>();
			//downtimeEvent.AffectedDepartments.Add(downtimeEvent.DepartmentID);
			//downtimeEvent.AffectedSystems.Add(downtimeEvent.SystemID);
			

			PowerteqContext.DowntimeEvents.Add(downtimeEvent);
			PowerteqContext.SaveChanges();
			return View(SetupDowntimeEventViewModel());
		}

		[HttpGet]
		public ActionResult EditDowntimeEvent(int Id)

		{

			Models.EditDTEventModel DTEvent = new Models.EditDTEventModel();
			DTEvent.DTEvent = PowerteqContext.DowntimeEvents.Where(n => n.ID == Id).SingleOrDefault();
			DTEvent.Systems = PowerteqContext.Systems.AsEnumerable();
			DTEvent.Locations = PowerteqContext.Locations.AsEnumerable();
			DTEvent.Departments = PowerteqContext.Departments.AsEnumerable();
	
			return View(DTEvent);
		}
		
		[HttpPost]
		public ActionResult EditDowntimeEvent(EditDTEventModel modifiedDowntimeEvent)
		{

			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.DowntimeEvents.Find(modifiedDowntimeEvent.DTEvent.ID);
				if (modifiedDowntimeEvent.DTEvent.AffectedDepartments.Count != 0)
				{
					existing.AffectedDepartments = new List<int>();
					foreach (var x in modifiedDowntimeEvent.DTEvent.AffectedDepartments)
					{
						existing.AffectedDepartments.Add(x);
					}
				}
				if (modifiedDowntimeEvent.DTEvent.AffectedSystems.Count != 0)
				{
					existing.AffectedSystems = new List<int>();
					foreach (var x in modifiedDowntimeEvent.DTEvent.AffectedSystems)
					{
						existing.AffectedSystems.Add(x);
					}
				}
				PowerteqContext.Entry(existing).CurrentValues.SetValues(modifiedDowntimeEvent.DTEvent);
				
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedDowntimeEvent);
			}

			return RedirectToAction("DowntimeEvent");
		}

		public ActionResult DeleteDowntimeEvent(DowntimeEventModel modifiedDowntimeEvent)
		{
			if (ModelState.IsValid)
			{
				var existing = PowerteqContext.DowntimeEvents.Find(modifiedDowntimeEvent.ID);
				PowerteqContext.DowntimeEvents.Remove(existing);
				PowerteqContext.SaveChanges();
			}
			else
			{
				return View(modifiedDowntimeEvent);
			}

			return RedirectToAction("DowntimeEvents");
		}

		//
		// REPORTS
		//
		//

		public ActionResult Reports()
		{
			return View("Reports");
		}

		public ActionResult _Reports()
		{
			return PartialView("_Reports");
		}

		[HttpGet]
		public ActionResult ReportDepartmentAvailability()
		{
			return View("");
		}



		public ActionResult ReportIncidents()
		{
			var EndTime = DateTime.Now;
			var StartTime = DateTime.Now.AddDays(-28);
			var allIncidents = PowerteqContext.DowntimeEvents.AsEnumerable();
			ReportIncidents reports = new ReportIncidents();
			if (allIncidents.Count<object>() != 0)
			{
				foreach (var x in allIncidents)
				{
					if (x.EndDateTime <= EndTime && x.StartDateTime >= StartTime)
					{
						reports.Incidents.Add(x);
					}
				}
			}
			return View(reports);
		}

		public ActionResult ReportUptimeBySystems()
		{

			var EndTime = DateTime.Now;
			var StartTime = DateTime.Now.AddDays(-28);
			var uptimeHours = new TimeSpan(1);
			if (EndTime != StartTime)
				uptimeHours = EndTime - StartTime;

			List<ReportUptimeBySystem> SysUps = new List<ReportUptimeBySystem>();
			var DownTimes = PowerteqContext.DowntimeEvents.AsEnumerable();
			var Systems = PowerteqContext.Systems.AsEnumerable();

			foreach (var x in Systems)
			{
				ReportUptimeBySystem sys = new ReportUptimeBySystem();
				sys.SystemTimeUP = uptimeHours;
				sys.SystemName = x.SystemName;
				foreach (var y in DownTimes)
				{
					//var affectedSystems = PowerteqContext.DowntimeEvents.Find(y.ID).AffectedSystems;
					//if (PowerteqContext.DowntimeEvents.Find(y.ID).AffectedSystems.Contains(x.ID))
					if(PowerteqContext.DowntimeEvents.Find(y.ID).AffectedSystems.Contains(x.ID))
					{
						sys.SystemTimeUP -= y.StartDateTime - y.EndDateTime;
					}
				}
				SysUps.Add(sys);
			}
			//Models.EditDTEventModel DTEvent = new Models.EditDTEventModel();
			////DTEvent.DTEvent = PowerteqContext.DowntimeEvents.Where(n => n.ID == Id).SingleOrDefault();
			//DTEvent.DTEvent = PowerteqContext.DowntimeEvents.AsEnumerable();
			//DTEvent.Systems = PowerteqContext.Systems.AsEnumerable();
			//DTEvent.Locations = PowerteqContext.Locations.AsEnumerable();
			//DTEvent.Departments = PowerteqContext.Departments.AsEnumerable();
			return View(SysUps);
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
