using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public interface IWorkedHoursCalculator
    {
        TimeSpan Calculate(IWorkDay workDay);
    }
}