using System;
namespace UDOT.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumCrashes { get; set; }
        public int CrashesPerPage { get; set; }
        public int CurrentPage { get; set; }

        //calculates how many pages are needed
        public int TotalPages => (int)Math.Ceiling((double)TotalNumCrashes / CrashesPerPage);
    }

    public class PageInfo2
    {
        public int TotalNumCrashes2 { get; set; }
        public int CrashesPerPage2 { get; set; }
        public int CurrentPage2 { get; set; }

        //calculates how many pages are needed
        public int TotalPages2 => (int)Math.Ceiling((double)TotalNumCrashes2 / CrashesPerPage2);
    }
}
