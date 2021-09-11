using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class EmployeeMasterMVC
    {
        public EmployeeMasterMVC()
        {
            LeaveDetails = new HashSet<LeaveDetail>();
            TimeSheets = new HashSet<TimeSheetMVC>();
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
        public virtual LoginTableMVC LoginTable { get; set; }
        public virtual PayrollMasterMVC PayrollMaster { get; set; }
        public virtual ICollection<LeaveDetail> LeaveDetails { get; set; }
        public virtual ICollection<TimeSheetMVC> TimeSheets { get; set; }
    }
}
