using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerteqDTModels
{
	public class DowntimeEventModel
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public DateTime StartDateTime { get; set; }
		[Required]
		public DateTime EndDateTime { get; set; }
		public int LocationID { get; set; }
		[Required]
		public string Description { get; set; }
		public int DepartmentID { get; set; }
		public int SystemID { get; set; }
		public virtual ICollection<SystemModel> AffectedSystems { get; set; }
		public virtual ICollection<DepartmentModel> AffectedDepartments { get; set; }
	}
}
