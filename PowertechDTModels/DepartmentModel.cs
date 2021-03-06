﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerteqDTModels
{
	public class DepartmentModel
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public string DepartmentName { get; set; }

		[Required]
		public int LocationID { get; set; } 

		public bool WorkdayMon { get; set; }
		public bool WorkdayTue { get; set; }
		public bool WorkdayWed { get; set; }
		public bool WorkdayThu { get; set; }
		public bool WorkdayFri { get; set; }
		public bool WorkdaySat { get; set; }
		public bool WorkdaySun { get; set; }

		[Required, ForeignKey("LocationID")]
		public virtual LocationModel Location { get; set; }

		[Required]
		[DataType(DataType.Time)]
		public DateTime StartWorkingHours { get; set; }

		[Required]
		[DataType(DataType.Time)]
		public DateTime EndWorkingHours { get; set; }

		public virtual ICollection<DowntimeEventModel> DowntimeEvents { get; set; }

	}
	
}
