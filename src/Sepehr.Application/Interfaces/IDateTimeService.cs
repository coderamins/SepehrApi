using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        DateTime ToDateTime(string objDate, string time = "00:00");
    }
}
