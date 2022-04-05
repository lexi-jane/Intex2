using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UDOT.Models;

namespace UDOT.Components
{
    public class SeverityViewComponent : ViewComponent
    {
        private ICrashRepository _context { get; set; }

        public SeverityViewComponent (ICrashRepository temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedSeverity = RouteData?.Values["severity"];

            var severityOptions = _context.Crashes.Select(x => x.Crash_Severity_ID).Distinct().OrderBy(x => x);

            return View(severityOptions);
        }
    }
}
