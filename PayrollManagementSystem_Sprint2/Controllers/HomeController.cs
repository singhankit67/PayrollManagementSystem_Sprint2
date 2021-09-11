using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PayrollManagementSystem_Sprint2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PMS_SYSTEM_SPRINT2.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> EmployeeAddress()
        {
            List<EmpAddress> empAddress = new List<EmpAddress>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:44314/api/EmpAddresses");
            string apiResponse = await response.Content.ReadAsStringAsync();
            empAddress = JsonConvert.DeserializeObject<List<EmpAddress>>(apiResponse);
            return View(empAddress);
        }

        public async Task<IActionResult> Details(string id)
        {
            var empAddress = new EmpAddress();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:44314/api/EmpAddresses/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                empAddress = JsonConvert.DeserializeObject<EmpAddress>(apiResponse);
            }
            
            return View(empAddress);
        }
        [HttpPost]
        //public async Task<IActionResult> Details(string id)
        //{
        //    var empAddress = new EmpAddress();
        //    HttpClient httpClient = new HttpClient();
        //    var response = await httpClient.GetAsync($"https://localhost:44309/api/EmpAddresses/{id}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        empAddress = JsonConvert.DeserializeObject<EmpAddress>(apiResponse);
        //    }

        //    return View(empAddress);
        //}

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(EmpAddress empAddress)
        {

            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(empAddress), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:44336/api/PostEmployee", content);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (JsonConvert.DeserializeObject<EmpAddress>(apiResponse) != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            EmpAddress empAddress = new EmpAddress();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:44309/api/EmpAddresses/{id}")) 
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    empAddress = JsonConvert.DeserializeObject<EmpAddress>(apiResponse);
                }
            }
            return View(empAddress);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmpAddress empAddress)
        {
            using (var httpClient = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(empAddress);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response =  httpClient.PutAsync("https://localhost:44309/api/EmpAddresses/"+empAddress.EmployeeId, contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            }
            return View(empAddress);
                


            //EmpAddress empEditObj = new EmpAddress();
            //using (var httpClient = new HttpClient())
            //{


            //    var content = new MultipartFormDataContent();
            //    content.Add(new StringContent(empAddress.EmployeeId.ToString()), "EmployeeId");
            //    content.Add(new StringContent(empAddress.StreetAddress), "Street Address");
            //    content.Add(new StringContent(empAddress.City), "City");
            //    content.Add(new StringContent(empAddress.State), "State");
            //    content.Add(new StringContent(empAddress.Country), "Country");
            //    content.Add(new StringContent(empAddress.Pincode.ToString()), "Pincode");

            //    using (var response = await httpClient.PutAsync("https://localhost:44309/api/EmpAddresses", content))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        ViewBag.Result = "Success";
            //        empEditObj = JsonConvert.DeserializeObject<EmpAddress>(apiResponse);
            //    }
            //}
            //return View(empEditObj);
        }
        [HttpGet]
        public async Task<IActionResult> AddressDelete(string id)
        {
            var empAddress = new EmpAddress();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:44309/api/EmpAddresses/{id}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                empAddress = JsonConvert.DeserializeObject<EmpAddress>(apiResponse);
            }

            return View(empAddress);
            
           

        }
        [HttpPost]
        public async Task<IActionResult> AddressDelete(string id,EmpAddress empAddress)
        {
          
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:44309/api/EmpAddresses/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Message = response.Content.ReadAsStringAsync().Result;               
                }
            }

            return RedirectToAction("Index");
            //using (var httpClient = new HttpClient())
            //{
            //    HttpResponseMessage response = httpClient.DeleteAsync($"https://localhost:44309/api/EmpAddresses/{employeeId}").Result;
            //    TempData["Message"] = response.Content.ReadAsStringAsync().Result;
            //    return RedirectToAction ("Index");

            //}

        }

        //public 

    //     using (var httpClient = new HttpClient())
    //        {
    //            HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44309/api/EmpAddresses/{employeeId}");
    //string stringData = response.Content.ReadAsStringAsync().Result;
    //EmpAddress data = JsonConvert.DeserializeObject<EmpAddress>(stringData);

//}

//return View("data");
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
