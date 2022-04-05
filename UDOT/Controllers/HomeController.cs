using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UDOT.Models;

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
            <List>Crash crashes;
            crashes = _context.Crashes
                   .OrderBy(c => c.Crash_Datetime)
                   .ToList();
            return View(crashes);
        }



        //------------------ ADD ------------------//
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
        [HttpGet]
        public IActionResult UpdateCrashForm()
        {
            ViewBag.Teams = _context.Teams.ToList();
            //Crash c = _context.Crashes.FirstOrDefault(c => c.CRASH_ID == id);
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCrash([FromForm] Crash crash)
        {
            return RedirectToAction("CrashDetailsList");
        }


    }
}
