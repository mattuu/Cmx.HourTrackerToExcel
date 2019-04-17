using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public interface IWorkedHoursCalculator
    {
        void AdjustTimes(IWorkDay workDay);

        void VerifyTimes(IWorkDay workDay);
    }
}