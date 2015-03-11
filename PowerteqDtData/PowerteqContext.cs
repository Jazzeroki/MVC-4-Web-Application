﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerteqDTModels;


namespace PowerteqDtData
{
	public class PowerteqContext : DbContext, IPowerteqContext
	{
		public DbSet<LocationModel> Locations{ get; set; }
		public DbSet<SystemModel> Systems { get; set; }
		public DbSet<DepartmentModel> Departments { get; set; }
		public DbSet<DowntimeEventModel> DowntimeEvents { get; set; }

		public PowerteqContext() : base("PowerteqSystemUptime")
		{

		}

		void IPowerteqContext.SaveChanges()
		{
			//base.SaveChanges();
			try
			{
				base.SaveChanges();
			}
			catch (Exception ex)
			{

			}
		}
	}
}
