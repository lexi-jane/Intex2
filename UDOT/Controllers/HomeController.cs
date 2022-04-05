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


        //------------------ Landing Page ------------------//
        public IActionResult Index()
        {
            return View();
        }



        //------------------ READ LIST ------------------//
        [Authorize]
        public IActionResult CrashDetailsList(string countySelect)
        {
            List<Crash> crashes = _context.Crashes
                .Where(x => x.County_Name == countySelect || countySelect == null)
                .ToList();
            return View(crashes);
        }




        //------------------ ADMIN FUNCTIONS ------------------//


        //------------------ ADD ------------------//
        [Authorize]
        public IActionResult CreateCrashForm()
        {
            ViewBag.Teams = _context.Crashes.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateCrash([FromForm] Crash crash)
        {
            int newCrashID = _context.Crashes.OrderBy(x => x.CRASH_ID).Last().CRASH_ID;
            crash.CRASH_ID = newCrashID + 1;
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
}
