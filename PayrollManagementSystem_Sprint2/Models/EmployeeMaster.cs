using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string EmployeePassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EmployeeDoj { get; set; }


        [Required]        
        public string EmployeeFirstname { get; set; }
        [Required]
        public string EmployeeLastname { get; set; }
        
        public string EmployeeUserName { get; set; }

        [Required]        
        public bool AdminPrivilege { get; set; }

        [Required]        
        public string Gender { get; set; }
        [Required]
        public string MaritalStatus { get; set; }

        public virtual EmpAddress EmpAddress { get; set; }
        public virtual LeaveMaster LeaveMaster { get; set; }
        public virtual LoginTable LoginTable { get; set; }
        public virtual PayrollMaster PayrollMaster { get; set; }
        public virtual ICollection<LeaveDetail> LeaveDetails { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
    }
}
