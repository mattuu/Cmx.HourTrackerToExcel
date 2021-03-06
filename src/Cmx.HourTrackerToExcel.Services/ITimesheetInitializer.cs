﻿using System;
using System.Collections.Generic;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public interface ITimesheetInitializer
    {
        ITimesheet Initialize(IEnumerable<IWorkDay> workDays);
    }
}