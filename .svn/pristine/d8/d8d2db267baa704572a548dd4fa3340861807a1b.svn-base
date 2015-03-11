using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerteqDTModels;
using PowerteqDtData;
using PowerteqDTReport.Models;

namespace PowerteqDTReport.Controllers
{
	public class ReportsController : Controller
	{

		private IPowerteqContext PowerteqContext;
		public ReportsController(IPowerteqContext context)
		{
			// This is a disposible resource. There is a better way to get this, but we'll leave it for now.
			PowerteqContext = context;
		}
		//
		// GET: /Reports/

		public ActionResult Index()
		{
			return View();
		}


		public ActionResult Reports()
		{
			return View("Reports");
		}

		public ActionResult _Reports()
		{
			return PartialView("_Reports");
		}

		[HttpGet]
		public ActionResult ReportDepartmentAvailability(DateTime? startDate, DateTime? endDate)
		{
			//var EndTime = DateTime.Now;
			//var StartTime = DateTime.Now.AddDays(-28);
			//var uptimeHours = new TimeSpan(1);
			//if (EndTime != StartTime)
			//	uptimeHours = EndTime - StartTime;

			//var allDepartments = PowerteqContext.Departments;
			//var allLocations = PowerteqContext.Locations.ToList();
			//var downtimeEvents = PowerteqContext.DowntimeEvents.ToList();

			//List<ReportDeparmtentUptime> availDepts = new List<ReportDeparmtentUptime>();

			//foreach (var dep in allDepartments)
			//{
			//	ReportDeparmtentUptime d = new ReportDeparmtentUptime();
			//	d.DepartmentName = dep.DepartmentName;
			//	d.LocationName = (from a in allLocations
			//					  where a.ID == dep.LocationID
			//					  select new { a.LocationName }).ToString();
			//	d.DepartmentUptime = uptimeHours;
			//	availDepts.Add(d);

			//}

			var downtimeData = from d in PowerteqContext.Departments
							   select new
							   {
								   Department = d,
								   DowntimeEvents = d.DowntimeEvents.Where(n => n.StartDateTime >= startDate && n.StartDateTime <= endDate),
								   d.Location
							   };

			foreach (var de in downtimeData)
			{

			}

			return View();// /*availDepts*/);
		}



		public ActionResult ReportIncidents()
		{
			var EndTime = DateTime.Now;
			var StartTime = DateTime.Now.AddDays(-28);
			var allIncidents = PowerteqContext.DowntimeEvents.ToList();
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
			var DownTimes = PowerteqContext.DowntimeEvents.ToList();
			var Systems = PowerteqContext.Systems.ToList();

			foreach (var x in Systems)
			{
				ReportUptimeBySystem sys = new ReportUptimeBySystem();
				sys.SystemTimeUP = uptimeHours;
				sys.SystemName = x.SystemName;
				foreach (var y in DownTimes)
				{
					if (PowerteqContext.DowntimeEvents.Find(y.ID).AffectedSystems.Contains(x))
					{
						sys.SystemTimeUP -= y.StartDateTime - y.EndDateTime;
					}
				}
				SysUps.Add(sys);
			}
			return View(SysUps);
		}
	}
}
