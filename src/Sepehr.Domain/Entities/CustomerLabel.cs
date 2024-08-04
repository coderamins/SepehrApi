using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CustomerLabel : AuditableBaseEntity<int>
    {
        //public Guid CustomerId { get; set; }
        public int CustomerLabelTypeId { get; set; }
        public string LabelNameCode { get; set; } = string.Empty;
        public string LabelName { get; set; } = string.Empty;

        //public virtual required Customer Customer { get; set; }
        public virtual required CustomerLabelType CustomerLabelType { get; set; }
    }
}
