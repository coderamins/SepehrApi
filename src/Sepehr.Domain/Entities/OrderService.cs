using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class OrderService:BaseEntity<int>
    {
        public Guid OrderId { get; set; }
        public int ServiceId { get; set; }
        public string? Description { get; set; }

        public virtual required Service Service { get; set; }
        public virtual required Order Order { get; set; }
    }
}
