using System;
using System.Collections.Generic;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class EmployeeMaster
    {
        public EmployeeMaster()
        {
            LeaveDetails = new HashSet<LeaveDetail>();
            TimeSheets = new HashSet<TimeSheet>();
        }

        public string EmployeeId { get; set; }
        
        public string EmployeePassword { get; set; }
        public DateTime EmployeeDoj { get; set; }
        public string EmployeeFirstname { get; set; }
        public string EmployeeLastname { get; set; }
        public string EmployeeUserName { get; set; }
        public bool? AdminPrivilege { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }

        public virtual EmpAddress EmpAddress { get; set; }
        public virtual LeaveMaster LeaveMaster { get; set; }
        public virtual LoginTable LoginTable { get; set; }
        public virtual PayrollMaster PayrollMaster { get; set; }
        public virtual ICollection<LeaveDetail> LeaveDetails { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
    }
}
