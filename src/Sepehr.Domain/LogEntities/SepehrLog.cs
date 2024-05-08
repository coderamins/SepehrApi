using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.LogEntities
{
    public class SepehrLog:AuditableBaseEntityLog<int>
    {
        public string? ErrorMessage { get; set; }
        public string? ErrorSource { get; set; }
    }
}
