using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UDOT.Models;
using UDOT.Models.ViewModels;

namespace UDOT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        //------------------ READ LIST ------------------//

        
        public IActionResult CrashDetailsList()
        {
            return View();
        }



        //------------------ ADD ------------------//
        [Authorize]
        [HttpGet]
        public IActionResult CreateCrashForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCrash([FromForm] Crash crash)
        {
            return RedirectToAction("CrashDetailsList");
        }



        //------------------ EDIT(UPDATE) ------------------//
        [Authorize]
        [HttpGet]
        public IActionResult UpdateCrashForm()
        {
            //ViewBag.Teams = _context.Teams.ToList();
            //Crash c = _context.Crashes.FirstOrDefault(c => c.CRASH_ID == id);
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCrash([FromForm] Crash crash)
        {
            return RedirectToAction("CrashDetailsList");
        }

        //---------------- Delete -------------------------//
        //[Authorize]
        //[HttpGet]
    }
    //public class AccountController:Controller
    //{ 

    //    //--- Admin Functionality ---//
    //    private UserManager<IdentityUser> userManager;
    //    private SignInManager<IdentityUser> signInManager;

    //    public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
    //    {
    //        userManager = um;
    //        signInManager = sim;
    //    }

    //    [HttpGet]
    //    public IActionResult Login(string returnUrl)
    //    {
    //        return View(new LoginModel { ReturnUrl = returnUrl });
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Login(LoginModel loginModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            IdentityUser user = await userManager.FindByNameAsync(loginModel.Username);

    //            if (user != null)
    //            {
    //                await signInManager.SignOutAsync();

    //                if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
    //                {
    //                    return Redirect(loginModel?.ReturnUrl ?? "/Admin");
    //                }
    //            }
    //        }

    //        ModelState.AddModelError("", "invalid name or password");
    //        return View(loginModel);
    //    }

    //    public async Task<RedirectResult> Logout(string returnUrl = "/")
    //    {
    //        await signInManager.SignOutAsync();

    //        return Redirect(returnUrl);
    //    }


    //}
}
