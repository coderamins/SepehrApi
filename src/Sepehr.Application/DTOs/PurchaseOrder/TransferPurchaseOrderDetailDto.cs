using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.PurchaseOrder
{
    public class TransferPurchaseOrderDetailDto
    {
        public int PurchaseOrderDetailId { get; set; }
        public string ProductBrandName { get; set; }
        public decimal TransferedAmount { get; set; }

    }
}
