using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrderTransferDetail : BaseEntity<int>
    {
        public int PurchaseOrderDetailId { get; set; }
        public decimal TransferedAmount { get; set; }

        public required virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }
    }
}
