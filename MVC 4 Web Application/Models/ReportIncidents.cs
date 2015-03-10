using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PowerteqDTModels;

namespace PowerteqDTReport.Models
{
	public class ReportIncidents	
	{
		public ReportIncidents()
		{
			Incidents = new List<DowntimeEventModel>();
		}
		public List<DowntimeEventModel> Incidents { get; set; }
	}
}