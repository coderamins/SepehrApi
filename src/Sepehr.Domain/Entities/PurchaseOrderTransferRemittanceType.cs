using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrderTransferRemittanceType
    {
        public int Id { get; set; }
        public string RemittanceTypeDesc { get; set; } = string.Empty;
    }
}
