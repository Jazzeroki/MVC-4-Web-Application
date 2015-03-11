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
	public class SystemsController : Controller
	{
		private IPowerteqContext PowerteqContext;
		public SystemsController(IPowerteqContext context)
		{
			// This is a disposible resource. There is a better way to get this, but we'll leave it for now.
			PowerteqContext = context;
		}
		//
		// GET: /Systems/

		public ActionResult Index()
		{
			return View();
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
				Systems = PowerteqContext.Systems.ToList()
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

	}
}
