﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloApp.Services
{
    public class TimeService
    {
        public string GetTime() => DateTime.Now.ToString("hh:mm:ss");
    }
}
