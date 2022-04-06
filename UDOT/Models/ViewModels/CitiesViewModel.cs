using System;
using System.Linq;

namespace UDOT.Models.ViewModels
{
    public class CitiesViewModel
    {
        public IQueryable<Crash> Crashes { get; set; }
        public IQueryable<Crash> Cities { get; set; }

    }
}
