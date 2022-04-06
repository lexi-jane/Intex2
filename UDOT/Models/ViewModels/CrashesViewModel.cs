using System;
using System.Linq;

namespace UDOT.Models.ViewModels
{
    public class CrashesViewModel
    {
        public IQueryable<Crash> Crashes { get; set; }
        public PageInfo PageInfo { get; set; }
        public PageInfo2 PageInfo2 { get; set; }

    }
}
