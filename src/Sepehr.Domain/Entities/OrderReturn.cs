using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class OrderReturn:AuditableBaseEntity<int>
    {
        public Guid OrderId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal ReturnedAmount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int ReturnStatusId { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<OrderDetailReturn> OrderDetailReturns { get; set; }
    }
}
