using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class DriverFareAmountApprove:AuditableBaseEntity<int>
    {
        public string Description { get; set; } = string.Empty;
        public Guid? PurOrderTransRemittUnloadingPermitId { get; set; }
        public Guid? LadingExitPermitId { get; set; }

        public virtual UnloadingPermit? PurOrderTransRemittUnloadingPermit { get; set; }
        public virtual LadingExitPermit? LadingExitPermit { get; set; }
    }
}
