using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrderTransfer:AuditableBaseEntity<Guid>
    {
        public Guid PurchaseOrderId { get; set; }

        public required virtual ICollection<PurchaseOrderTransferDetail> PurchaseOrderTransferDetails { get; set; }
        public required virtual PurchaseOrder PurchaseOrder { get; set; }    
    }
}
