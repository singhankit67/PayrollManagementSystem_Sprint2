using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PayrollManagementSystem_Sprint2.Models;

namespace PayrollManagementSystem_API.Models
{
    public class AuthorizationResponse
    {
        public EmployeeMaster employeeObj { get; set; }
        public string _token { get; set; }

        public AuthorizationResponse(EmployeeMaster empObj,string tokenObj)
        {
            employeeObj = empObj;
            _token = tokenObj;
        }


    }
}
