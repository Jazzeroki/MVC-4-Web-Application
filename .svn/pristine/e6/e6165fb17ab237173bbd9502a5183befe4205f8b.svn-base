using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerteqDTModels;
using PowerteqDtData;

namespace PowerteqDTReport.Controllers
{
	public class DowntimeEventController : Controller
	{
		private IPowerteqContext PowerteqContext;

		//
		// GET: /Home/
		public DowntimeEventController(IPowerteqContext context)
		{
			// This is a disposible resource. There is a better way to get this, but we'll leave it for now.
			PowerteqContext = context;
		}

		public ActionResult GetDropdowns()
		{
			return View();
		}

		public ActionResult EditDTEvent(int ID){
			Models.EditDTEventModel DTEvent = new Models.EditDTEventModel();
		
			DTEvent.Systems = PowerteqContext.Systems.AsEnumerable();
			DTEvent.Locations = PowerteqContext.Locations.AsEnumerable();
			DTEvent.Departments = PowerteqContext.Departments.AsEnumerable();
			return View();
		}

	}
}
