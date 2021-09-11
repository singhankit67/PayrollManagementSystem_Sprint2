using System;
using System.Collections.Generic;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class PayrollDetail
    {
        public PayrollDetail()
        {
            PayrollMasters = new HashSet<PayrollMaster>();
        }

        public string EmployeeGrade { get; set; }
        public double? EmployeeBasic { get; set; }
        public double? EmployeeHra { get; set; }
        public double? EmployeeDa { get; set; }
        public double? EmployeePf { get; set; }
        public double? NetSalary { get; set; }
        public double? TotalWorkingDays { get; set; }

        public virtual ICollection<PayrollMaster> PayrollMasters { get; set; }
    }
}
