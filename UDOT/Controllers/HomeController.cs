using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UDOT.Models;

namespace UDOT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
            //ViewBag.Teams = _context.Teams.ToList();
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
