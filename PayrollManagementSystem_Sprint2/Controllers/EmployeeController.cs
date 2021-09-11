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
    public class EmployeeController : Controller
    {

        static string localHostLink = "https://localhost:44314/";   //Common localhost link variable
        public IActionResult Index()
        {
            return View();
        }                 //Index Page
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

        #region Employee
        [HttpGet]
        public async Task<IActionResult> EmployeeDetailsView(string id)     //View self details
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

        public async Task<IActionResult> SelfAddressDetails(string id)     //View self details
        {
            var emp = new EmpAddress();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/EmpAddresses/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                emp = JsonConvert.DeserializeObject<EmpAddress>(apiResponse);
            }            
            return View(emp);
        }
                
        public async Task<IActionResult> EditAddress(string id)            //Edit Employee Address
        {
            EmpAddress emplist = new EmpAddress();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{localHostLink}api/EmpAddresses/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    emplist = JsonConvert.DeserializeObject<EmpAddress>(apiResponse);
                }
            }
            return View(emplist);
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(EmpAddress emp)
        {
            using (var httpClient = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(emp);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PutAsync($"{localHostLink}api/EmpAddresses/" + emp.EmployeeId, contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                ViewBagCheck(response.IsSuccessStatusCode);
            }
            return View(emp);
        }
        #endregion

        #region Leaves

        [HttpGet]
        public async Task<IActionResult> ViewLeaveDetails()
        {
            List<LeaveDetail> leaveDetail = new List<LeaveDetail>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/LeaveDetails");
            string apiResponse = await response.Content.ReadAsStringAsync();
            leaveDetail = JsonConvert.DeserializeObject<List<LeaveDetail>>(apiResponse);
            return View(leaveDetail);
        }

        public async Task<IActionResult> ViewLeaveMasterEmployee(string id)
        {
            var leaveMaster = new LeaveMaster();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/LeaveMasters/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                leaveMaster = JsonConvert.DeserializeObject<LeaveMaster>(apiResponse);
            }

            return View(leaveMaster);
        }

        public ActionResult ApplyForLeave()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ApplyForLeave(LeaveDetail leave)
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.PostAsJsonAsync<LeaveDetail>($"{localHostLink}api/LeaveDetails", leave);
            response.Wait();
            var result = response.Result;
            ViewBagCheck(result.IsSuccessStatusCode);
            return View();
        }

        #endregion

        #region Payroll 


        [HttpGet]
        public async Task<IActionResult> ViewPayrollMasterEmployee(string id)
        {
            var payrollMaster = new PayrollMaster();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{localHostLink}api/PayrollMasters/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                payrollMaster = JsonConvert.DeserializeObject<PayrollMaster>(apiResponse);
            }
            var temp1 = payrollMaster.EmployeeGrade;
            var temp2 = payrollMaster.EmployeeId;
            var temp3 = 0;
            return View(payrollMaster);
        }
        #endregion

        #region Timesheet 
        public ActionResult CreateTimesheet()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateTimesheet(TimeSheet employeeTimesheet)
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.PostAsJsonAsync<TimeSheet>($"{localHostLink}api/TimeSheets", employeeTimesheet);
            response.Wait();
            var result = response.Result;
            ViewBagCheck(result.IsSuccessStatusCode);
            return View();
        }

        #endregion

    }
}



//Payroll details employee