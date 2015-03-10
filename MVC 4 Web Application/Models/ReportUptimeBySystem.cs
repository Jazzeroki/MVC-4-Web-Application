using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerteqDTModels;

namespace PowerteqDTReport.Models
{
	public class ReportUptimeBySystem : Controller
	{
		public string SystemName { get; set; }
		public TimeSpan SystemTimeUP { get; set; }
	   // //
	   // // GET: /ReportUptimeBySystem/

	   //public ReportUptimeBySystem()
	   // {
	   //	 Incidents = new List<DowntimeEventModel>();
	   //	 Systems = new List<SystemModel>();
	   // }
	   // public List<DowntimeEventModel> Incidents { get; set; }
	   // public List<SystemModel> Systems { get; set; }
	   // public double SystemTimeUp { get; set; }

	}
}
