using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class LoginTable
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string EmployeeUserName { get; set; }        
        public string SecurityAnswer { get; set; }

        public virtual EmployeeMaster Employee { get; set; }
    }
}
