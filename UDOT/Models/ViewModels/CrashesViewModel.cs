﻿using System;
using System.Linq;

namespace UDOT.Models.ViewModels
{
    public class CrashesViewModel
    {
        public IQueryable<Crash> crashes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
