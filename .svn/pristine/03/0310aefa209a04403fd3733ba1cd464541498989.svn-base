using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using PowerteqDTModels;

namespace PowerteqDtData
{
	public interface IPowerteqContext
	{
		DbSet<LocationModel> Locations { get; set; }
		DbSet<SystemModel> Systems { get; set; }
		DbSet<DepartmentModel> Departments { get; set; }
		DbSet<DowntimeEventModel> DowntimeEvents { get; set; }
		void SaveChanges();
	}
}