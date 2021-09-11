using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class LeaveDetail
    {
        public int IndexLd { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public DateTime? LeaveDate { get; set; }
        [Required]
        public DateTime? ApplyDate { get; set; }

        [Required]
        public double? LeaveDays { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string LeaveType { get; set; }
        
        public bool? LeaveStatus { get; set; }
        public int? LeaveMonth { get; set; }
        [Required]
        public string ApprovedBy { get; set; }

        public virtual EmployeeMaster Employee { get; set; }
        public virtual LeaveType LeaveTypeNavigation { get; set; }
    }
}
