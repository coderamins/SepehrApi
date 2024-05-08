using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrderTransferRemittanceStatus
    {
        public int Id { get; set; }
        public string StatusDesc { get; set; } = string.Empty;
    }
}
