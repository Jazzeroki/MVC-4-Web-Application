using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerteqDTReport.Models
{
	public class IndexSystemUptime
	{
		public string Department { get; set; }
		public string Location { get; set; }
		public double UptimePercentage { get; set; }
		public TimeSpan AmountTimeUp { get; set; }
		
	}

	public class UptimesViewModel
	{
		public UptimesViewModel()
		{
			UptimesList = new List<IndexSystemUptime>();
		}
		public List<IndexSystemUptime> UptimesList{ get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
	}
}