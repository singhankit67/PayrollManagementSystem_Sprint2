using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayrollManagementSystem_Sprint2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem_Sprint2.Controllers
{
    
    public class AdminController : Controller
    {
        static string localHostLink = "https://localhost:44314/";  //Common localhost link variable

        
        public IActionResult Index()
        {            
                return View();
        }                           //Index 

        public void ViewBagCheck(bool variable)
        {
            if (variable)
            {
                ViewBag.Message = "Operation Successfull";
                ViewBag.Color = "green";
            }
            else
            {
                ViewBag.Message = "Operation unsuccessful";
                ViewBag.Color = "red";
            }
        }
        
        #region Employees

        public async Task<IActionResult> Employees()                 //View All Employees
        {
            List<EmployeeMaster> empList = new List<EmployeeMaster>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/EmployeeMasters");
            string apiResponse = await response.Content.ReadAsStringAsync();
            empList = JsonConvert.DeserializeObject<List<EmployeeMaster>>(apiResponse);
            return View(empList);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDelete(string id)     //Delete one employee
        {
            var emp = new EmployeeMaster();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/EmployeeMasters/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                emp = JsonConvert.DeserializeObject<EmployeeMaster>(apiResponse);                
            }
            return View(emp);
        }    
        [HttpPost]
        public async Task<IActionResult> EmployeeDelete(string id, EmployeeMaster emp)  //Post method Delete one employee
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{localHostLink}api/EmployeeMasters/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                    ViewBagCheck(response.IsSuccessStatusCode);
                }  
                
            }
            return View(); 
        }  

        public async Task<IActionResult> ViewAddressDetails(string id)                 //View All Employee Addresses
        {
            var empAddress = new EmpAddress();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/EmpAddresses/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                empAddress = JsonConvert.DeserializeObject<EmpAddress>(apiResponse);
            }
            return View(empAddress);
        }

        public ActionResult CreateNewEmployee()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewEmployee(EmployeeMaster employee)
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.PostAsJsonAsync<EmployeeMaster>($"{localHostLink}api/EmployeeMasters", employee);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "New Employee Created Successfully";
                ViewBag.Color = "green";
            }
            else
            {
                ViewBag.Message = "Exployee not inserted,Kindly check all the details again";
                ViewBag.Color = "red";
            }
            return View();
        }

        public async Task<IActionResult> EditEmployeeDetails(string id)            
        {
            EmployeeMaster emplist = new EmployeeMaster();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{localHostLink}api/EmployeeMasters/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    emplist = JsonConvert.DeserializeObject<EmployeeMaster>(apiResponse);
                }
            }
            return View(emplist);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployeeDetails(EmployeeMaster employee)
        {
            using (var httpClient = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(employee);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PutAsync($"{localHostLink}api/EmployeeMasters/" + employee.EmployeeId, contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                ViewBagCheck(response.IsSuccessStatusCode);
            }
            return View(employee);
        }       

        #endregion

        #region Timesheet

        public async Task<IActionResult> EmployeeTimesheets()                 //View All Timesheets
        {
            List<TimeSheet> timesheetList = new List<TimeSheet>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/TimeSheets");
            string apiResponse = await response.Content.ReadAsStringAsync();
            timesheetList = JsonConvert.DeserializeObject<List<TimeSheet>>(apiResponse);
            return View(timesheetList);                        
        }

        public async Task<IActionResult> EditTimesheet(int id)             //Approve Timesheets
        {
            TimeSheet timesheetList = new TimeSheet();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{localHostLink}api/Timesheets/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    timesheetList = JsonConvert.DeserializeObject<TimeSheet>(apiResponse);
                }
            }
            return View(timesheetList);
        }

        [HttpPost]
        public async Task<IActionResult> EditTimesheet(TimeSheet timesheet)
        {
            using (var httpClient = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(timesheet);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PutAsync($"{localHostLink}api/Timesheets/" + timesheet.IndexTs, contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                ViewBagCheck(response.IsSuccessStatusCode);
            }
            return View(timesheet);
        }    //Post method

        #endregion

        #region Payroll
        //View Salary report of all employees	

        public async Task<IActionResult> PayrollReport()
        {
            List<PayrollMaster> payrollMaster = new List<PayrollMaster>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/PayrollMasters");
            string apiResponse = await response.Content.ReadAsStringAsync();
            payrollMaster = JsonConvert.DeserializeObject<List<PayrollMaster>>(apiResponse);
            return View(payrollMaster);
        }
        public async Task<IActionResult> ViewPayrollDetails(string id)
        {
            var payrollDetail = new PayrollMaster();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/PayrollMasters/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                payrollDetail = JsonConvert.DeserializeObject<PayrollMaster>(apiResponse);
            }
            return View(payrollDetail);
        }
        public async Task<IActionResult> EditGradeDetails(string id)            //Edit Employee Details
        {
            PayrollMaster emplist = new PayrollMaster();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{localHostLink}api/PayrollMasters/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    emplist = JsonConvert.DeserializeObject<PayrollMaster>(apiResponse);
                }
            }
            return View(emplist);
        }
        [HttpPost]
        public async Task<IActionResult> EditGradeDetails(PayrollMaster employee2)
        {
            using (var httpClient = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(employee2);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PutAsync($"{localHostLink}api/PayrollMasters/" + employee2.EmployeeId, contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                ViewBagCheck(response.IsSuccessStatusCode);
            }
            return View(employee2);
        }

        #endregion

        #region Leaves
        public async Task<IActionResult> LeaveDetails()
        {
            List<LeaveDetail> leaveDetail = new List<LeaveDetail>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/LeaveDetails");
            string apiResponse = await response.Content.ReadAsStringAsync();
            leaveDetail = JsonConvert.DeserializeObject<List<LeaveDetail>>(apiResponse);
            return View(leaveDetail);
        }

        public async Task<IActionResult> ViewLeaveDetails(string id)
        {
            var leaveDetail = new LeaveDetail();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/LeaveDetails/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                leaveDetail = JsonConvert.DeserializeObject<LeaveDetail>(apiResponse);
            }
            return View(leaveDetail);
        }

        public async Task<IActionResult> EditLeaveStatus(int id)
        {
            LeaveDetail leaveDetail = new LeaveDetail();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{localHostLink}api/LeaveDetails/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    leaveDetail = JsonConvert.DeserializeObject<LeaveDetail>(apiResponse);
                }
            }
            return View(leaveDetail);
        }

        [HttpPost]
        public async Task<IActionResult> EditLeaveStatus(LeaveDetail leaveDetail)
        {
            using (var httpClient = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(leaveDetail);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PutAsync($"{localHostLink}api/LeaveDetails/" + leaveDetail.IndexLd, contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                ViewBagCheck(response.IsSuccessStatusCode);
            }
            return View(leaveDetail);

        }

        public async Task<IActionResult> LeaveMaster()
        {
            List<LeaveMaster> leaveMaster = new List<LeaveMaster>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/LeaveMasters");
            string apiResponse = await response.Content.ReadAsStringAsync();
            leaveMaster = JsonConvert.DeserializeObject<List<LeaveMaster>>(apiResponse);
            return View(leaveMaster);
        }       

        #endregion

    }
}
