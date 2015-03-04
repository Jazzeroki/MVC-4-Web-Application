using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PowerteqDTModels;

namespace PowerteqDTReport.Models
{
	public class DowntimeEventViewModel
	{
		public IEnumerable<DowntimeEventModel> DowntimeEvents { get; set; }
		public DowntimeEventModel DowntimeEvent { get; set; }
		public IEnumerable<LocationModel> Locations { get; set; }
		public IEnumerable<SystemModel> Systems { get; set; }
		public IEnumerable<DepartmentModel> Departments { get; set; }

	}
}