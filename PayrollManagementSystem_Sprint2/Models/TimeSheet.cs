using System;
using System.Collections.Generic;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class TimeSheet
    {
        public int IndexTs { get; set; }
        public string EmployeeId { get; set; }
        public string TimesheetId { get; set; }
        public DateTime WorkDate { get; set; }
        public string EmployeeActivity { get; set; }
        public int? WorkMonth { get; set; }
        public double? NumberOfHoursSpent { get; set; }
        public double? TotalHoursPerDay { get; set; }
        public bool? Status { get; set; }

        public virtual EmployeeMaster Employee { get; set; }
    }
}
