using System;
using System.Collections.Generic;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class EmpAddress
    {
        public string EmployeeId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Pincode { get; set; }

        public virtual EmployeeMaster Employee { get; set; }
    }
}
