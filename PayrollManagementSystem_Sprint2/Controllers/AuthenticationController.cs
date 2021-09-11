using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayrollManagementSystem_Sprint2.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using PayrollManagementSystem_API.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace PayrollManagementSystem_Sprint2.Controllers
{

    public class AuthenticationController : Controller
    {
        private HttpContent content;
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public async Task<EmployeeMaster> IsValidEmployee(AuthorizationResponse authResponse)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44314/api/");
                client.DefaultRequestHeaders.Clear();

                //Defining Request Data Format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Result = await client.PostAsync("Employee/login", content);
                if (Result.IsSuccessStatusCode)
                {
                    //storing the response details recieved from web api
                    var employeeResponse = Result.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web Api and storing into the mployee list
                    var resultValue = JsonConvert.DeserializeObject<LoginGet>(employeeResponse);
                    HttpContext.Session.SetString("Token", resultValue._applicationSideToken);
                    return resultValue.EmpMasterObj;
                }
                else
                {
                    return null;
                }
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        public ActionResult Login(AuthorizationResponse model)
        {
            if (ModelState.IsValid)
            {
                //validation of the Employee weather the employee is valid or Not
                var isValidUser = IsValidEmployee(model).Result;

                //If the employee Is Valid and present in Db we redirect them to their respective Pages that is weather
                //admin page or Employee Page
                if (isValidUser != null)
                {
                    string isAdminString = null;
                    if (isValidUser.AdminPrivilege == true)
                    {
                        isAdminString = "True";
                    }
                    else
                    {
                        isAdminString = "False";
                    }
                    HttpContext.Session.SetString("EmployeeID", isValidUser.EmployeeId);
                    HttpContext.Session.SetString("EmpFirstName", isValidUser.EmployeeFirstname);
                    HttpContext.Session.SetString("AdminPrivelege", isAdminString);
                    return HttpContext.Session.GetString("AdminPrivelege") == "True" ? RedirectToAction("AdminController", "Index") : RedirectToAction("EmployeeController", "Index");
                    //return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "You entered wrong username and password Combination");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }







    }

}
