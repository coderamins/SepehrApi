using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.CustomerWarehouse
{
    public class CustomerWarehouseDto
    {
        public Guid CustomerId { get; set; }
        public int WarehouseId { get; set; }
    }
}
