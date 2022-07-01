using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management.Data
{
    public class EmployeeManagerData
    {
        public string EmployeeName { get; set; }
        public int? EmployeeSalary { get; set; }
        public string ManagerName { get; set; }
        public int? ManagerTotalEmployees { get; set; }
        public int? ManagerSalary { get; set; }

    }
}
