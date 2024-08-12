using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class TransferWarehouseInventory:BaseEntity<int>
    {
        public int OriginWarehouseId { get; set; }
        public int Amount { get; set; }

    }
}
