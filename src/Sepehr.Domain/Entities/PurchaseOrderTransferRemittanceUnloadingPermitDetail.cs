using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrderTransferRemittanceUnloadingPermitDetail : BaseEntity<int>
    {
        public Guid PurchaseOrderTransferRemittanceUnloadingPermitId { get; set; }
        public int PurchaseOrderTransferRemittanceDetailId { get; set; }
        public decimal UnloadedAmount { get; set; }

        public virtual required PurchaseOrderTransferRemittanceUnloadingPermit PurchaseOrderTransferRemittanceUnloadingPermit { get; set; }
        public virtual required PurchaseOrderTransferRemittanceDetail PurchaseOrderTransferRemittanceDetail { get; set; }

    }
}
