using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class Warehouse : PrimaryBaseEntity<int>
    {
        public required string Name { get; set; }
        public int WarehouseTypeId { get; set; }

        public virtual WarehouseType WarehouseType { get; set; }
        public virtual ICollection<Product>? Products { get; set; } 
        public ICollection<CustomerWarehouse> CustomerWarehouses { get; set; }

    }
}
