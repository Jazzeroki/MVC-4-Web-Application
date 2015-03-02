using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerteqDTModels;


namespace PowerteqDtData
{
    public class PowerteqContext : DbContext
    {
        public DbSet<LocationModel> Locations{ get; set; }
        public DbSet<SystemModel> Systems { get; set; }
    }
}
