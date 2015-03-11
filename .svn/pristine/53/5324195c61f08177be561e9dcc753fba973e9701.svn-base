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
	public class LocationsController : Controller
	{
		private IPowerteqContext PowerteqContext;
		public LocationsController(IPowerteqContext context)
		{
			// This is a disposible resource. There is a better way to get this, but we'll leave it for now.
			PowerteqContext = context;
		}

		//
		// GET: /Locations/

		public ActionResult Index()
		{
			return View();
		}


		[HttpGet]
		public ActionResult Locations()
		{
			return View(SetupLocationViewModel());
		}

		private Models.LocationViewModel SetupLocationViewModel()
		{
			return new Models.LocationViewModel
			{
				Locations = PowerteqContext.Locations.ToList()
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
