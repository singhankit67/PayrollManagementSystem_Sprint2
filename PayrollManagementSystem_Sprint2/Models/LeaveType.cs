using System;
using System.Collections.Generic;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class LeaveType
    {
        public LeaveType()
        {
            LeaveDetails = new HashSet<LeaveDetail>();
        }

        public string TypesOfLeave { get; set; }

        public virtual ICollection<LeaveDetail> LeaveDetails { get; set; }
    }
}
