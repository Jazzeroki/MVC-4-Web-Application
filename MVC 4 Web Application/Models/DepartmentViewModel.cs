﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PowerteqDTModels;

namespace PowerteqDTReport.Models
{
    public class DepartmentViewModel
    {
        public IEnumerable<DepartmentModel> Departments { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
