using PayrollManagementSystem_Sprint2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollManagementSystem_API.Models
{
    public class EmployeeMasterWithToken:EmployeeMaster
    {
        public EmployeeMasterWithToken(EmployeeMaster employee)
        {
            this.EmployeeId = employee.EmployeeId;
            this.EmployeeFirstname = employee.EmployeePassword;
            this.EmployeeDoj = employee.EmployeeDoj;
            this.EmployeeFirstname = employee.EmployeeFirstname;
            this.EmployeeLastname = employee.EmployeeLastname;
            this.EmployeeUserName = employee.EmployeeUserName;
            this.Gender = employee.Gender;
            this.MaritalStatus = employee.MaritalStatus;

            this.AdminPrivilege = employee.AdminPrivilege;
        }
    }
}
