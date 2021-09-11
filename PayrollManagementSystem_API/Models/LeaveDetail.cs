using System;
using System.Collections.Generic;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class LeaveDetail
    {
        public int IndexLd { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public DateTime? ApplyDate { get; set; }
        public double? LeaveDays { get; set; }
        public string Reason { get; set; }
        public string LeaveType { get; set; }
        public bool? LeaveStatus { get; set; }
        public int? LeaveMonth { get; set; }
        public string ApprovedBy { get; set; }

        public virtual EmployeeMaster Employee { get; set; }
        public virtual LeaveType LeaveTypeNavigation { get; set; }
    }
}
