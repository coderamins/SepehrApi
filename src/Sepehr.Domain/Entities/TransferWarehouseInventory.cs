using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class TransferWarehouseInventory:AuditableBaseEntity<int>
    {
        public int OriginWarehouseId { get; set; }
        public Guid PurchaseOrderId { get; set; }

        public virtual required PurchaseOrder PurchaseOrder { get; set; }
        public virtual ICollection<TransferWarehouseInventoryDetail> Details { get; set; }=new List<TransferWarehouseInventoryDetail>();
    }
}
