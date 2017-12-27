using System.Collections.Generic;
using System.IO;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Import
{
    public interface ICsvDataReader
    {
        IEnumerable<ICsvLine> Read(Stream stream);
    }
}