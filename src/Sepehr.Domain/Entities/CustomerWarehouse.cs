using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CustomerWarehouse:BaseEntity<int>
    {
        public required Guid CustomerId { get; set; }
        public required int WarehouseId { get; set; }

        public required virtual Customer Customer { get; set; }  
        public required virtual Warehouse Warehouse { get; set; }
    }
}
