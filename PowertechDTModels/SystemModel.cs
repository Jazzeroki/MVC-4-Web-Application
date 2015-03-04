﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerteqDTModels
{
	public class SystemModel
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public string SystemName { get; set; }
	}
}