using System;
using System.Collections.Generic;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class LeaveMaster
    {
        public string EmployeeId { get; set; }
        public double? LeavesAvailable { get; set; }
        public double? LeavesAvailed { get; set; }
        public double? LeavesBalance { get; set; }

        public virtual EmployeeMaster Employee { get; set; }
    }
}
