using System;
using System.Collections.Generic;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class PayrollMaster
    {
        public string EmployeeId { get; set; }
        public string EmployeeGrade { get; set; }
        public string EmployeeDesignation { get; set; }
        public double? EmployeeSalary { get; set; }

        public virtual EmployeeMaster Employee { get; set; }
        public virtual PayrollDetail EmployeeGradeNavigation { get; set; }
    }
}
