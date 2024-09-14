using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class OrderDetailReturn:AuditableBaseEntity<int>
    {
        public int OrderDetailId { get; set; }
        public int OrderReturnId { get; set; }
        public decimal ReturnedAmount { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
        public virtual OrderReturn OrderReturn { get; set; }
    }
}
