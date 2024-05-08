using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class LadingExitPermitDetail:AuditableBaseEntity<Guid>
    {
        public Guid LadingExitPermitId { get; set; }
        public int CargoAnnounceDetailId { get; set; }

        [DataType(DataType.Currency)]
        public decimal RealAmount { get; set; }
        public int ProductSubUnitId { get; set; }
        public decimal ProductSubUnitAmount { get; set; }

        public required virtual ProductUnit ProductSubUnit { get; set; }
        public virtual required CargoAnnounceDetail CargoAnnounceDetail { get; set; }
        public required virtual LadingExitPermit LadingExitPermit { get; set; }

    }
}
