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
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using UDOT.Models;
using UDOT.Models.ViewModels;

namespace UDOT.Controllers
{
    public class HomeController : Controller
    {
        private CrashDbContext _context { get; set; }
        private InferenceSession _session;

        public HomeController(CrashDbContext context, InferenceSession session)
        {
            _context = context;
            _session = session;
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

        //---------------- Delete Confirmation / Deletion -----------------//
        [Authorize]
        [Route("/Home/DeleteConfirmation/{id}")]
        public IActionResult DeleteConfirmation(int id)
        {
            Crash crash = _context.Crashes.FirstOrDefault(c => c.CRASH_ID == id);
            return View("DeleteConfirmation", crash);
        }
        [Authorize]
        [Route("/Home/DeleteCrash/{id}")]
        public IActionResult DeleteCrash(int id)
        {
            Crash c = _context.Crashes.FirstOrDefault(c => c.CRASH_ID == id);
            _context.Crashes.Remove(c);
            _context.SaveChanges();
            return RedirectToAction("CrashDetailsList");
        }

        [HttpGet]
        public IActionResult Calculator()
        {
            return View();
        }

        //---------------- Predictor -----------------//
        [HttpPost]
        public IActionResult Calculator(crashData data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();
            // Rounding to nearest integer for predictor and setting a limit on how high the predictor can calculate
            if (prediction.PredictedValue >= 4.5)
            {
                prediction.PredictedValue = 5;
            }
            else if (prediction.PredictedValue >= 3.5)
            {
                prediction.PredictedValue = 4;
            }
            else if (prediction.PredictedValue >= 2.5)
            {
                prediction.PredictedValue = 3;
            }
            else if (prediction.PredictedValue >= 1.5)
            {
                prediction.PredictedValue = 2;
            }
            else if (prediction.PredictedValue >= .5)
            {
                prediction.PredictedValue = 1;
            }
            return RedirectToAction("Calculation", prediction);
        }

        //---------------- Display Predictor Results -----------------//
        public IActionResult Calculation(Prediction prediction)
        {
            return View(prediction);
        }



    }
}
