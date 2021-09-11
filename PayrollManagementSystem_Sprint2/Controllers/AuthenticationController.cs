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

namespace PayrollManagementSystem_Sprint2.Controllers
{

    public class AuthenticationController : Controller
    {
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        
        public ActionResult Login()
        {
            return View();
        }

        
        

       

    }

}
