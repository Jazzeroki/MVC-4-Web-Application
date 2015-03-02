using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerteqDTModels;

namespace PowerteqDtData
{
	public class PowerteqContextMock: IPowerteqContext
	{
		private DbSet<LocationModel> _locations;
		private DbSet<SystemModel> _systems;
		private DbSet<DepartmentModel> _departments;
		private DbSet<DowntimeEventModel> _downtimeEvents;

		public DbSet<LocationModel> Locations
		{
			get { return _locations; }
			set { _locations = value; }
		}

		public DbSet<SystemModel> Systems
		{
			get { return _systems; }
			set { _systems = value; }
		}

		public DbSet<DepartmentModel> Departments
		{
			get { return _departments; }
			set { _departments = value; }
		}

		public DbSet<DowntimeEventModel> DowntimeEvents
		{
			get { return _downtimeEvents; }
			set { _downtimeEvents = value; }
		}

		public void SaveChanges()
		{
			//this is an in memory mock so this will probably not do anyting.
		}
	}
}
