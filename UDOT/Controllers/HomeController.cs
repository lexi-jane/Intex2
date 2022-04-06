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



        //------------------ READ LIST For Everyone ------------------//
        public IActionResult AllList(string countySelect, int pageNum = 1)
        //public IActionResult CrashDetailsList(DateTime crashDate, int pageNum = 1)

        {
            int pageSize = 50;

            var x = new CrashesViewModel
            {
                Crashes = _context.Crashes
                .Where(p => p.County_Name == countySelect || countySelect == null)
                .OrderBy(p => p.County_Name)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                    (countySelect == null ? _context.Crashes.Count()
                    : _context.Crashes.Where(p => p.County_Name == countySelect).Count()),
                    //TotalNumCrashes = _context.Crashes.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

        //~~~~~~~~~~~~~~~ PAGINATION ~~~~~~~~~~~~~~~//
        [Authorize]
        public IActionResult CrashDetailsList(string countySelect, int pageNum = 1)
        //public IActionResult CrashDetailsList(DateTime crashDate, int pageNum = 1)

        {
            int pageSize = 50;

            var x = new CrashesViewModel
            {
                Crashes = _context.Crashes
                .Where(p => p.County_Name == countySelect || countySelect == null)
                .OrderBy(p => p.County_Name)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                    (countySelect == null ? _context.Crashes.Count()
                    : _context.Crashes.Where(p => p.County_Name == countySelect).Count()),
                    //TotalNumCrashes = _context.Crashes.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }




        //~~~~~~~~~~~~~~~ ADMIN FUNCTIONS ~~~~~~~~~~~~~~~//


        //------------------ Add ------------------//
        [Authorize]
        [HttpGet]
        public IActionResult CreateCrashForm()
        {
            ViewBag.Crashes = _context.Crashes.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateCrash([FromForm] Crash crash)
        {
            int newCrashID = _context.Crashes.OrderBy(x => x.CRASH_ID).Last().CRASH_ID;
            crash.CRASH_ID = newCrashID + 1;
            crash.Crash_Date = crash.Crash_Date.Date;
            _context.Add(crash);
            _context.SaveChanges();
            return RedirectToAction("CrashDetailsList");
        }

        //------------------ Edit/Update ------------------//
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
            crash.Crash_Date = crash.Crash_Date.Date;
            _context.Update(crash);
            _context.SaveChanges();
            return RedirectToAction("CrashDetailsList");
        }

        //---------------- Delete -----------------//
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
