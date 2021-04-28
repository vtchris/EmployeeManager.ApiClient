using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.ApiClient.Models;
using EmployeeManager.ApiClient.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.ApiClient.Controllers
{    
    public class SecurityController : Controller
    {
        // these will be set in the constructor
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;
        private readonly SignInManager<AppIdentityUser> signInManger;

        public SecurityController(
            UserManager<AppIdentityUser> userManager, 
            RoleManager<AppIdentityRole> roleManager, 
            SignInManager<AppIdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManger = signInManager;
        }

        // Get is the default, this annotation is not strickly needed here.
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            if(ModelState.IsValid)
            {
                if(!roleManager.RoleExistsAsync("Manager").Result)
                {
                    var role = new AppIdentityRole();
                    role.Name = "Manager";
                    role.Description = "Can Perform CRUD Operations";
                    var roleResult = await roleManager.CreateAsync(role);
                }

                var user = new AppIdentityUser();
                user.UserName = register.UserName;
                user.Email = register.Email;
                user.FullName = register.FullName;
                user.Birthdate = register.BirthDate;

                var result = await userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                    return RedirectToAction("SignIn", "Security");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User");
                }

            }            
            return View(register); 
        }     

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignIn signIn)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManger.PasswordSignInAsync(signIn.UserName, signIn.Password, signIn.RememberMe, false);
                
                if(result.Succeeded)
                {
                    return RedirectToAction("List", "EmployeeManager");
                }
                else
                {
                    ModelState.AddModelError("", "Logon failed");
                }
                
            }
            return View(signIn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult SignOut()
        {
            signInManger.SignOutAsync().Wait();
            return RedirectToAction("SignIn", "Security");

        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
