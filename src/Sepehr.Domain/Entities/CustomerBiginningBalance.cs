using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CustomerBiginningBalance:AuditableBaseEntity<int>
    {
        public Guid CustomerId { get; set; }
        public decimal BeginningBalance { get; set; }
        public int FiscalYearId { get; set; }

        public virtual required FiscalYear FiscalYear { get; set; }
        public virtual required Customer Customer { get; set; }
    }
}
