using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerteqDTModels;

namespace PowerteqDTReport.Models
{
	public class EditDepartmentViewModel : Controller
	{
		public DepartmentModel Department { get; set; }
		public IEnumerable<LocationModel> Locations { get; set; }

	}
}
