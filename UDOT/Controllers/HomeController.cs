using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UDOT.Models;
using UDOT.Models.ViewModels;

namespace UDOT.Controllers
{
    public class HomeController : Controller
    {
        private CrashDbContext _context { get; set; }

        public HomeController(CrashDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        //------------------ READ LIST ------------------//

        
        public IActionResult CrashDetailsList()
        {
            List<Crash> crashes = _context.Crashes.ToList();
            return View(crashes);
        }



        //------------------ ADD ------------------//
        public IActionResult CreateCrashForm()
        {
            ViewBag.Teams = _context.Crashes.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateCrash([FromForm] Crash crash)
        {
            _context.Add(crash);
            _context.SaveChanges();
            return RedirectToAction("CrashDetailsList");
        }

        //------------------ EDIT(UPDATE) ------------------//
        [Authorize]
        [HttpGet]
        [Route("/Home/UpdateCrashForm/{id}")]
        public IActionResult UpdateCrashForm(int id)
        {
            ViewBag.Crashes = _context.Crashes.ToList();
            Crash c = _context.Crashes.FirstOrDefault(c => c.CRASH_ID == id);
            return View(c);
        }

        [HttpPost]
        public IActionResult UpdateCrash([FromForm] Crash crash)
        {
            _context.Update(crash);
            _context.SaveChanges();
            return RedirectToAction("CrashDetailsList");
        }

        //---------------- Delete -------------------------//
        [Authorize]
        [Route("/Home/DeleteCrash/{id}")]
        public IActionResult DeleteCrash(int id)
        {
            Crash c = _context.Crashes.FirstOrDefault(c => c.CRASH_ID == id);
            _context.Crashes.Remove(c);
            _context.SaveChanges();
            return RedirectToAction("CrashDetailsList");
        }
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
